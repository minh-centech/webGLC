namespace webGLCv2.Models;

public sealed class OnlineOrderRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string OrderCode { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string IdentityNumber { get; set; } = string.Empty;
    public string VehicleNumber { get; set; } = string.Empty;
    public string TaxCode { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public string CompanyEmail { get; set; } = string.Empty;
    public string HouseBill { get; set; } = string.Empty;
    public string ContainerNumber { get; set; } = string.Empty;
    public DateTime? PickupDate { get; set; }
    public string DeclarationNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
