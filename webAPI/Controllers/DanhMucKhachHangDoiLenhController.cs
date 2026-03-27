using coreDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Data;
using coreBUS;
using coreDTO;
using cenDTO;
using cenBUS;
using webAPI.Code;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using cenCommon;
using GlobalVariables = webAPI.Code.GlobalVariables;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using coreCommon;
using System.Configuration;
namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DanhMucKhachHangDoiLenhController : ApiController
    {
        [HttpGet]
        public webAPIresponse GetLoginCaptcha()
        {
            var response = new webAPIresponse();

            try
            {
                var captchaSecretKey = ConfigurationManager.AppSettings["CaptchaSecretKey"];
                if (string.IsNullOrWhiteSpace(captchaSecretKey))
                    throw new Exception("Thiếu cấu hình CaptchaSecretKey.");

                var captchaCode = CaptchaTokenHelper.GenerateCode();
                var captchaToken = CaptchaTokenHelper.CreateToken(captchaCode, captchaSecretKey);

                response.Status = 0;
                response.Data = JsonConvert.SerializeObject(new
                {
                    CaptchaDisplayText = captchaCode,
                    CaptchaToken = captchaToken
                });
                response.ErrorMsg = string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = string.Empty;
                response.ErrorMsg = ex.Message;
            }

            return response;
        }

        // GET: DanhMucKhachHangDoiLenh
        [HttpPost]
        public webAPIresponse List()
        {
            webAPIresponse response = new webAPIresponse();
            string ErrMsg = string.Empty;
            try
            {
                coreCommon.GlobalVariables.IDDonVi = Code.GlobalVariables.IDDanhMucDonVi;
                DataTable dt = DanhMucKhachHangDoiLenhBUS.List(GlobalVariables.ConnectionString, GlobalVariables.IDDanhMucDonVi, GlobalVariables.IDDanhMucKhachHangDoiLenh, null);

                List<DanhMucKhachHangDoiLenh> list = new List<DanhMucKhachHangDoiLenh>();

                foreach (DataRow dataRow in dt.Rows)
                {
                    list.Add(new DanhMucKhachHangDoiLenh()
                    {
                        ID = coreCommon.coreCommon.longParse(dataRow["ID"]),
                        IDDanhMucDonVi = coreCommon.coreCommon.longParse(dataRow["IDDanhMucDonVi"]),
                        IDDanhMucLoaiDoiTuong = coreCommon.coreCommon.longParse(dataRow["IDDanhMucLoaiDoiTuong"]),
                        Email = coreCommon.coreCommon.stringParse(dataRow["Email"]),
                        Ten = coreCommon.coreCommon.stringParse(dataRow["Ten"]),
                        SoDienThoai = coreCommon.coreCommon.stringParse(dataRow["SoDienThoai"]),
                        Password = coreCommon.coreCommon.stringParse(dataRow["Password"]),
                        PartnerGUID = coreCommon.coreCommon.stringParse(dataRow["PartnerGUID"]),
                        KichHoat = coreCommon.coreCommon.stringParse(dataRow["KichHoat"]),
                        IDDanhMucNguoiSuDungCreate = coreCommon.coreCommon.longParse(dataRow["IDDanhMucNguoiSuDungCreate"]),
                        IDDanhMucNguoiSuDungEdit = coreCommon.coreCommon.longParse(dataRow["IDDanhMucNguoiSuDungEdit"]),
                    });
                }
                if (ErrMsg == String.Empty)
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(list);
                    response.ErrorMsg = String.Empty;
                }
                else
                    throw new Exception(ErrMsg);

            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = String.Empty;
                response.ErrorMsg = ex.Message;
            }

            return response;
        }
        [HttpPost]
        public webAPIresponse Insert(DanhMucKhachHangDoiLenhInsertRequest objInsert)
        {
            webAPIresponse response = new webAPIresponse();
            string ErrMsg = string.Empty;
            try
            {
                coreCommon.GlobalVariables.IDDonVi = Code.GlobalVariables.IDDanhMucDonVi;

                string GUID = Guid.NewGuid().ToString();

                if (coreCommon.coreCommon.IsNull(objInsert.Email))
                    throw new Exception("Địa chỉ email không được bỏ trống!");

                //if (!new EmailAddressAttribute().IsValid(coreCommon.coreCommon.stringParse(objInsert.Email.ToString())))
                //    throw new Exception("Địa chỉ email không đúng định dạng!");

                DataTable dtDanhMucKhachHangDoiLenh = DanhMucKhachHangDoiLenhBUS.List(GlobalVariables.ConnectionString, GlobalVariables.IDDanhMucDonVi, GlobalVariables.IDDanhMucKhachHangDoiLenh, -1);
                DataRow drDanhMucKhachHangDoiLenh = dtDanhMucKhachHangDoiLenh.NewRow();
                drDanhMucKhachHangDoiLenh["ID"] = DBNull.Value;
                drDanhMucKhachHangDoiLenh["IDDanhMucDonVi"] = GlobalVariables.IDDanhMucDonVi;
                drDanhMucKhachHangDoiLenh["IDDanhMucLoaiDoiTuong"] = GlobalVariables.IDDanhMucKhachHangDoiLenh;
                drDanhMucKhachHangDoiLenh["Email"] = objInsert.Email;
                drDanhMucKhachHangDoiLenh["Ten"] = objInsert.Ten;
                drDanhMucKhachHangDoiLenh["SoDienThoai"] = objInsert.SoDienThoai;
                drDanhMucKhachHangDoiLenh["PartnerGUID"] = GUID;
                drDanhMucKhachHangDoiLenh["Password"] = coreCommon.coreCommon.EncryptString(objInsert.Password.ToString(), GUID);
                drDanhMucKhachHangDoiLenh["PasswordConfirm"] = coreCommon.coreCommon.EncryptString(objInsert.PasswordConfirm.ToString(), GUID);
                drDanhMucKhachHangDoiLenh["IDDanhMucNguoiSuDungCreate"] = GlobalVariables.IDDanhMucNguoiSuDungGuest;
                dtDanhMucKhachHangDoiLenh.Rows.Add(drDanhMucKhachHangDoiLenh);
                if (DanhMucKhachHangDoiLenhBUS.Insert(GlobalVariables.ConnectionString, drDanhMucKhachHangDoiLenh, out object IDDanhMucKhachHangDoiLenh, out string MaKichHoat))
                {
                    response.Status = 0;
                    var data = new
                    {
                        ID = IDDanhMucKhachHangDoiLenh,
                        MaKichHoat = MaKichHoat
                    };

                    // Chuyển đối tượng thành chuỗi JSON
                    response.Data = Newtonsoft.Json.JsonConvert.SerializeObject(data); // Yêu cầu Newtonsoft.Json

                    response.ErrorMsg = String.Empty;
                }
                else
                    throw new Exception(ErrMsg);

            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = String.Empty;
                response.ErrorMsg = ex.Message;
            }
            return response;
        }
        [HttpPost]
        public webAPIresponse Login(DanhMucKhachHangDoiLenhLoginRequest objLogin)
        {
            webAPIresponse response = new webAPIresponse();
            string ErrMsg = string.Empty;
            try
            {
                coreCommon.GlobalVariables.IDDonVi = GlobalVariables.IDDanhMucDonVi;
                var captchaSecretKey = ConfigurationManager.AppSettings["CaptchaSecretKey"];

                if (string.IsNullOrWhiteSpace(captchaSecretKey))
                    throw new Exception("Thiếu cấu hình CaptchaSecretKey.");

                if (coreCommon.coreCommon.IsNull(objLogin.CaptchaCode))
                    throw new Exception("Mã captcha không được bỏ trống.");

                if (coreCommon.coreCommon.IsNull(objLogin.CaptchaToken))
                    throw new Exception("Phiên captcha không hợp lệ.");

                if (!CaptchaTokenHelper.ValidateToken(objLogin.CaptchaCode, objLogin.CaptchaToken, captchaSecretKey))
                    throw new Exception("Mã captcha không đúng hoặc đã hết hạn.");

                //if (objLogin.Email.ToString().ToUpper().Trim() != "ADMIN@EVERLINK.COM.VN") throw new Exception("Hệ thống đang nâng cấp, mời bạn quay lại sau ít phút!");

                object PartnerGUID = DanhMucKhachHangDoiLenhBUS.GetPartnerGUIDByEmail(GlobalVariables.ConnectionString, GlobalVariables.IDDanhMucDonVi, GlobalVariables.IDDanhMucKhachHangDoiLenh, objLogin.Email);
                DataTable dtDanhMucKhachHangDoiLenh = DanhMucKhachHangDoiLenhBUS.ListLogin(GlobalVariables.ConnectionString, GlobalVariables.IDDanhMucDonVi, GlobalVariables.IDDanhMucKhachHangDoiLenh, objLogin.Email, coreCommon.coreCommon.EncryptString(coreCommon.coreCommon.stringParse(objLogin.Password), coreCommon.coreCommon.stringParse(PartnerGUID)));

                if (dtDanhMucKhachHangDoiLenh != null && dtDanhMucKhachHangDoiLenh.Rows.Count == 1)
                {
                    var x = new
                    {
                        ID = dtDanhMucKhachHangDoiLenh.Rows[0]["ID"].ToString(),
                        Email = dtDanhMucKhachHangDoiLenh.Rows[0]["Email"].ToString(),
                        Ten = dtDanhMucKhachHangDoiLenh.Rows[0]["Ten"].ToString(),
                        PartnerGUID = dtDanhMucKhachHangDoiLenh.Rows[0]["PartnerGUID"].ToString(),
                    };
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(x);
                    response.ErrorMsg = String.Empty;
                }
                else
                    throw new Exception("Email hoặc mật khẩu không đúng.");

            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = String.Empty;
                response.ErrorMsg = ex.Message;
            }
            return response;
        }
        [HttpPost]
        public webAPIresponse UpdateKichHoat(DanhMucKhachHangDoiLenhKichHoatRequest obj)
        {
            webAPIresponse response = new webAPIresponse();
            string ErrMsg = string.Empty;
            try
            {
                coreCommon.GlobalVariables.IDDonVi = GlobalVariables.IDDanhMucDonVi;
                bool OK = DanhMucKhachHangDoiLenhBUS.UpdateKichHoat(GlobalVariables.ConnectionString, obj.ID, obj.MaKichHoat);
                if (OK)
                {
                    response.Status = 0;
                    response.Data = "Kích hoạt tài khoản thành công";
                    response.ErrorMsg = String.Empty;
                }
                else
                    throw new Exception(ErrMsg);

            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = "Kích hoạt tài khoản không thành công, liên hệ 0707.126.126 để được hỗ trợ!";
                response.ErrorMsg = ex.Message;
            }
            return response;
        }

        [HttpPost]
        public webAPIresponse getMaKichHoatByEmail(DanhMucKhachHangDoiLenhGetMaKichHoatByEmailRequest obj)
        {
            webAPIresponse response = new webAPIresponse();
            string ErrMsg = string.Empty;
            try
            {

                coreCommon.GlobalVariables.IDDonVi = GlobalVariables.IDDanhMucDonVi;
                bool OK = DanhMucKhachHangDoiLenhBUS.getMaKichHoatByEmail(GlobalVariables.ConnectionString, obj.Email, out object ID, out object MaKichHoat);
                obj.ID = ID.ToString();
                obj.MaKichHoat = MaKichHoat.ToString();
                if (OK)
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(obj);
                    response.ErrorMsg = String.Empty;
                }
                else
                    throw new Exception(ErrMsg);

            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = "Kích hoạt tài khoản không thành công, liên hệ 0707.126.126 để được hỗ trợ!";
                response.ErrorMsg = ex.Message;
            }
            return response;
        }
        [HttpPost]
        public webAPIresponse getMaXacNhanByEmail(DanhMucKhachHangDoiLenhGetMaXacNhanByEmailRequest obj)
        {
            webAPIresponse response = new webAPIresponse();
            string ErrMsg = string.Empty;
            try
            {

                coreCommon.GlobalVariables.IDDonVi = GlobalVariables.IDDanhMucDonVi;
                bool OK = DanhMucKhachHangDoiLenhBUS.getMaXacNhanByEmail(GlobalVariables.ConnectionString, obj.Email, out object ID, out object MaXacNhan);
                obj.ID = ID.ToString();
                obj.MaXacNhan = MaXacNhan.ToString();
                if (OK)
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(obj);
                    response.ErrorMsg = String.Empty;
                }
                else
                    throw new Exception(ErrMsg);

            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = "Lỗi khi lấy mã xác nhận, liên hệ 0707.126.126 để được hỗ trợ!";
                response.ErrorMsg = ex.Message;
            }
            return response;
        }
        [HttpPost]
        public webAPIresponse xacNhanDoiMatKhau(DanhMucKhachHangDoiLenhXacNhanDoiMatKhauRequest obj)
        {
            webAPIresponse response = new webAPIresponse();
            string ErrMsg = string.Empty;

            try
            {
                object PartnerGUID = DanhMucKhachHangDoiLenhBUS.GetPartnerGUIDByEmail(GlobalVariables.ConnectionString, GlobalVariables.IDDanhMucDonVi, GlobalVariables.IDDanhMucKhachHangDoiLenh, obj.Email);

                coreCommon.GlobalVariables.IDDonVi = GlobalVariables.IDDanhMucDonVi;
                obj.MatKhauMoi = coreCommon.coreCommon.EncryptString(obj.MatKhauMoi.ToString(), PartnerGUID.ToString());
                obj.XacNhanMatKhauMoi = coreCommon.coreCommon.EncryptString(obj.XacNhanMatKhauMoi.ToString(), PartnerGUID.ToString());
                bool OK = DanhMucKhachHangDoiLenhBUS.xacNhanDoiMatKhau(GlobalVariables.ConnectionString, obj.MatKhauMoi, obj.XacNhanMatKhauMoi, obj.ID, obj.MaXacNhan);
                
                if (OK)
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(obj);
                    response.ErrorMsg = String.Empty;
                }
                else
                    throw new Exception(ErrMsg);

            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = "Lỗi khi lấy mã xác nhận, liên hệ 0707.126.126 để được hỗ trợ!";
                response.ErrorMsg = ex.Message;
            }
            return response;
        }
        [HttpPost]
        public webAPIresponse RecoverPassword(DanhMucKhachHangDoiLenhLoginRequest objLogin)
        {
            webAPIresponse response = new webAPIresponse();
            string ErrMsg = string.Empty;
            try
            {
                coreCommon.GlobalVariables.IDDonVi = GlobalVariables.IDDanhMucDonVi;
                long Password = coreDAO.ConnectionDAO.MaxAutoID(GlobalVariables.ConnectionString);
                object PartnerGUID = DanhMucKhachHangDoiLenhBUS.GetPartnerGUIDByEmail(GlobalVariables.ConnectionString, GlobalVariables.IDDanhMucDonVi, GlobalVariables.IDDanhMucKhachHangDoiLenh, objLogin.Email);
                bool OK = DanhMucKhachHangDoiLenhBUS.InsertRecoverPasswordLog(GlobalVariables.ConnectionString, GlobalVariables.IDDanhMucDonVi, objLogin.Email, coreCommon.coreCommon.EncryptString(coreCommon.coreCommon.stringParse(Password), coreCommon.coreCommon.stringParse(PartnerGUID)), GlobalVariables.IDDanhMucNguoiSuDungGuest);
                if (!coreCommon.coreCommon.IsNull(PartnerGUID))
                {
                    coreCommon.GlobalVariables.IDDonVi = Code.GlobalVariables.IDDanhMucDonVi;
                    //MyMailer.SendMail(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoEmail_MailAddress).ToString(), DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoEmail_MailPassword).ToString(), coreCommon.coreCommon.stringParse(objLogin.Email), "Cấp lại tài khoản G-Fortune", @"<h1>Xin chào, Password mới của bạn là <b>" + Password.ToString() + @"</b>!</h1>");
                    response.Status = 0;
                    response.Data = Password.ToString();
                    response.ErrorMsg = String.Empty;
                }
                else
                    throw new Exception(ErrMsg);

            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = String.Empty;
                response.ErrorMsg = ex.Message;
            }
            return response;
        }
        [HttpPost]
        public webAPIresponse UpdateChangePassword(DanhMucKhachHangDoiLenhChangePasswordRequest objInsert)
        {
            webAPIresponse response = new webAPIresponse();
            string ErrMsg = string.Empty;
            try
            {
                coreCommon.GlobalVariables.IDDonVi = Code.GlobalVariables.IDDanhMucDonVi;

                if (coreCommon.coreCommon.IsNull(objInsert.OldPassword)) throw new Exception("Mật khẩu cũ không được bỏ trống!");
                if (coreCommon.coreCommon.IsNull(objInsert.NewPassword)) throw new Exception("Mật khẩu mới không được bỏ trống!");
                if (coreCommon.coreCommon.IsNull(objInsert.NewPasswordConfirm)) throw new Exception("Mật khẩu mới nhập lại không được bỏ trống!");

                object PartnerGUID = DanhMucKhachHangDoiLenhBUS.GetPartnerGUIDByEmail(GlobalVariables.ConnectionString, GlobalVariables.IDDanhMucDonVi, GlobalVariables.IDDanhMucKhachHangDoiLenh, objInsert.Email);

                if (!coreCommon.coreCommon.IsNull(PartnerGUID))
                {
                    objInsert.OldPassword = coreCommon.coreCommon.EncryptString(objInsert.OldPassword.ToString(), PartnerGUID.ToString());
                    objInsert.NewPassword = coreCommon.coreCommon.EncryptString(objInsert.NewPassword.ToString(), PartnerGUID.ToString());
                    objInsert.NewPasswordConfirm = coreCommon.coreCommon.EncryptString(objInsert.NewPasswordConfirm.ToString(), PartnerGUID.ToString());

                    if (DanhMucKhachHangDoiLenhBUS.UpdatePassword(GlobalVariables.ConnectionString, objInsert.Email, objInsert.OldPassword, objInsert.NewPassword, objInsert.NewPasswordConfirm, GlobalVariables.IDDanhMucNguoiSuDungGuest))
                    {
                        response.Status = 0;
                        response.Data = String.Empty;
                        response.ErrorMsg = String.Empty;
                    }
                    else
                        throw new Exception(ErrMsg);
                }
                else
                    throw new Exception($"Không tìm thấy PartnerGUID\n{ErrMsg}");

            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = String.Empty;
                response.ErrorMsg = ex.Message;
            }
            return response;
        }
       
    } 
}
