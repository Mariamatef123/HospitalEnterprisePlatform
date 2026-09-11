using Hospital.Domain.Common;

namespace Hospital.Domain.Patients.ValueObjects
{
    public sealed class DateOfBirth : ValueObject
    {
        public DateOnly Value { get; }

        public DateOfBirth(DateOnly value)
        {
            if (value > DateOnly.FromDateTime(DateTime.Today))
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    value,
                    "Date of birth cannot be in the future.");

            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}