using Hospital.Domain.Common.Validation;
using Hospital.Domain.Common.ValueObjects;
using Hospital.Domain.Patients;
using Hospital.Domain.Patients.ValueObjects;
namespace Hospital.Domain.Tests.Common.Validation
{
    public class ResultOfTTests
    {
            [Fact]
            public void Success_ShouldCreateSuccessfulResult()
            {
            Patient patient =  Patient.Create(
                new NationalId("30603050301253"),
                new PersonName("مريم","عاطف"),
                new PersonName("Mariam","Atef"),
                new DateOfBirth(new DateOnly(2006,03,06)),
                Gender.Female,
                BloodType.ANegative,
                new ContactInfo(
                    new PhoneNumber("01271989509"),
                    new EmailAddress("mariamatef353@gmail.com"),
                    new HomeAddress("Cairo","Helwan","Main Street","14"),
                    null));
            Result<Patient> result = Result<Patient>.Success(patient);
                Assert.True(result.IsSuccess);
                Assert.False(result.IsFailure);
            Assert.Same(patient, result.Value);
            Assert.Empty(result.Errors);
            }

            [Fact]
            public void Failure_ShouldCreateFailureResult()
            {
                ValidationError validationError = new ValidationError(
                    code: "PATIENT.NATIONAL_ID.REQUIRED",
                    message: "National ID is required.",
                    field: "NationalId");

                List<ValidationError> validationErrors = new List<ValidationError>
            {
                validationError
            };

                Result<Patient> result = Result<Patient>.Failure(validationErrors);

                Assert.False(result.IsSuccess);
                Assert.True(result.IsFailure);
                Assert.Single(result.Errors);
                Assert.Same(validationError, result.Errors[0]);
            }
        }
}
