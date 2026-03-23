using MediatR;

using Stambat.Domain.Enums;

namespace Stambat.Application.CQRS.Queries.Tenants;

public sealed record GetAllTenantStaffQuery(
    int Page = 1,
    int Size = 5) : IRequest<GetAllTenantStaffQueryResult>;

public sealed record GetAllTenantStaffQueryResult(IEnumerable<StaffRecord> Staff);

public sealed record StaffRecord(
    string Name,
    string Email,
    RolesEnum Role,
    DateOnly JoinedDate,
    bool IsActive);
