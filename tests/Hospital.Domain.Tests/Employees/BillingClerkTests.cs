using FluentAssertions;
using Hospital.Domain.Employees;
using Hospital.Domain.Employees.ValueObjects;

namespace Hospital.Domain.Tests.Employees
{
    public class BillingClerkTests
    {
        private BillingClerk CreateBillingClerk()
        {
            return new BillingClerk(
                Guid.NewGuid(),
                new NationalId("30203020106543"),
                new PersonName("Mariam", "Atef"),
                new PhoneNumber("01271689560"),
                Guid.NewGuid()
            );
        }

        [Fact]
        public void BillingClerk_AssignedToHospitalEmployee_IsBillingClerk()
        {
            HospitalEmployee employee = CreateBillingClerk();

            employee.Should().BeOfType<BillingClerk>();
        }

        [Fact]
        public void BillingClerk_Should_Have_Correct_Permissions()
        {
            var billingClerk = CreateBillingClerk();

            var permissions = billingClerk.GetRolePermissions();

            permissions.Should().Contain(EmployeePermission.ProcessBilling);
        }
    }
}