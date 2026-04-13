# Database Entity Relationship Diagram

> **⚠️ MAINTENANCE RULE:** This diagram MUST be updated whenever:
> - A new entity is added to `Stambat.Domain/Entities/`
> - A relationship between entities changes
> - A new FK or navigation property is added/removed
> - An entity configuration in `Infrastructure/Configurations/` is modified
>
> When updating, re-read the relevant entity files and configuration files to ensure accuracy.

## ER Diagram

```mermaid
erDiagram
    %% ==========================================
    %% IDENTITY & AUTHENTICATION
    %% ==========================================

    User {
        Guid Id PK
        string FullName "ValueObject"
        string Username
        string Email "ValueObject, Unique"
        Guid UserCredentialsId FK "nullable"
        string SecurityStamp
        bool IsActive
        bool IsDeleted
        bool IsVerified
        DateTime CreatedAt
        Guid CreatedBy
        DateTime UpdatedAt "nullable"
        Guid UpdatedBy "nullable"
    }

    UserCredentials {
        Guid Id PK
        string PasswordHash "BCrypt"
        Guid UserId FK
    }

    RefreshToken {
        Guid Id PK
        Guid UserId FK
        Guid TokenFamilyId
        string TokenHash
        string PlaintextToken "not persisted"
        DateTime ExpiresAt
        DateTime RevokedAt "nullable"
        string ReasonRevoked "nullable"
        Guid ReplacedByTokenId FK "nullable, self-ref"
        string SecurityStampAtIssue "nullable"
        DateTime CreatedAt
        Guid CreatedBy
    }

    UserToken {
        Guid Id PK
        Guid UserId FK
        string Token
        UserTokenType Type "enum"
        DateTime ExpiryDate
        bool IsUsed
    }

    Role {
        Guid Id PK
        string Name "Unique, max 100"
        string Description "nullable, max 256"
        DateTime CreatedAt
        Guid CreatedBy
        DateTime UpdatedAt "nullable"
        Guid UpdatedBy "nullable"
        bool IsDeleted
    }

    Permission {
        Guid Id PK
        string Name "Unique, e.g. Users.Read"
        string Description "nullable"
        DateTime CreatedAt
        Guid CreatedBy
        DateTime UpdatedAt "nullable"
        Guid UpdatedBy "nullable"
        bool IsDeleted
    }

    RolePermission {
        Guid RoleId PK_FK
        Guid PermissionId PK_FK
    }

    UserRoleTenant {
        Guid Id PK
        Guid UserId FK
        Guid RoleId FK
        Guid TenantId FK "nullable - null for SuperAdmin"
    }

    %% ==========================================
    %% BUSINESS / TENANT
    %% ==========================================

    Tenant {
        Guid Id PK
        string BusinessName "max 100"
        string Email "ValueObject, Unique"
        Guid TenantProfileId FK "nullable"
        bool IsActive "default true"
        string TimeZoneId "default Asia/Amman"
        string CurrencyCode "default JOD, max 3"
        DateTime CreatedAt
        Guid CreatedBy
        DateTime UpdatedAt "nullable"
        Guid UpdatedBy "nullable"
        bool IsDeleted
    }

    TenantProfile {
        Guid Id PK
        string Slug "Unique index"
        string LogoUrl "nullable"
        string PrimaryColor "default #000000"
        string SecondaryColor "default #FFFFFF"
        Guid TenantId FK
        DateTime CreatedAt
        Guid CreatedBy
        DateTime UpdatedAt "nullable"
        Guid UpdatedBy "nullable"
        bool IsDeleted
    }

    %% ==========================================
    %% LOYALTY SYSTEM
    %% ==========================================

    CardTemplate {
        Guid Id PK
        Guid TenantId FK
        string Title
        string Description "nullable"
        decimal RequiredBalance "goal for stamps or points"
        string RewardDescription "nullable"
        CardType CardType "default Standard"
        int ExpiryDurationInDays "nullable"
        RedemptionType RedemptionType "default Stamps"
        decimal PointsPerCurrencyUnit "nullable, for Points cards"
        string PrimaryColorOverride "nullable"
        string SecondaryColorOverride "nullable"
        string LogoUrlOverride "nullable"
        string EmptyStampUrl "nullable"
        string EarnedStampUrl "nullable"
        string TermsAndConditions "nullable"
        bool IsActive "default true"
        DateTime CreatedAt
        Guid CreatedBy
        DateTime UpdatedAt "nullable"
        Guid UpdatedBy "nullable"
        bool IsDeleted
    }

    WalletPass {
        Guid Id PK
        Guid UserId FK
        Guid CardTemplateId FK
        decimal CurrentBalance "default 0, concurrency token"
        decimal RequiredBalance "snapshot from CardTemplate"
        RedemptionType RedemptionType "default Stamps, snapshot"
        WalletPassStatus Status "default Active"
        DateTime ExpiresAt "nullable"
        string ApplePassSerialNumber "nullable, max 100"
        string GooglePayId "nullable, max 100"
        string QrTokenPayload "nullable, max 500"
        DateTime LastProgressAt "nullable"
        DateTime RedeemedAt "nullable"
        DateTime CreatedAt
        Guid CreatedBy
        DateTime UpdatedAt "nullable"
        Guid UpdatedBy "nullable"
        bool IsDeleted
    }

    StampTransaction {
        Guid Id PK
        Guid WalletPassId FK
        Guid MerchantId FK "User who scanned"
        StampTransactionType Type "default Stamp"
        decimal AmountAdded "stamps count or points earned"
        string Note "nullable"
        DateTime CreatedAt
        Guid CreatedBy
        DateTime UpdatedAt "nullable"
        Guid UpdatedBy "nullable"
        bool IsDeleted
    }

    Invitation {
        Guid Id PK
        string Email
        string TokenHash "stored in DB"
        Guid TenantId FK "nullable - null for new signup"
        Guid RoleId FK
        DateTime ExpiresAt
        bool IsUsed
        DateTime CreatedAt
        Guid CreatedBy
        DateTime UpdatedAt "nullable"
        Guid UpdatedBy "nullable"
        bool IsDeleted
    }

    %% ==========================================
    %% RELATIONSHIPS
    %% ==========================================

    %% Identity
    User ||--o| UserCredentials : "has credentials"
    User ||--o{ RefreshToken : "has refresh tokens"
    User ||--o{ UserToken : "has user tokens"
    User ||--o{ UserRoleTenant : "assigned roles"
    User ||--o{ WalletPass : "owns passes"

    %% Authentication & Authorization
    Role ||--o{ UserRoleTenant : "assigned to users"
    Role }o--o{ Permission : "has permissions (via RolePermission)"
    RolePermission }o--|| Role : "links"
    RolePermission }o--|| Permission : "links"

    %% Tenant
    Tenant ||--o| TenantProfile : "has profile"
    Tenant ||--o{ UserRoleTenant : "has members"
    Tenant ||--o{ CardTemplate : "owns templates"
    Tenant ||--o{ Invitation : "has invitations"

    %% UserRoleTenant (3-way join)
    UserRoleTenant }o--o| Tenant : "scoped to tenant (nullable)"

    %% Loyalty
    CardTemplate ||--o{ WalletPass : "issued passes"
    WalletPass ||--o{ StampTransaction : "has transactions"
    StampTransaction }o--|| User : "stamped by merchant"

    %% Invitation
    Invitation }o--|| Role : "for role"

    %% RefreshToken self-reference
    RefreshToken |o--o| RefreshToken : "replaced by"
```

