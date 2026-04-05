# Technology Stack: Stambat Backend

## Core Framework & Language
- **Language:** C# (.NET 10.0)
- **Framework:** ASP.NET Core Minimal API (no controllers — uses endpoint modules)
- **Architecture:** Clean Architecture with Domain-Driven Design (DDD) principles.

## Data & Persistence
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core (EF Core)
- **Patterns:** Repository Pattern (Aggregate Root focused), Unit of Work, EF Core SaveChanges Interceptors (for Automated Auditing)

## Domain Modeling
- **Value Objects:** Implemented as C# `record` types with self-validation.
- **Aggregate Roots:** Rich domain models with encapsulated state (private/init setters) and static factory methods.
- **Validation:** `Guard` clauses for domain-level business logic validation.
- **Persistence:** Repositories consolidated to manage aggregate roots (e.g., `IUserRepository` manages the full Identity aggregate).

## Application Logic
- **Pattern:** CQRS (Command Query Responsibility Segregation) via **MediatR**
- **Mapping:** Mapster
- **Validation:** Fluent Validation

## Security & Authentication
- **Auth:** JWT (JSON Web Tokens)
- **Password Hashing:** BCrypt (using BCrypt.Net-Next)
- **Services:** Dedicated `JwtService`, `PermissionService`, `SecurityService`
- **Middleware:** Custom JWT and Exception Handling middleware.

## Infrastructure & Tools
- **CLI Tasks:** Makefile
- **Pre-commit Hooks:** Husky.Net
- **API Documentation:** Swagger/OpenAPI with custom Auth extension.
- **Static Analysis:** Microsoft.CodeAnalysis.BannedApiAnalyzers (to enforce Guid.CreateVersion7 via `IdGenerator.New()`)
