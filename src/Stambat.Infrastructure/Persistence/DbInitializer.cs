using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.ValueObjects;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Infrastructure.Configurations.Seed;
using Stambat.Application.Utilities;
using Stambat.Domain.Interfaces.Application.Services;

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

            // 2. Seed the "System" User and Credentials
            if (!await context.Users.AnyAsync(u => u.Id == AuthSeedConstants.SystemUserId))
            {
                Guid systemUserId = AuthSeedConstants.SystemUserId;
                Guid credentialsId = Id.New();

                User? systemUser = new()
                {
                    Id = systemUserId,
                    FullName = new FullName { FirstName = "system", LastName = "system" },
                    Username = "system",
                    Email = "system@example.com",
                    UserCredentialsId = credentialsId,
                    // Credentials = superAdminCredentials,
                    Credentials = new UserCredentials
                    {
                        Id = credentialsId,
                        UserId = systemUserId,
                        PasswordHash = securityService.HashSecret(configuration.GetRequiredSetting("Security:SuperAdminPassword"))
                    },
                    SecurityStamp = AuthSeedConstants.SystemSecurityStampGuid,
                    IsActive = true,
                    IsVerified = true,
                    IsDeleted = false,
                    CreatedAt = AuthSeedConstants.SeedDateUtc,
                    CreatedBy = systemUserId,
                };

                context.Users.Add(systemUser);

                // Assign SuperAdmin role to system user
                context.UserRoleTenants.Add(new UserRoleTenant
                {
                    Id = Id.New(),
                    UserId = systemUserId,
                    RoleId = AuthSeedConstants.RoleIdSuperAdmin
                });

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
                    context.UserRoleTenants.Add(new UserRoleTenant
                    {
                        Id = Id.New(),
                        UserId = AuthSeedConstants.SystemUserId,
                        RoleId = AuthSeedConstants.RoleIdSuperAdmin
                    });
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
