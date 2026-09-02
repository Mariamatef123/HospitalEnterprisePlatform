# Nile Care — Architecture Overview

## 1. Architecture Style

Nile Care V1 uses a **Clean Architecture-inspired layered architecture**.

The system is a **monolithic Console Application** with file-based persistence.

The main goal is to keep business rules independent from technical details such as the console, JSON files, XML reports, and external services.

## 2. Layers

The system is divided into the following projects:

### Hospital.Domain

Contains the core hospital domain model and business rules.

Responsibilities:

* Entities
* Value Objects
* Aggregate Roots
* Domain behavior and business rules

The Domain layer must not depend on infrastructure, console, file I/O, HTTP, or external frameworks.

### Hospital.Application

Contains application use cases and workflows.

Examples:

* Register Patient
* Schedule Appointment
* Admit Patient
* Dispense Medication
* Create Invoice

The Application layer coordinates domain objects but does not implement infrastructure details.

### Hospital.Infrastructure

Contains technical implementations.

Responsibilities:

* JSON persistence
* XML report generation
* Binary backup
* File I/O
* HTTP/external service communication
* Plugin loading
* Logging

Infrastructure implements abstractions required by the Application or Domain.

### Hospital.Presentation

Contains the console interface.

Responsibilities:

* Read user input
* Parse commands
* Display results
* Display validation and error messages

Presentation communicates with the Application layer.

### Hospital.Contracts

Contains stable contracts shared with the plugin system.

Example:

```text
IPlugin
```

This project should remain independent from the main application layers.

### Hospital.Plugins

Contains plugin implementations that use the contracts defined by `Hospital.Contracts`.

## 3. Dependency Direction

The intended dependency direction is:

```text
Hospital.Presentation
        |
        v
Hospital.Application
        |
        v
Hospital.Domain
        ^
        |
Hospital.Infrastructure
```

Infrastructure implements abstractions defined by the core/application layers.

The important rule is that **Domain and Application must not depend on Infrastructure**.

## 4. V1 Constraints

Nile Care V1 intentionally has the following constraints:

* Console application only
* No database
* File-based persistence
* JSON for operational data
* XML for reports
* Binary format for backups
* UTF-8 support for Arabic and multilingual data
* No LINQ
* Plugin system through stable contracts
* No microservices
* No unnecessary external infrastructure
* Business rules remain independent from persistence and presentation

## 5. Main Architectural Goal

The architecture should make it possible to change technical details without changing the core business rules.

For example:

```text
JSON files → Database
Console → Web API
Local files → Cloud storage
```

These changes should not require rewriting the core domain model.
