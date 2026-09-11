using FluentAssertions;
using Hospital.Domain.Patients.ValueObjects;
namespace Hospital.Domain.Tests.Patients.ValueObjects
{
    public class DateOfBirthTests
    {
        [Theory]
        [InlineData(2006, 3, 6)]
        [InlineData(2025, 01, 01)]
        [InlineData(1900, 01, 01)]
        [InlineData(2000, 02, 29)]
        [InlineData(2024, 02, 28)]
        [InlineData(2024, 12, 31)]
        [InlineData(2026, 01, 01)]
        public void DateOfBirth_ValidValues_ShouldBeCreated(int year, int month, int day)
        {
            var date = new DateOnly(year, month, day);

            var dateOfBirth = new DateOfBirth(date);

            dateOfBirth.Value.Should().Be(date);
        }
        [Fact]
        public void DateOfBirth_Today_ShouldBeValid()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var dateOfBirth = new DateOfBirth(today);

            dateOfBirth.Value.Should().Be(today);
        }
        [Fact]
        public void DateOfBirth_Yesterday_ShouldBeValid()
        {
            var yesterday = DateOnly.FromDateTime(DateTime.Today).AddDays(-1);

            var dateOfBirth = new DateOfBirth(yesterday);

            dateOfBirth.Value.Should().Be(yesterday);
        }
        [Theory]
        [InlineData(2206, 3, 6)]
        [InlineData(2027, 09, 10)]
        [InlineData(2026, 09, 28)]
        [InlineData(2026, 10, 10)]
        public void DateOfBirth_FutureDate_ShouldThrowArgumentOutOfRangeException(
            int year,
            int month,
            int day)
        {
            var date = new DateOnly(year, month, day);

            Action act = () => new DateOfBirth(date);

            var exception = act.Should()
                 .Throw<ArgumentOutOfRangeException>()
                 .WithParameterName("value")
                 .Which;

            exception.Message.Should()
                .Contain("Date of birth cannot be in the future.");

            exception.ActualValue.Should().Be(date);
        }
        [Fact]
        public void DateOfBirth_Tomorrow_ShouldThrow()
        {
            var tomorrow = DateOnly.FromDateTime(DateTime.Today).AddDays(1);

            Action act = () => new DateOfBirth(tomorrow);

            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName("value");
        }
        [Fact]
        public void DateOfBirth_Equality_ShouldBeEqual()
        {
            var date1 = new DateOfBirth(new DateOnly(2000, 1, 1));
            var date2 = new DateOfBirth(new DateOnly(2000, 1, 1));

            date1.Should().Be(date2);
            date1.GetHashCode().Should().Be(date2.GetHashCode());
        }
        [Theory]
        [InlineData(2001, 01, 01)]
        [InlineData(2000, 02, 01)]
        [InlineData(2000, 01, 02)]
        public void DateOfBirth_DifferentEqualityComponent_ShouldNotBeEqual(int year, int month, int day)
        {
            var date1 = new DateOfBirth(new DateOnly(year, month, day));
            var date2 = new DateOfBirth(new DateOnly(2000, 01, 01));
            date1.Should().NotBe(date2);
        }
        [Fact]
        public void DateOfBirth_EqualsNull_ShouldBeFalse()
        {
            var date = new DateOfBirth(new DateOnly(2000, 01, 01));
            date.Equals(null).Should().BeFalse();
        }
        [Fact]
        public void DateOfBirth_Equality_ShouldNotBeEqualWithDifferentTypes()
        {
            var date1 = new DateOfBirth(new DateOnly(2000, 01, 01));
            var notAnDate = new { Year = 2000, Month = 01, Day = 01 };
            date1.Equals(notAnDate).Should().BeFalse();

        }
        [Fact]
        public void DateOfBirth_Properties_ShouldBeReadOnly()
        {
            var properties = typeof(DateOfBirth)
                .GetProperties();

            foreach (var property in properties)
            {
                property.CanWrite.Should().BeFalse();
            }
        }

    }
}
