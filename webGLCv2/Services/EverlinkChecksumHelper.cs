using System;
using System.Security.Cryptography;
using System.Text;

public static class EverlinkChecksumHelper
{
    private const string PrivateKey = "Everlink@2026@$#@!";

    public class HashResult
    {
        public long t { get; set; }
        public string hash { get; set; } = string.Empty;
    }

    /// <summary>
    /// Tạo hash từ chuỗi: privateKey + payload + t
    /// </summary>
    public static HashResult CreateHash(string payload)
    {
        long t = GetUnixTimestampSeconds();

        string raw = PrivateKey + payload + t;
        string hash = Sha256(raw);

        return new HashResult
        {
            t = t,
            hash = hash
        };
    }

    /// <summary>
    /// Kiểm tra checksum
    /// </summary>
    public static bool VerifyChecksum(string payload, string hash, long t)
    {
        if (string.IsNullOrWhiteSpace(hash))
            return false;

        string raw = PrivateKey + payload + t;
        string expectedHash = Sha256(raw);

        return string.Equals(expectedHash, hash, StringComparison.OrdinalIgnoreCase);
    }

    private static string Sha256(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }

    private static long GetUnixTimestampSeconds()
    {
        return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
    }
}
