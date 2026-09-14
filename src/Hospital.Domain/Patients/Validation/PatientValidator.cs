using Hospital.Domain.Common.Validation;
namespace Hospital.Domain.Patients.Validation
{
    public sealed class PatientValidator
    {
        public Result Validate(PatientValidationInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var errors = new List<ValidationError>();

            ValidateRequiredFields(input, errors);
            ValidateNationalIdLength(input, errors);
            ValidateNationalIdFormat(input, errors);
            ValidateNames(input, errors);
            ValidateDateOfBirth(input, errors);
            ValidateEnums(input, errors);
            ValidateEmergencyContact(input, errors);

            if (errors.Count > 0)
            {
                return Result.Failure(errors);
            }

            return Result.Success();
        }

        private static void ValidateRequiredFields(
            PatientValidationInput input,
            List<ValidationError> errors)
        {
            if (string.IsNullOrWhiteSpace(input.NationalId))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.NATIONAL_ID.REQUIRED",
                        message: "National ID is required.",
                        field: nameof(PatientValidationInput.NationalId)));
            }

            if (string.IsNullOrWhiteSpace(input.FirstArabicName))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.ARABIC_FIRST_NAME.REQUIRED",
                        message: "Arabic first name is required.",
                        field: nameof(PatientValidationInput.FirstArabicName)));
            }

            if (string.IsNullOrWhiteSpace(input.LastArabicName))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.ARABIC_LAST_NAME.REQUIRED",
                        message: "Arabic last name is required.",
                        field: nameof(PatientValidationInput.LastArabicName)));
            }

            if (string.IsNullOrWhiteSpace(input.FirstEnglishName))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.ENGLISH_FIRST_NAME.REQUIRED",
                        message: "English first name is required.",
                        field: nameof(PatientValidationInput.FirstEnglishName)));
            }

            if (string.IsNullOrWhiteSpace(input.LastEnglishName))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.ENGLISH_LAST_NAME.REQUIRED",
                        message: "English last name is required.",
                        field: nameof(PatientValidationInput.LastEnglishName)));
            }

            if (string.IsNullOrWhiteSpace(input.PhoneNumber))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.PHONE_NUMBER.REQUIRED",
                        message: "Phone number is required.",
                        field: nameof(PatientValidationInput.PhoneNumber)));
            }

            if (string.IsNullOrWhiteSpace(input.Email))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.EMAIL.REQUIRED",
                        message: "Email address is required.",
                        field: nameof(PatientValidationInput.Email)));
            }

            if (string.IsNullOrWhiteSpace(input.Governorate))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.GOVERNORATE.REQUIRED",
                        message: "Governorate is required.",
                        field: nameof(PatientValidationInput.Governorate)));
            }

            if (string.IsNullOrWhiteSpace(input.City))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.CITY.REQUIRED",
                        message: "City is required.",
                        field: nameof(PatientValidationInput.City)));
            }

            if (string.IsNullOrWhiteSpace(input.Street))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.STREET.REQUIRED",
                        message: "Street is required.",
                        field: nameof(PatientValidationInput.Street)));
            }

            if (string.IsNullOrWhiteSpace(input.BuildingNumber))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.BUILDING_NUMBER.REQUIRED",
                        message: "Building number is required.",
                        field: nameof(PatientValidationInput.BuildingNumber)));
            }
        }

        private static void ValidateNationalIdLength(
            PatientValidationInput input,
            List<ValidationError> errors)
        {
            if (string.IsNullOrWhiteSpace(input.NationalId))
            {
                return;
            }

            string nationalId = input.NationalId.Trim();

            if (nationalId.Length != 14)
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.NATIONAL_ID.LENGTH_INVALID",
                        message: "National ID must contain exactly 14 digits.",
                        field: nameof(PatientValidationInput.NationalId)));
            }
        }

        private static void ValidateNationalIdFormat(
            PatientValidationInput input,
            List<ValidationError> errors)
        {
            if (string.IsNullOrWhiteSpace(input.NationalId))
            {
                return;
            }

            string nationalId = input.NationalId.Trim();

            foreach (char character in nationalId)
            {
                if (character >= '0' && character <= '9')
                {
                    continue;
                }

                errors.Add(
                    new ValidationError(
                        code: "PATIENT.NATIONAL_ID.FORMAT_INVALID",
                        message: "National ID must contain digits only.",
                        field: nameof(PatientValidationInput.NationalId)));

                break;
            }
        }

        private static void ValidateNames(
            PatientValidationInput input,
            List<ValidationError> errors)
        {
            ValidateArabicName(
                input.FirstArabicName,
                nameof(PatientValidationInput.FirstArabicName),
                errors);

            ValidateArabicName(
                input.LastArabicName,
                nameof(PatientValidationInput.LastArabicName),
                errors);

            ValidateEnglishName(
                input.FirstEnglishName,
                nameof(PatientValidationInput.FirstEnglishName),
                errors);

            ValidateEnglishName(
                input.LastEnglishName,
                nameof(PatientValidationInput.LastEnglishName),
                errors);
        }

        private static void ValidateArabicName(
            string name,
            string field,
            List<ValidationError> errors)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            foreach (char character in name)
            {
                if (character == ' ')
                {
                    continue;
                }

                if (character >= '\u0600' &&
                    character <= '\u06FF' &&
                    char.IsLetter(character))
                {
                    continue;
                }

                errors.Add(
                    new ValidationError(
                        code: "PATIENT.NAME.INVALID",
                        message: "Arabic name must contain Arabic letters only.",
                        field: field));

                break;
            }
        }

        private static void ValidateEnglishName(
            string name,
            string field,
            List<ValidationError> errors)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            foreach (char character in name)
            {
                if (character == ' ')
                {
                    continue;
                }

                if ((character >= 'A' && character <= 'Z') ||
                    (character >= 'a' && character <= 'z'))
                {
                    continue;
                }

                errors.Add(
                    new ValidationError(
                        code: "PATIENT.NAME.INVALID",
                        message: "English name must contain English letters only.",
                        field: field));

                break;
            }
        }

        private static void ValidateDateOfBirth(
            PatientValidationInput input,
            List<ValidationError> errors)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            if (input.DateOfBirth > today)
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.DATE_OF_BIRTH.FUTURE",
                        message: "Date of birth cannot be in the future.",
                        field: nameof(PatientValidationInput.DateOfBirth)));
            }
        }

        private static void ValidateEnums(
            PatientValidationInput input,
            List<ValidationError> errors)
        {
            if (!Enum.IsDefined(input.Gender))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.GENDER.INVALID",
                        message: "Gender is invalid.",
                        field: nameof(PatientValidationInput.Gender)));
            }

            if (!Enum.IsDefined(input.BloodType))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.BLOOD_TYPE.INVALID",
                        message: "Blood type is invalid.",
                        field: nameof(PatientValidationInput.BloodType)));
            }
        }

        private static void ValidateEmergencyContact(
            PatientValidationInput input,
            List<ValidationError> errors)
        {
            bool hasName =
                !string.IsNullOrWhiteSpace(input.EmergencyName);

            bool hasRelationship =
                input.EmergencyRelationship is not null;

            bool hasPhoneNumber =
                !string.IsNullOrWhiteSpace(input.EmergencyPhoneNumber);

            if (!hasName && !hasRelationship && !hasPhoneNumber)
            {
                return;
            }

            if (!hasName)
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.EMERGENCY_CONTACT.NAME_REQUIRED",
                        message: "Emergency contact name is required.",
                        field: nameof(PatientValidationInput.EmergencyName)));
            }

            if (!hasRelationship)
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.EMERGENCY_CONTACT.RELATIONSHIP_REQUIRED",
                        message: "Emergency contact relationship is required.",
                        field: nameof(PatientValidationInput.EmergencyRelationship)));
            }
            else if (!Enum.IsDefined(input.EmergencyRelationship.Value))
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.EMERGENCY_CONTACT.RELATIONSHIP.INVALID",
                        message: "Emergency contact relationship is invalid.",
                        field: nameof(PatientValidationInput.EmergencyRelationship)));
            }

            if (!hasPhoneNumber)
            {
                errors.Add(
                    new ValidationError(
                        code: "PATIENT.EMERGENCY_CONTACT.PHONE_NUMBER_REQUIRED",
                        message: "Emergency contact phone number is required.",
                        field: nameof(PatientValidationInput.EmergencyPhoneNumber)));
            }
        }
    }
}

