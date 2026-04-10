using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Queries.Cards;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Cards;

public class GetCardById : IParameterizedQueryRoute<GetCardByIdQuery>
{
    public static async Task<IResult> RegisterRoute(
        [AsParameters] GetCardByIdQuery query,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<GetCardByIdQuery> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        GetCardByIdQueryResult response = await mediator.Send(query);
        return Results.Ok(
            ApiResponse<CardRecord>.SuccessResponse(response.Card));
    }
}
