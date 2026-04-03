using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using webGLCv2.Models;

namespace webGLCv2.Services;

public sealed class TurnstileValidationService
{
    private readonly HttpClient _httpClient;
    private readonly TurnstileOptions _options;

    public TurnstileValidationService(HttpClient httpClient, IOptions<TurnstileOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<bool> ValidateAsync(string token, string? remoteIp, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_options.SecretKey))
        {
            throw new InvalidOperationException("Missing configuration: CloudflareTurnstile:SecretKey");
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        using var payload = new FormUrlEncodedContent(BuildPayload(token, remoteIp));
        using var response = await _httpClient.PostAsync(_options.VerifyUrl, payload, cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TurnstileVerifyResponse>(cancellationToken: cancellationToken);
        return result?.Success == true;
    }

    private Dictionary<string, string> BuildPayload(string token, string? remoteIp)
    {
        var payload = new Dictionary<string, string>
        {
            ["secret"] = _options.SecretKey,
            ["response"] = token
        };

        if (!string.IsNullOrWhiteSpace(remoteIp))
        {
            payload["remoteip"] = remoteIp;
        }

        return payload;
    }
}
