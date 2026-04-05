# Common Commands

## Development

| Command | Description |
|---------|-------------|
| `make run` | Run the WebAPI project |
| `make watch` | Run with hot-reload |
| `make build` | Build the solution |
| `make restore` | Restore NuGet packages |
| `make clean` | Clean build artifacts |

## Database

| Command | Description |
|---------|-------------|
| `make dev-db-start` | Start PostgreSQL in Docker |
| `make dev-db-stop` | Stop PostgreSQL Docker container |
| `make migrate name=<Name>` | Create a new EF Core migration |
| `make migrate-remove` | Remove the last migration |
| `make db-update` | Apply pending migrations |

## Tools

| Command | Description |
|---------|-------------|
| `make install-dotnet-ef` | Install EF Core tools locally |
| `make restore-tool` | Restore local .NET tools |
| `dotnet format` | Format code (also runs on pre-commit via Husky) |

## Docker

```bash
# Dev database (PostgreSQL)
docker compose -f docker-compose.dev.yml up -d    # Start
docker compose -f docker-compose.dev.yml down      # Stop
```

## Configuration Files
- `appsettings.json` — Base configuration
- `appsettings.Development.json` — Development overrides
- `appsettings.Staging.json` — Staging overrides
- `.env` — Environment variables (gitignored)
