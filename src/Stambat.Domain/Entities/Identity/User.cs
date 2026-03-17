using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Interfaces.Domain.Auditing;
using Stambat.Domain.ValueObjects;
namespace Stambat.Domain.Entities.Identity;

public class User : IBaseEntity
{
    public required Guid Id { get; init; }
    public required FullName FullName { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public Guid? UserCredentialsId { get; set; }
    public required string SecurityStamp { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; } = false;
    public required bool IsVerified { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public virtual UserCredentials? Credentials { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<UserRoleTenant> UserRoleTenants { get; set; } = [];
    public ICollection<WalletPass> WalletPasses { get; set; } = [];
}
