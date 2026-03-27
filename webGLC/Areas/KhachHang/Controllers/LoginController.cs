using cenDTO;
using coreDTO;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using webGLC.Areas.Admin.Models;
using webGLC.Areas.KhachHang.Models;

namespace webGLC.Areas.KhachHang.Controllers
{
    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }


    public class LoginController : Controller
    {
        private const string SessionUserKey = "KhachHangUser";
        private const string SessionUserEmailKey = "KhachHangEmail";
        private const string SessionUserIdKey = "KhachHangId";
        private const string SessionUserDisplayNameKey = "KhachHangDisplayName";

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

        private void StoreAuthenticatedUserSession(string email, string id = null, string rawData = null)
        {
            var displayName = TryExtractDisplayNameFromApiData(rawData, email);
            Session[SessionUserKey] = new
            {
                Email = email,
                ID = id,
                DisplayName = displayName,
                Data = rawData
            };
            Session[SessionUserEmailKey] = email;
            Session[SessionUserIdKey] = id;
            Session[SessionUserDisplayNameKey] = displayName;
        }

        private void ClearAuthenticatedUserSession()
        {
            Session.Remove(SessionUserKey);
            Session.Remove(SessionUserEmailKey);
            Session.Remove(SessionUserIdKey);
            Session.Remove(SessionUserDisplayNameKey);
        }

        private string TryExtractIdFromApiData(string rawData)
        {
            if (string.IsNullOrWhiteSpace(rawData))
            {
                return null;
            }

            try
            {
                var jsonObject = JObject.Parse(rawData);
                return jsonObject["ID"]?.ToString();
            }
            catch
            {
                return null;
            }
        }

        private string TryExtractDisplayNameFromApiData(string rawData, string email)
        {
            var fallbackName = !string.IsNullOrWhiteSpace(email) && email.Contains("@")
                ? email.Split('@')[0]
                : email;

            if (string.IsNullOrWhiteSpace(rawData))
            {
                return fallbackName;
            }

            try
            {
                var jsonObject = JObject.Parse(rawData);
                var candidateKeys = new[] { "Name", "HoTen", "FullName", "TenNguoiDung", "TenKH", "DisplayName" };
                foreach (var key in candidateKeys)
                {
                    var value = jsonObject[key]?.ToString();
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return value;
                    }
                }
            }
            catch
            {
                return fallbackName;
            }

            return fallbackName;
        }

        private ActionResult SignOutAndRedirectToLogin()
        {
            FormsAuthentication.SignOut();
            ClearAuthenticatedUserSession();

            Session.Clear();
            Session.Abandon();

            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                authCookie.Expires = DateTime.Now.AddDays(-1);
                authCookie.Value = string.Empty;
                Response.Cookies.Add(authCookie);
            }

            return RedirectToAction("Index", "Login");
        }

        // GET: Admin/Login
        public ActionResult Index()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "KhachHang" });
            }
            return View(new KhachHangLoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(KhachHangLoginViewModel model)
        {
            return await ExecuteLogin(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(KhachHangLoginViewModel model)
        {
            return await ExecuteLogin(model);
        }

        private async Task<ActionResult> ExecuteLogin(KhachHangLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginRequest = new DanhMucKhachHangDoiLenhLoginRequest
                {
                    Email = model.Email,
                    Password = model.Password
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetApiUrl("Login"));
                    var response = await client.PostAsJsonAsync(client.BaseAddress, loginRequest);

                    if (response.IsSuccessStatusCode)
                    {
                        webAPIresponse result = await response.Content.ReadAsAsync<webAPIresponse>();
                        if (result.Status == 0)
                        {
                            Console.WriteLine(result.ToString());
                            FormsAuthentication.SetAuthCookie(model.Email, false);
                            var userId = TryExtractIdFromApiData(result.Data);
                            StoreAuthenticatedUserSession(model.Email, userId, result.Data);
                            return RedirectToAction("Index", "Home", new { area = "KhachHang" });
                        }
                        else if (result.Status == 1)
                        {
                            ModelState.AddModelError("", result.ErrorMsg);
                        }

                        // Xử lý kết quả đăng nhập thành công, ví dụ: lưu thông tin người dùng vào session

                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin.");
                    }
                }
            }
            return View("Index", model);
        }
        // GET: Admin/Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(DanhMucKhachHangDoiLenhInsertRequest model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetApiUrl("Insert"));
                    var response = await client.PostAsJsonAsync(client.BaseAddress, model);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<webAPIresponse>();
                        if (result.Status == 0)

                        {
                            JObject jsonObject = JObject.Parse(result.Data);
                            string maKichHoat = jsonObject["MaKichHoat"].ToString();
                            string idKhachHang = jsonObject["ID"].ToString();

                            Session["ID"] = idKhachHang; // Hoặc TempData["ID"] = id;
                            try
                            {
                                // Tạo đối tượng email
                                var message = new MimeMessage();
                                message.From.Add(new MailboxAddress("CFS-GLC", "nmdat571.work@gmail.com")); // Thay thế bằng email của bạn
                                message.To.Add(new MailboxAddress("Recipient Name", model.Email)); // Thay thế bằng email người nhận cố định
                                message.Subject = "Mã xác nhận tài khoản của bạn"; // Tiêu đề email cố định
                                message.Body = new TextPart("plain") { Text = $"Mã xác nhận kích hoạt hệ thống GLC của bạn là: {maKichHoat}" }; // Nội dung email cố định


                                using (var client1 = new SmtpClient())
                                {

                                    client1.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                                    client1.Authenticate("nmdat571.work@gmail.com", "qszzyzblolxpnbhe"); // Thay thế bằng email và mật khẩu của bạn

                                    // Gửi email
                                    client1.Send(message);

                                    // Ngắt kết nối
                                    client1.Disconnect(true);
                                }

                                return RedirectToAction("ConfirmAccount", "Login");
                            }
                            catch (Exception ex)
                            {
                                // Ghi log lỗi chi tiết để debug
                                Console.WriteLine($"Lỗi: {ex.Message}"); // Hoặc sử dụng logger chuyên nghiệp
                                throw new Exception(ex.Message); // Trả về lỗi cho client
                            }

                            // Xử lý kết quả đăng ký thành công, ví dụ: chuyển hướng đến trang đăng nhập

                        }
                        else if (result.Status == 1)
                        {
                            ModelState.AddModelError("", result.ErrorMsg);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công. Vui lòng kiểm tra lại thông tin.");
                    }
                }
            }
            return View(model);
        }

        public ActionResult Success()
        {
            // Kiểm tra xem có thông báo thành công hay không
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            return View();
        }
        public ActionResult SuccessDoiMatKhau()
        {
            // Kiểm tra xem có thông báo thành công hay không
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            return View();
        }

        public ActionResult ConfirmAccount()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ConfirmAccount(DanhMucKhachHangDoiLenhKichHoatRequest model)
        {
            if (ModelState.IsValid)
            {
                string id = (string)Session["ID"]; // Hoặc long id = (long)TempData["ID"];
               

                // Gán ID và Email cho model
                model.ID = id;
               
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetApiUrl("UpdateKichHoat"));


                    var response = await client.PostAsJsonAsync(client.BaseAddress, model);

                    if (response.IsSuccessStatusCode)
                    {
                        webAPIresponse result = await response.Content.ReadAsAsync<webAPIresponse>();
                        if (result.Status == 0)
                        {
                            Console.WriteLine(result.ToString());
                            FormsAuthentication.SetAuthCookie(model.ID, false);
                            // 1. Hiển thị thông báo thành công
                            TempData["SuccessMessage"] = "Bạn đã đăng ký thành công!";

                            // 2. Chuyển hướng đến trang Success
                            return RedirectToAction("Success", "Login");


                        }
                        else if (result.Status == 1)
                        {
                            ModelState.AddModelError("", result.ErrorMsg);
                        }

                        // Xử lý kết quả đăng nhập thành công, ví dụ: lưu thông tin người dùng vào session

                    }
                    else
                    {
                        ModelState.AddModelError("", "Tạo tài khoản không thành công. Vui lòng kiểm tra lại thông tin.");
                    }
                }
            }
            return View(model);
        }
        public ActionResult getMaKichHoatByEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> getMaKichHoatByEmail(DanhMucKhachHangDoiLenhGetMaKichHoatByEmailRequest model)
        {
            if (ModelState.IsValid)
            {
                // First, get the ID based on the email
                using (var client = new HttpClient())
                {



                    // Now, proceed with the main request using the retrieved ID

                    client.BaseAddress = new Uri(GetApiUrl("getMaKichHoatByEmail"));
                    var response = await client.PostAsJsonAsync(client.BaseAddress, model);

                    if (response.IsSuccessStatusCode)
                    {
                        webAPIresponse result = await response.Content.ReadAsAsync<webAPIresponse>();
                        if (result.Status == 0)
                        {
                            JObject jsonObject = JObject.Parse(result.Data); // Changed variable name for clarity
                            string maKichHoat = jsonObject["MaKichHoat"]?.ToString();

                            // Store ID and MaKichHoat in TempData
                            string idKhachHang = jsonObject["ID"].ToString();
                           
                            Session["ID"] = idKhachHang; // Hoặc TempData["ID"] = id;
                         
                            //string idKhachHang = jsonObject["ID"].ToString();
                            //TempData["idKhachHang"] = idKhachHang;

                            try
                            {
                                var message = new MimeMessage();
                                message.From.Add(new MailboxAddress("CFS-GLC", "nmdat571.work@gmail.com")); // Replace with your email
                                message.To.Add(new MailboxAddress("Recipient Name", "kingbeeg123@gmail.com")); // Replace with recipient email
                                message.Subject = "Mã xác nhận tài khoản của bạn"; // Fixed email subject
                                message.Body = new TextPart("plain") { Text = $"Mã xác nhận của bạn là: {maKichHoat}" }; // Fixed email content

                                using (var client1 = new SmtpClient())
                                {
                                    client1.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                                    client1.Authenticate("nmdat571.work@gmail.com", "qszzyzblolxpnbhe"); // Replace with your email and password

                                    // Send email
                                    client1.Send(message);

                                    // Disconnect
                                    client1.Disconnect(true);
                                }

                                return RedirectToAction("ConfirmAccount", "Login");
                            }
                            catch (Exception ex)
                            {
                                // Log detailed error for debugging
                                Console.WriteLine($"Error: {ex.Message}"); // Or use a professional logger
                                throw new Exception(ex.Message); // Return error to client
                            }
                        }
                        else if (result.Status == 1)
                        {
                            ModelState.AddModelError("", result.ErrorMsg);
                        }

                        // Handle successful login result, e.g., save user info to session
                    }
                    else
                    {
                        ModelState.AddModelError("", "Account activation failed. Please check your information.");
                    }

                }


            }
            return View(model);
        }
        public ActionResult GetMaXacNhanByEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GetMaXacNhanByEmail(DanhMucKhachHangDoiLenhGetMaXacNhanByEmailRequest model)
        {
            if (ModelState.IsValid)
            {
                // First, get the ID based on the email
                using (var client = new HttpClient())
                {
                    // Now, proceed with the main request using the retrieved ID

                    client.BaseAddress = new Uri(GetApiUrl("getMaXacNhanByEmail"));
                    var response = await client.PostAsJsonAsync(client.BaseAddress, model);

                    if (response.IsSuccessStatusCode)
                    {
                        webAPIresponse result = await response.Content.ReadAsAsync<webAPIresponse>();
                        if (result.Status == 0)
                        {
                            JObject jsonObject = JObject.Parse(result.Data); // Changed variable name for clarity

                            string maXacNhan = jsonObject["MaXacNhan"]?.ToString();
                            string idKhachHang = jsonObject["ID"].ToString();
                            string emailKhachHang = jsonObject["Email"]?.ToString();
                            Session["ID"] = idKhachHang; // Hoặc TempData["ID"] = id;
                            Session["Email"] = emailKhachHang; // Hoặc TempData["Email"] = email;
                          
                            // Store ID and MaKichHoat in TempData
                            //string emailKhachHang = jsonObject["Email"]?.ToString();
                            //TempData["emailKhachHang"] = emailKhachHang;
                           
                            //TempData["idKhachHang"] = idKhachHang;

                            try
                            {
                                var message = new MimeMessage();
                                message.From.Add(new MailboxAddress("CFS-GLC", "nmdat571.work@gmail.com")); // Replace with your email
                                message.To.Add(new MailboxAddress("Recipient Name", "kingbeeg123@gmail.com")); // Replace with recipient email
                                message.Subject = "Mã xác nhận tài khoản của bạn"; // Fixed email subject
                                //message.Body = new TextPart("plain") { Text = $"Mã xác nhận của bạn là: {maXacNhan}" }; // Fixed email content
                                var bodyBuilder = new BodyBuilder();
                                bodyBuilder.TextBody = $"Mã xác nhận của bạn là: {maXacNhan}"; // Fixed email content

                                // Add the text file as an attachment
                                string filePath = @"C:\Users\Admin\Desktop\text1.txt"; // Replace with the actual file path
                                bodyBuilder.Attachments.Add(filePath);
                                message.Body = bodyBuilder.ToMessageBody();

                                using (var client1 = new SmtpClient())
                                {
                                    client1.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                                    client1.Authenticate("nmdat571.work@gmail.com", "qszzyzblolxpnbhe"); // Replace with your email and password

                                    // Send email
                                    client1.Send(message);

                                    // Disconnect
                                    client1.Disconnect(true);
                                }

                                return RedirectToAction("XacNhanDoiMatKhau", "Login");
                            }
                            catch (Exception ex)
                            {
                                // Log detailed error for debugging
                                Console.WriteLine($"Error: {ex.Message}"); // Or use a professional logger
                                throw new Exception(ex.Message); // Return error to client
                            }
                        }
                        else if (result.Status == 1)
                        {
                            ModelState.AddModelError("", result.ErrorMsg);
                        }
                       
                        // Handle successful login result, e.g., save user info to session
                    }
                    else
                    {
                        ModelState.AddModelError("", "Account activation failed. Please check your information.");
                    }

                }


            }
            

           
            
            return View(model);
        }
        public ActionResult XacNhanDoiMatKhau()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> XacNhanDoiMatKhau(DanhMucKhachHangDoiLenhXacNhanDoiMatKhauRequest model)
        {
            if (ModelState.IsValid)
            {
                // Lấy ID và Email từ Session (hoặc TempData)
                string id = (string)Session["ID"]; // Hoặc long id = (long)TempData["ID"];
                string email = (string)Session["Email"]; // Hoặc string email = (string)TempData["Email"];

                // Gán ID và Email cho model
                model.ID = id;
                model.Email = email;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetApiUrl("xacNhanDoiMatKhau"));


                    var response = await client.PostAsJsonAsync(client.BaseAddress, model);

                    if (response.IsSuccessStatusCode)
                    {
                        webAPIresponse result = await response.Content.ReadAsAsync<webAPIresponse>();
                        if (result.Status == 0)
                        {
                            JObject jsonObject = JObject.Parse(result.Data); // Changed variable name for clarity
                          
                            // Store ID and MaKichHoat in TempData

                          
                            Console.WriteLine(result.ToString());
                            FormsAuthentication.SetAuthCookie(model.ID, false);
                            // 1. Hiển thị thông báo thành công
                            TempData["SuccessMessage"] = "Bạn đã đăng ký thành công!";

                            // 2. Chuyển hướng đến trang Success
                            return RedirectToAction("SuccessDoiMatKhau", "Login");


                        }
                        else if (result.Status == 1)
                        {
                            ModelState.AddModelError("", result.ErrorMsg);
                        }

                        // Xử lý kết quả đăng nhập thành công, ví dụ: lưu thông tin người dùng vào session

                    }
                    else
                    {
                        ModelState.AddModelError("", "Tạo tài khoản không thành công. Vui lòng kiểm tra lại thông tin.");
                    }
                }
            }
         

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            return SignOutAndRedirectToLogin();
        }

        [HttpGet]
        public ActionResult Logout(string returnUrl = null)
        {
            return SignOutAndRedirectToLogin();
        }
    }
}
