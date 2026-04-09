using MediatR;

namespace Stambat.Application.CQRS.Commands.Tenants;

public sealed record RoleSelection(Guid RoleId, bool IsSelected);

public sealed record UpdateStaffRolesCommand(
    Guid StaffId,
    IEnumerable<RoleSelection> Roles) : IRequest<UpdateStaffRolesCommandResult>;

public sealed record UpdateStaffRolesCommandResult(string Message);
