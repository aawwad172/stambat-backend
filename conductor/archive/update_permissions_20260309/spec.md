# Specification: Replace Template Permissions with Project-Specific Permissions

## Overview
This track involves a complete overhaul of the permission system in the Stambat backend. We are moving away from the generic template permissions (`User.Read`, `Post.Approve`) to a granular, project-specific set of permissions that align with the core entities: Tenants, Users, Invitations, Loyalty Cards, Rewards, and Scan actions.

## Functional Requirements
1. **Remove Template Permissions:** Delete all existing references to `User.Read` and `Post.Approve` from:
   - `PermissionConstants` (Domain layer)
   - `AuthSeedConstants` (Infrastructure layer)
   - `PermissionsSeed` (Infrastructure layer)
2. **Define New Permission Constants:** Add new string constants in `PermissionConstants` using dot notation.
3. **Define New Permission GUIDs:** Add corresponding unique static GUIDs (Version 7) in `AuthSeedConstants`.
4. **Update Seeding Logic:** Update `PermissionsSeed` to insert the new permissions into the database.
5. **Categorize Permissions:** Provide View, Edit, Add, and Delete for each major entity, plus specialized permissions for scanning.

### Proposed Permissions List
| Category | Actions | Permission Strings |
| :--- | :--- | :--- |
| **Tenants** | View, Add, Edit, Delete, Setup | `Tenants.View`, `Tenants.Add`, `Tenants.Edit`, `Tenants.Delete`, `Tenants.Setup` |
| **Users** | View, Add, Edit, Delete | `Users.View`, `Users.Add`, `Users.Edit`, `Users.Delete` |
| **Invitations** | View, Add, Edit, Delete | `Invitations.View`, `Invitations.Add`, `Invitations.Edit`, `Invitations.Delete` |
| **Loyalty Cards** | View, Add, Edit, Delete | `Cards.View`, `Cards.Add`, `Cards.Edit`, `Cards.Delete` |
| **Rewards** | View, Add, Edit, Delete | `Rewards.View`, `Rewards.Add`, `Rewards.Edit`, `Rewards.Delete` |
| **Scan** | Stamping, Redeem | `Scan.Stamping`, `Scan.Redeem` |
| **System (SA)** | Manage, Logs, Audit, Settings | `System.Manage`, `System.Logs.View`, `System.Audit.View`, `System.Settings.Edit` |
| **Tenants (SA)** | Manage All Tenants | `Tenants.Manage` |

## Functional Requirements (Roles & Assignments)
1. **Define Roles Enum:** Create a `RolesEnum` in the Domain layer for type-safe role references.
2. **Define Role GUIDs:** Use Version 7 GUIDs for `SuperAdmin`, `TenantAdmin`, `Merchant`, and `User` roles.
3. **Update Roles Seed:** Replace existing roles with the new project-specific roles.
4. **Update Role-Permission Seeding:** Assign appropriate permissions to each role:
   - `SuperAdmin`: All permissions (including `Tenants.Setup`).
   - `TenantAdmin`: Management of Tenants (own), Users, Invitations, Cards, Rewards, and Scan.
   - `Merchant`: Viewing of Tenants, Users, Cards, Rewards, and Scan actions.
   - `User`: Viewing of Tenants, Cards, and Rewards, plus `Tenants.Setup`.

## Non-Functional Requirements
- **Consistency:** Ensure permission strings and GUIDs are consistently named across the project.
- **Maintainability:** Use static constants to avoid magic strings in the application logic.
- **GUID Standards:** Use Version 7 GUIDs to ensure consistency with project standards.

## Acceptance Criteria
- [ ] Existing `User.Read` and `Post.Approve` permissions are completely removed.
- [ ] New permissions for Tenants, Users, Invitations, Cards, Rewards, and Scan are defined in `PermissionConstants`.
- [ ] Corresponding GUIDs for all new permissions are defined in `AuthSeedConstants` (using Version 7 GUIDs).
- [ ] `PermissionsSeed.cs` correctly seeds all new permissions.
- [ ] Project compiles without errors.

## Out of Scope
- Assigning these new permissions to specific roles (this will be handled in a subsequent track).
- Modifying the `Permission` entity structure itself.
