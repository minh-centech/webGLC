using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using webGLCv2.Models;

namespace webGLCv2.Services;

public sealed class OnlineOrderService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    private const string ThongQuanApiPath = "m1/1263694-1261439-default/TrangThaiThongQuan";
    private const string PhiLuuKhoApiPath = "m1/1263694-1261439-default/PhiLuuKho";
    private const string ChiTietHouseBillApiPath = "m1/1263694-1261439-default/ChiTietHouseBill";

    private readonly HttpClient _httpClient;
    private readonly OnlineOrderWorkflowOptions _workflowOptions;
    private readonly List<ImportCheckRecord> _importChecks = CreateImportCheckSeedData();

    public OnlineOrderService(HttpClient httpClient, IOptions<OnlineOrderWorkflowOptions> workflowOptions)
    {
        _httpClient = httpClient;
        _workflowOptions = workflowOptions.Value;
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
        var response = await _httpClient.PostAsJsonAsync(
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
        int pageSize = 10)
        => GetOrdersInternalAsync(
            null,
            tuNgay,
            denNgay,
            houseBill,
            soCont,
            maSoThue,
            page,
            pageSize);

    private async Task<OnlineOrderPageResult> GetOrdersInternalAsync(
        long? idDanhMucKhachHangDoiLenh,
        DateTime? tuNgay,
        DateTime? denNgay,
        string? houseBill,
        string? soCont,
        string? maSoThue,
        int page,
        int pageSize)
    {
        var response = await _httpClient.PostAsJsonAsync(
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
        var response = await _httpClient.PostAsJsonAsync(
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
        var response = await _httpClient.PostAsJsonAsync(
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

    public async Task<ThongQuanCheckResponse> CheckThongQuanAsync(string houseBill, string soCont)
    {
        var response = await _httpClient.PostAsJsonAsync(
            BuildWorkflowUrl(ThongQuanApiPath),
            new
            {
                HouseBill = houseBill,
                SoCont = soCont
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ThongQuanCheckResponse>(JsonOptions)
            ?? new ThongQuanCheckResponse
            {
                Success = false,
                Message = "Khong the doc ket qua kiem tra thong quan."
            };
    }

    public async Task<PhiLuuKhoResponse> GetPhiLuuKhoAsync(string houseBill, string soCont)
    {
        var response = await _httpClient.PostAsJsonAsync(
            BuildWorkflowUrl(PhiLuuKhoApiPath),
            new
            {
                HouseBill = houseBill,
                SoCont = soCont
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PhiLuuKhoResponse>(JsonOptions)
            ?? new PhiLuuKhoResponse
            {
                Success = false,
                Message = "Khong the doc ket qua tinh phi luu kho."
            };
    }

    public async Task<ChiTietHouseBillResponse> GetChiTietHouseBillAsync(string houseBill, string soCont)
    {
        var response = await _httpClient.PostAsJsonAsync(
            BuildWorkflowUrl(ChiTietHouseBillApiPath),
            new
            {
                HouseBill = houseBill,
                SoCont = soCont
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ChiTietHouseBillResponse>(JsonOptions)
            ?? new ChiTietHouseBillResponse
            {
                Success = false,
                Message = "Khong the doc thong tin truy van house bill."
            };
    }

    public async Task<OnlineOrderWorkflowResult> RunOrderWorkflowAsync(string houseBill, string soCont)
    {
        var thongQuan = await CheckThongQuanAsync(houseBill, soCont);

        var result = new OnlineOrderWorkflowResult
        {
            ThongQuan = thongQuan
        };

        if (!thongQuan.Success || !thongQuan.IsThongQuan)
        {
            return result;
        }

        result.PhiLuuKho = await GetPhiLuuKhoAsync(houseBill, soCont);
        result.ChiTietHouseBill = await GetChiTietHouseBillAsync(houseBill, soCont);
        return result;
    }

    public async Task SeedDefaultsAsync(long idDanhMucKhachHangDoiLenh)
    {
        foreach (var draft in CreateSeedData())
        {
            await AddOrderAsync(idDanhMucKhachHangDoiLenh, draft);
        }
    }

    public async Task AddOrderAsync(long idDanhMucKhachHangDoiLenh, OnlineOrderDraft draft)
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
            TrangThai = 0,
            IDDanhMucKhachHangDoiLenh = idDanhMucKhachHangDoiLenh
        };

        var response = await _httpClient.PostAsJsonAsync("api/LenhOnlines/Insert", payload, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Khong the luu lenh online.");
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

        var samples = new Dictionary<string, (string Name, string Address, string Email)>(StringComparer.OrdinalIgnoreCase)
        {
            ["0201930936"] = ("CONG TY CO PHAN DAU TU CONG NGHE CENTECH", "Hai An, Hai Phong", "info@centech.vn"),
            ["0301464823"] = ("CONG TY TNHH THUONG MAI EVERLINK", "Quan 1, TP. Ho Chi Minh", "admin@everlink.com.vn")
        };

        if (samples.TryGetValue(normalized, out var match))
        {
            return Task.FromResult((true, "Da lay thong tin cong ty theo ma so thue.", match.Name, match.Address, match.Email));
        }

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

    private static DateTime? GetDateTime(JsonElement element, string propertyName)
    {
        var value = GetString(element, propertyName);
        return DateTime.TryParse(value, out var parsed) ? parsed : null;
    }

    private static string? NullIfEmpty(string value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private string BuildWorkflowUrl(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(_workflowOptions.BaseUrl))
        {
            throw new InvalidOperationException("Missing configuration: OnlineOrderWorkflow:BaseUrl");
        }

        return $"{_workflowOptions.BaseUrl.TrimEnd('/')}/{relativePath.TrimStart('/')}";
    }

    private static OnlineOrderRecord MapOnlineOrder(JsonElement item)
    {
        var id = GetLong(item, "ID");
        var soThuTuLenh = GetLong(item, "SoThuTuLenh");
        var trangThai = GetInt(item, "TrangThai");
        var hasPaymentInfo = GetLong(item, "ChiTietId") > 0;
        var paymentStatusCode = GetInt(item, "TrangThaiThanhToan");

        return new OnlineOrderRecord
        {
            Id = id.ToString(),
            UserId = GetLong(item, "IDDanhMucKhachHangDoiLenh"),
            UserEmail = GetString(item, "EmailUser"),
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
            StatusCode = trangThai,
            Status = GetTrangThaiText(trangThai),
            HasPaymentInfo = hasPaymentInfo,
            PaymentStatusCode = paymentStatusCode,
            PaymentStatus = hasPaymentInfo ? GetPaymentStatusText(paymentStatusCode) : string.Empty,
            InvoiceDownloadUrl = hasPaymentInfo ? GetInvoiceDownloadUrl(item) : string.Empty
        };
    }

    private static string GetOrderCode(long soThuTuLenh, long fallbackId)
    {
        var value = soThuTuLenh > 0 ? soThuTuLenh : fallbackId;
        return $"LO-{value:000000000}";
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

    private static string GetPaymentStatusText(int trangThaiThanhToan)
        => trangThaiThanhToan switch
        {
            0 => "Chưa thanh toán",
            1 => "Đã thanh toán",
            2 => "Đã hoàn tiền",
            _ => "Không xác định"
        };

    private static string GetInvoiceDownloadUrl(JsonElement item)
    {
        var directLink = GetString(item, "LinkTaiHoaDon").Trim();
        var filePath = GetString(item, "DuongDanFileHoaDon").Trim();
        if (!string.IsNullOrWhiteSpace(directLink))
        {
            if (Uri.TryCreate(directLink, UriKind.Absolute, out var absoluteUri))
            {
                return absoluteUri.ToString();
            }

            return directLink.StartsWith("/")
                ? directLink
                : "/" + directLink;
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            return string.Empty;
        }

        if (Uri.TryCreate(filePath, UriKind.Absolute, out var fileUri))
        {
            return fileUri.ToString();
        }

        if (filePath.StartsWith("/"))
        {
            return filePath;
        }

        return $"/api/TaiLieu/ViewPdf?path={Uri.EscapeDataString(filePath)}";
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
}


