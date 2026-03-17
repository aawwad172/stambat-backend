using Stambat.Domain.Entities.Identity;

namespace Stambat.Domain.Interfaces.Infrastructure.IRepositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByUsernameAsync(string username);
}

