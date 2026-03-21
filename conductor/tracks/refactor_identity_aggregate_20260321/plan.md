# Implementation Plan: Refactor Identity Aggregate to Rich Domain Model

## Phase 1: Value Objects and Core Aggregate Foundation [checkpoint: 1c3c8f9]
1.  - [x] Task: Implement Value Objects
    - [x] Create `FullName` value object in `Stambat.Domain.ValueObjects`.
    - [x] Create `Email` value object in `Stambat.Domain.ValueObjects`.
    - [x] Ensure validation (not empty, trim, lowercase) is implemented in both.
2.  - [x] Task: Update User Entity for Value Objects
    - [x] Update `User.cs` to use `FullName` and `Email` types for its properties.
    - [x] Update EF Core configurations in `Stambat.Infrastructure` to map these value objects (using `HasConversion` or similar).
3.  - [x] Task: Encapsulate User Entity State
    - [x] Make all property setters `private` or `init` in `User.cs`.
    - [x] Remove public constructors and implement `public static User Create(...)`.
    - [x] Ensure related entities (`RefreshToken`, `UserToken`, `UserRoleTenant`) also have encapsulated state and restricted constructors.
4.  - [x] Task: Conductor - User Manual Verification 'Phase 1: Value Objects and Core Aggregate Foundation' (Protocol in workflow.md)

## Phase 2: Domain Logic Implementation [checkpoint: 4a6ec2e]
1.  - [x] Task: Implement Domain Methods in User Aggregate
    - [x] Implement `VerifyEmail()` method in `User`.
    - [x] Implement `LinkToTenant(Guid tenantId, Guid roleId)` with the specified logic (Link/Update/Throw).
    - [x] Implement `AddRefreshToken(string token, DateTime expiry)` to manage the `RefreshTokens` collection internally.
    - [x] Implement `RevokeRefreshToken(string token)` to mark a token as revoked.
    - [x] Implement `AddUserToken(UserTokenType type, string value, DateTime expiry)` to manage the `UserTokens` collection.
2.  - [ ] Task: Conductor - User Manual Verification 'Phase 2: Domain Logic Implementation' (Protocol in workflow.md)

## Phase 3: Infrastructure and Repository Consolidation
1.  - [x] Task: Consolidate Repositories
    - [x] Remove `IUserRoleTenantRepository`, `IUserTokenRepository`, and `IRefreshTokenRepository` interfaces and implementations.
    - [x] Update `IUserRepository` to be the sole entry point for persisting the `User` aggregate and its children.
    - [x] Update `UserRepository` implementation to use `.Include(...)` for `RefreshTokens`, `UserTokens`, and `UserRoleTenants`.
2.  - [x] Task: Update Dependency Injection
    - [x] Remove registrations for deleted repositories in `Stambat.Infrastructure.DependencyInjection`.
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
