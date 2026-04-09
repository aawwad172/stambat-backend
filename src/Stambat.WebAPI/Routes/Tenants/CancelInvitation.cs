using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Tenants;

public class CancelInvitation : IParameterizedQueryRoute<CancelInvitationCommand>
{
    public static async Task<IResult> RegisterRoute(
        [AsParameters] CancelInvitationCommand command,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<CancelInvitationCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        CancelInvitationCommandResult response = await mediator.Send(command);
        return Results.Ok(
            ApiResponse<CancelInvitationCommandResult>.SuccessResponse(response));
    }
}
