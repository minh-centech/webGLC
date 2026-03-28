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

namespace webGLC.Areas.KhachHang.Controllers
{
    [KhachHangAuthorize]
    public class NguoiDungController : Controller
    {
        private const string SessionUserIdKey = "KhachHangId";
        private const string SessionUserAccountTypeKey = "KhachHangLoaiTaiKhoan";

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

        public async Task<ActionResult> Index()
        {
            var userContext = GetCurrentUserContext();
            if (userContext == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var viewModel = new DoanhNghiepListPageViewModel
            {
                CanCreate = userContext.LoaiTaiKhoan == 1,
                IsEnterpriseAccount = userContext.LoaiTaiKhoan == 2
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(GetApiUrl("ListDoanhNghiepByKhachHang") + "?khachHangId=" + userContext.UserId);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Không thể tải danh sách doanh nghiệp.");
                    }

                    var result = await response.Content.ReadAsAsync<webAPIresponse>();
                    if (result == null || result.Status != 0)
                    {
                        throw new Exception(result != null ? result.ErrorMsg : "Không thể tải danh sách doanh nghiệp.");
                    }

                    var items = JsonConvert.DeserializeObject<List<NguoiDungDoanhNghiepDto>>(result.Data) ?? new List<NguoiDungDoanhNghiepDto>();
                    viewModel.Items = items.Select(MapItem).ToList();
                }
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var userContext = GetCurrentUserContext();
            if (userContext == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (userContext.LoaiTaiKhoan != 1)
            {
                return RedirectToAction("Index");
            }

            return View("Edit", new DoanhNghiepEditViewModel
            {
                IDDanhMucKhachHangDoiLenh = userContext.UserId,
                LoaiTaiKhoanNguoiDung = userContext.LoaiTaiKhoan,
                IsReadOnlyAccount = false,
                TenDangNhapDangKyDichVu = Session["KhachHangEmail"] as string
            });
        }

        public async Task<ActionResult> Edit(string id)
        {
            var userContext = GetCurrentUserContext();
            if (userContext == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("Index");
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(GetApiUrl("GetDoanhNghiep") + "?id=" + Uri.EscapeDataString(id));
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Không thể tải thông tin doanh nghiệp.");
                    }

                    var result = await response.Content.ReadAsAsync<webAPIresponse>();
                    if (result == null || result.Status != 0)
                    {
                        throw new Exception(result != null ? result.ErrorMsg : "Không thể tải thông tin doanh nghiệp.");
                    }

                    var dto = JsonConvert.DeserializeObject<NguoiDungDoanhNghiepDto>(result.Data);
                    var ownerId = ParseLong(dto != null ? dto.IDDanhMucKhachHangDoiLenh : null);
                    if (ownerId != userContext.UserId)
                    {
                        return RedirectToAction("Index");
                    }

                    return View(MapEdit(dto, userContext.LoaiTaiKhoan));
                }
            }
            catch (Exception ex)
            {
                TempData["DoanhNghiepError"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(DoanhNghiepEditViewModel model)
        {
            var userContext = GetCurrentUserContext();
            if (userContext == null)
            {
                return RedirectToAction("Index", "Login");
            }

            model.IDDanhMucKhachHangDoiLenh = userContext.UserId;
            model.LoaiTaiKhoanNguoiDung = userContext.LoaiTaiKhoan;

            if (userContext.LoaiTaiKhoan == 2 && !model.ID.HasValue)
            {
                TempData["DoanhNghiepError"] = "Tài khoản doanh nghiệp chỉ được chỉnh sửa hồ sơ đã đăng ký.";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(GetApiUrl("SaveDoanhNghiep"), model.ToRequest());
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Không thể lưu doanh nghiệp.");
                    }

                    var result = await response.Content.ReadAsAsync<webAPIresponse>();
                    if (result == null || result.Status != 0)
                    {
                        throw new Exception(result != null ? result.ErrorMsg : "Không thể lưu doanh nghiệp.");
                    }

                    TempData["DoanhNghiepSuccess"] = model.ID.HasValue
                        ? "Đã cập nhật doanh nghiệp thành công."
                        : "Đã thêm mới doanh nghiệp thành công.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", model);
            }
        }

