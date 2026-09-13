# Validation Strategy

## Purpose

This document defines the validation strategy for the **Hospital Domain**.

The domain uses the **Result Pattern** to represent expected validation and business-rule failures explicitly.

Unexpected technical failures are not converted into `Result` failures. They remain exceptions and are handled at the appropriate application or infrastructure boundary.

---

## 1. Domain Validation

Domain validation ensures that domain objects and operations satisfy the rules defined by the Hospital domain.

Examples include:

* Invalid National ID
* Missing required patient information
* Invalid patient name
* Invalid date of birth
* Invalid phone number
* Invalid domain value
* Violation of a domain business rule

These are expected validation or business-rule failures.

They should be represented explicitly using `Result` or `Result<T>` rather than being treated as exceptional program failures.

---

## 2. Expected Validation Failure

The expected validation flow is:

```text
Invalid Domain Data
        ↓
Domain Validation
        ↓
Validation Errors
        ↓
Result / Result<T>
        ↓
Explicitly Handled
```

For example:

```text
Create Patient
      ↓
Validate Patient Data
      ↓
   Valid?
    /   \
  Yes    No
   ↓      ↓
Patient  Result.Failure(...)
```

A failed validation does not mean that the system itself has malfunctioned.

It means that the supplied domain data does not satisfy the required rules.

---

## 3. Validation Errors

Validation failures are represented using structured `ValidationError` objects.

A `ValidationError` describes one validation failure and provides enough information to identify, understand, and handle the problem.

The structure is:

```text
ValidationError
├── Code
├── Message
├── Field / Property
└── Details
```

### 3.1 Error Code

The `Code` uniquely identifies the validation rule that failed.

Error codes follow this format:

`<AGGREGATE>.<FIELD>.<PROBLEM>`

Examples:

```text
PATIENT.NATIONAL_ID.REQUIRED
PATIENT.NATIONAL_ID.FORMAT_INVALID
PATIENT.NATIONAL_ID.LENGTH_INVALID
PATIENT.NAME.REQUIRED
PATIENT.NAME.INVALID
PATIENT.DATE_OF_BIRTH.INVALID
PATIENT.DATE_OF_BIRTH.FUTURE
```

Rules:

* Codes use uppercase letters.
* Code segments are separated by periods (`.`).
* The first segment identifies the aggregate or domain concept.
* The second segment identifies the related field or property when applicable.
* The final segment identifies the specific validation problem.
* Codes are machine-readable and stable.
* Codes must not contain human-readable messages.

### 3.2 Error Message

The `Message` provides a human-readable explanation of the validation failure.

Example:

```text
Code:
PATIENT.NATIONAL_ID.LENGTH_INVALID

Message:
National ID must contain exactly 14 digits.
```

The message is separate from the error code so that the message can change without changing the identity of the validation error.

The message must:

* Clearly describe the validation failure.
* Be meaningful to the intended consumer.
* Not be used as the machine-readable identifier.
* Not contain the error code.

### 3.3 Error Field / Property

The `Field` identifies the domain property associated with the validation failure.

Example:

```text
Field:
NationalId
```

A field is optional because some validation or business-rule errors may apply to the entire object rather than to one specific property.

For example:

```text
Code:
PATIENT.INVALID

Field:
null
```

When provided, the field name should be normalized and should identify the relevant domain property.

### 3.4 Error Details Collection

`Details` provides additional information about the validation failure when required.

The collection:

* May contain zero or more details.
* Must not be `null`.
* Is exposed as `IReadOnlyList<string>`.
* Must not expose the original mutable collection.
* Allows additional information without changing the main error `Code` or `Message`.

A validation operation may return multiple `ValidationError` objects when multiple independent rules fail.

Multiple independent validation failures should normally be represented as separate `ValidationError` objects rather than combining unrelated failures into one error.

### 3.5 Validation Error Immutability

`ValidationError` is immutable after creation.

Its properties are read-only:

```csharp
public string Code { get; }
public string Message { get; }
public string? Field { get; }
public IReadOnlyList<string> Details { get; }
```

The constructor validates the required structure of the error.

`Code` and `Message` cannot be null, empty, or whitespace.

`Field` is optional.

The `Details` collection is defensively copied so that changes to the collection supplied to the constructor cannot modify the created `ValidationError`.

Therefore, once a `ValidationError` has been created, its state cannot be changed.

---

## 4. Result Pattern

The Domain provides two result forms.

### Result

Used when an operation does not need to return a value.

```text
Result
├── IsSuccess
├── IsFailure
└── Errors
```

### Result<T>

Used when a successful operation returns a domain value.

```text
Result<T>
├── IsSuccess
├── IsFailure
├── Value
└── Errors
```

The generic result can represent:

```text
Success
   ↓
Result<T>
   ↓
Value
```

or:

```text
Failure
   ↓
Result<T>
   ↓
Validation Errors
```

A failed `Result<T>` does not expose a successful domain value.

---

## 5. Domain Isolation

The validation model is part of the Domain layer.

The Domain validation components must not depend on:

* Database access
* Repositories
* File systems
* HTTP
* External APIs
* Application services
* Infrastructure services
* UI or presentation concerns

Validation should operate on domain data and domain rules only.

---

## 6. Duplicate National ID

Duplicate National ID handling is a **business-rule validation** that requires checking whether another patient already has the same National ID.

