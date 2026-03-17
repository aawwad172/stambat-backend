# Plan: Update Security to use BCrypt

## Phase 1: Preparation & Dependency [checkpoint: ab29dc6]
- [x] Task: Add BCrypt.Net-Next dependency (0cc9abc)
    - [x] Identify the correct project file (`Stambat.Application.csproj`).
    - [x] Install the `BCrypt.Net-Next` package using `dotnet add package`.
- [x] Task: Conductor - User Manual Verification 'Preparation & Dependency' (Protocol in workflow.md)

## Phase 2: Implementation & Refactoring [checkpoint: f2d26c6]
- [x] Task: Update SecurityService internal logic (3e7b0e6)
    - [x] Update hashing method to use `BCrypt.HashPassword`.
    - [x] Update verification method to use `BCrypt.Verify`.
- [x] Task: Clean up manual security code (4780461)
    - [x] Identify and remove manual hashing logic in `SecurityUtilities.cs`.
    - [x] Remove any unused helper methods related to the old hashing scheme.
- [x] Task: Verify CommandHandler integration (c201fc9)
    - [x] Ensure `RegisterUserCommandHandler` correctly uses the updated `SecurityService`.
    - [x] Ensure `LoginCommandHandler` correctly uses the updated `SecurityService`.
- [x] Task: Conductor - User Manual Verification 'Implementation & Refactoring' (Protocol in workflow.md)
