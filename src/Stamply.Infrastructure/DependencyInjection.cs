using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Stamply.Application.Services;
using Stamply.Application.Utilities;
using Stamply.Domain.Interfaces.Application.Services;
using Stamply.Domain.Interfaces.Infrastructure.IEmail;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;
using Stamply.Domain.ValueObjects;
using Stamply.Infrastructure.Email;
using Stamply.Infrastructure.Persistence;
using Stamply.Infrastructure.Persistence.Interceptors;
using Stamply.Infrastructure.Persistence.Repositories;

namespace Stamply.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetRequiredSetting("ConnectionStrings:DbConnectionString");

        services.AddScoped<AuditingInterceptor>();

        services.AddDbContext<ApplicationDbContext>((IServiceProvider provider, DbContextOptionsBuilder options) =>
        {
            options.UseNpgsql(connectionString);
            options.AddInterceptors(provider.GetRequiredService<AuditingInterceptor>());
        });
        // Add your repositories like this here
        // services.AddScoped<IRepository, Repository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddLogging();


        services.AddScoped<IEmailService, EmailService>();
        // Bind the JSON section to the EmailSettings class
        EmailSettings emailSettings = configuration
            .GetSection("EmailSettings")
            .Get<EmailSettings>()
            ?? throw new NullReferenceException("Email Settings should not be null");

        // Configure FluentEmail using the bound settings
        services
            .AddFluentEmail(emailSettings.DefaultFrom)
            .AddRazorRenderer(typeof(EmailService))
            .AddSmtpSender(emailSettings.SmtpServer, emailSettings.Port, emailSettings.Username, emailSettings.Password);

        return services;
    }
}
