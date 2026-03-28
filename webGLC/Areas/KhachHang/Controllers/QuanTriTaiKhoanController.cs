using cenDTO;
using coreDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using webGLC.Areas.KhachHang.Models;
using Newtonsoft.Json.Linq;

namespace webGLC.Areas.KhachHang.Controllers
{
    [KhachHangAdminAuthorize]
    public class QuanTriTaiKhoanController : Controller
    {
        private static string GetApiUrl(string action)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ConfigurationErrorsException("Missing appSetting 'ApiBaseUrl' in Web.config.");
            }

            return string.Format(
                "{0}/api/DanhMucKhachHangDoiLenh/{1}",
                baseUrl.TrimEnd('/'),
                action.TrimStart('/'));
        }

        private static string GetApiBaseUrl()
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ConfigurationErrorsException("Missing appSetting 'ApiBaseUrl' in Web.config.");
            }

            return baseUrl.TrimEnd('/');
        }

        public async Task<ActionResult> Index(string searchTerm = "", string phone = "", string tab = "all")
        {
            var viewModel = new QuanTriTaiKhoanPageViewModel
            {
                SearchTerm = (searchTerm ?? string.Empty).Trim(),
                Phone = (phone ?? string.Empty).Trim(),
                Tab = NormalizeTab(tab)
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetApiUrl("List"));
                    var response = await client.PostAsJsonAsync(client.BaseAddress, new { });

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Không thể tải danh sách tài khoản.");
                    }

                    var result = await response.Content.ReadAsAsync<webAPIresponse>();
                    if (result == null || result.Status != 0)
                    {
                        throw new Exception(result != null ? result.ErrorMsg : "Không thể tải danh sách tài khoản.");
                    }

                    var accounts = JsonConvert.DeserializeObject<List<DanhMucKhachHangDoiLenh>>(result.Data) ?? new List<DanhMucKhachHangDoiLenh>();
                    var mappedAccounts = accounts
                        .Select(MapAccount)
                        .OrderByDescending(x => x.LoaiTaiKhoan == 0)
                        .ThenBy(x => x.Ten)
                        .ThenBy(x => x.Email)
                        .ToList();

                    viewModel.TotalCount = mappedAccounts.Count;
                    viewModel.InactiveCount = mappedAccounts.Count(x => !x.IsActive);
                    viewModel.Accounts = ApplyFilters(mappedAccounts, viewModel.SearchTerm, viewModel.Phone, viewModel.Tab);
                }
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetActive(string id, bool isActive, string searchTerm = "", string phone = "", string tab = "all", bool returnToDocuments = false)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["QuanTriTaiKhoanError"] = "Không xác định được tài khoản cần cập nhật.";
                return returnToDocuments
                    ? RedirectToAction("Documents", new { id })
                    : RedirectToAction("Index", new { searchTerm, phone, tab });
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetApiUrl("SetActive"));
                    var request = new DanhMucKhachHangDoiLenhSetActiveRequest
                    {
                        ID = id,
                        IsActive = isActive
                    };

                    var response = await client.PostAsJsonAsync(client.BaseAddress, request);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Không thể cập nhật trạng thái tài khoản.");
                    }

                    var result = await response.Content.ReadAsAsync<webAPIresponse>();
                    if (result == null || result.Status != 0)
                    {
                        throw new Exception(result != null ? result.ErrorMsg : "Không thể cập nhật trạng thái tài khoản.");
                    }

                    TempData["QuanTriTaiKhoanSuccess"] = isActive
                        ? "Đã kích hoạt tài khoản thành công."
                        : "Đã khóa tài khoản thành công.";
                }
            }
            catch (Exception ex)
            {
                TempData["QuanTriTaiKhoanError"] = ex.Message;
            }

            if (returnToDocuments)
            {
                return RedirectToAction("Documents", new { id });
            }

            return RedirectToAction("Index", new { searchTerm, phone, tab = NormalizeTab(tab) });
        }

        public async Task<ActionResult> Documents(string id)
        {
            var viewModel = new HoSoDoanhNghiepViewModel
            {
                ID = id
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(GetApiUrl("GetHoSoDoanhNghiep") + "?id=" + Uri.EscapeDataString(id ?? string.Empty));
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Không thể tải hồ sơ tài liệu doanh nghiệp.");
                    }

                    var result = await response.Content.ReadAsAsync<webAPIresponse>();
                    if (result == null || result.Status != 0)
                    {
                        throw new Exception(result != null ? result.ErrorMsg : "Không thể tải hồ sơ tài liệu doanh nghiệp.");
                    }

                    var data = JObject.Parse(result.Data);
                    var apiBaseUrl = GetApiBaseUrl();
                    viewModel.ID = data["ID"] != null ? data["ID"].ToString() : id;
                    viewModel.Ten = data["Ten"] != null ? data["Ten"].ToString() : string.Empty;
                    viewModel.Email = data["Email"] != null ? data["Email"].ToString() : string.Empty;
                    viewModel.SoDienThoai = data["SoDienThoai"] != null ? data["SoDienThoai"].ToString() : string.Empty;
                    viewModel.LoaiTaiKhoan = ParseInt(data["LoaiTaiKhoan"]);
                    viewModel.LoaiTaiKhoanText = GetLoaiTaiKhoanText(viewModel.LoaiTaiKhoan);
                    viewModel.IsActive = ParseBool(data["IsActive"]);
                    viewModel.KichHoat = ParseBool(data["KichHoat"]);
                    viewModel.TenDoanhNghiep = data["TenDoanhNghiep"] != null ? data["TenDoanhNghiep"].ToString() : string.Empty;
                    viewModel.MaSoThue = data["MaSoThue"] != null ? data["MaSoThue"].ToString() : string.Empty;
                    viewModel.EmailDoanhNghiep = data["EmailDoanhNghiep"] != null ? data["EmailDoanhNghiep"].ToString() : string.Empty;
                    viewModel.Documents = BuildDocuments(data, apiBaseUrl, viewModel.LoaiTaiKhoan);
                }
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }

            return View(viewModel);
        }

        private static QuanTriTaiKhoanItemViewModel MapAccount(DanhMucKhachHangDoiLenh account)
        {
            var loaiTaiKhoan = ParseInt(account.LoaiTaiKhoan);
            var isActive = ParseBool(account.IsActive);
            var kichHoat = ParseBool(account.KichHoat);

            return new QuanTriTaiKhoanItemViewModel
            {
                ID = account.ID != null ? account.ID.ToString() : string.Empty,
                Ten = account.Ten != null ? account.Ten.ToString() : string.Empty,
                Email = account.Email != null ? account.Email.ToString() : string.Empty,
                SoDienThoai = account.SoDienThoai != null ? account.SoDienThoai.ToString() : string.Empty,
                LoaiTaiKhoan = loaiTaiKhoan,
                IsActive = isActive,
                KichHoat = kichHoat,
                LoaiTaiKhoanText = GetLoaiTaiKhoanText(loaiTaiKhoan),
                TrangThaiText = GetTrangThaiText(isActive, kichHoat)
            };
        }

        private static int ParseInt(object value)
        {
            int parsed;
            return value != null && int.TryParse(value.ToString(), out parsed) ? parsed : 0;
        }

        private static TaiLieuDoanhNghiepItemViewModel BuildDocument(string name, JToken pathToken, string apiBaseUrl)
        {
            var relativePath = pathToken != null ? pathToken.ToString() : string.Empty;
            return new TaiLieuDoanhNghiepItemViewModel
            {
                TenTaiLieu = name,
                RelativePath = relativePath,
                ViewUrl = string.IsNullOrWhiteSpace(relativePath)
                    ? string.Empty
                    : apiBaseUrl + "/api/TaiLieu/ViewPdf?path=" + Uri.EscapeDataString(relativePath)
            };
        }

        private static List<TaiLieuDoanhNghiepItemViewModel> BuildDocuments(JObject data, string apiBaseUrl, int loaiTaiKhoan)
        {
            var documents = new List<TaiLieuDoanhNghiepItemViewModel>();

            if (loaiTaiKhoan == 2)
            {
                documents.Add(BuildDocument("Bản scan giấy phép kinh doanh", data["BanScanGiayPhepKinhDoanhPath"], apiBaseUrl));
                documents.Add(BuildDocument("Bản scan CMND / Căn cước", data["BanScanSoCMNDCanCuocPath"], apiBaseUrl));
                documents.Add(BuildDocument("Bản đăng ký ePort có chữ ký số", data["BanDangKyEPortChuKySoPath"], apiBaseUrl));
            }
            else
            {
                documents.Add(BuildDocument("Bản scan CMND / Căn cước", data["BanScanSoCMNDCanCuocPathCaNhan"], apiBaseUrl));
            }

            return documents;
        }

        private static bool ParseBool(object value)
        {
            if (value == null)
            {
                return false;
            }

            var text = value.ToString();
            return string.Equals(text, "1", StringComparison.OrdinalIgnoreCase)
                || string.Equals(text, "true", StringComparison.OrdinalIgnoreCase);
        }

        private static string GetLoaiTaiKhoanText(int loaiTaiKhoan)
        {
            switch (loaiTaiKhoan)
            {
                case 0:
                    return "Admin";
                case 1:
                    return "Cá nhân";
                case 2:
                    return "Doanh nghiệp";
                default:
                    return "Khác";
            }
        }

        private static string GetTrangThaiText(bool isActive, bool kichHoat)
        {
            if (!kichHoat)
            {
                return isActive ? "Chờ kích hoạt" : "Chưa kích hoạt, đang khóa";
            }

            return isActive ? "Đang hoạt động" : "Đã khóa";
        }

        private static string NormalizeTab(string tab)
        {
            return string.Equals(tab, "inactive", StringComparison.OrdinalIgnoreCase) ? "inactive" : "all";
        }

        private static List<QuanTriTaiKhoanItemViewModel> ApplyFilters(
            List<QuanTriTaiKhoanItemViewModel> accounts,
            string searchTerm,
            string phone,
            string tab)
        {
            IEnumerable<QuanTriTaiKhoanItemViewModel> query = accounts ?? Enumerable.Empty<QuanTriTaiKhoanItemViewModel>();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var keyword = searchTerm.Trim();
                query = query.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.Ten) && x.Ten.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.Email) && x.Email.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                var phoneKeyword = phone.Trim();
                query = query.Where(x =>
                    !string.IsNullOrWhiteSpace(x.SoDienThoai) &&
                    x.SoDienThoai.IndexOf(phoneKeyword, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (string.Equals(NormalizeTab(tab), "inactive", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(x => !x.IsActive);
            }

            return query.ToList();
        }
    }
}
