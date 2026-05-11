using System.Text.Json.Serialization;

namespace webGLCv2.Models;

public sealed class ThongQuanCheckResponse
{
    public bool Success { get; set; }
    public bool IsThongQuan { get; set; }
    public string Message { get; set; } = string.Empty;
}

public sealed class PhiLuuKhoResponse
{
    public bool Success { get; set; }
    public PhiLuuKhoData? Data { get; set; }
    public string Message { get; set; } = string.Empty;
}

public sealed class PhiLuuKhoQuaHanResponse
{
    public bool Success { get; set; }
    public PhiLuuKhoData? Data { get; set; }
    public string Message { get; set; } = string.Empty;
}

public sealed class PhiLuuKhoData
{
    [JsonPropertyName("chi_tiet_hoa_don")]
    public List<PhiLuuKhoItem> ChiTietHoaDon { get; set; } = [];

    [JsonPropertyName("vat")]
    public decimal Vat { get; set; }

    [JsonPropertyName("don_vi_tien_te")]
    public string DonViTienTe { get; set; } = string.Empty;

    [JsonPropertyName("trangthai_thanhtoan")]
    public int TrangThaiThanhToan { get; set; } = 0;
}

public sealed class PhiLuuKhoItem
{
    [JsonPropertyName("id_danh_muc_cuoc")]
    public long IDDanhMucCuoc { get; set; }

    [JsonPropertyName("ma_danh_muc_cuoc")]
    public string MaDanhMucCuoc { get; set; } = string.Empty;

    [JsonPropertyName("ten_danh_muc_cuoc")]
    public string TenDanhMucCuoc { get; set; } = string.Empty;

    [JsonPropertyName("mo_ta")]
    public string MoTa { get; set; } = string.Empty;

    [JsonPropertyName("donvi_tinh")]
    public string DonViTinh { get; set; } = string.Empty;

