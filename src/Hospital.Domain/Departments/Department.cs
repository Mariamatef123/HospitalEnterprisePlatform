using Hospital.Domain.Common;
namespace Hospital.Domain.Departments
{
    public sealed class Department : AggregateRoot<Guid>, IOrganizationUnit
    {
        private readonly List<IOrganizationUnit> _children = new();
        private readonly List<Guid> _assignedEmployeeIds = new();


        public string Name { get; }
        public Department? Parent { get; private set; }
        public Guid? ParentDepartmentId => Parent?.Id;
        public Department(Guid id, string name):base(id)
        {

            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Department name cannot be null or whitespace.", nameof(name));
            }   
            Name = name.Trim();
        }
        public IReadOnlyCollection<IOrganizationUnit> Children =>
            _children.AsReadOnly();

        public IReadOnlyCollection<Guid> AssignedEmployeeIds =>
    _assignedEmployeeIds.AsReadOnly();
        public void AddSubDepartment(IOrganizationUnit child)
        {
            ArgumentNullException.ThrowIfNull(child);
            if (Id == child.Id)
            {
                throw new InvalidOperationException(
                    "Cannot add a department as a child of itself.");
            }
            foreach (var existingChild in _children)
            {
                if (existingChild.Id == child.Id)
                {
                    throw new InvalidOperationException(
                        "The specified child already exists.");
                }
            }

            if (IsAncestor(child))
            {
                throw new InvalidOperationException(
                            "Cannot create a circular department hierarchy.");
            }

            if (child is Department department )
            {
                if (department.Parent is not null)
                {
                    throw new InvalidOperationException(
                    "The specified child already has a parent.");
                }
                department.Parent = this;

            }

            _children.Add(child);
        }

        public void RemoveSubDepartment(IOrganizationUnit child)
        {
            ArgumentNullException.ThrowIfNull(child);

            if (!_children.Remove(child))
            {
                throw new InvalidOperationException(
                    "The specified child does not exist.");
            }

            if (child is Department department)
            {
                department.Parent = null;
            }
           
        }

        public bool HasChildren() => _children.Count > 0;

        private bool IsAncestor(IOrganizationUnit department)
        {
            Department? current = Parent;
            while (current != null)
            {
                if (current.Id == department.Id)
                {
                    return true;
                }
                current = current.Parent;
            }
            return false;
        }
        public Department? FindDepartmentRecursive(Guid id)
        {
            if (Id == id)
            {
                return this;
            }

            if (_children.Count == 0)
            {
                return null;
            }

            foreach (var child in _children)
            {
                if (child is Department department)
                {
                    var found = department.FindDepartmentRecursive(id);

                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }
        public void AssignEmployee(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentException("Employee ID cannot be empty.", nameof(employeeId));
            }
            if (_assignedEmployeeIds.Contains(employeeId))
            {
                throw new InvalidOperationException("Employee is already assigned to this department.");
            }
            _assignedEmployeeIds.Add(employeeId);
        }
        public void UnassignEmployee(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentException("Employee ID cannot be empty.", nameof(employeeId));
            }
            if (!_assignedEmployeeIds.Contains(employeeId))
            {
                throw new InvalidOperationException("Employee is not assigned to this department.");
            }
            _assignedEmployeeIds.Remove(employeeId);
        }
        public void ReassignEmployee(
           Guid employeeId,
           Guid targetDepartmentId,
           Department rootDepartment)
        {
            ArgumentNullException.ThrowIfNull(rootDepartment);
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentException("Employee ID cannot be empty.", nameof(employeeId));
            }
            if (targetDepartmentId == Guid.Empty)
            {
                throw new ArgumentException(
                    "Target department ID cannot be empty.",
                    nameof(targetDepartmentId));
            }
            if (!_assignedEmployeeIds.Contains(employeeId))
            {
                throw new InvalidOperationException(
                    "Employee is not assigned to this department.");
            }

            if (targetDepartmentId == Id)
            {
                throw new InvalidOperationException(
                    "Employee is already in the target department.");
            }

            Department? targetDepartment =
                rootDepartment.FindDepartmentRecursive(targetDepartmentId);

            if (targetDepartment is null)
            {
                throw new InvalidOperationException(
                    "Target department not found.");
            }

            if (targetDepartment._assignedEmployeeIds.Contains(employeeId))
            {
                throw new InvalidOperationException(
                    "Employee is already assigned to the target department.");
            }

            _assignedEmployeeIds.Remove(employeeId);
            targetDepartment.AssignEmployee(employeeId);
        }
        public IReadOnlyList<string> GetDepartmentHierarchy(int indentLevel = 0)
        {
            var hierarchy = new List<string>();

            hierarchy.Add(
                $"{new string(' ', indentLevel * 2)}- {Name} (ID: {Id})");

            foreach (var child in _children)
            {
                if (child is Department department)
                {
                    hierarchy.AddRange(
                        department.GetDepartmentHierarchy(indentLevel + 1));
                }
            }

            return hierarchy;
        }

    }
}
