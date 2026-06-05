using cenBUS;
using cenDTO;
using coreDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using System.Web.Http.Cors;
using coreCommon;
using GlobalVariables = webAPI.Code.GlobalVariables;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConfigHDDTController : ApiController
    {
        [HttpPost]
        public webAPIresponse List(ConfigHDDTFilterRequest request)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                request = request ?? new ConfigHDDTFilterRequest();
                DataTable dt = ConfigHDDTBUS.List(
                    GlobalVariables.ConnectionString,
                    request.ID,
                    request.Nam,
                    request.IDDanhMucDonVi,
                    request.IDDanhMucLoaiDoiTuong);

                List<ConfigHDDT> list = new List<ConfigHDDT>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    list.Add(MapRow(dataRow));
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

        [HttpPost]
        public webAPIresponse Insert(ConfigHDDTSaveRequest model)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                Normalize(model);
                Validate(model, false);

                if (ConfigHDDTBUS.Insert(
                    GlobalVariables.ConnectionString,
                    model,
                    GlobalVariables.IDDanhMucNguoiSuDungGuest,
                    out object id))
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(new
                    {
                        ID = id
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
        public webAPIresponse Update(ConfigHDDTSaveRequest model)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                Normalize(model);
                Validate(model, true);

                if (ConfigHDDTBUS.Update(
                    GlobalVariables.ConnectionString,
                    model,
                    GlobalVariables.IDDanhMucNguoiSuDungGuest))
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(new
                    {
                        ID = model.ID
                    });
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
        public webAPIresponse Delete(ConfigHDDTDeleteRequest request)
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                if (request == null || request.ID <= 0)
                {
                    throw new Exception("ID khong hop le.");
                }

                if (ConfigHDDTBUS.Delete(GlobalVariables.ConnectionString, request.ID))
                {
                    response.Status = 0;
                    response.Data = JsonConvert.SerializeObject(new
                    {
                        ID = request.ID
                    });
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

        private ConfigHDDT MapRow(DataRow dataRow)
        {
            return new ConfigHDDT
            {
                ID = coreCommon.coreCommon.longParse(dataRow["ID"]),
                Nam = dataRow["Nam"] == DBNull.Value ? null : dataRow["Nam"],
                URLHDDT = coreCommon.coreCommon.stringParse(dataRow["URLHDDT"]),
                Account = coreCommon.coreCommon.stringParse(dataRow["Account"]),
                ACPass = coreCommon.coreCommon.stringParse(dataRow["ACPass"]),
                UserName = coreCommon.coreCommon.stringParse(dataRow["UserName"]),
                Pass = coreCommon.coreCommon.stringParse(dataRow["Pass"]),
                Pattern = coreCommon.coreCommon.stringParse(dataRow["Pattern"]),
                Serial = coreCommon.coreCommon.stringParse(dataRow["Serial"]),
                CreateDate = dataRow["CreateDate"] == DBNull.Value ? null : dataRow["CreateDate"],
                EditDate = dataRow["EditDate"] == DBNull.Value ? null : dataRow["EditDate"],
                IDDanhMucNguoiSuDungCreate = dataRow["IDDanhMucNguoiSuDungCreate"] == DBNull.Value ? null : dataRow["IDDanhMucNguoiSuDungCreate"],
                IDDanhMucNguoiSuDungEdit = dataRow["IDDanhMucNguoiSuDungEdit"] == DBNull.Value ? null : dataRow["IDDanhMucNguoiSuDungEdit"],
                IDDanhMucDonVi = dataRow["IDDanhMucDonVi"] == DBNull.Value ? null : dataRow["IDDanhMucDonVi"],
                IDDanhMucLoaiDoiTuong = dataRow["IDDanhMucLoaiDoiTuong"] == DBNull.Value ? null : dataRow["IDDanhMucLoaiDoiTuong"]
            };
        }

        private void Normalize(ConfigHDDTSaveRequest model)
        {
            if (model == null)
            {
                throw new Exception("Du lieu khong hop le.");
            }

            model.URLHDDT = NormalizeText(model.URLHDDT);
            model.Account = NormalizeText(model.Account);
            model.ACPass = NormalizeText(model.ACPass);
            model.UserName = NormalizeText(model.UserName);
            model.Pass = NormalizeText(model.Pass);
            model.Pattern = NormalizeText(model.Pattern);
            model.Serial = NormalizeText(model.Serial);
        }

        private void Validate(ConfigHDDTSaveRequest model, bool isUpdate)
        {
            if (isUpdate && (!model.ID.HasValue || model.ID.Value <= 0))
            {
                throw new Exception("ID cap nhat khong hop le.");
            }

            if (!model.Nam.HasValue || model.Nam.Value <= 0)
            {
                throw new Exception("Nam khong hop le.");
            }
        }

        private string NormalizeText(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
