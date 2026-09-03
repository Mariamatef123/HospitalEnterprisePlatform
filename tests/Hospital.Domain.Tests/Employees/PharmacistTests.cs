using FluentAssertions;
using Hospital.Domain.Employees;
using Hospital.Domain.Employees.ValueObjects;

namespace Hospital.Domain.Tests.Employees
{
    public class PharmacistTests
    {
        private Pharmacist CreatePharmacist()
        {
            return new Pharmacist(
                Guid.NewGuid(),
                new NationalId("30203020106543"),
                new PersonName("Mariam", "Atef"),
                new PhoneNumber("01271689560"),
                Guid.NewGuid()
            );
        }

        [Fact]
        public void Pharmacist_AssignedToHospitalEmployee_IsPharmacist()
        {
            HospitalEmployee employee = CreatePharmacist();

            employee.Should().BeOfType<Pharmacist>();
        }

        [Fact]
        public void Pharmacist_Should_Have_Correct_Permissions()
        {
            var pharmacist = CreatePharmacist();

            var permissions = pharmacist.GetRolePermissions();

            permissions.Should().Contain(
                EmployeePermission.ManageMedicationInventory);
        }
    }
}