# Hospital Enterprise Platform — System Overview

## 1. Purpose

The **Hospital Enterprise Platform** is a console-based hospital management system designed to centralize and manage the main operational activities of a general hospital.

The system provides a unified platform for managing patients, employees, departments, appointments, medical records, laboratory requests and results, pharmacy inventory, admissions, billing, auditing, reporting, backups, and extensibility through plugins.

The platform is designed for **Life Bridge Hospital**, a 200-bed hospital that currently relies on paper records and separate spreadsheets. The system aims to improve data consistency, reduce scheduling conflicts, protect historical information, and provide reliable access to hospital information.

The V1 system is intentionally limited to a **console interface and file-based persistence**. Web applications, mobile applications, SQL databases, and production distributed infrastructure are outside the scope of V1.

---

# 2. Problem Statement

Nile Care Hospital currently depends on paper files and separate spreadsheets to manage hospital operations.

This creates several problems:

* Patient information can be lost or duplicated.
* Patient records are difficult to search and maintain.
* Doctors may accidentally be scheduled for multiple patients at the same time.
* Nurses and other staff may update the same information inconsistently.
* Medical history may become difficult to track.
* Laboratory requests and results may be difficult to coordinate.
* Pharmacy stock information may become inaccurate.
* Medication may be dispensed beyond available stock if inventory is not properly controlled.
* Bed occupancy information may become inconsistent.
* Billing information may require manual calculations.
* Insurance verification may interrupt the billing process when an external service is unavailable.
* Historical medical information must be preserved reliably.
* Audit information is difficult to maintain using disconnected files.
* Generating management reports manually is time-consuming.
* Backups and recovery are difficult to manage consistently.

The Hospital Enterprise Platform addresses these problems by providing a centralized system for managing hospital operations within the defined V1 scope.

---

# 3. System Scope

## 3.1 V1 Scope

The first version of the system includes:

* Console interface
* Patient management
* Employee management
* Department management
* Appointment management
* Medical records
* Laboratory management
* Pharmacy and inventory management
* Admissions and bed management
* Billing and payment management
* File-based persistence
* Authentication and authorization
* Notifications
* Audit management
* Reporting
* Backup and recovery
* Import and export
* Plugin management
* Command management
* Reflection and metadata inspection
* Simulated external services
* Concurrency and background operations

## 3.2 V2 / Out of Scope

The following are explicitly outside the V1 scope:

* Web UI
* Mobile application
* SQL database
* Entity Framework Core
* ASP.NET Core
* Production insurance integration
* Production medication supplier integration
* Distributed deployment
* Production cloud infrastructure

The V1 system uses files for persistence rather than a relational database.

---

# 4. System Boundary

## 4.1 Inside the V1 System

The following components are inside the Hospital Enterprise Platform boundary:

* Hospital console
* Patient management
* Employee management
* Department management
* Appointment management
* Medical records
* Laboratory
* Pharmacy
* Admissions
* Billing
* Authentication
* Authorization
* File persistence
* Reporting
* Audit
* Backup
* Import/export
* Plugins
* Reflection and metadata inspection
* Command interpreter
* Notifications
* Concurrency processing

## 4.2 Outside the V1 System

The following are outside the V1 system boundary:

* Web UI
* Mobile application
* SQL database
* Entity Framework Core
* ASP.NET Core
* Distributed deployment
* Production insurance systems
* Production medication supplier systems

External services are represented through controlled contracts and simulated integrations in V1.

---

# 5. Actors

## 5.1 Human Actors

### Doctor

Responsibilities:

* View assigned appointments.
* View patient medical history.
* Add diagnoses.
* Create prescriptions.
* Request laboratory tests.
* Review laboratory results.

### Receptionist

Responsibilities:

* Register patients.
* Search for patients.
* Update patient information.
* Manage patient-related appointments.
* View appropriate patient and appointment information.

### Billing Clerk

Responsibilities:

* Generate patient invoices.
* Add hospital service charges.
* Calculate invoice totals.
* Check insurance coverage.
* Record payments.
* View appropriate billing information.

### Lab Technician

Responsibilities:

* Receive laboratory requests.
* Process laboratory requests.
* Record laboratory results.
* Complete or cancel laboratory requests.

