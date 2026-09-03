using Hospital.Domain.Common;

namespace Hospital.Domain.Employees.ValueObjects
{
    public sealed class PhoneNumber: ValueObject
    {
        public string Value { get; }
        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number is required.");
            Value = value.Trim();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    
    }
}