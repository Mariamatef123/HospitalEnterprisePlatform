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
        public string Specialty { get; private set; }
        public string LicenseNumber { get; private set; }
        public Doctor(Guid id, NationalId nationalId, PersonName name, PhoneNumber phone, Guid departmentId, string specialty, string licenseNumber) : base(id, nationalId, name, phone, departmentId)
        {
            if (string.IsNullOrWhiteSpace(specialty))
                throw new ArgumentException("Specialty is required.");

            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new ArgumentException("License number is required.");

            Specialty = specialty;
            LicenseNumber = licenseNumber;
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
