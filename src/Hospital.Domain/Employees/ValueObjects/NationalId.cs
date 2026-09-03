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

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
