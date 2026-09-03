using Hospital.Domain.Employees.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Employees
{
    public sealed class Nurse: HospitalEmployee
    {
        public ShiftType Shift { get; private set; }

        public Nurse(Guid id,NationalId nationalId,PersonName name,PhoneNumber phone,Guid departmentId,ShiftType shift): base(id, nationalId, name, phone, departmentId)
        {
            Shift = shift;
        }

        public override IReadOnlyCollection<EmployeePermission>
            GetRolePermissions()
        {
            return new[]
            {
            EmployeePermission.ViewMedicalRecord,
            EmployeePermission.UpdateVitalSigns
        };
        }
    }
}
