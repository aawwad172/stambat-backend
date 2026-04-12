namespace Stambat.Domain.Exceptions;

public class WalletProviderException : Exception
{
    public WalletProviderException()
        : base("A wallet provider error occurred.")
    {
    }

    public WalletProviderException(string message)
        : base(message)
    {
    }

    public WalletProviderException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
