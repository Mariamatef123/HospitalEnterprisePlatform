# Validation Strategy

## Purpose

This document defines the validation strategy for the **Hospital Domain**.

The domain uses the **Result Pattern** to represent expected validation and business-rule failures explicitly.

Unexpected technical failures are outside the current Domain implementation scope.

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

These are expected validation failures.

They should be represented explicitly using `Result` or `Result<T>` rather than being used as exceptional program failures.

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

It means that the domain data does not satisfy the required rules.

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
PATIENT.NATIONAL_ID.INVALID
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
PATIENT.NATIONAL_ID.INVALID

Message:
National ID must contain 14 digits.
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

Example:

```text
Code:
PATIENT.INVALID

Message:
Patient contains invalid information.

Field:
null

Details:
- National ID format is invalid.
- Date of birth cannot be in the future.
```

A validation operation may return multiple `ValidationError` objects when multiple independent rules fail.

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

The Domain will provide two result forms.

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

Duplicate National ID handling requires checking whether another patient already has the same National ID.

The actual lookup of existing patients is outside the current isolated validation model.

Therefore, the domain validation model should distinguish between:

```text
Validate National ID format
        ↓
Domain validation
```

and:

```text
Check whether National ID already exists
        ↓
Requires existing patient data
```

The current Day 6 Domain work will focus on the validation rules that can be evaluated from the domain data itself.

---

## 7. Unexpected Technical Failures

Unexpected technical failures are conceptually different from domain validation failures.

Examples include:

* Database failure
* File-system failure
* Network failure
* External service failure

These are outside the current Domain implementation scope.

The Domain layer should not convert technical failures into validation errors.

---

## 8. Design Principle

The Day 6 Domain validation strategy follows this principle:

> Expected domain validation failures are represented explicitly using `Result` / `Result<T>` and structured `ValidationError` objects.

The Domain layer remains isolated from infrastructure and application concerns.

---

## 9. Scope of Day 6 Domain Work

The current implementation will focus on:

* `ValidationError`
* `Result`
* `Result<T>`
* Domain validation
* `PatientValidator`
* Patient validation rules
* Validation tests

Application and Infrastructure error-handling mechanisms are outside the current scope.
