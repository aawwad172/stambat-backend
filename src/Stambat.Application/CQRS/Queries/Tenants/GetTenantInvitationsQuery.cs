using MediatR;

using Stambat.Domain.Enums;

namespace Stambat.Application.CQRS.Queries.Tenants;

public sealed record GetTenantInvitationsQuery(
    int Page = 1,
    int Size = 10,
    InvitationStatus? Status = null,
    Guid? RoleId = null) : IRequest<GetTenantInvitationsQueryResult>;

public sealed record InvitationRecord(
    Guid Id,
    string RecipientEmail,
    DateOnly SentDate,
    string Role,
    InvitationStatus Status);

public sealed record GetTenantInvitationsQueryResult(
    IEnumerable<InvitationRecord> Invitations,
    int TotalRecords,
    int TotalDisplayRecords);
