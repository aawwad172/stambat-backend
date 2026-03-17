using Stambat.Domain.Entities;

namespace Stambat.Domain.Interfaces.Infrastructure.IRepositories;

public interface IInvitationRepository : IRepository<Invitation>
{
    Task<Invitation?> GetInvitationByTokenHashAsync(string tokenHash);
    Task<Invitation?> GetLastActiveInvitationForTenantAndRole(string email, Guid tenantId, Guid roleId);

    Task<bool> ExistsActiveAsync(string email, Guid tenantId, Guid roleId, CancellationToken cancellationToken = default);
}
