namespace webGLCv2.Models;

public sealed class AccountDocumentDetails
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int AccountType { get; set; }
    public string AccountTypeText { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsLockAccount { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string TaxCode { get; set; } = string.Empty;
    public string CompanyEmail { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public string CompanyPhone { get; set; } = string.Empty;
    public string CompanyFax { get; set; } = string.Empty;
    public string BusinessLicenseNumber { get; set; } = string.Empty;
    public DateTime? IssueDate { get; set; }
    public string IssuePlace { get; set; } = string.Empty;
    public string AuthorizedRepresentative { get; set; } = string.Empty;
    public string RepresentativeTitle { get; set; } = string.Empty;
    public string AuthorizedCompany { get; set; } = string.Empty;
    public string ServiceUserName { get; set; } = string.Empty;
    public string BillingEmail { get; set; } = string.Empty;
    public string CitizenIdNumber { get; set; } = string.Empty;
    public List<DocumentFileItem> Documents { get; set; } = new();
    public bool HasAllRequiredDocuments => Documents.Where(x => x.IsRequired).All(x => x.HasFile);
}
