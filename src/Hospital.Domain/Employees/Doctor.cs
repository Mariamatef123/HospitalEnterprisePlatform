using Hospital.Domain.Employees.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Employees
{
    public sealed class Doctor: HospitalEmployee
    {
        public DoctorSpecialty Specialty { get;  }
        public string LicenseNumber { get; private set; }
        public Doctor(Guid id, NationalId nationalId, PersonName name, PhoneNumber phone, Guid departmentId, DoctorSpecialty specialty, string licenseNumber) : base(id, nationalId, name, phone, departmentId)
        {
            if (!Enum.IsDefined(specialty))
                throw new ArgumentException("Invalid doctor specialty.");
            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new ArgumentException("License number is required.");

            Specialty = specialty;
            LicenseNumber = licenseNumber.Trim();
        }

        public override IReadOnlyCollection<EmployeePermission> GetRolePermissions()
        {
            return new[]
            {
            EmployeePermission.ViewMedicalRecord,
            EmployeePermission.UpdateMedicalRecord,
            EmployeePermission.CreatePrescription
        };
        }
        }
}
