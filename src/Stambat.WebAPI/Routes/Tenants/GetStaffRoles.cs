using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Queries.Tenants;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Tenants;

public class GetStaffRoles : IQueryRoute<GetStaffRolesQuery>
{
    public static async Task<IResult> RegisterRoute(
        [AsParameters] GetStaffRolesQuery query,
        [FromServices] IMediator mediator)
    {
        GetStaffRolesQueryResult response = await mediator.Send(query);
        return Results.Ok(
            ApiResponse<IEnumerable<AvailableRoleRecord>>.SuccessResponse(response.Roles));
    }
}
