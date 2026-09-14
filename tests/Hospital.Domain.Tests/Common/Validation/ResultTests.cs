using Hospital.Domain.Common.Validation;

namespace Hospital.Domain.Tests.Common.Validation
{
    public class ResultTests
    {
        [Fact]
        public void Success_ShouldCreateSuccessfulResult()
        {
            Result result = Result.Success();

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
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

            Result result = Result.Failure(validationErrors);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Same(validationError, result.Errors[0]);
        }
    }
}