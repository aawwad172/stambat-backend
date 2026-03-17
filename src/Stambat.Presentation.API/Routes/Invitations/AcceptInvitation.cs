using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Invitations;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Invitations;

public class AcceptInvitation : ICommandRoute<AcceptInvitationCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] AcceptInvitationCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<AcceptInvitationCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            // Throw a custom ValidationException that your middleware will catch
            throw new CustomValidationException("Validation failed", errors);
        }

        AcceptInvitationCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<AcceptInvitationCommandResult>.SuccessResponse(response));
    }
}
