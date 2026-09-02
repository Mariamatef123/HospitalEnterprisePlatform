# ADR-002: Console Application

## Status

Accepted

## Context

Nile Care V1 needs a user interface for performing hospital operations.

Possible approaches include a Console Application, ASP.NET Core Web API, MVC application, or another user interface.

The primary purpose of V1 is to implement and demonstrate domain modeling, business rules, architecture, persistence, testing, concurrency, and other C# features without introducing unnecessary web-specific complexity.

## Decision

Nile Care V1 will use a **Console Application** as its presentation layer.

The console application will:

* Accept commands from users.
* Parse command arguments.
* Invoke application use cases.
* Display results.
* Display validation and error messages.

The console layer will not contain core hospital business rules.

## Rationale

A Console Application was selected because it:

* Keeps the presentation layer simple.
* Allows focus on the domain and application layers.
* Reduces unnecessary web infrastructure.
* Is easy to run and test locally.
* Matches the V1 project scope.

## Alternatives Considered

### ASP.NET Core Web API

Rejected for V1 because HTTP and web infrastructure are not required for the initial application.

### ASP.NET Core MVC

Rejected because a web user interface is outside the scope of V1.

## Consequences

### Positive

* Simple user interface.
* Low infrastructure complexity.
* Faster development.
* Clear separation between presentation and business logic.

### Negative

* No web interface in V1.
* No HTTP API as the primary user interface.
* A future web interface would require an additional presentation layer.
