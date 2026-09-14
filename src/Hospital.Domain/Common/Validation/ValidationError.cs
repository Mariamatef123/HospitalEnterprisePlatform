using System;
using System.Collections.Generic;

namespace Hospital.Domain.Common.Validation
{
    public sealed class ValidationError
    {
        public string Code { get; }
        public string Message { get; }
        public string? Field { get; }
        public IReadOnlyList<string> Details { get; }

        public ValidationError(
            string code,
            string message,
            string? field = null,
            IReadOnlyList<string>? details = null)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException(
                    "Validation error code is required.",
                    nameof(code));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(
                    "Validation error message is required.",
                    nameof(message));
            }

            Code = code.Trim();
            Message = message.Trim();

            Field = string.IsNullOrWhiteSpace(field)
                ? null
                : field.Trim();

            Details = details is null
                ? Array.Empty<string>()
                : new List<string>(details).AsReadOnly();
        }
    }
}