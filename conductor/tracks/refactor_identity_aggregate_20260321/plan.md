# Implementation Plan: Refactor Identity Aggregate to Rich Domain Model

## Phase 1: Value Objects and Core Aggregate Foundation
1.  - [ ] Task: Implement Value Objects
    - [ ] Create `FullName` value object in `Stambat.Domain.ValueObjects`.
    - [ ] Create `Email` value object in `Stambat.Domain.ValueObjects`.
    - [ ] Ensure validation (not empty, trim, lowercase) is implemented in both.
2.  - [ ] Task: Update User Entity for Value Objects
    - [ ] Update `User.cs` to use `FullName` and `Email` types for its properties.
    - [ ] Update EF Core configurations in `Stambat.Infrastructure` to map these value objects (using `HasConversion` or similar).
3.  - [ ] Task: Encapsulate User Entity State
    - [ ] Make all property setters `private` or `init` in `User.cs`.
    - [ ] Remove public constructors and implement `public static User Create(...)`.
    - [ ] Ensure related entities (`RefreshToken`, `UserToken`, `UserRoleTenant`) also have encapsulated state and restricted constructors.
4.  - [ ] Task: Conductor - User Manual Verification 'Phase 1: Value Objects and Core Aggregate Foundation' (Protocol in workflow.md)

## Phase 2: Domain Logic Implementation
1.  - [ ] Task: Implement Domain Methods in User Aggregate
    - [ ] Implement `VerifyEmail()` method in `User`.
    - [ ] Implement `LinkToTenant(Guid tenantId, Guid roleId)` with the specified logic (Link/Update/Throw).
    - [ ] Implement `AddRefreshToken(string token, DateTime expiry)` to manage the `RefreshTokens` collection internally.
    - [ ] Implement `RevokeRefreshToken(string token)` to mark a token as revoked.
    - [ ] Implement `AddUserToken(UserTokenType type, string value, DateTime expiry)` to manage the `UserTokens` collection.
2.  - [ ] Task: Conductor - User Manual Verification 'Phase 2: Domain Logic Implementation' (Protocol in workflow.md)

## Phase 3: Infrastructure and Repository Consolidation
1.  - [ ] Task: Consolidate Repositories
    - [ ] Remove `IUserRoleTenantRepository`, `IUserTokenRepository`, and `IRefreshTokenRepository` interfaces and implementations.
    - [ ] Update `IUserRepository` to be the sole entry point for persisting the `User` aggregate and its children.
    - [ ] Update `UserRepository` implementation to use `.Include(...)` for `RefreshTokens`, `UserTokens`, and `UserRoleTenants`.
2.  - [ ] Task: Update Dependency Injection
    - [ ] Remove registrations for deleted repositories in `Stambat.Infrastructure.DependencyInjection`.
3.  - [ ] Task: Conductor - User Manual Verification 'Phase 3: Infrastructure and Repository Consolidation' (Protocol in workflow.md)

## Phase 4: Application Layer Refactoring
1.  - [ ] Task: Refactor Handlers - Registration and Setup
    - [ ] Update `RegisterHandler` (or equivalent) to use `User.Create`.
    - [ ] Update `SetupTenantHandler` to use domain methods.
2.  - [ ] Task: Refactor Handlers - Authentication
    - [ ] Update `LoginHandler` to use `AddRefreshToken`.
    - [ ] Update `LogoutHandler` to use `RevokeRefreshToken`.
    - [ ] Update `RefreshTokenHandler` to use domain methods.
3.  - [ ] Task: Refactor Handlers - Identity Management
    - [ ] Update `InviteStaffHandler` and `AcceptInvitationHandler` to use `LinkToTenant`.
    - [ ] Update `VerifyEmailHandler` to use `VerifyEmail` and `AddUserToken`/`UseUserToken` logic.
4.  - [ ] Task: Conductor - User Manual Verification 'Phase 4: Application Layer Refactoring' (Protocol in workflow.md)

## Phase 5: Verification and Cleanup
1.  - [ ] Task: Final System Verification
    - [ ] Ensure all handlers compile and tests pass.
    - [ ] Verify no direct manipulation of Identity collections remains in the Application layer.
2.  - [ ] Task: Conductor - User Manual Verification 'Phase 5: Verification and Cleanup' (Protocol in workflow.md)
