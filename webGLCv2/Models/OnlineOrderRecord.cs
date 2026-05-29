namespace webGLCv2.Models;

public sealed class OnlineOrderRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public long UserId { get; set; }
    public long CreatorUserId { get; set; }
    public string CreatorEmail { get; set; } = string.Empty;
    public string CreatorName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public long SequenceNumber { get; set; }
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
    public int StatusCode { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool IsHoanThanh { get; set; }
    public bool HasPaymentInfo { get; set; }
    public int PaymentStatusCode { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public string InvoiceDownloadUrl { get; set; } = string.Empty;

    public long IDctLenhNhapKhoHangNhapKhauChiTiet { get; set; }
}

