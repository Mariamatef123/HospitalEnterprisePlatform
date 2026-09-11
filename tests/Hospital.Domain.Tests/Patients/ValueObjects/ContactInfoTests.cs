using FluentAssertions;
using Hospital.Domain.Common.ValueObjects;
using Hospital.Domain.Patients;
using Hospital.Domain.Patients.ValueObjects;
namespace Hospital.Domain.Tests.Patients.ValueObjects
{
    public class ContactInfoTests
    {
        private ContactInfo CreateContactInfo(string phone, string email, string governorate, string city, string street, string buildingNumber, string fName, string lName, EmergencyContactRelationship rel, string emergencyPhone)
        {
            PhoneNumber phoneNo = new PhoneNumber(phone);
            EmailAddress emailAddress = new EmailAddress(email);
            HomeAddress homeAddress = new HomeAddress(governorate, city, street, buildingNumber);
            EmergencyContact emergencyContact = new EmergencyContact(
                new PersonName(fName, lName),
                rel,
                new PhoneNumber(emergencyPhone));
            ContactInfo contactInfo = new ContactInfo(phoneNo, emailAddress, homeAddress, emergencyContact);
            return contactInfo;
        }

        #region Constructor
        [Fact]
        public void ContactInfo_ValidValues_ShouldBeCreated()
        {
            PhoneNumber phone = new PhoneNumber("01271989509");
            EmailAddress emailAddress = new EmailAddress("Mariamatef353@gmail.com");
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15");
            EmergencyContact emergencyContact = new EmergencyContact(
                new PersonName("Mariam", "Atef"),
                EmergencyContactRelationship.Father,
                new PhoneNumber("01271989519"));
            ContactInfo contactInfo = new ContactInfo(phone, emailAddress, homeAddress,emergencyContact);
            contactInfo.PhoneNumber.Should().Be(phone);
            contactInfo.EmergencyContact.Should().Be(emergencyContact);
            contactInfo.EmailAddress.Should().Be(emailAddress);
            contactInfo.HomeAddress.Should().Be(homeAddress);
        }

        [Fact]
        public void ContactInfo_NullPhoneNumber_ShouldThrowArgumentNullException()
        {
            EmailAddress emailAddress = new EmailAddress("Mariamatef353@gmail.com");
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15");
            EmergencyContact emergencyContact = new EmergencyContact(
                new PersonName("Mariam", "Atef"),
                EmergencyContactRelationship.Father,
                new PhoneNumber("01271989519"));
            Action act = () => new ContactInfo(null!, emailAddress, homeAddress, emergencyContact);
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Phone number is required. (Parameter 'phoneNumber')")
                .WithParameterName("phoneNumber");

        }
        [Fact]
        public void ContactInfo_NullEmailAddress_ShouldThrowArgumentNullException()
        {
            PhoneNumber phone = new PhoneNumber("01271989509");
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15");
            EmergencyContact emergencyContact = new EmergencyContact(
                new PersonName("Mariam", "Atef"),
                EmergencyContactRelationship.Father,
                new PhoneNumber("01271989519"));
            Action act = () => new ContactInfo(phone, null!, homeAddress, emergencyContact);
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Email Address is required. (Parameter 'emailAddress')")
                .WithParameterName("emailAddress");

        }
        [Fact]
        public void ContactInfo_NullHomeAddress_ShouldThrowArgumentNullException()
        {
            PhoneNumber phone = new PhoneNumber("01271989509");
            EmailAddress emailAddress = new EmailAddress("Mariamatef353@gmail.com");
            EmergencyContact emergencyContact = new EmergencyContact(
                new PersonName("Mariam", "Atef"),
                EmergencyContactRelationship.Father,
                new PhoneNumber("01271989519"));
            Action act = () => new ContactInfo(phone, emailAddress, null!, emergencyContact);
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Home address is required. (Parameter 'homeAddress')")
                .WithParameterName("homeAddress");

        }
        [Fact]
        public void ContactInfo_NullEmergencyContact_ShouldBeAllowed()
        {
            PhoneNumber phone = new PhoneNumber("01271989509");
            EmailAddress emailAddress = new EmailAddress("Mariamatef353@gmail.com");
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15");
            EmergencyContact emergencyContact = new EmergencyContact(
                new PersonName("Mariam", "Atef"),
                EmergencyContactRelationship.Father,
                new PhoneNumber("01271989519"));
            Action act = () => new ContactInfo(phone, emailAddress, homeAddress, null);
            act.Should().NotThrow();

        }
        #endregion

