using Stambat.Domain.Entities;
using Stambat.Domain.Entities.Identity;

namespace Stambat.Domain.Interfaces.Infrastructure.IRepositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetByIdWithDetailsAsync(Guid id);
    Task<User?> GetUserByIdentityTokenAsync(string token);
    Task<PaginationResult<User>> GetStaffByTenantAsync(Guid tenantId, int? pageNumber, int? pageSize);
    Task<User?> GetStaffMemberByTenantAsync(Guid tenantId, Guid staffId);
}
