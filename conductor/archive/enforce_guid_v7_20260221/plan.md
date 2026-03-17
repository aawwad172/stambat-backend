# Implementation Plan: Enforce Guid.CreateVersion7

## Phase 1: Global Build Configuration
Setting up the infrastructure to detect and block `Guid.NewGuid()`.

- [x] Task: Create `Directory.Build.props` in the root directory. 0ca702c
    - Configure it to include the `Microsoft.CodeAnalysis.BannedApiAnalyzers` package for all projects.
    - Set `EnforceCodeStyleInBuild` to `true`.
- [x] Task: Create `BannedSymbols.txt` in the root directory. 0ca702c
    - Add `M:System.Guid.NewGuid;Use Guid.CreateVersion7() instead` to the file.
- [x] Task: Update project files or `Directory.Build.props` to ensure `BannedSymbols.txt` is treated as an `AdditionalFiles` item for the analyzer. 0ca702c
- [~] Task: Conductor - User Manual Verification 'Global Build Configuration' (Protocol in workflow.md)

## Phase 2: Codebase Refactoring
Replacing all existing occurrences of `Guid.NewGuid()` and `Guid.CreateVersion7()` with a central `Id.New()` wrapper.

- [x] Task: Create static `Id` class in `Stambat.Domain.Common`. 9311a76
- [x] Task: Search the entire solution for `Guid.NewGuid()` and `Guid.CreateVersion7()`. 9311a76
- [x] Task: Replace all found occurrences with `Id.New()`. 9311a76
    - Ensure necessary `using Stambat.Domain.Common;` is used.
- [x] Task: Conductor - User Manual Verification 'Codebase Refactoring' (Protocol in workflow.md)

## Phase 3: Final Validation
Confirming the enforcement is active and the build is clean.

- [x] Task: Attempt to build the solution (`dotnet build`).
    - Verify it builds successfully after refactoring.
- [x] Task: Temporarily re-introduce one instance of `Guid.NewGuid()` to verify that the build fails as expected.
- [x] Task: Conductor - User Manual Verification 'Final Validation' (Protocol in workflow.md)
