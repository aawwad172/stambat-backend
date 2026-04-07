using MediatR;

namespace Stambat.Application.CQRS.Commands.Authentication;

public record SelectTenantCommand(string IdentityToken, Guid TenantId) : IRequest<SelectTenantCommandResult>;

public record SelectTenantCommandResult(
    string AccessToken, string RefreshToken, Guid TenantId, string TenantName);
