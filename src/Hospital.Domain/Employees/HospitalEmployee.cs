using Hospital.Domain.Common;
using Hospital.Domain.Employees.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Employees
{
    public abstract class HospitalEmployee: AggregateRoot<Guid>
    {
        public NationalId NationalId { get; private set; }
        public PersonName Name { get; private set; }
        public PhoneNumber Phone { get; private set; }
        public Guid DepartmentId { get; private set; }
        public EmploymentStatus Status { get; private set; }


        protected HospitalEmployee(
         Guid id,
         NationalId nationalId,
         PersonName name,
         PhoneNumber phone,
         Guid departmentId)
         : base(id)
        {
            if (departmentId == Guid.Empty)
                throw new ArgumentException("Department ID is required.");

            NationalId = nationalId;
            Name = name;
            Phone = phone;
            DepartmentId = departmentId;
            Status = EmploymentStatus.Active;
        }
        public void Activate()
        {
            Status = EmploymentStatus.Active;
        }

        public void Deactivate()
        {
            Status = EmploymentStatus.Inactive;
        }
        public void Suspend()
        {
            if (Status == EmploymentStatus.Inactive)
                throw new InvalidOperationException(
                    "Inactive employees cannot be suspended.");

            Status = EmploymentStatus.Suspended;
        }
        public abstract IReadOnlyCollection<EmployeePermission> GetRolePermissions();
    }
}
