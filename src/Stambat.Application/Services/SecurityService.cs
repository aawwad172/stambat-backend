using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Configuration;

using Stambat.Application.Utilities;
using Stambat.Domain.Interfaces.Application.Services;

namespace Stambat.Application.Services;

public class SecurityService(IConfiguration configuration) : ISecurityService
{
    private readonly byte[] _encryptionKey = Encoding.UTF8.GetBytes(configuration.GetRequiredSetting("Security:EncryptionKey"));

    public string HashSecret(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public string HashToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be null or empty.", nameof(token));

        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(bytes); // Deterministic!
    }

    public string EncryptString(string text)
    {
        using AesGcm aesGcm = new(_encryptionKey, AesGcm.TagByteSizes.MaxSize);
        byte[] nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
        RandomNumberGenerator.Fill(nonce);

        byte[] plainBytes = Encoding.UTF8.GetBytes(text);
        byte[] cipherBytes = new byte[plainBytes.Length];
        byte[] tag = new byte[AesGcm.TagByteSizes.MaxSize];

        aesGcm.Encrypt(nonce, plainBytes, cipherBytes, tag);

        // Combine nonce, tag, and ciphertext into one array for storage or transmission.
        byte[] result = new byte[nonce.Length + tag.Length + cipherBytes.Length];
        Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
        Buffer.BlockCopy(tag, 0, result, nonce.Length, tag.Length);
        Buffer.BlockCopy(cipherBytes, 0, result, nonce.Length + tag.Length, cipherBytes.Length);

        return Convert.ToBase64String(result);
    }

    public string DecryptString(string encryptedText)
    {
        // Convert the Base64-encoded input back into bytes
        byte[] encryptedData = Convert.FromBase64String(encryptedText);

        // Define the sizes of the nonce and tag based on AES-GCM specifications
        int nonceLength = AesGcm.NonceByteSizes.MaxSize; // Typically 12 bytes
        int tagLength = AesGcm.TagByteSizes.MaxSize;       // Typically 16 bytes

        // Allocate buffers for nonce, tag, and ciphertext
        byte[] nonce = new byte[nonceLength];
        byte[] tag = new byte[tagLength];
        byte[] ciphertext = new byte[encryptedData.Length - nonceLength - tagLength];

        // Extract the nonce (first part)
        Buffer.BlockCopy(encryptedData, 0, nonce, 0, nonceLength);
        // Extract the tag (next part)
        Buffer.BlockCopy(encryptedData, nonceLength, tag, 0, tagLength);
        // Extract the ciphertext (remaining bytes)
        Buffer.BlockCopy(encryptedData, nonceLength + tagLength, ciphertext, 0, ciphertext.Length);

        byte[] decryptedBytes = new byte[ciphertext.Length];

        // Use AesGcm to decrypt; it will verify the tag automatically
        using AesGcm aesGcm = new(_encryptionKey, AesGcm.TagByteSizes.MaxSize);
        aesGcm.Decrypt(nonce, ciphertext, tag, decryptedBytes);

        // Convert the decrypted bytes to a UTF8 string and return it
        return Encoding.UTF8.GetString(decryptedBytes);
    }

    public bool VerifySecret(string password, string passwordHash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        catch (BCrypt.Net.SaltParseException)
        {
            return false;
        }
    }
}
