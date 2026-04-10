namespace Stambat.Infrastructure.Clients.WalletPass.Options;

public class AppleWalletOptions
{
    public const string SectionName = "AppleWallet";

    public required string PassTypeIdentifier { get; set; }
    public required string TeamIdentifier { get; set; }
    public required string CertificatePath { get; set; }
    public required string CertificatePassword { get; set; }
    public required string WwdrCertificatePath { get; set; }
}