## Relationship Summary

| Relationship | Type | Notes |
|---|---|---|
| User → UserCredentials | 1:0..1 | Optional, FK on UserCredentials |
| User → RefreshToken | 1:N | Cascade delete |
| User → UserToken | 1:N | Email verification, password reset tokens |
| User → UserRoleTenant | 1:N | User's role assignments per tenant |
| User → WalletPass | 1:N | Customer's loyalty passes |
| Role → UserRoleTenant | 1:N | Which users have this role |
| Role ↔ Permission | M:N | Via `RolePermission` join table (composite PK) |
| Tenant → TenantProfile | 1:0..1 | Optional profile, FK on TenantProfile |
| Tenant → UserRoleTenant | 1:N | Cascade delete |
| Tenant → CardTemplate | 1:N | Business's loyalty card templates |
| Tenant → Invitation | 1:N | Staff invitations |
| CardTemplate → WalletPass | 1:N | Passes issued from this template |
| WalletPass → StampTransaction | 1:N | Stamp history for a pass |
| StampTransaction → User (Merchant) | N:1 | The staff who performed the scan |
| Invitation → Role | N:1 | Role to assign on acceptance |
| RefreshToken → RefreshToken | 0..1:0..1 | Token rotation chain (self-referencing) |

## Key Constraints

- **UserRoleTenant:** Two filtered unique indexes:
  - `(UserId, RoleId, TenantId)` UNIQUE where `TenantId IS NOT NULL`
  - `(UserId, RoleId)` UNIQUE where `TenantId IS NULL` (super admins)
- **TenantProfile.Slug:** Unique index
- **Tenant.Email:** Unique index
- **Role.Name:** Unique index
- **User.Email:** Value object with unique constraint
- **RolePermission:** Composite PK `(RoleId, PermissionId)`
