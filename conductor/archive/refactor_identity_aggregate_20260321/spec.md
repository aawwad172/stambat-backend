# Track Specification: Refactor Identity Aggregate to Rich Domain Model

## Overview
This track focuses on refactoring the Identity system into a rich domain model using DDD principles. The primary goal is to shift logic from handlers and services into the `User` aggregate root, ensuring that all state changes and consistency rules are handled internally.

## Functional Requirements

### 1. User Aggregate Root Refactoring
- **Encapsulation:** All property setters in the `User` entity must be changed to `private` or `init`.
- **Static Factory:** Implement `public static User Create(...)` to handle initial state validation and creation. Public constructors should be removed.
- **Domain Methods:**
  - `VerifyEmail()`: Sets `IsVerified = true` and updates the security stamp.
  - `LinkToTenant(Guid tenantId, Guid roleId)`:
    - If user is not linked to the tenant, add a new `UserRoleTenant`.
    - If user is linked to the tenant with a different role, update the role.
    - If user is already linked with the same role, throw a `ConflictException`.
  - `AddRefreshToken(string token, DateTime expiry)`: Add a new `RefreshToken` to the internal collection.
  - `RevokeRefreshToken(string token)`: Find the token in the collection and mark it as revoked (set `RevokedAt`).
  - `AddUserToken(UserTokenType type, string value, DateTime expiry)`: Manage the `UserToken` collection for general-purpose tokens (e.g., email verification).

### 2. Value Objects
- **FullName:** Handles validation and storage of the user's full name.
- **Email:** Handles validation and storage of the email address.
- **Rules:** Value objects must ensure data is not empty, trimmed, and always stored in lowercase.

### 3. Repository and Persistence
- **Repository Consolidation:** Remove `IUserRoleTenantRepository`, `IUserTokenRepository`, and `IRefreshTokenRepository` (if they exist).
- **Aggregate Root Focus:** `IUserRepository` becomes the only repository for managing the `User` aggregate and its children (`UserRoleTenant`, `RefreshToken`, `UserToken`).
- **Eager Loading:** Update `IUserRepository` implementation to use `.Include(...)` for all child collections to maintain consistency in memory.

### 4. Application Layer Refactoring
- **Handler Updates:** Refactor all handlers interacting with the User aggregate to use the new domain-driven approach:
  - `RegisterHandler` (or equivalent for user creation)
  - `LoginHandler`
  - `LogoutHandler`
  - `RefreshTokenHandler`
  - `SetupTenantHandler`
  - `InviteStaffHandler`
  - `AcceptInvitationHandler`
  - `VerifyEmailHandler`
  - Any queries that check verification status (e.g., `IsVerifiedQuery`).
  - Ensure handlers do not manipulate collections or related entities directly.

## Non-Functional Requirements
- **Consistency:** Ensure the aggregate root is always in a valid state.
- **Architecture:** Maintain strict separation between Domain, Application, and Infrastructure layers.

## Acceptance Criteria
- `User` entity has no public setters.
- `User.Create` is used for user registration.
- `FullName` and `Email` are implemented as value objects.
- All Identity-related logic (linking tenants, managing tokens) is encapsulated within the `User` entity.
- All listed handlers are simplified and only call domain methods on the `User` aggregate.
- Only `IUserRepository` is used to persist Identity-related changes.

## Out of Scope
- Refactoring of non-Identity entities (e.g., `TenantProfile`, `CardTemplate`).
- Database schema changes (except for column types if needed for value objects).
