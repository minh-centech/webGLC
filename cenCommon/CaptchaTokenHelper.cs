using System;
using System.Security.Cryptography;
using System.Text;

namespace cenCommon
{
    public static class CaptchaTokenHelper
    {
        private const int DefaultExpiryMinutes = 10;

        public static string GenerateCode(int length = 5)
        {
            const string alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var chars = new char[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                var buffer = new byte[length];
                rng.GetBytes(buffer);
                for (var i = 0; i < length; i++)
                {
                    chars[i] = alphabet[buffer[i] % alphabet.Length];
                }
            }

            return new string(chars);
        }

        public static string CreateToken(string captchaCode, string secretKey, int expiryMinutes = DefaultExpiryMinutes)
        {
            if (string.IsNullOrWhiteSpace(captchaCode))
            {
                throw new ArgumentException("Captcha code is required.", nameof(captchaCode));
            }

            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new ArgumentException("Secret key is required.", nameof(secretKey));
            }

            var normalizedCode = NormalizeCode(captchaCode);
            var expiresUtc = DateTime.UtcNow.AddMinutes(expiryMinutes);
            var payload = normalizedCode + "|" + expiresUtc.Ticks;
            var signature = ComputeSignature(payload, secretKey);

            return Base64UrlEncode(payload + "|" + signature);
        }

        public static bool ValidateToken(string captchaCode, string token, string secretKey)
        {
            if (string.IsNullOrWhiteSpace(captchaCode) || string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(secretKey))
            {
                return false;
            }

            string decoded;
            try
            {
                decoded = Base64UrlDecode(token);
            }
            catch
            {
                return false;
            }

            var parts = decoded.Split('|');
            if (parts.Length != 3)
            {
                return false;
            }

            var expectedCode = parts[0];
            long ticks;
            if (!long.TryParse(parts[1], out ticks))
            {
                return false;
            }

            var payload = parts[0] + "|" + parts[1];
            var expectedSignature = ComputeSignature(payload, secretKey);
            if (!FixedTimeEquals(parts[2], expectedSignature))
            {
                return false;
            }

            if (!string.Equals(expectedCode, NormalizeCode(captchaCode), StringComparison.Ordinal))
            {
                return false;
            }

            return new DateTime(ticks, DateTimeKind.Utc) >= DateTime.UtcNow;
        }

        private static string NormalizeCode(string value)
        {
            return (value ?? string.Empty).Trim().ToUpperInvariant();
        }

        private static string ComputeSignature(string payload, string secretKey)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                return Convert.ToBase64String(hash);
            }
        }

        private static string Base64UrlEncode(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value))
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        private static string Base64UrlDecode(string value)
        {
            var normalized = value.Replace('-', '+').Replace('_', '/');
            switch (normalized.Length % 4)
            {
                case 2:
                    normalized += "==";
                    break;
                case 3:
                    normalized += "=";
                    break;
            }

            return Encoding.UTF8.GetString(Convert.FromBase64String(normalized));
        }

        private static bool FixedTimeEquals(string left, string right)
        {
            var leftBytes = Encoding.UTF8.GetBytes(left ?? string.Empty);
            var rightBytes = Encoding.UTF8.GetBytes(right ?? string.Empty);

            if (leftBytes.Length != rightBytes.Length)
            {
                return false;
            }

            var diff = 0;
            for (var i = 0; i < leftBytes.Length; i++)
            {
                diff |= leftBytes[i] ^ rightBytes[i];
            }

            return diff == 0;
        }
    }
}
