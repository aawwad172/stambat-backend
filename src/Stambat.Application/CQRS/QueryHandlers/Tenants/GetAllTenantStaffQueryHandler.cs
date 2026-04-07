using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Queries.Tenants;
using Stambat.Domain.Entities;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Enums;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.QueryHandlers.Tenants;

public class GetAllTenantStaffQueryHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService currentTenantProviderService,
    ILogger<GetAllTenantStaffQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository)
    : BaseHandler<GetAllTenantStaffQuery, GetAllTenantStaffQueryResult>(currentUserService, currentTenantProviderService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;

    public override async Task<GetAllTenantStaffQueryResult> Handle(GetAllTenantStaffQuery request, CancellationToken cancellationToken)
    {
        if (_currentTenant.TenantId is null)
            throw new ArgumentNullException("TenantId should be provided via JWT claims");

        Guid tenantId = _currentTenant.TenantId.Value;

        PaginationResult<User> result = await _userRepository.GetStaffByTenantAsync(
            tenantId,
            pageNumber: request.Page,
            pageSize: request.Size);

        IEnumerable<StaffRecord> staff = (result.Page ?? []).Select(user =>
        {
            // Get the user's role for this specific tenant
            var tenantRole = user.UserRoleTenants
                .FirstOrDefault(urt => urt.TenantId == tenantId);

            return new StaffRecord(
                Id: user.Id,
                Name: user.FullName.Formatted,
                Email: user.Email.ToString(),
                Role: Enum.TryParse<RolesEnum>(tenantRole?.Role?.Name, out var role)
                    ? role
                    : RolesEnum.Merchant,
                JoinedDate: DateOnly.FromDateTime(user.CreatedAt),
                IsActive: user.IsActive);
        });

        return new GetAllTenantStaffQueryResult(
            Staff: staff,
            TotalRecords: result.TotalRecords,
            TotalDisplayRecords: result.TotalDisplayRecords);
    }
}
