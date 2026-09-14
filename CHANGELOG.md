# Changelog

## [0.1.0] � 2026-09-01

### Added

* Initial Nile Care Hospital Enterprise Platform project structure.
* Initial Software Requirements Specification (`docs/requirements/SRS.md`).
* Functional and non-functional requirement definitions.
* Business rules documentation.
* Actor analysis and access matrix.
* V1 and V2 system boundaries.
* C4 Level 1 system context documentation.
* GitHub repository labels and project issues.
* Initial CI build and test workflow.

### Documentation

* Added system overview.
* Added hospital case study.
* Added requirements documentation.
* Added project progress tracking.

## [0.2.0] � 2026-09-02

### Added

* Clean Architecture-inspired project structure.
* Hospital.Domain, Hospital.Application, Hospital.Infrastructure,
  Hospital.Presentation, and Hospital.Contracts.
* Entity<TId>.
* ValueObject.
* AggregateRoot<TId>.
* Architecture dependency rules.
* ADR-001 through ADR-004.

### Architecture

* Defined Presentation ? Application ? Domain dependency direction.
* Defined Infrastructure as the implementation layer for inward-facing abstractions.
* Enforced Domain isolation.
* Established plugin contracts through Hospital.Contracts.

### Verification

* Verified successful solution build with dotnet build.
* Verified Domain compile-time isolation.

## [0.3.0] � 2026-09-03

### Added

* Implemented `HospitalEmployee` as the Employee aggregate root.
* Added employee specializations:

  * `Doctor`
  * `Nurse`
  * `Receptionist`
  * `Pharmacist`
  * `LabTechnician`
  * `BillingClerk`
* Added employee value objects:

  * `PersonName`
  * `PhoneNumber`
  * `NationalId`
* Added employee permissions and role-specific behavior.
* Implemented inheritance and runtime polymorphism.
* Added employee validation rules.
* Added automated Employee Domain tests.
* Added Employee UML class diagram.
* Added Employee Domain documentation.
* Documented inheritance and DDD design decisions.

### Verification

* Verified successful solution build with `dotnet build`.
* Verified all Employee Domain tests pass with `dotnet test`.
* Verified Employee Domain isolation from Infrastructure, Application,
  Presentation, JSON, database, EF Core, HTTP, and file I/O.
## [0.4.0] — 2026-09-05

### Added

* Implemented `Department` as the Department aggregate root.

* Added Department identity using `Guid`.

* Added Department name validation.

* Added recursive parent-child Department hierarchy.

* Added support for root, child, grandchild, and deep nested Departments.

* Added `AddSubDepartment()` for building the Department hierarchy.

* Added `RemoveSubDepartment()` for removing child Departments.

* Added recursive Department lookup.

* Added `HasChildren()` for Department hierarchy inspection.

* Added employee assignment to Departments using employee IDs.

* Added duplicate employee assignment validation.

* Added employee reassignment between Departments.

* Added support for cross-branch employee reassignment.

* Added validation for invalid employee reassignment.

* Added circular hierarchy protection.

* Added self-reference protection.

* Added recursive Department tree generation.

* Added Depth-First Search (DFS) Department traversal.

* Added support for arbitrary-depth Department hierarchies.

### Testing

* Added automated Department Domain tests.

* Added tests for root, child, grandchild, and deep Department hierarchies.

* Added tests for Department name validation.

* Added tests for recursive Department lookup.

* Added tests for Department not found scenarios.

* Added tests for self-reference and circular hierarchy protection.

* Added tests for employee assignment.

* Added tests for duplicate employee assignment.

* Added tests for employee reassignment.

* Added tests for invalid reassignment.

* Added tests for cross-branch reassignment.

* Added tests for DFS traversal and Department tree rendering.

### Documentation

* Added Department Domain documentation.

* Added Department UML class diagram.

* Added Employee Reassignment Activity Diagram.

* Documented Department hierarchy and business rules.

* Documented employee assignment and reassignment rules.

### Verification

* Verified successful solution build with `dotnet build`.

* Verified all Department Domain tests pass with `dotnet test`.

* Verified recursive Department hierarchy operations.

* Verified DFS traversal behavior.

* Verified circular hierarchy protection.

* Verified duplicate employee assignment prevention.

* Verified employee reassignment behavior.

* Verified Department Domain isolation from Infrastructure, Application, Presentation, JSON, database, EF Core, HTTP, and file I/O.

