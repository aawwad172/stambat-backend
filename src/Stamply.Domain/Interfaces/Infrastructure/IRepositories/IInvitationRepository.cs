using Stamply.Domain.Entities;

namespace Stamply.Domain.Interfaces.Infrastructure.IRepositories;

public interface IInvitationRepository : IRepository<Invitation>
{
    Task<Invitation?> GetInvitationByTokenHashAsync(string tokenHash);
    Task<Invitation?> GetLastActiveInvitationForTenantAndRole(string email, Guid tenantId, Guid roleId);
}
