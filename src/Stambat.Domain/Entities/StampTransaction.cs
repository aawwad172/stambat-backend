using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Enums;
using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities;

public class StampTransaction : IBaseEntity
{
    public Guid Id { get; init; }

    // Link to the specific pass
    public Guid WalletPassId { get; set; }
    public virtual WalletPass WalletPass { get; set; } = null!;

    // The "Merchant" who performed the scan (from your Unified User Table)
    public Guid MerchantId { get; set; }
    public virtual User Merchant { get; set; } = null!;

    // Transaction Type
    public StampTransactionType Type { get; set; } = StampTransactionType.Stamp;

    // Data for the transaction
    public decimal AmountAdded { get; set; } // Stamps count or points earned
    public string? Note { get; set; } // e.g., "Double stamp Tuesday"

    // Auditing
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    // Factory Method
    public static StampTransaction Create(
        Guid walletPassId,
        Guid merchantId,
        decimal amountAdded,
        StampTransactionType type,
        string? note = null)
    {
        Guard.AgainstDefault(walletPassId, nameof(walletPassId));
        Guard.AgainstDefault(merchantId, nameof(merchantId));
        Guard.AgainstNegative(amountAdded, nameof(amountAdded));

        return new StampTransaction
        {
            Id = IdGenerator.New(),
            WalletPassId = walletPassId,
            MerchantId = merchantId,
            AmountAdded = amountAdded,
            Type = type,
            Note = note
        };
    }
}
