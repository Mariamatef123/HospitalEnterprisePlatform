using Hospital.Domain.Common;
using Hospital.Domain.Common.ValueObjects;

namespace Hospital.Domain.Patients.ValueObjects
{
    public sealed class ContactInfo : ValueObject
    {
        public PhoneNumber PhoneNumber { get; }
        public EmailAddress EmailAddress { get; }
        public HomeAddress HomeAddress { get; }
        public EmergencyContact? EmergencyContact { get; }

        public ContactInfo(PhoneNumber phoneNumber, EmailAddress emailAddress, HomeAddress homeAddress, EmergencyContact? emergencyContact)
        {
            if (phoneNumber is null)
                throw new ArgumentNullException(nameof(phoneNumber), "Phone number is required.");
            if (emailAddress is null)
                throw new ArgumentNullException(nameof(emailAddress), "Email address is required.");
            if (homeAddress is null)
                throw new ArgumentNullException(nameof(homeAddress), "Home address is required.");
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            HomeAddress = homeAddress;
            EmergencyContact = emergencyContact;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return PhoneNumber;
            yield return EmailAddress;
            yield return HomeAddress;
            yield return EmergencyContact;
        }
    }
}
