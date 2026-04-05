# Tech Stack

## Core
- **Language:** C# (.NET 10, SDK 10.0.101)
- **Framework:** ASP.NET Core Minimal API (no controllers — uses endpoint modules)
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core (Code-First with Migrations)

## Architecture
- **Pattern:** Domain-Driven Design (DDD) + Clean Architecture
- **CQRS:** Command/Query Responsibility Segregation via MediatR
- **Layers:**
  - `Stambat.Domain` — Entities, Value Objects, Enums, Domain Interfaces
  - `Stambat.Application` — CQRS Commands/Queries/Handlers, Application Services
  - `Stambat.Infrastructure` — EF Core, Repositories, External Services, Configurations
  - `Stambat.WebAPI` — Endpoints, Middleware, Validators, DI setup

## Authentication & Security
- **Auth:** JWT-based authentication
- **Encryption:** Custom encryption key for sensitive data
- **Authorization:** Role-based + Permission-based (granular permissions via `PermissionsEnum`)

## Key Libraries & Tools
- MediatR (CQRS)
- FluentValidation (request validation in WebAPI layer)
- BCrypt (password hashing)
- JWT (authentication tokens)
- EF Core (PostgreSQL provider — Npgsql)
- EF Core Tools (dotnet-ef, installed locally via `make install-dotnet-ef`)
- Husky.Net (pre-commit hooks)
- BannedApiAnalyzers (code quality — Roslyn analyzer)
- Docker Compose (dev PostgreSQL database)

## Dev Environment
- **OS:** macOS
- **IDE:** JetBrains Rider / VS Code
- **Build:** `dotnet build` / `make build`
- **Run:** `dotnet run` or `make run` / `make watch`
