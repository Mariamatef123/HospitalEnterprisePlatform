# ADR-003: File-Based Persistence

## Status

Accepted

## Context

Nile Care V1 needs to persist operational data, generate reports, and create backups.

A relational database could be used for persistence, but V1 intentionally focuses on C# domain modeling, architecture, file handling, serialization, testing, and business rules.

## Decision

Nile Care V1 will use **file-based persistence instead of a database**.

The following formats will be used:

```text
JSON   → Operational data
XML    → Reports
Binary → Backups
```

UTF-8 encoding will be used to support Arabic and multilingual data.

File persistence will be implemented in:

```text
Hospital.Infrastructure
```

The Domain and Application layers must not directly perform file I/O.

## Rationale

File-based persistence was selected because:

* It is part of the V1 requirements.
* It avoids database infrastructure.
* It allows practicing serialization and file handling.
* It keeps the initial system simpler.
* It preserves the separation between business logic and persistence technology.

## Alternatives Considered

### SQL Server

Rejected for V1 because database persistence is outside the initial scope.

### Entity Framework Core

Rejected because V1 does not use a database and EF Core is therefore unnecessary.

### PostgreSQL / MySQL

Rejected for the same reason: V1 does not require relational database persistence.

## Consequences

### Positive

* No database setup required.
* Simple local deployment.
* Easy inspection of stored JSON data.
* Provides practice with multiple serialization formats.

### Negative

* File persistence is less suitable for high-concurrency production workloads.
* Querying and transaction management are more limited than with a database.
* A future database migration may require a new Infrastructure implementation.

## Future Consideration

A future version may replace file-based persistence with a database.

Such a change should primarily affect the Infrastructure layer while keeping the Domain business rules independent from the persistence technology.
