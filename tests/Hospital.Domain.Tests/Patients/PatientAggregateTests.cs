using FluentAssertions;
using Hospital.Domain.Common.ValueObjects;
using Hospital.Domain.Patients;
using Hospital.Domain.Patients.ValueObjects;
using System.Reflection;
namespace Hospital.Domain.Tests.Patients
{
    public class PatientAggregateTests
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
        public void Patient_ValidValues_ShouldBeCreated()
        {
            var data = CreateValidPatientData();
            Patient patient = Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                data.ContactInfo);
            patient.NationalId.Should().Be(data.NationalId);
            patient.ArabicName.Should().Be(data.ArabicName);
            patient.EnglishName.Should().Be(data.EnglishName);
            patient.DateOfBirth.Should().Be(data.DateOfBirth);
            patient.Gender.Should().Be(data.Gender);
            patient.BloodType.Should().Be(data.BloodType);
            patient.ContactInfo.Should().Be(data.ContactInfo);
            patient.Status.Should().Be(PatientStatus.Active);
            patient.Id.Should().NotBe(Guid.Empty);
        }
        [Fact]
        public void Patient_CreateMultiplePatients_ShouldGenerateUniqueIds()
        {
            var data = CreateValidPatientData();

            Patient patient1 = Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                data.ContactInfo);

            Patient patient2 = Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                data.ContactInfo);

            patient1.Id.Should().NotBe(Guid.Empty);
            patient2.Id.Should().NotBe(Guid.Empty);
            patient1.Id.Should().NotBe(patient2.Id);
        }
        [Fact]
       public void Patient_NullNationalId_ShouldThrowArgumentNullException()
        {
            var data = CreateValidPatientData();
            Action act = () => Patient.Create(null!, data.ArabicName, data.EnglishName, data.DateOfBirth, data.Gender, data.BloodType, data.ContactInfo);
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("National ID is required. (Parameter 'nationalId')")
                .WithParameterName("nationalId");
        }
        [Fact]
        public void Patient_NullArabicName_ShouldThrowArgumentNullException()
        {
            var data = CreateValidPatientData();
            Action act = () => Patient.Create(
                data.NationalId,
                null!,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                data.ContactInfo);
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Arabic name is required. (Parameter 'arabicName')")
                .WithParameterName("arabicName");
        }
        [Fact]
        public void Patient_NullEnglishName_ShouldThrowArgumentNullException()
        {
            var data = CreateValidPatientData();
            Action act = () => Patient.Create(
                data.NationalId,
                data.ArabicName,
                null!,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                data.ContactInfo);
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("English name is required. (Parameter 'englishName')")
                .WithParameterName("englishName");
        }
        [Fact]
        public void Patient_NullDateOfBirth_ShouldThrowArgumentNullException()
        {
            var data = CreateValidPatientData();
            Action act = () => Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                null!,
                data.Gender,
                data.BloodType,
                data.ContactInfo);
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Date of birth is required. (Parameter 'dateOfBirth')")
                .WithParameterName("dateOfBirth");
        }
        [Fact]
        public void Patient_NullContactInfo_ShouldThrowArgumentNullException()
        {
            var data = CreateValidPatientData();
            Action act = () => Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                data.BloodType,
                null!);
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Contact info is required. (Parameter 'contactInfo')")
                .WithParameterName("contactInfo");
        }
        [Fact]
        public void Patient_InvalidGender_ShouldThrowArgumentException()
        {
            var data = CreateValidPatientData();
            Action act = () => Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                (Gender)99,
                data.BloodType,
                data.ContactInfo);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Invalid gender. (Parameter 'gender')")
                .WithParameterName("gender");
        }
        [Fact]
        public void Patient_InvalidBloodType_ShouldThrowArgumentException()
        {
            var data = CreateValidPatientData();
            Action act = () => Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                (BloodType)99,
                data.ContactInfo);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Invalid blood type. (Parameter 'bloodType')")
                .WithParameterName("bloodType");
        }
        [Fact]
        public void Patient_StateProperties_ShouldHavePrivateSetters()
        {
            var properties = typeof(Patient)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.DeclaringType == typeof(Patient));

            foreach (var property in properties)
            {
                property.SetMethod.Should().NotBeNull();
                property.SetMethod!.IsPrivate.Should().BeTrue();
            }
        }
        [Theory]
        [InlineData(Gender.Male)]
        [InlineData(Gender.Female)]
        public void Patient_ValidGender_ShouldBeCreated(Gender gender)
        {
            var data = CreateValidPatientData();

            Patient patient = Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                gender,
                data.BloodType,
                data.ContactInfo);

            patient.Gender.Should().Be(gender);
        }
        [Theory]
        [InlineData(BloodType.APositive)]
        [InlineData(BloodType.ANegative)]
        [InlineData(BloodType.ABNegative)]
        [InlineData(BloodType.ABPositive)]
        [InlineData(BloodType.OPositive)]
        [InlineData(BloodType.ONegative)]
        [InlineData(BloodType.BNegative)]
        [InlineData(BloodType.BPositive)]
        public void Patient_ValidBloodType_ShouldBeCreated(BloodType bloodType)
        {
            var data = CreateValidPatientData();

            Patient patient = Patient.Create(
                data.NationalId,
                data.ArabicName,
                data.EnglishName,
                data.DateOfBirth,
                data.Gender,
                bloodType,
                data.ContactInfo);

            patient.BloodType.Should().Be(bloodType);
        }
    }
}
