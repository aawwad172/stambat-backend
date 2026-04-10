using Stambat.Domain.Enums;
using Stambat.Domain.ValueObjects;

namespace Stambat.Domain.Interfaces.Infrastructure.IClients;

public interface IWalletPassProvider
{
    WalletProviderType ProviderType { get; }
    Task<WalletClassResult> CreateClassAsync(WalletClassRequest request, CancellationToken cancellationToken = default);
    Task UpdateClassAsync(WalletClassRequest request, string classId, CancellationToken cancellationToken = default);
    Task<WalletPassResult> CreatePassAsync(WalletPassRequest request, CancellationToken cancellationToken = default);
    Task<WalletPassResult> UpdatePassAsync(WalletPassUpdateRequest request, CancellationToken cancellationToken = default);
}
