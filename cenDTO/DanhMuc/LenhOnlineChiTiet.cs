using System;

namespace cenDTO
{
    public class LenhOnlineChiTietUpsertRequest
    {
        public long IDLenhOnline { get; set; }
        public decimal PhiLuuKho { get; set; }
        public decimal PhiGiaoNhan { get; set; }
        public decimal PhiBocXep { get; set; }
        public decimal VAT { get; set; }
        public int TrangThaiThanhToan { get; set; }
        public int TrangThaiThongQuan { get; set; }
        public string ThuKho { get; set; }
        public string Forwarder { get; set; }
        public string TenTau { get; set; }
        public string ChuHang { get; set; }
        public int? SoKien { get; set; }
        public string SoChuyen { get; set; }
        public string SoHouseBill { get; set; }
        public DateTime? NgayTauCap { get; set; }
        public decimal? TrongLuong { get; set; }
        public string SoCont { get; set; }
        public decimal? SoKhoi { get; set; }
        public string LinkTaiHoaDon { get; set; }
        public string DuongDanFileHoaDon { get; set; }
        public bool IsHoanThanh {  get; set; }
    }

    public class LenhOnlineChiTiet
    {
        public const string tableName = "tblLenhOnlineChiTiet";
        public const string tableUpsertProcedureName = "dbo.Upsert_" + tableName;
    }
}
