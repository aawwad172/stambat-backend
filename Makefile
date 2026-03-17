# Set default shell
SHELL := /bin/bash

rename-project:
	@echo "Making the Script Executable"
	chmod +x scripts/rename_dotnet_template.sh
	@echo "Executing rename script with name: $(name)"
	scripts/./rename_dotnet_template.sh $(name)

install-dotnet-ef:
	dotnet tool install --local dotnet-ef

restore-tool:
	dotnet tool restore

# Run the API project
run:
	dotnet run --project src/Stambat.Presentation.API

# Run the API project
watch:
	dotnet watch --project src/Stambat.Presentation.API

# Build the solution
build:
	dotnet build

# Restore NuGet packages
restore:
	dotnet restore

# Clean the project
clean:
	dotnet clean

# Create and apply database migrations (for EF Core)
migrate:
	dotnet ef migrations add $(name) --project src/Stambat.Infrastructure --startup-project src/Stambat.Presentation.API

migrate-remove:
	dotnet ef migrations remove --project src/Stambat.Infrastructure --startup-project src/Stambat.Presentation.API

db-update:
	dotnet ef database update --project src/Stambat.Infrastructure --startup-project src/Stambat.Presentation.API

dev-db-start:
	docker compose -f docker-compose.dev.yml up -d

dev-db-stop:
	docker compose -f docker-compose.dev.yml down