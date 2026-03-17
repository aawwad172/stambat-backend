using System;

using Stambat.Domain.Interfaces.Application.Services;

namespace Stambat.Application.Services;

public class TenantProviderService : ITenantProviderService
{
    public Guid? TenantId { get; set; }
}
