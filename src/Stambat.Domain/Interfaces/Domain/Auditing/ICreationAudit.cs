namespace Stambat.Domain.Interfaces.Domain.Auditing;

public interface ICreationAudit
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}
