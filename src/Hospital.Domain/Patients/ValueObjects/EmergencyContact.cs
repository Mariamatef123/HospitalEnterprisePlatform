using Hospital.Domain.Common;
using Hospital.Domain.Common.ValueObjects;
namespace Hospital.Domain.Patients.ValueObjects
{
    public sealed class EmergencyContact : ValueObject
    {
        public PersonName Name { get; }
        public EmergencyContactRelationship Relationship { get; }
        public PhoneNumber PhoneNumber { get; }

        public EmergencyContact(
            PersonName name,
            EmergencyContactRelationship relationship,
            PhoneNumber phoneNumber)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (phoneNumber is null)
                throw new ArgumentNullException(nameof(phoneNumber));

            Name = name;
            Relationship = relationship;
            PhoneNumber = phoneNumber;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Name;
            yield return Relationship;
            yield return PhoneNumber;
        }
    }
}