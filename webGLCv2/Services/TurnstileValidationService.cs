using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using webGLCv2.Models;

namespace webGLCv2.Services;

public sealed class TurnstileValidationService
{
    private readonly HttpClient _httpClient;
    private readonly TurnstileOptions _options;

    public bool IsValidationEnabled => _options.EnableValidation;

    public TurnstileValidationService(HttpClient httpClient, IOptions<TurnstileOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<bool> ValidateAsync(string token, string? remoteIp, CancellationToken cancellationToken = default)
    {
        var result = await ValidateDetailedAsync(token, remoteIp, cancellationToken);
        return result.Success;
    }

    public async Task<(bool Success, string? ErrorDetail)> ValidateDetailedAsync(
        string token,
        string? remoteIp,
        CancellationToken cancellationToken = default)
    {
        if (!_options.EnableValidation)
        {
            return (true, null);
        }

        if (string.IsNullOrWhiteSpace(_options.SecretKey))
        {
            throw new InvalidOperationException("Missing configuration: CloudflareTurnstile:SecretKey");
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            return (false, "missing-input-response");
        }

        using var payload = new FormUrlEncodedContent(BuildPayload(token, remoteIp));
        using var response = await _httpClient.PostAsync(_options.VerifyUrl, payload, cancellationToken);
        response.EnsureSuccessStatusCode();

        var verifyResponse = await response.Content.ReadFromJsonAsync<TurnstileVerifyResponse>(cancellationToken: cancellationToken);
        if (verifyResponse?.Success == true)
        {
            return (true, null);
        }

        var errorDetail = verifyResponse?.ErrorCodes is { Count: > 0 }
            ? string.Join(",", verifyResponse.ErrorCodes)
            : "unknown-turnstile-error";

        return (false, errorDetail);
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
