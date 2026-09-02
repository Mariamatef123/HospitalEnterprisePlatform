# Nile Care — Dependency Rules

## 1. Purpose

These rules define which projects are allowed to depend on each other.

The purpose is to protect the Domain and Application layers from technical details.

## 2. Allowed Dependencies

| Project                 | Allowed Dependency                    |
| ----------------------- | ------------------------------------- |
| Hospital.Domain         | None                                  |
| Hospital.Application    | Hospital.Domain                       |
| Hospital.Infrastructure | Hospital.Application, Hospital.Domain |
| Hospital.Presentation   | Hospital.Application                  |
| Hospital.Contracts      | None                                  |
| Hospital.Plugins        | Hospital.Contracts                    |

The intended dependency flow is:

```text
Presentation
     |
     v
Application
     |
     v
Domain
     ^
     |
Infrastructure
```

## 3. Domain Rules

`Hospital.Domain` must have no dependency on:

* Hospital.Infrastructure
* Hospital.Presentation
* Hospital.Application
* Console APIs
* File-system APIs
* JSON/XML serialization
* HTTP clients
* Database libraries
* Plugin implementations

The Domain must remain independently compilable.

## 4. Application Rules

`Hospital.Application` may depend on:

```text
Hospital.Domain
```

It must not depend directly on:

```text
Hospital.Infrastructure
Hospital.Presentation
Hospital.Plugins
```

Application code should depend on abstractions rather than concrete infrastructure implementations.

For example:

```text
Application
    |
    v
IPatientRepository
    ^
    |
JsonPatientRepository
    |
Infrastructure
```

## 5. Infrastructure Rules

`Hospital.Infrastructure` contains technical implementations.

It may depend on:

```text
Hospital.Application
Hospital.Domain
```

Infrastructure must not force the Domain to depend on its implementation details.

Examples of Infrastructure responsibilities include:

* JSON persistence
* File I/O
* XML generation
* Binary backup
* HTTP communication
* Plugin loading
* Logging

## 6. Presentation Rules

`Hospital.Presentation` is responsible only for the console interface.

It may depend on:

```text
Hospital.Application
```

It should not contain:

* Domain business rules
* JSON persistence logic
* File I/O implementations
* Database logic
* Infrastructure business decisions

## 7. Contracts Rules

`Hospital.Contracts` must remain independent.

It contains stable contracts used by plugins.

For example:

```text
IPlugin
```

`Hospital.Contracts` must not depend on:

```text
Hospital.Domain
Hospital.Application
Hospital.Infrastructure
Hospital.Presentation
```

## 8. Plugin Rules

`Hospital.Plugins` depends on:

```text
Hospital.Contracts
```

Plugins should communicate with the main application through the stable contracts.

## 9. Forbidden Dependencies

The following dependencies are explicitly forbidden:

```text
Domain → Infrastructure        
Domain → Presentation           
Domain → Application            
Domain → Plugins                

Application → Infrastructure    
Application → Presentation      
Application → Plugins           

Contracts → Domain              
Contracts → Application         
Contracts → Infrastructure      

Plugins → Domain                
Plugins → Application           
Plugins → Infrastructure        
```

## 10. Architectural Principle

The core rule is:

> **Business logic must not depend on technical implementation details.**

Technical details depend on the core abstractions, not the other way around.

This keeps the Domain independently testable and replaceable.
