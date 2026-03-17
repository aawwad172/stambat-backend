using Stambat.Application.CQRS.Commands.Invitations;
using Stambat.Application.CQRS.Queries.Invitations;
using Stambat.Domain.Constants;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;
using Stambat.WebAPI.Routes.Invitations;

namespace Stambat.WebAPI.Endpoints;

public class InvitationModule : IEndpointModule
{
    private readonly string _group = "/invitations";
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder invitationGroup = app.MapGroup(_group)
            .WithTags(EndpointTags.Invitations);

        invitationGroup.MapGet(EndpointRoutes.ValidateInvitation, ValidateInvitation.RegisterRoute)
            .Produces<ApiResponse<ValidateInvitationQueryResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Accepts<ValidateInvitationQuery>("application/json");

        invitationGroup.MapPost(EndpointRoutes.AcceptInvitation, AcceptInvitation.RegisterRoute)
            .Produces<ApiResponse<AcceptInvitationCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Accepts<AcceptInvitationCommand>("application/json");
    }
}

