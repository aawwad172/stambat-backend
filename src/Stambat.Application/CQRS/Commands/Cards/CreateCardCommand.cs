using MediatR;

using Stambat.Domain.Enums;

namespace Stambat.Application.CQRS.Commands.Cards;

public sealed record CreateCardCommand(
    string Title,
    string? Description,
    decimal RequiredBalance,
    string? RewardDescription,
    CardType CardType,
    int? ExpiryDurationInDays,
    RedemptionType RedemptionType,
    decimal? PointsPerCurrencyUnit,
    string? PrimaryColorOverride,
    string? SecondaryColorOverride,
    string? LogoUrlOverride,
    string? EmptyStampUrl,
    string? EarnedStampUrl,
    string? TermsAndConditions) : IRequest<CreateCardCommandResult>;

public sealed record CreateCardCommandResult(Guid CardTemplateId);
