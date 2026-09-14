using Hospital.Domain.Patients;
using Hospital.Domain.Patients.Validation;
namespace Hospital.Domain.Tests.Common.Validation
{
    public class ValidationErrorTests
    {
            [Fact]
            public void ValidPatient_ShouldReturnSuccess()
            {
                PatientValidationInput input = new PatientValidationInput(
                    nationalId: "30603050301253",
                    firstArabicName: "مريم",
                    lastArabicName: "عاطف",
                    firstEnglishName: "Mariam",
                    lastEnglishName: "Atef",
                    dateOfBirth: new DateOnly(2006, 3, 6),
                    gender: Gender.Female,
                    bloodType: BloodType.ANegative,
                    phoneNumber: "01271989509",
                    email: "mariamatef353@gmail.com",
                    governorate: "Cairo",
                    city: "Helwan",
                    street: "Main Street",
                    buildingNumber: "14");
            PatientValidator validator = new PatientValidator();

                var result = validator.Validate(input);

                Assert.True(result.IsSuccess);
                Assert.False(result.IsFailure);
                Assert.Empty(result.Errors);
            }
        }
}
