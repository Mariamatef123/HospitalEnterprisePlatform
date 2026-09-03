# Changelog

## [0.1.0] — 2026-09-01

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

## [0.2.0] — 2026-09-02

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

## [0.3.0] — 2026-09-03

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
