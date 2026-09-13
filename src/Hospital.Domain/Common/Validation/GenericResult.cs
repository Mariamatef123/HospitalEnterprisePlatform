using System;
using System.Collections.Generic;

namespace Hospital.Domain.Common.Validation
{
    public sealed class Result<T>
    {
        private readonly T? _value;

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public T Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException(
                        "Cannot access the value of a failed result.");
                }

                return _value!;
            }
        }

        public IReadOnlyList<ValidationError> Errors { get; }

        private Result(
            bool isSuccess,
            T? value,
            IReadOnlyList<ValidationError> errors)
        {
            if (isSuccess && value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!isSuccess && (errors is null || errors.Count == 0))
            {
                throw new ArgumentException(
                    "A failure result must contain at least one validation error.",
                    nameof(errors));
            }

            IsSuccess = isSuccess;
            _value = value;

            Errors = new List<ValidationError>(errors).AsReadOnly();
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(
                true,
                value,
                Array.Empty<ValidationError>());
        }

        public static Result<T> Failure(
            IReadOnlyList<ValidationError> errors)
        {
            return new Result<T>(
                false,
                default,
                errors);
        }
    }
}