namespace webGLCv2.Models;

public sealed class TurnstileOptions
{
    public const string SectionName = "CloudflareTurnstile";

    public bool EnableValidation { get; set; } = true;

    public string SiteKey { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public string VerifyUrl { get; set; } = "https://challenges.cloudflare.com/turnstile/v0/siteverify";
}
