using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Interfaces.Domain;
using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities;

public class Invitation : IBaseEntity, IAggregateRoot
{
    public Guid Id { get; init; }
    public string Email { get; private set; }
    public string? Token { get; private set; } // <--- NOT MAPPED TO DB
    public string TokenHash { get; private set; } // <--- STORED IN DB

    // For a new signup, this is NULL until the Tenant is created in Phase 2
    // For an employee invite, this would be set by the Tenant Admin
    public Guid? TenantId { get; private set; }
    public virtual Tenant? Tenant { get; private set; }

    public Guid RoleId { get; private set; }
    public virtual Role Role { get; private set; } = null!;

    public DateTime ExpiresAt { get; private set; }
    public bool IsUsed { get; private set; }

    // Auditing
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    // EF Core constructor
    private Invitation()
    {
        Email = default!;
        TokenHash = default!;
    }

    public static Invitation Create(
        string email,
        string tokenHash,
        string? token,
        Guid roleId,
        Guid? tenantId,
        DateTime expiresAt)
    {
        Guard.AgainstNullOrEmpty(email, nameof(email));
        Guard.AgainstNullOrEmpty(tokenHash, nameof(tokenHash));
        Guard.AgainstDefault(roleId, nameof(roleId));

        return new Invitation
        {
            Id = IdGenerator.New(),
            Email = email,
            TokenHash = tokenHash,
            Token = token,
            RoleId = roleId,
            TenantId = tenantId,
            ExpiresAt = expiresAt,
            IsUsed = false,
            IsDeleted = false
        };
    }

    public void MarkAsUsed() => IsUsed = true;
}
