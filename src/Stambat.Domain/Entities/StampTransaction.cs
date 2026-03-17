using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities;

public class StampTransaction : IBaseEntity
{
    public Guid Id { get; init; }

    // Link to the specific pass being stamped
    public Guid WalletPassId { get; set; }
    public virtual WalletPass WalletPass { get; set; } = null!;

    // The "Merchant" who performed the scan (from your Unified User Table)
    public Guid MerchantId { get; set; }
    public virtual User Merchant { get; set; } = null!;

    // Data for the transaction
    public int StampsAdded { get; set; } = 1; // Usually 1, but could be more for promos
    public string? Note { get; set; } // e.g., "Double stamp Tuesday"

    // Auditing
    public DateTime CreatedAt { get; set; } // When the stamp happened
    public Guid CreatedBy { get; set; }      // Usually same as MerchantId

    // Unused for transactions but kept for interface consistency
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
