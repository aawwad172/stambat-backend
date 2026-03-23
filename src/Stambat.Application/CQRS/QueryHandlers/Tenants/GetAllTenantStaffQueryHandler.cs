// using Microsoft.Extensions.Logging;

// using Stambat.Application.CQRS.Queries.Tenants;
// using Stambat.Domain.Interfaces.Application.Services;
// using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

// namespace Stambat.Application.CQRS.QueryHandlers.Tenants;

// public class GetAllTenantStaffQueryHandler(
//     ICurrentUserService currentUserService,
//     ITenantProviderService currentTenantProviderService,
//     ILogger<GetAllTenantStaffQueryHandler> logger,
//     IUnitOfWork unitOfWork,
//     IUserRoleTenantRepository userRoleTenantRepository)
//     : BaseHandler<GetAllTenantStaffQuery, GetAllTenantStaffQueryResult>(currentUserService, currentTenantProviderService, logger, unitOfWork)
// {
//     private readonly IUserRoleTenantRepository _userRoleTenantRepository = userRoleTenantRepository;
//     public override Task<GetAllTenantStaffQueryResult> Handle(GetAllTenantStaffQuery request, CancellationToken cancellationToken)
//     {
//         if (_currentTenant.TenantId is null)
//             throw new ArgumentNullException("TenantId should be provided via headers");

//         try
//         {
//             var staff = _userRoleTenantRepository.GetAllAsync(request.Page, request.Size);
//         }
//         catch (System.Exception)
//         {

//             throw;
//         }
//     }
// }
