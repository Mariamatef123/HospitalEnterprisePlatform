using Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Employees.ValueObjects
{
    public sealed class NationalId : ValueObject
    {
        public string Value { get; }

        public NationalId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("National ID is required.");

            value = value.Trim();

            if (value.Length != 14)
                throw new ArgumentException("National ID must contain exactly 14 digits.");

            if (!value.All(char.IsDigit))
                throw new ArgumentException("National ID must contain digits only.");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
