# Patient Registration Use Case

## 1. Use Case Overview

**Use Case:** Register Patient
**Actor:** Receptionist
**Goal:** Register a new patient in the hospital system after validating the provided patient information and ensuring that the National ID is not already registered.

---

## 2. Actor

### Receptionist

The receptionist is the hospital staff member responsible for entering and submitting patient registration information.

The receptionist provides the required patient information through the registration interface. The actual registration workflow will be implemented in the Application layer in a later phase.

---

## 3. Registration Input

The registration process receives the following patient information:

### Patient Identity

* National ID
* Arabic first name
* Arabic last name
* English first name
* English last name
* Date of birth
* Gender
* Blood type

### Contact Information

* Phone number
* Email address

### Home Address

* Governorate
* City
* Street
* Building number
* District (optional)
* Apartment number (optional)
* Postal code (optional)

### Emergency Contact

* Name (optional)
* Relationship (optional)
* Phone number (optional)

Emergency contact information is optional as a group. If any emergency contact field is provided, the required parts of the emergency contact must be completed.

---

## 4. Preconditions

Before patient registration:

1. The receptionist is authorized to register patients.
2. The registration input is available.
3. The system is able to perform patient validation.
4. The system is able to check existing patient National IDs.

---

## 5. Main Success Flow

1. The receptionist enters the patient's information.
2. The system receives the registration input.
3. The system validates the input.
4. If validation succeeds, the system checks whether the National ID already exists.
5. If the National ID does not already exist, the system creates a new `Patient`.
6. The new patient is assigned a unique patient identifier.
7. The patient is registered successfully.
8. The system returns a successful registration result.

---

## 6. Validation

The system validates the registration input before creating the `Patient`.

Validation includes:

* Required fields must be provided.
* National ID must contain exactly 14 digits.
* National ID must contain digits only.
* Arabic names must contain valid Arabic letters.
* English names must contain valid English letters.
* Names must not be empty or whitespace-only.
* Date of birth must not be in the future.
* Gender must contain a valid defined value.
* Blood type must contain a valid defined value.
* If emergency contact information is partially provided, the required emergency contact fields must be completed.

Validation failures are expected business/input failures and are returned using `Result` and `ValidationError`.

The validator must collect applicable validation errors rather than stopping at the first error.

---

## 7. Duplicate National ID Check

After the input passes validation, the system checks whether a patient with the same National ID already exists.

### If the National ID already exists

Registration is rejected.

The system returns a validation/business error:

**Code:** `PATIENT.NATIONAL_ID.DUPLICATE`

**Message:** `A patient with this National ID already exists.`

The existing patient must not be overwritten or duplicated.

### If the National ID does not exist

The registration process continues and a new patient can be created.

---

## 8. Validation Failure Flow

If the registration input fails validation:

1. The system does not create a `Patient`.
2. The system does not perform patient registration.
3. The system returns a failed `Result`.
4. The returned result contains one or more `ValidationError` objects.
5. Each error contains a machine-readable error code, message, and affected field when applicable.
6. The receptionist can correct the invalid information and submit the registration again.

Example:

```text
Input
  ↓
PatientValidator
  ↓
Validation Failure
  ↓
Result.Failure
  ↓
ValidationError collection
```

Example errors:

* `PATIENT.NATIONAL_ID.REQUIRED`
* `PATIENT.NATIONAL_ID.LENGTH_INVALID`
* `PATIENT.NATIONAL_ID.FORMAT_INVALID`
* `PATIENT.ARABIC_FIRST_NAME.REQUIRED`
* `PATIENT.NAME.INVALID`
* `PATIENT.DATE_OF_BIRTH.FUTURE`

---

## 9. Unexpected Technical Failure

Unexpected technical failures are different from expected validation failures.

Examples include:

* Corrupted persistence file
* File access or permission failure
* Unexpected infrastructure failure
* Plugin loading failure
* Unexpected system exception

