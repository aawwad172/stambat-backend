using Stambat.Domain.Entities.Identity;

namespace Stambat.Domain.Interfaces.Infrastructure.IRepositories;

public interface IUserTokenRepository : IRepository<UserToken>
{
    Task<UserToken?> GetUserTokenByTokenAsync(string token);
}
