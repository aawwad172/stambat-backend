using Stambat.Domain.Common;
using Stambat.Domain.Interfaces.Domain;

namespace Stambat.Domain.Entities.Identity;

public class UserCredentials : IEntity
{
    public Guid Id { get; init; }
    public string PasswordHash { get; private set; }
    public Guid UserId { get; private set; }
    public virtual User? User { get; private set; }

    // EF Core constructor
    private UserCredentials()
    {
        PasswordHash = default!;
    }

    public static UserCredentials Create(
        Guid userId,
        string passwordHash)
    {
        return new UserCredentials
        {
            Id = IdGenerator.New(),
            UserId = userId,
            PasswordHash = passwordHash
        };
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }
}
