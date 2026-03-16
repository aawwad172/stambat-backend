using MediatR;

namespace Stamply.Application.CQRS.Commands.Tenants;

public sealed record SetupTenantCommand(
    string CompanyName,
    string BusinessEmail) : IRequest<SetupTenantCommandResult>;

public sealed record SetupTenantCommandResult(Guid TenantId, Guid UserId);
