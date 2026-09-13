using System;
using System.Collections.Generic;

namespace Hospital.Domain.Common.Validation
{
    public sealed class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<ValidationError> Errors { get; }

        private Result(
            bool isSuccess,
            IReadOnlyList<ValidationError> errors)
        {
            IsSuccess = isSuccess;
            Errors = new List<ValidationError>(errors).AsReadOnly();
        }

        public static Result Success()
        {
            return new Result(
                true,
                Array.Empty<ValidationError>());
        }

        public static Result Failure(
            IReadOnlyList<ValidationError> errors)
        {
            if (errors is null || errors.Count == 0)
            {
                throw new ArgumentException(
                    "A failure result must contain at least one validation error.",
                    nameof(errors));
            }

            return new Result(
                false,
                errors);
        }
    }
}