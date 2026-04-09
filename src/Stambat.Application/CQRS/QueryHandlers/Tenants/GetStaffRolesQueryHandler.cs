using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Queries.Tenants;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.QueryHandlers.Tenants;

public class GetStaffRolesQueryHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService currentTenantProviderService,
    ILogger<GetStaffRolesQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IRoleRepository roleRepository)
    : BaseHandler<GetStaffRolesQuery, GetStaffRolesQueryResult>(currentUserService, currentTenantProviderService, logger, unitOfWork)
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public override async Task<GetStaffRolesQueryResult> Handle(GetStaffRolesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Role> roles = await _roleRepository.GetStaffRolesAsync();

        IEnumerable<AvailableRoleRecord> result = roles.Select(r => new AvailableRoleRecord(r.Id, r.Name));

        return new GetStaffRolesQueryResult(result);
    }
}
