using FluentAssertions;
using Hospital.Domain.Common.ValueObjects;
using Hospital.Domain.Patients;
using Hospital.Domain.Patients.ValueObjects;
namespace Hospital.Domain.Tests.Patients
{
    public class PatientLifecycleTests
    {
        private static (
            NationalId NationalId,
            PersonName ArabicName,
            PersonName EnglishName,
            DateOfBirth DateOfBirth,
            Gender Gender,
            BloodType BloodType,
            ContactInfo ContactInfo) CreateValidPatientData()
        {
            NationalId nationalId = new NationalId("30403060302506");
            PersonName arabicName = new PersonName("مريم", "عاطف");
            PersonName englishName = new PersonName("Mariam", "Atef");
            DateOfBirth dateOfBirth = new DateOfBirth(new DateOnly(2006, 3, 6));

            ContactInfo contactInfo = new ContactInfo(
                new PhoneNumber("01271989509"),
                new EmailAddress("MariamAtef353@gmail.com"),
                new HomeAddress("Cairo", "Helwan", "Main Street", "15"),
                new EmergencyContact(
                    new PersonName("Mariam", "Atef"),
                    EmergencyContactRelationship.Brother,
                    new PhoneNumber("01271989519")));

            return (
                nationalId,
                arabicName,
                englishName,
                dateOfBirth,
                Gender.Female,
                BloodType.APositive,
                contactInfo);
        }
        [Fact]
       public void Patient_NewPatient_ShouldStartAsActive()
        {
            var data = CreateValidPatientData();
            Patient patient = Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                data.ContactInfo
                );
            patient.Status.Should().Be(PatientStatus.Active);
        }
        [Fact]
        public void Patient_ActivePatient_Deactivate_ShouldSetStatusToInactive()
        {
            var data = CreateValidPatientData();
            Patient patient = Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                data.ContactInfo
                );
            patient.Deactivate();
            patient.Status.Should().Be(PatientStatus.Inactive);
        }
        [Fact]
        public void Patient_InactivePatient_Deactivate_ShouldThrowInvalidOperationException()
        {

            var data = CreateValidPatientData();
            Patient patient = Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                data.ContactInfo
                );
            patient.Deactivate();
            Action action = ()=> patient.Deactivate();
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("Patient is already inactive.");
            patient.Status.Should().Be(PatientStatus.Inactive);
        }
        [Fact]
        public void Patient_InactivePatient_Reactivate_ShouldSetStatusToActive()
        {
            var data = CreateValidPatientData();
            Patient patient = Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                data.ContactInfo
                );
            patient.Deactivate();
            patient.Reactivate();
            patient.Status.Should().Be(PatientStatus.Active);
        }
        [Fact]
        public void Patient_ActivePatient_Reactivate_ShouldThrowInvalidOperationException()
        {
            var data = CreateValidPatientData();
            Patient patient = Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                data.ContactInfo
                );
            Action action = ()=> patient.Reactivate();
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("Patient is already active.");
            patient.Status.Should().Be(PatientStatus.Active);
        }
        [Fact]
        public void Patient_Lifecycle_ShouldTransitionCorrectly()
        {
            var data = CreateValidPatientData();
            Patient patient = Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                data.ContactInfo
                );
            patient.Status.Should().Be(PatientStatus.Active);
            patient.Deactivate();
            patient.Status.Should().Be(PatientStatus.Inactive);
            patient.Reactivate();
            patient.Status.Should().Be(PatientStatus.Active);
        }
    }
}
