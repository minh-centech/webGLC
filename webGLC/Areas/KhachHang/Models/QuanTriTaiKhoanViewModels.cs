using System.Collections.Generic;

namespace webGLC.Areas.KhachHang.Models
{
    public class QuanTriTaiKhoanItemViewModel
    {
        public string ID { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public System.DateTime? CreateDate { get; set; }
        public int LoaiTaiKhoan { get; set; }
        public bool IsActive { get; set; }
        public bool KichHoat { get; set; }
        public string LoaiTaiKhoanText { get; set; }
        public string TrangThaiText { get; set; }
    }

    public class QuanTriTaiKhoanPageViewModel
    {
        public List<QuanTriTaiKhoanItemViewModel> Accounts { get; set; }
        public string ErrorMessage { get; set; }
        public string SearchTerm { get; set; }
        public string Phone { get; set; }
        public string Tab { get; set; }
        public int TotalCount { get; set; }
        public int InactiveCount { get; set; }

        public QuanTriTaiKhoanPageViewModel()
        {
            Accounts = new List<QuanTriTaiKhoanItemViewModel>();
            ErrorMessage = string.Empty;
            SearchTerm = string.Empty;
            Phone = string.Empty;
            Tab = "all";
            TotalCount = 0;
            InactiveCount = 0;
        }
    }

    public class TaiLieuDoanhNghiepItemViewModel
    {
        public string TenTaiLieu { get; set; }
        public string RelativePath { get; set; }
        public string ViewUrl { get; set; }
        public bool HasFile
        {
            get { return !string.IsNullOrWhiteSpace(RelativePath); }
        }
    }

    public class HoSoDoanhNghiepViewModel
    {
        public string ID { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public int LoaiTaiKhoan { get; set; }
        public bool IsActive { get; set; }
        public bool KichHoat { get; set; }
        public string LoaiTaiKhoanText { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string MaSoThue { get; set; }
        public string EmailDoanhNghiep { get; set; }
        public string ErrorMessage { get; set; }
        public List<TaiLieuDoanhNghiepItemViewModel> Documents { get; set; }

        public HoSoDoanhNghiepViewModel()
        {
            ErrorMessage = string.Empty;
            Documents = new List<TaiLieuDoanhNghiepItemViewModel>();
        }
    }
}
