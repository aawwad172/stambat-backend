using Stambat.Domain.Entities.Identity;

namespace Stambat.Domain.Interfaces.Application.Services;

public interface IPermissionService
{
    Task<List<string>> GetUserRolesAsync(Guid userId);
    Task<List<string>> GetUserPermissionsAsync(User user);
}
