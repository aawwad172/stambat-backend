# Architecture Guide

## Project Structure

```
stambat-backend/
├── src/
│   ├── Stambat.Domain/           # Core domain — NO external dependencies
│   │   ├── Common/               # Base classes (e.g., BaseEntity)
│   │   ├── Constants/            # Domain constants
│   │   ├── Entities/             # Aggregate roots and entities
│   │   │   └── Identity/         # User, Role, UserRoleTenant, UserToken
│   │   ├── Enums/                # RolesEnum, PermissionsEnum, UserTokenType
│   │   ├── Exceptions/           # Domain exceptions
│   │   ├── Interfaces/           # Repository interfaces, domain service interfaces
│   │   └── ValueObjects/         # Value objects
│   │
│   ├── Stambat.Application/      # Application logic — depends on Domain only
│   │   ├── CQRS/
│   │   │   ├── Commands/         # Command records (input DTOs for writes)
│   │   │   ├── CommandHandlers/  # Command handler implementations
│   │   │   ├── Queries/          # Query records (input DTOs for reads)
│   │   │   ├── QueryHandlers/    # Query handler implementations
│   │   │   └── BaseHandler.cs    # Base handler with shared dependencies
│   │   ├── Services/             # Application services (IJwtService, etc.)
│   │   └── Utilities/            # Utility classes
│   │
│   ├── Stambat.Infrastructure/   # External concerns — depends on Domain & Application
│   │   ├── Configurations/       # EF Core entity configurations (Fluent API)
│   │   ├── Clients/              # External API clients
│   │   ├── Email/                # Email service implementation
│   │   ├── Migrations/           # EF Core migrations
│   │   ├── Pagination/           # Pagination query extensions
│   │   └── Persistence/          # DbContext, Repositories, UnitOfWork
│   │
│   └── Stambat.WebAPI/           # Presentation — depends on all layers (for DI)
│       ├── Configurations/       # App configuration (JWT config, etc.)
│       ├── Endpoints/            # Minimal API endpoint modules
│       ├── Interfaces/           # Presentation-level interfaces
│       ├── Middlewares/          # Exception handling, JWT middleware
│       ├── Models/               # API request/response models
│       ├── Routes/               # Route mapping
│       ├── Validators/           # Request validators (FluentValidation?)
│       └── Validators/           # (Legacy) Request validators (FluentValidation?)
│
├── docs/                         # Business rules documentation
├── conductor/                    # Orchestration config (Primary Docs for AI)
├── scripts/                      # Utility scripts
├── Makefile                      # Common dev commands
├── docker-compose.dev.yml        # Dev PostgreSQL container
└── stambat.sln                   # Solution file
```

## Domain Entities

### Core Entities
- **Tenant** — A business using the platform
- **TenantProfile** — Business profile information
- **CardTemplate** — Loyalty card template definition (stamps required, reward)
- **WalletPass** — Individual digital card in a user's wallet
- **StampTransaction** — Record of a stamping action
- **Invitation** — Staff/user invitation

### Identity Entities (in `Entities/Identity/`)
- **User** — System user
- **UserRoleTenant** — Many-to-many linking user, role, and tenant
- **UserToken** — Token storage (refresh tokens, etc.)

## Dependency Flow (strict)
```
Domain ← Application ← Infrastructure
                      ← WebAPI (for DI wiring)
```
- **Domain** depends on NOTHING external
- **Application** depends on Domain only
- **Infrastructure** depends on Domain + Application
- **WebAPI** references all layers but only for DI registration

## Key Patterns

### Rich Domain Model
- Domain entities contain business logic (methods, validations)
- Entities are NOT anemic — they encapsulate behavior
- Use factory methods or constructors for creation logic
- EF Core change tracking considerations: new entities added to loaded collections should be tracked as `Added` not `Modified`

### CQRS with MediatR
- Commands = write operations (returns void or simple result)
- Queries = read operations (returns data)
- Each has a dedicated handler
- All handlers inherit from `BaseHandler.cs`

### Repository + Unit of Work
- Generic repository pattern in Infrastructure
- Unit of Work for transaction management
- Repository interfaces defined in Domain

### Minimal API Endpoints
- Uses endpoint modules (e.g., `AuthenticationModule.cs`, `TenantModule.cs`)
- NOT traditional controllers
- Endpoints registered via `EndpointExtensions.cs`

### EF Core Configurations
- Fluent API configurations in `Infrastructure/Configurations/`
- One configuration file per entity
- Code-first migrations
