using FluentAssertions;
using Hospital.Domain.Departments;
namespace Hospital.Domain.Tests.Departments
{
    public class DepartmentTests
    {

     
        #region Constructor
        [Fact]
        public void Department_CanBeInstantiated()
        {
            var departmentId = Guid.NewGuid();
            var departmentName = "Cardiology";
            var department = new Department(departmentId, departmentName);
            department.Id.Should().Be(departmentId);
            department.Name.Should().Be(departmentName);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Department_InvalidName_ThrowsArgumentException(string? invalidName)
        {
            var departmentId = Guid.NewGuid();

            Action action = () =>
                new Department(departmentId, invalidName!);

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage("Department name cannot be null or whitespace. (Parameter 'name')")
                .WithParameterName("name");
        }
        [Fact]
        public void Department_NameTrimmed()
        {
            var department = new Department(Guid.NewGuid(), " Cardiology ");
            department.Name.Should().Be("Cardiology");
        }
        #endregion

        #region AddSubDepartment

        [Fact]
        public void AddSubDepartment_ValidChild_AddsChildSuccessfully()
        {
            var parentDepartment = new Department(
                Guid.NewGuid(), "Cardiology");

            var child = new Department(
                Guid.NewGuid(), "Pediatrics");

            parentDepartment.AddSubDepartment(child);

            parentDepartment.Children.Should().Contain(child);
            child.Parent.Should().Be(parentDepartment);
            child.ParentDepartmentId.Should().Be(parentDepartment.Id);
        }
        [Fact]
        public void AddSubDepartment_NullChild_ThrowsArgumentNullException()
        {
            var parentDepartment = new Department(Guid.NewGuid(), "Cardiology");
            Action act = () => parentDepartment.AddSubDepartment(null!);

            act.Should()
     .Throw<ArgumentNullException>()
     .WithParameterName("child");
        }
        [Fact]
        public void AddSubDepartment_DuplicateChild_ThrowsInvalidOperationException()
        {
            var parentDepartment =
                new Department(Guid.NewGuid(), "Cardiology");

            var childId = Guid.NewGuid();

            var firstChild =
                new Department(childId, "Pediatrics");

            var secondChild =
                new Department(childId, "Pediatrics");

            parentDepartment.AddSubDepartment(firstChild);

            Action act = () =>
                parentDepartment.AddSubDepartment(secondChild);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("The specified child already exists.");

            parentDepartment.Children.Should().Contain(firstChild);
        }
        [Fact]
        public void AddSubDepartment_SelfReference_ThrowsInvalidOperationException()
        {
            var department =
                new Department(Guid.NewGuid(), "Cardiology");

            Action act = () => department.AddSubDepartment(department);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Cannot add a department as a child of itself.");

            department.Children.Should().BeEmpty();
            department.Parent.Should().BeNull();
        }

        [Fact]
        public void AddSubDepartment_ChildAlreadyHasParent_ThrowsInvalidOperationException()
        {
            var parent1 =
                new Department(Guid.NewGuid(), "Pediatrics");

            var parent2 =
                new Department(Guid.NewGuid(), "Cardiology");

            var childDepartment =
                new Department(Guid.NewGuid(), "Neurology");

            parent1.AddSubDepartment(childDepartment);

            Action act = () =>
                parent2.AddSubDepartment(childDepartment);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("The specified child already has a parent.");

            childDepartment.Parent.Should().BeSameAs(parent1);
            childDepartment.ParentDepartmentId.Should().Be(parent1.Id);

            parent1.Children.Should().Contain(childDepartment);
            parent2.Children.Should().NotContain(childDepartment);
        }
     
        [Fact]
        public void AddSubDepartment_CircularHierarchy_ThrowsInvalidOperationException()
        {
            var root =
                new Department(Guid.NewGuid(), "Hospital");

            var cardiology =
                new Department(Guid.NewGuid(), "Cardiology");

            var neurology =
                new Department(Guid.NewGuid(), "Neurology");

            root.AddSubDepartment(cardiology);
            cardiology.AddSubDepartment(neurology);

            Action act = () =>
                neurology.AddSubDepartment(root);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Cannot create a circular department hierarchy.");
            root.Parent.Should().BeNull();
            cardiology.Parent.Should().BeSameAs(root);
            neurology.Parent.Should().BeSameAs(cardiology);

            root.Children.Should().Contain(cardiology);
            cardiology.Children.Should().Contain(neurology);
            neurology.Children.Should().NotContain(root);
        }
        #endregion

        #region RemoveSubDepartment
        [Fact]
        public void RemoveSubDepartment_ValidChild_RemovesChildSuccessfully()
        {
            var child = new Department(Guid.NewGuid(), "Pediatrics");
            var parentDepartment = new Department(Guid.NewGuid(), "Cardiology");
            parentDepartment.AddSubDepartment(child);
            parentDepartment.RemoveSubDepartment(child);
           parentDepartment.Children.Should().NotContain(child);
            parentDepartment.HasChildren().Should().BeFalse();
        }
        [Fact]
        public void RemoveSubDepartment_PreservesRemovedDepartmentChildren()
        {
            var root = new Department(Guid.NewGuid(), "Hospital");
            var parent = new Department(Guid.NewGuid(), "Cardiology");
            var child = new Department(Guid.NewGuid(), "ICU");

            root.AddSubDepartment(parent);
            parent.AddSubDepartment(child);

            root.RemoveSubDepartment(parent);

            root.Children.Should().NotContain(parent);
            parent.Parent.Should().BeNull();
            parent.Children.Should().Contain(child);
            child.Parent.Should().Be(parent);
        }
        [Fact]
        public void RemoveSubDepartment_ClearsChildParentRelationship()
        {
            var parentDepartment = new Department(Guid.NewGuid(), "Cardiology");
            var child = new Department(Guid.NewGuid(), "Pediatrics");
            var child2 = new Department(Guid.NewGuid(), "Surgery");

            parentDepartment.AddSubDepartment(child);
            child.AddSubDepartment(child2);

            parentDepartment.RemoveSubDepartment(child);

            parentDepartment.Children.Should().NotContain(child);
            child.Parent.Should().BeNull();
            child.ParentDepartmentId.Should().BeNull();
            parentDepartment.HasChildren().Should().BeFalse();
        }
        [Fact]
        public void RemoveSubDepartment_NonExistentChild_ThrowsInvalidOperationException()
        {
            var parentDepartment = new Department(Guid.NewGuid(), "Cardiology");
            var nonExistentChild = new Department(Guid.NewGuid(), "Pediatrics");
            Action act = () => parentDepartment.RemoveSubDepartment(nonExistentChild);
            act.Should()
      .Throw<InvalidOperationException>()
      .WithMessage("The specified child does not exist.");

            parentDepartment.Children.Should().BeEmpty();
            nonExistentChild.Parent.Should().BeNull();

        }
        [Fact]
        public void RemoveSubDepartment_NullChild_ThrowsArgumentNullException()
        {
            var parentDepartment = new Department(Guid.NewGuid(), "Cardiology");
            Action act = () => parentDepartment.RemoveSubDepartment(null!);
            act.Should()
     .Throw<ArgumentNullException>()
     .WithParameterName("child");
        }
        #endregion

        #region HasChildren
        [Fact]
        public void HasChildren_WithChildren_ReturnsTrue()
        {
            var child = new Department(Guid.NewGuid(), "Pediatrics");
            var parentDepartment = new Department(Guid.NewGuid(), "Cardiology");
            parentDepartment.AddSubDepartment(child);
            parentDepartment.HasChildren().Should().BeTrue();
        }
        [Fact]
        public void HasChildren_WithNoChildren_ReturnsFalse()
        {
            var parentDepartment = new Department(Guid.NewGuid(), "Cardiology");
            parentDepartment.HasChildren().Should().BeFalse();
        }
        [Fact]
        public void HasChildren_AfterRemovingChild_ReturnsFalse()
        {
            var child = new Department(Guid.NewGuid(), "Pediatrics");
            var parentDepartment = new Department(Guid.NewGuid(), "Cardiology");
            parentDepartment.AddSubDepartment(child);
            parentDepartment.RemoveSubDepartment(child);
            parentDepartment.HasChildren().Should().BeFalse();
        }
        #endregion

        #region FindDepartmentRecursive
        [Fact]
        public void FindDepartmentRecursive_FindsCurrentDepartment_ReturnsDepartment()
        {
            var child = new Department(Guid.NewGuid(), "Pediatrics");
            var parentDepartment = new Department(Guid.NewGuid(), "Cardiology");
            parentDepartment.AddSubDepartment(child);
            parentDepartment.FindDepartmentRecursive(parentDepartment.Id).Should().Be(parentDepartment);
        }
        [Fact]
        public void FindDepartmentRecursive_FindsDirectChild_ReturnsDepartment()
        {
            var child = new Department(Guid.NewGuid(), "Pediatrics");
            var parentDepartment = new Department(Guid.NewGuid(), "Cardiology");
            parentDepartment.AddSubDepartment(child);
            parentDepartment.FindDepartmentRecursive(child.Id).Should().Be(child);
        }
        [Fact]
        public void FindDepartmentRecursive_FindsNestedDepartment_ReturnsDepartment()
        {
            var child = new Department(Guid.NewGuid(), "Cardiology");
            var child1 = new Department(Guid.NewGuid(), "Pediatrics");
            var child2 = new Department(Guid.NewGuid(), "Neurology");
            var parentDepartment = new Department(Guid.NewGuid(), "Surgery");
            parentDepartment.AddSubDepartment(child);
            child.AddSubDepartment(child1);
            child1.AddSubDepartment(child2);
            parentDepartment.FindDepartmentRecursive(child2.Id).Should().Be(child2);
        }
        [Fact]
        public void FindDepartmentRecursive_FindsNonExistingDepartment_ReturnsNull()
        {
            var child = new Department(Guid.NewGuid(), "Pediatrics");
            var parentDepartment = new Department(Guid.NewGuid(), "Cardiology");
            parentDepartment.AddSubDepartment(child);
            parentDepartment.FindDepartmentRecursive(Guid.NewGuid()).Should().BeNull();
        }
        #endregion

        #region Employee Assignment
        [Fact]
        public void AssignEmployee_ValidEmployee_AssignsSuccessfully()
        {
            var department = new Department(Guid.NewGuid(), "Cardiology");
            var employeeId = Guid.NewGuid();
            department.AssignEmployee(employeeId);
            department.AssignedEmployeeIds.Should().Contain(employeeId);
        }
        [Fact]
        public void AssignEmployee_EmptyEmployeeId_ThrowsArgumentException()
        {
            var department = new Department(Guid.NewGuid(), "Cardiology");
            Action act = () => department.AssignEmployee(Guid.Empty);
            act.Should()
       .Throw<ArgumentException>()
       .WithMessage("Employee ID cannot be empty. (Parameter 'employeeId')")
       .WithParameterName("employeeId");

            department.AssignedEmployeeIds.Should().BeEmpty();
        }
        [Fact]
        public void AssignEmployee_DuplicateEmployee_ThrowsInvalidOperationException()
        {
            var department = new Department(Guid.NewGuid(), "Cardiology");
            var employeeId = Guid.NewGuid();
            department.AssignEmployee(employeeId);
            Action act = () => department.AssignEmployee(employeeId);
            act.Should()
      .Throw<InvalidOperationException>()
      .WithMessage("Employee is already assigned to this department.");

            department.AssignedEmployeeIds.Should()
                .ContainSingle()
                .Which.Should().Be(employeeId);

        }
        [Fact]
        public void UnassignEmployee_ValidEmployee_RemovesSuccessfully()
        {
            var department = new Department(Guid.NewGuid(), "Cardiology");
            var employeeId = Guid.NewGuid();
            department.AssignEmployee(employeeId);
            department.UnassignEmployee(employeeId);
            department.AssignedEmployeeIds.Should().NotContain(employeeId);
        }
        [Fact]
        public void UnassignEmployee_NonExistingEmployee_ThrowsInvalidOperationException()
        {
            var department = new Department(Guid.NewGuid(), "Cardiology");
            Action act = () => department.UnassignEmployee(Guid.NewGuid());
            act.Should()
    .Throw<InvalidOperationException>()
    .WithMessage("Employee is not assigned to this department.");
            department.AssignedEmployeeIds.Should().BeEmpty();
        }
        [Fact]
        public void UnassignEmployee_EmptyEmployeeId_ThrowsArgumentException()
        {
            var department = new Department(Guid.NewGuid(), "Cardiology");
            Action act = () => department.UnassignEmployee(Guid.Empty);
            act.Should()
    .Throw<ArgumentException>()
    .WithMessage("Employee ID cannot be empty. (Parameter 'employeeId')")
    .WithParameterName("employeeId");
        }
        #endregion

        #region Employee Reassignment
        [Fact]
        public void ReassignEmployee_TargetNotFound_ThrowsInvalidOperationException()
        {
            var rootDepartment =
                new Department(Guid.NewGuid(), "Hospital");

            var sourceDepartment =
                new Department(Guid.NewGuid(), "Cardiology");

            rootDepartment.AddSubDepartment(sourceDepartment);

            var employeeId = Guid.NewGuid();
            var nonExistingTargetId = Guid.NewGuid();

            sourceDepartment.AssignEmployee(employeeId);

            Action act = () =>
                sourceDepartment.ReassignEmployee(
                    employeeId,
                    nonExistingTargetId,
                    rootDepartment);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Target department not found.");
            sourceDepartment.AssignedEmployeeIds.Should()
                .Contain(employeeId);
        }
        [Fact]
        public void ReassignEmployee_ValidEmployeeBetweenBranches_ReassignsSuccessfully()
        {
            var rootDepartment = new Department(Guid.NewGuid(), "Hospital");
            var department1 = new Department(Guid.NewGuid(), "Cardiology");
            var department2 = new Department(Guid.NewGuid(), "Neurology");
            var department3 = new Department(Guid.NewGuid(), "Pediatrics");
            var department4 = new Department(Guid.NewGuid(), "Oncology");
            var employeeId = Guid.NewGuid();
            rootDepartment.AddSubDepartment(department1);
            rootDepartment.AddSubDepartment(department2);
            department2.AddSubDepartment(department3);
            department2.AddSubDepartment(department4);
            department4.AssignEmployee(employeeId);
            department4.ReassignEmployee(employeeId, department1.Id, rootDepartment);
            department4.AssignedEmployeeIds.Should().NotContain(employeeId);
            department1.AssignedEmployeeIds.Should().Contain(employeeId);
        }
        [Fact]
        public void ReassignEmployee_EmptyEmployeeId_ThrowsArgumentException()
        {
            var sourceDepartment =
                new Department(Guid.NewGuid(), "Cardiology");

            var rootDepartment =
                new Department(Guid.NewGuid(), "Hospital");

            Action act = () =>
                sourceDepartment.ReassignEmployee(
                    Guid.Empty,
                    Guid.NewGuid(),
                    rootDepartment);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("Employee ID cannot be empty. (Parameter 'employeeId')")
                .WithParameterName("employeeId");
        }
        [Fact]
        public void ReassignEmployee_EmptyTargetDepartmentId_ThrowsArgumentException()
        {
            var sourceDepartment =
                new Department(Guid.NewGuid(), "Cardiology");

            var rootDepartment =
                new Department(Guid.NewGuid(), "Hospital");

            var employeeId = Guid.NewGuid();

            sourceDepartment.AssignEmployee(employeeId);

            Action act = () =>
                sourceDepartment.ReassignEmployee(
                    employeeId,
                    Guid.Empty,
                    rootDepartment);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("Target department ID cannot be empty. (Parameter 'targetDepartmentId')")
                .WithParameterName("targetDepartmentId");

            sourceDepartment.AssignedEmployeeIds.Should()
                .Contain(employeeId);
        }
        [Fact]
        public void ReassignEmployee_NullRootDepartment_ThrowsArgumentNullException()
        {
            var sourceDepartment =
                new Department(Guid.NewGuid(), "Cardiology");

            var targetDepartmentId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();

            Action act = () =>
                sourceDepartment.ReassignEmployee(
                    employeeId,
                    targetDepartmentId,
                    null!);

            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("rootDepartment");
        }
        [Fact]
        public void ReassignEmployee_EmployeeNotAssigned_ThrowsInvalidOperationException()
        {
            var sourceDepartment =
                new Department(Guid.NewGuid(), "Cardiology");

            var rootDepartment =
                new Department(Guid.NewGuid(), "Hospital");

            var targetDepartment =
                new Department(Guid.NewGuid(), "Neurology");

            rootDepartment.AddSubDepartment(sourceDepartment);
            rootDepartment.AddSubDepartment(targetDepartment);

            var employeeId = Guid.NewGuid();

            Action act = () =>
                sourceDepartment.ReassignEmployee(
                    employeeId,
                    targetDepartment.Id,
                    rootDepartment);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Employee is not assigned to this department.");

            sourceDepartment.AssignedEmployeeIds.Should().NotContain(employeeId);
            targetDepartment.AssignedEmployeeIds.Should().NotContain(employeeId);
        }
        [Fact]
        public void ReassignEmployee_SameDepartment_ThrowsInvalidOperationException()
        {
            var department =
                new Department(Guid.NewGuid(), "Cardiology");

            var rootDepartment =
                new Department(Guid.NewGuid(), "Hospital");

            var employeeId = Guid.NewGuid();

            department.AssignEmployee(employeeId);

            Action act = () =>
                department.ReassignEmployee(
                    employeeId,
                    department.Id,
                    rootDepartment);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Employee is already in the target department.");

            department.AssignedEmployeeIds.Should()
                .Contain(employeeId);
        }
        [Fact]
        public void ReassignEmployee_EmployeeAlreadyAssignedToTarget_ThrowsInvalidOperationException()
        {
            var rootDepartment =
                new Department(Guid.NewGuid(), "Hospital");

            var sourceDepartment =
                new Department(Guid.NewGuid(), "Cardiology");

            var targetDepartment =
                new Department(Guid.NewGuid(), "Neurology");

            rootDepartment.AddSubDepartment(sourceDepartment);
            rootDepartment.AddSubDepartment(targetDepartment);

            var employeeId = Guid.NewGuid();

            sourceDepartment.AssignEmployee(employeeId);
            targetDepartment.AssignEmployee(employeeId);

            Action act = () =>
                sourceDepartment.ReassignEmployee(
                    employeeId,
                    targetDepartment.Id,
                    rootDepartment);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Employee is already assigned to the target department.");
            sourceDepartment.AssignedEmployeeIds.Should()
                .Contain(employeeId);

            targetDepartment.AssignedEmployeeIds.Should()
                .Contain(employeeId);
        }
        #endregion

        #region GetDepartmentHierarchy
        [Fact]
        public void GetDepartmentHierarchy_ReturnsRootDepartment()
        {
            var rootDepartment = new Department(Guid.NewGuid(), "Hospital");
            rootDepartment.GetDepartmentHierarchy().Should().Contain($"- {rootDepartment.Name} (ID: {rootDepartment.Id})");
        }
        [Fact]
        public void GetDepartmentHierarchy_ReturnsChildDepartments()
        {
            var rootDepartment = new Department(Guid.NewGuid(), "Hospital");
            var department1 = new Department(Guid.NewGuid(), "Cardiology");
            rootDepartment.AddSubDepartment(department1);
            var hierarchy = rootDepartment.GetDepartmentHierarchy();

            hierarchy.Should().Contain(
                $"- {rootDepartment.Name} (ID: {rootDepartment.Id})");

            hierarchy.Should().Contain(
                $"  - {department1.Name} (ID: {department1.Id})");
        }
        [Fact]
        public void GetDepartmentHierarchy_ReturnsNestedChildDepartments()
        {
            var rootDepartment = new Department(Guid.NewGuid(), "Hospital");
            var department1 = new Department(Guid.NewGuid(), "Cardiology");
            var department2 = new Department(Guid.NewGuid(), "Neurology");
            var department3 = new Department(Guid.NewGuid(), "Pediatrics");

            rootDepartment.AddSubDepartment(department1);
            rootDepartment.AddSubDepartment(department2);
            department1.AddSubDepartment(department3);

            var hierarchy = rootDepartment.GetDepartmentHierarchy();

            hierarchy.Should().Contain(
                $"- {rootDepartment.Name} (ID: {rootDepartment.Id})");

            hierarchy.Should().Contain(
                $"  - {department1.Name} (ID: {department1.Id})");

            hierarchy.Should().Contain(
                $"  - {department2.Name} (ID: {department2.Id})");

            hierarchy.Should().Contain(
                $"    - {department3.Name} (ID: {department3.Id})");
        }
        [Fact]
        public void GetDepartmentHierarchy_ReturnsCorrectDepartments()
        {
            var rootDepartment = new Department(Guid.NewGuid(), "Hospital");
            var department1 = new Department(Guid.NewGuid(), "Cardiology");
            var department2 = new Department(Guid.NewGuid(), "Neurology");
            var department3 = new Department(Guid.NewGuid(), "Pediatrics");

            rootDepartment.AddSubDepartment(department1);
            rootDepartment.AddSubDepartment(department2);
            department1.AddSubDepartment(department3);

            var hierarchy = rootDepartment.GetDepartmentHierarchy();

            hierarchy.Should().ContainInOrder(
             $"- {rootDepartment.Name} (ID: {rootDepartment.Id})",
             $"  - {department1.Name} (ID: {department1.Id})",
             $"    - {department3.Name} (ID: {department3.Id})",
             $"  - {department2.Name} (ID: {department2.Id})");
        }
        [Fact]
        public void GetDepartmentHierarchy_UsesSpecifiedIndentLevel()
        {
            var root = new Department(Guid.NewGuid(), "Root");

            var result = root.GetDepartmentHierarchy(2);

            result.Should().ContainSingle()
                .Which.Should().StartWith("    - Root");
        }
        #endregion
    }
}
