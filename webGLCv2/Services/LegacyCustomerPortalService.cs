using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using webGLCv2.Models;

namespace webGLCv2.Services;

public sealed class LegacyCustomerPortalService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly EmailHelper _emailHelper;

    public LegacyCustomerPortalService(HttpClient httpClient, EmailHelper emailHelper)
    {
        _httpClient = httpClient;
        _emailHelper = emailHelper;
    }
    public async Task<AuthenticatedUserDto> LoginAsync(string email, string password)
    {
        var payload = new
        {
            Email = email,
            Password = password
        };

        var response = await _httpClient.PostAsJsonAsync("api/DanhMucKhachHangDoiLenh/Login", payload, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Đăng nhập không thành công.");

        using var document = JsonDocument.Parse(envelope!.Data);
        var root = document.RootElement;
        var accountType = GetInt(root, "LoaiTaiKhoan");
        var emailValue = GetString(root, "Email");
        var displayName = GetString(root, "Ten");

        return new AuthenticatedUserDto
        {
            Id = GetString(root, "ID"),
            Email = string.IsNullOrWhiteSpace(emailValue) ? email : emailValue,
            DisplayName = string.IsNullOrWhiteSpace(displayName) ? email : displayName,
            AccountType = accountType,
            AccountTypeName = GetAccountTypeName(accountType),
            RoleName = accountType == 0 ? "Admin" : "User"
        };
    }

    public async Task<List<AccountListItem>> GetAccountsAsync()
    {
        var response = await _httpClient.PostAsJsonAsync("api/DanhMucKhachHangDoiLenh/List", new { }, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể tải danh sách tài khoản.");

        var result = new List<AccountListItem>();
        using var document = JsonDocument.Parse(envelope!.Data);
        foreach (var item in document.RootElement.EnumerateArray())
        {
            var accountType = GetInt(item, "LoaiTaiKhoan");
            var isActive = GetBool(item, "IsActive");
            var activatedFlag = GetBool(item, "KichHoat");
            result.Add(new AccountListItem
            {
                Id = GetString(item, "ID"),
                Name = GetString(item, "Ten"),
                Email = GetString(item, "Email"),
                Phone = GetString(item, "SoDienThoai"),
                AccountType = accountType,
                AccountTypeText = GetAccountTypeName(accountType),
                IsActive = isActive,
                ActivatedFlag = activatedFlag,
                StatusText = ResolveStatusText(isActive, activatedFlag)
            });
        }

        return result;
    }

    public async Task SetAccountActiveAsync(string id, bool isActive)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/DanhMucKhachHangDoiLenh/SetActive",
            new { ID = id, IsActive = isActive },
            JsonOptions);

        response.EnsureSuccessStatusCode();
        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể cập nhật trạng thái tài khoản.");
    }

    public async Task ApproveAccountAndNotifyAsync(string id, string email)
    {
        await SetAccountActiveAsync(id, true);
        await _emailHelper.SendEmailAsync(email, EmailHelper.AccountApprovedSuccessTemplateId);
    }

    public async Task<OnlineOrderRecord?> FindOnlineOrderByHouseBillAsync(string houseBill)
    {
        if (string.IsNullOrWhiteSpace(houseBill))
        {
            return null;
        }

        var response = await _httpClient.PostAsJsonAsync(
            "api/LenhOnlines/List",
            new
            {
                HouseBill = houseBill.Trim(),
                Page = 1,
                PageSize = 1
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể kiểm tra lệnh online theo House Bill.");

        using var document = JsonDocument.Parse(envelope!.Data);
        var root = document.RootElement;
        var itemsElement = root.TryGetProperty("Items", out var itemsValue) ? itemsValue : root;

        if (itemsElement.ValueKind != JsonValueKind.Array || itemsElement.GetArrayLength() == 0)
        {
            return null;
        }

        var item = itemsElement[0];
        return new OnlineOrderRecord
        {
            Id = GetString(item, "ID"),
            OrderCode = GetString(item, "OrderCode"),
            UserId = GetLong(item, "IDDanhMucKhachHangDoiLenh"),
            UserEmail = GetString(item, "UserEmail"),
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
            DeclarationNumber = GetString(item, "SoToKhai"),
            StatusCode = GetInt(item, "TrangThai"),
            Status = GetString(item, "TrangThaiText")
        };
    }

    public async Task<AccountDocumentDetails> GetAccountDocumentsAsync(string id)
    {
        var envelope = await _httpClient.GetFromJsonAsync<ApiEnvelope>(
            $"api/DanhMucKhachHangDoiLenh/GetHoSoDoanhNghiep?id={Uri.EscapeDataString(id)}",
            JsonOptions);

        EnsureSuccess(envelope, "Không thể tải hồ sơ tài liệu.");

        using var document = JsonDocument.Parse(envelope!.Data);
        var root = document.RootElement;
        var accountType = GetInt(root, "LoaiTaiKhoan");
        var details = new AccountDocumentDetails
        {
            Id = GetString(root, "ID"),
            Name = GetString(root, "Ten"),
            Email = GetString(root, "Email"),
            Phone = GetString(root, "SoDienThoai"),
            AccountType = accountType,
            AccountTypeText = GetAccountTypeName(accountType),
            IsActive = GetBool(root, "IsActive"),
            ActivatedFlag = GetBool(root, "KichHoat"),
            CompanyName = GetString(root, "TenDoanhNghiep"),
            TaxCode = GetString(root, "MaSoThue"),
            CompanyEmail = GetString(root, "EmailDoanhNghiep"),
            BillingEmail = GetString(root, "EmailXuatHoaDon")
        };

        if (accountType == 2)
        {
            var companyProfile = await GetLatestEnterpriseProfileAsync(id);
            if (companyProfile is not null)
            {
                details.CompanyId = companyProfile.ID.ToString();
                details.CompanyName = companyProfile.TenDoanhNghiep;
                details.TaxCode = companyProfile.MaSoThue;
                details.CompanyEmail = companyProfile.EmailDoanhNghiep;
                details.CompanyAddress = companyProfile.DiaChi;
                details.CompanyPhone = companyProfile.SoDienThoaiDoanhNghiep;
                details.CompanyFax = companyProfile.SoFax;
                details.BusinessLicenseNumber = companyProfile.GiayPhepKinhDoanh;
                details.IssueDate = companyProfile.NgayCap;
                details.IssuePlace = companyProfile.NoiCap;
                details.AuthorizedRepresentative = companyProfile.DaiDienCoThamQuyen;
                details.RepresentativeTitle = companyProfile.ChucVu;
                details.AuthorizedCompany = companyProfile.DoanhNghiepCongTyDuocUyQuyen;
                details.ServiceUserName = companyProfile.TenDangNhapDangKyDichVu;
                details.BillingEmail = companyProfile.EmailXuatHoaDon;
                details.CitizenIdNumber = companyProfile.SoCMNDCanCuoc;
            }

            details.Documents.Add(BuildDocument("Bản scan giấy phép kinh doanh", GetString(root, "BanScanGiayPhepKinhDoanhPath"), "BanScanGiayPhepKinhDoanhPath", "NguoiDungDoanhNghiep/GiayPhepKinhDoanh"));
            details.Documents.Add(BuildDocument("Bản scan CMND / Căn cước", GetString(root, "BanScanSoCMNDCanCuocPath"), "BanScanSoCMNDCanCuocPath", "NguoiDungDoanhNghiep/CanCuoc"));
            details.Documents.Add(BuildDocument("Bản đăng ký ePort có chữ ký số", GetString(root, "BanDangKyEPortChuKySoPath"), "BanDangKyEPortChuKySoPath", "NguoiDungDoanhNghiep/DangKyEPort"));
        }
        else
        {
            details.Documents.Add(BuildDocument("Bản scan CMND / Căn cước", GetString(root, "BanScanSoCMNDCanCuocPathCaNhan"), "BanScanSoCMNDCanCuocPathCaNhan", "KhachHangCaNhan/CanCuoc"));
            details.Documents.Add(BuildDocument("Bản đăng ký cá nhân có chữ ký", GetString(root, "BanDangKyCaNhanCoChuKyPath"), "BanDangKyCaNhanCoChuKyPath", "KhachHangCaNhan/DangKy"));
        }

        return details;
    }

    public async Task UpdatePersonalDocumentsAsync(
        string accountId,
        string currentCitizenCardPath,
        string currentSignedFormPath,
        string fieldKey,
        string relativePath)
    {
        var payload = new
        {
            ID = accountId,
            BanScanSoCMNDCanCuocPathCaNhan = fieldKey == "BanScanSoCMNDCanCuocPathCaNhan" ? relativePath : NullIfEmpty(currentCitizenCardPath),
            BanDangKyCaNhanCoChuKyPath = fieldKey == "BanDangKyCaNhanCoChuKyPath" ? relativePath : NullIfEmpty(currentSignedFormPath)
        };

        var response = await _httpClient.PostAsJsonAsync("api/DanhMucKhachHangDoiLenh/SaveTaiLieuCaNhan", payload, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể cập nhật tài liệu cá nhân.");
    }

    public async Task UpdateEnterpriseDocumentAsync(string accountId, string fieldKey, string relativePath)
    {
        var profile = await GetLatestEnterpriseProfileAsync(accountId);
        if (profile is null)
        {
            throw new InvalidOperationException("Không tìm thấy hồ sơ doanh nghiệp để cập nhật tài liệu.");
        }

        switch (fieldKey)
        {
            case "BanScanGiayPhepKinhDoanhPath":
                profile.BanScanGiayPhepKinhDoanhPath = relativePath;
                break;
            case "BanScanSoCMNDCanCuocPath":
                profile.BanScanSoCMNDCanCuocPath = relativePath;
                break;
            case "BanDangKyEPortChuKySoPath":
                profile.BanDangKyEPortChuKySoPath = relativePath;
                break;
            default:
                throw new InvalidOperationException("Loại tài liệu doanh nghiệp không hợp lệ.");
        }

        var payload = new
        {
            ID = profile.ID,
            IDDanhMucKhachHangDoiLenh = profile.IDDanhMucKhachHangDoiLenh,
            TenDoanhNghiep = profile.TenDoanhNghiep,
            MaSoThue = profile.MaSoThue,
            DiaChi = profile.DiaChi,
            SoDienThoaiDoanhNghiep = profile.SoDienThoaiDoanhNghiep,
            EmailDoanhNghiep = profile.EmailDoanhNghiep,
            SoFax = NullIfEmpty(profile.SoFax),
            GiayPhepKinhDoanh = NullIfEmpty(profile.GiayPhepKinhDoanh),
            NgayCap = profile.NgayCap,
            NoiCap = NullIfEmpty(profile.NoiCap),
            DaiDienCoThamQuyen = NullIfEmpty(profile.DaiDienCoThamQuyen),
            ChucVu = NullIfEmpty(profile.ChucVu),
            DoanhNghiepCongTyDuocUyQuyen = NullIfEmpty(profile.DoanhNghiepCongTyDuocUyQuyen),
            TenDangNhapDangKyDichVu = NullIfEmpty(profile.TenDangNhapDangKyDichVu),
            EmailXuatHoaDon = NullIfEmpty(profile.EmailXuatHoaDon),
            SoCMNDCanCuoc = NullIfEmpty(profile.SoCMNDCanCuoc),
            BanScanGiayPhepKinhDoanhPath = NullIfEmpty(profile.BanScanGiayPhepKinhDoanhPath),
            BanScanSoCMNDCanCuocPath = NullIfEmpty(profile.BanScanSoCMNDCanCuocPath),
            BanDangKyEPortChuKySoPath = NullIfEmpty(profile.BanDangKyEPortChuKySoPath),
            IsActive = profile.IsActive
        };

        var response = await _httpClient.PostAsJsonAsync("api/DanhMucKhachHangDoiLenh/SaveDoanhNghiep", payload, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể cập nhật tài liệu doanh nghiệp.");
    }

    public async Task<List<LegacyCompanyProfile>> GetCompaniesByCustomerAsync(string accountId)
    {
        var envelope = await _httpClient.GetFromJsonAsync<ApiEnvelope>(
            $"api/DanhMucKhachHangDoiLenh/ListDoanhNghiepByKhachHang?khachHangId={Uri.EscapeDataString(accountId)}",
            JsonOptions);

        EnsureSuccess(envelope, "Không thể tải thông tin doanh nghiệp.");

        return JsonSerializer.Deserialize<List<LegacyCompanyProfile>>(envelope!.Data, JsonOptions) ?? new List<LegacyCompanyProfile>();
    }

    public async Task<long> CreateCompanyAsync(string accountId, UserCompanyFormModel model)
    {

        var payload = new
        {
            IDDanhMucKhachHangDoiLenh = accountId,
            TenDoanhNghiep = model.TenDoanhNghiep,
            MaSoThue = model.MaSoThue,
            DiaChi = model.DiaChi,
            SoDienThoaiDoanhNghiep = model.SoDienThoaiDoanhNghiep,
            EmailDoanhNghiep = model.EmailDoanhNghiep,
            SoFax = NullIfEmpty(model.SoFax ?? string.Empty),
            GiayPhepKinhDoanh = NullIfEmpty(model.GiayPhepKinhDoanh ?? string.Empty),
            NgayCap = model.NgayCap,
            NoiCap = NullIfEmpty(model.NoiCap ?? string.Empty),
            DaiDienCoThamQuyen = NullIfEmpty(model.DaiDienCoThamQuyen ?? string.Empty),
            ChucVu = NullIfEmpty(model.ChucVu ?? string.Empty),
            DoanhNghiepCongTyDuocUyQuyen = NullIfEmpty(model.DoanhNghiepCongTyDuocUyQuyen ?? string.Empty),
            TenDangNhapDangKyDichVu = NullIfEmpty(model.TenDangNhapDangKyDichVu ?? string.Empty),
            EmailXuatHoaDon = NullIfEmpty(model.EmailXuatHoaDon ?? string.Empty),
            SoCMNDCanCuoc = NullIfEmpty(model.SoCMNDCanCuoc ?? string.Empty),
            BanScanGiayPhepKinhDoanhPath = (string?)null,
            BanScanSoCMNDCanCuocPath = (string?)null,
            BanDangKyEPortChuKySoPath = (string?)null,
            IsActive = false
        };

        var response = await _httpClient.PostAsJsonAsync("api/DanhMucKhachHangDoiLenh/SaveDoanhNghiep", payload, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể tạo thông tin doanh nghiệp.");

        using var document = JsonDocument.Parse(envelope!.Data);
        return GetLong(document.RootElement, "ID");
    }


    public async Task SetEnterpriseApprovalAsync(LegacyCompanyProfile company, bool isApproved)
    {
        var payload = new
        {
            ID = company.ID,
            IDDanhMucKhachHangDoiLenh = company.IDDanhMucKhachHangDoiLenh,
            TenDoanhNghiep = company.TenDoanhNghiep,
            MaSoThue = company.MaSoThue,
            DiaChi = company.DiaChi,
            SoDienThoaiDoanhNghiep = company.SoDienThoaiDoanhNghiep,
            EmailDoanhNghiep = company.EmailDoanhNghiep,
            SoFax = NullIfEmpty(company.SoFax),
            GiayPhepKinhDoanh = NullIfEmpty(company.GiayPhepKinhDoanh),
            NgayCap = company.NgayCap,
            NoiCap = NullIfEmpty(company.NoiCap),
            DaiDienCoThamQuyen = NullIfEmpty(company.DaiDienCoThamQuyen),
            ChucVu = NullIfEmpty(company.ChucVu),
            DoanhNghiepCongTyDuocUyQuyen = NullIfEmpty(company.DoanhNghiepCongTyDuocUyQuyen),
            TenDangNhapDangKyDichVu = NullIfEmpty(company.TenDangNhapDangKyDichVu),
            EmailXuatHoaDon = NullIfEmpty(company.EmailXuatHoaDon),
            SoCMNDCanCuoc = NullIfEmpty(company.SoCMNDCanCuoc),
            BanScanGiayPhepKinhDoanhPath = NullIfEmpty(company.BanScanGiayPhepKinhDoanhPath),
            BanScanSoCMNDCanCuocPath = NullIfEmpty(company.BanScanSoCMNDCanCuocPath),
            BanDangKyEPortChuKySoPath = NullIfEmpty(company.BanDangKyEPortChuKySoPath),
            IsActive = isApproved
        };

        var response = await _httpClient.PostAsJsonAsync("api/DanhMucKhachHangDoiLenh/SaveDoanhNghiep", payload, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể cập nhật trạng thái duyệt doanh nghiệp.");
    }

    public async Task ApproveEnterpriseAndNotifyAsync(LegacyCompanyProfile company, string email)
    {
        await SetEnterpriseApprovalAsync(company, true);
        await _emailHelper.SendEmailAsync(email, EmailHelper.AccountApprovedSuccessTemplateId);
    }

    public async Task UpdateCompanyProfileAsync(
        LegacyCompanyProfile company,
        string companyName,
        string billingEmail,
        string companyAddress,
        string companyPhone)
    {
        var payload = new
        {
            ID = company.ID,
            IDDanhMucKhachHangDoiLenh = company.IDDanhMucKhachHangDoiLenh,
            TenDoanhNghiep = companyName,
            MaSoThue = company.MaSoThue,
            DiaChi = companyAddress,
            SoDienThoaiDoanhNghiep = companyPhone,
            EmailDoanhNghiep = NullIfEmpty(company.EmailDoanhNghiep),
            SoFax = NullIfEmpty(company.SoFax),
            GiayPhepKinhDoanh = NullIfEmpty(company.GiayPhepKinhDoanh),
            NgayCap = company.NgayCap,
            NoiCap = NullIfEmpty(company.NoiCap),
            DaiDienCoThamQuyen = NullIfEmpty(company.DaiDienCoThamQuyen),
            ChucVu = NullIfEmpty(company.ChucVu),
            DoanhNghiepCongTyDuocUyQuyen = NullIfEmpty(company.DoanhNghiepCongTyDuocUyQuyen),
            TenDangNhapDangKyDichVu = NullIfEmpty(company.TenDangNhapDangKyDichVu),
            EmailXuatHoaDon = NullIfEmpty(billingEmail),
            SoCMNDCanCuoc = NullIfEmpty(company.SoCMNDCanCuoc),
            BanScanGiayPhepKinhDoanhPath = NullIfEmpty(company.BanScanGiayPhepKinhDoanhPath),
            BanScanSoCMNDCanCuocPath = NullIfEmpty(company.BanScanSoCMNDCanCuocPath),
            BanDangKyEPortChuKySoPath = NullIfEmpty(company.BanDangKyEPortChuKySoPath),
            IsActive = company.IsActive
        };

        var response = await _httpClient.PostAsJsonAsync("api/DanhMucKhachHangDoiLenh/SaveDoanhNghiep", payload, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể cập nhật thông tin doanh nghiệp.");
    }

    public async Task UpdateEnterpriseProfileAsync(
        string accountId,
        string companyName,
        string companyEmail,
        string companyAddress,
        string companyPhone,
        string? companyFax,
        string? businessLicenseNumber,
        DateTime? issueDate,
        string? issuePlace,
        string? authorizedRepresentative,
        string? representativeTitle,
        string? billingEmail)
    {
        var profile = await GetLatestEnterpriseProfileAsync(accountId);
        if (profile is null)
        {
            throw new InvalidOperationException("Không tìm thấy hồ sơ doanh nghiệp để cập nhật.");
        }

        var payload = new
        {
            ID = profile.ID,
            IDDanhMucKhachHangDoiLenh = profile.IDDanhMucKhachHangDoiLenh,
            TenDoanhNghiep = companyName,
            MaSoThue = profile.MaSoThue,
            DiaChi = companyAddress,
            SoDienThoaiDoanhNghiep = companyPhone,
            EmailDoanhNghiep = companyEmail,
            SoFax = NullIfEmpty(companyFax ?? string.Empty),
            GiayPhepKinhDoanh = NullIfEmpty(businessLicenseNumber ?? string.Empty),
            NgayCap = issueDate,
            NoiCap = NullIfEmpty(issuePlace ?? string.Empty),
            DaiDienCoThamQuyen = NullIfEmpty(authorizedRepresentative ?? string.Empty),
            ChucVu = NullIfEmpty(representativeTitle ?? string.Empty),
            DoanhNghiepCongTyDuocUyQuyen = NullIfEmpty(profile.DoanhNghiepCongTyDuocUyQuyen),
            TenDangNhapDangKyDichVu = NullIfEmpty(profile.TenDangNhapDangKyDichVu),
            EmailXuatHoaDon = NullIfEmpty(billingEmail ?? string.Empty),
            SoCMNDCanCuoc = NullIfEmpty(profile.SoCMNDCanCuoc),
            BanScanGiayPhepKinhDoanhPath = NullIfEmpty(profile.BanScanGiayPhepKinhDoanhPath),
            BanScanSoCMNDCanCuocPath = NullIfEmpty(profile.BanScanSoCMNDCanCuocPath),
            BanDangKyEPortChuKySoPath = NullIfEmpty(profile.BanDangKyEPortChuKySoPath),
            IsActive = profile.IsActive
        };

        var response = await _httpClient.PostAsJsonAsync("api/DanhMucKhachHangDoiLenh/SaveDoanhNghiep", payload, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể cập nhật thông tin doanh nghiệp.");
    }
    public async Task RegisterAccountAsync(RegisterAccountModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/DanhMucKhachHangDoiLenh/RegisterTaiKhoan", model, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể gửi đăng ký tài khoản.");
    }

    public async Task<string> RecoverPasswordAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required.", nameof(email));
        }

        var response = await _httpClient.PostAsJsonAsync(
            "api/DanhMucKhachHangDoiLenh/RecoverPassword",
            new { Email = email.Trim() },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể cấp lại mật khẩu.");

        var newPassword = envelope?.Data?.Trim();
        if (string.IsNullOrWhiteSpace(newPassword))
        {
            throw new InvalidOperationException("Không thể tạo mật khẩu mới.");
        }

        return newPassword;
    }

    public async Task RecoverPasswordAndNotifyAsync(string email)
    {
        var newPassword = await RecoverPasswordAsync(email);
        await _emailHelper.SendPasswordResetEmailAsync(email, newPassword);
    }

    public async Task UpdatePersonalProfileAsync(string accountId, string fullName, string phoneNumber, string? billingEmail, string? email = null)
    {
        var payload = new
        {
            ID = accountId,
            Ten = fullName,
            SoDienThoai = phoneNumber,
            Email = NullIfEmpty(email ?? string.Empty),
            EmailXuatHoaDon = NullIfEmpty(billingEmail ?? string.Empty)
        };

        var response = await _httpClient.PostAsJsonAsync("api/DanhMucKhachHangDoiLenh/SaveThongTinCaNhan", payload, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể cập nhật thông tin cá nhân.");
    }

    public async Task UpdateOrderPickupDateAsync(OnlineOrderRecord order, DateTime pickupDate)
    {
        if (!long.TryParse(order.Id, out var id))
        {
            throw new InvalidOperationException("Không xác định được ID lệnh để cập nhật ngày lấy hàng.");
        }

        var response = await _httpClient.PostAsJsonAsync(
            "api/LenhOnlines/Update",
            new LenhOnlinesUpdateRequest
            {
                ID = id,
                HoVaTen = order.CustomerName,
                SoDienThoai = order.PhoneNumber,
                SoCMND = order.IdentityNumber,
                SoXe = order.VehicleNumber,
                MaSoThue = order.TaxCode,
                TenCongTy = order.CompanyName,
                DiaChi = order.CompanyAddress,
                Email = order.CompanyEmail,
                HouseBill = order.HouseBill,
                SoCont = order.ContainerNumber,
                NgayLayHang = pickupDate.Date,
                SoToKhai = order.DeclarationNumber,
                TrangThai = order.StatusCode,
                IDDanhMucKhachHangDoiLenh = order.UserId
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể cập nhật ngày lấy hàng cho lệnh online.");
    }

    public async Task ChangePasswordAsync(string email, string oldPassword, string newPassword, string newPasswordConfirm)
    {
        var payload = new
        {
            Email = email,
            OldPassword = oldPassword,
            NewPassword = newPassword,
            NewPasswordConfirm = newPasswordConfirm,
            Ten = string.Empty
        };

        var response = await _httpClient.PostAsJsonAsync("api/DanhMucKhachHangDoiLenh/UpdateChangePassword", payload, JsonOptions);
        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions);
        EnsureSuccess(envelope, "Không thể đổi mật khẩu.");
    }

    public async Task<LegacyUploadPdfResult> UploadPdfAsync(
        Stream stream,
        string fileName,
        string folder,
        CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();
        using var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        content.Add(fileContent, "file", fileName);

        var response = await _httpClient.PostAsync(
            $"api/TaiLieu/UploadPdf?folder={Uri.EscapeDataString(folder)}",
            content,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var envelope = await response.Content.ReadFromJsonAsync<ApiEnvelope>(JsonOptions, cancellationToken);
        EnsureSuccess(envelope, "Không thể tải file PDF lên hệ thống.");

        var result = JsonSerializer.Deserialize<LegacyUploadPdfResult>(envelope!.Data, JsonOptions);
        return result ?? new LegacyUploadPdfResult();
    }

    private async Task<LegacyCompanyProfile?> GetLatestEnterpriseProfileAsync(string accountId)
    {
        var envelope = await _httpClient.GetFromJsonAsync<ApiEnvelope>(
            $"api/DanhMucKhachHangDoiLenh/ListDoanhNghiepByKhachHang?khachHangId={Uri.EscapeDataString(accountId)}",
            JsonOptions);

        EnsureSuccess(envelope, "Không thể tải hồ sơ doanh nghiệp.");

        var companies = JsonSerializer.Deserialize<List<LegacyCompanyProfile>>(envelope!.Data, JsonOptions);
        return companies?.FirstOrDefault();
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

    private static int GetInt(JsonElement element, string propertyName)
        => int.TryParse(GetString(element, propertyName), out var parsed) ? parsed : 0;

    private static long GetLong(JsonElement element, string propertyName)
        => long.TryParse(GetString(element, propertyName), out var parsed) ? parsed : 0;

    private static bool GetBool(JsonElement element, string propertyName)
    {
        var value = GetString(element, propertyName);
        return value.Equals("true", StringComparison.OrdinalIgnoreCase)
            || value.Equals("1", StringComparison.OrdinalIgnoreCase);
    }

    private static string GetAccountTypeName(int accountType)
        => accountType switch
        {
            0 => "Admin",
            1 => "Cá nhân",
            2 => "Doanh nghiệp",
            _ => "Tài khoản"
        };

    private DocumentFileItem BuildDocument(string name, string relativePath, string fieldKey, string uploadFolder)
    {
        var trimmedPath = relativePath?.Trim() ?? string.Empty;
        return new DocumentFileItem
        {
            Name = name,
            FieldKey = fieldKey,
            UploadFolder = uploadFolder,
            RelativePath = trimmedPath,
            ViewUrl = string.IsNullOrWhiteSpace(trimmedPath)
                ? string.Empty
                : $"{_httpClient.BaseAddress}api/TaiLieu/ViewPdf?path={Uri.EscapeDataString(trimmedPath)}"
        };
    }

    private static string? NullIfEmpty(string value)
        => string.IsNullOrWhiteSpace(value) ? null : value;

    private static string ResolveStatusText(bool isActive, bool activatedFlag)
    {
        if (!activatedFlag)
        {
            return isActive ? "Chờ kích hoạt" : "Chưa kích hoạt";
        }

        return isActive ? "Đang hoạt động" : "Đã khóa";
    }

    private sealed class LenhOnlinesUpdateRequest
    {
        public long? ID { get; set; }
        public string HoVaTen { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public string? SoCMND { get; set; }
        public string? SoXe { get; set; }
        public string? MaSoThue { get; set; }
        public string? TenCongTy { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }
        public string? HouseBill { get; set; }
        public string? SoCont { get; set; }
        public DateTime? NgayLayHang { get; set; }
        public string? SoToKhai { get; set; }
        public int TrangThai { get; set; }
        public long IDDanhMucKhachHangDoiLenh { get; set; }
    }
}