The actual lookup of existing patients is outside the current isolated `PatientValidator` model.

Therefore, the domain distinguishes between:

```text
Validate National ID format
        ↓
Local validation
        ↓
Result
```

and:

```text
Check whether National ID already exists
        ↓
Requires existing patient data
        ↓
Business-rule validation
        ↓
Result.Failure(...)
```

For example:

```text
PATIENT.NATIONAL_ID.DUPLICATE
```

may be returned when the registration flow determines that the National ID already belongs to another patient.

The current Day 6 isolated validator does not perform this lookup.

---

# 7. Expected vs Unexpected Errors

The validation system must distinguish between **expected domain failures** and **unexpected technical failures**.

## 7.1 Expected Validation Failures

Expected failures occur when supplied domain data does not satisfy a known validation or business rule.

Examples include:

* Missing National ID
* National ID with invalid format
* National ID with invalid length
* Missing patient name
* Invalid patient name
* Date of birth in the future
* Invalid Gender value
* Invalid Blood Type value
* Incomplete emergency contact information
* Invalid emergency contact relationship
* Duplicate National ID when checked during patient registration

These failures should be represented using:

```text
Expected Domain Failure
        ↓
ValidationError
        ↓
Result.Failure(...)
```

They are normal outcomes of processing input and should be handled explicitly by the calling code.

---

## 7.2 Unexpected Technical Failures

Unexpected technical failures are different from invalid domain input.

Examples include:

* Database connection failure
* File-system failure
* Corrupted persistence file
* Permission denied
* Network failure
* External service failure
* Plugin loading failure
* Unexpected programming error

These failures should **not** be converted into `ValidationError` objects simply because an exception occurred.

The general flow is:

```text
Technical Failure
        ↓
Exception
        ↓
Handled at the appropriate boundary
```

The Domain validation system must not silently swallow these exceptions.

---

## 7.3 Invalid Date Handling

An invalid patient date that violates a known domain rule is an expected validation failure.

For example:

```text
DateOfBirth > Today
        ↓
PATIENT.DATE_OF_BIRTH.FUTURE
        ↓
Result.Failure(...)
```

This is not an unexpected technical exception.

The `PatientValidator` is responsible for detecting this rule violation.

---

## 7.4 Missing Name Handling

A missing required patient name is an expected validation failure.

For example:

```text
FirstArabicName = ""
        ↓
PATIENT.ARABIC_FIRST_NAME.REQUIRED
        ↓
Result.Failure(...)
```

The validator should report the validation error rather than throw an exception for this expected input condition.

---

## 7.5 Invalid Command Input

When a future application/use-case layer receives a patient registration command, invalid patient data should be passed through the domain validation flow.

For example:

```text
RegisterPatientCommand
        ↓
PatientValidationInput
        ↓
PatientValidator
        ↓
Result.Failure(...)
```

Invalid command data should not be represented as an unexpected technical exception.

The Application layer may then translate the validation result into the appropriate response for the caller.

---

## 7.6 Corrupted File Handling

A corrupted persistence file is not a patient validation error.

For example:

```text
Read patients.json
        ↓
File is corrupted
        ↓
Technical/Persistence Failure
        ↓
Exception
```

It should not be converted into:

```text
Result.Failure(
    PATIENT.FILE.INVALID
)
```

because the problem is with the persistence mechanism, not with the validity of patient input.

File-related exception handling belongs to the appropriate Infrastructure or Application boundary.

---

## 7.7 Permission Error Handling

A file-system permission failure is an infrastructure failure.

For example:

```text
Write patients.json
        ↓
Access denied
        ↓
Exception
```

The validation system must not convert this into a patient validation error.

The appropriate infrastructure or application layer is responsible for handling, logging, or reporting the failure.

---

## 7.8 Plugin Loading Errors

A plugin loading failure is a technical failure rather than a domain validation failure.

For example:

```text
Load Statistics Plugin
        ↓
Plugin cannot be loaded
        ↓
Exception
```

The Domain validation system must not convert plugin loading failures into `ValidationError` objects.

Plugin loading belongs outside the Domain validation boundary.

---

## 7.9 Validation Must Not Hide Technical Exceptions

The `PatientValidator` must only convert **known domain validation failures** into `Result.Failure(...)`.

It must not use a broad exception handler such as:

```csharp
try
{
    // validation
}
catch (Exception)
{
    return Result.Failure(...);
}
```

This would incorrectly hide unexpected technical or programming failures.

The intended behavior is:

```text
Expected domain rule violation
        ↓
Result.Failure(...)


Unexpected technical/programming failure
        ↓
Exception
```

This distinction ensures that the Result Pattern communicates expected outcomes without masking real system failures.

---

## 8. Design Principle

The Day 6 Domain validation strategy follows this principle:

> **Expected domain validation and business-rule failures are represented explicitly using `Result` / `Result<T>` and structured `ValidationError` objects. Unexpected technical failures remain exceptions and must not be silently converted into validation failures.**

The Domain layer remains isolated from infrastructure and application concerns.

---

## 9. Scope of Day 6 Domain Work

The current implementation focuses on:

* `ValidationError`
* `Result`
* `Result<T>`
* Domain validation
* `PatientValidator`
* Patient validation rules
* Separation of expected and unexpected failures
* Validation tests

Application and Infrastructure error-handling mechanisms are outside the current implementation scope.
