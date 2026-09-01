using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Employees
{
    public class LabTechnician : HospitalEmployee
    {
        public LabTechnician(int id, string name, EmploymentStatus employmentStatus) : base(id, name, employmentStatus)
        {
        }

        public override string GetRoleDescription()
        {
           return "Lab Technician";
        }
        public override string GetDisplayInfo()
        {
            return $"{Name} - {GetRoleDescription()} - Lab Staff";
        }
    }
}
