using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Domain;
using Stambat.Domain.Interfaces.Domain.Auditing;

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
    public int CurrentStamps { get; private set; }
    public WalletPassStatus Status { get; private set; } = WalletPassStatus.Active;

    // Wallet Integration
    public WalletProviderType ProviderType { get; private set; }
    public string? ApplePassSerialNumber { get; private set; }
    public string? GooglePayId { get; private set; }

    // QR Token (encrypted payload stored for reference)
    public string? QrTokenPayload { get; private set; }

    // Redemption Tracking
    public DateTime? LastStampedAt { get; private set; }
    public DateTime? RedeemedAt { get; private set; }
    public int RedemptionCount { get; private set; }

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
        WalletProviderType providerType)
    {
        Guard.AgainstDefault(userId, nameof(userId));
        Guard.AgainstDefault(cardTemplateId, nameof(cardTemplateId));

        return new WalletPass
        {
            Id = IdGenerator.New(),
            UserId = userId,
            CardTemplateId = cardTemplateId,
            ProviderType = providerType,
            CurrentStamps = 0,
            Status = WalletPassStatus.Active,
            RedemptionCount = 0
        };
    }

    // Behavior Methods
    public bool CanStamp() => Status == WalletPassStatus.Active;

    public bool CanRedeem() => Status == WalletPassStatus.Completed;

    public void AddStamp(int count)
    {
        if (!CanStamp())
            throw new BusinessRuleException($"Cannot stamp a wallet pass with status '{Status}'.");

        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Stamp count must be positive.");

        CurrentStamps += count;
        LastStampedAt = DateTime.UtcNow;

        if (CurrentStamps >= CardTemplate.StampsRequired)
        {
            CurrentStamps = CardTemplate.StampsRequired; // Cap at max
            Status = WalletPassStatus.Completed;
        }
    }

    public void Redeem()
    {
        if (!CanRedeem())
            throw new BusinessRuleException($"Cannot redeem a wallet pass with status '{Status}'.");

        Status = WalletPassStatus.Redeemed;
        RedeemedAt = DateTime.UtcNow;
        RedemptionCount++;
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
}
