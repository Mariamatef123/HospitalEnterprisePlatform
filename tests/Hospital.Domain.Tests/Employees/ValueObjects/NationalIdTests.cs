using FluentAssertions;
using Hospital.Domain.Employees.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Tests.Employees.ValueObjects
{
    public class NationalIdTests
    {
        [Fact]
        public void Create_WithValidNationalId_CreatesNationalId()
        {
            var nationalId1 = new NationalId("30203546982365");
            nationalId1.Value.Should().Be("30203546982365");
        }
        [Fact]
        public void Create_WithSpacesAroundNationalId_TrimsSpaces()
        {
            var nationalId = new NationalId("  30203546982365  ");

            nationalId.Value.Should().Be("30203546982365");
        }
        [Theory]
        [InlineData("123456")]
        [InlineData("123456789012555553456")]
        public void Create_WithInvalidLengthNationalId_ThrowsException(string nationalId)
        {
            Action act = () => new NationalId(nationalId);
            act.Should().Throw<ArgumentException>().WithMessage("National ID must contain exactly 14 digits.");
        }
        [Theory]
        [InlineData("30203546982abc")]
        [InlineData("abc30203546982")]
        [InlineData("30203abs546982")]
        public void Create_WithInvalidFormatNationalId_ThrowsException(string nationalId)
        {
            Action act = () => new NationalId(nationalId);
            act.Should().Throw<ArgumentException>().WithMessage("National ID must contain digits only.");
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_WithMissingNationalId_ThrowsException(string? nationalId)
        {
            Action act = () => new NationalId(nationalId!);

            act.Should()
               .Throw<ArgumentException>()
               .WithMessage("National ID is required.");
        }
        [Fact]
        public void NationalId_WithSameValues_ShouldBeEqual()
        {
            var nationalId1 = new NationalId("30203546982365");
            var nationalId2 = new NationalId("30203546982365");

            nationalId1.Should().Be(nationalId2);
        }

        [Fact]
        public void NationalId_WithDifferentValues_ShouldNotBeEqual()
        {
            var nationalId1 = new NationalId("30203546982365");
            var nationalId2 = new NationalId("30203546982366");

            nationalId1.Should().NotBe(nationalId2);
        }
    }
}
