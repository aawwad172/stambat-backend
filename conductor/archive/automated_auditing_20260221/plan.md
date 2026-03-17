# Implementation Plan: Automated Auditing for Entities

## Phase 1: Interceptor Infrastructure
Implementing the EF Core Interceptor and registering it within the application lifecycle.

- [x] Task: Create `AuditingInterceptor.cs` in `src/Stambat.Infrastructure/Persistence/Interceptors/`.
    - Implement `SavingChangesAsync` and `SavingChanges` methods.
    - Logic should iterate through `ChangeTracker.Entries()`.
    - For `EntityState.Added`: Populate `ICreationAudit` fields (`CreatedAt`, `CreatedBy`).
    - For `EntityState.Modified`: Populate `IModificationAudit` fields (`UpdatedAt`, `UpdatedBy`).
    - Integrate `ICurrentUserService` to fetch the current user ID, falling back to `AuthSeedConstants.SystemUserId`.
- [x] Task: Register `AuditingInterceptor` in `DependencyInjection.cs`.
    - Add it as a Scoped service.
- [x] Task: Configure `ApplicationDbContext` to use the `AuditingInterceptor`.
    - Update `AddDbContext` in `DependencyInjection.cs` to include `.AddInterceptors(provider.GetRequiredService<AuditingInterceptor>())`.
- [x] Task: Conductor - User Manual Verification 'Interceptor Infrastructure' (Protocol in workflow.md)

## Phase 2: Refactoring Application Logic
Removing manual auditing logic from existing handlers to leverage the new automated system.

- [x] Task: Refactor `RegisterUserCommandHandler.cs`.
    - Remove manual assignments for `CreatedAt`, `CreatedBy`, `IsActive`, `IsDeleted`, `IsVerified`, etc., if they are now handled or should be handled by defaults/interceptor.
    - Specifically focus on the audit fields defined in `ICreationAudit` and `IModificationAudit`.
- [x] Task: Search for other handlers (e.g., `LogoutCommandHandler`, `RefreshTokenCommandHandler`) and remove manual audit field assignments.
- [x] Task: Conductor - User Manual Verification 'Refactoring Application Logic' (Protocol in workflow.md)

## Phase 3: Verification & Validation
Ensuring the automated auditing works as expected across different scenarios.

- [x] Task: Verify automatic auditing for User Registration.
    - Run the API and register a new user; check the database to ensure `CreatedAt` and `CreatedBy` (System User if no context) are set.
- [x] Task: Verify automatic auditing for Entity Updates.
    - Update an existing user or entity and ensure `UpdatedAt` and `UpdatedBy` are correctly populated.
- [x] Task: Verify System User fallback.
    - Ensure that operations performed without a logged-in user (like initial registration) correctly default to the System User ID.
- [x] Task: Conductor - User Manual Verification 'Verification & Validation' (Protocol in workflow.md)
