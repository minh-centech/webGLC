using System.ComponentModel.DataAnnotations;

namespace webGLCv2.Models
{
    public class LenhXuatKhoHangNhapKhauPheDuyet
    {
        public long? IDDanhMucDonVi { get; set; } = 1;

        public string? SoLenhXuat { get; set; }
        public string? NguoiPheDuyet { get; set; }
        public string? GhiChu { get; set; }
    }

    public class LenhXuatKhoHangNhapKhauDetail
    {
        // Giữ lại duy nhất ID đầu tiên
        public long? ID { get; set; }

        public string? So { get; set; }
        public DateTime? NgayLap { get; set; }
        public bool? PheDuyet { get; set; }
        public string? NguoiPheDuyet { get; set; }
        public DateTime? NgayGiaHan { get; set; }
        public string? SoVanDon { get; set; }
        public string? HoTenNguoiNhanHang { get; set; }
        public string? SoCMND { get; set; }
        public string? SoDienThoaiNguoiNhanHang { get; set; }
        public string? TenDanhMucKhachHang { get; set; }
        public string? DiaChi { get; set; }
        public string? MaSoThue { get; set; }
        public bool? NgoaiGio { get; set; }
        public bool? HangNguyHiem { get; set; }
        public decimal? SoLuongQuaKho { get; set; }
        public decimal? SoLuongQuaTai { get; set; }
        public DateTime? NgayNhapKho { get; set; }
        public DateTime? NgayGiaHanCuoi { get; set; }
        public string? SoContainer { get; set; }
        public string? SoSeal { get; set; }
        public string? MasterBill { get; set; }
        public string? EDO { get; set; }
        public string? TenTau { get; set; }
        public string? NgayTauDen { get; set; }
        public string? SoHoaDon { get; set; }
        public DateTime? NgayHoaDon { get; set; }
        public string? TenChuHang { get; set; }
        public decimal? SoLuongKienXuat { get; set; }
        public decimal? KhoiLuongXuat { get; set; }
        public decimal? CBMXuat { get; set; }
        public string? SoToKhai { get; set; }
        public string? GhiChu { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
