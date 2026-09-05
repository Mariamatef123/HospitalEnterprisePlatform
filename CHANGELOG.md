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
