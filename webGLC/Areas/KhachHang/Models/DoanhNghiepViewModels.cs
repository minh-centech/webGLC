using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using cenDTO;

namespace webGLC.Areas.KhachHang.Models
{
    public class DoanhNghiepListItemViewModel
    {
        public string ID { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string MaSoThue { get; set; }
        public string EmailDoanhNghiep { get; set; }
        public string SoDienThoaiDoanhNghiep { get; set; }
        public bool IsActive { get; set; }
        public string TrangThaiText { get; set; }
    }

    public class DoanhNghiepListPageViewModel
    {
        public List<DoanhNghiepListItemViewModel> Items { get; set; }
        public bool CanCreate { get; set; }
        public bool IsEnterpriseAccount { get; set; }
        public string ErrorMessage { get; set; }

        public DoanhNghiepListPageViewModel()
        {
            Items = new List<DoanhNghiepListItemViewModel>();
            ErrorMessage = string.Empty;
        }
    }

    public class DoanhNghiepEditViewModel
    {
        public long? ID { get; set; }
        public long IDDanhMucKhachHangDoiLenh { get; set; }
        public int LoaiTaiKhoanNguoiDung { get; set; }
        public bool IsReadOnlyAccount { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên doanh nghiệp.")]
        public string TenDoanhNghiep { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã số thuế.")]
        public string MaSoThue { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ doanh nghiệp.")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại doanh nghiệp.")]
        public string SoDienThoaiDoanhNghiep { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email doanh nghiệp.")]
        [EmailAddress(ErrorMessage = "Email doanh nghiệp không đúng định dạng.")]
        public string EmailDoanhNghiep { get; set; }

        public string SoFax { get; set; }
        public string GiayPhepKinhDoanh { get; set; }
        public DateTime? NgayCap { get; set; }
        public string NoiCap { get; set; }
        public string DaiDienCoThamQuyen { get; set; }
        public string ChucVu { get; set; }
        public string DoanhNghiepCongTyDuocUyQuyen { get; set; }
        public string TenDangNhapDangKyDichVu { get; set; }
        public string EmailXuatHoaDon { get; set; }
        public string SoCMNDCanCuoc { get; set; }
        public string BanScanGiayPhepKinhDoanhPath { get; set; }
        public string BanScanSoCMNDCanCuocPath { get; set; }
        public string BanDangKyEPortChuKySoPath { get; set; }
        public bool IsActive { get; set; }

        public DoanhNghiepEditViewModel()
        {
            IsActive = true;
        }

        public NguoiDungDoanhNghiepSaveRequest ToRequest()
        {
            return new NguoiDungDoanhNghiepSaveRequest
            {
                ID = ID,
                IDDanhMucKhachHangDoiLenh = IDDanhMucKhachHangDoiLenh,
                TenDoanhNghiep = TenDoanhNghiep,
                MaSoThue = MaSoThue,
                DiaChi = DiaChi,
                SoDienThoaiDoanhNghiep = SoDienThoaiDoanhNghiep,
                EmailDoanhNghiep = EmailDoanhNghiep,
                SoFax = SoFax,
                GiayPhepKinhDoanh = GiayPhepKinhDoanh,
                BanScanGiayPhepKinhDoanhPath = BanScanGiayPhepKinhDoanhPath,
                NgayCap = NgayCap,
                NoiCap = NoiCap,
                DaiDienCoThamQuyen = DaiDienCoThamQuyen,
                ChucVu = ChucVu,
                DoanhNghiepCongTyDuocUyQuyen = DoanhNghiepCongTyDuocUyQuyen,
                TenDangNhapDangKyDichVu = TenDangNhapDangKyDichVu,
                EmailXuatHoaDon = EmailXuatHoaDon,
                SoCMNDCanCuoc = SoCMNDCanCuoc,
                BanScanSoCMNDCanCuocPath = BanScanSoCMNDCanCuocPath,
                BanDangKyEPortChuKySoPath = BanDangKyEPortChuKySoPath,
                IsActive = IsActive
            };
        }
    }
}
