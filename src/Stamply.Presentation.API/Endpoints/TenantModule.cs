using Stamply.Application.CQRS.Commands.Tenants;
using Stamply.Domain.Constants;
using Stamply.Domain.Enums;
using Stamply.Presentation.API.Interfaces;
using Stamply.Presentation.API.Models;
using Stamply.Presentation.API.Routes.Tenants;

namespace Stamply.Presentation.API.Endpoints;

public class TenantModule : IEndpointModule
{
    private readonly string _group = "/tenants";
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder tenants = app.MapGroup(_group)
            .WithTags(EndpointTags.Tenants);

        tenants.MapPost(EndpointRoutes.InviteStaff, InviteStaff.RegisterRoute)
            .RequireAuthorization(PermissionConstants.InvitationsAdd)
            .Produces<ApiResponse<InviteStaffCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Accepts<InviteStaffCommand>("application/json");

        tenants.MapPost(EndpointRoutes.SetupTenant, SetupTenant.RegisterRoute)
            .RequireAuthorization(PermissionConstants.TenantsSetup)
            .Produces<ApiResponse<SetupTenantCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json");
    }
}
