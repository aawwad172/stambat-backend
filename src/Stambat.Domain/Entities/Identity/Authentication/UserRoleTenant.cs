using Stambat.Domain.Interfaces.Domain;

namespace Stambat.Domain.Entities.Identity.Authentication;

public class UserRoleTenant : IEntity
{
    public required Guid Id { get; init; }

    public required Guid UserId { get; set; }
    public User? User { get; set; }

    // Which role
    public required Guid RoleId { get; set; }
    public Role? Role { get; set; }

    // Whome
    public Guid? TenantId { get; set; }
    public Tenant? Tenant { get; set; }
}