These failures must not be converted into normal validation errors.

The system should allow the unexpected exception to propagate to the appropriate Application or Infrastructure error-handling boundary, where it can be logged and handled appropriately.

The validation layer must not use a broad exception handler to hide unexpected technical failures.

Example:

```text
Technical Failure
      ↓
Exception
      ↓
Application / Infrastructure handling
      ↓
Logging / appropriate error response
```

---

## 10. Successful Registration

A registration is successful when:

1. The input passes validation.
2. The National ID does not already exist.
3. A valid `Patient` aggregate is created.
4. The patient is successfully registered by the appropriate application workflow.

The successful result may return the created patient or its identifier depending on the final Application-layer use case design.

---

## 11. Postconditions

### Successful Registration

After successful registration:

* A new patient exists in the system.
* The patient has a unique Patient ID.
* The patient's National ID is registered.
* The patient starts with the default `Active` status.
* The patient information satisfies the domain invariants.

### Failed Validation

After validation failure:

* No patient is created.
* No patient data is registered.
* Validation errors are returned to the caller.

### Duplicate National ID

After a duplicate National ID failure:

* No new patient is created.
* The existing patient remains unchanged.
* The duplicate error is returned.

### Unexpected Technical Failure

After an unexpected technical failure:

* The system does not treat the failure as a normal validation error.
* The exception is handled by the appropriate higher-level boundary.
* The failure should be logged according to the application's error-handling strategy.

---

## 12. Error Messages

| Error Code                            | Message                                         | Field                 |
| ------------------------------------- | ----------------------------------------------- | --------------------- |
| `PATIENT.NATIONAL_ID.REQUIRED`        | National ID is required.                        | `NationalId`          |
| `PATIENT.NATIONAL_ID.LENGTH_INVALID`  | National ID must contain exactly 14 digits.     | `NationalId`          |
| `PATIENT.NATIONAL_ID.FORMAT_INVALID`  | National ID must contain digits only.           | `NationalId`          |
| `PATIENT.NATIONAL_ID.DUPLICATE`       | A patient with this National ID already exists. | `NationalId`          |
| `PATIENT.ARABIC_FIRST_NAME.REQUIRED`  | Arabic first name is required.                  | `FirstArabicName`     |
| `PATIENT.ARABIC_LAST_NAME.REQUIRED`   | Arabic last name is required.                   | `LastArabicName`      |
| `PATIENT.ENGLISH_FIRST_NAME.REQUIRED` | English first name is required.                 | `FirstEnglishName`    |
| `PATIENT.ENGLISH_LAST_NAME.REQUIRED`  | English last name is required.                  | `LastEnglishName`     |
| `PATIENT.NAME.INVALID`                | Name contains invalid characters.               | Applicable name field |
| `PATIENT.DATE_OF_BIRTH.FUTURE`        | Date of birth cannot be in the future.          | `DateOfBirth`         |
| `PATIENT.GENDER.INVALID`              | Gender is invalid.                              | `Gender`              |
| `PATIENT.BLOOD_TYPE.INVALID`          | Blood type is invalid.                          | `BloodType`           |

---

## 13. Expected vs Unexpected Failures

| Failure Type                 | Example                | Handling         |
| ---------------------------- | ---------------------- | ---------------- |
| Expected validation failure  | Empty name             | `Result.Failure` |
| Expected validation failure  | Invalid National ID    | `Result.Failure` |
| Business failure             | Duplicate National ID  | `Result.Failure` |
| Unexpected technical failure | Corrupted file         | Exception        |
| Unexpected technical failure | Permission error       | Exception        |
| Unexpected technical failure | Plugin loading failure | Exception        |

The key rule is:

> **Expected business/input failures are represented explicitly with `Result` and `ValidationError`; unexpected technical failures remain exceptions and are handled at the appropriate higher application/infrastructure boundary.**
