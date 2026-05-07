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

    [JsonPropertyName("mo_ta")]
    public string MoTa { get; set; } = string.Empty;

    [JsonPropertyName("donvi_tinh")]
    public string DonViTinh { get; set; } = string.Empty;

    [JsonPropertyName("soluong")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal SoLuong { get; set; }

    [JsonPropertyName("dongia")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGia { get; set; }

    [JsonIgnore]
    public decimal TienHang { get; set; }

    [JsonIgnore]
    public decimal TienHangThucTe => TienHang > 0m ? TienHang : SoLuong * DonGia;

    [JsonIgnore]
    public decimal TienThue { get; set; }

    [JsonIgnore]
    public decimal ThueSuat { get; set; }

    [JsonIgnore]
    public decimal TongTien => TienHangThucTe + TienThue;

    [JsonIgnore]
    public decimal ThanhTien => TienHangThucTe;
}

public sealed class PhiLuuKhoApiItem
{
    [JsonPropertyName("IDDanhMucCuoc")]
    public long IDDanhMucCuoc { get; set; }

    [JsonPropertyName("MaDanhMucCuoc")]
    public string MaDanhMucCuoc { get; set; } = string.Empty;

    [JsonPropertyName("DienGiai")]
    public string DienGiai { get; set; } = string.Empty;

    [JsonPropertyName("DonViTinh")]
    public string DonViTinh { get; set; } = string.Empty;

    [JsonPropertyName("SoLuong")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal SoLuong { get; set; }

    [JsonPropertyName("DonGia")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal DonGia { get; set; }

    [JsonPropertyName("TienHang")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal TienHang { get; set; }

    [JsonPropertyName("ThueSuat")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal ThueSuat { get; set; }

    [JsonPropertyName("TienThue")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal TienThue { get; set; }

    [JsonPropertyName("TongTien")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal TongTien { get; set; }
}

public sealed class ChiTietHouseBillResponse
{
    public bool Success { get; set; }
    public ChiTietHouseBillData? Data { get; set; }
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

public sealed class ChiTietHouseBillData
{
    [JsonPropertyName("thu_kho")]
    public string ThuKho { get; set; } = string.Empty;

    [JsonPropertyName("chu_hang")]
    public string ChuHang { get; set; } = string.Empty;

    [JsonPropertyName("so_house_bill")]
    public string SoHouseBill { get; set; } = string.Empty;

    [JsonPropertyName("so_cont")]
    public string SoCont { get; set; } = string.Empty;

    [JsonPropertyName("forwarder")]
    public string Forwarder { get; set; } = string.Empty;

    [JsonPropertyName("so_kien")]
    public int SoKien { get; set; }

    [JsonPropertyName("trong_luong")]
    public decimal TrongLuong { get; set; }

    [JsonPropertyName("so_khoi")]
    public decimal SoKhoi { get; set; }

    [JsonPropertyName("ten_tau")]
    public string TenTau { get; set; } = string.Empty;

    [JsonPropertyName("so_chuyen")]
    public string SoChuyen { get; set; } = string.Empty;

    [JsonPropertyName("ngay_tau_cap")]
    public string NgayTauCap { get; set; } = string.Empty;

    [JsonPropertyName("is_hoanthanh")]
    public bool IsHoanThanh { get; set; }
}

public sealed class OnlineOrderWorkflowResult
{
    public ThongQuanCheckResponse? ThongQuan { get; set; }
    public PhiLuuKhoResponse? PhiLuuKho { get; set; }
    public PhiLuuKhoQuaHanResponse? PhiLuuKhoQuaHan { get; set; }
    public ChiTietHouseBillResponse? ChiTietHouseBill { get; set; }
}
