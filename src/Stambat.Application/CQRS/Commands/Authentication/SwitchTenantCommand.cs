using MediatR;

namespace Stambat.Application.CQRS.Commands.Authentication;

public record SwitchTenantCommand(Guid TenantId) : IRequest<SwitchTenantCommandResult>;

public record SwitchTenantCommandResult(
    string AccessToken, string RefreshToken, Guid TenantId, string TenantName);
