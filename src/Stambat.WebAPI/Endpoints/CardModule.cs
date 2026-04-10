using Stambat.Application.CQRS.Commands.Cards;
using Stambat.Application.CQRS.Queries.Cards;
using Stambat.Domain.Constants;
using Stambat.Domain.Enums;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;
using Stambat.WebAPI.Routes.Cards;

namespace Stambat.WebAPI.Endpoints;

public class CardModule : IEndpointModule
{
    private readonly string _group = "/cards";

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder cards = app.MapGroup(_group)
            .WithTags(EndpointTags.Cards);

        cards.MapPost(EndpointRoutes.CreateCard, CreateCard.RegisterRoute)
            .RequireAuthorization(PermissionConstants.CardsAdd)
            .Accepts<CreateCardCommand>("application/json")
            .Produces<ApiResponse<CreateCardCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json");

        cards.MapGet(EndpointRoutes.GetAllCards, GetAllCards.RegisterRoute)
            .RequireAuthorization(PermissionConstants.CardsView)
            .Produces<ApiResponse<GetAllCardsQueryResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json");

        cards.MapGet(EndpointRoutes.GetCardById, GetCardById.RegisterRoute)
            .RequireAuthorization(PermissionConstants.CardsView)
            .Produces<ApiResponse<CardRecord>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<string>>(StatusCodes.Status404NotFound, "application/json");

        cards.MapPut(EndpointRoutes.UpdateCard, UpdateCard.RegisterRoute)
            .RequireAuthorization(PermissionConstants.CardsEdit)
            .Accepts<UpdateCardCommand>("application/json")
            .Produces<ApiResponse<UpdateCardCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<string>>(StatusCodes.Status404NotFound, "application/json");

        cards.MapDelete(EndpointRoutes.DeleteCard, DeleteCard.RegisterRoute)
            .RequireAuthorization(PermissionConstants.CardsDelete)
            .Accepts<DeleteCardCommand>("application/json")
            .Produces<ApiResponse<DeleteCardCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<string>>(StatusCodes.Status404NotFound, "application/json");
    }
}
