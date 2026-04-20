namespace webGLCv2.Models;

public sealed class AccountListItem
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int AccountType { get; set; }
    public string AccountTypeText { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool ActivatedFlag { get; set; }
    public bool HasPendingEnterpriseApproval { get; set; }
    public string StatusText { get; set; } = string.Empty;
}

