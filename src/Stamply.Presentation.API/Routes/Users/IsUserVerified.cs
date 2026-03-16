using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stamply.Application.CQRS.Queries.Users;
using Stamply.Domain.Exceptions;
using Stamply.Presentation.API.Interfaces;
using Stamply.Presentation.API.Models;

namespace Stamply.Presentation.API.Routes.Users;

public class IsUserVerified : IParameterizedQueryRoute<IsUserVerifiedQuery>
{
    public static async Task<IResult> RegisterRoute(
        [AsParameters] IsUserVerifiedQuery query,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<IsUserVerifiedQuery> validator)
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

        IsUserVerifiedQueryResult response = await mediator.Send(query);
        return Results.Ok(
            ApiResponse<IsUserVerifiedQueryResult>.SuccessResponse(response));
    }
}
