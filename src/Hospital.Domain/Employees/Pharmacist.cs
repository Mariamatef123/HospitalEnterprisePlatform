using Hospital.Domain.Employees.ValueObjects;

namespace Hospital.Domain.Employees
{
    public sealed class Pharmacist : HospitalEmployee
    {
        public Pharmacist(
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
                EmployeePermission.ManageMedicationInventory
            };
        }
    }
}