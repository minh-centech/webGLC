namespace webGLCv2.Models;

public sealed class DocumentFileItem
{
    public string Name { get; set; } = string.Empty;
    public string FieldKey { get; set; } = string.Empty;
    public string UploadFolder { get; set; } = string.Empty;
    public string RelativePath { get; set; } = string.Empty;
    public string ViewUrl { get; set; } = string.Empty;
    public bool IsRequired { get; set; } = true;
    public bool HasFile => !string.IsNullOrWhiteSpace(RelativePath);
}
