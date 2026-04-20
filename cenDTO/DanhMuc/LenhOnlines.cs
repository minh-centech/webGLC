using coreDTO;
using System;

namespace cenDTO
{
    public class LenhOnlines : BaseDTO
    {
        public object SoThuTuLenh { get; set; }
        public object HoVaTen { get; set; }
        public object SoDienThoai { get; set; }
        public object SoCMND { get; set; }
        public object SoXe { get; set; }
        public object MaSoThue { get; set; }
        public object TenCongTy { get; set; }
        public object DiaChi { get; set; }
        public object Email { get; set; }
        public object HouseBill { get; set; }
        public object NgayLamLenh { get; set; }
        public object SoCont { get; set; }
        public object NgayLayHang { get; set; }
        public object SoToKhai { get; set; }
        public object TrangThai { get; set; }
        public object IDDanhMucKhachHangDoiLenh { get; set; }

        public const string tableName = "LenhOnlines";
        public const string listProcedureName = "List_" + tableName;
        public const string insertProcedureName = "Insert_" + tableName;
        public const string updateProcedureName = "Update_" + tableName;
        public const string deleteProcedureName = "Delete_" + tableName;
    }

    public class LenhOnlinesFilterRequest
    {
        public long? ID { get; set; }
        public long? IDDanhMucKhachHangDoiLenh { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string HouseBill { get; set; }
        public string SoCont { get; set; }
        public string MaSoThue { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class LenhOnlinesSaveRequest
    {
        public long? ID { get; set; }
        public string HoVaTen { get; set; }
        public string SoDienThoai { get; set; }
        public string SoCMND { get; set; }
        public string SoXe { get; set; }
        public string MaSoThue { get; set; }
        public string TenCongTy { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string HouseBill { get; set; }
        public string SoCont { get; set; }
        public DateTime? NgayLayHang { get; set; }
        public string SoToKhai { get; set; }
        public int TrangThai { get; set; }
        public long IDDanhMucKhachHangDoiLenh { get; set; }
    }

    public class LenhOnlinesDeleteRequest
    {
        public long ID { get; set; }
    }
}
