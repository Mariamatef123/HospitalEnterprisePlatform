# Nile Care Hospital — Software Requirements Specification (SRS)

**Project:** Nile Care Hospital Enterprise Platform
**Version:** 1.0
**Status:** Final — Version 1
**Application Type:** Console Application
**Persistence:** File-based
**Database:** Not used in V1
**Framework:** .NET / C#

---

# 1. Introduction

## 1.1 Purpose

The **Nile Care Hospital Enterprise Platform** is a console-based hospital management system designed to centralize the hospital's core operations.

Nile Care Hospital currently relies on paper records and separate spreadsheets. This creates problems with patient information, appointments, pharmacy inventory, laboratory results, billing, reporting, auditing, and concurrent operations.

The system will provide a centralized application for hospital employees to manage these operations while maintaining data integrity, reliability, extensibility, and traceability.

## 1.2 V1 Constraints

Version 1 is intentionally limited to:

* Console interface
* Local file-based persistence
* No relational database
* No EF Core
* No LINQ
* No ASP.NET Core
* No web UI
* No mobile application
* Simulated external services
* Local plugin loading

---

# 2. Case Study

## 2.1 Hospital Background

**Nile Care Hospital** is a 200-bed hospital currently using paper files and disconnected spreadsheets.

This causes problems such as:

* Patient information can be lost.
* Doctors can accidentally be scheduled at the same time.
* Nurses may update the same information incorrectly.
* Pharmacy stock can become inaccurate.
* Laboratory results are difficult to track.
* Billing takes too long.
* Management cannot easily see hospital statistics.
* There is no reliable history of who changed what.

The hospital wants one centralized Hospital Management System running on a computer.

---

# 3. Stakeholders and Actors

| Actor                  | Responsibilities                                                                                                     |
| ---------------------- | -------------------------------------------------------------------------------------------------------------------- |
| Receptionist           | Register patients, search patients, update patient information, schedule/cancel appointments, check bed availability |
| Doctor                 | View patients and medical history, record diagnoses, create prescriptions, manage appointments                       |
| Nurse                  | Admit/discharge patients, manage beds, update patient-related information                                            |
| Lab Technician         | Receive laboratory requests, process tests, record results, complete requests                                        |
| Pharmacist             | Manage medications, dispense medication, check stock, monitor expiration                                             |
| Billing Clerk          | Create invoices, add services, record payments, check insurance coverage                                             |
| Hospital Administrator | Manage users/departments, view reports/audits, monitor pharmacy stock, load plugins                                  |
| Insurance Provider     | External simulated insurance coverage service                                                                        |
| Medication Supplier    | External simulated medication supplier                                                                               |

---

# 4. System Boundary

## 4.1 In Scope — V1

The following functionality belongs inside the V1 system boundary:

* Patient Management
* Employee Management
* Department Management
* Appointment Management
* Medical Records
* Laboratory
* Pharmacy
* Admissions
* Beds
* Billing
* Users and Roles
* Authentication
* Audit
* Notifications
* Reporting
* Dashboard
* Backup
* Import/Export
* Persistence
* Plugin System
* Reflection and Metadata Inspection
* Concurrency and Threading
* Console Command System
* Simulated External Integration

## 4.2 Out of Scope — V1

The following are intentionally excluded:

* Web UI
* Mobile application
* SQL database
* EF Core
* ASP.NET Core
* Distributed deployment
* Production insurance integration
* Production medication supplier integration

---

# 5. End-to-End Hospital Workflow

The system shall support the following workflow:

```text
Patient arrives
      ↓
Reception registers patient
      ↓
Appointment scheduled
      ↓
Doctor sees patient
      ↓
Doctor records diagnosis
      ↓
Doctor creates prescription
      ↓
Pharmacy dispenses medicine
      ↓
Stock updated
      ↓
Doctor requests laboratory test
      ↓
Lab processes test
      ↓
Result recorded
      ↓
Invoice generated
      ↓
Insurance checked
      ↓
Payment recorded
      ↓
Hospital reports updated
      ↓
Audit trail records operations
      ↓
Backup performed
      ↓
Administrator can load plugins
```

---

# 6. Functional Requirements

## 6.1 Patient Management

### FR-001 — Register Patient

The system shall allow registering a new patient with:

* Full name
* English name
* Arabic name
* Date of birth
* Gender
* National ID
* Blood type
* Phone number
* Email
* Address
* Emergency contact

