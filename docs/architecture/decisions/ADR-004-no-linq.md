# ADR-004: No LINQ

## Status

Accepted

## Context

C# provides Language Integrated Query (LINQ), which can simplify collection filtering, projection, sorting, and aggregation.

However, Nile Care V1 intentionally requires manual collection operations in selected areas of the system.

The purpose is to strengthen understanding of core C# collection processing, iteration, conditions, and algorithms.

## Decision

Nile Care V1 will **not use LINQ**.

Collection operations that would normally be implemented using LINQ must instead use appropriate C# constructs such as:

* `foreach`
* `for`
* `if`
* Manual collection building
* Explicit sorting and searching logic where required

For example, instead of:

```csharp
patients.Where(p => p.Name == name)
```

the application should perform the required filtering manually.

## Rationale

This constraint was selected because it:

* Is an explicit V1 project requirement.
* Strengthens understanding of C# fundamentals.
* Makes collection-processing logic explicit.
* Provides practice with manual filtering and searching.
* Prevents relying on LINQ for required learning objectives.

## Scope

The no-LINQ rule applies to application and domain collection-processing code where manual implementation is required.

Developers should not introduce LINQ as a shortcut for these operations.

## Alternatives Considered

### Using LINQ

Rejected because it conflicts with the V1 project constraint.

### Allowing LINQ Everywhere

Rejected because the purpose of V1 includes practicing manual collection operations.

## Consequences

### Positive

* Stronger understanding of C# collection processing.
* Explicit algorithms.
* Compliance with the V1 requirements.

### Negative

* More verbose code.
* Some operations require more implementation effort.
* Code may be less concise than equivalent LINQ expressions.

## Future Consideration

A future version may allow LINQ if the project requirements change and its use provides a clear benefit.
