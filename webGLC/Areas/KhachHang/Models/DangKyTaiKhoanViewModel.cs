using System;
using System.ComponentModel.DataAnnotations;
using cenDTO;

namespace webGLC.Areas.KhachHang.Models
{
    public class DangKyTaiKhoanViewModel : DangKyTaiKhoanRequest
    {
        [Required(ErrorMessage = "Vui lòng chọn loại tài khoản.")]
        public new int LoaiTaiKhoan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        public new string Ten { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public new string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public new string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public new string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập xác nhận mật khẩu.")]
        public new string PasswordConfirm { get; set; }

        public DangKyTaiKhoanViewModel()
        {
            LoaiTaiKhoan = 1;
            IsActive = false;
        }

        public DangKyTaiKhoanRequest ToRequest()
        {
            return new DangKyTaiKhoanRequest
            {
                LoaiTaiKhoan = LoaiTaiKhoan,
                IsActive = IsActive,
                TenDangNhap = TenDangNhap,
                Email = Email,
                Ten = Ten,
                SoDienThoai = SoDienThoai,
                Password = Password,
                PasswordConfirm = PasswordConfirm,
                EmailXuatHoaDon = EmailXuatHoaDon,
                SoCMNDCanCuoc = SoCMNDCanCuoc,
                BanScanSoCMNDCanCuocPath = BanScanSoCMNDCanCuocPath,
                TenDoanhNghiep = TenDoanhNghiep,
                MaSoThue = MaSoThue,
                DiaChi = DiaChi,
                SoDienThoaiDoanhNghiep = SoDienThoaiDoanhNghiep,
                EmailDoanhNghiep = EmailDoanhNghiep,
                SoFax = SoFax,
                GiayPhepKinhDoanh = GiayPhepKinhDoanh,
                NgayCap = NgayCap,
                NoiCap = NoiCap,
                DaiDienCoThamQuyen = DaiDienCoThamQuyen,
                ChucVu = ChucVu,
                DoanhNghiepCongTyDuocUyQuyen = DoanhNghiepCongTyDuocUyQuyen,
                BanScanGiayPhepKinhDoanhPath = BanScanGiayPhepKinhDoanhPath,
                BanDangKyEPortChuKySoPath = BanDangKyEPortChuKySoPath
            };
        }
    }
}
