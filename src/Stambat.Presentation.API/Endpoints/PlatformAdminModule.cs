using Stambat.Application.CQRS.Commands.Admin;
using Stambat.Domain.Constants;
using Stambat.Domain.Enums;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;
using Stambat.WebAPI.Routes.Admin;

namespace Stambat.WebAPI.Endpoints;

public class PlatformAdminModule : IEndpointModule
{
    private readonly string _group = "/admin";
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder admin = app.MapGroup(_group)
            .WithTags(EndpointTags.Admin);

        admin.MapPost(EndpointRoutes.InviteTenant, InviteTenant.RegisterRoute)
            .Produces<ApiResponse<InviteTenantCommandResult>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest)
            .RequireAuthorization(PermissionConstants.SystemManage)
            .Accepts<InviteTenantCommand>("application/json");
    }
}
