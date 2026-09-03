using FluentAssertions;
using Hospital.Domain.Employees;
using Hospital.Domain.Employees.ValueObjects;
namespace Hospital.Domain.Tests.Employees
{
    public class LabTechnicianTests
    {
 
            private LabTechnician CreateLabTechnician()
            {
                return new LabTechnician(
                    Guid.NewGuid(),
                    new NationalId("30203020106543"),
                    new PersonName("Mariam", "Atef"),
                    new PhoneNumber("01271689560"),
                    Guid.NewGuid()
                );
            }

            [Fact]
            public void LabTechnician_AssignedToHospitalEmployee_IsLabTechnician()
            {
                HospitalEmployee employee = CreateLabTechnician();

                employee.Should().BeOfType<LabTechnician>();
            }

            [Fact]
            public void LabTechnician_Should_Have_Correct_Permissions()
            {
                var labTechnician = CreateLabTechnician();

                var permissions = labTechnician.GetRolePermissions();

                permissions.Should().Contain(EmployeePermission.ProcessLabTests);
       
            }
        }
   
}
