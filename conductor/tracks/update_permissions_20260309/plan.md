# Implementation Plan: Replace Template Permissions with Project-Specific Permissions

## Phase 1: Cleanup and Preparation [checkpoint: c3098d3]
- [x] Task: Remove existing template permissions from domain layer [12e4baf]
    - [x] Remove `UserRead` and `PostApprove` from `PermissionConstants.cs` [12e4baf]
- [x] Task: Remove existing template permissions from infrastructure layer [12e4baf]
    - [x] Remove `PermissionIdUserRead` and `PermissionIdPostApprove` from `AuthSeedConstants.cs` [12e4baf]
- [x] Task: Conductor - User Manual Verification 'Cleanup and Preparation' (Protocol in workflow.md) [c3098d3]

## Phase 2: Domain Layer Update [checkpoint: fd64a64]
- [x] Task: Define new permission string constants in `PermissionConstants.cs` [8b136f9]
    - [x] Add `Tenants.View`, `Tenants.Add`, `Tenants.Edit`, `Tenants.Delete` [8b136f9]
    - [x] Add `Users.View`, `Users.Add`, `Users.Edit`, `Users.Delete` [8b136f9]
    - [x] Add `Invitations.View`, `Invitations.Add`, `Invitations.Edit`, `Invitations.Delete` [8b136f9]
    - [x] Add `Cards.View`, `Cards.Add`, `Cards.Edit`, `Cards.Delete` [8b136f9]
    - [x] Add `Rewards.View`, `Rewards.Add`, `Rewards.Edit`, `Rewards.Delete` [8b136f9]
    - [x] Add `Scan.Stamping`, `Scan.Redeem` [8b136f9]
- [x] Task: Conductor - User Manual Verification 'Domain Layer Update' (Protocol in workflow.md) [fd64a64]

## Phase 3: Infrastructure Layer Update [checkpoint: 41b682c]
- [x] Task: Generate and add new Version 7 GUIDs to `AuthSeedConstants.cs` [20df43f]
    - [x] Add GUIDs for all new permissions defined in Phase 2 [20df43f]
- [x] Task: Update `PermissionsSeed.cs` with the new permissions [2f3232d]
    - [x] Replace existing data with the full list of project-specific permissions [2f3232d]
- [x] Task: Conductor - User Manual Verification 'Infrastructure Layer Update' (Protocol in workflow.md) [41b682c]

## Phase 4: Final Verification
- [x] Task: Build the solution to ensure no compilation errors [98c0598]
- [x] Task: Verify that all permissions are correctly seeded (manually or via a script) [98c0598]
- [x] Task: Conductor - User Manual Verification 'Final Verification' (Protocol in workflow.md) [41b682c]

## Phase 5: Super Admin Permissions
- [x] Task: Define Super Admin permission constants in `PermissionConstants.cs` [e80ffdf]
    - [x] Add `System.Manage`, `System.Logs.View`, `System.Audit.View`, `System.Settings.Edit`, `Tenants.Manage` [e80ffdf]
- [x] Task: Generate and add GUIDs for Super Admin permissions in `AuthSeedConstants.cs` [8f1f13f]
- [x] Task: Update `PermissionsSeed.cs` with Super Admin permissions [08927df]
- [x] Task: Conductor - User Manual Verification 'Super Admin Permissions' (Protocol in workflow.md) [41b682c]

## Phase 6: Roles and Permission Assignments [checkpoint: 72e2416]
- [x] Task: Create `RolesEnum` in Domain layer [db8bfe5]
    - [x] Add `SuperAdmin`, `TenantAdmin`, `Merchant`, `User` [db8bfe5]
- [x] Task: Update `AuthSeedConstants.cs` with new Role GUIDs (v7) [db8bfe5]
- [x] Task: Update `RolesSeed.cs` with project-specific roles [db8bfe5]
- [x] Task: Update `RolesPermissionsSeeding.cs` with role-permission mappings [db8bfe5]
- [x] Task: Conductor - User Manual Verification 'Roles and Assignments' (Protocol in workflow.md) [72e2416]

## Phase 7: Refactor Handlers to use RolesEnum
- [x] Task: Update `RegisterUserCommandHandler.cs` to use `RolesEnum` [8b4f1e1]
- [x] Task: Update `SetupTenantCommandHandler.cs` to use `RolesEnum` [f8a5f81]
- [x] Task: Conductor - User Manual Verification 'Refactor Handlers' (Protocol in workflow.md) [41b682c]

## Phase 8: Endpoint Protection and Policy Cleanup
- [x] Task: Clean up outdated authorization policies in `Program.cs` [2531238]
- [x] Task: Add authorization policies for new permissions in `DependencyInjection.cs` [2531238]
- [x] Task: Protect `SetupTenant` endpoint with `Tenants.Add` permission [2531238]
- [x] Task: Assign `Tenants.Add` permission to `User` role in `RolesPermissionsSeeding.cs` [2531238]
- [x] Task: Conductor - User Manual Verification 'Endpoint Protection' (Protocol in workflow.md) [41b682c]

## Phase 9: Refine Tenant Setup Permission [checkpoint: b181ed0]
- [x] Task: Add `Tenants.Setup` constant to `PermissionConstants.cs` [c9aa64a]
- [x] Task: Generate and add GUID for `Tenants.Setup` in `AuthSeedConstants.cs` [c9aa64a]
- [x] Task: Update `PermissionsSeed.cs` with `Tenants.Setup` [c9aa64a]
- [x] Task: Assign `Tenants.Setup` to `User` and `SuperAdmin` roles in `RolesPermissionsSeeding.cs` [c9aa64a]
- [x] Task: Update `SetupTenant` endpoint protection to use `Tenants.Setup` [c9aa64a]
- [x] Task: Conductor - User Manual Verification 'Tenants.Setup' (Protocol in workflow.md) [b181ed0]
