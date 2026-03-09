# Implementation Plan: Replace Template Permissions with Project-Specific Permissions

## Phase 1: Cleanup and Preparation
- [ ] Task: Remove existing template permissions from domain layer
    - [ ] Remove `UserRead` and `PostApprove` from `PermissionConstants.cs`
- [ ] Task: Remove existing template permissions from infrastructure layer
    - [ ] Remove `PermissionIdUserRead` and `PermissionIdPostApprove` from `AuthSeedConstants.cs`
- [ ] Task: Conductor - User Manual Verification 'Cleanup and Preparation' (Protocol in workflow.md)

## Phase 2: Domain Layer Update
- [ ] Task: Define new permission string constants in `PermissionConstants.cs`
    - [ ] Add `Tenants.View`, `Tenants.Add`, `Tenants.Edit`, `Tenants.Delete`
    - [ ] Add `Users.View`, `Users.Add`, `Users.Edit`, `Users.Delete`
    - [ ] Add `Invitations.View`, `Invitations.Add`, `Invitations.Edit`, `Invitations.Delete`
    - [ ] Add `Cards.View`, `Cards.Add`, `Cards.Edit`, `Cards.Delete`
    - [ ] Add `Rewards.View`, `Rewards.Add`, `Rewards.Edit`, `Rewards.Delete`
    - [ ] Add `Scan.Stamping`, `Scan.Redeem`
- [ ] Task: Conductor - User Manual Verification 'Domain Layer Update' (Protocol in workflow.md)

## Phase 3: Infrastructure Layer Update
- [ ] Task: Generate and add new Version 7 GUIDs to `AuthSeedConstants.cs`
    - [ ] Add GUIDs for all new permissions defined in Phase 2
- [ ] Task: Update `PermissionsSeed.cs` with the new permissions
    - [ ] Replace existing data with the full list of project-specific permissions
- [ ] Task: Conductor - User Manual Verification 'Infrastructure Layer Update' (Protocol in workflow.md)

## Phase 4: Final Verification
- [ ] Task: Build the solution to ensure no compilation errors
- [ ] Task: Verify that all permissions are correctly seeded (manually or via a script)
- [ ] Task: Conductor - User Manual Verification 'Final Verification' (Protocol in workflow.md)
