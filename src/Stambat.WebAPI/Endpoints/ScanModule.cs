using Stambat.Application.CQRS.Commands.Scanning;
using Stambat.Domain.Constants;
using Stambat.Domain.Enums;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;
using Stambat.WebAPI.Routes.Scanning;

namespace Stambat.WebAPI.Endpoints;

public class ScanModule : IEndpointModule
{
    private readonly string _group = "/scan";

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder scan = app.MapGroup(_group)
            .WithTags(EndpointTags.Scanning);

        scan.MapPost(EndpointRoutes.ScanStamp, ScanStamp.RegisterRoute)
            .RequireAuthorization(PermissionConstants.ScanStamping)
            .Accepts<ScanStampCommand>("application/json")
            .Produces<ApiResponse<ScanStampCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<string>>(StatusCodes.Status404NotFound, "application/json");

        scan.MapPost(EndpointRoutes.ScanRedeem, ScanRedeem.RegisterRoute)
            .RequireAuthorization(PermissionConstants.ScanRedeem)
            .Accepts<ScanRedeemCommand>("application/json")
            .Produces<ApiResponse<ScanRedeemCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<string>>(StatusCodes.Status404NotFound, "application/json");
    }
}
