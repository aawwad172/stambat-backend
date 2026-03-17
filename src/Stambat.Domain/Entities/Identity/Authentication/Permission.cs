using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities.Identity.Authentication;

public class Permission : IBaseEntity
{
    public Guid Id { get; init; }

    /// <summary>
    /// Stable unique key used in code/policies, e.g. "Users.Read", "Roles.Write".
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Optional human description for admin UI.
    /// </summary>
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;

    // Navigations
    public ICollection<Role> Roles { get; set; } = [];
}

