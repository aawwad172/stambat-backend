using MapsterMapper;

using Microsoft.Extensions.DependencyInjection;

using Stambat.Application.CQRS.CommandHandlers.Authentication;
using Stambat.Application.CQRS.CommandHandlers.Users;
using Stambat.Application.CQRS.QueryHandlers.Users;
using Stambat.Application.Services;
using Stambat.Domain.Interfaces.Application.Services;

namespace Stambat.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(LogoutCommandHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(IsUserVerifiedQueryHandler).Assembly);
        });
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ITenantProviderService, TenantProviderService>();
        services.AddScoped<IMapper, Mapper>();

        return services;
    }
}