        #region Equality
        [Fact]
        public void ContactInfo_Equality_ShouldBeEqual()
        {
            PhoneNumber phone = new PhoneNumber("01271989509");
            EmailAddress emailAddress = new EmailAddress("Mariamatef353@gmail.com");
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15");
            EmergencyContact emergencyContact = new EmergencyContact(
                new PersonName("Mariam", "Atef"),
                EmergencyContactRelationship.Father,
                new PhoneNumber("01271989519"));
            ContactInfo contactInfo1 = new ContactInfo(phone, emailAddress, homeAddress, emergencyContact);
            ContactInfo contactInfo2 = new ContactInfo(phone, emailAddress, homeAddress, emergencyContact);
            contactInfo1.Should().Be(contactInfo2);
            contactInfo1.GetHashCode().Should().Be(contactInfo2.GetHashCode());
            contactInfo1.Equals(contactInfo2).Should().BeTrue();
        }
        [Fact]
        public void ContactInfo_DifferentValues_ShouldNotBeEqual()
        {
            ContactInfo contact1  = CreateContactInfo("01271989509", "Mariamatef353@gmail.com", " Cairo", "Helwan", "Main Street", "15", "Mariam", "Atef", EmergencyContactRelationship.Son, "01271989519");
            ContactInfo contact2 = CreateContactInfo("01271989519", "Mariamatef353@gmail.com", " Cairo", "Helwan", "Main Street", "15", "Mariam", "Atef", EmergencyContactRelationship.Son, "01271989519");
            ContactInfo contact3 = CreateContactInfo("01271989509", "Mariamatef3534@gmail.com", " Cairo", "Helwan", "Main Street", "15", "Mariam", "Atef", EmergencyContactRelationship.Son, "01271989519");
            ContactInfo contact4 = CreateContactInfo("01271989509", "Mariamatef353@gmail.com", " Alex", "Helwan", "Main Street", "15", "Mariam", "Atef", EmergencyContactRelationship.Son, "01271989519");
            ContactInfo contact5 = CreateContactInfo("01271989509", "Mariamatef353@gmail.com", " Cairo", "Helwan", "Main Street", "15", "Mariam", "Atef", EmergencyContactRelationship.Son, "01271289519");
            contact1.Should().NotBe(contact2);
            contact1.Should().NotBe(contact3);
            contact1.Should().NotBe(contact4);
            contact1.Should().NotBe(contact5);
        }


        [Fact]
        public void ContactInfo_EqualsNull_ShouldBeFalse()
        {
            ContactInfo contact1 = CreateContactInfo("01271989509", "Mariamatef353@gmail.com", " Cairo", "Helwan", "Main Street", "15", "Mariam", "Atef", EmergencyContactRelationship.Son, "01271989519");
            contact1.Equals(null).Should().BeFalse();
        }
        [Fact]
        public void ContactInfo_Equality_ShouldNotBeEqualWithDifferentTypes()
        {
            ContactInfo contact1 = CreateContactInfo(
                "01271989509",
                "Mariamatef353@gmail.com",
                "Cairo",
                "Helwan",
                "Main Street",
                "15",
                "Mariam",
                "Atef",
                EmergencyContactRelationship.Son,
                "01271989519");

            var notContactInfo = new
            {
                PhoneNumber = new PhoneNumber("01271989509"),
                EmailAddress = new EmailAddress("Mariamatef353@gmail.com"),
                HomeAddress = new HomeAddress(
                    "Cairo",
                    "Helwan",
                    "Main Street",
                    "15"),
                EmergencyContact = new EmergencyContact(
                    new PersonName("Mariam", "Atef"),
                    EmergencyContactRelationship.Son,
                    new PhoneNumber("01271989519"))
            };

            contact1.Should().NotBe(notContactInfo);
        }
        [Fact]
        public void ContactInfo_BothNullEmergencyContacts_ShouldBeEqual()
        {
            PhoneNumber phone = new PhoneNumber("01271989509");
            EmailAddress email = new EmailAddress("Mariamatef353@gmail.com");
            HomeAddress address = new HomeAddress("Cairo", "Helwan", "Main Street", "15");

            ContactInfo contact1 = new ContactInfo(phone, email, address, null);
            ContactInfo contact2 = new ContactInfo(phone, email, address, null);

            contact1.Should().Be(contact2);
            contact1.GetHashCode().Should().Be(contact2.GetHashCode());
        }
        #endregion

        #region Immutability
        [Fact]
        public void ContactInfo_Properties_ShouldBeReadOnly()
        {
            var properties = typeof(ContactInfo).GetProperties();
            foreach (var property in properties)
            {
                property.CanWrite.Should().BeFalse();
            }
        }
        #endregion
    }

}
