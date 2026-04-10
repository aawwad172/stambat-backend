using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Stambat.Application.Services;
using Stambat.Application.Utilities;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IClients;
using Stambat.Domain.Interfaces.Infrastructure.IEmail;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;
using Stambat.Domain.ValueObjects;
using Stambat.Infrastructure.Clients.QrCode;
using Stambat.Infrastructure.Clients.WalletPass;
using Stambat.Infrastructure.Clients.WalletPass.Options;
using Stambat.Infrastructure.Email;
using Stambat.Infrastructure.Persistence;
using Stambat.Infrastructure.Persistence.Interceptors;
using Stambat.Infrastructure.Persistence.Repositories;

namespace Stambat.Infrastructure;

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
            options.LogTo(Console.WriteLine, LogLevel.Information)
              .EnableSensitiveDataLogging()
              .EnableDetailedErrors();
        });
        // Add your repositories like this here
        // services.AddScoped<IRepository, Repository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IInvitationRepository, InvitationRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddLogging();


        // QR Code Service
        services.AddScoped<IQrCodeService, QrCodeService>();

        // Wallet Pass Providers (Factory + Strategy)
        services.AddScoped<IWalletPassProvider, GoogleWalletPassProvider>();
        services.AddScoped<IWalletPassProviderFactory, WalletPassProviderFactory>();
        services.Configure<GoogleWalletOptions>(configuration.GetSection(GoogleWalletOptions.SectionName));
        services.Configure<AppleWalletOptions>(configuration.GetSection(AppleWalletOptions.SectionName));

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
