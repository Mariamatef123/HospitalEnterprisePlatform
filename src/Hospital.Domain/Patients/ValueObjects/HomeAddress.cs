using Hospital.Domain.Common;
namespace Hospital.Domain.Patients.ValueObjects
{
  public sealed class HomeAddress : ValueObject
   {
            public string Governorate { get; }
            public string City { get; }
            public string Street { get; }
            public string BuildingNumber { get; }

            public string? District { get; }
            public string? ApartmentNumber { get; }
            public string? PostalCode { get; }

        public HomeAddress(string governorate, string city, string street, string buildingNumber, string? district = null, string? apartmentNumber = null, string? postalCode = null)
        {
            if(string.IsNullOrWhiteSpace(governorate))
                throw new ArgumentException("Governorate is required.", nameof(governorate));
            if(string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required.", nameof(city));
            if(string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street is required.", nameof(street));
            if(string.IsNullOrWhiteSpace(buildingNumber))
                throw new ArgumentException("Building number is required.", nameof(buildingNumber));
           

            Governorate = governorate.Trim();
            City = city.Trim();
            Street = street.Trim();
            BuildingNumber = buildingNumber.Trim();
            District = string.IsNullOrWhiteSpace(district)
                ? null
                : district.Trim();

            if (apartmentNumber is not null)
            {
                apartmentNumber = apartmentNumber.Trim();

                if (apartmentNumber.Length == 0)
                    throw new ArgumentException(
                        "Apartment number cannot be empty.",
                        nameof(apartmentNumber));

                if (apartmentNumber.Length > 20)
                    throw new ArgumentException(
                        "Apartment number cannot exceed 20 characters.",
                        nameof(apartmentNumber));
            }

            ApartmentNumber = apartmentNumber;

            if (postalCode is not null)
            {
                postalCode = postalCode.Trim();

                if (postalCode.Length != 5 || !postalCode.All(char.IsDigit))
                    throw new ArgumentException(
                        "Postal code must contain exactly 5 digits.",
                        nameof(postalCode));
            }

            PostalCode = postalCode;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
          yield return Governorate;
          yield return City;
          yield return Street;
          yield return BuildingNumber;
          yield return District;
          yield return ApartmentNumber;
          yield return PostalCode;
        }
    }
    
}
