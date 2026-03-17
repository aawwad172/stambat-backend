using Stambat.Domain.Entities.Identity.Authentication;

namespace Stambat.Domain.Interfaces.Infrastructure.IRepositories;

public interface IRoleRepository
{
    Task<Role?> GetRoleByNameAsync(string name);
}
