using MediatR;

namespace Stambat.Application.CQRS.Queries.Tenants;

public sealed record GetStaffMemberQuery(Guid StaffId) : IRequest<GetStaffMemberQueryResult>;

public sealed record GetStaffMemberQueryResult(StaffRecord Staff);
