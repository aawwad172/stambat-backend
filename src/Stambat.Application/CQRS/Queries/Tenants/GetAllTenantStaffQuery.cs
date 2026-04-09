using MediatR;

namespace Stambat.Application.CQRS.Queries.Tenants;

public sealed record GetAllTenantStaffQuery(
    int Page = 1,
    int Size = 5) : IRequest<GetAllTenantStaffQueryResult>;

public sealed record GetAllTenantStaffQueryResult(
    IEnumerable<StaffRecord> Staff,
    int TotalRecords,
    int TotalDisplayRecords);

public sealed record StaffRecord(
    Guid Id,
    string Name,
    string Email,
    IEnumerable<Guid> Roles,
    DateOnly JoinedDate,
    bool IsActive);
