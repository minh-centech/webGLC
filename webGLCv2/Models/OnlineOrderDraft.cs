using System.ComponentModel.DataAnnotations;

namespace webGLCv2.Models;

public sealed class OnlineOrderDraft
{
    public DateTime OrderDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    public string PhoneNumber { get; set; } = string.Empty;

    public string IdentityNumber { get; set; } = string.Empty;
    public string VehicleNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mã số thuế.")]
    public string TaxCode { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public string CompanyEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập House Bill.")]
    public string HouseBill { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số cont.")]
    public string ContainerNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn ngày lấy hàng.")]
    public DateTime? PickupDate { get; set; }

    public string DeclarationNumber { get; set; } = string.Empty;
}
