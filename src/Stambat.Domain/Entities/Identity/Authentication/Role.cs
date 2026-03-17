using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities.Identity.Authentication;

public class Role : IBaseEntity
{
    public Guid Id { get; init; }

    // Human-friendly identifier (e.g., "SuperAdmin", "TenantAdmin", "SubAdmin", "AppUser")
    public required string Name { get; init; }
    public string? Description { get; init; }

    // Navigations
    public ICollection<UserRoleTenant> UserRoleTenants { get; set; } = [];
    public ICollection<Permission> Permissions { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
