```markdown
# Employee Domain

## 1. Overview

The Employee Domain represents hospital employees and their responsibilities within the hospital system.

The domain uses object-oriented and Domain-Driven Design (DDD) concepts to model common employee information, specialized employee roles, value objects, domain behavior, and employee permissions.

**Project Structure:**

```text
Hospital.Domain/
├── Common/
│   ├── AggregateRoot.cs
│   ├── Entity.cs
│   └── ValueObject.cs
├── Employees/
│   ├── ValueObjects/
│   │   ├── NationalId.cs
│   │   ├── PersonName.cs
│   │   └── PhoneNumber.cs
│   ├── BillingClerk.cs
│   ├── Doctor.cs
│   ├── DoctorSpecialty.cs
│   ├── EmployeePermission.cs
│   ├── EmploymentStatus.cs
│   ├── HospitalEmployee.cs
│   ├── LabTechnician.cs
│   ├── Nurse.cs
│   ├── Pharmacist.cs
│   ├── Receptionist.cs
│   └── ShiftType.cs
└── Class1.cs
```

The employee hierarchy is:

```text
HospitalEmployee (Aggregate Root)
│
├── Doctor
├── Nurse
├── Receptionist
├── LabTechnician
├── Pharmacist
└── BillingClerk
```

`HospitalEmployee` provides common employee state and behavior, while each specialized employee defines role-specific behavior and permissions.

## 2. HospitalEmployee

`HospitalEmployee` is an abstract base class that serves as the **Aggregate Root** for all hospital employees.

It contains common information shared by employees:

- Employee ID (inherited from `Entity`) - `Guid`
- National ID (`NationalId` value object)
- Name (`PersonName` value object)
- Phone number (`PhoneNumber` value object)
- Department ID (`Guid`)
- Employment Status (`EmploymentStatus` enum)

The class is abstract because the system should not create a generic employee directly. Every employee must have a specific role.

The `GetRolePermissions()` method is abstract because each employee type has different permissions. It returns a collection of `EmployeePermission` enum values.

```csharp
public abstract class HospitalEmployee : AggregateRoot
{
    public NationalId NationalId { get; private set; }
    public PersonName Name { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Guid DepartmentId { get; private set; }
    public EmploymentStatus Status { get; private set; }
    
    public abstract IReadOnlyCollection<EmployeePermission> GetRolePermissions();
}
```

## 3. Employee Specializations

### 3.1 Doctor

A `Doctor` inherits from `HospitalEmployee`.

Doctor-specific information includes:

- Doctor specialty (`DoctorSpecialty` enum)
- License number (`string`)

Doctor permissions include:

- `ViewMedicalRecord`
- `UpdateMedicalRecord`
- `CreatePrescription`

### 3.2 Nurse

A `Nurse` inherits from `HospitalEmployee`.

A nurse has a specific shift type (`ShiftType` enum) and permissions related to patient care.

Nurse permissions include:

- `ViewMedicalRecord`
- `UpdateVitalSigns`

### 3.3 Receptionist

A `Receptionist` inherits from `HospitalEmployee`.

The receptionist is responsible for front-desk operations.

Receptionist permissions include:

- `RegisterPatient`
- `ScheduleAppointment`

### 3.4 LabTechnician

A `LabTechnician` inherits from `HospitalEmployee`.

The lab technician is responsible for laboratory-related operations.

Lab Technician permission:

- `ProcessLabTests`

### 3.5 Pharmacist

A `Pharmacist` inherits from `HospitalEmployee`.

The pharmacist is responsible for medication inventory operations.

Pharmacist permission:

- `ManageMedicationInventory`

### 3.6 BillingClerk

A `BillingClerk` inherits from `HospitalEmployee`.

The billing clerk is responsible for billing operations.

Billing Clerk permission:

- `ProcessBilling`

## 4. Employee Value Objects

The Employee Domain uses value objects to represent important employee information:

- `PersonName`
- `PhoneNumber`
- `NationalId`

These value objects inherit from the domain `ValueObject` base class located in the `Common` folder.

Unlike entities, value objects do not have their own identity. Their equality is based on their values.

### 4.1 PersonName

`PersonName` represents an employee's name.

It contains:

- `FirstName` (`string`)
- `LastName` (`string`)

The constructor validates that both names are provided and trims unnecessary surrounding spaces.

### 4.2 PhoneNumber

`PhoneNumber` represents an employee's phone number.

It:

- Requires a phone number
- Trims surrounding spaces
- Allows an optional leading `+`
- Validates digits only after the optional `+`
- Requires between 7 and 15 digits

### 4.3 NationalId

`NationalId` represents the employee's national identification number.

The current implementation validates:

- The value is required
- Surrounding spaces are removed
- Exactly 14 digits are required
- Only numeric characters are allowed

## 5. Aggregate Boundary

The Employee Domain treats the employee as the main consistency boundary for employee-specific state and behavior.

Conceptually:

```text
Employee Aggregate
│
└── HospitalEmployee (AggregateRoot)
    │
    ├── Identity (Id: Guid from Entity)
    ├── NationalId (ValueObject)
    ├── PersonName (ValueObject)
    ├── PhoneNumber (ValueObject)
    ├── DepartmentId (Guid)
    ├── EmploymentStatus (Enum)
    │
    └── Role-specific behavior
        ├── Doctor
        │   ├── DoctorSpecialty (Enum)
        │   └── LicenseNumber (string)
        ├── Nurse
        │   └── ShiftType (Enum)
        ├── Receptionist
        ├── LabTechnician
        ├── Pharmacist
        └── BillingClerk
```

The aggregate boundary helps keep employee-related rules inside the domain model rather than scattering them across other layers.

Other aggregates should not directly modify internal employee state.

## 6. Domain Behavior

The Employee Domain contains behavior that represents business rules.

The main example is:

```csharp
GetRolePermissions()
```

Each employee type returns permissions appropriate to its role using the `EmployeePermission` enum.

For example:

```text
Doctor
    ↓
EmployeePermission.ViewMedicalRecord
EmployeePermission.UpdateMedicalRecord
EmployeePermission.CreatePrescription
```

```text
Nurse
    ↓
EmployeePermission.ViewMedicalRecord
EmployeePermission.UpdateVitalSigns
```

```text
Pharmacist
    ↓
EmployeePermission.ManageMedicationInventory
```

This keeps role-related business behavior inside the domain model.

## 7. Polymorphism

The Employee Domain uses runtime polymorphism through the `HospitalEmployee` base type.

A specialized employee can be referenced using the base type:

```csharp
HospitalEmployee employee = new Doctor(...);
```

The actual runtime object is still a `Doctor`.

When the application calls:

```csharp
var permissions = employee.GetRolePermissions();
```

the overridden implementation belonging to the actual employee type is executed.

This allows the application to work with different employee types through the common `HospitalEmployee` abstraction without large `if` or `switch` statements.

## 8. Inheritance Decision

### 8.1 Why use inheritance?

Inheritance is appropriate because all specialized employee types share the same fundamental identity and common employee information.

Every:

- Doctor
- Nurse
- Receptionist
- LabTechnician
- Pharmacist
- BillingClerk

is a hospital employee.

Therefore:

```text
Doctor IS-A HospitalEmployee
Nurse IS-A HospitalEmployee
Receptionist IS-A HospitalEmployee
LabTechnician IS-A HospitalEmployee
Pharmacist IS-A HospitalEmployee
BillingClerk IS-A HospitalEmployee
```

This represents a genuine **IS-A relationship**.

### 8.2 Why not use composition for employee roles?

Composition is appropriate when an object has another object.

For example:

```text
Doctor HAS-A PersonName
Doctor HAS-A PhoneNumber
Doctor HAS-A NationalId
```

These relationships are represented using value objects.

However:

```text
Doctor HAS-A HospitalEmployee
```

would not correctly represent the domain.

A doctor is not an object that contains an employee. A doctor **is** an employee.

Therefore inheritance is appropriate for the employee role hierarchy, while composition is used for employee attributes such as name, phone number, and national ID.

## 9. DDD Decisions

### 9.1 Entity

An employee is modeled as an Entity because it has a unique identity.

The identity is represented by:

```csharp
Guid Id
```

Two employees with different IDs represent different entities even if they have the same name or other values.

The `Entity` base class is located in the `Common` folder.

### 9.2 Aggregate Root

`HospitalEmployee` represents the root abstraction of the Employee aggregate. It inherits from `AggregateRoot` located in the `Common` folder.

The aggregate root is responsible for controlling employee-related state and domain behavior.

Specialized employees such as `Doctor`, `Nurse`, and `Pharmacist` inherit from this root abstraction and provide their specific behavior.

### 9.3 Value Object

The following are modeled as Value Objects:

```text
PersonName
PhoneNumber
NationalId
```

They do not have independent identities. Their equality depends on their values.

These inherit from the `ValueObject` base class in the `Common` folder.

### 9.4 Enums

The following enums are used to represent fixed sets of values:

- `EmployeePermission` - Defines all available permissions
- `EmploymentStatus` - Employee employment states (e.g., Active, OnLeave, Terminated)
- `DoctorSpecialty` - Medical specialties for doctors
- `ShiftType` - Shift types for nurses

### 9.5 Domain Behavior

Domain behavior represents operations and rules that belong to the Employee Domain.

The primary example is:

```csharp
GetRolePermissions()
```

Each employee specialization provides its own permission set.

This keeps role-related behavior close to the employee model.

### 9.6 Aggregate Boundary

The Employee aggregate boundary contains employee state and employee-specific domain behavior.

The boundary exists to maintain consistency and protect domain invariants.

Other parts of the system should interact with employees through appropriate domain or application operations instead of directly modifying internal employee state.

## 10. Design Summary

| Concept | Design Decision |
| ------- | --------------- |
| Entity Base | `Entity` (in `Common/Entity.cs`) with `Guid Id` |
| Aggregate Root | `HospitalEmployee` inherits from `AggregateRoot` |
| Inheritance | Specialized employee roles inherit from `HospitalEmployee` |
| Polymorphism | Each role overrides `GetRolePermissions()` |
| Value Objects | `PersonName`, `PhoneNumber`, `NationalId` inherit from `ValueObject` |
| Enums | `EmployeePermission`, `EmploymentStatus`, `DoctorSpecialty`, `ShiftType` |
| Domain Behavior | Role permission calculation via `GetRolePermissions()` |
| Aggregate Boundary | Employee state and behavior are kept within the Employee aggregate |
| Validation | Invalid employee values are rejected by constructors |
| Encapsulation | Employee properties are controlled by the domain model |

**Type Summary:**

| Property | Type |
| -------- | ---- |
| `Id` | `Guid` (inherited from `Entity`) |
| `NationalId` | `NationalId` (Value Object) |
| `Name` | `PersonName` (Value Object) |
| `PhoneNumber` | `PhoneNumber` (Value Object) |
| `DepartmentId` | `Guid` |
| `Status` | `EmploymentStatus` (Enum) |
| `DoctorSpecialty` | `DoctorSpecialty` (Enum) |
| `ShiftType` | `ShiftType` (Enum) |
| `LicenseNumber` | `string` |

**Folder Structure Summary:**

```text
Hospital.Domain/
├── Common/
│   ├── AggregateRoot.cs    # Base for aggregate roots
│   ├── Entity.cs           # Base for entities with Guid Id
│   └── ValueObject.cs      # Base for value objects
├── Employees/
│   ├── ValueObjects/
│   │   ├── NationalId.cs   # National ID validation (14 digits)
│   │   ├── PersonName.cs   # Name validation
│   │   └── PhoneNumber.cs  # Phone number validation
│   ├── HospitalEmployee.cs # Abstract aggregate root with Guid DepartmentId
│   ├── Doctor.cs           # Doctor specialization
│   ├── Nurse.cs            # Nurse specialization
│   ├── Receptionist.cs     # Receptionist specialization
│   ├── LabTechnician.cs    # Lab Technician specialization
│   ├── Pharmacist.cs       # Pharmacist specialization
│   ├── BillingClerk.cs     # Billing Clerk specialization
│   ├── EmployeePermission.cs # Permission enum
│   ├── EmploymentStatus.cs # Employment status enum
│   ├── DoctorSpecialty.cs  # Doctor specialty enum
│   └── ShiftType.cs        # Nurse shift type enum
└── Class1.cs
```

The design uses inheritance where a true **IS-A** relationship exists and value objects where concepts are defined by their values rather than identity. Common DDD building blocks are organized in the `Common` folder, while employee-specific implementations reside in the `Employees` folder with clear separation of value objects and entities. All identifiers (`Id`, `DepartmentId`) are typed as `Guid` for consistency across the domain.
```

---

