namespace webGLCv2.Models;

public sealed class AuthenticatedUserDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public int AccountType { get; set; }
    public string AccountTypeName { get; set; } = string.Empty;
    public string RoleName { get; set; } = "User";
}
