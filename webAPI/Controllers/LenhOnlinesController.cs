using cenBUS;
using cenDTO;
using coreDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using System.Web.Http.Cors;
using GlobalVariables = webAPI.Code.GlobalVariables;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LenhOnlinesController : ApiController
    {
        [HttpPost]
        public webAPIresponse List(LenhOnlinesFilterRequest request)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                request = request ?? new LenhOnlinesFilterRequest();
                DataTable dt = LenhOnlinesBUS.List(
                    GlobalVariables.ConnectionString,
                    request.ID,
                    request.IDDanhMucKhachHangDoiLenh,
                    request.TuNgay,
                    request.DenNgay,
                    NormalizeText(request.HouseBill),
                    NormalizeText(request.SoCont),
                    NormalizeText(request.MaSoThue),
                    request.Page <= 0 ? 1 : request.Page,
                    request.PageSize <= 0 ? 10 : request.PageSize);

                List<LenhOnlines> list = new List<LenhOnlines>();
                int totalCount = 0;
                foreach (DataRow dataRow in dt.Rows)
                {
                    totalCount = coreCommon.coreCommon.intParse(dataRow["TotalCount"]);
                    list.Add(new LenhOnlines
                    {
                        ID = coreCommon.coreCommon.longParse(dataRow["ID"]),
                        SoThuTuLenh = coreCommon.coreCommon.longParse(dataRow["SoThuTuLenh"]),
                        HoVaTen = coreCommon.coreCommon.stringParse(dataRow["HoVaTen"]),
                        SoDienThoai = coreCommon.coreCommon.stringParse(dataRow["SoDienThoai"]),
                        SoCMND = coreCommon.coreCommon.stringParse(dataRow["SoCMND"]),
                        SoXe = coreCommon.coreCommon.stringParse(dataRow["SoXe"]),
                        MaSoThue = coreCommon.coreCommon.stringParse(dataRow["MaSoThue"]),
                        TenCongTy = coreCommon.coreCommon.stringParse(dataRow["TenCongTy"]),
                        DiaChi = coreCommon.coreCommon.stringParse(dataRow["DiaChi"]),
                        Email = coreCommon.coreCommon.stringParse(dataRow["Email"]),
                        HouseBill = coreCommon.coreCommon.stringParse(dataRow["HouseBill"]),
                        NgayLamLenh = dataRow["NgayLamLenh"] == DBNull.Value ? null : dataRow["NgayLamLenh"],
                        SoCont = coreCommon.coreCommon.stringParse(dataRow["SoCont"]),
                        NgayLayHang = dataRow["NgayLayHang"] == DBNull.Value ? null : dataRow["NgayLayHang"],
                        SoToKhai = coreCommon.coreCommon.stringParse(dataRow["SoToKhai"]),
                        TrangThai = coreCommon.coreCommon.intParse(dataRow["TrangThai"]),
                        IDDanhMucKhachHangDoiLenh = coreCommon.coreCommon.longParse(dataRow["IDDanhMucKhachHangDoiLenh"]),
                        ChiTietId = dataRow["ChiTietId"] == DBNull.Value ? null : dataRow["ChiTietId"],
                        TrangThaiThanhToan = dataRow["TrangThaiThanhToan"] == DBNull.Value ? null : dataRow["TrangThaiThanhToan"],
                        IsHoanThanh = dataRow["IsHoanThanh"] == DBNull.Value ? null : dataRow["IsHoanThanh"],
                        LinkTaiHoaDon = dataRow["LinkTaiHoaDon"] == DBNull.Value ? null : dataRow["LinkTaiHoaDon"],
                        DuongDanFileHoaDon = dataRow["DuongDanFileHoaDon"] == DBNull.Value ? null : dataRow["DuongDanFileHoaDon"],
                        CreateDate = dataRow["CreateDate"] == DBNull.Value ? null : dataRow["CreateDate"],
                        EditDate = dataRow["EditDate"] == DBNull.Value ? null : dataRow["EditDate"]
                    });
                }

                response.Status = 0;
                response.Data = JsonConvert.SerializeObject(new
                {
                    Items = list,
                    TotalCount = totalCount,
                    Page = request.Page <= 0 ? 1 : request.Page,
                    PageSize = request.PageSize <= 0 ? 10 : request.PageSize
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
        public webAPIresponse Insert(LenhOnlinesSaveRequest model)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                Normalize(model);
                Validate(model, false);

                if (LenhOnlinesBUS.Insert(GlobalVariables.ConnectionString, model, out object id, out object ngayLamLenh))
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(new
                    {
                        ID = id,
                        NgayLamLenh = ngayLamLenh
                    });
                    response.ErrorMsg = string.Empty;
                }
                else
                {
                    throw new Exception("Khong the them du lieu.");
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
        public webAPIresponse Update(LenhOnlinesSaveRequest model)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                Normalize(model);
                Validate(model, true);

                if (LenhOnlinesBUS.Update(GlobalVariables.ConnectionString, model))
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(new { ID = model.ID });
                    response.ErrorMsg = string.Empty;
                }
                else
                {
                    throw new Exception("Khong the cap nhat du lieu.");
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
        public webAPIresponse UpsertChiTiet(LenhOnlineChiTietUpsertRequest model)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                if (model == null || model.IDLenhOnline <= 0)
                {
                    throw new Exception("IDLenhOnline khong hop le.");
                }

                if (LenhOnlineChiTietBUS.Upsert(GlobalVariables.ConnectionString, model))
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(new
                    {
                        IDLenhOnline = model.IDLenhOnline
                    });
                    response.ErrorMsg = string.Empty;
                }
                else
                {
                    throw new Exception("Khong the cap nhat du lieu chi tiet lenh online.");
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
        public webAPIresponse Delete(LenhOnlinesDeleteRequest request)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                if (request == null || request.ID <= 0)
                {
                    throw new Exception("ID khong hop le.");
                }

                if (LenhOnlinesBUS.Delete(GlobalVariables.ConnectionString, request.ID))
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(new { ID = request.ID });
                    response.ErrorMsg = string.Empty;
                }
                else
                {
                    throw new Exception("Khong the xoa du lieu.");
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

        private void Normalize(LenhOnlinesSaveRequest model)
        {
            if (model == null)
            {
                throw new Exception("Du lieu khong hop le.");
            }

            model.HoVaTen = NormalizeText(model.HoVaTen);
            model.SoDienThoai = NormalizeText(model.SoDienThoai);
            model.SoCMND = NormalizeText(model.SoCMND);
            model.SoXe = NormalizeText(model.SoXe);
            model.MaSoThue = NormalizeText(model.MaSoThue);
            model.TenCongTy = NormalizeText(model.TenCongTy);
            model.DiaChi = NormalizeText(model.DiaChi);
            model.Email = NormalizeText(model.Email);
            model.HouseBill = NormalizeText(model.HouseBill);
            model.SoCont = NormalizeText(model.SoCont);
            model.SoToKhai = NormalizeText(model.SoToKhai);
        }

        private void Validate(LenhOnlinesSaveRequest model, bool isUpdate)
        {
            if (isUpdate && (!model.ID.HasValue || model.ID.Value <= 0))
            {
                throw new Exception("ID cap nhat khong hop le.");
            }

            if (string.IsNullOrWhiteSpace(model.HoVaTen))
            {
                throw new Exception("HoVaTen khong duoc bo trong.");
            }

            if (model.IDDanhMucKhachHangDoiLenh <= 0)
            {
                throw new Exception("IDDanhMucKhachHangDoiLenh khong hop le.");
            }

            if (model.TrangThai < 0 || model.TrangThai > 5)
            {
                throw new Exception("TrangThai khong hop le.");
            }
        }

        private string NormalizeText(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
