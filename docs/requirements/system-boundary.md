# Hospital Enterprise Platform — System Boundary

## 1. Purpose

The system boundary defines what is included in **Version 1 (V1)** of the Hospital Enterprise Platform and what is outside its scope.

The boundary prevents the V1 implementation from expanding into technologies and features that are explicitly planned for future versions.

---

# 2. V1 System Boundary

The following components are **inside the V1 system boundary**:

```text
┌──────────────────────────────────────────────────────────────┐
│             Hospital Enterprise Platform — V1              │
│                                                              │
│  Hospital Console                                            │
│  ├── Patient Management                                      │
│  ├── Employee Management                                     │
│  ├── Appointment Management                                  │
│  ├── Medical Records                                         │
│  ├── Laboratory Management                                   │
│  ├── Pharmacy & Inventory                                    │
│  ├── Admissions & Beds                                       │
│  ├── Billing & Payment                                       │
│  ├── Authentication & Authorization                          │
│  ├── File Persistence                                        │
│  ├── Reporting                                               │
│  ├── Audit                                                   │
│  ├── Backup                                                  │
│  ├── Import & Export                                         │
│  ├── Plugin Management                                       │
│  └── Command Management                                      │
│                                                              │
└──────────────────────────────────────────────────────────────┘
```

### 2.1 Hospital Console

The V1 system provides a command-line interface through which authorized hospital users interact with the system.

### 2.2 Patient Management

Includes patient registration, searching, updating, and soft deactivation.

### 2.3 Employee Management

Includes hospital employee management and the employee hierarchy required by the system.

### 2.4 Appointment Management

Includes appointment creation, cancellation, completion, schedule management, and prevention of double-booking.

### 2.5 Medical Records

Includes medical records, diagnoses, prescriptions, and relevant patient medical history.

### 2.6 Laboratory

Includes laboratory requests, laboratory processing, queues, emergency requests, and laboratory results.

### 2.7 Pharmacy & Inventory

Includes medication inventory, dispensing, stock management, expiration monitoring, and low-stock notifications.

### 2.8 Admissions & Beds

Includes patient admission, discharge, ward management, bed assignment, and occupancy management.

### 2.9 Billing & Payment

Includes invoice creation, invoice calculation, payment recording, hospital service charges, and insurance verification through the defined external contract.

### 2.10 File Persistence

V1 uses file-based persistence.

The system uses:

* JSON for operational entity data.
* XML for reports and import/export data.
* Binary format for internal backup/archive data.

### 2.11 Plugins

V1 supports dynamically loaded plugins through the defined plugin contract.

Plugins are loaded from the designated `plugins/` folder without compile-time references from the host to individual plugin implementations.

### 2.12 Reporting

V1 provides hospital reports such as:

* Patient reports
* Doctor reports
* Pharmacy reports
* Laboratory reports

The reporting subsystem also supports the required concurrent and cancellable report-generation operations.

### 2.13 Audit

V1 records auditable system actions, including who performed an action and when it occurred.

### 2.14 Backup

V1 provides backup and restoration functionality for internal hospital data.

### 2.15 Import & Export

V1 supports the required import and export operations using the appropriate persistence formats.

---

# 3. Outside the V1 System Boundary

The following technologies and capabilities are explicitly **outside the V1 system boundary**.

## 3.1 Web Application

A browser-based user interface is not part of V1.

## 3.2 Mobile Application

Android or iOS applications are not part of V1.

## 3.3 SQL Database

V1 does not use a SQL database as its primary persistence mechanism.

Database-based persistence may be introduced in a future version.

## 3.4 Entity Framework Core

Entity Framework Core is outside the V1 implementation.

## 3.5 ASP.NET Core

ASP.NET Core and web-based API infrastructure are outside the V1 implementation.

## 3.6 Production Insurance Service

V1 does not connect to a real production insurance provider.

Insurance communication is simulated through a controlled external service and a defined contract.

## 3.7 Distributed Deployment

V1 is not designed as a distributed deployment.

Distributed services, service orchestration, and multi-server deployment are outside the V1 scope.

---

# 4. External System Boundary

The **Insurance Provider** and **Medication Supplier** are external systems.

They are **not part of the Hospital Enterprise Platform domain**.

The hospital system communicates with them through defined contracts/interfaces.

```text
                         V1 System Boundary

┌───────────────────────────────────────────────────────────────┐
│                                                               │
│              Hospital Enterprise Platform                    │
│                                                               │
│   Hospital Domain                                             │
│   Patient | Employee | Appointment | Medical Record          │
│   Laboratory | Pharmacy | Admission | Billing | etc.          │
│                                                               │
│                    │                         │                 │
│              Contract / Interface      Contract / Interface   │
│                    │                         │                 │
└────────────────────┼─────────────────────────┼─────────────────┘
                     │                         │
                     ▼                         ▼
             ┌───────────────┐       ┌──────────────────┐
             │   Insurance   │       │    Medication    │
             │    Provider   │       │     Supplier     │
             │ External      │       │ External         │
             │ System        │       │ System           │
             └───────────────┘       └──────────────────┘
```

---

# 5. Contract-Based Interaction

The V1 system must communicate with external systems through abstractions rather than directly embedding external-system behavior inside the hospital domain.

For example:

```text
Hospital.Application
        │
        ▼
IInsuranceProvider
        │
        ▼
SimulatedInsuranceProvider
        │
        ▼
External HTTP Service
```

Similarly:

```text
Hospital.Application
        │
        ▼
IMedicationSupplier
        │
        ▼
SimulatedMedicationSupplier
        │
        ▼
External HTTP Service
```

The hospital domain therefore depends on the **contract/interface**, not on the concrete external service.

This allows the external implementation to be replaced without changing the core hospital domain.

---

# 6. Boundary Rules

The following rules apply to the V1 system boundary:

1. V1 interaction is console-based.
2. V1 persistence is file-based.
3. SQL databases are not used as the primary V1 persistence mechanism.
4. EF Core is not used in V1.
5. ASP.NET Core is not used in V1.
6. Web and mobile interfaces are outside V1.
7. Real production insurance integrations are outside V1.
8. Insurance Provider and Medication Supplier remain external systems.
9. External systems communicate through contracts/interfaces.
10. External service failures must not crash the hospital application.
11. Plugin implementations must remain separate from the core hospital domain.
12. The V1 boundary must remain stable unless the SRS is formally changed.

---

# 7. V1 vs Future Scope

| Area                | V1                    | Future Versions           |
| ------------------- | --------------------- | ------------------------- |
| User Interface      | Console               | Web / Mobile              |
| Persistence         | Files                 | SQL Database              |
| ORM                 | None                  | EF Core                   |
| Web Framework       | None                  | ASP.NET Core              |
| Insurance           | Simulated service     | Production integration    |
| Medication Supplier | Simulated service     | Production integration    |
| Deployment          | Local/non-distributed | Distributed deployment    |
| Plugins             | Dynamic DLL loading   | Extended plugin ecosystem |
