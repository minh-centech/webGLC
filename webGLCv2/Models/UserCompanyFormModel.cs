using System.ComponentModel.DataAnnotations;

namespace webGLCv2.Models;

public sealed class UserCompanyFormModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên doanh nghiệp.")]
    public string TenDoanhNghiep { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mã số thuế.")]
    public string MaSoThue { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
    public string DiaChi { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại doanh nghiệp.")]
    public string SoDienThoaiDoanhNghiep { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email doanh nghiệp.")]
    [EmailAddress(ErrorMessage = "Email doanh nghiệp không đúng định dạng.")]
    public string EmailDoanhNghiep { get; set; } = string.Empty;

    public string? SoFax { get; set; }
    public string? GiayPhepKinhDoanh { get; set; }
    public DateTime? NgayCap { get; set; }
    public string? NoiCap { get; set; }
    public string? DaiDienCoThamQuyen { get; set; }
    public string? ChucVu { get; set; }
    public string? DoanhNghiepCongTyDuocUyQuyen { get; set; }
    public string? TenDangNhapDangKyDichVu { get; set; }
    public string? EmailXuatHoaDon { get; set; }
    public string? SoCMNDCanCuoc { get; set; }
}
