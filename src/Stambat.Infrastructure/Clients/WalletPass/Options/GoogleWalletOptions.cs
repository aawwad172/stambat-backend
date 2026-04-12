namespace Stambat.Infrastructure.Clients.WalletPass.Options;

public class GoogleWalletOptions
{
    public const string SectionName = "GoogleWallet";

    public required string IssuerId { get; set; }
    public required string ServiceAccountKeyPath { get; set; }
    public string ApplicationName { get; set; } = "Stambat";
}