Arabic, English, and Unicode text shall be supported correctly.

### FR-002 — Unique National ID

The system shall reject patient registration when the National ID already exists.

### FR-003 — Search Patient

The system shall allow searching for patients by:

* Partial, case-insensitive name
* Exact National ID

Filtering shall be implemented using manual loops and shall not use LINQ.

### FR-004 — Update and Deactivate Patient

The system shall allow updating patient information and deactivating old patient records.

Patient records shall not be physically deleted.

### FR-005 — Contact Information Composition

`Patient.ContactInfo` shall be modeled as a separate composed type rather than flattened fields.

---

# 7. Staff and Department Management

### FR-010 — Employee Hierarchy

The system shall model `HospitalEmployee` as an abstract base class.

Concrete employee types shall include:

* Doctor
* Nurse
* Receptionist
* LabTechnician
* Pharmacist

Each subclass shall override at least one virtual member.

### FR-011 — Department Hierarchy

Departments shall support recursive hierarchical structures.

Example:

```text
Nile Care Hospital
├── Medical
│   ├── Cardiology
│   ├── Neurology
│   └── Pediatrics
├── Laboratory
│   ├── Blood Laboratory
│   └── Microbiology
├── Pharmacy
└── Administration
    ├── Billing
    └── Reception
```

The system shall provide a command to display the hierarchy recursively.

### FR-012 — Employee Assignment

The system shall allow assigning and reassigning doctors and nurses to departments.

---

# 8. Appointment Management

### FR-020 — Create Appointment

The system shall allow scheduling an appointment containing:

* Patient
* Doctor
* Date
* Time
* Reason

### FR-021 — Prevent Double Booking

The system shall reject an appointment when the doctor already has an appointment in the same time slot.

Appointments in the past shall also be rejected.

### FR-022 — Appointment Events

The system shall support appointment cancellation and completion.

Appointment operations shall raise appropriate events.

### FR-023 — Appointment Status

Appointment status shall be represented using an enum:

```text
Scheduled
Completed
Cancelled
NoShow
```

### FR-024 — Doctor Daily Schedule

The system shall provide a command to list a doctor's daily appointments sorted manually by time.

LINQ shall not be used.

### FR-025 — Appointment Slot Analysis

The system shall demonstrate named tuples and deconstruction for appointment availability analysis.

Example:

```csharp
(int AvailableSlots, int OccupiedSlots, double Utilization)
```

---

# 9. Medical Records

### FR-030 — Medical Information

Doctors shall be able to:

* Open patient medical records
* Add diagnoses
* Add prescriptions

A diagnosis shall contain:

* Code
* Description

A prescription shall contain:

* Medication
* Dosage
* Frequency
* Duration

### FR-031 — Params

The system shall accept a variable number of diagnosis codes using a `params` parameter.

### FR-032 — Medical Record Composition

The system shall model:

```text
Patient
   ↓
MedicalRecord
   ↓
Diagnoses
Prescriptions
```

This shall demonstrate composition.

### FR-033 — Enums and Flags

Diagnosis severity or prescription frequency shall use enums.

Medication special-handling requirements shall use a `[Flags]` enum.

Examples:

```text
Refrigerated
ControlledSubstance
FragileHandling
```

---

# 10. Laboratory

### FR-040 — Laboratory Requests

The system shall allow creating laboratory requests and recording results.

Request status shall support:

```text
Requested
InProgress
Completed
Cancelled
```

### FR-041 — Lab Events

Completing a laboratory request shall raise a `LabCompleted` event.

Multiple independent subscribers shall be supported, including:

* Audit
* Notification
* Statistics

### FR-042 — Laboratory Queue

Laboratory requests shall be processed using `Queue<T>`.

Emergency requests shall have a separate priority-processing path.

---

# 11. Pharmacy and Inventory

### FR-050 — Medication Inventory

The system shall track:

* Medication name
* Available quantity
* Unit price
* Expiration date
* Minimum stock level
* Special handling information

### FR-051 — Low Stock Event

Dispensing medication shall raise a medication event when stock falls below the configured minimum level.

The administrator shall receive a low-stock notification.

### FR-052 — Insufficient Stock

The system shall reject dispensing when the requested quantity exceeds available stock.

An `InsufficientStockException` shall be raised.

### FR-053 — Expiring Medications

The system shall provide a command to find medications expiring within a specified number of days.

