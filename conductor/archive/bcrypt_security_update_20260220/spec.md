# Specification: Update Security to use BCrypt

## Overview
This track aims to replace the manual and potentially unreliable security code (password hashing and verification) with the well-tested and industry-standard `BCrypt.Net-Next` library. This will improve the security and reliability of the Stambat Digital Loyalty System while reducing custom codebase maintenance. The existing `SecurityService` will be preserved, but its internal implementation will be swapped out for BCrypt.

## Scope
- **Target Files:** `SecurityService.cs` (logic update), `SecurityUtilities.cs` (cleanup), and related `CommandHandlers` (Registration/Login).
- **Core Action:** Replace custom hashing/verification logic with `BCrypt.Net-Next`.
- **Cleanup:** Remove all unused manual security utility methods and legacy hashing code from the project.
- **Exclusion:** No legacy password migration is required as the project is in a fresh state.

## Functional Requirements
- **Secure Registration:** Update the registration flow to use `BCrypt.HashPassword`.
- **Reliable Login:** Update the login flow to use `BCrypt.Verify`.
- **Preserved Interface:** Maintain the `ISecurityService` interface to minimize breaking changes across the application.

## Acceptance Criteria
- New users can register, and their passwords are hashed using BCrypt.
- Users can log in by having their provided password verified against the BCrypt-hashed password.
- All manual hashing and verification code is removed from the project.
- No compilation errors occur after the library integration and code update.
- The `BCrypt.Net-Next` package is correctly added as a dependency to the `Stambat.Application` project.
