using MediatR;

namespace Stambat.Application.CQRS.Commands.Authentication;

public record LogoutCommand(string? RefreshToken) : IRequest<LogoutCommandResult>;

public record LogoutCommandResult(string Message);