Filtering and sorting shall be implemented manually without LINQ.

---

# 12. Beds and Admissions

### FR-060 — Patient Admission and Discharge

The system shall allow admitting and discharging patients.

The corresponding operations shall raise:

* `PatientAdmitted`
* `PatientDischarged`

### FR-061 — Bed and Vital Structures

Ward beds shall be represented using a two-dimensional array:

```csharp
Bed[,]
```

Historical patient vital readings shall use a jagged array:

```csharp
double[][]
```

### FR-062 — Occupied Bed Protection

The system shall reject admission to an occupied bed.

---

# 13. Billing and Invoicing

### FR-070 — Invoice Generation

The system shall generate invoices containing:

* Appointment fees
* Medication costs
* Laboratory fees
* Discounts where applicable

Totals shall be calculated using manual loops.

### FR-071 — Money Struct

All monetary values shall use a custom `Money` struct.

The struct shall support:

```text
+
-
*
==
!=
>
<
```

It shall also provide appropriate currency formatting through `ToString()`.

### FR-072 — Insurance Integration

The system shall attempt to check insurance coverage through a simulated external HTTP service.

If the service is unavailable, billing shall continue through a documented default/manual override.

Successful payment shall raise `PaymentCompleted`.

### FR-073 — Fluent Invoice API

Invoice construction shall support fluent method chaining.

Example:

```csharp
invoice
    .AddLine(...)
    .AddLine(...)
    .ApplyDiscount(...);
```

Extension methods shall be used.

---

# 14. Users, Roles and Auditing

### FR-080 — Login

The system shall provide console login using:

* Username
* Hashed password
* Role

Roles shall include:

* Admin
* Doctor
* Nurse
* Receptionist
* Pharmacist
* LabTechnician

### FR-081 — Auditable Attributes

Entities marked with `[Auditable]` shall automatically generate audit entries.

Reflection shall be used to discover and enforce the attribute.

### FR-082 — Sensitive Data

Fields marked `[SensitiveData]` shall be masked when displayed.

### FR-083 — Audit History

The system shall provide an audit command showing:

* Action
* User
* Timestamp
* Entity

The administrator shall be able to search audit history.

---

# 15. Reporting

### FR-090 — Concurrent Reports

The system shall generate the following reports concurrently:

* Patient
* Doctor
* Pharmacy
* Laboratory

`Task.WhenAll` shall be used.

Concurrency shall be demonstrated through timestamps.

### FR-091 — Lazy Reports

Large reports shall use `yield return` rather than materializing the entire result set.

### FR-092 — StringBuilder Reports

Report output shall use `StringBuilder`.

The system shall demonstrate:

* `AppendFormat`
* `AppendJoin`
* `GetChunks()`

### FR-093 — Report Cancellation and Progress

Long-running reports shall support:

* `CancellationToken`
* `IProgress<T>`

---

# 16. Reflection, Attributes and Assemblies

### FR-100 — Metadata Inspector

The system shall provide a metadata inspection command displaying:

* Namespace
* Base type
* Interfaces
* Constructors
* Methods
* Properties
* Fields
* Attributes
* Parameters

### FR-101 — Assembly Inspection

The system shall inspect loaded assemblies and display:

* Full name
* Version
* Public key token
* Location

It shall demonstrate:

```csharp
Assembly.GetExecutingAssembly()
Assembly.GetCallingAssembly()
Assembly.GetEntryAssembly()
```

### FR-102 — Custom Attributes

The following attributes shall be implemented and actually consumed:

```text
[Auditable]
[Required]
[DisplayName]
[Permission]
[SensitiveData]
[Exportable]
```

Each shall define appropriate `AttributeUsage` constraints.

### FR-103 — Embedded Resources

The system shall read at least one embedded resource using:

```csharp
Assembly.GetManifestResourceStream()
```

---

# 17. Plugin System

### FR-110 — Plugin Contract

`Hospital.Contracts` shall define:

```csharp
IPlugin
```

The interface shall provide:

* Name
* Version
* Execute(PluginContext)

### FR-111 — Dynamic Plugin Loading

The host shall:

1. Scan the `plugins/` directory.
2. Load DLL files using `Assembly.LoadFrom`.
3. Discover `IPlugin` implementations through reflection.
4. Instantiate plugins.
5. Register plugins.

The host shall not reference plugin implementations at compile time.

