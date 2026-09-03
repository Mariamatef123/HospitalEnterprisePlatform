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

            if (value.StartsWith("+"))
                value = value[1..];

            if (!value.All(char.IsDigit))
                throw new ArgumentException(
                    "Phone number must contain digits only.");

            if (value.Length < 7 || value.Length > 15)
                throw new ArgumentException(
                    "Phone number must contain 7 to 15 digits.");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    
    }
}