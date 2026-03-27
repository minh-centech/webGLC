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
        public object Email { get; set; }
        public object Ten { get; set; }
        public object SoDienThoai { get; set; }
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
        public string Email { get; set; }
        public string Ten { get; set; }
        public string SoDienThoai { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        public DanhMucKhachHangDoiLenhInsertRequest()
        {
            Email = null;
            Ten = null;
            SoDienThoai = null;
            Password = null;
            PasswordConfirm = null;
        }
    }
    public class DanhMucKhachHangDoiLenhLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public DanhMucKhachHangDoiLenhLoginRequest()
        {
            Email = null;
            Password = null;
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
}