### Nurse

Responsibilities:

* View appropriate patient information.
* Assist with patient admissions.
* Record patient-related information and vitals.
* Work with assigned departments and patients.

### Pharmacist

Responsibilities:

* View prescriptions.
* Check medication availability.
* Dispense medications.
* Update medication stock.
* Monitor medication expiration and low-stock conditions.

### Hospital Administrator

Responsibilities:

* Manage users and roles.
* Manage employees and departments.
* View audit information.
* Manage system configuration.
* Manage plugins.
* Run administrative commands.
* Manage backups.
* Access administrative reports.

---

# 6. External Actors

## 6.1 Insurance Provider

The Insurance Provider represents an external system responsible for checking insurance coverage.

In V1, insurance communication is simulated.

The Hospital Enterprise Platform must not fail completely when the external insurance service is unavailable. A documented fallback or manual-override path must allow billing to continue.

## 6.2 Medication Supplier

The Medication Supplier represents an external system that may provide medication-related information or services.

In V1, the supplier interaction is simulated.

External service failures must not cause the main hospital system to terminate.

---

# 7. Entities

The main domain entities include:

## 7.1 Employee Hierarchy

```text
HospitalEmployee (abstract)
├── Doctor
├── Nurse
├── Receptionist
├── LabTechnician
└── Pharmacist
```

Each concrete employee type represents a specialized hospital role.

## 7.2 Patient and Medical Information

```text
Patient
└── ContactInfo

Patient
└── MedicalRecord
    ├── Diagnosis
    └── Prescription
```

`ContactInfo` is composed into `Patient`.

`MedicalRecord` represents the patient's medical history and contains diagnoses and prescriptions.

## 7.3 Hospital Entities

* HospitalEmployee
* Patient
* ContactInfo
* Department
* Ward
* Bed
* Appointment
* MedicalRecord
* Diagnosis
* Prescription
* LabTest
* LabRequest
* LabResult
* Medication
* Admission
* HospitalService
* Invoice
* InvoiceItem
* Payment
* Insurance
* User
* Role
* Notification
* AuditLogEntry
* Report
* Plugin
* Backup

---

# 8. System Modules

## 8.1 Hospital Management

Provides the overall coordination of hospital operations.

## 8.2 Authentication and Authorization

Provides:

* Console login
* Username and password authentication
* Password hashing
* Role-based access control

Supported roles include:

* Admin
* Doctor
* Nurse
* Receptionist
* Pharmacist
* LabTechnician
* BillingClerk

## 8.3 User and Staff Management

Manages hospital employees, users, roles, and employee assignments.

## 8.4 Department Management

Manages hospital departments using a hierarchical structure.

Example:

```text
Hospital
└── Medical
    └── Cardiology
```

Doctors and nurses can be assigned and reassigned to departments.

## 8.5 Patient Management

Provides:

* Patient registration
* Patient search
* Patient update
* Patient deactivation
* Contact information management

Patient records are not physically deleted.

## 8.6 Appointment Management

Provides:

* Appointment scheduling
* Appointment cancellation
* Appointment completion
* Appointment status management
* Doctor daily schedules
* Appointment availability analysis

The system prevents doctor double-booking.

## 8.7 Medical Records Management

Provides:

* Medical history
* Diagnoses
* Prescriptions
* Medication information
* Prescription frequency
* Diagnosis information

Historical medical records must be preserved.

## 8.8 Laboratory Management

Provides:

* Lab request creation
* Test type management
* Lab request queues
* Emergency request processing
* Laboratory result recording
* Laboratory completion notifications

## 8.9 Pharmacy and Inventory Management

Provides:

* Medication inventory
* Stock quantity
* Unit price
* Expiration date
* Prescription-based dispensing
* Low-stock detection
* Expiration monitoring

Medication stock must never become negative.

## 8.10 Bed and Admission Management

Provides:

* Patient admission
* Bed assignment
* Patient discharge
* Ward bed layout
* Patient vitals

A bed cannot be assigned to more than one patient at the same time.

## 8.11 Billing and Payment Management

Provides:

