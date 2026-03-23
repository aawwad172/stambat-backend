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
    - [x] Implement `AssignRole(Guid roleId, Guid? tenantId)` with the specified logic.
    - [x] Implement `AddRefreshToken(string token, DateTime expiry)` to manage the `RefreshTokens` collection internally.
    - [x] Implement `RevokeRefreshToken(string token)` to mark a token as revoked.
    - [x] Implement `AddUserToken(UserTokenType type, string value, DateTime expiry)` to manage the `UserTokens` collection.
2.  - [x] Task: Conductor - User Manual Verification 'Phase 2: Domain Logic Implementation' (Protocol in workflow.md)

## Phase 3: Infrastructure and Repository Consolidation [checkpoint: e406b2f]
1.  - [x] Task: Consolidate Repositories
    - [x] Remove `IUserRoleTenantRepository`, `IUserTokenRepository`, and `IRefreshTokenRepository` interfaces and implementations.
    - [x] Update `IUserRepository` to be the sole entry point for persisting the `User` aggregate and its children.
    - [x] Update `UserRepository` implementation to use `.Include(...)` for `RefreshTokens`, `UserTokens`, and `UserRoleTenants`.
2.  - [x] Task: Update Dependency Injection
    - [x] Remove registrations for deleted repositories in `Stambat.Infrastructure.DependencyInjection`.
3.  - [x] Task: Conductor - User Manual Verification 'Phase 3: Infrastructure and Repository Consolidation' (Protocol in workflow.md)

## Phase 4: Application Layer Refactoring
1.  - [x] Task: Refactor Handlers - Registration and Setup
    - [x] Update `RegisterHandler` (or equivalent) to use `User.Create`.
    - [x] Update `SetupTenantHandler` to use domain methods.
2.  - [x] Task: Refactor Handlers - Authentication
    - [x] Update `LoginHandler` to use `AddRefreshToken`.
    - [x] Update `LogoutHandler` to use `RevokeRefreshToken`.
    - [x] Update `RefreshTokenHandler` to use domain methods.
3.  - [x] Task: Refactor Handlers - Identity Management
    - [x] Update `InviteStaffHandler` and `AcceptInvitationHandler` to use `AssignRole`.
    - [x] Update `VerifyEmailHandler` to use `VerifyEmail` and `AddUserToken`/`UseUserToken` logic.
4.  - [x] Task: Conductor - User Manual Verification 'Phase 4: Application Layer Refactoring' (Protocol in workflow.md)

## Phase 5: Verification and Cleanup
1.  - [x] Task: Final System Verification
    - [x] Ensure all handlers compile and tests pass.
    - [x] Verify no direct manipulation of Identity collections remains in the Application layer.
    - [x] Refactor `Role`, `Permission`, and `RolePermission` to rich domain models.
2.  - [x] Task: Conductor - User Manual Verification 'Phase 5: Verification and Cleanup' (Protocol in workflow.md)
