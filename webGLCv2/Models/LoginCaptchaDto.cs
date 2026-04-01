namespace webGLCv2.Models;

public sealed class LoginCaptchaDto
{
    public string CaptchaDisplayText { get; set; } = "------";
    public string CaptchaToken { get; set; } = string.Empty;
}