    [JsonPropertyName("soluong")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal SoLuong { get; set; }

    [JsonPropertyName("ngay_luu_kho")]
    public DateTime? NgayLuuKho { get; set; }

    [JsonPropertyName("dongia")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGia { get; set; }

    [JsonPropertyName("don_gia_cuoc")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGiaCuoc { get; set; }

    [JsonPropertyName("don_gia_tra_dai_ly_theo_hop_dong")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGiaTraDaiLyTheoHopDong { get; set; }

    [JsonPropertyName("don_gia_tra_dai_ly_thu_them")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGiaTraDaiLyThuThem { get; set; }

    [JsonPropertyName("id_danh_muc_thue_suat")]
    public long IDDanhMucThueSuat { get; set; }

    [JsonIgnore]
    public decimal TienHang { get; set; }

    [JsonIgnore]
    public decimal TienHangThucTe => TienHang > 0m ? TienHang : SoLuong * DonGia;

    [JsonIgnore]
    public decimal TienThue { get; set; }

    [JsonPropertyName("thue_suat")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal ThueSuat { get; set; }

    [JsonIgnore]
    public decimal ThanhTien { get; set; }

    [JsonIgnore]
    public decimal TongTien => TienHangThucTe + TienThue;

    [JsonPropertyName("ma_danh_muc_tai_khoan_ke_toan_doanh_thu")]
    public string MaDanhMucTaiKhoanKeToanDoanhThu { get; set; } = string.Empty;

    [JsonPropertyName("ma_danh_muc_tai_khoan_ke_toan_thanh_toan")]
    public string MaDanhMucTaiKhoanKeToanThanhToan { get; set; } = string.Empty;

    [JsonPropertyName("ma_danh_muc_tai_khoan_ke_toan_thue")]
    public string MaDanhMucTaiKhoanKeToanThue { get; set; } = string.Empty;
}

public sealed class PhiLuuKhoApiItem
{
    [JsonPropertyName("IDDanhMucCuoc")]
    public long IDDanhMucCuoc { get; set; }

    [JsonPropertyName("MaDanhMucCuoc")]
    public string MaDanhMucCuoc { get; set; } = string.Empty;

    [JsonPropertyName("TenDanhMucCuoc")]
    public string TenDanhMucCuoc { get; set; } = string.Empty;

    [JsonPropertyName("DienGiai")]
    public string DienGiai { get; set; } = string.Empty;

    [JsonPropertyName("DonViTinh")]
    public string DonViTinh { get; set; } = string.Empty;

    [JsonPropertyName("SoLuong")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal SoLuong { get; set; }

    [JsonPropertyName("NgayLuuKho")]
    public DateTime? NgayLuuKho { get; set; }

    [JsonPropertyName("DonGia")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGia { get; set; }

    [JsonPropertyName("DonGiaCuoc")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGiaCuoc { get; set; }

    [JsonPropertyName("DonGiaTraDaiLyTheoHopDong")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGiaTraDaiLyTheoHopDong { get; set; }

    [JsonPropertyName("DonGiaTraDaiLyThuThem")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGiaTraDaiLyThuThem { get; set; }

    [JsonPropertyName("TienHang")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal TienHang { get; set; }

    [JsonPropertyName("IDDanhMucThueSuat")]
    public long IDDanhMucThueSuat { get; set; }

    [JsonPropertyName("ThueSuat")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal ThueSuat { get; set; }

    [JsonPropertyName("TienThue")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal TienThue { get; set; }

    [JsonPropertyName("ThanhTien")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal ThanhTien { get; set; }

    [JsonPropertyName("TongTien")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal TongTien { get; set; }

    [JsonPropertyName("MaDanhMucTaiKhoanKeToanDoanhThu")]
    public string MaDanhMucTaiKhoanKeToanDoanhThu { get; set; } = string.Empty;

    [JsonPropertyName("MaDanhMucTaiKhoanKeToanThanhToan")]
    public string MaDanhMucTaiKhoanKeToanThanhToan { get; set; } = string.Empty;

    [JsonPropertyName("MaDanhMucTaiKhoanKeToanThue")]
    public string MaDanhMucTaiKhoanKeToanThue { get; set; } = string.Empty;
}

public sealed class ChiTietHouseBillResponse
{
    public bool Success { get; set; }
    public string Data { get; set; } = string.Empty;
    [JsonIgnore]
    public ChiTietHouseBillData? ParsedData { get; set; }
    public string Message { get; set; } = string.Empty;
}

public sealed class ThongTinThanhToanResponse
{
    public bool Success { get; set; }
    public ThongTinThanhToanData? Data { get; set; }
    public string Message { get; set; } = string.Empty;
}

public sealed class ThongTinThanhToanData
{
    [JsonPropertyName("id_payment")]
    public string IdPayment { get; set; } = string.Empty;

    [JsonPropertyName("so_tien")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal SoTien { get; set; }

    [JsonPropertyName("so_taikhoan")]
    public string SoTaiKhoan { get; set; } = string.Empty;

    [JsonPropertyName("ten_taikhoan")]
    public string TenTaiKhoan { get; set; } = string.Empty;

    [JsonPropertyName("ten_nganhang")]
    public string TenNganHang { get; set; } = string.Empty;

    [JsonPropertyName("qrcode_base64")]
    public string QrCodeBase64 { get; set; } = string.Empty;
}

public sealed class LenhXuatKhoHangNhapKhauTempInsertRequest
{
    [JsonPropertyName("LenhXuatKho")]
    public LenhXuatKhoHangNhapKhauTempInsertData LenhXuatKho { get; set; } = new();

    [JsonPropertyName("DanhSachPhi")]
    public List<PhiLuuKhoApiItem> DanhSachPhi { get; set; } = [];
}

public sealed class LenhXuatKhoHangNhapKhauTempInsertData
{
    [JsonPropertyName("NgayLap")]
    public string NgayLap { get; set; } = string.Empty;

    [JsonPropertyName("NgayGiaHan")]
    public string NgayGiaHan { get; set; } = string.Empty;

    [JsonPropertyName("SoVanDon")]
    public string SoVanDon { get; set; } = string.Empty;

    [JsonPropertyName("IDctLenhNhapKhoHangNhapKhauChiTiet")]
    public long IDctLenhNhapKhoHangNhapKhauChiTiet { get; set; }

    [JsonPropertyName("SoLuongQuaKho")]
    public decimal SoLuongQuaKho { get; set; }

    [JsonPropertyName("SoLuongQuaTai")]
    public decimal SoLuongQuaTai { get; set; }

    [JsonPropertyName("MaSoThue")]
    public string MaSoThue { get; set; } = string.Empty;

    [JsonPropertyName("HoTenNguoiNhanHang")]
    public string HoTenNguoiNhanHang { get; set; } = string.Empty;

    [JsonPropertyName("SoCMND")]
    public string SoCMND { get; set; } = string.Empty;

    [JsonPropertyName("SoDienThoaiNguoiNhanHang")]
    public string SoDienThoaiNguoiNhanHang { get; set; } = string.Empty;

    [JsonPropertyName("SoLuongKienXuat")]
    public int SoLuongKienXuat { get; set; }

    [JsonPropertyName("KhoiLuongXuat")]
    public decimal KhoiLuongXuat { get; set; }

    [JsonPropertyName("CBMXuat")]
    public decimal CBMXuat { get; set; }

    [JsonPropertyName("IDDanhMucCuaLamHang")]
    public long IDDanhMucCuaLamHang { get; set; }

    [JsonPropertyName("SoToKhai")]
    public string SoToKhai { get; set; } = string.Empty;

    [JsonPropertyName("GhiChu")]
    public string GhiChu { get; set; } = string.Empty;

    [JsonPropertyName("IDDanhMucKhachHangDoiLenh")]
    public long IDDanhMucKhachHangDoiLenh { get; set; }
}

public sealed class LenhXuatKhoHangNhapKhauTempInsertResponse
{
    [JsonPropertyName("ID")]
    public int ID { get; set; }

    [JsonPropertyName("So")]
    public string So { get; set; } = string.Empty;

    [JsonPropertyName("QRCode")]
    public string QRCode { get; set; } = string.Empty;
}

public sealed class ChiTietHouseBillData
{
    [JsonPropertyName("ID")]
    public int ID { get; set; }

    [JsonPropertyName("SoVanDon")]
    public string SoVanDon { get; set; } = string.Empty;

    [JsonPropertyName("TrangThaiKhoa")]
    public bool TrangThaiKhoa { get; set; }

    [JsonPropertyName("SoContainer")]
    public string SoContainer { get; set; } = string.Empty;

    [JsonPropertyName("MoTaHangHoa")]
    public string MoTaHangHoa { get; set; } = string.Empty;

    [JsonPropertyName("TenChuHang")]
    public string TenChuHang { get; set; } = string.Empty;

    [JsonPropertyName("MaDanhMucDonViTinh")]
    public string MaDanhMucDonViTinh { get; set; } = string.Empty;

    [JsonPropertyName("SoLuongKienNhap")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal SoLuongKienNhap { get; set; }

    [JsonPropertyName("KhoiLuongNhap")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal KhoiLuongNhap { get; set; }

    [JsonPropertyName("CBMNhap")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal CBMNhap { get; set; }

    [JsonPropertyName("DonGiaLuuKho")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGiaLuuKho { get; set; }

    [JsonPropertyName("DonGiaGiaoNhan")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGiaGiaoNhan { get; set; }

    [JsonPropertyName("DonGiaBocXep")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGiaBocXep { get; set; }

    [JsonPropertyName("DonGiaQuanLy")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGiaQuanLy { get; set; }

    [JsonPropertyName("HangNguyHiem")]
    public bool HangNguyHiem { get; set; }

    [JsonPropertyName("HangQuaKho")]
    public bool HangQuaKho { get; set; }

    [JsonPropertyName("SoLuongQuaKho")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal SoLuongQuaKho { get; set; }

    [JsonPropertyName("HangQuaTai")]
    public bool HangQuaTai { get; set; }

    [JsonPropertyName("SoLuongQuaTai")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal SoLuongQuaTai { get; set; }

    [JsonPropertyName("EDO")]
    public string EDO { get; set; } = string.Empty;

    [JsonPropertyName("MaDanhMucCuaLamHang")]
    public string MaDanhMucCuaLamHang { get; set; } = string.Empty;

    [JsonPropertyName("IDDanhMucCuaLamHang")]
    public long IDDanhMucCuaLamHang { get; set; }

    [JsonPropertyName("NgayNhapKho")]
    public DateTime? NgayNhapKho { get; set; }

    [JsonPropertyName("NgayGiaHanCuoi")]
    public DateTime? NgayGiaHanCuoi { get; set; }

    [JsonPropertyName("MaDanhMucDaiLy")]
    public string MaDanhMucDaiLy { get; set; } = string.Empty;

    [JsonPropertyName("IDctKeHoachKhaiThacHangNhapKhau")]
    public long IDctKeHoachKhaiThacHangNhapKhau { get; set; }

    [JsonPropertyName("SoSeal")]
    public string SoSeal { get; set; } = string.Empty;

    [JsonPropertyName("MaDanhMucLoaiContainer")]
    public string MaDanhMucLoaiContainer { get; set; } = string.Empty;

    [JsonPropertyName("MasterBill")]
    public string MasterBill { get; set; } = string.Empty;

    [JsonPropertyName("MaDanhMucHangTau")]
    public string MaDanhMucHangTau { get; set; } = string.Empty;

    [JsonPropertyName("TenTau")]
    public string TenTau { get; set; } = string.Empty;

    [JsonPropertyName("SoChuyen")]
    public string SoChuyen { get; set; } = string.Empty;

    [JsonPropertyName("NgayTauDen")]
    public DateTime? NgayTauDen { get; set; }

    [JsonPropertyName("GhiChu")]
    public string GhiChu { get; set; } = string.Empty;

    [JsonIgnore]
    public string ThuKho { get; set; } = string.Empty;

    [JsonIgnore]
    public string ChuHang { get; set; } = string.Empty;

    [JsonIgnore]
    public string SoHouseBill { get; set; } = string.Empty;

    [JsonIgnore]
    public string SoCont { get; set; } = string.Empty;

    [JsonIgnore]
    public string Forwarder { get; set; } = string.Empty;

    [JsonIgnore]
    public int SoKien { get; set; }

    [JsonIgnore]
    public decimal TrongLuong { get; set; }

    [JsonIgnore]
    public decimal SoKhoi { get; set; }

    [JsonIgnore]
    public string NgayTauCap { get; set; } = string.Empty;

    [JsonIgnore]
    public bool IsHoanThanh { get; set; }

    [JsonIgnore]
    public long ResolvedIDDanhMucCuaLamHang
        => IDDanhMucCuaLamHang > 0
            ? IDDanhMucCuaLamHang
            : long.TryParse(MaDanhMucCuaLamHang, out var parsedId) ? parsedId : 0;
}

public sealed class OnlineOrderWorkflowResult
{
    public ThongQuanCheckResponse? ThongQuan { get; set; }
    public PhiLuuKhoResponse? PhiLuuKho { get; set; }
    public PhiLuuKhoQuaHanResponse? PhiLuuKhoQuaHan { get; set; }
    public ChiTietHouseBillResponse? ChiTietHouseBill { get; set; }
}
