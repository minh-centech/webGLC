using Microsoft.Extensions.Options;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using webGLCv2.Models;
using static System.Net.Mime.MediaTypeNames;

namespace webGLCv2.Services;

public sealed class OnlineOrderService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly IHttpClientFactory _factory;

    private const string TraCuuToKhaiApiPath = "/api/TraCuuToKhai/TraCuuToKhai";
    private const string PhiLuuKhoApiPath = "/api/TinhCuoc/TinhCuoc";
    //private const string PhiLuuKhoQuaHanApiPath = "https://mock.apidog.com/m1/1263694-1261439-default/PhiLuuKhoQuaHan";
    private const string ChiTietHouseBillApiPath = "/api/ctLenhNhapKhoHangNhapKhau/ListChiTietSoVanDon";
    private const string ChiTietHouseBillValidApiPath = "/api/ctLenhNhapKhoHangNhapKhau/ListValidSoVanDonXuatKho";

    //private const string ThongTinThanhToanApiPath = "https://mock.apidog.com/m1/1263694-1261439-default/ThongTinThanhToan";
    private const string MaSoThueLookupApiPath = "/api/DanhMucKhachHang/ListValidMaSoThue";

    private readonly HttpClient _httpClient;
    private readonly OnlineOrderWorkflowOptions _workflowOptions;
    private readonly List<ImportCheckRecord> _importChecks = CreateImportCheckSeedData();

    public OnlineOrderService(HttpClient httpClient, IOptions<OnlineOrderWorkflowOptions> workflowOptions, IHttpClientFactory factory)
    {
        _httpClient = httpClient;
        _workflowOptions = workflowOptions.Value;
        _factory = factory;
    }

    public async Task<OnlineOrderPageResult> GetOrdersAsync(
        long idDanhMucKhachHangDoiLenh,
        DateTime? tuNgay = null,
        DateTime? denNgay = null,
        string? houseBill = null,
        string? soCont = null,
        string? maSoThue = null,
        int page = 1,
        int pageSize = 10)
    {
        // Dung service Legacy
        var legacyClient = _factory.CreateClient("LegacyApi");

        var response = await legacyClient.PostAsJsonAsync(
            "api/LenhOnlines/List",
            new
            {
                IDDanhMucKhachHangDoiLenh = idDanhMucKhachHangDoiLenh,
                TuNgay = tuNgay,
                DenNgay = denNgay,
                HouseBill = NullIfEmpty(houseBill),
                SoCont = NullIfEmpty(soCont),
                MaSoThue = NullIfEmpty(maSoThue),
                Page = page,
                PageSize = pageSize
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Khong the tai danh sach lenh online.");

        using var document = JsonDocument.Parse(envelope!.Data);
        var root = document.RootElement;
        var itemsElement = root.TryGetProperty("Items", out var itemsValue) ? itemsValue : root;
        var orders = new List<OnlineOrderRecord>();

        foreach (var item in itemsElement.EnumerateArray())
        {
            orders.Add(MapOnlineOrder(item));
        }

        orders = orders
            .OrderBy(order => order.StatusCode == 0 ? 0 : 1)
            .ThenByDescending(order => order.OrderDate)
            .ThenByDescending(order => long.TryParse(order.Id, out var parsedId) ? parsedId : 0)
            .ToList();

        return new OnlineOrderPageResult
        {
            Items = orders,
            TotalCount = root.TryGetProperty("TotalCount", out var totalCountValue) && totalCountValue.TryGetInt32(out var totalCount) ? totalCount : orders.Count,
            Page = root.TryGetProperty("Page", out var pageValue) && pageValue.TryGetInt32(out var currentPage) ? currentPage : page,
            PageSize = root.TryGetProperty("PageSize", out var pageSizeValue) && pageSizeValue.TryGetInt32(out var currentPageSize) ? currentPageSize : pageSize
        };
    }


    public Task<OnlineOrderPageResult> GetOrdersForAdminAsync(
        DateTime? tuNgay = null,
        DateTime? denNgay = null,
        string? houseBill = null,
        string? soCont = null,
        string? maSoThue = null,
        int page = 1,
        int pageSize = 10, int ReceiptStatusFilter = -1)
        => GetOrdersInternalAsync(
            null,
            tuNgay,
            denNgay,
            houseBill,
            soCont,
            maSoThue,
            page,
            pageSize, ReceiptStatusFilter);

    private async Task<OnlineOrderPageResult> GetOrdersInternalAsync(
        long? idDanhMucKhachHangDoiLenh,
        DateTime? tuNgay,
        DateTime? denNgay,
        string? houseBill,
        string? soCont,
        string? maSoThue,
        int page,
        int pageSize, int ReceiptStatusFilter = -1)
    {
        var legacyClient = _factory.CreateClient("LegacyApi");

        var response = await legacyClient.PostAsJsonAsync(
            "api/LenhOnlines/List",
            new
            {
                IDDanhMucKhachHangDoiLenh = idDanhMucKhachHangDoiLenh,
                TuNgay = tuNgay,
                DenNgay = denNgay,
                HouseBill = NullIfEmpty(houseBill),
                SoCont = NullIfEmpty(soCont),
                MaSoThue = NullIfEmpty(maSoThue),
                Page = page,
                PageSize = pageSize,
                TrangThaiThanhToanBienNhanGoc = ReceiptStatusFilter
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Khong the tai danh sach lenh online.");

        using var document = JsonDocument.Parse(envelope!.Data);
        var root = document.RootElement;
        var itemsElement = root.TryGetProperty("Items", out var itemsValue) ? itemsValue : root;
        var orders = new List<OnlineOrderRecord>();

        foreach (var item in itemsElement.EnumerateArray())
        {
            orders.Add(MapOnlineOrder(item));
        }

        return new OnlineOrderPageResult
        {
            Items = orders,
            TotalCount = root.TryGetProperty("TotalCount", out var totalCountValue) && totalCountValue.TryGetInt32(out var totalCount) ? totalCount : orders.Count,
            Page = root.TryGetProperty("Page", out var pageValue) && pageValue.TryGetInt32(out var currentPage) ? currentPage : page,
            PageSize = root.TryGetProperty("PageSize", out var pageSizeValue) && pageSizeValue.TryGetInt32(out var currentPageSize) ? currentPageSize : pageSize
        };
    }
    public async Task<OnlineOrderRecord?> GetOrderByIdAsync(long idDanhMucKhachHangDoiLenh, long orderId)
    {
        // Legacy
        var legacyClient = _factory.CreateClient("LegacyApi");
        var response = await legacyClient.PostAsJsonAsync(
            "api/LenhOnlines/List",
            new
            {
                ID = orderId,
                IDDanhMucKhachHangDoiLenh = idDanhMucKhachHangDoiLenh,
                Page = 1,
                PageSize = 1
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Khong the tai chi tiet lenh online.");

        using var document = JsonDocument.Parse(envelope!.Data);
        var root = document.RootElement;
        var itemsElement = root.TryGetProperty("Items", out var itemsValue) ? itemsValue : root;

        if (itemsElement.ValueKind != JsonValueKind.Array || itemsElement.GetArrayLength() == 0)
        {
            return null;
        }

        return MapOnlineOrder(itemsElement[0]);
    }


    public async Task<OnlineOrderRecord?> GetOrderByIdForAdminAsync(long orderId)
    {
        // Legacy
        var legacyClient = _factory.CreateClient("LegacyApi");

        var response = await legacyClient.PostAsJsonAsync(
            "api/LenhOnlines/List",
            new
            {
                ID = orderId,
                IDDanhMucKhachHangDoiLenh = (long?)null,
                Page = 1,
                PageSize = 1
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Khong the tai chi tiet lenh online.");

        using var document = JsonDocument.Parse(envelope!.Data);
        var root = document.RootElement;
        var itemsElement = root.TryGetProperty("Items", out var itemsValue) ? itemsValue : root;

        if (itemsElement.ValueKind != JsonValueKind.Array || itemsElement.GetArrayLength() == 0)
        {
            return null;
        }

        return MapOnlineOrder(itemsElement[0]);
    }

    public Task<ThongQuanCheckResponse> CheckThongQuanAsync(string houseBill, string soCont)
        => CheckThongQuanAsync(houseBill, soCont, null);

    public async Task<ThongQuanCheckResponse> CheckThongQuanAsync(
    string houseBill,
    string soCont,
    ChiTietHouseBillData? chiTiet)
    {
        var isTest = _workflowOptions.TestCheckThongQuan;
        var ngayTauDenEim = BuildNgayTauDenEimText(chiTiet);

        var soDinhDanhHangHoa = !string.IsNullOrWhiteSpace(chiTiet?.SoDinhDanhHangHoa)
            ? chiTiet!.SoDinhDanhHangHoa
            : houseBill;

        var soHieuPhuongTienVanTai = !string.IsNullOrWhiteSpace(chiTiet?.SoHieuPhuongTienVanTai)
            ? chiTiet!.SoHieuPhuongTienVanTai
            : string.IsNullOrWhiteSpace(chiTiet?.TenTau)
                ? soCont
                : chiTiet!.TenTau;

        var requestPayload = new
        {
            SoVanDon = houseBill,
            NgayTauDen = ngayTauDenEim,
            SoDinhDanhHangHoa = soDinhDanhHangHoa,
            SoHieuPhuongTienVanTai = soHieuPhuongTienVanTai
        };

        //var requestUrl = BuildWorkflowUrl(TraCuuToKhaiApiPath);
        var requestUrl = TraCuuToKhaiApiPath;

        Console.WriteLine($"[TraCuuToKhai] POST {requestUrl}");
        Console.WriteLine($"[TraCuuToKhai] Request={JsonSerializer.Serialize(requestPayload, JsonOptions)}");

        string responseBody;

        try
        {
            // =========================
            // TEST MODE
            // =========================
            if (isTest)
            {
                string SoToKhai = "012345678910";
                //string maHaiQuan = string.Join("", SoToKhai);
                responseBody = $$"""
            {
                "Status": 0,
                "Data": "{\"ID\":\"0\",\"IDDonVi\":null,\"IDChungTu\":null,\"IDDanhMucChungTu\":\"001.020\",\"SoHieuPhuongTienVanTai\":\"HF FORTUNE\",\"SoChuyen\":\"2616S\",\"NgayTauDen\":\"2026-06-12T00:00:00\",\"SoVanDon\":\"VTRS2606E029\",\"SoDinhDanhHangHoa\":\"VTRS2606E029\",\"SoLuong\":2.0,\"DonViTinh\":\"PP\",\"ViTriKho\":\"\",\"MoTaHangHoa\":\"HÀNG BÁCH HOÁ\",\"GhiChu\":\"Container rút hàng\",\"SoToKhai\":\"012345678910\",\"NgayToKhai\":\"2026-06-15 00:00:00\",\"MaHaiQuanDangKyToKhai\":\"03YY\",\"MaLoaiHinh\":\"A12\",\"MaHaiQuanGiamSat\":\"03TG\",\"ThoiGianKetXuatDuLieu\":\"2026-06-17T16:24:19\",\"LuongToKhai\":\"2\",\"TrangThaiToKhai\":\"TQ\",\"MaDoanhNghiepXuatKhau\":\"0200168458\",\"TenDoanhNghiepXuatKhau\":\"CôNG TY Cổ PHầN ắC QUY TIA SáNG\",\"CUS_reference\":null,\"CUS_customsReference\":null,\"CUS_acceptance\":null,\"CUS_message\":null,\"IDDanhMucNguoiSuDungCreate\":null,\"CreateDate\":null}",
                "ErrorMsg": "Loi 12 thu lai sau 10 giay"
            }
            """;

            }
            else
            {
                var response = await _httpClient.PostAsJsonAsync(
                    requestUrl,
                    requestPayload,
                    JsonOptions);

                responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"[TraCuuToKhai] Response={(int)response.StatusCode} {response.ReasonPhrase}");

                if (!response.IsSuccessStatusCode)
                {
                    return BuildThongQuanFailureResponse("Lỗi! Không kết nối được đến dịch vụ Hải quan.");
                }
            }

            Console.WriteLine($"[TraCuuToKhai] ResponseBody={responseBody}");

            ApiEnvelope? envelope;

            try
            {
                envelope = JsonSerializer.Deserialize<ApiEnvelope>(responseBody, JsonOptions);
            }
            catch (JsonException)
            {
                return BuildThongQuanFailureResponse("Không thể đọc kết quả kiểm tra thông quan.");
            }

            if (envelope == null)
            {
                return BuildThongQuanFailureResponse("Không thể đọc kết quả kiểm tra thông quan.");
            }

            if (envelope.Status != 0)
            {
                return BuildThongQuanFailureResponse(
                    FormatThongQuanErrorMessage(envelope.ErrorMsg));
            }

            if (string.IsNullOrWhiteSpace(envelope.Data))
            {
                return BuildThongQuanFailureResponse(
                    "Không thể đọc kết quả kiểm tra thông quan.");
            }

            TraCuuToKhaiData? declarationData;

            try
            {
                declarationData = JsonSerializer.Deserialize<TraCuuToKhaiData>(
                    envelope.Data,
                    JsonOptions);
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Loi ko parse json tra ve" + ex.ToString());
                declarationData = null;
            }

            return new ThongQuanCheckResponse
            {
                Success = true,
                IsThongQuan = string.Equals(
                    declarationData?.TrangThaiToKhai,
                    "TQ",
                    StringComparison.OrdinalIgnoreCase),

                Message = string.Empty,
                RawData = envelope.Data,
                Data = declarationData
            };
        }
        catch (TaskCanceledException ex)
            when (!ex.CancellationToken.IsCancellationRequested)
        {
            return BuildThongQuanFailureResponse(
                "Không kết nối được đến Hải quan. Hệ thống phản hồi quá lâu.");
        }
        catch (HttpRequestException)
        {
            return BuildThongQuanFailureResponse(
                "Không kết nối được đến Hải quan.");
        }
    }

    private static ThongQuanCheckResponse BuildThongQuanFailureResponse(string message)
        => new()
        {
            Success = false,
            IsThongQuan = false,
            Message = message,
            RawData = string.Empty,
            Data = null
        };

    private static string FormatThongQuanErrorMessage(string? errorMsg)
    {
        if (string.IsNullOrWhiteSpace(errorMsg))
        {
            return "Không thể tra cứu tình trạng thông quan.";
        }

        var trimmed = errorMsg.Trim();
        if (LooksLikeConnectionFailure(trimmed))
        {
            return "Không kết nối được đến Hải quan.";
        }

        var customsRejectionMessage = TryFormatCustomsRejectionMessage(trimmed);
        if (!string.IsNullOrWhiteSpace(customsRejectionMessage))
        {
            return customsRejectionMessage;
        }

        return trimmed;
    }

    private static bool LooksLikeConnectionFailure(string message)
        => message.Contains("Không kết nối", StringComparison.OrdinalIgnoreCase)
           || message.Contains("khong ket noi", StringComparison.OrdinalIgnoreCase)
           || message.Contains("connect", StringComparison.OrdinalIgnoreCase)
           || message.Contains("connection", StringComparison.OrdinalIgnoreCase)
           || message.Contains("timeout", StringComparison.OrdinalIgnoreCase)
           || message.Contains("timed out", StringComparison.OrdinalIgnoreCase)
           || message.Contains("refused", StringComparison.OrdinalIgnoreCase)
           || message.Contains("unreachable", StringComparison.OrdinalIgnoreCase)
           || message.Contains("could not be resolved", StringComparison.OrdinalIgnoreCase);

    private static string? TryFormatCustomsRejectionMessage(string errorMsg)
    {
        var xmlStart = errorMsg.IndexOf("<Declaration>", StringComparison.OrdinalIgnoreCase);
        if (xmlStart < 0)
        {
            return null;
        }

        var xml = errorMsg[xmlStart..].Trim();
        var xmlEnd = xml.LastIndexOf("</Declaration>", StringComparison.OrdinalIgnoreCase);
        if (xmlEnd >= 0)
        {
            xml = xml[..(xmlEnd + "</Declaration>".Length)];
        }

        try
        {
            var document = XDocument.Parse(xml, LoadOptions.None);
            var root = document.Root;
            if (root is null)
            {
                return null;
            }

            var reference = GetXmlDescendantValue(root, "reference");
            var function = GetXmlDescendantValue(root, "function");
            var additionalInformation = GetXmlDescendantValue(root, "AdditionalInformation");
            var additionalContent = GetXmlDescendantValue(root, "content");

            var parts = new List<string> { "Hải Quan từ chối tra cứu." };

            if (!string.IsNullOrWhiteSpace(reference))
            {
                parts.Add($"Mã tham chiếu: {reference}");
            }

            //if (!string.IsNullOrWhiteSpace(function))
            //{
            //    parts.Add($"function: {function}");
            //}

            var additionalMessage = !string.IsNullOrWhiteSpace(additionalContent)
                ? additionalContent
                : additionalInformation;

            if (!string.IsNullOrWhiteSpace(additionalMessage))
            {
                parts.Add($"Nội dung: {NormalizeWhitespace(additionalMessage)}");
            }

            return string.Join(" | ", parts);
        }
        catch
        {
            return null;
        }
    }

    private static string GetXmlDescendantValue(XElement root, string localName)
        => root.Descendants()
            .FirstOrDefault(element => string.Equals(element.Name.LocalName, localName, StringComparison.OrdinalIgnoreCase))
            ?.Value.Trim() ?? string.Empty;

    private static string NormalizeWhitespace(string value)
        => string.Join(" ", value.Split(new[] { '\r', '\n', '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries));
    private static string BuildNgayTauDenEimText(ChiTietHouseBillData? chiTiet)
    {
        if (chiTiet is null)
        {
            return DateTime.Today.ToString("MM/dd/yyyy 12:00:00 AM", CultureInfo.InvariantCulture);
        }

        if (!string.IsNullOrWhiteSpace(chiTiet.NgayTauDenEIM))
        {
            return chiTiet.NgayTauDenEIM;
        }

        var sourceDate = chiTiet.NgayTauDen ?? chiTiet.NgayNhapKho ?? DateTime.Today;
        return sourceDate.ToString("MM/dd/yyyy 12:00:00 AM", CultureInfo.InvariantCulture);
    }

    public async Task<CompanyTaxLookupResult?> LookupCompanyByMaSoThueAsync(string maSoThue)
    {
        var normalizedMaSoThue = NullIfEmpty(maSoThue);
        if (string.IsNullOrWhiteSpace(normalizedMaSoThue))
        {
            return null;
        }

        var response = await _httpClient.PostAsJsonAsync(
           //BuildWorkflowUrl(MaSoThueLookupApiPath),
           MaSoThueLookupApiPath,
            new { MaSoThue = normalizedMaSoThue },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Lỗi khi tra cứu mã số thuế.");

        if (string.IsNullOrWhiteSpace(envelope!.Data))
        {
            return null;
        }

        using var document = JsonDocument.Parse(envelope.Data);
        var root = document.RootElement;
        if (root.ValueKind != JsonValueKind.Array || root.GetArrayLength() == 0)
        {
            return null;
        }

        var item = root[0];
        var editDate = GetString(item, "EditDate");
        string formattedDate = DateTimeOffset.TryParse(editDate, out DateTimeOffset parsedDate) ? parsedDate.ToString("dd/MM/yyyy") : "-";
        return new CompanyTaxLookupResult
        {
            MaSoThue = GetString(item, "MaSoThue"),
            Ten = GetString(item, "Ten"),
            DiaChi = GetString(item, "DiaChi"),
            Email = GetString(item, "Email"),
            SoDienThoai = GetString(item, "SoDienThoai"),
            NguoiDaiDien = GetString(item, "NguoiDaiDien"),
            ChucVuNguoiDaiDien = GetString(item, "ChucVuNguoiDaiDien"),
            CreateDate = GetString(item, "CreateDate"),
            NgungSuDung = GetBool(item, "NgungSuDung"),
            EditDate = formattedDate
        };
    }

    public async Task<bool> HasValidMaSoThueAsync(string maSoThue)
    {
        var normalizedMaSoThue = NullIfEmpty(maSoThue);
        if (string.IsNullOrWhiteSpace(normalizedMaSoThue))
        {
            return false;
        }

        var response = await _httpClient.PostAsJsonAsync(
            //BuildWorkflowUrl(MaSoThueLookupApiPath),
            MaSoThueLookupApiPath,
            new { MaSoThue = normalizedMaSoThue },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Lỗi khi tra cứu mã số thuế.");

        if (string.IsNullOrWhiteSpace(envelope?.Data))
        {
            return false;
        }

        try
        {
            using var document = JsonDocument.Parse(envelope.Data);
            var root = document.RootElement;
            return root.ValueKind switch
            {
                JsonValueKind.Array => root.GetArrayLength() > 0,
                JsonValueKind.Object => root.EnumerateObject().Any(),
                JsonValueKind.String => !string.IsNullOrWhiteSpace(root.GetString()),
                _ => true
            };
        }
        catch (JsonException)
        {
            return !string.IsNullOrWhiteSpace(envelope.Data);
        }
    }

    public async Task<int> GetMaSoThueLookupStatusAsync(string maSoThue)
    {
        var normalizedMaSoThue = NullIfEmpty(maSoThue);
        if (string.IsNullOrWhiteSpace(normalizedMaSoThue))
        {
            throw new InvalidOperationException("Vui lòng nhập mã số thuế trước khi kiểm tra.");
        }

        var response = await _httpClient.PostAsJsonAsync(
            //BuildWorkflowUrl(MaSoThueLookupApiPath),
            MaSoThueLookupApiPath,
            new { MaSoThue = normalizedMaSoThue },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        if (envelope is null)
        {
            throw new InvalidOperationException("Không thể đọc kết quả kiểm tra mã số thuế.");
        }

        return envelope.Status;
    }

    public async Task<UpsertDanhMucKhachHangResult> UpsertDanhMucKhachHangAsync(
        string ma,
        string ten,
        string diaChi,
        string maSoThue,
        string nguoiDaiDien,
        string chucVuNguoiDaiDien,
        string email,
        string soDienThoai,
        string ghiChu,
        bool isUpdate)
    {
        var requestPayload = new UpsertDanhMucKhachHangRequest
        {
            Ma = NullIfEmpty(ma) ?? string.Empty,
            Ten = NullIfEmpty(ten) ?? string.Empty,
            DiaChi = NullIfEmpty(diaChi) ?? string.Empty,
            MaSoThue = NullIfEmpty(maSoThue) ?? string.Empty,
            NguoiDaiDien = NullIfEmpty(nguoiDaiDien) ?? string.Empty,
            ChucVuNguoiDaiDien = NullIfEmpty(chucVuNguoiDaiDien) ?? string.Empty,
            Email = NullIfEmpty(email) ?? string.Empty,
            SoDienThoai = NullIfEmpty(soDienThoai) ?? string.Empty,
            GhiChu = NullIfEmpty(ghiChu) ?? string.Empty,
            IsUpdate = isUpdate
        };

        //var requestUrl = BuildWorkflowUrl("/api/DanhMucKhachHang/UpsertDanhMucKhachHang");
        var requestUrl = "/api/DanhMucKhachHang/UpsertDanhMucKhachHang";

        Console.WriteLine($"[UpsertDanhMucKhachHang] POST {requestUrl}");
        Console.WriteLine($"[UpsertDanhMucKhachHang] Request={JsonSerializer.Serialize(requestPayload, JsonOptions)}");

        var response = await _httpClient.PostAsJsonAsync(
            requestUrl,
            requestPayload,
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"[UpsertDanhMucKhachHang] Response={(int)response.StatusCode} {response.ReasonPhrase}");
        Console.WriteLine($"[UpsertDanhMucKhachHang] ResponseBody={responseBody}");

        var envelope = JsonSerializer.Deserialize<ApiEnvelope>(responseBody, JsonOptions);
        if (envelope is null)
        {
            return new UpsertDanhMucKhachHangResult
            {
                Success = false,
                Message = "Khong the doc ket qua cap nhat danh muc khach hang."
            };
        }

        var result = new UpsertDanhMucKhachHangResult
        {
            Success = envelope.Status == 0,
            Message = envelope.Status == 0
                ? (string.IsNullOrWhiteSpace(envelope.Data) ? "Cap nhat danh muc khach hang thanh cong." : envelope.Data)
                : (string.IsNullOrWhiteSpace(envelope.ErrorMsg) ? "Khong the cap nhat danh muc khach hang." : envelope.ErrorMsg)
        };

        Console.WriteLine($"[UpsertDanhMucKhachHang] Result={JsonSerializer.Serialize(result, JsonOptions)}");
        return result;
    }

    public async Task<PhiLuuKhoResponse> GetPhiLuuKhoAsync(string houseBill, string? ngayGiaHan)
        => await GetPhiLuuKhoAsync(houseBill, string.Empty, ngayGiaHan);

    public async Task<PhiLuuKhoResponse> GetPhiLuuKhoAsync(string houseBill, string soCont, string? ngayLayHang)
    {
        if (string.IsNullOrWhiteSpace(ngayLayHang))
        {
            return new PhiLuuKhoResponse
            {
                Success = false,
                Message = "Lỗi ngày lấy hàng không được để trống."
            };
        }

        object payload = string.IsNullOrWhiteSpace(soCont)
            ? new
            {
                SoVanDonHangNhapKhau = houseBill,
                NgayGiaHan = ngayLayHang
            }
            : new
            {
                SoVanDonHangNhapKhau = houseBill,
                SoCont = soCont,
                NgayGiaHan = ngayLayHang
            };

        var response = await _httpClient.PostAsJsonAsync(
            //BuildWorkflowUrl(PhiLuuKhoApiPath),
            PhiLuuKhoApiPath,
            payload,
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        if (envelope is null)
        {
            return new PhiLuuKhoResponse
            {
                Success = false,
                Message = "Khong the doc ket qua tinh phi luu kho."
            };
        }

        if (envelope.Status != 0)
        {
            return new PhiLuuKhoResponse
            {
                Success = false,
                Message = string.IsNullOrWhiteSpace(envelope.ErrorMsg)
                    ? "Khong the doc ket qua tinh phi luu kho."
                    : envelope.ErrorMsg
            };
        }

        if (string.IsNullOrWhiteSpace(envelope.Data))
        {
            return new PhiLuuKhoResponse
            {
                Success = false,
                Message = "Khong the doc ket qua tinh phi luu kho."
            };
        }

        if (!string.IsNullOrWhiteSpace(envelope.ErrorMsg))
        {
            return new PhiLuuKhoResponse
            {
                Success = false,
                Message = envelope.ErrorMsg
            };
        }

        List<PhiLuuKhoApiItem>? apiItems;
        try
        {
            apiItems = JsonSerializer.Deserialize<List<PhiLuuKhoApiItem>>(envelope.Data, JsonOptions);
        }
        catch (JsonException)
        {
            apiItems = null;
        }

        if (apiItems is null)
        {
            return new PhiLuuKhoResponse
            {
                Success = false,
                Message = "Khong the doc ket qua tinh phi luu kho."
            };
        }

        var chiTietHoaDon = apiItems.Select(MapPhiLuuKhoItem).ToList();
        var vat = apiItems.FirstOrDefault()?.ThueSuat ?? 0m;

        return new PhiLuuKhoResponse
        {
            Success = true,
            Data = new PhiLuuKhoData
            {
                ChiTietHoaDon = chiTietHoaDon,
                Vat = vat,
                DonViTienTe = "VND",
                TrangThaiThanhToan = 0
            },
            Message = string.Empty
        };
    }

    //public async Task<PhiLuuKhoQuaHanResponse> GetPhiLuuKhoQuaHanAsync(string houseBill, string soCont)
    //{
    //    var response = await _httpClient.PostAsJsonAsync(
    //        BuildWorkflowUrl(PhiLuuKhoQuaHanApiPath),
    //        new
    //        {
    //            HouseBill = houseBill,
    //            SoCont = soCont
    //        },
    //        JsonOptions);

    //    response.EnsureSuccessStatusCode();

    //    return await response.Content.ReadFromJsonAsync<PhiLuuKhoQuaHanResponse>(JsonOptions)
    //        ?? new PhiLuuKhoQuaHanResponse
    //        {
    //            Success = false,
    //            Message = "Khong the doc ket qua tinh phi luu kho qua han."
    //        };
    //}

    public async Task<ChiTietHouseBillResponse> GetChiTietHouseBillValidAsync(string houseBill, string soCont = "")
    {
        Console.WriteLine($"[ChiTietHouseBill] Request houseBill={houseBill}, soCont={soCont}");

        var response = await _httpClient.PostAsJsonAsync(
            // BuildWorkflowUrl(ChiTietHouseBillValidApiPath),
            ChiTietHouseBillValidApiPath,
            new { SoVanDon = houseBill },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"[ChiTietHouseBill] Response={(int)response.StatusCode} {response.ReasonPhrase}");
        Console.WriteLine($"[ChiTietHouseBill] ResponseBody={responseBody}");

        var envelope = JsonSerializer.Deserialize<ApiEnvelope>(responseBody, JsonOptions);
        if (envelope is null)
        {
            return new ChiTietHouseBillResponse
            {
                Success = false,
                Message = "Khong the doc thong tin truy van house bill."
            };
        }

        var result = new ChiTietHouseBillResponse
        {
            Success = envelope.Status == 0,
            Data = envelope.Data ?? string.Empty,
            Message = string.IsNullOrWhiteSpace(envelope.ErrorMsg)
                ? string.Empty
                : envelope.ErrorMsg
        };

        Console.WriteLine($"[ChiTietHouseBill] ParsedEnvelope={JsonSerializer.Serialize(result, JsonOptions)}");
        result.ParsedData = ParseChiTietHouseBillData(result.Data, houseBill, soCont);
        Console.WriteLine($"[ChiTietHouseBill] ParsedData={(result.ParsedData is null ? "null" : JsonSerializer.Serialize(result.ParsedData, JsonOptions))}");
        if (result.Success && result.ParsedData is null && !string.Equals(result.Data?.Trim(), "[]", StringComparison.Ordinal))
        {
            result.Success = false;
            result.Message = string.IsNullOrWhiteSpace(result.Message)
                ? "Không tìm thấy số HouseBill trong hệ thống! Vui lòng kiểm tra lại."
                : result.Message;
        }

        return result;
    }

    public async Task<ChiTietHouseBillResponse> GetChiTietHouseBillAsync(string houseBill, string soCont = "")
    {
        Console.WriteLine($"[ChiTietHouseBillNew] Request houseBill={houseBill}, soCont={soCont}");

        var response = await _httpClient.PostAsJsonAsync(
           //BuildWorkflowUrl(ChiTietHouseBillApiPath),
           ChiTietHouseBillApiPath,
            new { SoVanDon = houseBill },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"[ChiTietHouseBillNew] Response={(int)response.StatusCode} {response.ReasonPhrase}");
        Console.WriteLine($"[ChiTietHouseBillNew] ResponseBody={responseBody}");

        var envelope = JsonSerializer.Deserialize<ApiEnvelope>(responseBody, JsonOptions);
        if (envelope.Status == 1)
        {
            return new ChiTietHouseBillResponse
            {
                Success = false,
                Message = envelope.ErrorMsg
            };
        }
        if (envelope is null)
        {
            return new ChiTietHouseBillResponse
            {
                Success = false,
                Message = "Khong the doc thong tin truy van house bill."
            };
        }

        var result = new ChiTietHouseBillResponse
        {
            Success = envelope.Status == 0,
            Data = envelope.Data ?? string.Empty,
            Message = string.IsNullOrWhiteSpace(envelope.ErrorMsg)
                ? string.Empty
                : envelope.ErrorMsg
        };

        Console.WriteLine($"[ChiTietHouseBillNew] ParsedEnvelope={JsonSerializer.Serialize(result, JsonOptions)}");
        result.ParsedData = ParseChiTietHouseBillData(result.Data, houseBill, soCont);
        Console.WriteLine($"[ChiTietHouseBillNew] ParsedData={(result.ParsedData is null ? "null" : JsonSerializer.Serialize(result.ParsedData, JsonOptions))}");

        if (result.Success && result.ParsedData is null && !string.Equals(result.Data?.Trim(), "[]", StringComparison.Ordinal))
        {
            result.Success = false;
            result.Message = string.IsNullOrWhiteSpace(result.Message)
                ? "Không tìm thấy số HouseBill trong hệ thống! Vui lòng kiểm tra lại."
                : result.Message;
        }

        return result;
    }

    //public async Task<ThongTinThanhToanResponse> GetThongTinThanhToanAsync(string houseBill, string soCont)
    //{
    //    var response = await _httpClient.PostAsJsonAsync(
    //        BuildWorkflowUrl(ThongTinThanhToanApiPath),
    //        new
    //        {
    //            HouseBill = houseBill,
    //            SoCont = soCont
    //        },
    //        JsonOptions);

    //    response.EnsureSuccessStatusCode();

    //    return await response.Content.ReadFromJsonAsync<ThongTinThanhToanResponse>(JsonOptions)
    //        ?? new ThongTinThanhToanResponse
    //        {
    //            Success = false,
    //            Message = "Khong the doc thong tin thanh toan."
    //        };
    //}

    public async Task<LenhXuatKhoHangNhapKhauTempListResponse> GetLenhXuatKhoHangNhapKhauTempListAsync(
        long idDanhMucKhachHangDoiLenh,
        string soVanDon)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                //BuildWorkflowUrl("/api/ctLenhXuatKhoHangNhapKhauTemp/ListTemp"),
                "/api/ctLenhXuatKhoHangNhapKhauTemp/ListTemp",
                new
                {
                    IDDanhMucKhachHangDoLenh = idDanhMucKhachHangDoiLenh,
                    SoVanDon = NullIfEmpty(soVanDon)
                },
                JsonOptions);

            response.EnsureSuccessStatusCode();

            var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
            Console.WriteLine(envelope?.Data);
            EnsureSuccess(envelope, "Khong the tai danh sach lenh xuat kho tam.");

            return new LenhXuatKhoHangNhapKhauTempListResponse
            {
                Success = true,
                Data = ParseLenhXuatKhoHangNhapKhauTempItems(envelope?.Data)
            };
        }
        catch (Exception ex)
        {
            return new LenhXuatKhoHangNhapKhauTempListResponse
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<BienNhanThanhToanHangNhapKhauTempListResponse> GetBienNhanThanhToanHangNhapKhauTempListAsync(
        long idctLenhNhapKhoHangNhapKhauChiTiet,
        long idDanhMucKhachHangDoiLenh)
    {
        try
        {
            var requestPayload = new
            {
                IDctLenhNhapKhoHangNhapKhauChiTiet = idctLenhNhapKhoHangNhapKhauChiTiet,
                IDDanhMucKhachHangDoLenh = idDanhMucKhachHangDoiLenh
            };
            //var requestUrl = BuildWorkflowUrl("/api/ctBienNhanThanhToanHangNhapKhauTemp/ListTemp");
            var requestUrl = "/api/ctBienNhanThanhToanHangNhapKhauTemp/ListTemp";
            Console.WriteLine($"[BienNhanThanhToanHangNhapKhauTemp/ListTemp] POST {requestUrl}");
            Console.WriteLine($"[BienNhanThanhToanHangNhapKhauTemp/ListTemp] Request={JsonSerializer.Serialize(requestPayload, JsonOptions)}");

            var response = await _httpClient.PostAsJsonAsync(
                requestUrl,
                requestPayload,
                JsonOptions);

            response.EnsureSuccessStatusCode();

            var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
            EnsureSuccess(envelope, "Khong the tai danh sach bien nhan thanh toan tam.");

            return new BienNhanThanhToanHangNhapKhauTempListResponse
            {
                Success = true,
                Data = ParseBienNhanThanhToanHangNhapKhauTempItems(envelope?.Data)
            };
        }
        catch (Exception ex)
        {
            return new BienNhanThanhToanHangNhapKhauTempListResponse
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<BienNhanThanhToanHangNhapKhauTempListResponse> GetBienNhanThanhToanHangNhapKhauTempListAdminAsync(
        DateTime? tuNgay = null,
        DateTime? denNgay = null,
        int? trangThaiThanhToan = 0,
        string? soBienNhan = null)
    {
        try
        {
            var requestPayload = new
            {
                TuNgay = FormatApiDate(tuNgay),
                DenNgay = FormatApiDate(denNgay),
                TrangThaiThanhToan = trangThaiThanhToan,
                SoBienNhan = NullIfEmpty(soBienNhan)
            };

            //var requestUrl = BuildWorkflowUrl("/api/ctBienNhanThanhToanHangNhapKhauTemp/ListTempAdmin");
            var requestUrl = "/api/ctBienNhanThanhToanHangNhapKhauTemp/ListTempAdmin";
            Console.WriteLine($"[BienNhanThanhToanHangNhapKhauTemp/ListTempAdmin] POST {requestUrl}");
            Console.WriteLine($"[BienNhanThanhToanHangNhapKhauTemp/ListTempAdmin] Request={JsonSerializer.Serialize(requestPayload, JsonOptions)}");

            var response = await _httpClient.PostAsJsonAsync(
                requestUrl,
                requestPayload,
                JsonOptions);

            response.EnsureSuccessStatusCode();

            var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
            EnsureSuccess(envelope, "Khong the tai danh sach bien nhan thanh toan admin.");

            return new BienNhanThanhToanHangNhapKhauTempListResponse
            {
                Success = true,
                Data = ParseBienNhanThanhToanHangNhapKhauTempItems(envelope?.Data)
            };
        }
        catch (Exception ex)
        {
            return new BienNhanThanhToanHangNhapKhauTempListResponse
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task ConfirmBienNhanThanhToanHangNhapKhauTempAsync(long id, string soBienNhan, decimal tongTien)
    {
        if (id <= 0)
        {
            throw new InvalidOperationException("ID bien nhan khong hop le.");
        }

        if (string.IsNullOrWhiteSpace(soBienNhan))
        {
            throw new InvalidOperationException("So bien nhan khong duoc de trong.");
        }

        var tinNhanRaw = BuildTinNhanRawForPayment(soBienNhan, tongTien);
        var hashResult = EverlinkChecksumHelper.CreateHash(tinNhanRaw);
        var requestPayload = new
        {
            TinNhanRaw = tinNhanRaw
        };

        await CheckThanhToanAutoRawAsync(hashResult.t.ToString(CultureInfo.InvariantCulture), hashResult.hash, tinNhanRaw);
    }

    public async Task<HttpResponseMessage> CheckThanhToanAutoRawAsync(string t, string checksum, string tinNhanRaw)
    {
        var normalizedT = NullIfEmpty(t);
        var normalizedChecksum = NullIfEmpty(checksum);
        var normalizedTinNhanRaw = NullIfEmpty(tinNhanRaw);

        if (string.IsNullOrWhiteSpace(normalizedT))
        {
            throw new InvalidOperationException("Tham so t khong hop le.");
        }

        if (string.IsNullOrWhiteSpace(normalizedChecksum))
        {
            throw new InvalidOperationException("Tham so checksum khong hop le.");
        }

        if (string.IsNullOrWhiteSpace(normalizedTinNhanRaw))
        {
            throw new InvalidOperationException("TinNhanRaw khong duoc de trong.");
        }

        var requestPayload = new
        {
            TinNhanRaw = normalizedTinNhanRaw
        };

        //var requestUrl = $"{BuildWorkflowUrl("/api/ctBienNhanThanhToanHangNhapKhauTemp/CheckThanhToanAuto")}?t={Uri.EscapeDataString(normalizedT)}&checksum={Uri.EscapeDataString(normalizedChecksum)}";
        var requestUrl = $"/api/ctBienNhanThanhToanHangNhapKhauTemp/CheckThanhToanAuto?t={Uri.EscapeDataString(normalizedT)}&checksum={Uri.EscapeDataString(normalizedChecksum)}";
        Console.WriteLine($"[BienNhanThanhToanHangNhapKhauTemp/CheckThanhToanAuto] POST {requestUrl}");
        Console.WriteLine($"[BienNhanThanhToanHangNhapKhauTemp/CheckThanhToanAuto] Request={JsonSerializer.Serialize(requestPayload, JsonOptions)}");

        return await _httpClient.PostAsJsonAsync(requestUrl, requestPayload, JsonOptions);
    }

    public async Task<LenhXuatKhoHangNhapKhauTempInsertResponse> InsertLenhXuatKhoHangNhapKhauTempAsync(
        OnlineOrderRecord order,
        ChiTietHouseBillData chiTiet,
        PhiLuuKhoResponse phiLuuKho,
        DateTime ngayGiaHan,
        long idDanhMucKhachHangDoiLenh,
        string? ghiChu = null)
    {
        if (phiLuuKho.Data is null)
        {
            throw new InvalidOperationException("Khong co du lieu phi luu kho de tao lenh xuat kho.");
        }

        var ngayLap = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        var ngayGiaHanText = ngayGiaHan.Date.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        var request = new LenhXuatKhoHangNhapKhauTempInsertRequest
        {
            LenhXuatKho = new LenhXuatKhoHangNhapKhauTempInsertData
            {
                NgayLap = ngayLap,
                NgayGiaHan = ngayGiaHanText,
                SoVanDon = string.IsNullOrWhiteSpace(chiTiet.SoVanDon) ? order.HouseBill : chiTiet.SoVanDon,
                IDctLenhNhapKhoHangNhapKhauChiTiet = chiTiet.ID,
                SoLuongQuaKho = chiTiet.SoLuongQuaKho,
                SoLuongQuaTai = chiTiet.SoLuongQuaTai,
                MaSoThue = order.TaxCode,
                SoXe = order.VehicleNumber,
                HoTenNguoiNhanHang = order.CustomerName,
                SoCMND = order.IdentityNumber,
                SoDienThoaiNguoiNhanHang = order.PhoneNumber,
                SoLuongKienXuat = ToInt32Safely(chiTiet.SoLuongKienNhap),
                KhoiLuongXuat = chiTiet.KhoiLuongNhap,
                CBMXuat = chiTiet.CBMNhap,
                IDDanhMucCuaLamHang = chiTiet.IDDanhMucCuaLamHang,
                SoToKhai = order.DeclarationNumber,
                GhiChu = NullIfEmpty(ghiChu) ?? string.Empty,
                IDDanhMucKhachHangDoiLenh = idDanhMucKhachHangDoiLenh
            },
            DanhSachPhi = phiLuuKho.Data.ChiTietHoaDon
                .Select(MapPhiLuuKhoApiItem)
                .ToList()
        };

        Console.WriteLine($"[LenhXuatKhoHangNhapKhauTempInsertData] {JsonSerializer.Serialize(request.LenhXuatKho, JsonOptions)}");
        Console.WriteLine($"[LenhXuatKhoHangNhapKhauTempInsertData request] {JsonSerializer.Serialize(request, JsonOptions)}");

        var response = await _httpClient.PostAsJsonAsync(
            //BuildWorkflowUrl("/api/ctLenhXuatKhoHangNhapKhauTemp/Insert"),
            "/api/ctLenhXuatKhoHangNhapKhauTemp/Insert",
            request,
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Khong the tao lenh xuat kho hang nhap khau tam.");

        if (string.IsNullOrWhiteSpace(envelope!.Data))
        {
            throw new InvalidOperationException("Khong the doc ket qua tao lenh xuat kho.");
        }

        var result = JsonSerializer.Deserialize<LenhXuatKhoHangNhapKhauTempInsertResponse>(envelope.Data, JsonOptions);
        if (result is null)
        {
            throw new InvalidOperationException("Khong the doc ket qua tao lenh xuat kho.");
        }

        return result;
    }

    public async Task<OnlineOrderWorkflowResult> RunOrderWorkflowAsync(
        long idLenhOnline,
        long idDanhMucKhachHangDoiLenh,
        string houseBill,
        string soCont,
        long idCtLenhNhapKhoHangNhapKhauChiTiet,
        DateTime? pickupDate = null,
        string? pickupDateStr = "",
        string? traceTag = null)
    {
        var tracePrefix = string.IsNullOrWhiteSpace(traceTag) ? string.Empty : $"[{traceTag}] ";
        Console.WriteLine($"{tracePrefix}RunOrderWorkflowAsync start. HouseBill={houseBill}, SoCont={soCont}, PickupDate={pickupDate:yyyy-MM-dd}, IDctLenhNhapKhoHangNhapKhauChiTiet={idCtLenhNhapKhoHangNhapKhauChiTiet}");

        //Bo khong check thong quan trong chi tiet
        //var thongQuan = await CheckThongQuanAsync(houseBill, soCont);
        //Console.WriteLine($"{tracePrefix}ThongQuan completed. Success={thongQuan.Success}, IsThongQuan={thongQuan.IsThongQuan}");

        var result = new OnlineOrderWorkflowResult
        {

        };

        //if (!thongQuan.Success || !thongQuan.IsThongQuan)
        //{
        //    return result;
        //}

        //Bo ko lay phi luu kho tu ben  ngoai
        //Console.WriteLine($"{tracePrefix}Calling PhiLuuKho...");
        //Console.WriteLine($"{tracePrefix}Calling PhiLuuKho houseBill ..." + houseBill);
        //result.PhiLuuKho = await GetPhiLuuKhoAsync(houseBill, soCont, pickupDateStr);
        //Console.WriteLine($"{tracePrefix}PhiLuuKho.Response={JsonSerializer.Serialize(result.PhiLuuKho, JsonOptions)}");

        Console.WriteLine($"{tracePrefix}Calling ChiTietHouseBill...");
        result.ChiTietHouseBill = await GetChiTietHouseBillAsync(houseBill, soCont);
        Console.WriteLine($"{tracePrefix}ChiTietHouseBill.Response={JsonSerializer.Serialize(result.ChiTietHouseBill, JsonOptions)}");
        Console.WriteLine($"{tracePrefix}ChiTietHouseBill.IsHoanThanh={result.ChiTietHouseBill?.ParsedData?.IsHoanThanh}");

        //fix: Bo phi luu kho qua han
        //if (IsOverduePickupDate(pickupDate, result.ChiTietHouseBill?.ParsedData?.IsHoanThanh ?? false))
        //{
        //    Console.WriteLine($"{tracePrefix}Calling PhiLuuKhoQuaHan...");
        //    result.PhiLuuKhoQuaHan = await GetPhiLuuKhoQuaHanAsync(houseBill, soCont);
        //}

        var chiTietResult = result.ChiTietHouseBill;
        var chiTiet = chiTietResult?.ParsedData;
        if (chiTiet is not null)
        {

        }

        //Lấy danh sách lệnh xuất kho và biên nhận thanh toán
        Console.WriteLine($"{tracePrefix}Calling LenhXuatKhoHangNhapKhauTemp/ListTemp...");
        result.LenhXuatKhoHangNhapKhauTemps = await GetLenhXuatKhoHangNhapKhauTempListAsync(
            idDanhMucKhachHangDoiLenh,
            houseBill);
        Console.WriteLine($"{tracePrefix}LenhXuatKhoHangNhapKhauTemp/ListTemp.Response={JsonSerializer.Serialize(result.LenhXuatKhoHangNhapKhauTemps, JsonOptions)}");

        Console.WriteLine($"{tracePrefix}Calling BienNhanThanhToanHangNhapKhauTemp/ListTemp...");
        long id = (long?)chiTiet?.ID
          ?? (long?)idCtLenhNhapKhoHangNhapKhauChiTiet
          ?? 0L;
        result.BienNhanThanhToanHangNhapKhauTemps = await GetBienNhanThanhToanHangNhapKhauTempListAsync(
            //chiTiet.ID,
            id,
            idDanhMucKhachHangDoiLenh);
        Console.WriteLine($"{tracePrefix}BienNhanThanhToanHangNhapKhauTemp/ListTemp.Response={JsonSerializer.Serialize(result.BienNhanThanhToanHangNhapKhauTemps, JsonOptions)}");
        //End

        //Khong cap nhat lai chi tiet
        //Console.WriteLine($"{tracePrefix}Upserting LenhOnlineChiTiet...");
        //await UpsertLenhOnlineChiTietAsync(idLenhOnline, houseBill, soCont, invoiceDownloadUrl, result, traceTag);

        Console.WriteLine($"{tracePrefix}RunOrderWorkflowAsync end.");
        return result;
    }

    private async Task UpsertLenhOnlineChiTietAsync(
        long idLenhOnline,
        string houseBill,
        string soCont,
        string? invoiceDownloadUrl,
        OnlineOrderWorkflowResult workflow,
        string? traceTag = null)
    {
        var tracePrefix = string.IsNullOrWhiteSpace(traceTag) ? string.Empty : $"[{traceTag}] ";

        if (workflow.ThongQuan is null || !workflow.ThongQuan.Success || !workflow.ThongQuan.IsThongQuan)
        {
            return;
        }

        if (workflow.PhiLuuKho is null || workflow.ChiTietHouseBill is null)
        {
            return;
        }

        var chiTiet = workflow.ChiTietHouseBill.ParsedData;
        var phi = workflow.PhiLuuKho.Data;
        if (chiTiet is null || phi is null)
        {
            return;
        }

        var payload = new
        {
            IDLenhOnline = idLenhOnline,
            PhiLuuKho = GetPhiAmountByDescription(phi, "Phí lưu kho"),
            PhiGiaoNhan = GetPhiAmountByDescription(phi, "Phí giao nhận"),
            PhiBocXep = GetPhiAmountByDescription(phi, "Phí bốc xếp"),
            VAT = phi.Vat,
            TrangThaiThanhToan = phi.TrangThaiThanhToan,
            TrangThaiThongQuan = workflow.ThongQuan.IsThongQuan ? 1 : 0,
            ThuKho = NullIfEmpty(chiTiet.ThuKho),
            Forwarder = NullIfEmpty(chiTiet.Forwarder),
            TenTau = NullIfEmpty(chiTiet.TenTau),
            ChuHang = NullIfEmpty(chiTiet.ChuHang),
            SoKien = chiTiet.SoKien,
            SoChuyen = NullIfEmpty(chiTiet.SoChuyen),
            SoHouseBill = NullIfEmpty(string.IsNullOrWhiteSpace(chiTiet.SoHouseBill) ? houseBill : chiTiet.SoHouseBill),
            NgayTauCap = ParseWorkflowDate(chiTiet.NgayTauCap),
            TrongLuong = chiTiet.TrongLuong,
            SoCont = NullIfEmpty(string.IsNullOrWhiteSpace(chiTiet.SoCont) ? soCont : chiTiet.SoCont),
            SoKhoi = chiTiet.SoKhoi,
            LinkTaiHoaDon = NullIfEmpty(invoiceDownloadUrl ?? string.Empty),
            DuongDanFileHoaDon = NullIfEmpty(invoiceDownloadUrl ?? string.Empty),
            IsHoanThanh = chiTiet.IsHoanThanh
        };

        Console.WriteLine($"{tracePrefix}UpsertChiTiet.IsHoanThanh={chiTiet.IsHoanThanh}");
        Console.WriteLine($"{tracePrefix}UpsertChiTiet.Payload={JsonSerializer.Serialize(payload, JsonOptions)}");

        // Legacy
        var legacyClient = _factory.CreateClient("LegacyApi");

        var response = await legacyClient.PostAsJsonAsync("api/LenhOnlines/UpsertChiTiet", payload, JsonOptions);
        Console.WriteLine($"{tracePrefix}UpsertChiTiet.POST api/LenhOnlines/UpsertChiTiet => {(int)response.StatusCode} {response.ReasonPhrase}");
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Khong the luu chi tiet lenh online.");
    }

    private static decimal GetPhiAmountByDescription(PhiLuuKhoData data, string descriptionPart)
    {
        return data.ChiTietHoaDon
            .Where(item => ContainsText(item.MoTa, descriptionPart))
            .Sum(item => item.ThanhTien);
    }

    private static PhiLuuKhoItem MapPhiLuuKhoItem(PhiLuuKhoApiItem item)
    {
        return new PhiLuuKhoItem
        {
            IDDanhMucCuoc = item.IDDanhMucCuoc,
            MaDanhMucCuoc = item.MaDanhMucCuoc,
            TenDanhMucCuoc = item.TenDanhMucCuoc,
            MoTa = item.DienGiai,
            DonViTinh = item.DonViTinh,
            SoLuong = item.SoLuong,
            NgayLuuKho = item.NgayLuuKho,
            DonGia = item.DonGia,
            DonGiaCuoc = item.DonGiaCuoc,
            DonGiaTraDaiLyTheoHopDong = item.DonGiaTraDaiLyTheoHopDong,
            DonGiaTraDaiLyThuThem = item.DonGiaTraDaiLyThuThem,
            IDDanhMucThueSuat = item.IDDanhMucThueSuat,
            TienHang = item.TienHang,
            ThueSuat = item.ThueSuat,
            TienThue = item.TienThue,
            ThanhTien = item.ThanhTien > 0m ? item.ThanhTien : item.TienHang + item.TienThue,
            MaDanhMucTaiKhoanKeToanDoanhThu = item.MaDanhMucTaiKhoanKeToanDoanhThu,
            MaDanhMucTaiKhoanKeToanThanhToan = item.MaDanhMucTaiKhoanKeToanThanhToan,
            MaDanhMucTaiKhoanKeToanThue = item.MaDanhMucTaiKhoanKeToanThue
        };
    }

    private static PhiLuuKhoApiItem MapPhiLuuKhoApiItem(PhiLuuKhoItem item)
    {
        return new PhiLuuKhoApiItem
        {
            IDDanhMucCuoc = item.IDDanhMucCuoc,
            MaDanhMucCuoc = item.MaDanhMucCuoc,
            TenDanhMucCuoc = item.TenDanhMucCuoc,
            DienGiai = item.MoTa,
            DonViTinh = item.DonViTinh,
            SoLuong = item.SoLuong,
            NgayLuuKho = item.NgayLuuKho,
            DonGia = item.DonGia,
            DonGiaCuoc = item.DonGiaCuoc,
            DonGiaTraDaiLyTheoHopDong = item.DonGiaTraDaiLyTheoHopDong,
            DonGiaTraDaiLyThuThem = item.DonGiaTraDaiLyThuThem,
            TienHang = item.TienHangThucTe,
            IDDanhMucThueSuat = item.IDDanhMucThueSuat,
            ThueSuat = item.ThueSuat,
            TienThue = item.TienThue,
            ThanhTien = item.TongTien,
            TongTien = item.TongTien,
            MaDanhMucTaiKhoanKeToanDoanhThu = item.MaDanhMucTaiKhoanKeToanDoanhThu,
            MaDanhMucTaiKhoanKeToanThanhToan = item.MaDanhMucTaiKhoanKeToanThanhToan,
            MaDanhMucTaiKhoanKeToanThue = item.MaDanhMucTaiKhoanKeToanThue
        };
    }

    private static bool ContainsText(string source, string value)
        => !string.IsNullOrWhiteSpace(source)
           && source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;

    private static int ToInt32Safely(decimal value)
    {
        if (value >= int.MaxValue)
        {
            return int.MaxValue;
        }

        if (value <= int.MinValue)
        {
            return int.MinValue;
        }

        return decimal.ToInt32(decimal.Round(value, 0, MidpointRounding.AwayFromZero));
    }

    private static DateTime? ParseWorkflowDate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var culture = CultureInfo.GetCultureInfo("vi-VN");
        return DateTime.TryParse(value, culture, DateTimeStyles.None, out var parsed)
            ? parsed
            : null;
    }

    private static bool IsOverduePickupDate(DateTime? pickupDate, bool isHoanThanh)
        => isHoanThanh && pickupDate.HasValue && pickupDate.Value.Date < DateTime.Today;

    private static ChiTietHouseBillData? ParseChiTietHouseBillData(string rawData, string houseBill, string soCont)
    {
        if (string.IsNullOrWhiteSpace(rawData))
        {
            return null;
        }

        var trimmed = rawData.Trim();
        try
        {
            if (trimmed.StartsWith("["))
            {
                using var document = JsonDocument.Parse(trimmed);
                var root = document.RootElement;
                if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                {
                    var data = MapChiTietHouseBillData(root[0], houseBill, soCont);
                    if (data is not null)
                    {
                        return data;
                    }
                }

                var items = JsonSerializer.Deserialize<List<ChiTietHouseBillData>>(trimmed, JsonOptions);
                return NormalizeChiTietHouseBillData(items?.FirstOrDefault(), houseBill, soCont);
            }

            if (trimmed.StartsWith("{"))
            {
                using var document = JsonDocument.Parse(trimmed);
                var data = MapChiTietHouseBillData(document.RootElement, houseBill, soCont);
                if (data is not null)
                {
                    return data;
                }

                var fallback = JsonSerializer.Deserialize<ChiTietHouseBillData>(trimmed, JsonOptions);
                return NormalizeChiTietHouseBillData(fallback, houseBill, soCont);
            }

            if (trimmed.StartsWith("\"") && trimmed.EndsWith("\""))
            {
                var unwrapped = JsonSerializer.Deserialize<string>(trimmed, JsonOptions);
                if (!string.IsNullOrWhiteSpace(unwrapped))
                {
                    return ParseChiTietHouseBillData(unwrapped, houseBill, soCont);
                }
            }
        }
        catch
        {
            return null;
        }

        return null;
    }

    private static ChiTietHouseBillData? MapChiTietHouseBillData(JsonElement element, string houseBill, string soCont)
    {
        if (element.ValueKind != JsonValueKind.Object)
        {
            return null;
        }

        var data = new ChiTietHouseBillData
        {
            ID = GetInt(element, "ID"),
            SoVanDon = GetString(element, "SoVanDon"),
            TrangThaiKhoa = GetNullableBool(element, "TrangThaiKhoa"),
            DaXacNhanHoanThanhXuatKho = GetInt(element, "DaXacNhanHoanThanhXuatKho"),
            SoContainer = GetString(element, "SoContainer"),
            MoTaHangHoa = GetString(element, "MoTaHangHoa"),
            TenChuHang = GetString(element, "TenChuHang"),
            MaDanhMucDonViTinh = GetString(element, "MaDanhMucDonViTinh"),
            SoLuongKienNhap = GetDecimal(element, "SoLuongKienNhap"),
            KhoiLuongNhap = GetDecimal(element, "KhoiLuongNhap"),
            CBMNhap = GetDecimal(element, "CBMNhap"),
            DonGiaLuuKho = GetDecimal(element, "DonGiaLuuKho"),
            DonGiaGiaoNhan = GetDecimal(element, "DonGiaGiaoNhan"),
            DonGiaBocXep = GetDecimal(element, "DonGiaBocXep"),
            DonGiaQuanLy = GetDecimal(element, "DonGiaQuanLy"),
            HangNguyHiem = GetBool(element, "HangNguyHiem"),
            HangQuaKho = GetBool(element, "HangQuaKho"),
            SoLuongQuaKho = GetDecimal(element, "SoLuongQuaKho"),
            HangQuaTai = GetBool(element, "HangQuaTai"),
            SoLuongQuaTai = GetDecimal(element, "SoLuongQuaTai"),
            EDO = GetString(element, "EDO"),
            MaDanhMucCuaLamHang = GetString(element, "MaDanhMucCuaLamHang"),
            IDDanhMucCuaLamHang = GetLong(element, "IDDanhMucCuaLamHang"),
            NgayNhapKho = GetDateTime(element, "NgayNhapKho"),
            NgayGiaHanCuoi = GetDateTime(element, "NgayGiaHanCuoi"),
            MaDanhMucDaiLy = GetString(element, "MaDanhMucDaiLy"),
            IDctKeHoachKhaiThacHangNhapKhau = GetLong(element, "IDctKeHoachKhaiThacHangNhapKhau"),
            SoSeal = GetString(element, "SoSeal"),
            MaDanhMucLoaiContainer = GetString(element, "MaDanhMucLoaiContainer"),
            MasterBill = GetString(element, "MasterBill"),
            SoToKhai = GetString(element, "SoToKhai"),
            MaDanhMucHangTau = GetString(element, "MaDanhMucHangTau"),
            TenTau = GetString(element, "TenTau"),
            SoChuyen = GetString(element, "SoChuyen"),
            NgayTauDen = GetDateTime(element, "NgayTauDen"),
            NgayTauDenEIM = GetString(element, "NgayTauDenEIM"),
            SoDinhDanhHangHoa = GetString(element, "SoDinhDanhHangHoa"),
            SoHieuPhuongTienVanTai = GetString(element, "SoHieuPhuongTienVanTai"),
            GhiChu = GetString(element, "GhiChu")
        };

        return NormalizeChiTietHouseBillData(data, houseBill, soCont);
    }

    private static ChiTietHouseBillData? NormalizeChiTietHouseBillData(ChiTietHouseBillData? data, string houseBill, string soCont)
    {
        if (data is null)
        {
            return null;
        }

        data.ThuKho = data.MaDanhMucCuaLamHang;
        data.ChuHang = data.TenChuHang;
        data.SoHouseBill = string.IsNullOrWhiteSpace(data.SoVanDon) ? houseBill : data.SoVanDon;
        data.SoCont = string.IsNullOrWhiteSpace(data.SoContainer) ? soCont : data.SoContainer;
        data.Forwarder = data.MaDanhMucDaiLy;
        data.SoKien = (int)data.SoLuongKienNhap;
        data.TrongLuong = data.KhoiLuongNhap;
        data.SoKhoi = data.CBMNhap;
        data.NgayTauCap = (data.NgayTauDen ?? data.NgayNhapKho)?.ToString("O") ?? string.Empty;
        data.NgayTauDenEIM = string.IsNullOrWhiteSpace(data.NgayTauDenEIM)
            ? (data.NgayTauDen?.ToString("M/d/yyyy 12:00:00 AM", CultureInfo.InvariantCulture)
               ?? data.NgayNhapKho?.ToString("M/d/yyyy 12:00:00 AM", CultureInfo.InvariantCulture)
               ?? string.Empty)
            : data.NgayTauDenEIM;
        data.SoDinhDanhHangHoa = string.IsNullOrWhiteSpace(data.SoDinhDanhHangHoa) ? data.SoVanDon : data.SoDinhDanhHangHoa;
        data.SoHieuPhuongTienVanTai = string.IsNullOrWhiteSpace(data.SoHieuPhuongTienVanTai)
            ? data.TenTau
            : data.SoHieuPhuongTienVanTai;
        data.SoToKhai = string.IsNullOrWhiteSpace(data.SoToKhai) ? data.MasterBill : data.SoToKhai;
        data.IsHoanThanh = data.DaXacNhanHoanThanhXuatKho == 1; //|| !data.IsKhoa;
        return data;
    }

    public async Task SeedDefaultsAsync(long idDanhMucKhachHangDoiLenh)
    {
        foreach (var draft in CreateSeedData())
        {
            await AddOrderAsync(idDanhMucKhachHangDoiLenh, draft);
        }
    }

    public async Task<long> AddOrderAsync(long idDanhMucKhachHangDoiLenh, OnlineOrderDraft draft)
    {
        var payload = new
        {
            HoVaTen = draft.CustomerName.Trim(),
            SoDienThoai = draft.PhoneNumber.Trim(),
            SoCMND = NullIfEmpty(draft.IdentityNumber),
            SoXe = NullIfEmpty(draft.VehicleNumber),
            MaSoThue = NullIfEmpty(draft.TaxCode),
            TenCongTy = NullIfEmpty(draft.CompanyName),
            DiaChi = NullIfEmpty(draft.CompanyAddress),
            Email = NullIfEmpty(draft.CompanyEmail),
            HouseBill = NullIfEmpty(draft.HouseBill),
            SoCont = NullIfEmpty(draft.ContainerNumber),
            NgayLayHang = draft.PickupDate,
            SoToKhai = NullIfEmpty(draft.DeclarationNumber),
            IDctLenhNhapKhoHangNhapKhauChiTiet = draft.IDctLenhNhapKhoHangNhapKhauChiTiet,
            TrangThai = 0,
            IDDanhMucKhachHangDoiLenh = idDanhMucKhachHangDoiLenh
        };

        // Legacy
        var legacyClient = _factory.CreateClient("LegacyApi");

        var response = await legacyClient.PostAsJsonAsync("api/LenhOnlines/Insert", payload, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Khong the luu lenh online.");

        if (string.IsNullOrWhiteSpace(envelope!.Data))
        {
            throw new InvalidOperationException("Khong the doc ID lenh online moi tao.");
        }

        using var document = JsonDocument.Parse(envelope.Data);
        var root = document.RootElement;
        var newOrderId = 0L;
        if (root.ValueKind == JsonValueKind.Object)
        {
            newOrderId = GetLong(root, "ID");
        }
        else if (root.ValueKind == JsonValueKind.String && long.TryParse(root.GetString(), out var parsedId))
        {
            newOrderId = parsedId;
        }

        if (newOrderId <= 0)
        {
            throw new InvalidOperationException("Khong the doc ID lenh online moi tao.");
        }

        return newOrderId;
    }

    public Task<(bool Success, string Message, string CompanyName, string CompanyAddress, string CompanyEmail)> LookupCompanyAsync(
        string taxCode,
        string currentEmail)
    {
        var normalized = string.IsNullOrWhiteSpace(taxCode) ? string.Empty : taxCode.Trim();
        if (string.IsNullOrWhiteSpace(normalized))
        {
            return Task.FromResult((false, "Vui long nhap ma so thue truoc khi lay thong tin.", string.Empty, string.Empty, string.Empty));
        }

        //var samples = new Dictionary<string, (string Name, string Address, string Email)>(StringComparer.OrdinalIgnoreCase)
        //{
        //    ["0201930936"] = ("CONG TY CO PHAN DAU TU CONG NGHE CENTECH", "Hai An, Hai Phong", "info@centech.vn"),
        //    ["0301464823"] = ("CONG TY TNHH THUONG MAI EVERLINK", "Quan 1, TP. Ho Chi Minh", "admin@everlink.com.vn")
        //};

        //if (samples.TryGetValue(normalized, out var match))
        //{
        //    return Task.FromResult((true, "Da lay thong tin cong ty theo ma so thue.", match.Name, match.Address, match.Email));
        //}

        return Task.FromResult((
            true,
            "Chua co du lieu dong bo tu dong. He thong da dien mau tham khao, ban co the chinh lai truoc khi luu.",
            $"Cong ty theo MST {normalized}",
            "Dia chi cong ty can duoc cap nhat",
            string.IsNullOrWhiteSpace(currentEmail) ? "contact@company.vn" : currentEmail.Trim()));
    }

    public Task<List<ImportCheckRecord>> SearchImportChecksAsync(string? houseBill, string? containerNumber)
    {
        var normalizedHouseBill = string.IsNullOrWhiteSpace(houseBill) ? string.Empty : houseBill.Trim();
        var normalizedContainer = string.IsNullOrWhiteSpace(containerNumber) ? string.Empty : containerNumber.Trim();

        var query = _importChecks.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(normalizedHouseBill))
        {
            query = query.Where(item => item.HouseBill.Contains(normalizedHouseBill, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(normalizedContainer))
        {
            query = query.Where(item => item.ContainerNumber.Contains(normalizedContainer, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(query.OrderByDescending(item => item.ReceivedDate).ToList());
    }

    private static void EnsureSuccess(ApiEnvelope? envelope, string fallbackMessage)
    {
        if (envelope is null)
        {
            throw new InvalidOperationException(fallbackMessage);
        }

        if (envelope.Status != 0)
        {
            throw new InvalidOperationException(string.IsNullOrWhiteSpace(envelope.ErrorMsg) ? fallbackMessage : envelope.ErrorMsg);
        }
    }

    private static string GetString(JsonElement element, string propertyName)
        => element.TryGetProperty(propertyName, out var value)
            ? value.ToString() ?? string.Empty
            : string.Empty;

    private static long GetLong(JsonElement element, string propertyName)
        => long.TryParse(GetString(element, propertyName), out var parsed) ? parsed : 0;

    private static int GetInt(JsonElement element, string propertyName)
        => int.TryParse(GetString(element, propertyName), out var parsed) ? parsed : 0;

    private static decimal GetDecimal(JsonElement element, string propertyName)
        => decimal.TryParse(
            GetString(element, propertyName),
            NumberStyles.Any,
            CultureInfo.InvariantCulture,
            out var parsed)
            ? parsed
            : 0m;

    private static bool GetBool(JsonElement element, string propertyName)
    {
        if (!TryGetPropertyIgnoreCase(element, propertyName, out var property))
        {
            return false;
        }

        return property.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Number => property.TryGetInt32(out var number) && number != 0,
            JsonValueKind.String when bool.TryParse(property.GetString(), out var parsedBool) => parsedBool,
            JsonValueKind.String when int.TryParse(property.GetString(), out var parsedInt) => parsedInt != 0,
            _ => false
        };
    }

    private static bool? GetNullableBool(JsonElement element, string propertyName)
    {
        if (!TryGetPropertyIgnoreCase(element, propertyName, out var property) || property.ValueKind == JsonValueKind.Null || property.ValueKind == JsonValueKind.Undefined)
        {
            return null;
        }

        return property.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Number => property.TryGetInt32(out var number) ? number != 0 : null,
            JsonValueKind.String when bool.TryParse(property.GetString(), out var parsedBool) => parsedBool,
            JsonValueKind.String when int.TryParse(property.GetString(), out var parsedInt) => parsedInt != 0,
            _ => null
        };
    }

    private static bool TryGetPropertyIgnoreCase(JsonElement element, string propertyName, out JsonElement property)
    {
        foreach (var candidate in element.EnumerateObject())
        {
            if (string.Equals(candidate.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            {
                property = candidate.Value;
                return true;
            }
        }

        property = default;
        return false;
    }

    private static DateTime? GetDateTime(JsonElement element, string propertyName)
    {
        var value = GetString(element, propertyName);
        return DateTime.TryParse(value, out var parsed) ? parsed : null;
    }

    private static string? NullIfEmpty(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    public sealed class CompanyTaxLookupResult
    {
        public string MaSoThue { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string NguoiDaiDien { get; set; } = string.Empty;
        public string ChucVuNguoiDaiDien { get; set; } = string.Empty;
        public string CreateDate { get; set; } = string.Empty;
        public bool NgungSuDung { get; set; } = false;
        public string EditDate { get; set; } = string.Empty;

    }

    public sealed class UpsertDanhMucKhachHangResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    private sealed class UpsertDanhMucKhachHangRequest
    {
        [JsonPropertyName("Ma")]
        public string Ma { get; set; } = string.Empty;

        [JsonPropertyName("Ten")]
        public string Ten { get; set; } = string.Empty;

        [JsonPropertyName("DiaChi")]
        public string DiaChi { get; set; } = string.Empty;

        [JsonPropertyName("MaSoThue")]
        public string MaSoThue { get; set; } = string.Empty;

        [JsonPropertyName("NguoiDaiDien")]
        public string NguoiDaiDien { get; set; } = string.Empty;

        [JsonPropertyName("ChucVuNguoiDaiDien")]
        public string ChucVuNguoiDaiDien { get; set; } = string.Empty;

        [JsonPropertyName("Email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("SoDienThoai")]
        public string SoDienThoai { get; set; } = string.Empty;

        [JsonPropertyName("GhiChu")]
        public string GhiChu { get; set; } = string.Empty;

        [JsonPropertyName("isUpdate")]
        public bool IsUpdate { get; set; }
    }

    //private string BuildWorkflowUrl(string relativePath)
    //{
    //    // Nếu relativePath đã là URL tuyệt đối (bắt đầu bằng http:// hoặc https://)
    //    if (relativePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
    //        relativePath.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
    //    {
    //        return relativePath;
    //    }
    //    if (string.IsNullOrWhiteSpace(_workflowOptions.BaseUrl))
    //    {
    //        throw new InvalidOperationException("Missing configuration: OnlineOrderWorkflow:BaseUrl");
    //    }

    //    return $"{_workflowOptions.BaseUrl.TrimEnd('/')}/{relativePath.TrimStart('/')}";
    //}

    private static OnlineOrderRecord MapOnlineOrder(JsonElement item)
    {
        var id = GetLong(item, "ID");
        var soThuTuLenh = GetLong(item, "SoThuTuLenh");
        var trangThai = GetInt(item, "TrangThai");
        var hoanThanh = GetInt(item, "IsHoanThanh");

        return new OnlineOrderRecord
        {
            Id = id.ToString(),
            CreatorUserId = GetLong(item, "IDDanhMucKhachHangDoiLenh"),
            UserId = GetLong(item, "IDDanhMucKhachHangDoiLenh"),
            CreatorEmail = GetString(item, "EmailNguoiTao"),
            CreatorName = GetString(item, "TenNguoiTao"),
            UserEmail = GetString(item, "EmailNguoiTao"),
            SequenceNumber = soThuTuLenh,
            OrderCode = GetOrderCode(soThuTuLenh, id),
            OrderDate = GetDateTime(item, "NgayLamLenh") ?? DateTime.Today,
            CustomerName = GetString(item, "HoVaTen"),
            PhoneNumber = GetString(item, "SoDienThoai"),
            IdentityNumber = GetString(item, "SoCMND"),
            VehicleNumber = GetString(item, "SoXe"),
            TaxCode = GetString(item, "MaSoThue"),
            CompanyName = GetString(item, "TenCongTy"),
            CompanyAddress = GetString(item, "DiaChi"),
            CompanyEmail = GetString(item, "Email"),
            HouseBill = GetString(item, "HouseBill"),
            ContainerNumber = GetString(item, "SoCont"),
            PickupDate = GetDateTime(item, "NgayLayHang"),
            DeclarationNumber = GetString(item, "SoToKhai"),
            IDctLenhNhapKhoHangNhapKhauChiTiet = GetLong(item, "IDctLenhNhapKhoHangNhapKhauChiTiet"),
            SoBienNhanDaThanhToan = GetInt(item, "SoBienNhanDaThanhToan"),
            SoBienNhanChuaThanhToan = GetInt(item, "SoBienNhanChuaThanhToan"),
            BienNhanThanhToanGoc = GetInt(item, "BienNhanThanhToanGoc"),
            StatusCode = trangThai,
            Status = GetTrangThaiText(trangThai),
            IsHoanThanh = hoanThanh == 1
        };
    }

    private static string GetOrderCode(long soThuTuLenh, long fallbackId)
    {
        var value = soThuTuLenh > 0 ? soThuTuLenh : fallbackId;
        //return $"LO-{value:000000000}";
        return $"LO-{value:0}";
    }

    // Cau hinh map trang thai LenhOnlines tu database:
    // 0 - Cho cap nhat
    // 1 - Chua thong quan
    // 2 - Chua doi lenh
    // 3 - Da doi lenh
    // 4 - Gia han
    // 5 - Da thong quan
    private static string GetTrangThaiText(int trangThai)
      => trangThai switch
      {
          0 => "Mới khởi tạo",
          1 => "Chưa thông quan",
          2 => "Chưa đổi lệnh",
          3 => "Đã đổi lệnh",
          4 => "Gia hạn",
          5 => "Đã thông quan",
          _ => "Không xác định"
      };

    private static List<LenhXuatKhoHangNhapKhauTempListItem> ParseLenhXuatKhoHangNhapKhauTempItems(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        var itemsElement = root.ValueKind == JsonValueKind.Array
            ? root
            : root.TryGetProperty("Items", out var itemsValue)
                ? itemsValue
                : root;

        var items = new List<LenhXuatKhoHangNhapKhauTempListItem>();
        if (itemsElement.ValueKind != JsonValueKind.Array)
        {
            return items;
        }

        foreach (var item in itemsElement.EnumerateArray())
        {
            var model = JsonSerializer.Deserialize<LenhXuatKhoHangNhapKhauTempListItem>(item.GetRawText(), JsonOptions);
            if (model is null)
            {
                continue;
            }

            model.DownloadUrl = GetTempDocumentDownloadUrl(
                "/api/ctLenhXuatKhoHangNhapKhauTemp/TaiPDFLenhXuat",
                "SoLenhXuat",
                model.So);
            items.Add(model);
        }

        return items;
    }

    private static List<BienNhanThanhToanHangNhapKhauTempListItem> ParseBienNhanThanhToanHangNhapKhauTempItems(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        var itemsElement = root.ValueKind == JsonValueKind.Array
            ? root
            : root.TryGetProperty("Items", out var itemsValue)
                ? itemsValue
                : root;

        var items = new List<BienNhanThanhToanHangNhapKhauTempListItem>();
        if (itemsElement.ValueKind != JsonValueKind.Array)
        {
            return items;
        }

        foreach (var item in itemsElement.EnumerateArray())
        {
            var model = JsonSerializer.Deserialize<BienNhanThanhToanHangNhapKhauTempListItem>(item.GetRawText(), JsonOptions);
            if (model is null)
            {
                continue;
            }

            model.DownloadUrl = GetTempDocumentDownloadUrl(
                "/api/ctBienNhanThanhToanHangNhapKhauTemp/TaiPDFBienNhan",
                "SoBienNhan",
                model.So);
            items.Add(model);
        }

        return items;
    }

    public Task<PdfDownloadResult> DownloadLenhXuatKhoHangNhapKhauTempPdfAsync(string soLenhXuat)
        => DownloadTempPdfAsync(
            "/api/ctLenhXuatKhoHangNhapKhauTemp/TaiPDFLenhXuat",
            "SoLenhXuat",
            soLenhXuat,
            "LenhXuatKho");

    public Task<PdfDownloadResult> DownloadBienNhanThanhToanHangNhapKhauTempPdfAsync(string soBienNhan)
        => DownloadTempPdfAsync(
            "/api/ctBienNhanThanhToanHangNhapKhauTemp/TaiPDFBienNhan",
            "SoBienNhan",
            soBienNhan,
            "BienNhanThanhToan");

    public async Task<PdfDownloadResult> DownloadHoaDonDienTuHangNhapKhauTempPdfAsync(string fKey)
    {
        try
        {
            return await DownloadTempPdfAsync(
                "/api/ctLenhXuatKhoHangNhapKhauTemp/TaiPDFHDDTAsync",
                "FKey",
                fKey,
                "HoaDonDienTu");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DownloadHoaDonDienTu] {ex}");
            throw new InvalidOperationException("Không thể tải hoá đơn điện tử từ dịch vụ.", ex);
        }
    }

    private async Task<PdfDownloadResult> DownloadTempPdfAsync(string relativePath, string queryName, string so, string filePrefix)
    {
        if (string.IsNullOrWhiteSpace(so))
        {
            throw new InvalidOperationException("So khong duoc de trong.");
        }

        //var url = $"{BuildWorkflowUrl(relativePath)}?{queryName}={Uri.EscapeDataString(so.Trim())}";
        var url = $"{(relativePath)}?{queryName}={Uri.EscapeDataString(so.Trim())}";
        Console.WriteLine($"[DownloadTempPdf] GET {url}");
        using var response = await _httpClient.GetAsync(url);
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"[DownloadTempPdf] Response={(int)response.StatusCode} {response.ReasonPhrase}");

        if (!response.IsSuccessStatusCode)
        {
            var detail = string.IsNullOrWhiteSpace(responseBody)
                ? string.Empty
                : $" Body={responseBody}";

            throw new HttpRequestException(
                $"Không thể tải {filePrefix} từ endpoint PDF. HTTP {(int)response.StatusCode} ({response.ReasonPhrase}).{detail}");
        }

        var content = await response.Content.ReadAsByteArrayAsync();
        var fileName = ExtractFileName(response) ?? $"{filePrefix}-{so.Trim()}.pdf";
        var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/pdf";

        return new PdfDownloadResult
        {
            Content = content,
            FileName = fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) ? fileName : $"{fileName}.pdf",
            ContentType = contentType
        };
    }

    private static string? ExtractFileName(HttpResponseMessage response)
    {
        var disposition = response.Content.Headers.ContentDisposition;
        if (disposition is null)
        {
            return null;
        }

        var fileName = disposition.FileNameStar ?? disposition.FileName;
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return null;
        }

        return fileName.Trim().Trim('"');
    }

    private static string GetTempDocumentDownloadUrl(string relativePath, string queryName, string so)
    {
        if (!string.IsNullOrWhiteSpace(so))
        {
            return $"{relativePath}?{queryName}={Uri.EscapeDataString(so)}";
        }

        return string.Empty;
    }

    private static string? FormatApiDate(DateTime? date)
        => date.HasValue
            ? date.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            : null;

    private static string BuildTinNhanRawForPayment(string soBienNhan, decimal tongTien)
    {
        var normalizedReceiptNo = soBienNhan.Trim();
        var amountText = Math.Round(tongTien, 0, MidpointRounding.AwayFromZero).ToString("N0", new CultureInfo("vi-VN"));
        return $"+{amountText}VND Ref:{normalizedReceiptNo}#AdminPortalVerify";
    }

    private static List<OnlineOrderDraft> CreateSeedData()
    {
        return
        [
            new OnlineOrderDraft
            {
                OrderDate = DateTime.Today,
                CustomerName = "Nguyen Van A",
                PhoneNumber = "0909123456",
                IdentityNumber = "001203456789",
                VehicleNumber = "51D-123.45",
                TaxCode = "0201930936",
                CompanyName = "CONG TY CO PHAN DAU TU CONG NGHE CENTECH",
                CompanyAddress = "Hai An, Hai Phong",
                CompanyEmail = "info@centech.vn",
                HouseBill = "HB-000009",
                ContainerNumber = "TCLU1234567",
                PickupDate = DateTime.Today.AddDays(1),
                DeclarationNumber = "TK-99887766"
            },
            new OnlineOrderDraft
            {
                OrderDate = DateTime.Today.AddDays(-3),
                CustomerName = "Tran Thi B",
                PhoneNumber = "0911222333",
                IdentityNumber = "079203456789",
                VehicleNumber = "61C-888.99",
                TaxCode = "0301464823",
                CompanyName = "CONG TY TNHH THUONG MAI EVERLINK",
                CompanyAddress = "Quan 1, TP. Ho Chi Minh",
                CompanyEmail = "admin@everlink.com.vn",
                HouseBill = "HB-000008",
                ContainerNumber = "OOLU7654321",
                PickupDate = DateTime.Today,
                DeclarationNumber = "TK-11223344"
            }
        ];
    }

    private static List<ImportCheckRecord> CreateImportCheckSeedData()
    {
        return
        [
            new ImportCheckRecord
            {
                ReceivedDate = DateTime.Today,
                MasterBill = "MBL-260401-01",
                HouseBill = "HB-000009",
                Quantity = 120,
                Volume = 18.5m,
                Weight = 10000m,
                ContainerNumber = "TCLU1234567",
                Forwarder = "Everlink Logistics",
                Status = "Da nhan hang",
                CustomsDeclaration = "TK-99887766"
            },
            new ImportCheckRecord
            {
                ReceivedDate = DateTime.Today.AddDays(-1),
                MasterBill = "MBL-260331-03",
                HouseBill = "HB-000008",
                Quantity = 80,
                Volume = 12.2m,
                Weight = 7200m,
                ContainerNumber = "OOLU7654321",
                Forwarder = "Centech Forwarding",
                Status = "Cho thong quan",
                CustomsDeclaration = "TK-11223344"
            },
            new ImportCheckRecord
            {
                ReceivedDate = DateTime.Today.AddDays(-3),
                MasterBill = "MBL-260329-07",
                HouseBill = "HB-000007",
                Quantity = 55,
                Volume = 9.6m,
                Weight = 4300m,
                ContainerNumber = "SEGU5566778",
                Forwarder = "Everlink Logistics",
                Status = "Da thong quan",
                CustomsDeclaration = "TK-44332211"
            }
        ];
    }
    //Xoá biên nhận và lệnh xuất kho khi chưa thanh toán
    public async Task<ApiEnvelope?> DeleteLenhXuatKhoAndBienNhanTemp(long idBienNhanTemp, long idLenhXuatTemp)
    {
        var response = await _httpClient.PostAsJsonAsync(
            //BuildWorkflowUrl("/api/ctLenhXuatKhoHangNhapKhauTemp/DeleteLenhXuatAndBienNhanTemp"),
            "/api/ctLenhXuatKhoHangNhapKhauTemp/DeleteLenhXuatAndBienNhanTemp",
            new
            {
                IDBienNhanTemp = idBienNhanTemp,
                IDLenhXuatTemp = idLenhXuatTemp,
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Lỗi không xoá được item.");

        return envelope;
    }

    //Lây chi tiết lệnh xuất kho
    public async Task<LenhXuatKhoHangNhapKhauDetail> LenhXuatKhoDetailAsync(string soLenhXuat)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "/api/ctLenhXuatKhoHangNhapKhau/ListLenhXuatBySo",
            new
            {
                IDDanhMucDonVi = 1,
                SoLenhXuat = soLenhXuat
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Lỗi khi lấy thông tin chi tiết lệnh xuất kho.");

        if (string.IsNullOrWhiteSpace(envelope!.Data))
        {
            throw new InvalidOperationException("Lỗi! Không lấy được dữ liệu.");
        }

        return JsonSerializer.Deserialize<LenhXuatKhoHangNhapKhauDetail>(
            envelope.Data,
            JsonOptions)
            ?? throw new InvalidOperationException("Không thể đọc dữ liệu từ API.");
    }

    //Phe duyet lenh xuat kho
    public async Task<ApiEnvelope?> ApproveLenhXuatKho(string soLenhXuat, string ghiChu, string nguoiPheDuyet)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "/api/ctLenhXuatKhoHangNhapKhau/PheDuyet",
            new LenhXuatKhoHangNhapKhauPheDuyet
            {
                GhiChu = ghiChu,
                NguoiPheDuyet = nguoiPheDuyet,
                SoLenhXuat = soLenhXuat
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Lỗi khi phê duyệt lệnh xuất kho online.");

        return envelope;
    }

    //Tải excel thong ke
    public async Task<byte[]> DownloadExcelAsync(string tuNgay, string denNgay, int trangThaiThanhToanBNG = 1)
    {
        var legacyClient = _factory.CreateClient("LegacyApi");

        var response = await legacyClient.PostAsJsonAsync(
            "api/LenhOnlines/ExportExcel",
            new
            {
                TuNgay = tuNgay,
                DenNgay = denNgay,
                TrangThaiThanhToanBNG = trangThaiThanhToanBNG,
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        // Đọc luồng dữ liệu file trả về từ Legacy API dạng Byte Array
        return await response.Content.ReadAsByteArrayAsync();
    }
}


