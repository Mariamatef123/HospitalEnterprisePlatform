using Hospital.Domain.Patients;
using Hospital.Domain.Patients.Validation;
using Hospital.Domain.Common.Validation;

namespace Hospital.Domain.Tests.Patients.Validation
{
    public class PatientValidatorTests
    {
        private readonly PatientValidator _validator = new PatientValidator();

        private static PatientValidationInput CreateValidInput(
            string nationalId = "30603050301253",
            string firstArabicName = "مريم",
            string lastArabicName = "عاطف",
            string firstEnglishName = "Mariam",
            string lastEnglishName = "Atef",
            DateOnly? dateOfBirth = null,
            Gender gender = Gender.Female,
            BloodType bloodType = BloodType.ANegative,
            string phoneNumber = "01271989509",
            string email = "mariamatef353@gmail.com",
            string governorate = "Cairo",
            string city = "Helwan",
            string street = "Main Street",
            string buildingNumber = "14",
            string? district = null,
            string? apartmentNumber = null,
            string? postalCode = null,
            string? emergencyName = null,
            EmergencyContactRelationship? emergencyRelationship = null,
            string? emergencyPhoneNumber = null)
        {
            return new PatientValidationInput(
                nationalId: nationalId,
                firstArabicName: firstArabicName,
                lastArabicName: lastArabicName,
                firstEnglishName: firstEnglishName,
                lastEnglishName: lastEnglishName,
                dateOfBirth: dateOfBirth ?? new DateOnly(2006, 3, 6),
                gender: gender,
                bloodType: bloodType,
                phoneNumber: phoneNumber,
                email: email,
                governorate: governorate,
                city: city,
                street: street,
                buildingNumber: buildingNumber,
                district: district,
                apartmentNumber: apartmentNumber,
                postalCode: postalCode,
                emergencyName: emergencyName,
                emergencyRelationship: emergencyRelationship,
                emergencyPhoneNumber: emergencyPhoneNumber);
        }

        [Fact]
        public void ValidPatient_ShouldReturnSuccess()
        {
            PatientValidationInput input = CreateValidInput();

            Result result = _validator.Validate(input);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void EmptyArabicName_ShouldReturnFailure()
        {
            PatientValidationInput input = CreateValidInput(
                firstArabicName: "");

            Result result = _validator.Validate(input);

            Assert.True(result.IsFailure);
            Assert.Contains(
                result.Errors,
                error => error.Code == "PATIENT.ARABIC_FIRST_NAME.REQUIRED");
        }

        [Fact]
        public void EmptyEnglishName_ShouldReturnFailure()
        {
            PatientValidationInput input = CreateValidInput(
                firstEnglishName: "");

            Result result = _validator.Validate(input);

            Assert.True(result.IsFailure);
            Assert.Contains(
                result.Errors,
                error => error.Code == "PATIENT.ENGLISH_FIRST_NAME.REQUIRED");
        }

        [Fact]
        public void WhitespaceOnlyName_ShouldReturnFailure()
        {
            PatientValidationInput input = CreateValidInput(
                firstArabicName: "   ");

            Result result = _validator.Validate(input);

            Assert.True(result.IsFailure);
            Assert.Contains(
                result.Errors,
                error => error.Code == "PATIENT.ARABIC_FIRST_NAME.REQUIRED");
        }

        [Fact]
        public void InvalidNationalIdLength_ShouldReturnFailure()
        {
            PatientValidationInput input = CreateValidInput(
                nationalId: "123456");

            Result result = _validator.Validate(input);

            Assert.True(result.IsFailure);
            Assert.Contains(
                result.Errors,
                error => error.Code == "PATIENT.NATIONAL_ID.LENGTH_INVALID");
        }

        [Fact]
        public void InvalidNationalIdFormat_ShouldReturnFailure()
        {
            PatientValidationInput input = CreateValidInput(
                nationalId: "3060305030125A");

            Result result = _validator.Validate(input);

            Assert.True(result.IsFailure);
            Assert.Contains(
                result.Errors,
                error => error.Code == "PATIENT.NATIONAL_ID.FORMAT_INVALID");
        }

        [Fact]
        public void FutureDateOfBirth_ShouldReturnFailure()
        {
            DateOnly futureDate = DateOnly.FromDateTime(DateTime.Today).AddDays(1);

            PatientValidationInput input = CreateValidInput(
                dateOfBirth: futureDate);

            Result result = _validator.Validate(input);

            Assert.True(result.IsFailure);
            Assert.Contains(
                result.Errors,
                error => error.Code == "PATIENT.DATE_OF_BIRTH.FUTURE");
        }

        [Fact]
        public void MissingRequiredField_ShouldReturnFailure()
        {
            PatientValidationInput input = CreateValidInput(
                phoneNumber: "");

            Result result = _validator.Validate(input);

            Assert.True(result.IsFailure);
            Assert.Contains(
                result.Errors,
                error => error.Code == "PATIENT.PHONE_NUMBER.REQUIRED");
        }

        [Fact]
        public void ArabicCharacters_ShouldBeAccepted()
        {
            PatientValidationInput input = CreateValidInput(
                firstArabicName: "مريم",
                lastArabicName: "عاطف");

            Result result = _validator.Validate(input);

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void MultiByteArabicCharacters_ShouldBeAccepted()
        {
            PatientValidationInput input = CreateValidInput(
                firstArabicName: "محمد",
                lastArabicName: "عبدالرحمن");

            Result result = _validator.Validate(input);

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void LeadingAndTrailingWhitespace_ShouldBeIgnored()
        {
            PatientValidationInput input = CreateValidInput(
                firstArabicName: "  مريم  ",
                lastArabicName: "  عاطف  ",
                firstEnglishName: "  Mariam  ",
                lastEnglishName: "  Atef  ");

            Result result = _validator.Validate(input);

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void MultipleValidationErrors_ShouldReturnAllErrors()
        {
            PatientValidationInput input = CreateValidInput(
                nationalId: "123",
                firstArabicName: "",
                firstEnglishName: "");

            Result result = _validator.Validate(input);

            Assert.True(result.IsFailure);
            Assert.True(result.Errors.Count >= 3);

            Assert.Contains(
                result.Errors,
                error => error.Code == "PATIENT.NATIONAL_ID.LENGTH_INVALID");

            Assert.Contains(
                result.Errors,
                error => error.Code == "PATIENT.ARABIC_FIRST_NAME.REQUIRED");

            Assert.Contains(
                result.Errors,
                error => error.Code == "PATIENT.ENGLISH_FIRST_NAME.REQUIRED");
        }

        [Fact]
        public void ValidationFailure_ShouldNotThrowException()
        {
            PatientValidationInput input = CreateValidInput(
                nationalId: "123",
                firstArabicName: "");

            Result? result = null;

            Action act = () =>
            {
                result = _validator.Validate(input);
            };

            Assert.Null(Record.Exception(act));
            Assert.NotNull(result);
            Assert.True(result!.IsFailure);
        }
    }
}