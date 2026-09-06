using Hospital.Domain.Common.ValueObjects;
using Hospital.Domain.Employees;

public sealed class BillingClerk : HospitalEmployee
{
    public BillingClerk(
        Guid id,
        NationalId nationalId,
        PersonName name,
        PhoneNumber phone,
        Guid departmentId)
        : base(id, nationalId, name, phone, departmentId)
    {
    }

    public override IReadOnlyCollection<EmployeePermission> GetRolePermissions()
    {
        return new[]
        {
            EmployeePermission.ProcessBilling
        };
    }
}