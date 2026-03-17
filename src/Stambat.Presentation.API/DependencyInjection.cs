using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.IdentityModel.Tokens;

using Stambat.Application.Utilities;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Enums;
using Stambat.WebAPI.Validators.Commands.Authentication;
using Stambat.WebAPI.Validators.Commands.Users;
using Stambat.WebAPI.Validators.Queries.Users;

namespace Stambat.WebAPI;

public static class DependencyInjection
{
    /// <summary>
    /// Registers Presentation layer services such as controllers, MediatR, FluentValidation, and any pipeline behaviors.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection for chaining.</returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Register FluentValidation validators found in the current assembly.
        services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<RefreshTokenCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<LogoutCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<IsUserVerifiedQueryValidator>();

        // Configure JSON options for Minimal APIs
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition =
                JsonIgnoreCondition.WhenWritingNull;
        });

        services.AddHttpContextAccessor();

        services.AddLogging(configure =>
        {
            configure.ClearProviders();
            configure.AddConsole();
            configure.AddDebug();
        });

        // Optionally, register pipeline behaviors (for example, a transactional behavior).
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        // Configure JWT authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration.GetRequiredSetting("Jwt:Issuer"),
                ValidAudience = configuration.GetRequiredSetting("Jwt:Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetRequiredSetting("Jwt:JwtSecretKey")))
            };
        });

        services.AddAuthorization(options =>
        {
            // Dynamically add policies for all permissions defined in PermissionConstants
            foreach (FieldInfo field in typeof(PermissionConstants).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            {
                if (field.IsLiteral && !field.IsInitOnly && field.GetValue(null) is string permissionName)
                {
                    options.AddPolicy(permissionName, policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(CustomClaims.Permission, permissionName);
                    });
                }
            }
        });

        return services;
    }
}
