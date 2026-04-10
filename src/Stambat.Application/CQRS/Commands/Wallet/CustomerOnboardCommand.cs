using MediatR;

using Stambat.Domain.Enums;

namespace Stambat.Application.CQRS.Commands.Wallet;

public sealed record CustomerOnboardCommand(
    Guid CardTemplateId,
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    WalletProviderType WalletProvider) : IRequest<CustomerOnboardCommandResult>;

public sealed record CustomerOnboardCommandResult(
    Guid WalletPassId,
    string? GoogleSaveUrl);
