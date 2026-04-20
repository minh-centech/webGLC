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
    [JsonPropertyName("mo_ta")]
    public string MoTa { get; set; } = string.Empty;

    [JsonPropertyName("so_tien")]
    public decimal SoTien { get; set; }
}

public sealed class ChiTietHouseBillResponse
{
    public bool Success { get; set; }
    public ChiTietHouseBillData? Data { get; set; }
    public string Message { get; set; } = string.Empty;
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
}

public sealed class OnlineOrderWorkflowResult
{
    public ThongQuanCheckResponse? ThongQuan { get; set; }
    public PhiLuuKhoResponse? PhiLuuKho { get; set; }
    public ChiTietHouseBillResponse? ChiTietHouseBill { get; set; }
}
