using Stambat.Domain.Enums;
using Stambat.Domain.Interfaces.Infrastructure.IClients;

namespace Stambat.Infrastructure.Clients.WalletPass;

public class WalletPassProviderFactory(IEnumerable<IWalletPassProvider> providers) : IWalletPassProviderFactory
{
    private readonly IEnumerable<IWalletPassProvider> _providers = providers;

    public IWalletPassProvider GetProvider(WalletProviderType providerType)
    {
        return _providers.FirstOrDefault(p => p.ProviderType == providerType)
            ?? throw new NotSupportedException($"Wallet provider '{providerType}' is not supported.");
    }
}
