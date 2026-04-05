# Git Workflow & Commit Guidelines

## Branch Strategy

### Branch Naming Convention
- `feat/<feature-name>` — New features (e.g., `feat/staff-management`)
- `feature/<feature-name>` — Also acceptable for features
- `refactor/<description>` — Refactoring work (e.g., `refactor/moving-to-rich-DDD`)
- `enhancement/<description>` — Improvements (e.g., `enhancement/using-bcrypt`)
- `docs/<description>` — Documentation changes (e.g., `docs/updating-readme`)
- `fix/<description>` — Bug fixes
- `hotfix/<description>` — Urgent production fixes

### Workflow
1. Create a new branch from `main`
2. Make changes and commit following the conventions below
3. Push branch to origin
4. Create a Pull Request to `main`
5. Merge via PR (not direct push to main)

## Commit Message Format
- Descriptive and in lowercase
- Describe what was done (e.g., "finishing active staff fetching")
- Keep messages concise but meaningful

## Merge Strategy
- **Squash all commits on the feature branch before merging into `main`**
- Use `git rebase -i` to squash all feature commits into a single commit on the branch
- Then merge the clean single-commit branch into `main`
- This results in one clean commit per feature, making cherry-picking easy
- The squash commit message should summarize the entire feature

### How to Squash Before Merging
```bash
# 1. On your feature branch, interactive rebase against main
git rebase -i main

# 2. In the editor, mark all commits except the first as "squash" (or "s")
# 3. Write a final commit message summarizing the feature
# 4. Push (force push since history was rewritten)
git push origin feat/<feature-name> --force-with-lease

# 5. Now create/merge the PR into main
```

## Pre-Commit Hooks (Husky.Net)
The project uses Husky.Net for automated checks:

### On Commit (`pre-commit`):
- **`dotnet format`** — Automatically formats staged `.cs` files

### On Push (`pre-push`):
- **`dotnet clean`** — Cleans build artifacts
- **`dotnet build /p:TreatWarningsAsErrors=true`** — Ensures no warnings exist

## How to Commit (Step by Step)

```bash
# 1. Stage your changes
git add <files>
# or stage all changes
git add .

# 2. Commit (Husky pre-commit hook will auto-format)
git commit -m "your descriptive commit message"

# 3. Push (Husky pre-push hook will clean + build with warnings-as-errors)
git push origin <branch-name>
```

## Common Scenarios

### Creating a New Feature Branch
```bash
git checkout main
git pull origin main
git checkout -b feat/<feature-name>
```

### After Making Changes
```bash
git add .
git commit -m "description of changes"
git push origin feat/<feature-name>
```

### If Pre-Push Hook Fails
```bash
# Fix any build warnings/errors first
dotnet build /p:TreatWarningsAsErrors=true
# Fix issues, then try pushing again
git push origin <branch-name>
```

## Important Notes
- **Never force push to `main`**
- **Always go through PRs for merging to `main`**
- The pre-commit hook runs `dotnet husky run` which triggers the task-runner.json tasks
- If you need to temporarily skip hooks: `git commit --no-verify` (use sparingly)