* Invoice generation
* Invoice items
* Appointment fees
* Medication costs
* Laboratory fees
* Insurance coverage checking
* Payment recording

Billing must continue through a documented fallback path when the external insurance service is unavailable.

## 8.12 Insurance Integration

Provides controlled communication with the simulated Insurance Provider.

The integration is external to the core hospital system and must not prevent billing from continuing when unavailable.

## 8.13 Notification Management

Provides notifications for important system events, including:

* Appointment events
* Laboratory completion
* Medication low-stock events
* Admission/discharge events
* Payment events

## 8.14 Audit Management

Records important system actions.

Audit information includes:

* Who performed the action
* What entity was affected
* When the action occurred

Sensitive information must be masked when displayed.

## 8.15 Reporting

Provides reports for:

* Patients
* Doctors
* Pharmacy
* Laboratory
* Management

Large reports may be generated lazily and multiple reports can be generated concurrently.

## 8.16 Backup Management

Provides backup and recovery functionality for hospital data.

V1 uses file-based backup storage.

## 8.17 Import and Export

Provides controlled import and export of hospital information using the required file formats.

## 8.18 Plugin Management

Provides dynamic extension of the hospital system.

Plugins can be discovered and loaded from the `plugins/` directory.

A plugin failure must not terminate the main application.

## 8.19 Command Management

Provides the administrative console command interpreter.

Examples include:

```text
patient add
patient search
patient list
appointment create
appointment list
appointment cancel
lab request
pharmacy dispense
report generate
audit list
plugin list
plugin run
backup start
```

## 8.20 Reflection and Metadata Inspection

Provides system metadata inspection, including:

* Types
* Base classes
* Interfaces
* Constructors
* Methods
* Properties
* Fields
* Attributes
* Parameters
* Assemblies

---

# 9. Main System Workflow

The main hospital workflow can be summarized as follows:

```text
Patient arrives
        ↓
Receptionist registers patient
        ↓
Patient information is saved
        ↓
Appointment scheduled with doctor
        ↓
Patient admitted if hospitalization is required
        ↓
Ward / Bed assigned
        ↓
Doctor receives appointment information
        ↓
Doctor sees patient
        ↓
Doctor views medical history
        ↓
Doctor records diagnosis
        ↓
Doctor creates prescription
        ↓
Doctor requests laboratory test
        ↓
Lab Technician receives laboratory request
        ↓
Lab Technician processes test
        ↓
Lab result recorded
        ↓
Doctor / relevant services notified
        ↓
Pharmacist receives prescription
        ↓
Pharmacist checks medication stock
        ↓
       ┌─────────────────────┐
       │ Enough medication? │
       └──────────┬──────────┘
                  │
          ┌───────┴───────┐
         YES              NO
          ↓                ↓
   Dispense medication   Reject operation
          ↓
   Update medication stock
          ↓
   Check minimum stock
          ↓
   ┌──────────────────────┐
   │ Stock below minimum? │
   └───────────┬──────────┘
               │
        ┌──────┴──────┐
       YES            NO
        ↓              ↓
 Notify Administrator Continue
        ↓
 Billing creates invoice
        ↓
 Add hospital services
        ↓
 Calculate total
        ↓
 Check insurance coverage
        ↓
 ┌────────────────────────┐
 │ Insurance available?   │
 └────────────┬───────────┘
              │
       ┌──────┴──────┐
      YES            NO
       ↓              ↓
 Coverage information  Manual/default
       │               coverage decision
       └──────┬────────┘
              ↓
       Payment recorded
              ↓
        Audit records
              ↓
       Reports generated
              ↓
      Management review
              ↓
       Import / Export
              ↓
      Automatic Backup
              ↓
 Administrator manages
 plugins and tools
```

---

# 10. Business Rules and Constraints

The system shall enforce the following business rules.

## BR-001 — Unique National ID

A patient's National ID must be unique among all existing patient records.

## BR-002 — Doctor Appointment Collision

A doctor cannot have two appointments in the same time slot.

## BR-003 — No Historical Appointments

Appointments cannot be created for a date/time in the past.

## BR-004 — Pharmacy Stock Non-Negativity

Medication cannot be dispensed when the requested quantity exceeds available stock, and medication stock must never become negative.

