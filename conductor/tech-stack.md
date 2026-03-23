# Technology Stack: Stambat Backend

## Core Framework & Language
- **Language:** C#
- **Framework:** .NET 8.0 (ASP.NET Core Web API)
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
- **Pattern:** CQRS (Command Query Responsibility Segregation)
- **Mapping:** Mapster
- **Validation:** Fluent Validation

## Security & Authentication
- **Auth:** JWT (JSON Web Tokens)
- **Password Hashing:** BCrypt (using BCrypt.Net-Next)
- **Services:** Dedicated `JwtService`, `PermissionService`, `SecurityService`
- **Middleware:** Custom JWT and Exception Handling middleware.

## Infrastructure & Tools
- **CLI Tasks:** Makefile
- **Pre-commit Hooks:** Husky
- **API Documentation:** Swagger/OpenAPI with custom Auth extension.
- **Static Analysis:** Microsoft.CodeAnalysis.BannedApiAnalyzers (to enforce Guid.CreateVersion7)
