using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Employees
{
    public enum EmployeePermission
    {
        RegisterPatient,
        ScheduleAppointment,
        ViewMedicalRecord,
        UpdateMedicalRecord,
        CreatePrescription,
        UpdateVitalSigns,
        ManageMedicationInventory,
        ProcessLabTests,
        ProcessBilling
    }
}
