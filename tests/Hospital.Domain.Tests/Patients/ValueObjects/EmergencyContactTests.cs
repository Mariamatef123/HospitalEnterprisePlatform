using FluentAssertions;
using Hospital.Domain.Common.ValueObjects;
using Hospital.Domain.Patients;
using Hospital.Domain.Patients.ValueObjects;
namespace Hospital.Domain.Tests.Patients.ValueObjects
{
    public class EmergencyContactTests
    {
        #region Constructor
        [Fact]
        public void EmergencyContact_ValidValues_ShouldBeCreated()
        {
            PersonName personName = new PersonName("Mariam", "Atef");
            PhoneNumber phone = new PhoneNumber("01271989509");
            EmergencyContact emergencyContact = new EmergencyContact(
                personName,
                EmergencyContactRelationship.Father,
                phone);
            emergencyContact.PhoneNumber.Should().Be(phone);
            emergencyContact.Relationship.Should().Be(EmergencyContactRelationship.Father);
            emergencyContact.Name.Should().Be(personName);
        }
        [Fact]
        public void EmergencyContact_NullName_ShouldThrowArgumentNullException()
        {
            PhoneNumber phone = new PhoneNumber("01271989509");
            Action act = () => new EmergencyContact(
                 null!,
                 EmergencyContactRelationship.Father,
                 phone);
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("name");
        }

        [Fact]
        public void EmergencyContact_NullPhoneNumber_ShouldThrowArgumentNullException()
        {
            PersonName personName = new PersonName("Mariam", "Atef");
            Action act = () => new EmergencyContact(
               personName,
                 EmergencyContactRelationship.Father,
                 null!);
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("phoneNumber");

        }
        #endregion

        #region Equality
        [Fact]
        public void EmergencyContact_Equality_ShouldBeEqual()
        {
            PersonName personName = new PersonName("Mariam", "Atef");
            PhoneNumber phone = new PhoneNumber("01271989509");
            EmergencyContact emergencyContact1 = new EmergencyContact(
                personName,
                EmergencyContactRelationship.Father,
                phone);
            EmergencyContact emergencyContact2 = new EmergencyContact(
                personName,
                EmergencyContactRelationship.Father,
                phone);
            emergencyContact1.Should().Be(emergencyContact2);
            emergencyContact1.GetHashCode().Should().Be(emergencyContact2.GetHashCode());
            emergencyContact1.Equals(emergencyContact2).Should().BeTrue();
        }
        [Fact]
        public void EmergencyContact_DifferentValues_ShouldNotBeEqual()
        {
            EmergencyContact emergencyContact = new EmergencyContact(
                new PersonName("Mariam", "Atef"),
                EmergencyContactRelationship.Father,
                new PhoneNumber("01271989509"));
            EmergencyContact emergencyContact1 = new EmergencyContact(
                 new PersonName("Maria", "Atef"),
                EmergencyContactRelationship.Father,
                new PhoneNumber("01271989509"));
            EmergencyContact emergencyContact2 = new EmergencyContact(
                new PersonName("Mariam", "Atef"),
               EmergencyContactRelationship.Son,
               new PhoneNumber("01271989509"));
            EmergencyContact emergencyContact3 = new EmergencyContact(
                new PersonName("Mariam", "Atef"),
               EmergencyContactRelationship.Father,
               new PhoneNumber("01271989508"));
            emergencyContact.Should().NotBe(emergencyContact1);
            emergencyContact.Should().NotBe(emergencyContact2);
            emergencyContact.Should().NotBe(emergencyContact3);
        }
        [Fact]
        public void EmergencyContact_EqualsNull_ShouldBeFalse()
        {
            EmergencyContact emergencyContact = new EmergencyContact(
                   new PersonName("Mariam", "Atef"),
                   EmergencyContactRelationship.Father,
                   new PhoneNumber("01271989509"));
            emergencyContact.Equals(null).Should().BeFalse();
        }
        [Fact]
        public void EmergencyContact_Equality_ShouldNotBeEqualWithDifferentTypes()
        {
            EmergencyContact emergencyContact = new EmergencyContact(
                   new PersonName("Mariam", "Atef"),
                   EmergencyContactRelationship.Father,
                   new PhoneNumber("01271989509"));
            var notEmergencyContact = new
            {
                Name = new PersonName("Mariam", "Atef"),
                Relationship = EmergencyContactRelationship.Father,
                PhoneNumber = new PhoneNumber("01271989509")
            };
            emergencyContact.Should().NotBe(notEmergencyContact);

        }
        #endregion

        #region Immutability
        [Fact]
        public void EmergencyContact_Properties_ShouldBeReadOnly()
        {
            var properties = typeof(EmergencyContact).GetProperties();
            foreach (var property in properties)
            {
                property.CanWrite.Should().BeFalse();
            }
        }
        #endregion

        #region Enum
        [Theory]
        [InlineData(EmergencyContactRelationship.Brother)]
        [InlineData(EmergencyContactRelationship.Father)]
        [InlineData(EmergencyContactRelationship.Mother)]
        [InlineData(EmergencyContactRelationship.Sister)]
        [InlineData(EmergencyContactRelationship.Son)]
        [InlineData(EmergencyContactRelationship.Daughter)]
        [InlineData(EmergencyContactRelationship.Other)]
        public void AllRelationshipValues_ShouldBeAccepted(EmergencyContactRelationship relationship)
        {
            PersonName personName = new PersonName("Mariam", "Atef");
            PhoneNumber phone = new PhoneNumber("01271989509");
            EmergencyContact emergencyContact = new EmergencyContact(
                          personName,
                          relationship,
                          phone);
            emergencyContact.Relationship.Should().Be(relationship);
        }
        #endregion
    }

}
