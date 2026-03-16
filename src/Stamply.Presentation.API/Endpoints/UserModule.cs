using Stamply.Application.CQRS.Commands.Users;
using Stamply.Application.CQRS.Queries.Users;
using Stamply.Domain.Constants;
using Stamply.Presentation.API.Interfaces;
using Stamply.Presentation.API.Models;
using Stamply.Presentation.API.Routes.Users;

namespace Stamply.Presentation.API.Endpoints;

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
