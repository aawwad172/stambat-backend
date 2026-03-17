namespace Stambat.Domain.Interfaces.Application.Services;

public interface ITenantProviderService
{
    Guid? TenantId { get; set; }
}
