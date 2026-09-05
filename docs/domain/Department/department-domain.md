# Department Domain

## 1. Overview

The Department Domain manages the hospital's organizational department hierarchy and employee assignments.

The domain supports:

* Department hierarchy
* Parent-child relationships
* Recursive department lookup
* DFS tree traversal
* Employee assignment
* Employee reassignment
* Circular hierarchy protection

The domain is independent of persistence, infrastructure, HTTP, and console UI.

---

## 2. Department Aggregate

`Department` is an **Aggregate Root** and inherits from `AggregateRoot<Guid>`.

The aggregate is responsible for maintaining department hierarchy and employee assignment rules.

### Main Properties

* `Id` — Unique department identifier.
* `Name` — Department name.
* `Parent` — Parent department reference.
* `ParentDepartmentId` — ID of the parent department.
* `Children` — Child departments.
* `AssignedEmployeeIds` — Employees currently assigned to the department.

---

## 3. Department Hierarchy

Departments use a recursive **N-ary tree** structure.

A department can have zero or more child departments, and each child can have its own children.

Example:

```text
Hospital
├── Medical
│   ├── Cardiology
│   └── Neurology
└── Administration
    └── Billing
```

The root department has no parent.

Child departments have a parent department.

The hierarchy supports arbitrary nesting.

---

## 4. Composite Pattern

The Department hierarchy uses the **Composite Pattern** through `IOrganizationUnit`.

`Department` can contain other `Department` objects as children.

This allows the same structure to represent:

```text
Department
    ↓
Department
    ↓
Department
```

The pattern is appropriate because hospital departments naturally form a hierarchical tree.

No separate `DepartmentLeaf` or `DepartmentComposite` classes are required because every department can potentially contain children.

---

## 5. Department Business Rules

The following rules are enforced by the domain:

1. Department names cannot be null, empty, or whitespace.
2. Valid department names are trimmed.
3. A root department can exist without a parent.
4. A child department can have only one parent.
5. A department cannot contain itself.
6. Circular department relationships are prohibited.
7. A child cannot be added twice to the same department.
8. Employee IDs cannot be empty.
9. An employee cannot be assigned twice to the same department.
10. An employee must exist in the current department before reassignment.
11. The target department must exist.
12. An employee cannot be reassigned to the same department.

---

## 6. Department Hierarchy Operations

### AddSubDepartment()

Adds a child department to the current department.

The operation:

* Validates the child.
* Prevents self-reference.
* Prevents duplicate children.
* Prevents circular relationships.
* Prevents multiple parents.
* Sets the child's parent.
* Adds the child to the hierarchy.

### RemoveSubDepartment()

Removes an existing child department and clears its parent relationship.

---

## 7. Recursive Department Lookup

`FindDepartmentRecursive(Guid id)` performs a **Depth-First Search (DFS)**.

The method:

1. Checks the current department.
2. Recursively searches each child.
3. Continues until the department is found.
4. Returns `null` when the department does not exist.

Example:

```text
Hospital
├── Medical
│   └── Cardiology
└── Administration
    └── Billing
```

Searching for `Billing` traverses:

```text
Hospital
→ Medical
→ Cardiology
→ Administration
→ Billing
```

---

## 8. Employee Assignment

`AssignEmployee(Guid employeeId)` adds an employee to the department.

The domain prevents:

* Empty employee IDs.
* Duplicate employee assignments.

`UnassignEmployee(Guid employeeId)` removes an employee from the department.

---

## 9. Employee Reassignment

`ReassignEmployee()` moves an employee from the current department to a valid target department.

The operation follows:

```text
Current Department
        ↓
Validate Employee
        ↓
Validate Target Department
        ↓
Check Target Assignment
        ↓
Remove Employee from Current Department
        ↓
Assign Employee to Target Department
```

Employees can move between different branches of the hierarchy.

Example:

```text
Hospital
├── Medical
│   └── Cardiology
└── Administration
    └── Billing
```

An employee can be reassigned:

```text
Cardiology → Billing
```

The root department is used to locate the target department recursively.

---

## 10. Tree Traversal

The department hierarchy is traversed recursively using DFS.

Example:

```text
Hospital
├── Medical
│   ├── Cardiology
│   └── Neurology
└── Administration
    └── Billing
```

DFS order:

```text
Hospital
Medical
Cardiology
Neurology
Administration
Billing
```

Manual indentation is used to represent hierarchy levels.

No LINQ is required.

---

## 11. Domain Isolation

The Department Domain contains only domain logic.

It does not depend on:

* JSON
* File I/O
* Database
* Entity Framework Core
* HTTP
* Console UI
* Application services
* Repository implementations

Presentation code is responsible for displaying department information.

---

## 12. Design Decision

The Department Aggregate keeps hierarchy and employee assignment rules together to protect domain invariants.

The recursive Composite structure provides a simple way to support hospital departments at any depth while allowing recursive lookup and DFS traversal.

This design keeps the Department Domain focused, testable, and independent from infrastructure concerns.
