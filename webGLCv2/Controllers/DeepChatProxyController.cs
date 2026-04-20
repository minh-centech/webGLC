using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;

namespace webGLCv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeepChatProxyController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AiProxyOptions _aiOptions;
        private readonly ILogger<DeepChatProxyController> _logger;
        private static int _activeChatSessions;
        private const int MaxConcurrentChatSessions = 3;

        public DeepChatProxyController(
            IHttpClientFactory httpClientFactory,
            IOptions<AiProxyOptions> aiOptions,
            ILogger<DeepChatProxyController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _aiOptions = aiOptions.Value;
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] DeepChatRequest request, CancellationToken cancellationToken)
        {
            var acquiredSlot = false;
            try
            {
                var currentSessions = Interlocked.Increment(ref _activeChatSessions);
                if (currentSessions > MaxConcurrentChatSessions)
                {
                    Interlocked.Decrement(ref _activeChatSessions);
                    return StatusCode(StatusCodes.Status429TooManyRequests, new DeepChatResponse
                    {
                        Text = "Hệ thống đang thử nghiệm, đang có nhiều phiên làm việc, bạn thử lại sau."
                    });
                }

                acquiredSlot = true;

                if (request == null || request.Messages == null || request.Messages.Count == 0)
                {
                    return BadRequest(new DeepChatResponse
                    {
                        Text = "Không nhận được nội dung tin nhắn."
                    });
                }

                var lastUserMessage = request.Messages
                    .LastOrDefault(x => string.Equals(x.Role, "user", StringComparison.OrdinalIgnoreCase));

                if (lastUserMessage == null || string.IsNullOrWhiteSpace(lastUserMessage.Text))
                {
                    return BadRequest(new DeepChatResponse
                    {
                        Text = "Tin nhắn người dùng không hợp lệ."
                    });
                }

                var userMessage = lastUserMessage.Text.Trim();

                var replyText = await CallOpenAiCompatibleApiAsync(userMessage, cancellationToken);
                //Test
                //var replyText = await CallOpenAiCompatibleApiAsyncTest(userMessage, cancellationToken);

                if (string.IsNullOrWhiteSpace(replyText))
                {
                    replyText = "Tôi chưa nhận được phản hồi hợp lệ từ hệ thống AI.";
                }

                return Ok(new DeepChatResponse
                {
                    Text = replyText
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xử lý DeepChatProxy.");

                return StatusCode(500, new DeepChatResponse
                {
                    Text = "Đã xảy ra lỗi trong quá trình xử lý yêu cầu."
                });
            }
            finally
            {
                if (acquiredSlot)
                {
                    Interlocked.Decrement(ref _activeChatSessions);
                }
            }
        }

        private Task<string> CallOpenAiCompatibleApiAsyncTest(string userMessage, CancellationToken cancellationToken)
        {
            return Task.FromResult("Hệ thống đang phát triển! Vui lòng thử lại sau");
        }
        private async Task<string> CallOpenAiCompatibleApiAsync(string userMessage, CancellationToken cancellationToken)
        {
            
            var client = _httpClientFactory.CreateClient();

            client.Timeout = TimeSpan.FromSeconds(120);

            if (!string.IsNullOrWhiteSpace(_aiOptions.BaseUrl))
            {
                client.BaseAddress = new Uri(_aiOptions.BaseUrl);
            }

            if (!string.IsNullOrWhiteSpace(_aiOptions.ApiKey))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _aiOptions.ApiKey);
            }

            var payload = new OpenAiChatCompletionRequest
            {
                Model = _aiOptions.Model,
                Messages = new List<OpenAiChatMessage>
                {
                    new OpenAiChatMessage
                    {
                        Role = "system",
                        Content = _aiOptions.SystemPrompt ?? "Bạn là trợ lý AI hữu ích."
                    },
                    new OpenAiChatMessage
                    {
                        Role = "user",
                        Content = userMessage
                    }
                },
                Temperature = 0
            };

            var json = JsonSerializer.Serialize(payload);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var endpoint = string.IsNullOrWhiteSpace(_aiOptions.ChatEndpoint)
                ? "/v1/chat/completions"
                : _aiOptions.ChatEndpoint;

            var requestUri = client.BaseAddress is null
                ? endpoint
                : new Uri(client.BaseAddress, endpoint).ToString();

            using var response = await client.PostAsync(endpoint, content, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("AI API lỗi. Status: {StatusCode}, Url: {Url}, Body: {Body}",
                    (int)response.StatusCode, requestUri, responseContent);

                //return $"AI API lỗi: {(int)response.StatusCode}. URL: {requestUri}";
                return $"Hệ thống đang gặp vấn đề! Vui lòng thử lại sau 1 phút nữa";
            }

            var aiResponse = JsonSerializer.Deserialize<OpenAiChatCompletionResponse>(
                responseContent,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            var rawContent = aiResponse?.Choices?.FirstOrDefault()?.Message?.Content ?? string.Empty;
            return SanitizeAssistantContent(rawContent);
        }

        private static string SanitizeAssistantContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return string.Empty;
            }

            // Remove hidden reasoning blocks if the upstream model includes <think>...</think> tags.
            var cleaned = Regex.Replace(content, "<think>[\\s\\S]*?</think>", string.Empty, RegexOptions.IgnoreCase)
                .Trim();

            return cleaned;
        }
    }

    #region DeepChat DTOs

    public class DeepChatRequest
    {
        [JsonPropertyName("messages")]
        public List<DeepChatMessage> Messages { get; set; } = new();
    }

    public class DeepChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class DeepChatResponse
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }

    #endregion

    #region OpenAI Compatible DTOs



    public class OpenAiChatCompletionRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;

        [JsonPropertyName("messages")]
        public List<OpenAiChatMessage> Messages { get; set; } = new();

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; } = 0.7;
    }

    public class OpenAiChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
    }

    public class OpenAiChatCompletionResponse
    {
        [JsonPropertyName("choices")]
        public List<OpenAiChoice> Choices { get; set; } = new();
    }

    public class OpenAiChoice
    {
        [JsonPropertyName("message")]
        public OpenAiAssistantMessage? Message { get; set; }
    }

    public class OpenAiAssistantMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
    }

    #endregion

    #region Options

    public class AiProxyOptions
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ChatEndpoint { get; set; } = "/v1/chat/completions";
        public string ApiKey { get; set; } = string.Empty;
        public string Model { get; set; } = "gpt-4o-mini";
        public string SystemPrompt { get; set; } = "Bạn là trợ lý AI hữu ích.";
    }

    #endregion
}







