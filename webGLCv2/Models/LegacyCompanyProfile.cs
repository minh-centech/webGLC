namespace webGLCv2.Models;

public sealed class LegacyCompanyProfile
{
    public long ID { get; set; }
    public long IDDanhMucKhachHangDoiLenh { get; set; }
    public string TenDoanhNghiep { get; set; } = string.Empty;
    public string MaSoThue { get; set; } = string.Empty;
    public string DiaChi { get; set; } = string.Empty;
    public string SoDienThoaiDoanhNghiep { get; set; } = string.Empty;
    public string EmailDoanhNghiep { get; set; } = string.Empty;
    public string SoFax { get; set; } = string.Empty;
    public string GiayPhepKinhDoanh { get; set; } = string.Empty;
    public DateTime? NgayCap { get; set; }
    public string NoiCap { get; set; } = string.Empty;
    public string DaiDienCoThamQuyen { get; set; } = string.Empty;
    public string ChucVu { get; set; } = string.Empty;
    public string DoanhNghiepCongTyDuocUyQuyen { get; set; } = string.Empty;
    public string TenDangNhapDangKyDichVu { get; set; } = string.Empty;
    public string EmailXuatHoaDon { get; set; } = string.Empty;
    public string SoCMNDCanCuoc { get; set; } = string.Empty;
    public string BanScanGiayPhepKinhDoanhPath { get; set; } = string.Empty;
    public string BanScanSoCMNDCanCuocPath { get; set; } = string.Empty;
    public string BanDangKyEPortChuKySoPath { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