### FR-112 — Plugin Commands

The system shall provide:

```text
plugin list
plugin run <name>
```

### FR-113 — Plugin Failure Isolation

A plugin that fails during loading or execution shall:

* Be caught.
* Be wrapped in `PluginLoadException`.
* Be logged.

The host application shall continue running.

### FR-114 — Independent Plugin

At least one plugin shall be an independently compiled project.

Example:

```text
Hospital.Plugins.Statistics
```

---

# 18. Concurrency and Threading

### FR-120 — Concurrent Workers

The system shall simulate concurrent:

* Pharmacy workers
* Laboratory workers
* Emergency workers

The implementation shall demonstrate appropriate use of:

* `Thread`
* `ThreadPool`
* `Task`

### FR-121 — Race Condition

The system shall deliberately demonstrate a race condition on shared medication stock.

It shall then fix the problem using:

* `lock`
* `Monitor`

The incorrect and corrected results shall be demonstrable.

### FR-122 — Deadlock

The system shall deliberately demonstrate a controlled deadlock caused by inconsistent lock ordering.

The deadlock shall then be resolved using:

* Consistent lock ordering, or
* `Monitor.TryEnter` with timeout

### FR-123 — Cancellable Operations

Long-running operations shall support cancellation using `CancellationToken`.

---

# 19. External Integration

### FR-130 — HTTP Integration

The system shall call at least one simulated external HTTP service using `HttpClient`.

The implementation shall use `async/await`.

Blocking with `.Result` or `.Wait()` shall not be used outside the top-level entry point.

### FR-131 — Graceful External Failure

An unavailable external service shall not cause the calling hospital operation to fail completely.

### FR-132 — Task.WhenAny

The system shall demonstrate racing multiple simulated external calls using:

```csharp
Task.WhenAny
```

The first successful response shall be used.

---

# 20. Persistence

### FR-140 — File Formats

The system shall use:

| Data            | Format |
| --------------- | ------ |
| Patients        | JSON   |
| Doctors         | JSON   |
| Appointments    | JSON   |
| Reports         | XML    |
| Import/Export   | XML    |
| Internal backup | Binary |

### FR-141 — Unicode Persistence

Arabic, English, and Unicode text shall survive persistence round trips without corruption.

UTF-8 encoding shall be explicitly used.

### FR-142 — Generic Repository

A generic:

```csharp
Repository<T>
```

shall provide entity persistence and manual query operations.

LINQ shall not be used.

### FR-143 — Pagination

A generic:

```csharp
PagedResult<T>
```

shall implement manual pagination.

---

# 21. Console Command Interface

### FR-150 — Command Interpreter

The system shall provide an administrative command interpreter.

Examples:

```text
patient add
patient search --name Mariam
appointment list --doctor D001
plugin list
plugin run Statistics
```

Command dispatch shall demonstrate:

* Switch expressions
* `when` guards
* Type patterns

### FR-151 — Safe Input Parsing

Numeric and date input shall use `TryParse`.

Bare `Parse` shall not be used without proper exception handling.

### FR-152 — Anonymous Types

At least one narrow console-display operation shall demonstrate an anonymous type.

Example:

```csharp
new { Name, Age }
```

---

# 22. C# Language Feature Coverage

### FR-160 — Indexer

A real domain type shall expose an indexer.

Example:

```csharp
WardLayout[row, column]
```

Bounds shall be validated.

### FR-161 — Nested Type

At least one appropriate nested type shall be used.

Example:

A custom collection may contain a private nested enumerator.

### FR-162 — Named and Optional Arguments

The system shall demonstrate:

* Named arguments
* Optional parameters

The design reason shall be documented.

### FR-163 — Local Functions

At least three local functions shall be used where appropriate.

### FR-164 — ref, out and in

The system shall demonstrate and document appropriate uses of:

```text
ref
out
in
```

### FR-165 — Boxing and Unboxing

A legacy boxing/unboxing example shall exist in `Hospital.CSharpFeatures`.

The example shall explain why generics replaced this approach.

---

# 23. String and Encoding

### FR-170 — ASCII and Unicode

The system shall provide an ASCII/Unicode exploration utility displaying:

* Decimal
* Hexadecimal
* Binary
* Character

### FR-171 — StringBuilder Capacity

The system shall demonstrate `StringBuilder` capacity growth and `GetChunks()`.

### FR-172 — String Intern Pool

