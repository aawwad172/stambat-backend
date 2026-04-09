using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Queries.Tenants;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.QueryHandlers.Tenants;

public class GetStaffMemberQueryHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService currentTenantProviderService,
    ILogger<GetStaffMemberQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository)
    : BaseHandler<GetStaffMemberQuery, GetStaffMemberQueryResult>(currentUserService, currentTenantProviderService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;

    public override async Task<GetStaffMemberQueryResult> Handle(GetStaffMemberQuery request, CancellationToken cancellationToken)
    {
        if (_currentTenant.TenantId is null)
            throw new ArgumentNullException("TenantId should be provided via JWT claims");

        Guid tenantId = _currentTenant.TenantId.Value;

        User user = await _userRepository.GetStaffMemberByTenantAsync(tenantId, request.StaffId)
            ?? throw new NotFoundException($"Staff member {request.StaffId} was not found in this tenant.");

        StaffRecord staff = new(
            Id: user.Id,
            Name: user.FullName.Formatted,
            Email: user.Email.ToString(),
            Roles: user.UserRoleTenants
                .Where(urt => urt.TenantId == tenantId)
                .Select(urt => urt.RoleId),
            JoinedDate: DateOnly.FromDateTime(user.CreatedAt),
            IsActive: user.UserRoleTenants.Any(urt => urt.TenantId == tenantId && urt.IsActive));

        return new GetStaffMemberQueryResult(staff);
    }
}
