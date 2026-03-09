using MediatR;

namespace Stamply.Application.CQRS.Commands.Authentication;

public sealed record VerifyEmailCommand(Guid UserId, string Token) : IRequest<VerifyEmailCommandResult>;

public sealed record VerifyEmailCommandResult(
    string AccessToken,
    string RefreshToken);