The system shall demonstrate:

* String literals
* `new string(...)`
* `String.Intern`
* Reference equality
* Value equality

### FR-173 — Composite Formatting

At least one tabular console report shall use:

```csharp
String.Format
```

with alignment format specifiers.

---

# 24. Performance Diagnostics

### FR-180 — Collection Benchmark

The system shall benchmark:

* `List<T>` linear search
* `Dictionary<TKey,TValue>` lookup
* `HashSet<T>.Contains`

The benchmark shall use multiple data sizes.

### FR-181 — String Concatenation Benchmark

The system shall benchmark:

* String concatenation
* StringBuilder

### FR-182 — Garbage Collection

The system shall demonstrate basic GC awareness using:

```csharp
GC.GetTotalMemory()
```

---

# 25. Records and Modern C# Features

### FR-183 — Records

The system shall use reference records for immutable read-only projections.

Examples:

```text
PatientSummary
AppointmentSummary
LabResultSummary
```

Positional records and `with` expressions shall be demonstrated where appropriate.

### FR-184 — Record Structs

The system shall use at least one record struct for a small immutable value.

Example:

```text
TimeSlot
Coordinate
```

### FR-185 — Custom IEnumerable/IEnumerator

The system shall implement at least one custom generic collection using:

```text
IEnumerable<T>
IEnumerator<T>
```

The collection shall contain a nested enumerator type and support `foreach`.

### FR-186 — NuGet Packaging

One reusable component shall be extracted into an independently packable NuGet package.

Another project shall consume the package.

---

# 26. Non-Functional Requirements

## 26.1 Reliability

### NFR-001

The application shall not terminate because of an unhandled exception during normal operation.

### NFR-002

A failing plugin shall not crash the host application.

### NFR-003

External service failures shall have documented fallback behavior.

---

## 26.2 Correctness and Data Integrity

### NFR-010

Concurrent operations shall not corrupt shared state.

### NFR-011

The application shall prevent unresolved deadlocks in concurrent operations.

### NFR-012

Persisted multilingual text shall round-trip correctly.

### NFR-013

National ID uniqueness and appointment collision rules shall always be enforced.

---

## 26.3 Performance

### NFR-020

Patient search, list, and single-record lookup shall complete in under 200 ms against a seeded dataset containing at least:

* 500 patients
* 100 doctors
* 2,000 appointments

### NFR-021

Concurrent report generation shall be measurably faster than sequential generation.

### NFR-022

Performance benchmarks shall use at least three data-size tiers.

Example:

```text
100
10,000
100,000
```

---

## 26.4 Console Usability

### NFR-030

Every console command shall provide a help/usage message.

### NFR-031

Invalid input shall produce a clear error message without exposing a raw stack trace.

### NFR-032

Long-running operations shall visibly report progress.

---

## 26.5 Maintainability and Extensibility

### NFR-040

New plugins shall be addable without recompiling the host application.

### NFR-041

Public classes and methods in `Hospital.Domain` and `Hospital.Application` shall have XML documentation.

### NFR-042

The solution shall compile with nullable reference types enabled and zero nullable warnings.

### NFR-043

Important collection and format choices shall be documented with inline design comments.

---

## 26.6 Security

### NFR-050

Passwords shall never be stored or logged in plaintext.

### NFR-051

Sensitive fields shall never appear unmasked in console output or logs.

### NFR-052

Role-based access shall protect administrative and destructive operations.

---

## 26.7 Testability

### NFR-060

At least 15 unit tests shall exist.

Tests shall cover at minimum:

* `Result<T>`
* Equality and `GetHashCode`
* Recursive algorithms
* Race-condition fix
* Generic repository

### NFR-061

Business logic shall be testable without directly accessing:

* File system
* Console

Dependencies shall be interface-based.

---

## 26.8 Portability

### NFR-070

The application shall run on operating systems supported by the selected .NET LTS version.

OS-specific path assumptions shall not be used.

`Path.Combine` shall be preferred over hardcoded separators.

---

## 26.9 Observability

### NFR-080

Domain events shall be logged with:

* Timestamp
* Entity affected
* Event type

### NFR-081

Audit history shall be queryable by:

* Entity type
* Date range

Filtering shall be implemented manually.

---

# 27. Business Rules

The following rules are derived from the requirements.

