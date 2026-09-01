using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Employees
{
    public abstract class HospitalEmployee
    {
        public int Id { get; }
        public string Name { get; }
        // TODO Day 3: Add Department reference/assignment to HospitalEmployee
        // when Hospital and Department entities are implemented.
        public EmploymentStatus EmploymentStatus { get; private set; }

        protected HospitalEmployee(int id, string name, EmploymentStatus employmentStatus)
        {
            Id = id;
            Name = name;
            EmploymentStatus = employmentStatus;
        }
        public abstract string GetRoleDescription();
        public void ChangeEmploymentStatus(EmploymentStatus newEmploymentStatus)
        {
            EmploymentStatus = newEmploymentStatus;
        }
        public virtual string GetDisplayInfo()
        {
            return $"{Name} - {GetRoleDescription()}";
        }
    }
}
