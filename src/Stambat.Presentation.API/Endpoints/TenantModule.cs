using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Domain.Constants;
using Stambat.Domain.Enums;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;
using Stambat.WebAPI.Routes.Tenants;

namespace Stambat.WebAPI.Endpoints;

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
