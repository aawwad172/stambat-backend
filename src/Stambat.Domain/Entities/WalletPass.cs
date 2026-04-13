using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Domain;
using Stambat.Domain.Interfaces.Domain.Auditing;
using Stambat.Domain.ValueObjects;

namespace Stambat.Domain.Entities;

public class WalletPass : IBaseEntity, IAggregateRoot
{
    public Guid Id { get; init; }

    // The "Who"
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;

    // The "What"
    public Guid CardTemplateId { get; set; }
    public virtual CardTemplate CardTemplate { get; set; } = null!;

    // Progress
    public decimal CurrentBalance { get; private set; }
    public decimal RequiredBalance { get; private set; }
    public RedemptionType RedemptionType { get; private set; } = RedemptionType.Stamps;
    public WalletPassStatus Status { get; private set; } = WalletPassStatus.Active;

    // Expiry (for Expirable card types)
    public DateTime? ExpiresAt { get; private set; }

    // Wallet Integration
    public WalletProviderType ProviderType { get; private set; }
    public string? ApplePassSerialNumber { get; private set; }
    public string? GooglePayId { get; private set; }

    // QR Token (encrypted payload stored for reference)
    public string? QrTokenPayload { get; private set; }

    // Tracking
    public DateTime? LastProgressAt { get; private set; }
    public DateTime? RedeemedAt { get; private set; }

    // Audit (IBaseEntity)
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    // Navigation Property
    public virtual ICollection<StampTransaction> Transactions { get; set; } = [];

    // Factory Method
    public static WalletPass Create(
        Guid userId,
        Guid cardTemplateId,
        WalletProviderType providerType,
        decimal requiredBalance,
        RedemptionType redemptionType,
        CardType cardType,
        int? expiryDurationInDays)
    {
        Guard.AgainstDefault(userId, nameof(userId));
        Guard.AgainstDefault(cardTemplateId, nameof(cardTemplateId));
        Guard.AgainstNegativeOrZero(requiredBalance, nameof(requiredBalance));

        DateTime? expiresAt = cardType == CardType.Expirable && expiryDurationInDays.HasValue
            ? DateTime.UtcNow.AddDays(expiryDurationInDays.Value)
            : null;

        return new WalletPass
        {
            Id = IdGenerator.New(),
            UserId = userId,
            CardTemplateId = cardTemplateId,
            ProviderType = providerType,
            CurrentBalance = 0,
            RequiredBalance = requiredBalance,
            RedemptionType = redemptionType,
            Status = WalletPassStatus.Active,
            ExpiresAt = expiresAt
        };
    }

    // Behavior Methods
    public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

    public bool CanAddProgress() => Status == WalletPassStatus.Active && !IsExpired;

    public bool CanRedeem() => Status == WalletPassStatus.Completed;

    public bool TryEnforceExpiry()
    {
        if (Status == WalletPassStatus.Active && IsExpired)
        {
            Status = WalletPassStatus.Expired;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Adds progress to the wallet pass.
    /// For Stamps: <paramref name="rawAmount"/> is the stamp count (must be whole number, max 5).
    /// For Points: <paramref name="rawAmount"/> is the purchase amount, converted using PointsPerCurrencyUnit.
    /// </summary>
    public void AddProgress(decimal rawAmount)
    {
        if (!CanAddProgress())
            throw new BusinessRuleException($"Cannot add progress to a wallet pass with status '{Status}'.");

        if (rawAmount <= 0)
            throw new BusinessRuleException("Amount must be positive.");

        decimal progressToAdd;
        if (RedemptionType == RedemptionType.Points)
        {
            if (CardTemplate.PointsPerCurrencyUnit is null or <= 0)
                throw new BusinessRuleException("Points per currency unit is not configured for this card.");

            progressToAdd = rawAmount * CardTemplate.PointsPerCurrencyUnit.Value;
        }
        else
        {
            if (rawAmount != Math.Floor(rawAmount))
                throw new BusinessRuleException("Stamp count must be a whole number.");

            if (rawAmount > 5)
                throw new BusinessRuleException("Cannot add more than 5 stamps at once.");

            progressToAdd = rawAmount;
        }

        CurrentBalance += progressToAdd;
        LastProgressAt = DateTime.UtcNow;

        if (CurrentBalance >= RequiredBalance)
        {
            CurrentBalance = RequiredBalance; // Cap at max
            Status = WalletPassStatus.Completed;
        }
    }

    public void Redeem()
    {
        if (!CanRedeem())
            throw new BusinessRuleException($"Cannot redeem a wallet pass with status '{Status}'.");

        Status = WalletPassStatus.Redeemed;
        RedeemedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status is WalletPassStatus.Redeemed or WalletPassStatus.Cancelled)
            throw new BusinessRuleException($"Cannot cancel a wallet pass with status '{Status}'.");

        Status = WalletPassStatus.Cancelled;
    }

    public void SetWalletIds(string? appleSerial, string? googlePayId)
    {
        ApplePassSerialNumber = appleSerial;
        GooglePayId = googlePayId;
    }

    public void SetQrToken(string qrTokenPayload)
    {
        Guard.AgainstNullOrEmpty(qrTokenPayload, nameof(qrTokenPayload));
        QrTokenPayload = qrTokenPayload;
    }

    public WalletPassUpdateRequest BuildUpdateRequest() =>
        new(
            WalletPassId: Id,
            ApplePassSerialNumber: ApplePassSerialNumber,
            GooglePayId: GooglePayId,
            CurrentBalance: CurrentBalance,
            RequiredBalance: RequiredBalance,
            RedemptionType: RedemptionType,
            Status: Status,
            QrCodeContent: QrTokenPayload,
            LogoUrl: CardTemplate.LogoUrlOverride,
            PrimaryColor: CardTemplate.PrimaryColorOverride,
            SecondaryColor: CardTemplate.SecondaryColorOverride,
            ExpiresAt: ExpiresAt);
}
