using Stambat.Domain.Enums;

namespace Stambat.Domain.Interfaces.Infrastructure.IClients;

public interface IWalletPassProviderFactory
{
    IWalletPassProvider GetProvider(WalletProviderType providerType);
}
