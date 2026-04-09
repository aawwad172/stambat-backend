using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Queries.Tenants;
using Stambat.Domain.Entities;
using Stambat.Domain.Enums;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.QueryHandlers.Tenants;

public class GetTenantInvitationsQueryHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService currentTenantProviderService,
    ILogger<GetTenantInvitationsQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IInvitationRepository invitationRepository)
    : BaseHandler<GetTenantInvitationsQuery, GetTenantInvitationsQueryResult>(currentUserService, currentTenantProviderService, logger, unitOfWork)
{
    private readonly IInvitationRepository _invitationRepository = invitationRepository;

    public override async Task<GetTenantInvitationsQueryResult> Handle(GetTenantInvitationsQuery request, CancellationToken cancellationToken)
    {
        if (_currentTenant.TenantId is null)
            throw new ArgumentNullException("TenantId should be provided via JWT claims");

        Guid tenantId = _currentTenant.TenantId.Value;

        PaginationResult<Invitation> result = await _invitationRepository.GetTenantInvitationsAsync(
            tenantId, request.Page, request.Size, request.Status, request.RoleId, cancellationToken);

        IEnumerable<InvitationRecord> invitations = (result.Page ?? []).Select(i => new InvitationRecord(
            Id: i.Id,
            RecipientEmail: i.Email,
            SentDate: DateOnly.FromDateTime(i.CreatedAt),
            Role: i.Role.Name,
            Status: DeriveStatus(i)));

        return new GetTenantInvitationsQueryResult(
            Invitations: invitations,
            TotalRecords: result.TotalRecords,
            TotalDisplayRecords: result.TotalDisplayRecords);
    }

    private static InvitationStatus DeriveStatus(Invitation i) =>
        i.IsCancelled ? InvitationStatus.Cancelled :
        i.ExpiresAt < DateTime.UtcNow ? InvitationStatus.Expired :
        InvitationStatus.Pending;
}
