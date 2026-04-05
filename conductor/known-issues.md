# Known Issues & Gotchas

## EF Core Change Tracking
- **Issue:** When adding a new entity (e.g., `UserRoleTenant`) to a collection of a database-loaded entity, EF Core may mark it as `Modified` instead of `Added`.
- **Cause:** Navigation property fixup can cause change tracker confusion.
- **Workaround:** Be mindful when adding to navigation collections. Consider setting state explicitly if needed.

## macOS + .NET Aspire
- **Issue:** 502 Bad Gateway and 500 errors when running through .NET Aspire on macOS
- **Cause:** Protocol mismatches and port binding conflicts between macOS and Aspire DCP proxy
- **Note:** Development currently runs without Aspire using `make run`

## BannedSymbols.txt
- Certain APIs are banned project-wide via Roslyn BannedApiAnalyzers
- Check `BannedSymbols.txt` in the project root for the current list
- Violations are treated as errors (`RS0030` is in `WarningsAsErrors`)