| ID     | Business Rule                                                               |
| ------ | --------------------------------------------------------------------------- |
| BR-001 | Patient National ID must be unique.                                         |
| BR-002 | A doctor cannot have two appointments in the same time slot.                |
| BR-003 | New appointments cannot be created in the past.                             |
| BR-004 | Medication stock cannot become negative.                                    |
| BR-005 | An occupied bed cannot be assigned to another patient.                      |
| BR-006 | Historical patient records must not be physically deleted.                  |
| BR-007 | Sensitive patient information must be masked when displayed where required. |
| BR-008 | Only authorized roles may perform administrative operations.                |
| BR-009 | External service failure must not stop billing.                             |
| BR-010 | Plugin failure must not crash the host application.                         |
| BR-011 | Successful payment must generate a payment event.                           |
| BR-012 | Completed laboratory requests must generate the required events.            |
| BR-013 | Concurrent updates must preserve correct shared state.                      |
| BR-014 | Passwords must never be stored or logged in plaintext.                      |
| BR-015 | Arabic and English data must be preserved correctly during persistence.     |

---

# 28. Data Persistence Requirements

The system shall use file-based persistence in V1.

```text
Operational Data
       ↓
     JSON

Reports / Import / Export
       ↓
      XML

Backup
       ↓
    Binary
```

The application shall reload persisted data when restarted.

---

# 29. Backup

The administrator shall be able to:

* Start a backup.
* Monitor backup progress.
* Cancel a backup.
* Determine whether the backup completed successfully.

Backup operations shall support cancellation.

---

# 30. Notifications

Important events shall generate notifications.

Examples:

```text
Low medication stock
Laboratory result completed
Appointment cancelled
Patient admitted
```

Notifications shall be recorded so important events are not silently lost.

---

# 31. Plugin Requirements

The plugin architecture shall allow the hospital to add functionality without modifying or recompiling the host application.

Example:

```text
plugins/
└── Hospital.Statistics.dll
```

The host shall discover and load the plugin dynamically.

A plugin failure shall be isolated from the main application.

---

# 32. Concurrency Requirements

The system shall support simulated simultaneous hospital operations.

Examples:

```text
Pharmacy Worker
Laboratory Worker
Emergency Worker
```

Shared state shall be protected against:

* Race conditions
* Data corruption
* Deadlocks

Concurrency behavior shall be demonstrated and tested rather than merely documented.

---

# 33. Traceability Principle

Every functional and non-functional requirement should trace back to an actual hospital scenario or operational need.

The project shall avoid adding C# syntax demonstrations that have no meaningful connection to the hospital domain.

C# features should be introduced because they solve a real problem in the system.

For example:

> Preventing two doctors from being booked simultaneously

is a **business requirement**.

The corresponding scheduling service is part of the **software design**.

Using:

```text
List<T>
Dictionary<TKey,TValue>
lock
Task
events
```

are **implementation techniques** used to satisfy the requirement.

---

# 34. V1 Architecture Boundary

The V1 system consists of the following major areas:

```text
Nile Care Hospital Enterprise Platform
│
├── Patient Management
├── Employee Management
├── Department Management
├── Appointment Management
├── Medical Records
├── Laboratory
├── Pharmacy
├── Admissions
├── Billing
├── Authentication
├── Authorization
├── Audit
├── Notifications
├── Reporting
├── Backup
├── Persistence
├── Reflection
├── Plugins
├── Concurrency
└── Console Interface
```

External systems:

```text
Nile Care Hospital Enterprise Platform
        │
        ├──────── Insurance Provider
        │
        └──────── Medication Supplier
```

These external services are simulated for V1.

---

# 35. Future V2 Direction

Future versions may introduce:

* ASP.NET Core Web API
* Web UI
* Mobile application
* SQL Server/PostgreSQL
* Entity Framework Core
* Authentication server
* Distributed deployment
* Real insurance integration
* Real medication supplier integration
* Cloud hosting

These features are not part of V1.

---

# 36. Definition of Done for the SRS

The SRS is considered complete when:

* All functional requirements have unique IDs.
* All non-functional requirements have unique IDs.
* Business rules have been identified.
* Actors are documented.
* System boundaries are documented.
* V1 and V2 scopes are separated.
* External systems are identified.
* Requirements can be traced to real hospital scenarios.
* The SRS is committed to GitHub.

---

# End of SRS

**Nile Care Hospital — Hospital Enterprise Platform**
**Software Requirements Specification — Version 1.0**
