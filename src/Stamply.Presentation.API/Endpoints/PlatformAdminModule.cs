using Stamply.Application.CQRS.Commands.Admin;
using Stamply.Domain.Constants;
using Stamply.Domain.Enums;
using Stamply.Presentation.API.Interfaces;
using Stamply.Presentation.API.Models;
using Stamply.Presentation.API.Routes.Admin;

namespace Stamply.Presentation.API.Endpoints;

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
