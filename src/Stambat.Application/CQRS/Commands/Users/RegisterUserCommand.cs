using MediatR;

using Stambat.Domain.ValueObjects;

namespace Stambat.Application.CQRS.Commands.Users;

public record RegisterUserCommand(
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string Username,
    string Password) : IRequest<RegisterUserCommandResult>;

public record RegisterUserCommandResult(
    Guid Id,
    FullName FullName,
    string Email,
    string Username,
    bool IsActive,
    bool IsVerified,
    string Message
);
