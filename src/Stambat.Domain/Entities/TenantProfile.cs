using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities;

public class TenantProfile : IBaseEntity
{
    public Guid Id { get; init; }
    public required string Slug { get; set; } // Unique index in DB
    public string? LogoUrl { get; set; }
    public string PrimaryColor { get; set; } = "#000000"; // Default Black
    public string SecondaryColor { get; set; } = "#FFFFFF"; // Default White

    // Audit
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    public required Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
}
