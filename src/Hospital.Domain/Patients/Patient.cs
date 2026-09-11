using Hospital.Domain.Common;
using Hospital.Domain.Common.ValueObjects;
using Hospital.Domain.Patients.ValueObjects;
namespace Hospital.Domain.Patients
{
    public class Patient : AggregateRoot<Guid>
    {
        public NationalId NationalId { get; private set; }
        public PersonName ArabicName { get; private set; }
        public PersonName EnglishName { get; private set; }
        public DateOfBirth DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public BloodType BloodType { get; private set; }
        public ContactInfo ContactInfo { get; private set; }
        public PatientStatus Status { get; private set; }

        protected Patient(
           Guid id,
           NationalId nationalId,
           PersonName arabicName,
           PersonName englishName,
           DateOfBirth dateOfBirth,
           Gender gender,
           BloodType bloodType,
           ContactInfo contactInfo)
           : base(id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(
                    "Patient ID is required.",
                    nameof(id));

            if (nationalId is null)
                throw new ArgumentNullException(
                    nameof(nationalId),
                    "National ID is required.");

            if (arabicName is null)
                throw new ArgumentNullException(
                    nameof(arabicName),
                    "Arabic name is required.");

            if (englishName is null)
                throw new ArgumentNullException(
                    nameof(englishName),
                    "English name is required.");

            if (dateOfBirth is null)
                throw new ArgumentNullException(
                    nameof(dateOfBirth),
                    "Date of birth is required.");

            if (contactInfo is null)
                throw new ArgumentNullException(
                    nameof(contactInfo),
                    "Contact info is required.");
            if (!Enum.IsDefined(gender))
                throw new ArgumentException(
                    "Invalid gender.",
                    nameof(gender));
            if (!Enum.IsDefined(bloodType))
                throw new ArgumentException(
                    "Invalid blood type.",
                    nameof(bloodType));
            NationalId = nationalId;
            ArabicName = arabicName;
            EnglishName = englishName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            BloodType = bloodType;
            ContactInfo = contactInfo;
            Status = PatientStatus.Active;
        }
        public static Patient Create(
           NationalId nationalId,
           PersonName arabicName,
           PersonName englishName,
           DateOfBirth dateOfBirth,
           Gender gender,
           BloodType bloodType,
           ContactInfo contactInfo)
        {
            return new Patient(
                Guid.NewGuid(),
                nationalId,
                arabicName,
                englishName,
                dateOfBirth,
                gender,
                bloodType,
                contactInfo);
        }
        public void Deactivate()
        {
            if (Status == PatientStatus.Inactive)
                throw new InvalidOperationException(
                    "Patient is already inactive.");
            Status = PatientStatus.Inactive;
        }

        public void Reactivate()
        {
            if (Status == PatientStatus.Active)
                throw new InvalidOperationException(
                    "Patient is already active.");

            Status = PatientStatus.Active;
        }
    }
}
