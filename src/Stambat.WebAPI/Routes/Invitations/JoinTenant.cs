using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Invitations;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Invitations;

public class JoinTenant : ICommandRoute<JoinTenantCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] JoinTenantCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<JoinTenantCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        JoinTenantCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<JoinTenantCommandResult>.SuccessResponse(response));
    }
}
