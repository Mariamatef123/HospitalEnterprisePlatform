using FluentAssertions;
using Hospital.Domain.Employees;
using Hospital.Domain.Employees.ValueObjects;

namespace Hospital.Domain.Tests.Employees
{
    public class ReceptionistTests
    {
        private Receptionist CreateReceptionist()
        {
            return new Receptionist(
                Guid.NewGuid(),
                new NationalId("30203020106543"),
                new PersonName("Mariam", "Atef"),
                new PhoneNumber("01271689560"),
                Guid.NewGuid()
            );
        }

        [Fact]
        public void Receptionist_AssignedToHospitalEmployee_IsReceptionist()
        {
            HospitalEmployee employee = CreateReceptionist();

            employee.Should().BeOfType<Receptionist>();
        }

        [Fact]
        public void Receptionist_Should_Have_Correct_Permissions()
        {
            var receptionist = CreateReceptionist();

            var permissions = receptionist.GetRolePermissions();

            permissions.Should().Contain(EmployeePermission.RegisterPatient);
            permissions.Should().Contain(EmployeePermission.ScheduleAppointment);
        }
    }
}