using System.ComponentModel.DataAnnotations;

namespace webGLCv2.Models;

public sealed class LoginPostModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ")]
    [StringLength(200, ErrorMessage = "Email không được dài quá 200 ký tự")]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }
}

