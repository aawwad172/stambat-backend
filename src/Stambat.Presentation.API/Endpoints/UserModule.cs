using Stambat.Application.CQRS.Commands.Users;
using Stambat.Application.CQRS.Queries.Users;
using Stambat.Domain.Constants;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;
using Stambat.WebAPI.Routes.Users;

namespace Stambat.WebAPI.Endpoints;

public class UserModule : IEndpointModule
{
    private readonly string _group = "/users";
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder users = app.MapGroup(_group)
            .WithTags(EndpointTags.Users);

        users.MapPost(EndpointRoutes.RegisterUser, RegisterUser.RegisterRoute)
           .Accepts<RegisterUserCommand>("application/json")
           .Produces<ApiResponse<RegisterUserCommandResult>>(StatusCodes.Status201Created, "application/json")
           .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
           .Produces<ApiResponse<RegisterUserCommandResult>>(StatusCodes.Status409Conflict, "application/json");

        users.MapPost(EndpointRoutes.VerifyEmail, VerifyEmail.RegisterRoute)
            .Accepts<VerifyEmailCommand>("application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<VerifyEmailCommandResult>>(StatusCodes.Status200OK);

        users.MapGet(EndpointRoutes.IsVerified, IsUserVerified.RegisterRoute)
            .Accepts<IsUserVerifiedQuery>("application/json")
            .Produces<ApiResponse<IsUserVerifiedQueryResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json");
    }
}
