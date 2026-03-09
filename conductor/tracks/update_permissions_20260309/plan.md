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
- [ ] Task: Build the solution to ensure no compilation errors
- [ ] Task: Verify that all permissions are correctly seeded (manually or via a script)
- [ ] Task: Conductor - User Manual Verification 'Final Verification' (Protocol in workflow.md)
