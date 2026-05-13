namespace webGLCv2.Models;

public sealed class EmailSenderOptions
{
    public const string SectionName = "EmailSender";

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool SSL { get; set; } = true;
    public int Port { get; set; } = 587;
    public string Host { get; set; } = string.Empty;
    public string EmailSender { get; set; } = string.Empty;
    
}
