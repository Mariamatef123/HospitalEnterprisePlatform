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

Validation failures are represented using structured validation errors.

A validation error should provide enough information to identify the problem.

The planned structure is:

```text
ValidationError
├── Code
├── Message
├── Field / Property
└── Details
```

### Error Code

Uniquely identifies the validation rule that failed.

Example:

```text
PATIENT.NATIONAL_ID.INVALID
```

### Error Message

Provides a human-readable explanation of the validation failure.

Example:

```text
National ID must contain 14 digits.
```

### Field / Property

Identifies the domain property related to the error.

Example:

```text
NationalId
```

### Details

Allows additional validation information when required.

A failed validation may contain multiple validation errors.

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
