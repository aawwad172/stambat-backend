using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Stambat.Application.Utilities;
using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.ValueObjects;
using Stambat.Infrastructure.Configurations.Seed;

namespace Stambat.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task ApplyMigrationsAndSeedAsync(this IServiceProvider serviceProvider)
    {
        using IServiceScope? scope = serviceProvider.CreateScope();
        IServiceProvider? services = scope.ServiceProvider;

        try
        {
            ApplicationDbContext? context = services.GetRequiredService<ApplicationDbContext>();
            IConfiguration? configuration = services.GetRequiredService<IConfiguration>();

            ISecurityService securityService = services.GetRequiredService<ISecurityService>();

            // 1. Automatically apply any pending migrations
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            // 1. Apply Migrations
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            // 2. STAGE 1: Seed Roles (Crucial First Step)
            // Ensure the SuperAdmin role exists physically in the DB
            if (!await context.Roles.AnyAsync(r => r.Id == AuthSeedConstants.RoleIdSuperAdmin))
            {
                var adminRole = Role.Create(
                    "SuperAdmin",
                    "Full System Access",
                    AuthSeedConstants.RoleIdSuperAdmin
                );
                context.Roles.Add(adminRole);
                // We MUST save here so the ID exists for the Foreign Key check in Stage 2
                await context.SaveChangesAsync();
            }

            // 2. Seed the "System" User and Credentials
            if (!await context.Users.AnyAsync(u => u.Id == AuthSeedConstants.SystemUserId))
            {
                Guid systemUserId = AuthSeedConstants.SystemUserId;
                Guid credentialsId = IdGenerator.New();

                User? systemUser = User.Create(
                    FullName.Create("system", "system"),
                    "system",
                    Stambat.Domain.ValueObjects.Email.Create("system@example.com"),
                    AuthSeedConstants.SystemSecurityStampGuid,
                    isVerified: true,
                    systemUserId);

                systemUser.CreatedAt = AuthSeedConstants.SeedDateUtc;

                UserCredentials credentials = UserCredentials.Create(
                    credentialsId,
                    securityService.HashSecret(configuration.GetRequiredSetting("Security:SuperAdminPassword"))
                );

                systemUser.SetCredentials(credentials);
                systemUser.AssignRole(AuthSeedConstants.RoleIdSuperAdmin);

                context.Users.Add(systemUser);

                await context.SaveChangesAsync();
            }
            else
            {
                // Ensure system user has the SuperAdmin role if it was created previously without it
                bool hasRole = await context.UserRoleTenants.AnyAsync(ur =>
                    ur.UserId == AuthSeedConstants.SystemUserId &&
                    ur.RoleId == AuthSeedConstants.RoleIdSuperAdmin);

                if (!hasRole)
                {
                    context.UserRoleTenants.Add(UserRoleTenant.Create(
                        IdGenerator.New(),
                        AuthSeedConstants.SystemUserId,
                        AuthSeedConstants.RoleIdSuperAdmin
                    ));
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            // Ideally use a logger here
            Console.WriteLine($"An error occurred during migration/seeding: {ex.Message}");
            throw;
        }
    }
}
