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