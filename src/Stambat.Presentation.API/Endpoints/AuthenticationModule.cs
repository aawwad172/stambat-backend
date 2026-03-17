using Stambat.Application.CQRS.Commands.Authentication;
using Stambat.Domain.Constants;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;
using Stambat.WebAPI.Routes.Authentication;

using RefreshToken = Stambat.WebAPI.Routes.Authentication.RefreshToken;

namespace Stambat.WebAPI.Endpoints;

public class AuthenticationModule : IEndpointModule
{
    private readonly string _group = "/auth";
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder auth = app.MapGroup(_group)
            .WithTags(EndpointTags.Authentication);

        auth.MapPost(EndpointRoutes.Login, Login.RegisterRoute)
           .Accepts<LoginCommand>("application/json")
           .Produces<ApiResponse<LoginCommandResult>>(StatusCodes.Status200OK, "application/json")
           .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
           .Produces<ApiResponse<LoginCommandResult>>(StatusCodes.Status401Unauthorized, "application/json");

        auth.MapPost(EndpointRoutes.RefreshToken, RefreshToken.RegisterRoute)
           .Accepts<RefreshTokenCommand>("application/json")
           .Produces<ApiResponse<RefreshTokenCommandResult>>(StatusCodes.Status200OK, "application/json")
           .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
           .Produces<ApiResponse<RefreshTokenCommandResult>>(StatusCodes.Status401Unauthorized, "application/json");

        auth.MapPost(EndpointRoutes.Logout, Logout.RegisterRoute)
           .Accepts<LogoutCommand>("application/json")
           .Produces<ApiResponse<LogoutCommandResult>>(StatusCodes.Status200OK, "application/json")
           .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
           .Produces<ApiResponse<LogoutCommandResult>>(StatusCodes.Status401Unauthorized, "application/json");
    }
}
