using System;

/// <summary>
/// Provides helper methods for security-related operations.
/// </summary>
public static class SecurityHelper
{
    /// <summary>
    /// Generates a HMACSHA256 signature for the specified payload using the specified signing key.
    /// </summary>
    /// <param name="singingKey">The signing key.</param>
    /// <param name="payload">The payload to sign.</param>
    /// <returns>The HMACSHA256 signature.</returns>
    public static string GenerateHMACSHA256Signature(string singingKey, string payload)
    {
        var encoding = new System.Text.UTF8Encoding();
        // Convert the key string to a byte array.
        byte[] keyByte = encoding.GetBytes(singingKey);
        byte[] messageBytes = encoding.GetBytes(payload);

        // Create an instance of HMACSHA256.
        using var hmacsha256 = new System.Security.Cryptography.HMACSHA256(keyByte);

        // Compute the hash.
        byte[] hashMessage = hmacsha256.ComputeHash(messageBytes);

        return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
    }

    /// <summary>
    /// Verifies the HMACSHA256 signature for the specified payload using the specified signing key.
    /// </summary>
    /// <param name="singingKey">The signing key.</param>
    /// <param name="payload">The payload to sign.</param>
    /// <param name="signature">The HMACSHA256 signature.</param>
    /// <returns>The result of the verification.</returns>
    public static bool VerifyHMACSHA256Signature(string singingKey, string payload, string signature)
    {
        return GenerateHMACSHA256Signature(singingKey, payload) == signature;
    }
}
