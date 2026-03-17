using Stambat.Domain.Enums;
using Stambat.Domain.Interfaces.Domain;

namespace Stambat.Domain.Entities.Identity;

public class UserToken : IEntity
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; set; }
    public required string Token { get; set; } = string.Empty;
    public required UserTokenType Type { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }

    // Navigation property
    public User User { get; set; } = null!;
}
