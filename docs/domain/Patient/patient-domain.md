# Patient Domain Model

## Patient Aggregate

`Patient` is the Aggregate Root responsible for the patient's identity, core personal information, contact information, and current status.

### Patient Fields

* `PatientId` — unique identifier for the patient.
* `NationalId` — unique national identification number.
* `ArabicName` — patient's Arabic name.
* `EnglishName` — patient's English name.
* `DateOfBirth` — patient's date of birth.
* `Gender` — patient's gender.
* `BloodType` — patient's blood type.
* `ContactInfo` — patient's contact information.
* `Status` — current patient status.

## ContactInfo Value Object

`ContactInfo` is modeled as an immutable Value Object owned by the `Patient` aggregate.

It contains:

* `PhoneNumber`
* `EmailAddress`
* `HomeAddress`
* `EmergencyContact`

`ContactInfo` uses structural equality, meaning two instances with the same values are considered equal.

Changing contact information creates a new `ContactInfo` instance rather than mutating the existing instance.

## Aggregate Boundary

The Patient aggregate contains only information and behavior that belong directly to the patient's identity and core state.

The following are separate domain concepts and are not contained inside the Patient aggregate:

* Appointments
* Medical Records
* Lab Orders
* Bills
* Admissions

These concepts can reference the patient using `PatientId`.

## Required Information

The following information is required when registering a Patient:

* PatientId
* NationalId
* ArabicName
* EnglishName
* DateOfBirth
* Gender
* BloodType
* ContactInfo
* Status

Invalid or incomplete data must not create an invalid Patient.

## Sensitive Information

Patient information that requires additional protection includes:

* NationalId
* DateOfBirth
* Contact information
* Medical information handled by related medical-domain components

Sensitive information should not be unnecessarily exposed or logged.

## Patient Status

The initial allowed patient statuses are:

* `Active`
* `Inactive`

A Patient remains a valid historical record after becoming inactive.

Changing the patient's status does not change or remove the patient's identity.

## Design Decisions

* `Patient` is the Aggregate Root.
* Patient identity is represented by `PatientId`.
* `ContactInfo` is a Value Object, not an Entity.
* `ContactInfo` is immutable.
* Patient registration must enforce required-field validation.
* Related concepts such as appointments and medical records are kept outside the Patient aggregate.
