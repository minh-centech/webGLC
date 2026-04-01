namespace webGLCv2.Models;

public sealed class ApiEnvelope
{
    public int Status { get; set; }
    public string Data { get; set; } = string.Empty;
    public string ErrorMsg { get; set; } = string.Empty;
}
