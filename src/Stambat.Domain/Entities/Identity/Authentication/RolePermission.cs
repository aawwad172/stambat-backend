using Stambat.Domain.Common;

namespace Stambat.Domain.Entities.Identity.Authentication;

public class RolePermission
{
    // Composite key will be configured in EF (RoleId + PermissionId)
    public Guid RoleId { get; private set; }
    public Role? Role { get; private set; }

    public Guid PermissionId { get; private set; }
    public Permission? Permission { get; private set; }

    private RolePermission() { }

    public static RolePermission Create(Guid roleId, Guid permissionId)
    {
        Guard.AgainstDefault(roleId, nameof(roleId));
        Guard.AgainstDefault(permissionId, nameof(permissionId));

        return new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        };
    }
}