## [0.5.0] — 2026-09-12

### Added

* Implemented `Patient` as the Patient aggregate root.
* Added Patient identity using `Guid`.
* Added Patient National ID.
* Added Arabic and English patient names.
* Added Date of Birth with validation against future dates.
* Added Gender and Blood Type.
* Added Patient status and lifecycle states.
* Added `ContactInfo` as an immutable value object.
* Added `EmailAddress` value object.
* Added `HomeAddress` value object.
* Added `EmergencyContact` value object.
* Reused shared `PersonName`, `NationalId`, and `PhoneNumber` value objects.
* Added Patient validation for required information.
* Added validation for Patient contact information.
* Added Patient lifecycle and status rules.
* Added immutable contact information replacement behavior.

### Testing

* Added automated Patient Domain tests.
* Added tests for valid Patient creation.
* Added tests for required Patient information.
* Added tests for Arabic and English names.
* Added tests for invalid National ID.
* Added tests for invalid Date of Birth.
* Added tests for invalid contact information.
* Added tests for Patient status.
* Added tests for `ContactInfo` structural equality.
* Added tests for equal `ContactInfo` hash codes.
* Added tests for different `ContactInfo` values.
* Added tests verifying `ContactInfo` immutability.
* Added tests verifying replacement of `ContactInfo` without mutating the original instance.

### Documentation

* Added Patient Domain documentation.
* Added Patient aggregate design documentation.
* Added Patient lifecycle documentation.
* Added Patient UML class diagram.
* Documented Patient and `ContactInfo` composition.
* Documented Patient invariants and required-field rules.
* Documented sensitive Patient data classification.
* Documented value object immutability and structural equality.
* Documented the design decision to model `ContactInfo` as a value object.

### Verification

* Verified successful solution build with `dotnet build`.
* Verified all Patient Domain tests pass with `dotnet test`.
* Verified Patient aggregate invariants.
* Verified `ContactInfo` structural equality.
* Verified `ContactInfo` immutability.
* Verified Patient Domain isolation from Infrastructure, Application,
  Presentation, JSON, database, EF Core, HTTP, and file I/O.
## [0.6.0] — 2026-09-14

### Added

* Implemented `ValidationError` for structured domain validation errors.
* Implemented non-generic `Result` for validation and business operation outcomes.
* Implemented generic `Result<T>` for operations returning a successful value.
* Added `PatientValidationInput` as an immutable validation input model.
* Implemented `PatientValidator` for Patient input validation.
* Added required-field validation for Patient registration.
* Added National ID length and format validation.
* Added Arabic and English name validation.
* Added Date of Birth future-date validation.
* Added Gender and Blood Type validation.
* Added Emergency Contact completeness validation.
* Added support for collecting multiple validation errors.
* Distinguished expected validation/business failures from unexpected technical exceptions.

### Testing

* Added automated `Result` tests.
* Added automated `Result<T>` tests.
* Added tests for successful results.
* Added tests for failure results.
* Added tests for multiple validation errors.
* Added tests for `ValidationError` details.
* Added tests for invalid Result states.
* Added automated `PatientValidator` tests.
* Added tests for required Patient information.
* Added tests for invalid National ID length.
* Added tests for invalid National ID format.
* Added tests for Arabic and English names.
* Added tests for whitespace-only names.
* Added tests for future Date of Birth.
* Added tests for valid Arabic and multi-byte Arabic characters.
* Added tests for multiple validation errors.
* Added tests verifying validation failures do not throw exceptions.

### Documentation

* Added Patient validation strategy documentation.
* Added Patient Registration use case documentation.
* Added Patient Registration Activity Diagram.
* Documented expected validation/business failures and unexpected technical failures.
* Documented Patient Registration validation and duplicate National ID behavior.
* Documented validation error codes and messages.

### Verification

* Verified successful solution build with `dotnet build`.
* Verified all Domain tests pass with `dotnet test`.
* Verified `Result` and `Result<T>` behavior.
* Verified Patient validation behavior.
* Verified multiple validation errors are collected.
* Verified expected validation failures are returned through `Result`.
* Verified unexpected technical failures remain distinguishable from validation failures.
* Verified Domain isolation from Infrastructure, Application, Presentation, JSON,
  database, EF Core, HTTP, and file I/O.
* Verified no LINQ was introduced in the validation implementation.
