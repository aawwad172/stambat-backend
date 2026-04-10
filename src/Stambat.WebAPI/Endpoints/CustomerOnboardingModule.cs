using Stambat.Application.CQRS.Commands.Wallet;
using Stambat.Domain.Constants;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;
using Stambat.WebAPI.Routes.Wallet;

namespace Stambat.WebAPI.Endpoints;

public class CustomerOnboardingModule : IEndpointModule
{
    private readonly string _group = "/public/onboarding";

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder onboarding = app.MapGroup(_group)
            .WithTags(EndpointTags.CustomerOnboarding);

        // Customer onboarding is public (no auth required)
        onboarding.MapPost(EndpointRoutes.CustomerOnboard, CustomerOnboard.RegisterRoute)
            .AllowAnonymous()
            .Accepts<CustomerOnboardCommand>("application/json")
            .Produces<ApiResponse<CustomerOnboardCommandResult>>(StatusCodes.Status200OK, "application/json")
            .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ApiResponse<string>>(StatusCodes.Status409Conflict, "application/json");
    }
}
