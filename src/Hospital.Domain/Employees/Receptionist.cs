using Hospital.Domain.Employees;
using Hospital.Domain.Employees.ValueObjects;

public sealed class Receptionist : HospitalEmployee
{
    public Receptionist(
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
            EmployeePermission.RegisterPatient,
            EmployeePermission.ScheduleAppointment
        };
    }
}
