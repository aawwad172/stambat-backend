using Stambat.Domain.Entities;
using Stambat.Domain.Enums;

namespace Stambat.Domain.Interfaces.Infrastructure.IRepositories;

public interface IInvitationRepository : IRepository<Invitation>
{
    Task<Invitation?> GetInvitationByTokenHashAsync(string tokenHash);
    Task<Invitation?> GetLastActiveInvitationForTenantAndRole(string email, Guid tenantId, Guid roleId);
    Task<bool> ExistsActiveAsync(string email, Guid tenantId, Guid roleId, CancellationToken cancellationToken = default);

    Task<PaginationResult<Invitation>> GetTenantInvitationsAsync(
        Guid tenantId, int page, int size,
        InvitationStatus? status, Guid? roleId,
        CancellationToken cancellationToken = default);

    Task<Invitation?> GetByIdForTenantAsync(Guid invitationId, Guid tenantId);
}
