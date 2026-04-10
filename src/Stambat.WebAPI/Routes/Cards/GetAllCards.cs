using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Queries.Cards;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Cards;

public class GetAllCards : IParameterizedQueryRoute<GetAllCardsQuery>
{
    public static async Task<IResult> RegisterRoute(
        [AsParameters] GetAllCardsQuery query,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<GetAllCardsQuery> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        GetAllCardsQueryResult response = await mediator.Send(query);
        return Results.Ok(
            ApiResponse<GetAllCardsQueryResult>.SuccessResponse(response));
    }
}
