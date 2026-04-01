namespace webGLCv2.Models;

public sealed class LegacyUploadPdfResult
{
    public string OriginalFileName { get; set; } = string.Empty;
    public string SavedFileName { get; set; } = string.Empty;
    public string RelativePath { get; set; } = string.Empty;
    public string ViewUrl { get; set; } = string.Empty;
}
