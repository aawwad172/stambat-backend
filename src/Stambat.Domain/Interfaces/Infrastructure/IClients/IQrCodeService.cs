namespace Stambat.Domain.Interfaces.Infrastructure.IClients;

public interface IQrCodeService
{
    byte[] GenerateQrCodePng(string content, int pixelSize = 300);
    string GenerateQrCodeBase64(string content, int pixelSize = 300);
}
