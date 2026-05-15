using coreDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cenDTO
{
    public  class DanhMucKhachHangDoiLenh : BaseDTO
    {
        public object IDDanhMucDonVi { get; set; }
        public object IDDanhMucLoaiDoiTuong { get; set; }
        public object LoaiTaiKhoan { get; set; }
        public object IsActive { get; set; }
        public object IsLockAccount { get; set; }
        public object Email { get; set; }
        public object Ten { get; set; }
        public object SoDienThoai { get; set; }
        public object BanScanSoCMNDCanCuocPath { get; set; }
        public object Password { get; set; }
        public object PasswordConfirm { get; set; }
        public object PartnerGUID { get; set; }
        public object KichHoat { get; set; }
        public object IDDanhMucNguoiSuDungCreate { get; set; }
        public object IDDanhMucNguoiSuDungEdit { get; set; }

        public const string tableName = "DanhMucKhachHangDoiLenh";
        public const string listProcedureName = "List_" + tableName;
        public const string listLoginProcedureName = "List_" + tableName + "_Login";
        public const string insertProcedureName = "Insert_" + tableName;
        public const string updateProcedureName = "Update_" + tableName;
        public const string updateKichHoatProcedureName = "Update_" + tableName + "_KichHoat";
        public const string updatePasswordProcedureName = "Update_" + tableName + "_Password";
        public const string deleteProcedureName = "Delete_" + tableName;

        public const string UpdateKichHoatAccountProcedure = "Update_" + tableName + "_KichHoatAccount";


     
        public const string insertRecoverPasswordLogProcedureName = "Insert_" + tableName + "RecoverPasswordLog";
        public const string getPartnerGUIDByEmailProcedureName = "Get_" + tableName + "_PartnerGUIDByEmail";
        public const string getMaKichHoatByEmail = "Get_" + tableName + "_MaKichHoatByEmail";
        public const string getMaXacNhanByEmail = "Get_" + tableName + "_MaXacNhanMatKhau";
        public const string updateXacNhanDoiMatKhau = "Update_" + tableName + "_XacNhanDoiMatKhau";


        public DanhMucKhachHangDoiLenh()
        {
            ID = null;
            IDDanhMucDonVi = null;
            IDDanhMucLoaiDoiTuong = null;
            LoaiTaiKhoan = 1;
            IsActive = false;
            IsLockAccount = false;
            Email = null;
            Ten = null;
            SoDienThoai = null;
            Password = null;
            PasswordConfirm = null;
            PartnerGUID = null;
            KichHoat = false;
            IDDanhMucNguoiSuDungCreate = null;
            IDDanhMucNguoiSuDungEdit = null;
            CreateDate = null;
            EditDate = null;
        }
    }
    public class DanhMucKhachHangDoiLenhInsertRequest
    {
        public int LoaiTaiKhoan { get; set; }
        public bool IsActive { get; set; }
        public bool IsLockAccount { get; set; }
        public string Email { get; set; }
        public string Ten { get; set; }
        public string SoDienThoai { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        public DanhMucKhachHangDoiLenhInsertRequest()
        {
            LoaiTaiKhoan = 1;
            IsActive = false;
            IsLockAccount = false;
            Email = null;
            Ten = null;
            SoDienThoai = null;
            Password = null;
            PasswordConfirm = null;
        }
    }
    public class DangKyTaiKhoanRequest
    {
        public int LoaiTaiKhoan { get; set; }
        public bool IsActive { get; set; }
        public bool IsLockAccount { get; set; }
        public string TenDangNhap { get; set; }
        public string Email { get; set; }
        public string Ten { get; set; }
        public string SoDienThoai { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string EmailXuatHoaDon { get; set; }
        public string SoCMNDCanCuoc { get; set; }
        public string BanScanSoCMNDCanCuocPath { get; set; }
        public string BanDangKyCaNhanCoChuKyPath { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string MaSoThue { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoaiDoanhNghiep { get; set; }
        public string EmailDoanhNghiep { get; set; }
        public string SoFax { get; set; }
        public string GiayPhepKinhDoanh { get; set; }
        public DateTime? NgayCap { get; set; }
        public string NoiCap { get; set; }
        public string DaiDienCoThamQuyen { get; set; }
        public string ChucVu { get; set; }
        public string DoanhNghiepCongTyDuocUyQuyen { get; set; }
        public string BanScanGiayPhepKinhDoanhPath { get; set; }
        public string BanDangKyEPortChuKySoPath { get; set; }

        public DangKyTaiKhoanRequest()
        {
            LoaiTaiKhoan = 1;
            IsActive = false;
            IsLockAccount = false;
            TenDangNhap = null;
            Email = null;
            Ten = null;
            SoDienThoai = null;
            BanScanSoCMNDCanCuocPath = null;
            Password = null;
            PasswordConfirm = null;
            EmailXuatHoaDon = null;
            SoCMNDCanCuoc = null;
            BanScanSoCMNDCanCuocPath = null;
            BanDangKyCaNhanCoChuKyPath = null;
            TenDoanhNghiep = null;
            MaSoThue = null;
            DiaChi = null;
            SoDienThoaiDoanhNghiep = null;
            EmailDoanhNghiep = null;
            SoFax = null;
            GiayPhepKinhDoanh = null;
            NgayCap = null;
            NoiCap = null;
            DaiDienCoThamQuyen = null;
            ChucVu = null;
            DoanhNghiepCongTyDuocUyQuyen = null;
            BanScanGiayPhepKinhDoanhPath = null;
            BanDangKyEPortChuKySoPath = null;
        }
    }
    public class DanhMucKhachHangDoiLenhLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CaptchaCode { get; set; }
        public string CaptchaToken { get; set; }

        public DanhMucKhachHangDoiLenhLoginRequest()
        {
            Email = null;
            Password = null;
            CaptchaCode = null;
            CaptchaToken = null;
        }
    }
    public class DanhMucKhachHangDoiLenhGetMaKichHoatByEmailRequest
    {
       
        public string Email { get; set; }
        public string ID { get; set; }
        public string MaKichHoat { get; set; }
        public string MaKichHoatMoi { get; set; }



        public DanhMucKhachHangDoiLenhGetMaKichHoatByEmailRequest()
        {
      
            Email = null;
            ID = null;
            MaKichHoat = null;
            MaKichHoatMoi = null;

        }
    }
    public class DanhMucKhachHangDoiLenhGetMaXacNhanByEmailRequest
    {


        public string Email { get; set; }
        public string ID { get; set; }
        public string MaXacNhan { get; set; }
        public string MaXacNhanMoi { get; set; }



        public DanhMucKhachHangDoiLenhGetMaXacNhanByEmailRequest()
        {


            Email = null;
            ID = null;
            MaXacNhan = null;
            MaXacNhanMoi = null;

        }
    }
    public class DanhMucKhachHangDoiLenhXacNhanDoiMatKhauRequest
    {

        public string ID { get; set; }
        public string Email { get; set; }
        public string MaXacNhan { get; set; }
        public string MatKhauMoi { get; set; }
        public string XacNhanMatKhauMoi { get; set; }



        public DanhMucKhachHangDoiLenhXacNhanDoiMatKhauRequest()
        {

            ID = null;
            Email = null;
            MaXacNhan = null;                 
            MatKhauMoi = null;
            XacNhanMatKhauMoi = null;

        }
    }
    public class DanhMucKhachHangDoiLenhKichHoatRequest
    {
        public string ID { get; set; }
        public string MaKichHoat { get; set; }


        public DanhMucKhachHangDoiLenhKichHoatRequest()
        {
            ID = null;
            MaKichHoat = null;
        }
    }
    public class DanhMucKhachHangDoiLenhChangePasswordRequest
    {
        public object Email { get; set; }
        public object Ten { get; set; }
        public object OldPassword { get; set; }
        public object NewPassword { get; set; }
        public object NewPasswordConfirm { get; set; }

        public DanhMucKhachHangDoiLenhChangePasswordRequest()
        {
            Email = null;
            Ten = null;
            OldPassword = null;
            NewPassword = null;
            NewPasswordConfirm = null;
        }
    }
    public class DanhMucKhachHangDoiLenhSetActiveRequest
    {
        public object ID { get; set; }
        public bool IsActive { get; set; }
        public bool IsLockAccount { get; set; }

        public DanhMucKhachHangDoiLenhSetActiveRequest()
        {
            ID = null;
            IsActive = true;
            IsLockAccount = false;
        }
    }

    public class NguoiDungDoanhNghiepDto
    {
        public object ID { get; set; }
        public object IDDanhMucKhachHangDoiLenh { get; set; }
        public object TenDoanhNghiep { get; set; }
        public object MaSoThue { get; set; }
        public object DiaChi { get; set; }
        public object SoDienThoaiDoanhNghiep { get; set; }
        public object EmailDoanhNghiep { get; set; }
        public object SoFax { get; set; }
        public object GiayPhepKinhDoanh { get; set; }
        public object BanScanGiayPhepKinhDoanhPath { get; set; }
        public object NgayCap { get; set; }
        public object NoiCap { get; set; }
        public object DaiDienCoThamQuyen { get; set; }
        public object ChucVu { get; set; }
        public object DoanhNghiepCongTyDuocUyQuyen { get; set; }
        public object TenDangNhapDangKyDichVu { get; set; }
        public object EmailXuatHoaDon { get; set; }
        public object SoCMNDCanCuoc { get; set; }
        public object BanScanSoCMNDCanCuocPath { get; set; }
        public object BanDangKyEPortChuKySoPath { get; set; }
        public object IsActive { get; set; }
        public object IsLockAccount { get; set; }
        public object CreateDate { get; set; }
        public object EditDate { get; set; }
    }

    public class NguoiDungDoanhNghiepSaveRequest
    {
        public long? ID { get; set; }
        public long IDDanhMucKhachHangDoiLenh { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string MaSoThue { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoaiDoanhNghiep { get; set; }
        public string EmailDoanhNghiep { get; set; }
        public string SoFax { get; set; }
        public string GiayPhepKinhDoanh { get; set; }
        public string BanScanGiayPhepKinhDoanhPath { get; set; }
        public DateTime? NgayCap { get; set; }
        public string NoiCap { get; set; }
        public string DaiDienCoThamQuyen { get; set; }
        public string ChucVu { get; set; }
        public string DoanhNghiepCongTyDuocUyQuyen { get; set; }
        public string TenDangNhapDangKyDichVu { get; set; }
        public string EmailXuatHoaDon { get; set; }
        public string SoCMNDCanCuoc { get; set; }
        public string BanScanSoCMNDCanCuocPath { get; set; }
        public string BanDangKyEPortChuKySoPath { get; set; }
        public bool IsActive { get; set; }
        public bool IsLockAccount { get; set; }

        public NguoiDungDoanhNghiepSaveRequest()
        {
            ID = null;
            TenDoanhNghiep = null;
            MaSoThue = null;
            DiaChi = null;
            SoDienThoaiDoanhNghiep = null;
            EmailDoanhNghiep = null;
            SoFax = null;
            GiayPhepKinhDoanh = null;
            BanScanGiayPhepKinhDoanhPath = null;
            NgayCap = null;
            NoiCap = null;
            DaiDienCoThamQuyen = null;
            ChucVu = null;
            DoanhNghiepCongTyDuocUyQuyen = null;
            TenDangNhapDangKyDichVu = null;
            EmailXuatHoaDon = null;
            SoCMNDCanCuoc = null;
            BanScanSoCMNDCanCuocPath = null;
            BanDangKyEPortChuKySoPath = null;
            IsActive = true;
            IsLockAccount = false;
        }
    }
}
