using FluentAssertions;
using Hospital.Domain.Common.ValueObjects;
namespace Hospital.Domain.Tests.Common.ValueObjects
{
    public class EmailAddressTests
    {
        [Theory]
        [InlineData("mariamAtef@gmail.com")]
        [InlineData("mariamAtef@hospital.com")]
        [InlineData("mariam.Atef@hospital.com")]
        [InlineData("mariam_Atef@hospital.com")]
        [InlineData("mariam-Atef@hospital.com")]
        [InlineData("mariam+Atef@hospital.com")]
        [InlineData("mariam%Atef@hospital.com")]
        [InlineData("mariam123@hospital.com")]
        [InlineData("a@b.co")]
        [InlineData("user@example.co.uk")]
        [InlineData("user@example-domain.com")]
        [InlineData("mariam_test@hospital.com")]
        [InlineData("mariam-test@hospital.com")]
        [InlineData("mariam.test@hospital.com")]
        public void EmailAddress_ShouldBeValid(string email)
        {
            var emailAddress = new EmailAddress(email);
            emailAddress.Value.Should().Be(email);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void EmailAddress_NullOrEmptyOrWhitespace_ThrowsArgumentException(string? email)
        {
            Action act = () => new EmailAddress(email!);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Email address is required. (Parameter 'value')")
                .WithParameterName("value");
        }
        [Theory]
        [InlineData(" mariamAtef@gmail.com")]
        [InlineData("mariamAtef@gmail.com ")]
        [InlineData(" mariamAtef@gmail.com ")]
        [InlineData("     mariamAtef@gmail.com       ")]
        public void EmailAddress_LeadingOrTrailingSpaces_ShouldTrimAndBeValid(string email)
        {
            var emailAddress = new EmailAddress(email);
            emailAddress.Value.Should().Be(email.Trim());
        }
        [Theory]
        [InlineData("mariamAtefgmail.com")]
        [InlineData("@.com")]
        [InlineData("mariamAtef@")]
        [InlineData("mariam@Atef@hospital.com")]
        public void EmailAddress_MissingOrInvalidFormat_ThrowsArgumentException(string email)
        {
            Action act = () => new EmailAddress(email);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Email address is not in a valid format. (Parameter 'value')")
                .WithParameterName("value");
        }
        [Theory]
        [InlineData(".mariamAtef@gmail.com")]
        [InlineData("mariamAtef.@gmail.com")]
        [InlineData("mariam..Atef@gmail.com")]
        [InlineData("mariam Atef@gmail.com")]
        [InlineData("mariam#Atef@gmail.com")]
        [InlineData("mariam!Atef@gmail.com")]
        [InlineData("user?name@example.com")]
        public void EmailAddress_InvalidLocalPart_ThrowsArgumentException(string email)
        {
            Action act = () => new EmailAddress(email);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Email address is not in a valid format. (Parameter 'value')")
                .WithParameterName("value");
        }
        [Theory]
        [InlineData("mariam@")]
        [InlineData("mariam@.gmail.com")]
        [InlineData("mariam@gmail.com.")]
        [InlineData("mariam@gmail..com")]
        [InlineData("mariam@-gmail.com")]
        [InlineData("mariam@gmail-.com")]
        [InlineData("mariam@gm_ail.com")]
        [InlineData("mariam@gmail .com")]
        public void EmailAddress_InvalidDomain_ThrowsArgumentException(string email)
        {
            Action act = () => new EmailAddress(email);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Email address is not in a valid format. (Parameter 'value')")
                .WithParameterName("value");
        }
        [Fact]
        public void EmailAddress_Equality_ShouldBeEqual()
        {
            var email1 = new EmailAddress("MariamAtef@gmail.com");
            var email2 = new EmailAddress("MariamAtef@gmail.com");
            email1.Equals(email2).Should().BeTrue();
            email1.Should().Be(email2);
            email1.GetHashCode().Should().Be(email2.GetHashCode());
        }
        [Fact]
        public void EmailAddress_TrimmedValues_ShouldBeEqual()
        {
            var email1 = new EmailAddress("MariamAtef@gmail.com");
            var email2 = new EmailAddress("  MariamAtef@gmail.com  ");

            email1.Should().Be(email2);
            email1.GetHashCode().Should().Be(email2.GetHashCode());
        }
        [Fact]
        public void EmailAddress_Equality_ShouldNotBeEqualWithDifferentValue()
        {
            var email1 = new EmailAddress("Mariamatef@gmail.com");
            var email2 = new EmailAddress("Mariamatef1@gmail.com");
            email1.Should().NotBe(email2);
            email1.GetHashCode().Should().NotBe(email2.GetHashCode());
        }
        [Fact]
        public void EmailAddress_Equality_ShouldNotBeEqualToNull()
        {
            var email = new EmailAddress("Mariamatef@gmail.com");
            email.Equals(null).Should().BeFalse();
        }
        [Fact]
        public void EmailAddress_Equality_ShouldNotBeEqualToDifferentType()
        {
            var email = new EmailAddress("Mariamatef@gmail.com");
            email.Equals("Mariamatef@gmail.com").Should().BeFalse();
        }
        [Fact]
        public void EmailAddress_Equality_ShouldBeCaseSensitive()
        {
            var email1 = new EmailAddress("Mariamatef@gmail.com");
            var email2 = new EmailAddress("mariamatef@gmail.com");
            email1.Should().NotBe(email2);
        }
        [Fact]
        public void EmailAddress_Value_ShouldRemainUnchanged()
        {
            var emailAddress = new EmailAddress("MariamAtef@gmail.com");

            var originalValue = emailAddress.Value;

            emailAddress.Value.Should().Be(originalValue);
        }
    }
}