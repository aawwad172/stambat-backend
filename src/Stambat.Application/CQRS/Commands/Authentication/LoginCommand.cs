using MediatR;

using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.Commands.Authentication;

public record LoginCommand(string Email, string Password) : IRequest<LoginCommandResult>;

/// <summary>
/// Result of the login attempt. 

/// 1. Regular User: Returns IdentityToken + Tenants list (AccessToken/RefreshToken are NULL).
/// Requires a second step (SelectTenant) to get final session tokens.

/// 2. SuperAdmin: Returns AccessToken + RefreshToken directly (IdentityToken/Tenants are NULL).
/// Bypasses the tenant selection step.

/// NOTE: Null fields are omitted from the serialized JSON in the WebAPI layer.
/// </summary>
public record LoginCommandResult(
    string? IdentityToken,
    string? AccessToken,
    string? RefreshToken,
    bool IsSuperAdmin,
    List<TenantInfo> Tenants);
