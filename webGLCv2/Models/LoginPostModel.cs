using System.ComponentModel.DataAnnotations;

namespace webGLCv2.Models;

public sealed class LoginPostModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }
}
