using Stamply.Domain.Entities.Identity;

namespace Stamply.Domain.Interfaces.Infrastructure.IRepositories;

public interface IUserTokenRepository : IRepository<UserToken>
{
    Task<UserToken?> GetUserTokenByTokenAsync(string token);
}
