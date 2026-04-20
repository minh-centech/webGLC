using System.Text.Json.Serialization;

namespace webGLCv2.Models;

public sealed class TurnstileVerifyResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("error-codes")]
    public List<string>? ErrorCodes { get; set; }
}
