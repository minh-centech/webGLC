using coreDTO;
using System;

namespace cenDTO
{
    public class ConfigHDDT : BaseDTO
    {
        public object Nam { get; set; }
        public object URLHDDT { get; set; }
        public object Account { get; set; }
        public object ACPass { get; set; }
        public object UserName { get; set; }
        public object Pass { get; set; }
        public object Pattern { get; set; }
        public object Serial { get; set; }
        public object IDDanhMucNguoiSuDungCreate { get; set; }
        public object IDDanhMucNguoiSuDungEdit { get; set; }
        public object IDDanhMucDonVi { get; set; }
        public object IDDanhMucLoaiDoiTuong { get; set; }

        public const string tableName = "ctConfigHDDT";
        public const string listProcedureName = "List_" + tableName;
        public const string insertProcedureName = "Insert_" + tableName;
        public const string updateProcedureName = "Update_" + tableName;
        public const string deleteProcedureName = "Delete_" + tableName;

        public ConfigHDDT()
        {
            ID = null;
            Nam = null;
            URLHDDT = null;
            Account = null;
            ACPass = null;
            UserName = null;
            Pass = null;
            Pattern = null;
            Serial = null;
            IDDanhMucNguoiSuDungCreate = null;
            IDDanhMucNguoiSuDungEdit = null;
            IDDanhMucDonVi = null;
            IDDanhMucLoaiDoiTuong = null;
            CreateDate = null;
            EditDate = null;
        }
    }

    public class ConfigHDDTFilterRequest
    {
        public long? ID { get; set; }
        public int? Nam { get; set; }
        public long? IDDanhMucDonVi { get; set; }
        public long? IDDanhMucLoaiDoiTuong { get; set; }

        public ConfigHDDTFilterRequest()
        {
            ID = null;
            Nam = null;
            IDDanhMucDonVi = null;
            IDDanhMucLoaiDoiTuong = null;
        }
    }

    public class ConfigHDDTSaveRequest
    {
        public long? ID { get; set; }
        public int? Nam { get; set; }
        public string URLHDDT { get; set; }
        public string Account { get; set; }
        public string ACPass { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public string Pattern { get; set; }
        public string Serial { get; set; }
        public long? IDDanhMucDonVi { get; set; }
        public long? IDDanhMucLoaiDoiTuong { get; set; }
    }

    public class ConfigHDDTDeleteRequest
    {
        public long ID { get; set; }
    }
}
