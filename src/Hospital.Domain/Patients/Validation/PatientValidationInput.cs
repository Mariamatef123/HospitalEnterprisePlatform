using Hospital.Domain.Common.ValueObjects;

namespace Hospital.Domain.Patients.Validation
{
    public sealed class PatientValidationInput
    {
        public string NationalId { get; }

        public string FirstArabicName { get; }
        public string LastArabicName { get; }

        public string FirstEnglishName { get; }
        public string LastEnglishName { get; }

        public DateOnly DateOfBirth { get; }

        public Gender Gender { get; }
        public BloodType BloodType { get; }

        public string PhoneNumber { get; }
        public string Email { get; }

        public string Governorate { get; }
        public string City { get; }
        public string Street { get; }
        public string BuildingNumber { get; }

        public string? District { get; }
        public string? ApartmentNumber { get; }
        public string? PostalCode { get; }

        public string? EmergencyName { get; }
        public EmergencyContactRelationship? EmergencyRelationship { get; }
        public string? EmergencyPhoneNumber { get; }

        public PatientValidationInput(
            string nationalId,
            string firstArabicName,
            string lastArabicName,
            string firstEnglishName,
            string lastEnglishName,
            DateOnly dateOfBirth,
            Gender gender,
            BloodType bloodType,
            string phoneNumber,
            string email,
            string governorate,
            string city,
            string street,
            string buildingNumber,
          string? district = null,
          string? apartmentNumber = null,
          string? postalCode = null,
          string? emergencyName = null,
          EmergencyContactRelationship? emergencyRelationship = null,
          string? emergencyPhoneNumber = null)
        {
            NationalId = nationalId;

            FirstArabicName = firstArabicName;
            LastArabicName = lastArabicName;

            FirstEnglishName = firstEnglishName;
            LastEnglishName = lastEnglishName;

            DateOfBirth = dateOfBirth;

            Gender = gender;
            BloodType = bloodType;

            PhoneNumber = phoneNumber;
            Email = email;

            Governorate = governorate;
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;

            District = district;
            ApartmentNumber = apartmentNumber;
            PostalCode = postalCode;

            EmergencyName = emergencyName;
            EmergencyRelationship = emergencyRelationship;
            EmergencyPhoneNumber = emergencyPhoneNumber;
        }
    }
}