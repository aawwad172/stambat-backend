using Stambat.Domain.Common;
using Stambat.Domain.Enums;
using Stambat.Domain.Interfaces.Domain;

namespace Stambat.Domain.Entities.Identity;

public class UserToken : IEntity
{
    public required Guid Id { get; init; }
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public UserTokenType Type { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public bool IsUsed { get; private set; }

    // Navigation property
    public User User { get; private set; } = null!;

    // EF Core constructor
    private UserToken() { }

    public static UserToken Create(Guid userId, string token, UserTokenType type, DateTime expiryDate)
    {
        Guard.AgainstDefault(userId, nameof(userId));
        Guard.AgainstNullOrEmpty(token, nameof(token));

        return new UserToken
        {
            Id = IdGenerator.New(),
            UserId = userId,
            Token = token,
            Type = type,
            ExpiryDate = expiryDate,
            IsUsed = false
        };
    }

    public void MarkAsUsed()
    {
        IsUsed = true;
    }
}
