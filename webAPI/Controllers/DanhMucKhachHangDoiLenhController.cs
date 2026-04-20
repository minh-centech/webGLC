using coreDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Data;
using coreBUS;
using cenDTO;
using cenBUS;
using webAPI.Code;
using System.Web.Http.Cors;
using cenCommon;
using GlobalVariables = webAPI.Code.GlobalVariables;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using coreCommon;
using System.Configuration;
using System.Data.SqlClient;
using webAPI.Models;
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
                        LoaiTaiKhoan = dataRow["LoaiTaiKhoan"],
                        IsActive = dataRow["IsActive"],
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
                drDanhMucKhachHangDoiLenh["LoaiTaiKhoan"] = objInsert.LoaiTaiKhoan;
                drDanhMucKhachHangDoiLenh["IsActive"] = objInsert.IsActive;
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
        public webAPIresponse RegisterTaiKhoan(DangKyTaiKhoanRequest model)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                coreCommon.GlobalVariables.IDDonVi = Code.GlobalVariables.IDDanhMucDonVi;
                NormalizeDangKyTaiKhoanRequest(model);
                model.IsActive = false;
                ValidateDangKyTaiKhoanRequest(model);

                string partnerGuid = Guid.NewGuid().ToString();
                string encryptedPassword = coreCommon.coreCommon.EncryptString(model.Password, partnerGuid);
                string maKichHoat = DanhMucKhachHangDoiLenhBUS.GenMaKichHoat(6);
                DateTime now = DateTime.Now;

                using (SqlConnection sqlConnection = new SqlConnection(GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction transaction = sqlConnection.BeginTransaction())
                    {
                        try
                        {
                            long customerId = InsertDanhMucDoiTuong(sqlConnection, transaction, now);
                            InsertDanhMucKhachHangDoiLenh(sqlConnection, transaction, customerId, model, partnerGuid, encryptedPassword, maKichHoat, now);

                            if (model.LoaiTaiKhoan == 2)
                            {
                                InsertNguoiDungDoanhNghiep(sqlConnection, transaction, customerId, model, now);
                            }

                            transaction.Commit();

                            response.Status = 0;
                            response.Data = JsonConvert.SerializeObject(new
                            {
                                ID = customerId,
                                MaKichHoat = maKichHoat
                            });
                            response.ErrorMsg = string.Empty;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = string.Empty;
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


                //if (objLogin.Email.ToString().ToUpper().Trim() != "ADMIN@EVERLINK.COM.VN") throw new Exception("Hệ thống đang nâng cấp, mời bạn quay lại sau ít phút!");

                object PartnerGUID = DanhMucKhachHangDoiLenhBUS.GetPartnerGUIDByEmail(GlobalVariables.ConnectionString, GlobalVariables.IDDanhMucDonVi, GlobalVariables.IDDanhMucKhachHangDoiLenh, objLogin.Email);
                DataTable dtDanhMucKhachHangDoiLenh = DanhMucKhachHangDoiLenhBUS.ListLogin(GlobalVariables.ConnectionString, GlobalVariables.IDDanhMucDonVi, GlobalVariables.IDDanhMucKhachHangDoiLenh, objLogin.Email, coreCommon.coreCommon.EncryptString(coreCommon.coreCommon.stringParse(objLogin.Password), coreCommon.coreCommon.stringParse(PartnerGUID)));

                if (dtDanhMucKhachHangDoiLenh != null && dtDanhMucKhachHangDoiLenh.Rows.Count == 1)
                {
                    var x = new
                    {
                        ID = dtDanhMucKhachHangDoiLenh.Rows[0]["ID"].ToString(),
                        LoaiTaiKhoan = dtDanhMucKhachHangDoiLenh.Rows[0]["LoaiTaiKhoan"].ToString(),
                        IsActive = dtDanhMucKhachHangDoiLenh.Rows[0]["IsActive"].ToString(),
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

        [HttpPost]
        public webAPIresponse SetActive(DanhMucKhachHangDoiLenhSetActiveRequest request)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                coreCommon.GlobalVariables.IDDonVi = Code.GlobalVariables.IDDanhMucDonVi;

                if (request == null || coreCommon.coreCommon.IsNull(request.ID))
                {
                    throw new Exception("Không tìm thấy tài khoản cần cập nhật.");
                }

                DataTable dtDanhMucKhachHangDoiLenh = DanhMucKhachHangDoiLenhBUS.List(
                    GlobalVariables.ConnectionString,
                    GlobalVariables.IDDanhMucDonVi,
                    GlobalVariables.IDDanhMucKhachHangDoiLenh,
                    request.ID);

                if (dtDanhMucKhachHangDoiLenh == null || dtDanhMucKhachHangDoiLenh.Rows.Count != 1)
                {
                    throw new Exception("Tài khoản không tồn tại hoặc đã bị xóa.");
                }

                DataRow drDanhMucKhachHangDoiLenh = dtDanhMucKhachHangDoiLenh.Rows[0];
                drDanhMucKhachHangDoiLenh["IsActive"] = request.IsActive;
                drDanhMucKhachHangDoiLenh["IDDanhMucNguoiSuDungEdit"] = GlobalVariables.IDDanhMucNguoiSuDungGuest;

                if (DanhMucKhachHangDoiLenhBUS.Update(GlobalVariables.ConnectionString, drDanhMucKhachHangDoiLenh, out object updatedId))
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(new
                    {
                        ID = updatedId ?? request.ID,
                        IsActive = request.IsActive
                    });
                    response.ErrorMsg = string.Empty;
                }
                else
                {
                    throw new Exception("Cập nhật trạng thái tài khoản không thành công.");
                }
            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = string.Empty;
                response.ErrorMsg = ex.Message;
            }

            return response;
        }

                [HttpPost]
        public webAPIresponse SaveThongTinCaNhan(DanhMucKhachHangDoiLenhSavePersonalProfileRequest request)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                coreCommon.GlobalVariables.IDDonVi = Code.GlobalVariables.IDDanhMucDonVi;

                if (request == null || coreCommon.coreCommon.IsNull(request.ID))
                {
                    throw new Exception("Khong tim thay tai khoan can cap nhat.");
                }

                long customerId;
                if (!long.TryParse(request.ID.ToString(), out customerId) || customerId <= 0)
                {
                    throw new Exception("Ma tai khoan khong hop le.");
                }

                if (string.IsNullOrWhiteSpace(request.Ten))
                {
                    throw new Exception("Ho va ten khong duoc bo trong.");
                }

                if (string.IsNullOrWhiteSpace(request.SoDienThoai))
                {
                    throw new Exception("So dien thoai khong duoc bo trong.");
                }

                const string sql = @"
                    update DanhMucKhachHangDoiLenh
                    set
                        Ten = @Ten,
                        SoDienThoai = @SoDienThoai,
                        EmailXuatHoaDon = @EmailXuatHoaDon,
                        IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
                        EditDate = @EditDate
                    where IDDanhMucDonVi = @IDDanhMucDonVi
                        and IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong
                        and ID = @ID";

                int affectedRows;
                using (SqlConnection sqlConnection = new SqlConnection(GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Ten", request.Ten.Trim());
                        command.Parameters.AddWithValue("@SoDienThoai", request.SoDienThoai.Trim());
                        command.Parameters.AddWithValue("@EmailXuatHoaDon", (object)(string.IsNullOrWhiteSpace(request.EmailXuatHoaDon) ? null : request.EmailXuatHoaDon.Trim()) ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IDDanhMucNguoiSuDungEdit", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucNguoiSuDungGuest));
                        command.Parameters.AddWithValue("@EditDate", DateTime.Now);
                        command.Parameters.AddWithValue("@IDDanhMucDonVi", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucDonVi));
                        command.Parameters.AddWithValue("@IDDanhMucLoaiDoiTuong", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucKhachHangDoiLenh));
                        command.Parameters.AddWithValue("@ID", customerId);

                        affectedRows = command.ExecuteNonQuery();
                    }
                }

                if (affectedRows <= 0)
                {
                    throw new Exception("Tai khoan khong ton tai hoac da bi xoa.");
                }

                response.Status = 0;
                response.Data = JsonConvert.SerializeObject(new
                {
                    ID = customerId
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

        [HttpPost]
        public webAPIresponse SaveTaiLieuCaNhan(DanhMucKhachHangDoiLenhSavePersonalDocumentsRequest request)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                coreCommon.GlobalVariables.IDDonVi = Code.GlobalVariables.IDDanhMucDonVi;

                if (request == null || coreCommon.coreCommon.IsNull(request.ID))
                {
                    throw new Exception("Khong tim thay tai khoan can cap nhat.");
                }

                long customerId;
                if (!long.TryParse(request.ID.ToString(), out customerId) || customerId <= 0)
                {
                    throw new Exception("Ma tai khoan khong hop le.");
                }

                const string sql = @"
                    update DanhMucKhachHangDoiLenh
                    set
                        BanScanSoCMNDCanCuocPath = @BanScanSoCMNDCanCuocPath,
                        BanDangKyCaNhanCoChuKyPath = @BanDangKyCaNhanCoChuKyPath,
                        IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
                        EditDate = @EditDate
                    where IDDanhMucDonVi = @IDDanhMucDonVi
                        and IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong
                        and ID = @ID";

                int affectedRows;
                string citizenCardPath = string.IsNullOrWhiteSpace(request.BanScanSoCMNDCanCuocPath)
                    ? request.BanScanSoCMNDCanCuocPathCaNhan
                    : request.BanScanSoCMNDCanCuocPath;
                using (SqlConnection sqlConnection = new SqlConnection(GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@BanScanSoCMNDCanCuocPath", (object)(string.IsNullOrWhiteSpace(citizenCardPath) ? null : citizenCardPath.Trim()) ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BanDangKyCaNhanCoChuKyPath", (object)(string.IsNullOrWhiteSpace(request.BanDangKyCaNhanCoChuKyPath) ? null : request.BanDangKyCaNhanCoChuKyPath.Trim()) ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IDDanhMucNguoiSuDungEdit", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucNguoiSuDungGuest));
                        command.Parameters.AddWithValue("@EditDate", DateTime.Now);
                        command.Parameters.AddWithValue("@IDDanhMucDonVi", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucDonVi));
                        command.Parameters.AddWithValue("@IDDanhMucLoaiDoiTuong", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucKhachHangDoiLenh));
                        command.Parameters.AddWithValue("@ID", customerId);
                        affectedRows = command.ExecuteNonQuery();
                    }
                }

                if (affectedRows <= 0)
                {
                    throw new Exception("Tai khoan khong ton tai hoac da bi xoa.");
                }

                response.Status = 0;
                response.Data = JsonConvert.SerializeObject(new
                {
                    ID = customerId
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

        [HttpGet]
        public webAPIresponse GetHoSoDoanhNghiep(string id)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                coreCommon.GlobalVariables.IDDonVi = Code.GlobalVariables.IDDanhMucDonVi;

                long customerId;
                if (!long.TryParse(id, out customerId) || customerId <= 0)
                {
                    throw new Exception("Mã tài khoản không hợp lệ.");
                }

                const string sql = @"
                    select top 1
                        kh.ID,
                        kh.Ten,
                        kh.Email,
                        kh.SoDienThoai,
                        kh.LoaiTaiKhoan,
                        kh.IsActive,
                        kh.KichHoat,
                        kh.BanScanSoCMNDCanCuocPath as BanScanSoCMNDCanCuocPathCaNhan,
                        kh.BanDangKyCaNhanCoChuKyPath,
                        kh.EmailXuatHoaDon,
                        dn.TenDoanhNghiep,
                        dn.MaSoThue,
                        dn.EmailDoanhNghiep,
                        dn.BanScanGiayPhepKinhDoanhPath,
                        dn.BanScanSoCMNDCanCuocPath,
                        dn.BanDangKyEPortChuKySoPath
                    from DanhMucKhachHangDoiLenh kh
                    left join NguoiDungDoanhNghiep dn on dn.IDDanhMucKhachHangDoiLenh = kh.ID
                    where kh.IDDanhMucDonVi = @IDDanhMucDonVi
                        and kh.IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong
                        and kh.ID = @ID";

                using (SqlConnection sqlConnection = new SqlConnection(GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@IDDanhMucDonVi", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucDonVi));
                        command.Parameters.AddWithValue("@IDDanhMucLoaiDoiTuong", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucKhachHangDoiLenh));
                        command.Parameters.AddWithValue("@ID", customerId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                throw new Exception("Không tìm thấy tài khoản cần xem hồ sơ.");
                            }

                            response.Status = 0;
                            response.Data = JsonConvert.SerializeObject(new
                            {
                                ID = reader["ID"].ToString(),
                                Ten = reader["Ten"].ToString(),
                                Email = reader["Email"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                LoaiTaiKhoan = reader["LoaiTaiKhoan"].ToString(),
                                IsActive = reader["IsActive"].ToString(),
                                KichHoat = reader["KichHoat"].ToString(),
                                TenDoanhNghiep = reader["TenDoanhNghiep"] == DBNull.Value ? null : reader["TenDoanhNghiep"].ToString(),
                                MaSoThue = reader["MaSoThue"] == DBNull.Value ? null : reader["MaSoThue"].ToString(),
                                EmailDoanhNghiep = reader["EmailDoanhNghiep"] == DBNull.Value ? null : reader["EmailDoanhNghiep"].ToString(),
                                BanScanSoCMNDCanCuocPathCaNhan = reader["BanScanSoCMNDCanCuocPathCaNhan"] == DBNull.Value ? null : reader["BanScanSoCMNDCanCuocPathCaNhan"].ToString(),
                                BanDangKyCaNhanCoChuKyPath = reader["BanDangKyCaNhanCoChuKyPath"] == DBNull.Value ? null : reader["BanDangKyCaNhanCoChuKyPath"].ToString(),
                                EmailXuatHoaDon = reader["EmailXuatHoaDon"] == DBNull.Value ? null : reader["EmailXuatHoaDon"].ToString(),
                                BanScanGiayPhepKinhDoanhPath = reader["BanScanGiayPhepKinhDoanhPath"] == DBNull.Value ? null : reader["BanScanGiayPhepKinhDoanhPath"].ToString(),
                                BanScanSoCMNDCanCuocPath = reader["BanScanSoCMNDCanCuocPath"] == DBNull.Value ? null : reader["BanScanSoCMNDCanCuocPath"].ToString(),
                                BanDangKyEPortChuKySoPath = reader["BanDangKyEPortChuKySoPath"] == DBNull.Value ? null : reader["BanDangKyEPortChuKySoPath"].ToString()
                            });
                            response.ErrorMsg = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = string.Empty;
                response.ErrorMsg = ex.Message;
            }

            return response;
        }

        [HttpGet]
        public webAPIresponse ListDoanhNghiepByKhachHang(string khachHangId)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                coreCommon.GlobalVariables.IDDonVi = Code.GlobalVariables.IDDanhMucDonVi;

                long customerId;
                if (!long.TryParse(khachHangId, out customerId) || customerId <= 0)
                {
                    throw new Exception("Mã khách hàng không hợp lệ.");
                }

                const string sql = @"
                    select
                        ID,
                        IDDanhMucKhachHangDoiLenh,
                        TenDoanhNghiep,
                        MaSoThue,
                        DiaChi,
                        SoDienThoaiDoanhNghiep,
                        EmailDoanhNghiep,
                        SoFax,
                        GiayPhepKinhDoanh,
                        BanScanGiayPhepKinhDoanhPath,
                        NgayCap,
                        NoiCap,
                        DaiDienCoThamQuyen,
                        ChucVu,
                        DoanhNghiepCongTyDuocUyQuyen,
                        TenDangNhapDangKyDichVu,
                        EmailXuatHoaDon,
                        SoCMNDCanCuoc,
                        BanScanSoCMNDCanCuocPath,
                        BanDangKyEPortChuKySoPath,
                        IsActive,
                        CreateDate,
                        EditDate
                    from NguoiDungDoanhNghiep
                    where IDDanhMucKhachHangDoiLenh = @IDDanhMucKhachHangDoiLenh
                    order by CreateDate desc, ID desc";

                var list = new List<NguoiDungDoanhNghiepDto>();
                using (SqlConnection sqlConnection = new SqlConnection(GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@IDDanhMucKhachHangDoiLenh", customerId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(MapNguoiDungDoanhNghiep(reader));
                            }
                        }
                    }
                }

                response.Status = 0;
                response.Data = JsonConvert.SerializeObject(list);
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

        [HttpGet]
        public webAPIresponse GetDoanhNghiep(string id)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                long companyId;
                if (!long.TryParse(id, out companyId) || companyId <= 0)
                {
                    throw new Exception("Mã doanh nghiệp không hợp lệ.");
                }

                const string sql = @"
                    select top 1
                        ID,
                        IDDanhMucKhachHangDoiLenh,
                        TenDoanhNghiep,
                        MaSoThue,
                        DiaChi,
                        SoDienThoaiDoanhNghiep,
                        EmailDoanhNghiep,
                        SoFax,
                        GiayPhepKinhDoanh,
                        BanScanGiayPhepKinhDoanhPath,
                        NgayCap,
                        NoiCap,
                        DaiDienCoThamQuyen,
                        ChucVu,
                        DoanhNghiepCongTyDuocUyQuyen,
                        TenDangNhapDangKyDichVu,
                        EmailXuatHoaDon,
                        SoCMNDCanCuoc,
                        BanScanSoCMNDCanCuocPath,
                        BanDangKyEPortChuKySoPath,
                        IsActive,
                        CreateDate,
                        EditDate
                    from NguoiDungDoanhNghiep
                    where ID = @ID";

                NguoiDungDoanhNghiepDto dto = null;
                using (SqlConnection sqlConnection = new SqlConnection(GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@ID", companyId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dto = MapNguoiDungDoanhNghiep(reader);
                            }
                        }
                    }
                }

                if (dto == null)
                {
                    throw new Exception("Không tìm thấy doanh nghiệp.");
                }

                response.Status = 0;
                response.Data = JsonConvert.SerializeObject(dto);
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

        [HttpPost]
        public webAPIresponse SaveDoanhNghiep(NguoiDungDoanhNghiepSaveRequest model)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                NormalizeNguoiDungDoanhNghiep(model);
                ValidateNguoiDungDoanhNghiep(model);

                using (SqlConnection sqlConnection = new SqlConnection(GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction transaction = sqlConnection.BeginTransaction())
                    {
                        try
                        {
                            long companyId = model.ID.HasValue && model.ID.Value > 0
                                ? UpdateNguoiDungDoanhNghiep(sqlConnection, transaction, model)
                                : InsertNguoiDungDoanhNghiepRecord(sqlConnection, transaction, model);

                            transaction.Commit();
                            response.Status = 0;
                            response.Data = JsonConvert.SerializeObject(new { ID = companyId });
                            response.ErrorMsg = string.Empty;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = string.Empty;
                response.ErrorMsg = ex.Message;
            }

            return response;
        }

        private void NormalizeDangKyTaiKhoanRequest(DangKyTaiKhoanRequest model)
        {
            if (model == null)
            {
                throw new Exception("Dữ liệu đăng ký không hợp lệ.");
            }

            model.Email = NormalizeText(model.Email);
            model.TenDangNhap = NormalizeText(model.TenDangNhap);
            model.Ten = NormalizeText(model.Ten);
            model.SoDienThoai = NormalizeText(model.SoDienThoai);
            model.Password = model.Password?.Trim();
            model.PasswordConfirm = model.PasswordConfirm?.Trim();
            model.EmailXuatHoaDon = NormalizeText(model.EmailXuatHoaDon);
            model.SoCMNDCanCuoc = NormalizeText(model.SoCMNDCanCuoc);
            model.BanScanSoCMNDCanCuocPath = NormalizeText(model.BanScanSoCMNDCanCuocPath);
            model.BanDangKyCaNhanCoChuKyPath = NormalizeText(model.BanDangKyCaNhanCoChuKyPath);
            model.TenDoanhNghiep = NormalizeText(model.TenDoanhNghiep);
            model.MaSoThue = NormalizeText(model.MaSoThue);
            model.DiaChi = NormalizeText(model.DiaChi);
            model.SoDienThoaiDoanhNghiep = NormalizeText(model.SoDienThoaiDoanhNghiep);
            model.EmailDoanhNghiep = NormalizeText(model.EmailDoanhNghiep);
            model.SoFax = NormalizeText(model.SoFax);
            model.GiayPhepKinhDoanh = NormalizeText(model.GiayPhepKinhDoanh);
            model.NoiCap = NormalizeText(model.NoiCap);
            model.DaiDienCoThamQuyen = NormalizeText(model.DaiDienCoThamQuyen);
            model.ChucVu = NormalizeText(model.ChucVu);
            model.DoanhNghiepCongTyDuocUyQuyen = NormalizeText(model.DoanhNghiepCongTyDuocUyQuyen);
            model.BanScanGiayPhepKinhDoanhPath = NormalizeText(model.BanScanGiayPhepKinhDoanhPath);
            model.BanDangKyEPortChuKySoPath = NormalizeText(model.BanDangKyEPortChuKySoPath);

            if (string.IsNullOrWhiteSpace(model.TenDangNhap))
            {
                model.TenDangNhap = model.Email;
            }
        }

        private void ValidateDangKyTaiKhoanRequest(DangKyTaiKhoanRequest model)
        {
            if (model.LoaiTaiKhoan != 1 && model.LoaiTaiKhoan != 2 && model.LoaiTaiKhoan != 0)
            {
                throw new Exception("Loại tài khoản không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                throw new Exception("Email đăng nhập không được bỏ trống.");
            }

            if (string.IsNullOrWhiteSpace(model.Ten))
            {
                throw new Exception("Họ và tên không được bỏ trống.");
            }

            if (string.IsNullOrWhiteSpace(model.SoDienThoai))
            {
                throw new Exception("Số điện thoại không được bỏ trống.");
            }

            if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.PasswordConfirm))
            {
                throw new Exception("Mật khẩu và xác nhận mật khẩu không được bỏ trống.");
            }

            if (!string.Equals(model.Password, model.PasswordConfirm, StringComparison.Ordinal))
            {
                throw new Exception("Mật khẩu và xác nhận mật khẩu không khớp.");
            }

            if (model.LoaiTaiKhoan == 2)
            {
                if (string.IsNullOrWhiteSpace(model.TenDoanhNghiep))
                {
                    throw new Exception("Tên doanh nghiệp không được bỏ trống.");
                }

                if (string.IsNullOrWhiteSpace(model.DiaChi))
                {
                    throw new Exception("Địa chỉ doanh nghiệp không được bỏ trống.");
                }

                if (string.IsNullOrWhiteSpace(model.MaSoThue))
                {
                    throw new Exception("Mã số thuế không được bỏ trống.");
                }

                if (string.IsNullOrWhiteSpace(model.SoDienThoaiDoanhNghiep))
                {
                    throw new Exception("Số điện thoại doanh nghiệp không được bỏ trống.");
                }

                if (string.IsNullOrWhiteSpace(model.EmailDoanhNghiep))
                {
                    throw new Exception("Email doanh nghiệp không được bỏ trống.");
                }
            }
        }

        private long InsertDanhMucDoiTuong(SqlConnection sqlConnection, SqlTransaction transaction, DateTime createDate)
        {
            using (SqlCommand command = new SqlCommand("Insert_DanhMucDoiTuong", sqlConnection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IDDanhMucDonVi", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucDonVi));
                command.Parameters.AddWithValue("@IDDanhMucLoaiDoiTuong", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucKhachHangDoiLenh));
                command.Parameters.AddWithValue("@Ma", DBNull.Value);
                command.Parameters.AddWithValue("@Ten", DBNull.Value);
                command.Parameters.AddWithValue("@IDDanhMucNguoiSuDungCreate", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucNguoiSuDungGuest));

                SqlParameter idParameter = new SqlParameter("@ID", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.InputOutput,
                    Value = DBNull.Value
                };
                SqlParameter createDateParameter = new SqlParameter("@CreateDate", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.InputOutput,
                    Value = createDate
                };

                command.Parameters.Add(idParameter);
                command.Parameters.Add(createDateParameter);
                command.ExecuteNonQuery();

                return Convert.ToInt64(idParameter.Value);
            }
        }

        private void InsertDanhMucKhachHangDoiLenh(SqlConnection sqlConnection, SqlTransaction transaction, long customerId, DangKyTaiKhoanRequest model, string partnerGuid, string encryptedPassword, string maKichHoat, DateTime createDate)
        {
            const string insertSql = @"
                insert into DanhMucKhachHangDoiLenh
                (
                    ID,
                    IDDanhMucDonVi,
                    IDDanhMucLoaiDoiTuong,
                    LoaiTaiKhoan,
                    IsActive,
                    Email,
                    Ten,
                    SoDienThoai,
                    BanScanSoCMNDCanCuocPath,
                    BanDangKyCaNhanCoChuKyPath,
                    [Password],
                    PartnerGUID,
                    MaKichHoat,
                    ThoiGianTaoMaKichHoat,
                    KichHoat,
                    IDDanhMucNguoiSuDungCreate,
                    CreateDate
                )
                values
                (
                    @ID,
                    @IDDanhMucDonVi,
                    @IDDanhMucLoaiDoiTuong,
                    @LoaiTaiKhoan,
                    @IsActive,
                    @Email,
                    @Ten,
                    @SoDienThoai,
                    @BanScanSoCMNDCanCuocPath,
                    @BanDangKyCaNhanCoChuKyPath,
                    @Password,
                    @PartnerGUID,
                    @MaKichHoat,
                    @ThoiGianTaoMaKichHoat,
                    0,
                    @IDDanhMucNguoiSuDungCreate,
                    @CreateDate
                )";

            using (SqlCommand command = new SqlCommand(insertSql, sqlConnection, transaction))
            {
                command.Parameters.AddWithValue("@ID", customerId);
                command.Parameters.AddWithValue("@IDDanhMucDonVi", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucDonVi));
                command.Parameters.AddWithValue("@IDDanhMucLoaiDoiTuong", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucKhachHangDoiLenh));
                command.Parameters.AddWithValue("@LoaiTaiKhoan", model.LoaiTaiKhoan);
                command.Parameters.AddWithValue("@IsActive", model.IsActive);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Ten", model.Ten);
                command.Parameters.AddWithValue("@SoDienThoai", model.SoDienThoai);
                command.Parameters.AddWithValue("@BanScanSoCMNDCanCuocPath", (object)model.BanScanSoCMNDCanCuocPath ?? DBNull.Value);
                command.Parameters.AddWithValue("@BanDangKyCaNhanCoChuKyPath", (object)model.BanDangKyCaNhanCoChuKyPath ?? DBNull.Value);
                command.Parameters.AddWithValue("@Password", encryptedPassword);
                command.Parameters.AddWithValue("@PartnerGUID", partnerGuid);
                command.Parameters.AddWithValue("@MaKichHoat", maKichHoat);
                command.Parameters.AddWithValue("@ThoiGianTaoMaKichHoat", createDate);
                command.Parameters.AddWithValue("@IDDanhMucNguoiSuDungCreate", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucNguoiSuDungGuest));
                command.Parameters.AddWithValue("@CreateDate", createDate);
                command.ExecuteNonQuery();
            }
        }

        private void InsertNguoiDungDoanhNghiep(SqlConnection sqlConnection, SqlTransaction transaction, long customerId, DangKyTaiKhoanRequest model, DateTime createDate)
        {
            long companyId;
            using (SqlCommand identityCommand = new SqlCommand("select isnull(max(ID), 0) + 1 from NguoiDungDoanhNghiep with (updlock, holdlock)", sqlConnection, transaction))
            {
                companyId = Convert.ToInt64(identityCommand.ExecuteScalar());
            }

            const string insertSql = @"
                insert into NguoiDungDoanhNghiep
                (
                    ID,
                    IDDanhMucKhachHangDoiLenh,
                    TenDoanhNghiep,
                    MaSoThue,
                    DiaChi,
                    SoDienThoaiDoanhNghiep,
                    EmailDoanhNghiep,
                    SoFax,
                    GiayPhepKinhDoanh,
                    BanScanGiayPhepKinhDoanhPath,
                    NgayCap,
                    NoiCap,
                    DaiDienCoThamQuyen,
                    ChucVu,
                    DoanhNghiepCongTyDuocUyQuyen,
                    TenDangNhapDangKyDichVu,
                    EmailXuatHoaDon,
                    SoCMNDCanCuoc,
                    BanScanSoCMNDCanCuocPath,
                    BanDangKyEPortChuKySoPath,
                    IsActive,
                    IDDanhMucNguoiSuDungCreate,
                    CreateDate
                )
                values
                (
                    @ID,
                    @IDDanhMucKhachHangDoiLenh,
                    @TenDoanhNghiep,
                    @MaSoThue,
                    @DiaChi,
                    @SoDienThoaiDoanhNghiep,
                    @EmailDoanhNghiep,
                    @SoFax,
                    @GiayPhepKinhDoanh,
                    @BanScanGiayPhepKinhDoanhPath,
                    @NgayCap,
                    @NoiCap,
                    @DaiDienCoThamQuyen,
                    @ChucVu,
                    @DoanhNghiepCongTyDuocUyQuyen,
                    @TenDangNhapDangKyDichVu,
                    @EmailXuatHoaDon,
                    @SoCMNDCanCuoc,
                    @BanScanSoCMNDCanCuocPath,
                    @BanDangKyEPortChuKySoPath,
                    1,
                    @IDDanhMucNguoiSuDungCreate,
                    @CreateDate
                )";

            using (SqlCommand command = new SqlCommand(insertSql, sqlConnection, transaction))
            {
                command.Parameters.AddWithValue("@ID", companyId);
                command.Parameters.AddWithValue("@IDDanhMucKhachHangDoiLenh", customerId);
                command.Parameters.AddWithValue("@TenDoanhNghiep", (object)model.TenDoanhNghiep ?? DBNull.Value);
                command.Parameters.AddWithValue("@MaSoThue", (object)model.MaSoThue ?? DBNull.Value);
                command.Parameters.AddWithValue("@DiaChi", (object)model.DiaChi ?? DBNull.Value);
                command.Parameters.AddWithValue("@SoDienThoaiDoanhNghiep", (object)model.SoDienThoaiDoanhNghiep ?? DBNull.Value);
                command.Parameters.AddWithValue("@EmailDoanhNghiep", (object)model.EmailDoanhNghiep ?? DBNull.Value);
                command.Parameters.AddWithValue("@SoFax", (object)model.SoFax ?? DBNull.Value);
                command.Parameters.AddWithValue("@GiayPhepKinhDoanh", (object)model.GiayPhepKinhDoanh ?? DBNull.Value);
                command.Parameters.AddWithValue("@BanScanGiayPhepKinhDoanhPath", (object)model.BanScanGiayPhepKinhDoanhPath ?? DBNull.Value);
                command.Parameters.AddWithValue("@NgayCap", (object)model.NgayCap ?? DBNull.Value);
                command.Parameters.AddWithValue("@NoiCap", (object)model.NoiCap ?? DBNull.Value);
                command.Parameters.AddWithValue("@DaiDienCoThamQuyen", (object)model.DaiDienCoThamQuyen ?? DBNull.Value);
                command.Parameters.AddWithValue("@ChucVu", (object)model.ChucVu ?? DBNull.Value);
                command.Parameters.AddWithValue("@DoanhNghiepCongTyDuocUyQuyen", (object)model.DoanhNghiepCongTyDuocUyQuyen ?? DBNull.Value);
                command.Parameters.AddWithValue("@TenDangNhapDangKyDichVu", (object)model.TenDangNhap ?? DBNull.Value);
                command.Parameters.AddWithValue("@EmailXuatHoaDon", (object)model.EmailXuatHoaDon ?? DBNull.Value);
                command.Parameters.AddWithValue("@SoCMNDCanCuoc", (object)model.SoCMNDCanCuoc ?? DBNull.Value);
                command.Parameters.AddWithValue("@BanScanSoCMNDCanCuocPath", (object)model.BanScanSoCMNDCanCuocPath ?? DBNull.Value);
                command.Parameters.AddWithValue("@BanDangKyEPortChuKySoPath", (object)model.BanDangKyEPortChuKySoPath ?? DBNull.Value);
                command.Parameters.AddWithValue("@IDDanhMucNguoiSuDungCreate", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucNguoiSuDungGuest));
                command.Parameters.AddWithValue("@CreateDate", createDate);
                command.ExecuteNonQuery();
            }
        }

        private string NormalizeText(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
        }

        private void NormalizeNguoiDungDoanhNghiep(NguoiDungDoanhNghiepSaveRequest model)
        {
            if (model == null)
            {
                throw new Exception("Dữ liệu doanh nghiệp không hợp lệ.");
            }

            model.TenDoanhNghiep = NormalizeText(model.TenDoanhNghiep);
            model.MaSoThue = NormalizeText(model.MaSoThue);
            model.DiaChi = NormalizeText(model.DiaChi);
            model.SoDienThoaiDoanhNghiep = NormalizeText(model.SoDienThoaiDoanhNghiep);
            model.EmailDoanhNghiep = NormalizeText(model.EmailDoanhNghiep);
            model.SoFax = NormalizeText(model.SoFax);
            model.GiayPhepKinhDoanh = NormalizeText(model.GiayPhepKinhDoanh);
            model.BanScanGiayPhepKinhDoanhPath = NormalizeText(model.BanScanGiayPhepKinhDoanhPath);
            model.NoiCap = NormalizeText(model.NoiCap);
            model.DaiDienCoThamQuyen = NormalizeText(model.DaiDienCoThamQuyen);
            model.ChucVu = NormalizeText(model.ChucVu);
            model.DoanhNghiepCongTyDuocUyQuyen = NormalizeText(model.DoanhNghiepCongTyDuocUyQuyen);
            model.TenDangNhapDangKyDichVu = NormalizeText(model.TenDangNhapDangKyDichVu);
            model.EmailXuatHoaDon = NormalizeText(model.EmailXuatHoaDon);
            model.SoCMNDCanCuoc = NormalizeText(model.SoCMNDCanCuoc);
            model.BanScanSoCMNDCanCuocPath = NormalizeText(model.BanScanSoCMNDCanCuocPath);
            model.BanDangKyEPortChuKySoPath = NormalizeText(model.BanDangKyEPortChuKySoPath);
        }

        private void ValidateNguoiDungDoanhNghiep(NguoiDungDoanhNghiepSaveRequest model)
        {
            if (model.IDDanhMucKhachHangDoiLenh <= 0)
            {
                throw new Exception("Không xác định được tài khoản khách hàng.");
            }

            if (string.IsNullOrWhiteSpace(model.TenDoanhNghiep))
            {
                throw new Exception("Tên doanh nghiệp không được bỏ trống.");
            }

            if (string.IsNullOrWhiteSpace(model.MaSoThue))
            {
                throw new Exception("Mã số thuế không được bỏ trống.");
            }

            if (string.IsNullOrWhiteSpace(model.DiaChi))
            {
                throw new Exception("Địa chỉ doanh nghiệp không được bỏ trống.");
            }

            if (string.IsNullOrWhiteSpace(model.SoDienThoaiDoanhNghiep))
            {
                throw new Exception("Số điện thoại doanh nghiệp không được bỏ trống.");
            }

            if (string.IsNullOrWhiteSpace(model.EmailDoanhNghiep))
            {
                throw new Exception("Email doanh nghiệp không được bỏ trống.");
            }
        }

        private long InsertNguoiDungDoanhNghiepRecord(SqlConnection sqlConnection, SqlTransaction transaction, NguoiDungDoanhNghiepSaveRequest model)
        {
            long companyId;
            using (SqlCommand identityCommand = new SqlCommand("select isnull(max(ID), 0) + 1 from NguoiDungDoanhNghiep with (updlock, holdlock)", sqlConnection, transaction))
            {
                companyId = Convert.ToInt64(identityCommand.ExecuteScalar());
            }

            const string sql = @"
                insert into NguoiDungDoanhNghiep
                (
                    ID,
                    IDDanhMucKhachHangDoiLenh,
                    TenDoanhNghiep,
                    MaSoThue,
                    DiaChi,
                    SoDienThoaiDoanhNghiep,
                    EmailDoanhNghiep,
                    SoFax,
                    GiayPhepKinhDoanh,
                    BanScanGiayPhepKinhDoanhPath,
                    NgayCap,
                    NoiCap,
                    DaiDienCoThamQuyen,
                    ChucVu,
                    DoanhNghiepCongTyDuocUyQuyen,
                    TenDangNhapDangKyDichVu,
                    EmailXuatHoaDon,
                    SoCMNDCanCuoc,
                    BanScanSoCMNDCanCuocPath,
                    BanDangKyEPortChuKySoPath,
                    IsActive,
                    IDDanhMucNguoiSuDungCreate,
                    CreateDate
                )
                values
                (
                    @ID,
                    @IDDanhMucKhachHangDoiLenh,
                    @TenDoanhNghiep,
                    @MaSoThue,
                    @DiaChi,
                    @SoDienThoaiDoanhNghiep,
                    @EmailDoanhNghiep,
                    @SoFax,
                    @GiayPhepKinhDoanh,
                    @BanScanGiayPhepKinhDoanhPath,
                    @NgayCap,
                    @NoiCap,
                    @DaiDienCoThamQuyen,
                    @ChucVu,
                    @DoanhNghiepCongTyDuocUyQuyen,
                    @TenDangNhapDangKyDichVu,
                    @EmailXuatHoaDon,
                    @SoCMNDCanCuoc,
                    @BanScanSoCMNDCanCuocPath,
                    @BanDangKyEPortChuKySoPath,
                    @IsActive,
                    @IDDanhMucNguoiSuDungCreate,
                    @CreateDate
                )";

            using (SqlCommand command = new SqlCommand(sql, sqlConnection, transaction))
            {
                BindNguoiDungDoanhNghiepParameters(command, companyId, model, true);
                command.ExecuteNonQuery();
            }

            return companyId;
        }

        private long UpdateNguoiDungDoanhNghiep(SqlConnection sqlConnection, SqlTransaction transaction, NguoiDungDoanhNghiepSaveRequest model)
        {
            const string sql = @"
                update NguoiDungDoanhNghiep set
                    TenDoanhNghiep = @TenDoanhNghiep,
                    MaSoThue = @MaSoThue,
                    DiaChi = @DiaChi,
                    SoDienThoaiDoanhNghiep = @SoDienThoaiDoanhNghiep,
                    EmailDoanhNghiep = @EmailDoanhNghiep,
                    SoFax = @SoFax,
                    GiayPhepKinhDoanh = @GiayPhepKinhDoanh,
                    BanScanGiayPhepKinhDoanhPath = @BanScanGiayPhepKinhDoanhPath,
                    NgayCap = @NgayCap,
                    NoiCap = @NoiCap,
                    DaiDienCoThamQuyen = @DaiDienCoThamQuyen,
                    ChucVu = @ChucVu,
                    DoanhNghiepCongTyDuocUyQuyen = @DoanhNghiepCongTyDuocUyQuyen,
                    TenDangNhapDangKyDichVu = @TenDangNhapDangKyDichVu,
                    EmailXuatHoaDon = @EmailXuatHoaDon,
                    SoCMNDCanCuoc = @SoCMNDCanCuoc,
                    BanScanSoCMNDCanCuocPath = @BanScanSoCMNDCanCuocPath,
                    BanDangKyEPortChuKySoPath = @BanDangKyEPortChuKySoPath,
                    IsActive = @IsActive,
                    IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
                    EditDate = @EditDate
                where ID = @ID and IDDanhMucKhachHangDoiLenh = @IDDanhMucKhachHangDoiLenh";

            using (SqlCommand command = new SqlCommand(sql, sqlConnection, transaction))
            {
                BindNguoiDungDoanhNghiepParameters(command, model.ID.Value, model, false);
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    throw new Exception("Không tìm thấy doanh nghiệp cần cập nhật.");
                }
            }

            return model.ID.Value;
        }

        private void BindNguoiDungDoanhNghiepParameters(SqlCommand command, long id, NguoiDungDoanhNghiepSaveRequest model, bool isInsert)
        {
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@IDDanhMucKhachHangDoiLenh", model.IDDanhMucKhachHangDoiLenh);
            command.Parameters.AddWithValue("@TenDoanhNghiep", (object)model.TenDoanhNghiep ?? DBNull.Value);
            command.Parameters.AddWithValue("@MaSoThue", (object)model.MaSoThue ?? DBNull.Value);
            command.Parameters.AddWithValue("@DiaChi", (object)model.DiaChi ?? DBNull.Value);
            command.Parameters.AddWithValue("@SoDienThoaiDoanhNghiep", (object)model.SoDienThoaiDoanhNghiep ?? DBNull.Value);
            command.Parameters.AddWithValue("@EmailDoanhNghiep", (object)model.EmailDoanhNghiep ?? DBNull.Value);
            command.Parameters.AddWithValue("@SoFax", (object)model.SoFax ?? DBNull.Value);
            command.Parameters.AddWithValue("@GiayPhepKinhDoanh", (object)model.GiayPhepKinhDoanh ?? DBNull.Value);
            command.Parameters.AddWithValue("@BanScanGiayPhepKinhDoanhPath", (object)model.BanScanGiayPhepKinhDoanhPath ?? DBNull.Value);
            command.Parameters.AddWithValue("@NgayCap", (object)model.NgayCap ?? DBNull.Value);
            command.Parameters.AddWithValue("@NoiCap", (object)model.NoiCap ?? DBNull.Value);
            command.Parameters.AddWithValue("@DaiDienCoThamQuyen", (object)model.DaiDienCoThamQuyen ?? DBNull.Value);
            command.Parameters.AddWithValue("@ChucVu", (object)model.ChucVu ?? DBNull.Value);
            command.Parameters.AddWithValue("@DoanhNghiepCongTyDuocUyQuyen", (object)model.DoanhNghiepCongTyDuocUyQuyen ?? DBNull.Value);
            command.Parameters.AddWithValue("@TenDangNhapDangKyDichVu", (object)model.TenDangNhapDangKyDichVu ?? DBNull.Value);
            command.Parameters.AddWithValue("@EmailXuatHoaDon", (object)model.EmailXuatHoaDon ?? DBNull.Value);
            command.Parameters.AddWithValue("@SoCMNDCanCuoc", (object)model.SoCMNDCanCuoc ?? DBNull.Value);
            command.Parameters.AddWithValue("@BanScanSoCMNDCanCuocPath", (object)model.BanScanSoCMNDCanCuocPath ?? DBNull.Value);
            command.Parameters.AddWithValue("@BanDangKyEPortChuKySoPath", (object)model.BanDangKyEPortChuKySoPath ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", model.IsActive);

            if (isInsert)
            {
                command.Parameters.AddWithValue("@IDDanhMucNguoiSuDungCreate", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucNguoiSuDungGuest));
                command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
            }
            else
            {
                command.Parameters.AddWithValue("@IDDanhMucNguoiSuDungEdit", coreCommon.coreCommon.longParse(GlobalVariables.IDDanhMucNguoiSuDungGuest));
                command.Parameters.AddWithValue("@EditDate", DateTime.Now);
            }
        }

        private NguoiDungDoanhNghiepDto MapNguoiDungDoanhNghiep(SqlDataReader reader)
        {
            return new NguoiDungDoanhNghiepDto
            {
                ID = reader["ID"],
                IDDanhMucKhachHangDoiLenh = reader["IDDanhMucKhachHangDoiLenh"],
                TenDoanhNghiep = reader["TenDoanhNghiep"],
                MaSoThue = reader["MaSoThue"],
                DiaChi = reader["DiaChi"],
                SoDienThoaiDoanhNghiep = reader["SoDienThoaiDoanhNghiep"],
                EmailDoanhNghiep = reader["EmailDoanhNghiep"],
                SoFax = reader["SoFax"] == DBNull.Value ? null : reader["SoFax"],
                GiayPhepKinhDoanh = reader["GiayPhepKinhDoanh"] == DBNull.Value ? null : reader["GiayPhepKinhDoanh"],
                BanScanGiayPhepKinhDoanhPath = reader["BanScanGiayPhepKinhDoanhPath"] == DBNull.Value ? null : reader["BanScanGiayPhepKinhDoanhPath"],
                NgayCap = reader["NgayCap"] == DBNull.Value ? null : reader["NgayCap"],
                NoiCap = reader["NoiCap"] == DBNull.Value ? null : reader["NoiCap"],
                DaiDienCoThamQuyen = reader["DaiDienCoThamQuyen"] == DBNull.Value ? null : reader["DaiDienCoThamQuyen"],
                ChucVu = reader["ChucVu"] == DBNull.Value ? null : reader["ChucVu"],
                DoanhNghiepCongTyDuocUyQuyen = reader["DoanhNghiepCongTyDuocUyQuyen"] == DBNull.Value ? null : reader["DoanhNghiepCongTyDuocUyQuyen"],
                TenDangNhapDangKyDichVu = reader["TenDangNhapDangKyDichVu"] == DBNull.Value ? null : reader["TenDangNhapDangKyDichVu"],
                EmailXuatHoaDon = reader["EmailXuatHoaDon"] == DBNull.Value ? null : reader["EmailXuatHoaDon"],
                SoCMNDCanCuoc = reader["SoCMNDCanCuoc"] == DBNull.Value ? null : reader["SoCMNDCanCuoc"],
                BanScanSoCMNDCanCuocPath = reader["BanScanSoCMNDCanCuocPath"] == DBNull.Value ? null : reader["BanScanSoCMNDCanCuocPath"],
                BanDangKyEPortChuKySoPath = reader["BanDangKyEPortChuKySoPath"] == DBNull.Value ? null : reader["BanDangKyEPortChuKySoPath"],
                IsActive = reader["IsActive"],
                CreateDate = reader["CreateDate"] == DBNull.Value ? null : reader["CreateDate"],
                EditDate = reader["EditDate"] == DBNull.Value ? null : reader["EditDate"]
            };
        }
    } 
}
