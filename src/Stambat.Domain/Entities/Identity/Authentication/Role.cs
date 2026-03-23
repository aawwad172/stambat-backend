using Stambat.Domain.Common;
using Stambat.Domain.Interfaces.Domain;
using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities.Identity.Authentication;

public class Role : IBaseEntity, IAggregateRoot
{
    public Guid Id { get; init; }

    // Human-friendly identifier (e.g., "SuperAdmin", "TenantAdmin", "SubAdmin", "AppUser")
    public string Name { get; private set; }
    public string? Description { get; private set; }

    // Navigations
    public ICollection<UserRoleTenant> UserRoleTenants { get; private set; } = [];
    public ICollection<Permission> Permissions { get; private set; } = [];

    // Auditing
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    // EF Core constructor
    private Role()
    {
        Name = default!;
    }

    public static Role Create(string name, string? description = null, Guid? id = null)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));

        return new Role
        {
            Id = id ?? IdGenerator.New(),
            Name = name,
            Description = description
        };
    }

    public void Update(string name, string? description)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));
        Name = name;
        Description = description;
    }
}
