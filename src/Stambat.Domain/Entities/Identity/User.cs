using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Domain;
using Stambat.Domain.Interfaces.Domain.Auditing;
using Stambat.Domain.ValueObjects;

namespace Stambat.Domain.Entities.Identity;

public class User : IBaseEntity, IAggregateRoot
{
    // Properties
    public required Guid Id { get; init; }
    public FullName FullName { get; private set; }
    public string Username { get; private set; }
    public Email Email { get; private set; }
    public Guid? UserCredentialsId { get; private set; }
    public string SecurityStamp { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDeleted { get; set; }
    public bool IsVerified { get; private set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    public virtual UserCredentials? Credentials { get; private set; }
    public ICollection<RefreshToken> RefreshTokens { get; private set; } = [];
    public ICollection<UserRoleTenant> UserRoleTenants { get; private set; } = [];
    public ICollection<UserToken> UserTokens { get; private set; } = [];
    public ICollection<WalletPass> WalletPasses { get; private set; } = [];

    // EF Core constructor
    private User()
    {
        // Required for EF Core
        FullName = default!;
        Username = default!;
        Email = default!;
        SecurityStamp = default!;
    }

    public void SetCredentials(UserCredentials credentials)
    {
        Credentials = credentials;
        UserCredentialsId = credentials.Id;
    }

    public void UpdateSecurityStamp()
    {
        SecurityStamp = IdGenerator.New().ToString();
    }

    public void VerifyEmail()
    {
        IsVerified = true;
        UpdateSecurityStamp();
    }

    /// <summary>
    /// Assigns a role to the user for a specific tenant (or globally if tenantId is null).
    /// Multiple roles can be assigned for the same tenant.
    /// </summary>
    public void AssignRole(Guid roleId, Guid? tenantId = null)
    {
        Guard.AgainstDefault(roleId, nameof(roleId));

        var alreadyHasRole = UserRoleTenants.Any(x => x.TenantId == tenantId && x.RoleId == roleId);

        if (alreadyHasRole)
        {
            throw new ConflictException($"User already has the specified role for this {(tenantId.HasValue ? "tenant" : "global scope")}.");
        }

        UserRoleTenants.Add(UserRoleTenant.Create(Id, roleId, tenantId));
    }

    /// <summary>
    /// Removes a specific role for a specific tenant (or global scope).
    /// </summary>
    public void RemoveRole(Guid roleId, Guid? tenantId = null)
    {
        UserRoleTenant? link = UserRoleTenants.FirstOrDefault(x => x.TenantId == tenantId && x.RoleId == roleId);
        if (link != null)
        {
            UserRoleTenants.Remove(link);
        }
    }

    public void AddRefreshToken(
        string tokenHash,
        string plaintextToken,
        DateTime expiresAt,
        Guid tokenFamilyId)
    {
        Guard.AgainstNullOrEmpty(tokenHash, nameof(tokenHash));
        Guard.AgainstNullOrEmpty(plaintextToken, nameof(plaintextToken));

        RefreshToken refreshToken = RefreshToken.Create(
            userId: Id,
            tokenFamilyId: tokenFamilyId,
            tokenHash: tokenHash,
            plaintextToken: plaintextToken,
            expiresAt: expiresAt,
            securityStampAtIssue: SecurityStamp
        );

        RefreshTokens.Add(refreshToken);
    }

    public void RevokeRefreshToken(string tokenHash, string reason, Guid? replacedByTokenId = null)
    {
        Guard.AgainstNullOrEmpty(tokenHash, nameof(tokenHash));

        RefreshToken? token = RefreshTokens.FirstOrDefault(rt => rt.TokenHash == tokenHash && !rt.IsRevoked);
        token?.Revoke(reason, replacedByTokenId);
    }

    public void AddUserToken(UserTokenType type, string value, DateTime expiry)
    {
        Guard.AgainstNullOrEmpty(value, nameof(value));

        UserToken userToken = UserToken.Create(
            userId: Id,
            token: value,
            type: type,
            expiryDate: expiry
        );

        UserTokens.Add(userToken);
    }

    // Static factory for user registration/creation
    public static User Create(
        FullName fullName,
        string username,
        Email email,
        string securityStamp,
        bool isVerified = false,
        Guid? id = null)
    {
        Guard.AgainstNullOrEmpty(username, nameof(username));
        Guard.AgainstNull(fullName, nameof(fullName));
        Guard.AgainstNull(email, nameof(email));
        Guard.AgainstNullOrEmpty(securityStamp, nameof(securityStamp));

        return new User
        {
            Id = id ?? IdGenerator.New(),
            FullName = fullName,
            Username = username,
            Email = email,
            SecurityStamp = securityStamp,
            IsActive = true,
            IsDeleted = false,
            IsVerified = isVerified,
        };
    }
}
