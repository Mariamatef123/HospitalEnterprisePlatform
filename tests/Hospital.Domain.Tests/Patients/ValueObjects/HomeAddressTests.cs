using FluentAssertions;
using Hospital.Domain.Patients.ValueObjects;
namespace Hospital.Domain.Tests.Patients.ValueObjects
{
    public class HomeAddressTests
    {
        [Theory]
        [InlineData("Cairo", "Nasr City", "Makram Ebeid","15")]
        [InlineData("Giza","Dokki","Tahrir Street","20")]
        [InlineData("Alexandria","Smouha","Victor Emmanuel","7A")]
        [InlineData("Cairo", "Helwan","Main Street","100")]
        public void HomeAddress_ValidRequiredValues_ShouldBeCreated(
            string governorate,
            string city,
            string street,
            string buildingNumber)
        {
            HomeAddress homeAddress = new HomeAddress(governorate,city, street,buildingNumber);
            homeAddress.Should().NotBeNull();
            homeAddress.Governorate.Should().Be(governorate);
            homeAddress.Street.Should().Be(street);
            homeAddress.City.Should().Be(city);
            homeAddress.BuildingNumber.Should().Be(buildingNumber);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void HomeAddress_InvalidGovernorate_ThrowsArgumentException(string? governorate)
        {
            Action action = () => new HomeAddress(governorate!, "Helwan", "Main Street", "15");
            action.Should().Throw<ArgumentException>()
                  .WithMessage("Governorate is required. (Parameter 'governorate')")
                  .WithParameterName("governorate");
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void HomeAddress_InvalidCity_ThrowsArgumentException(string? city)
        {
            Action action = () => new HomeAddress("Cairo", city!, "Main Street", "15");
            action.Should().Throw<ArgumentException>()
                  .WithMessage("City is required. (Parameter 'city')")
                  .WithParameterName("city");
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void HomeAddress_InvalidStreet_ThrowsArgumentException(string? street)
        {
            Action action = () => new HomeAddress("Cairo", "Helwan", street!, "15");
            action.Should().Throw<ArgumentException>()
                  .WithMessage("Street is required. (Parameter 'street')")
                  .WithParameterName("street");
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void HomeAddress_InvalidBuildingNumber_ThrowsArgumentException(string? buildingNumber)
        {
            Action action = () => new HomeAddress("Cairo", "Helwan", "Main Street", buildingNumber!);
            action.Should().Throw<ArgumentException>()
                  .WithMessage("Building number is required. (Parameter 'buildingNumber')")
                  .WithParameterName("buildingNumber");
        }
        [Fact]
        public void HomeAddress_RequiredValues_ShouldTrim()
        {
            HomeAddress homeAddress = new HomeAddress(" Cairo ", " Helwan ", " Main Street ", " 15 ");
            homeAddress.Governorate.Should().Be("Cairo");
            homeAddress.City.Should().Be("Helwan");
            homeAddress.Street.Should().Be("Main Street");
            homeAddress.BuildingNumber.Should().Be("15");
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void HomeAddress_EmptyDistrict_ShouldBeNull(string? district)
        {
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15", district);
            homeAddress.District.Should().BeNull();
        }
        [Fact]
        public void HomeAddress_ValidDistrict_ShouldBeAccepted()
        {
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15", "Hadayek Helwan");
            homeAddress.District.Should().Be("Hadayek Helwan");
        }
        [Fact]
        public void HomeAddress_District_ShouldBeTrimmed()
        {
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15", "  Hadayek Helwan ");
            homeAddress.District.Should().Be("Hadayek Helwan");
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void HomeAddress_InvalidApartmentNumber_ThrowsArgumentException(string apartmentNumber)
        {
            Action action = () => new HomeAddress("Cairo", "Helwan", "Main Street", "15", apartmentNumber: apartmentNumber);
            action.Should().Throw<ArgumentException>()
                     .WithMessage("Apartment number cannot be empty. (Parameter 'apartmentNumber')")
                     .WithParameterName("apartmentNumber");
        }
        [Fact]
        public void HomeAddress_ApartmentNumberExceeding20Characters_ThrowsArgumentException()
        {
            Action action = () => new HomeAddress("Cairo", "Helwan", "Main Street", "15", apartmentNumber: "123456789012345678901");
            action.Should().Throw<ArgumentException>()
                  .WithMessage("Apartment number cannot exceed 20 characters. (Parameter 'apartmentNumber')")
                  .WithParameterName("apartmentNumber");
        }
        [Theory]
        [InlineData("12")]
        [InlineData("12A")]
        [InlineData("A-12")]
        [InlineData(" 12 ")]
        [InlineData("12345678901234567890")]
        public void HomeAddress_ValidApartmentNumber_ShouldBeAccepted(string apartmentNumber)
        {
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15", apartmentNumber: apartmentNumber);
            homeAddress.ApartmentNumber.Should().Be(apartmentNumber.Trim());
        }
        [Fact]
        public void HomeAddress_NullApartmentNumber_ShouldBeAllowed()
        {
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15", apartmentNumber: null);
            homeAddress.ApartmentNumber.Should().BeNull();
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void HomeAddress_InvalidPostalCode_ThrowsArgumentException(string postalCode)
        {
            Action action = () => new HomeAddress("Cairo", "Helwan", "Main Street", "15", postalCode: postalCode);
            action.Should().Throw<ArgumentException>()
                  .WithMessage("Postal code must contain exactly 5 digits. (Parameter 'postalCode')")
                  .WithParameterName("postalCode");
        }
        [Theory]
        [InlineData("00000")]
        [InlineData("12345")]
        [InlineData(" 12345 ")]
        public void HomeAddress_ValidPostalCode_ShouldBeAccepted(string postalCode)
        {
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15", postalCode: postalCode);
            homeAddress.PostalCode.Should().Be(postalCode.Trim());
        }
        [Fact]
        public void HomeAddress_NullPostalCode_ShouldBeAllowed()
        {
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15", postalCode: null);
            homeAddress.PostalCode.Should().BeNull();
        }
        [Fact]
        public void HomeAddress_PostalCodeWithSpaces_ShouldTrimAndBeValid()
        {
            HomeAddress homeAddress = new HomeAddress("Cairo", "Helwan", "Main Street", "15", postalCode: " 21500 ");
            homeAddress.PostalCode.Should().Be("21500");
        }
        [Theory]
        [InlineData("215003")]
        [InlineData("6270")]
        [InlineData("622 04")]
        [InlineData("ABCDE")]
        public void HomeAddress_InvalidPostalCodeFormats_ShouldThrowArgumentException(string postalCode)
        {
            Action action = () => new HomeAddress("Cairo", "Helwan", "Main Street", "15", postalCode: postalCode);
            action.Should().Throw<ArgumentException>()
                  .WithMessage("Postal code must contain exactly 5 digits. (Parameter 'postalCode')")
                  .WithParameterName("postalCode");
        }
        [Fact]
        public void HomeAddress_Equality_ShouldBeEqual()
        {
            var address1 = new HomeAddress("Cairo", "Helwan", "Main Street", "15", "Hadayek Helwan", "Apt 2", "21500");
            var address2 = new HomeAddress("Cairo", "Helwan", "Main Street", "15", "Hadayek Helwan", "Apt 2", "21500");
            address1.Should().Be(address2);
            address1.GetHashCode().Should().Be(address2.GetHashCode());
        }
        [Theory]
        [InlineData("CA", "Helwan", "Main Street", "15", "Hadayek Helwan", "Apt 2", "21500")]
        [InlineData("Cairo", "Chicago", "Main Street", "15", "Hadayek Helwan", "Apt 2", "21500")]
        [InlineData("Cairo", "Helwan", "Tahrir St", "15", "Hadayek Helwan", "Apt 2", "21500")]
        [InlineData("Cairo", "Helwan", "Main Street", "62705", "Hadayek Helwan", "Apt 2", "21500")]
        [InlineData("Cairo", "Helwan", "Main Street", "15", "Dokki", "Apt 2", "21500")]
        [InlineData("Cairo", "Helwan", "Main Street", "15", "Hadayek Helwan", "Apt 1", "21500")]
        [InlineData("Cairo", "Helwan", "Main Street", "15", "Hadayek Helwan", "Apt 2", "12345")]
        public void HomeAddress_DifferentEqualityComponent_ShouldNotBeEqual(
           string governorate,
           string city,
           string street,
           string buildingNumber,
           string district,
           string apartmentNumber,
           string postalCode)
        {
            var address1 = new HomeAddress(
                "Cairo",
                "Helwan",
                "Main Street",
                "15",
                "Hadayek Helwan",
                "Apt 2",
                "21500");

            var address2 = new HomeAddress(
                governorate,
                city,
                street,
                buildingNumber,
                district,
                apartmentNumber,
                postalCode);

            address1.Should().NotBe(address2);
        }
        [Fact]
        public void HomeAddress_Equality_ShouldNotBeEqualWithNull()
        {
            var address1 = new HomeAddress(
                "Cairo",
                "Helwan",
                "Main Street",
                "15",
                "Hadayek Helwan",
                "Apt 2",
                "21500");

            address1.Equals(null).Should().BeFalse();
        }
        [Fact]
        public void HomeAddress_Equality_ShouldNotBeEqualWithDifferentTypes()
        {
            var address1 = new HomeAddress("Cairo", "Helwan", "Main Street", "15", "Hadayek Helwan", "Apt 2", "21500");
            var notAnAddress = new { Governorate = "Cairo", City = "Helwan" };
            address1.Equals(notAnAddress).Should().BeFalse();

        }
        [Fact]
        public void HomeAddress_TrimmedValues_ShouldBeEqual()
        {
            var address1 = new HomeAddress(" Cairo", "Helwan", "Main Street", " 15", "Hadayek Helwan", "Apt 2", "21500 ");
            var address2 = new HomeAddress("Cairo", " Helwan", "Main Street ", "15", "Hadayek Helwan", "Apt 2 ", "21500");
            address1.Equals(address2).Should().BeTrue();
            address1.GetHashCode().Should().Be(address2.GetHashCode());
            address1.Should().Be(address2);
        }
        [Fact]
        public void HomeAddress_Properties_ShouldBeReadOnly()
        {
            var properties = typeof(HomeAddress)
                .GetProperties();

            foreach (var property in properties)
            {
                property.CanWrite.Should().BeFalse();
            }
        }
    }
}
