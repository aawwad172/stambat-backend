using MediatR;

namespace Stambat.Application.CQRS.Queries.Tenants;

public sealed record GetStaffRolesQuery : IRequest<GetStaffRolesQueryResult>;

public sealed record GetStaffRolesQueryResult(IEnumerable<AvailableRoleRecord> Roles);

public sealed record AvailableRoleRecord(Guid Id, string Name);
