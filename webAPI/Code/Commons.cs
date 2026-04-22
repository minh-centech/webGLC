using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace webAPI.Code
{
    //public class GlobalVariables
    //{
    //    public static string ConnectionString = @"Data Source=192.168.1.100, 1433;Initial Catalog=everWareHouse-CFS-GLC;Persist Security Info=True;User ID=sa;Password=Str0ng!Passw0rd;Connect Timeout=60";
    //    public static string IDDanhMucDonVi = "1";
    //    public static string IDDanhMucKhachHangDoiLenh = "1284696";
    //    public static string IDDanhMucNguoiSuDungGuest = "1289642";
    //}

    using System.Configuration;

    public class GlobalVariables
    {
        #region Default Values (Giá trị mặc định)
        private static readonly string defaultConnectionString = @"Data Source=192.168.1.100, 1433;Initial Catalog=everWareHouse-CFS-GLC;Persist Security Info=True;User ID=sa;Password=Str0ng!Passw0rd;Connect Timeout=60";
        private static readonly string defaultIDDanhMucDonVi = "1";
        private static readonly string defaultIDDanhMucKhachHangDoiLenh = "1284696";
        private static readonly string defaultIDDanhMucNguoiSuDungGuest = "1289642";
        #endregion

        #region Public Properties (Lấy từ Web.config hoặc dùng Default)

        public static string ConnectionString
        {
            get
            {
                var val = ConfigurationManager.AppSettings["ConnectionString"];
                return !string.IsNullOrEmpty(val) ? val : defaultConnectionString;
            }
        }

        public static string IDDanhMucDonVi
        {
            get
            {
                var val = ConfigurationManager.AppSettings["IDDanhMucDonVi"];
                return !string.IsNullOrEmpty(val) ? val : defaultIDDanhMucDonVi;
            }
        }

        public static string IDDanhMucKhachHangDoiLenh
        {
            get
            {
                var val = ConfigurationManager.AppSettings["IDDanhMucKhachHangDoiLenh"];
                return !string.IsNullOrEmpty(val) ? val : defaultIDDanhMucKhachHangDoiLenh;
            }
        }

        public static string IDDanhMucNguoiSuDungGuest
        {
            get
            {
                var val = ConfigurationManager.AppSettings["IDDanhMucNguoiSuDungGuest"];
                return !string.IsNullOrEmpty(val) ? val : defaultIDDanhMucNguoiSuDungGuest;
            }
        }

        #endregion
    }
}