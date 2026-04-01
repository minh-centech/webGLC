using System.ComponentModel.DataAnnotations;

namespace webGLCv2.Models;

public sealed class RegisterAccountModel
{
    [Required(ErrorMessage = "Vui lòng chọn loại tài khoản.")]
    public int LoaiTaiKhoan { get; set; } = 1;

    public bool IsActive { get; set; }
    public string? TenDangNhap { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    public string Ten { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    public string SoDienThoai { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập xác nhận mật khẩu.")]
    public string PasswordConfirm { get; set; } = string.Empty;

    public string? EmailXuatHoaDon { get; set; }
    public string? SoCMNDCanCuoc { get; set; }
    public string? BanScanSoCMNDCanCuocPath { get; set; }
    public string? BanDangKyCaNhanCoChuKyPath { get; set; }
    public string? TenDoanhNghiep { get; set; }
    public string? MaSoThue { get; set; }
    public string? DiaChi { get; set; }
    public string? SoDienThoaiDoanhNghiep { get; set; }
    public string? EmailDoanhNghiep { get; set; }
    public string? SoFax { get; set; }
    public string? GiayPhepKinhDoanh { get; set; }
    public DateTime? NgayCap { get; set; }
    public string? NoiCap { get; set; }
    public string? DaiDienCoThamQuyen { get; set; }
    public string? ChucVu { get; set; }
    public string? DoanhNghiepCongTyDuocUyQuyen { get; set; }
    public string? BanScanGiayPhepKinhDoanhPath { get; set; }
    public string? BanDangKyEPortChuKySoPath { get; set; }
}
