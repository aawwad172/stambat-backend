using MapsterMapper;

using Microsoft.Extensions.DependencyInjection;

using Stamply.Application.CQRS.CommandHandlers.Authentication;
using Stamply.Application.CQRS.CommandHandlers.Users;
using Stamply.Application.CQRS.QueryHandlers.Users;
using Stamply.Application.Services;
using Stamply.Domain.Interfaces.Application.Services;

namespace Stamply.Application;

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
