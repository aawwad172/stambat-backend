using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities;

public class Invitation : IBaseEntity
{
    public Guid Id { get; init; }
    public required string Email { get; set; }
    public string? Token { get; set; } // <--- NOT MAPPED TO DB
    public required string TokenHash { get; set; } // <--- STORED IN DB

    // For a new signup, this is NULL until the Tenant is created in Phase 2
    // For an employee invite, this would be set by the Tenant Admin
    public Guid? TenantId { get; set; }
    public virtual Tenant? Tenant { get; set; }

    public required Guid RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }

    // Auditing
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
