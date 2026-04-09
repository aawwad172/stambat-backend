using MediatR;

namespace Stambat.Application.CQRS.Commands.Tenants;

public sealed record DeactivateStaffCommand(Guid StaffId) : IRequest<DeactivateStaffCommandResult>;

public sealed record DeactivateStaffCommandResult(string Message);
