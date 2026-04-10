using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Cards;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Cards;

public class DeleteCard : ICommandRoute<DeleteCardCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] DeleteCardCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<DeleteCardCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        DeleteCardCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<DeleteCardCommandResult>.SuccessResponse(response));
    }
}
