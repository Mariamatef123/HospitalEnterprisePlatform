using Hospital.Domain.Common;
using System.Text.RegularExpressions;

namespace Hospital.Domain.Employees.ValueObjects
{
    public sealed class PhoneNumber: ValueObject
    {
        public string Value { get; }
        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number is required.");
            value = value.Trim();

            if (!Regex.IsMatch(value, @"^\+?[0-9]{7,15}$"))
                throw new ArgumentException(
                    "Phone number must contain 7 to 15 digits and may start with +.");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    
    }
}