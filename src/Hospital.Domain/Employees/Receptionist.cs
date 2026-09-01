using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Employees
{
    public class Receptionist : HospitalEmployee
    {
        public Receptionist(int id, string name, EmploymentStatus employmentStatus) : base(id, name, employmentStatus)
        {
        }

        public override string GetRoleDescription()
        {
            return "Receptionist";
        }
    }
}
