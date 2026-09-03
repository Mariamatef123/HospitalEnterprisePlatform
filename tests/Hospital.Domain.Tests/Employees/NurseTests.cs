using FluentAssertions;
using Hospital.Domain.Employees;
using Hospital.Domain.Employees.ValueObjects;
namespace Hospital.Domain.Tests.Employees
{
    public class NurseTests
    {

            private Nurse CreateNurse()
            {
                return new Nurse(
                    Guid.NewGuid(),
                    new NationalId("30203020106543"),
                    new PersonName("Mariam", "Atef"),
                    new PhoneNumber("01271689560"),
                    Guid.NewGuid(),
                    ShiftType.Evening);
            }
            [Fact]
            public void Nurse_AssignedToHospitalEmployee_IsNurse()
            {
                HospitalEmployee employee = CreateNurse();

                employee.Should().BeOfType<Nurse>();
            }
            [Fact]
            public void CreateNurse_WithInvalidShiftType_ThrowsArgumentException()
            {
                Action act = () => new Nurse(
                    Guid.NewGuid(),
                    new NationalId("30203020106543"),
                    new PersonName("Mariam", "Atef"),
                    new PhoneNumber("01271689560"),
                    Guid.NewGuid(),
                    (ShiftType)999);

                act.Should().Throw<ArgumentException>()
                   .WithMessage("Invalid Shift.");
            }
     
            [Fact]
            public void Nurse_Should_Have_Shift()
            {
                var nurse = CreateNurse();
                nurse.Shift.Should().Be(ShiftType.Evening);
            }

            [Fact]
            public void Nurse_Should_Have_Correct_Permissions()
            {
                var nurse = CreateNurse();
                var permissions = nurse.GetRolePermissions();

            permissions.Should().Contain(EmployeePermission.ViewMedicalRecord);
                permissions.Should().Contain(EmployeePermission.UpdateVitalSigns);

            }
        }
    

}
