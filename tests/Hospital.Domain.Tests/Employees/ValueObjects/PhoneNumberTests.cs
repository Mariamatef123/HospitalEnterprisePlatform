using FluentAssertions;
using Hospital.Domain.Employees.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Tests.Employees.ValueObjects
{
    public class PhoneNumberTests
    {
        [Theory]
        [InlineData("1234567")]
        [InlineData("1234567890")]
        [InlineData("123456789012345")]
        [InlineData("+201012345678")]
        [InlineData("+14155552671")]
        [InlineData("+447911123456")]
        public void Create_WithValidPhoneNumber_ShouldCreatePhoneNumber(string phoneNumber)
        {
            var phone = new PhoneNumber(phoneNumber);
            phone.Value.Should().Be(phoneNumber.Replace("+",""));
        }
        [Fact]
        public void Create_WithSpacesAroundPhoneNumber_TrimsSpaces()
        {
            var phone = new PhoneNumber("  +14155552671  ");

            phone.Value.Should().Be("14155552671");
        }
        [Theory]
        [InlineData("123456")]
        [InlineData("1234567890123456")]
        public void Create_WithInvalidLengthPhoneNumber_ThrowsException(string phoneNumber)
        {
            Action act = () => new PhoneNumber(phoneNumber);
            act.Should().Throw<ArgumentException>().WithMessage("Phone number must contain 7 to 15 digits.");
        }
        [Theory]
        [InlineData("(123) 456-7890")]
        [InlineData("1234567abc")]
        [InlineData("123-abc-7890")]
        [InlineData("abc1234567")]
        [InlineData("123-456-7890")]
        [InlineData("123.456.7890")]
        [InlineData("123 456 7890")]
        public void Create_WithInvalidFormatPhoneNumber_ThrowsException(string phoneNumber)
        {
            Action act = () => new PhoneNumber(phoneNumber);
            act.Should().Throw<ArgumentException>().WithMessage("Phone number must contain digits only.");
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_WithMissingPhoneNumber_ThrowsException(string? phoneNumber)
        {
            Action act = () => new PhoneNumber(phoneNumber!);

            act.Should()
               .Throw<ArgumentException>()
               .WithMessage("Phone number is required.");
        }
        [Fact]
        public void PhoneNumber_WithSameNumberDifferentFormat_ShouldBeEqual()
        {
            var phone1 = new PhoneNumber("+14155552671");
            var phone2 = new PhoneNumber("14155552671");

            phone1.Should().Be(phone2);
        }

        [Fact]
        public void PhoneNumber_WithDifferentNumbers_ShouldNotBeEqual()
        {
            var phone1 = new PhoneNumber("+14155552671");
            var phone2 = new PhoneNumber("+447911123456");

            phone1.Should().NotBe(phone2);
        }
    }
}
