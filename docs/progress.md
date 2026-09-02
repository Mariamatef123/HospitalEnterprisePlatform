# Project Progress

## Day 1 — Requirements & Actor Analysis

### Phase 1 — Understand the SRS

* [x] Read the SRS once without designing classes
* [x] Understand the hospital problem
* [x] Identify why the system is needed
* [x] Identify system users
* [x] Identify what the system manages

### Phase 2 — Requirements

* [x] All SRS requirements classified
* [x] Functional requirements identified
* [x] FR IDs created
* [x] Non-functional requirements identified
* [x] NFR IDs created
* [x] Requirements categorized by module
* [x] Requirements mapping/traceability started

### Phase 3 — Business Rules

* [x] BR-001 — National ID must be unique
* [x] BR-002 — Doctor cannot have two appointments in the same slot
* [x] BR-003 — Appointment cannot be created in the past
* [x] BR-004 — Medication stock cannot become negative
* [x] BR-005 — Occupied bed cannot be assigned again
* [x] `business-rules.md` created

### Phase 4 — System Overview

* [x] `system-overview.md` created
* [x] System purpose documented
* [x] Problem statement documented
* [x] V1 scope defined
* [x] V2 / out-of-scope defined
* [x] Major system modules documented
* [x] External systems identified

### Phase 5 — Case Study

* [x] `case-study.md` created
* [x] 200-bed hospital documented
* [x] Current paper/spreadsheet problems documented
* [x] Proposed solution documented
* [x] Human users documented
* [x] External systems documented

### Phase 6 — Actor Analysis

#### Human Actors

* [x] Receptionist
* [x] Doctor
* [x] Nurse
* [x] Lab Technician
* [x] Pharmacist
* [x] Billing Clerk
* [x] Administrator

#### External Systems

* [x] Insurance Provider
* [x] Medication Supplier

#### Actor Details

* [x] Responsibilities documented
* [x] Commands/actions identified
* [x] Information access identified
* [x] Restricted information identified
* [x] Modules identified

### Phase 7 — Actor Access Matrix

* [x] Patient permissions
* [x] Appointment permissions
* [x] Medical Record permissions
* [x] Laboratory permissions
* [x] Pharmacy permissions
* [x] Billing permissions
* [x] Reporting permissions
* [x] Administrator permissions
* [x] Exact permissions verified against SRS

### Phase 8 — System Boundary

* [x] V1 boundary defined
* [x] Console application inside V1
* [x] File persistence inside V1
* [x] Core hospital modules inside V1
* [x] Plugins inside V1
* [x] Reporting inside V1
* [x] Audit inside V1
* [x] Backup inside V1
* [x] Web UI outside V1
* [x] Mobile application outside V1
* [x] SQL database outside V1
* [x] EF Core outside V1
* [x] ASP.NET Core outside V1
* [x] Distributed deployment outside V1
* [x] Production external services outside V1
* [x] External systems interact through contracts/interfaces

### Phase 9 — UML

* [x] C4 Level 1 System Context Diagram
* [x] Hospital Enterprise Platform shown at center
* [x] Human actors shown
* [x] Insurance Provider shown
* [x] Medication Supplier shown
* [x] System boundary shown
* [x] No classes in C4 Level 1
* [x] No database tables
* [x] No repositories
* [x] No controllers
* [x] No methods

### Phase 10 — GitHub

* [x] Repository created
* [x] Initial folders created
* [x] Labels created
* [x] Major issues created
* [x] CI workflow created
* [x] CI workflow pushed
* [x] Pull Request created
* [x] CI check verified
* [x] Main branch rules configured

### Phase 11 — Final Review

* [x] Requirements complete
* [x] Documentation complete
* [x] Actors complete
* [x] Access matrix complete
* [x] System boundary complete
* [x] C4 Level 1 complete
* [x] GitHub setup complete

### Later Deliverables — NOT Day 1

* [ ] Complete `traceability-matrix.md`
* [ ] Map every FR to implementation
* [ ] Map every NFR to implementation/evidence
* [ ] Map requirements to tests
* [ ] Finalize requirement evidence
* [ ] Finalize complete RTM

### Day 1 Status

**Completed ✅**

### Acceptance Check

* [x] Can explain the hospital system in approximately two minutes
* [x] Can explain the actors and their responsibilities
* [x] Can explain the V1 boundary
* [x] Can explain what is outside V1
* [x] Can explain the five core business rules
* [x] Can explain the external systems
* [x] Can explain the purpose of the C4 Level 1 diagram

-------------------------------------------------------

# Day 2 — Architecture & Project Layering

**Status:** ✅ Completed

## Projects

* [x] `Hospital.Domain`
* [x] `Hospital.Application`
* [x] `Hospital.Infrastructure`
* [x] `Hospital.Presentation`
* [x] `Hospital.Contracts`

## Dependencies

* [x] `Presentation → Application`
* [x] `Application → Domain`
* [x] `Infrastructure → inward-facing abstractions/core`
* [x] `Domain` does not reference `Infrastructure`
* [x] `Domain` does not reference `Presentation`
* [x] `Application` does not reference `Presentation`
* [x] Plugins are not referenced by the host at compile time

## Domain Isolation

* [x] No Console dependency
* [x] No JSON dependency
* [x] No HTTP dependency
* [x] No File I/O
* [x] No plugin loading
* [x] No unnecessary third-party dependencies

## Domain Primitives

* [x] Created `Entity<TId>`

  * Identity through `Id`
  * Identity-based equality
* [x] Created `ValueObject`

  * Value-based equality
* [x] Created `AggregateRoot<TId>`

  * Extends `Entity<TId>`
  * Provides the base for aggregate roots

## Documentation

* [x] `docs/architecture/architecture-overview.md`
* [x] `docs/architecture/dependency-rules.md`
* [x] `docs/architecture/system-boundary.md`
* [x] `ADR-001` — Layered Architecture
* [x] `ADR-002` — Console Application
* [x] `ADR-003` — File-Based Persistence
* [x] `ADR-004` — No LINQ
* [x] Architecture diagram
* [x] Dependency diagram

## Verification

* [x] `dotnet build` succeeds
* [x] `Hospital.Domain` compiles successfully
* [x] Domain can compile without requiring Infrastructure
* [x] Dependency direction verified
* [x] Compile-time isolation verified

## Evidence

Executed:

```powershell
dotnet build
```

Result:

```text
Hospital.Domain succeeded
Hospital.Contracts succeeded
Hospital.Infrastructure succeeded
Hospital.Application succeeded
```

The build completed successfully. The `CS1668` messages are environment warnings caused by invalid legacy Visual Studio paths in the `LIB` environment variable and do not affect the architecture verification.

## Day 2 Outcome

Nile Care V1 now has a defined layered architecture with clear dependency rules and an isolated Domain layer. The core Domain primitives (`Entity<TId>`, `ValueObject`, and `AggregateRoot<TId>`) have been established without adding unnecessary domain complexity.
