using MediatR;

namespace Stamply.Application.CQRS.Queries.Invitations;

public sealed record ValidateInvitationQuery(string Token) : IRequest<ValidateInvitationQueryResult>;

public sealed record ValidateInvitationQueryResult(string Email,
    string TenantName,
    string RoleName);
