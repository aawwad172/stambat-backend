using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Invitations;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Invitations;

public class SetupCredentialsAndJoin : ICommandRoute<SetupCredentialsAndJoinCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] SetupCredentialsAndJoinCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<SetupCredentialsAndJoinCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        SetupCredentialsAndJoinCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<SetupCredentialsAndJoinCommandResult>.SuccessResponse(response));
    }
}
