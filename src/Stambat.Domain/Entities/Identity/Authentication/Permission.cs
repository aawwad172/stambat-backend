using Stambat.Domain.Common;
using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities.Identity.Authentication;

public class Permission : IBaseEntity
{
    public Guid Id { get; init; }

    /// <summary>
    /// Stable unique key used in code/policies, e.g. "Users.Read", "Roles.Write".
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Optional human description for admin UI.
    /// </summary>
    public string? Description { get; private set; }

    // Auditing
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    // Navigations
    public ICollection<Role> Roles { get; private set; } = [];

    // EF Core constructor
    private Permission()
    {
        Name = default!;
    }

    public static Permission Create(string name, string? description = null, Guid? id = null)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));

        return new Permission
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
