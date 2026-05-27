using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webGLCv2.Services;

namespace webGLCv2.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public sealed class TaiLieuController : ControllerBase
{
    private readonly LegacyCustomerPortalService _legacyCustomerPortalService;

    public TaiLieuController(LegacyCustomerPortalService legacyCustomerPortalService)
    {
        _legacyCustomerPortalService = legacyCustomerPortalService;
    }

    [HttpGet("ViewPdf")]
    public async Task<IActionResult> ViewPdf([FromQuery] string path, CancellationToken cancellationToken)
    {
        try
        {
            var pdfStream = await _legacyCustomerPortalService.ViewPdfAsync(path, cancellationToken);
            return File(pdfStream, "application/pdf", enableRangeProcessing: true);
        }
        catch (FileNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
