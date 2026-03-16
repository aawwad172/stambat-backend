using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stamply.Application.CQRS.Queries.Invitations;
using Stamply.Domain.Exceptions;
using Stamply.Presentation.API.Interfaces;
using Stamply.Presentation.API.Models;

namespace Stamply.Presentation.API.Routes.Invitations;

public class ValidateInvitation : IParameterizedQueryRoute<ValidateInvitationQuery>
{
    public static async Task<IResult> RegisterRoute(
        [AsParameters] ValidateInvitationQuery query,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<ValidateInvitationQuery> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            // Throw a custom ValidationException that your middleware will catch
            throw new CustomValidationException("Validation failed", errors);
        }

        ValidateInvitationQueryResult response = await mediator.Send(query);
        return Results.Ok(
            ApiResponse<ValidateInvitationQueryResult>.SuccessResponse(response));
    }
}