## BR-005 — Single Bed Occupancy

A patient cannot be admitted to a bed that is already occupied.

## Additional Business Constraints

* Patient records must never be permanently deleted.
* Inactive patients shall be deactivated using soft removal.
* Insurance service failure must not prevent billing from continuing.
* Historical medical records must be preserved.
* Audit history must preserve who performed an action and when it occurred.
* Sensitive patient information must be masked where required.
* Hospital data must remain available after the application is closed and restarted.
* A plugin failure must not terminate the main hospital system.
* Concurrent operations must not corrupt shared data.

---

# 11. Data Persistence

V1 uses file-based persistence.

Different formats are used for different purposes:

| Format     | Purpose                                           |
| ---------- | ------------------------------------------------- |
| **JSON**   | Patient, doctor, and appointment operational data |
| **XML**    | Hospital reports and import/export data           |
| **Binary** | Internal backup/archive data                      |

All persisted multilingual data must preserve Arabic, English, and other Unicode characters correctly.

The system must explicitly use UTF-8 encoding where applicable.

---

# 12. Concurrency and Reliability

The system must safely support concurrent operations.

Examples include:

* Pharmacy workers
* Laboratory workers
* Emergency workers
* Concurrent report generation
* Background backup operations
* External service calls

Shared data must not be corrupted by concurrent access.

The system also demonstrates controlled handling of:

* Race conditions
* Deadlocks
* Cancellation
* Background tasks
* Thread-based operations

A top-level error-handling boundary must prevent unhandled exceptions from terminating the application unexpectedly during normal operation.

---

# 13. Security

The system provides console-appropriate security controls.

These include:

* Username/password authentication
* Password hashing
* Role-based authorization
* Sensitive-data masking
* Audit logging
* Administrative command protection

Passwords must never be stored or logged in plaintext.

Administrative and destructive operations must only be accessible to authorized roles.

---

# 14. Observability and Auditing

Important system events and actions shall be observable through logging and auditing.

Audit information shall include:

* Entity type
* Entity affected
* User who performed the action
* Timestamp
* Action performed

The audit trail shall support querying by:

* Entity type
* Date range

Domain events shall also be logged even when no other subscriber reacts to the event.

---

# 15. Reporting and Performance

The system provides reporting capabilities for major hospital areas.

Reports include:

* Patient reports
* Doctor reports
* Pharmacy reports
* Laboratory reports

The system supports concurrent report generation and progress reporting for long-running operations.

Performance diagnostics include:

* Collection lookup comparisons
* `List<T>` linear search
* `Dictionary<TKey,TValue>` lookup
* `HashSet<T>.Contains`
* String concatenation vs. `StringBuilder`
* Garbage collection memory observations
* `Stopwatch`-based performance measurements

---

# 16. Requirements Traceability

The system requirements are tracked using unique identifiers.

Functional requirements use the following format:

```text
FR-001
FR-002
FR-003
...
FR-186
```

Non-functional requirements use:

```text
NFR-001
NFR-002
NFR-003
...
NFR-081
```

Business rules use:

```text
BR-001
BR-002
BR-003
BR-004
BR-005
```

The Requirements Traceability Matrix (RTM) maps each requirement to its:

* Requirement type
* Category
* Business rule
* Actor
* Verification method

This provides traceability from the original SRS through implementation and testing.

---

# 17. High-Level System Summary

The Hospital Enterprise Platform is a **V1 console-based hospital management system with file-based persistence**.

It connects hospital staff and simulated external services through defined system boundaries and provides centralized management of:

```text
Patients
   ↓
Appointments
   ↓
Medical Records
   ↓
Laboratory / Pharmacy
   ↓
Admissions
   ↓
Billing
   ↓
Payment
   ↓
Audit
   ↓
Reporting
   ↓
Backup
```

The system is designed to demonstrate not only hospital functionality but also reliability, security, concurrency, persistence, reporting, reflection, plugins, testing, performance diagnostics, and advanced C# capabilities within the defined V1 scope.

The architecture and implementation must remain within the V1 boundary. Web, mobile, SQL, EF Core, ASP.NET Core, production external integrations, and distributed deployment are reserved for future versions.