        private UserContext GetCurrentUserContext()
        {
            long userId;
            int loaiTaiKhoan;

            if (!long.TryParse(Convert.ToString(Session[SessionUserIdKey]), out userId))
            {
                return null;
            }

            if (!int.TryParse(Convert.ToString(Session[SessionUserAccountTypeKey]), out loaiTaiKhoan))
            {
                loaiTaiKhoan = 1;
            }

            return new UserContext
            {
                UserId = userId,
                LoaiTaiKhoan = loaiTaiKhoan
            };
        }

        private static DoanhNghiepListItemViewModel MapItem(NguoiDungDoanhNghiepDto dto)
        {
            var isActive = ParseBool(dto.IsActive);
            return new DoanhNghiepListItemViewModel
            {
                ID = Convert.ToString(dto.ID),
                TenDoanhNghiep = Convert.ToString(dto.TenDoanhNghiep),
                MaSoThue = Convert.ToString(dto.MaSoThue),
                EmailDoanhNghiep = Convert.ToString(dto.EmailDoanhNghiep),
                SoDienThoaiDoanhNghiep = Convert.ToString(dto.SoDienThoaiDoanhNghiep),
                IsActive = isActive,
                TrangThaiText = isActive ? "Đang hoạt động" : "Đang tạm khóa"
            };
        }

        private static DoanhNghiepEditViewModel MapEdit(NguoiDungDoanhNghiepDto dto, int loaiTaiKhoan)
        {
            return new DoanhNghiepEditViewModel
            {
                ID = ParseNullableLong(dto.ID),
                IDDanhMucKhachHangDoiLenh = ParseLong(dto.IDDanhMucKhachHangDoiLenh),
                LoaiTaiKhoanNguoiDung = loaiTaiKhoan,
                IsReadOnlyAccount = loaiTaiKhoan == 2,
                TenDoanhNghiep = Convert.ToString(dto.TenDoanhNghiep),
                MaSoThue = Convert.ToString(dto.MaSoThue),
                DiaChi = Convert.ToString(dto.DiaChi),
                SoDienThoaiDoanhNghiep = Convert.ToString(dto.SoDienThoaiDoanhNghiep),
                EmailDoanhNghiep = Convert.ToString(dto.EmailDoanhNghiep),
                SoFax = Convert.ToString(dto.SoFax),
                GiayPhepKinhDoanh = Convert.ToString(dto.GiayPhepKinhDoanh),
                BanScanGiayPhepKinhDoanhPath = Convert.ToString(dto.BanScanGiayPhepKinhDoanhPath),
                NgayCap = ParseNullableDate(dto.NgayCap),
                NoiCap = Convert.ToString(dto.NoiCap),
                DaiDienCoThamQuyen = Convert.ToString(dto.DaiDienCoThamQuyen),
                ChucVu = Convert.ToString(dto.ChucVu),
                DoanhNghiepCongTyDuocUyQuyen = Convert.ToString(dto.DoanhNghiepCongTyDuocUyQuyen),
                TenDangNhapDangKyDichVu = Convert.ToString(dto.TenDangNhapDangKyDichVu),
                EmailXuatHoaDon = Convert.ToString(dto.EmailXuatHoaDon),
                SoCMNDCanCuoc = Convert.ToString(dto.SoCMNDCanCuoc),
                BanScanSoCMNDCanCuocPath = Convert.ToString(dto.BanScanSoCMNDCanCuocPath),
                BanDangKyEPortChuKySoPath = Convert.ToString(dto.BanDangKyEPortChuKySoPath),
                IsActive = ParseBool(dto.IsActive)
            };
        }

        private static long ParseLong(object value)
        {
            long parsed;
            return long.TryParse(Convert.ToString(value), out parsed) ? parsed : 0;
        }

        private static long? ParseNullableLong(object value)
        {
            long parsed;
            return long.TryParse(Convert.ToString(value), out parsed) ? parsed : (long?)null;
        }

        private static bool ParseBool(object value)
        {
            var text = Convert.ToString(value);
            return string.Equals(text, "1", StringComparison.OrdinalIgnoreCase)
                || string.Equals(text, "true", StringComparison.OrdinalIgnoreCase);
        }

        private static DateTime? ParseNullableDate(object value)
        {
            DateTime parsed;
            return DateTime.TryParse(Convert.ToString(value), out parsed) ? parsed : (DateTime?)null;
        }

        private class UserContext
        {
            public long UserId { get; set; }
            public int LoaiTaiKhoan { get; set; }
        }
    }
}
