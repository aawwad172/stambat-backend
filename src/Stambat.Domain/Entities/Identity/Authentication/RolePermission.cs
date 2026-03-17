namespace Stambat.Domain.Entities.Identity.Authentication;

public class RolePermission
{
    // Composite key will be configured in EF (RoleId + PermissionId)
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }

    public Guid PermissionId { get; set; }
    public Permission? Permission { get; set; }
}
