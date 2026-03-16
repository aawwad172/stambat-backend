using Stamply.Application.CQRS.Commands.Invitations;
using Stamply.Application.CQRS.Queries.Invitations;
using Stamply.Domain.Constants;
using Stamply.Presentation.API.Interfaces;
using Stamply.Presentation.API.Models;
using Stamply.Presentation.API.Routes.Invitations;

namespace Stamply.Presentation.API.Endpoints;

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

