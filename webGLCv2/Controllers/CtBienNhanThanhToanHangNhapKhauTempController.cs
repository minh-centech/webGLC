using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using webGLCv2.Services;

namespace webGLCv2.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("api/ctBienNhanThanhToanHangNhapKhauTemp")]
[ApiController]
public sealed class CtBienNhanThanhToanHangNhapKhauTempController : ControllerBase
{
    private readonly OnlineOrderService _onlineOrderService;

    public CtBienNhanThanhToanHangNhapKhauTempController(OnlineOrderService onlineOrderService)
    {
        _onlineOrderService = onlineOrderService;
    }

    [HttpPost("CheckThanhToanAuto")]
    public async Task<IActionResult> CheckThanhToanAuto(
        [FromQuery] string t,
        [FromQuery] string checksum,
        [FromBody] CheckThanhToanAutoRequest request)
    {
        try
        {
            using var response = await _onlineOrderService.CheckThanhToanAutoRawAsync(t, checksum, request.TinNhanRaw);
            var body = await response.Content.ReadAsStringAsync();
            return new ContentResult
            {
                StatusCode = (int)response.StatusCode,
                Content = body,
                ContentType = response.Content.Headers.ContentType?.ToString() ?? "application/json"
            };
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Status = 1,
                Data = string.Empty,
                ErrorMsg = "Lỗi không kết nối được dịch vụ nguồn!"
            });
        }
        catch (InvalidOperationException)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Status = 1,
                Data = string.Empty,
                ErrorMsg = "Lỗi không kết nối được dịch vụ nguồn!"
            });
        }
    }
}

public sealed class CheckThanhToanAutoRequest
{
    public string TinNhanRaw { get; set; } = string.Empty;
}
