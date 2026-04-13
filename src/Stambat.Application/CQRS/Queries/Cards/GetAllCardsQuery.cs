using MediatR;

using Stambat.Domain.Enums;

namespace Stambat.Application.CQRS.Queries.Cards;

public sealed record GetAllCardsQuery(
    int Page = 1,
    int Size = 10,
    bool? IsActive = null,
    string SortBy = "CreatedAt",
    bool SortDescending = true) : IRequest<GetAllCardsQueryResult>;

public sealed record CardRecord(
    Guid Id,
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
    string? TermsAndConditions,
    string? JoinUrl,
    string? JoinQrCodeBase64,
    bool IsActive,
    DateTime CreatedAt);

public sealed record GetAllCardsQueryResult(
    IEnumerable<CardRecord> Cards,
    int TotalRecords,
    int TotalDisplayRecords);
