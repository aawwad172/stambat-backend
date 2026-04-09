using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Application.CQRS.Queries.Tenants;
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

        tenants.MapGet(EndpointRoutes.StaffInvitations, GetTenantInvitations.RegisterRoute)
            .RequireAuthorization(PermissionConstants.InvitationsView)
            .Produces<ApiResponse<GetTenantInvitationsQueryResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json");

        tenants.MapDelete(EndpointRoutes.StaffInvitations, CancelInvitation.RegisterRoute)
            .RequireAuthorization(PermissionConstants.InvitationsDelete)
            .Produces<ApiResponse<CancelInvitationCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<string>>(StatusCodes.Status404NotFound, "application/json");

        tenants.MapPost(EndpointRoutes.InviteStaff, InviteStaff.RegisterRoute)
            .RequireAuthorization(PermissionConstants.InvitationsAdd)
            .Produces<ApiResponse<InviteStaffCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Accepts<InviteStaffCommand>("application/json");

        tenants.MapPost(EndpointRoutes.SetupTenant, SetupTenant.RegisterRoute)
            .RequireAuthorization(PermissionConstants.TenantsSetup)
            .Produces<ApiResponse<SetupTenantCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json");

        tenants.MapGet(EndpointRoutes.GetAllTenantStaff, GetAllTenantStaff.RegisterRoute)
            .RequireAuthorization(PermissionConstants.TenantsView)
            .Accepts<GetAllTenantStaffQuery>("application/json")
            .Produces<ApiResponse<GetAllTenantStaffQueryResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json");

        tenants.MapGet(EndpointRoutes.GetStaffMember, GetStaffMember.RegisterRoute)
            .RequireAuthorization(PermissionConstants.UsersView)
            .Produces<ApiResponse<StaffRecord>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<string>>(StatusCodes.Status404NotFound, "application/json");

        tenants.MapGet(EndpointRoutes.StaffRoles, GetStaffRoles.RegisterRoute)
            .RequireAuthorization(PermissionConstants.UsersView)
            .Produces<ApiResponse<IEnumerable<AvailableRoleRecord>>>(StatusCodes.Status200OK, "application/json");

        tenants.MapDelete(EndpointRoutes.RemoveStaff, RemoveStaff.RegisterRoute)
            .RequireAuthorization(PermissionConstants.UsersDelete)
            .Produces<ApiResponse<RemoveStaffCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<string>>(StatusCodes.Status404NotFound, "application/json");

        tenants.MapPatch(EndpointRoutes.DeactivateStaff, DeactivateStaff.RegisterRoute)
            .RequireAuthorization(PermissionConstants.UsersEdit)
            .Produces<ApiResponse<DeactivateStaffCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<string>>(StatusCodes.Status404NotFound, "application/json");

        tenants.MapPatch(EndpointRoutes.StaffRoles, UpdateStaffRoles.RegisterRoute)
            .RequireAuthorization(PermissionConstants.UsersEdit)
            .Accepts<UpdateStaffRolesCommand>("application/json")
            .Produces<ApiResponse<UpdateStaffRolesCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<string>>(StatusCodes.Status404NotFound, "application/json");
    }
}
