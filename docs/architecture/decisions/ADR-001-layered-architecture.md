# ADR-001: Layered Architecture

## Status

Accepted

## Context

Nile Care V1 contains different types of responsibilities, including hospital business rules, application workflows, console interaction, file persistence, external services, and plugins.

If these responsibilities are mixed together, changes to technical details could directly affect the hospital business logic.

The system therefore needs a clear separation between business logic and technical implementation.

## Decision

Nile Care V1 will use a **Clean Architecture-inspired layered architecture**.

The solution will be divided into the following projects:

```text
Hospital.Presentation
        ↓
Hospital.Application
        ↓
Hospital.Domain
        ↑
Hospital.Infrastructure

Hospital.Contracts
        ↑
Hospital.Plugins
```

### Hospital.Domain

Contains the core hospital domain model and business rules.

Examples:

* Entities
* Value Objects
* Aggregate Roots
* Domain behavior
* Business rules

### Hospital.Application

Contains application use cases and workflows.

Examples:

* Register Patient
* Schedule Appointment
* Admit Patient
* Dispense Medication
* Create Invoice

### Hospital.Infrastructure

Contains technical implementations.

Examples:

* JSON persistence
* XML reports
* Binary backups
* File I/O
* HTTP communication
* Plugin loading
* Logging

### Hospital.Presentation

Contains the console user interface.

Examples:

* Command input
* Command parsing
* Output formatting
* Error messages

### Hospital.Contracts

Contains stable contracts used by the plugin system.

### Hospital.Plugins

Contains plugin implementations that depend on the stable plugin contracts.

## Dependency Rules

The main dependency direction is:

```text
Presentation → Application → Domain
                         ↑
                         |
                  Infrastructure
```

The Domain must not depend on Infrastructure or Presentation.

The Application must not depend directly on Infrastructure.

Infrastructure implements abstractions required by the core/application layers.

## Rationale

This architecture was selected because it:

* Separates responsibilities.
* Protects the domain model from technical details.
* Supports independent testing of business logic.
* Makes infrastructure replaceable.
* Makes the dependency direction explicit.
* Supports future changes without rewriting the core domain.

## Alternatives Considered

### All-in-One Architecture

Rejected because all responsibilities would be placed in one project, making separation and testing more difficult.

### Traditional N-Layer Architecture

Not selected as the primary architectural model because Nile Care needs stronger dependency direction and protection of the domain from infrastructure details.

### Microservices

Rejected for V1 because the system does not require independently deployable services and microservices would introduce unnecessary complexity.

## Consequences

### Positive

* Clear separation of concerns.
* Better testability.
* Business rules remain independent from technical details.
* Easier replacement of infrastructure implementations.

### Negative

* More projects and files.
* More initial architectural setup.
* Developers must follow dependency rules carefully.
