using MediatR;

namespace Stambat.Application.CQRS.Commands.Tenants;

public sealed record RemoveStaffCommand(Guid StaffId) : IRequest<RemoveStaffCommandResult>;

public sealed record RemoveStaffCommandResult(string Message);
