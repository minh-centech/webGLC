using cenBUS;
using cenDTO;
using coreDTO;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

                // Gọi SP đã nâng cấp (truyền parameter TrangThaiThanhToanBienNhanGoc xuống trực tiếp SQL)
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
                    request.PageSize <= 0 ? 10 : request.PageSize,
                    request.TrangThaiThanhToanBienNhanGoc);

                List<LenhOnlines> list = new List<LenhOnlines>();
                int totalCount = 0;

                foreach (DataRow dataRow in dt.Rows)
                {
                    // Lấy TotalCount chuẩn trực tiếp từ dòng đầu tiên của SQL
                    if (totalCount == 0)
                    {
                        totalCount = coreCommon.coreCommon.intParse(dataRow["TotalCount"]);
                    }

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
                        EmailNguoiTao = dataRow.Table.Columns.Contains("EmailNguoiTao") ? coreCommon.coreCommon.stringParse(dataRow["EmailNguoiTao"]) : string.Empty,
                        TenNguoiTao = dataRow.Table.Columns.Contains("TenNguoiTao") ? coreCommon.coreCommon.stringParse(dataRow["TenNguoiTao"]) : string.Empty,
                        IsHoanThanh = dataRow["IsHoanThanh"] == DBNull.Value ? null : dataRow["IsHoanThanh"],
                        CreateDate = dataRow["CreateDate"] == DBNull.Value ? null : dataRow["CreateDate"],
                        EditDate = dataRow["EditDate"] == DBNull.Value ? null : dataRow["EditDate"],
                        IDctLenhNhapKhoHangNhapKhauChiTiet = dataRow["IDctLenhNhapKhoHangNhapKhauChiTiet"] == DBNull.Value ? null : dataRow["IDctLenhNhapKhoHangNhapKhauChiTiet"],

                        // Map dữ liệu biên nhận trực tiếp từ DataTable trả về
                        SoBienNhanDaThanhToan = coreCommon.coreCommon.intParse(dataRow["SoBienNhanDaThanhToan"]),
                        SoBienNhanChuaThanhToan = coreCommon.coreCommon.intParse(dataRow["SoBienNhanChuaThanhToan"]),
                        BienNhanThanhToanGoc = coreCommon.coreCommon.intParse(dataRow["BienNhanThanhToanGoc"])
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


        [HttpPost]
        public HttpResponseMessage ExportExcel(LenhOnlinesExportExcelRequest request)
        {
            try
            {
                request = request ?? new LenhOnlinesExportExcelRequest();

                DataTable dt = LenhOnlinesBUS.ExportExcel(
                    GlobalVariables.ConnectionString,
                    request.TuNgay,
                    request.DenNgay,
                    request.TrangThaiThanhToanBNG
                );

                if (dt == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Không lấy được dữ liệu.");
                }

                MemoryStream stream = new MemoryStream();

                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ThongKeLenhOnline");

                    int totalRows = dt.Rows.Count;
                    int totalCols = dt.Columns.Count;

                    // 0. 🟢 CHÈN DÒNG TIÊU ĐỀ LỚN Ở DÒNG 1
                    // Kiểm tra chuỗi string null/rỗng đơn giản
                    string strTuNgay = DateTime.TryParse(request.TuNgay, out DateTime dtFrom) ? dtFrom.ToString("dd/MM/yyyy") : "...";
                    string strDenNgay = DateTime.TryParse(request.DenNgay, out DateTime dtTo) ? dtTo.ToString("dd/MM/yyyy") : "...";

                    string titleText = $"THỐNG KÊ LỆNH ONLINE ĐÃ THANH TOÁN TỪ NGÀY {strTuNgay} ĐẾN NGÀY {strDenNgay}";
                    worksheet.Cells[1, 1].Value = titleText.ToUpper();

                    // Định dạng Dòng Tiêu Đề
                    int mergeCols = totalCols > 0 ? totalCols : 10;
                    using (var titleRange = worksheet.Cells[1, 1, 1, mergeCols])
                    {
                        titleRange.Merge = true;
                        titleRange.Style.Font.Bold = true;
                        titleRange.Style.Font.Size = 14;
                        titleRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        titleRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }
                    worksheet.Row(1).Height = 30;

                    // 1. Đổ dữ liệu từ DataTable vào Worksheet BẮT ĐẦU TỪ DÒNG 3
                    worksheet.Cells["A3"].LoadFromDataTable(dt, true);

                    // Định dạng Header Tên Cột (Dòng 3)
                    if (totalCols > 0)
                    {
                        using (var range = worksheet.Cells[3, 1, 3, totalCols])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                            range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                    }

                    // 2. 🟢 TỰ ĐỘNG KHẮC PHỤC LỖI NGÀY THÁNG HIỆN SỐ 46234
                    for (int colIndex = 0; colIndex < totalCols; colIndex++)
                    {
                        var colType = dt.Columns[colIndex].DataType;
                        if (colType == typeof(DateTime) || colType == typeof(DateTime?))
                        {
                            // EPPlus tính cột từ 1 nên là colIndex + 1
                            // Cột kiểu ngày sẽ tự chuyển dạng dd/MM/yyyy HH:mm (Nếu chỉ muốn ngày thì đổi thành "dd/MM/yyyy")
                            worksheet.Column(colIndex + 1).Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                        }
                    }

                    if (totalRows > 0)
                    {
                        // 3. 🟢 BÔI MÀU CÁC DÒNG DỮ LIỆU (Bắt đầu từ Dòng 4)
                        for (int i = 0; i < totalRows; i++)
                        {
                            DataRow row = dt.Rows[i];
                            int excelRowIndex = i + 4;

                            int bngVal = row["BienNhanThanhToanGoc"] != DBNull.Value ? Convert.ToInt32(row["BienNhanThanhToanGoc"]) : -1;
                            bool hasBienNhan = row["IDBienNhan"] != DBNull.Value;

                            if (hasBienNhan)
                            {
                                using (var rowRange = worksheet.Cells[excelRowIndex, 1, excelRowIndex, totalCols])
                                {
                                    rowRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                                    if (bngVal == 1)
                                    {
                                        // 🟦 Biên nhận gốc -> Xanh nước biển nhạt
                                        rowRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(200, 220, 240));
                                    }
                                    else if (bngVal == 0)
                                    {
                                        // ⬜ Biên nhận gia hạn -> Trắng
                                        rowRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 255, 255));
                                    }
                                }
                            }
                        }

                        // 4. Tính toán các số liệu thống kê từ DataTable
                        int tongSoLenhCoBNGDaTT = dt.AsEnumerable()
                            .Where(r => r.Field<int?>("BienNhanThanhToanGoc") == 1)
                            .Select(r => Convert.ToInt64(r["IDLenh"]))
                            .Distinct()
                            .Count();

                        int tongSoBienNhanDaTT = dt.AsEnumerable()
                            .Where(r => r["IDBienNhan"] != DBNull.Value)
                            .Count();

                        // 5. Ghép dòng Thống kê vào bên dưới bảng Dữ liệu
                        int startSummaryRow = totalRows + 5;

                        // --- Dòng Tổng 1: Tổng số Lệnh có BNG đã thanh toán ---
                        worksheet.Cells[startSummaryRow, 1].Value = "TỔNG SỐ LỆNH CÓ BIÊN NHẬN GỐC ĐÃ THANH TOÁN:";
                        worksheet.Cells[startSummaryRow, 5].Value = tongSoLenhCoBNGDaTT;

                        using (var range = worksheet.Cells[startSummaryRow, 1, startSummaryRow, 4])
                        {
                            range.Merge = true;
                            range.Style.Font.Bold = true;
                            range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        }
                        worksheet.Cells[startSummaryRow, 5].Style.Font.Bold = true;

                        // --- Dòng Tổng 2: Tổng số tất cả Biên nhận đã thanh toán ---
                        worksheet.Cells[startSummaryRow + 1, 1].Value = "TỔNG SỐ BIÊN NHẬN ĐÃ THANH TOÁN (GỐC + GIA HẠN):";
                        worksheet.Cells[startSummaryRow + 1, 5].Value = tongSoBienNhanDaTT;

                        using (var range = worksheet.Cells[startSummaryRow + 1, 1, startSummaryRow + 1, 4])
                        {
                            range.Merge = true;
                            range.Style.Font.Bold = true;
                            range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        }
                        worksheet.Cells[startSummaryRow + 1, 5].Style.Font.Bold = true;
                    }

                    // AutoFit kích thước cột
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    package.Save();
                }

                stream.Position = 0;

                // Trả về Response Stream
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                string fileName = $"ThongKe_LenhOnline_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
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

            if (!isUpdate && string.IsNullOrWhiteSpace(model.SoToKhai))
            {
                throw new Exception("SoToKhai khong duoc bo trong.");
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

        private Dictionary<long, (int DaThanhToan, int ChuaThanhToan, int BienNhanThanhToanGoc)> LoadReceiptCounts(DataTable lenhOnlinesTable)
        {
            var detailIds = lenhOnlinesTable.AsEnumerable()
                .Select(row => row["IDctLenhNhapKhoHangNhapKhauChiTiet"] == DBNull.Value
                    ? 0L
                    : coreCommon.coreCommon.longParse(row["IDctLenhNhapKhoHangNhapKhauChiTiet"]))
                .Where(id => id > 0)
                .Distinct()
                .ToList();

            var result = new Dictionary<long, (int DaThanhToan, int ChuaThanhToan, int BienNhanThanhToanGoc)>();
            if (detailIds.Count == 0)
            {
                return result;
            }

            using (var sqlConnection = new SqlConnection(GlobalVariables.ConnectionString))
            {
                sqlConnection.Open();

                var parameters = detailIds
                    .Select((id, index) => new SqlParameter($"@id{index}", id))
                    .ToArray();

                var inClause = string.Join(", ", parameters.Select(parameter => parameter.ParameterName));
                var sql = $@"
                select
                    IDctLenhNhapKhoHangNhapKhauChiTiet,
                    sum(case when DaThanhToan = 1 then 1 else 0 end) as SoBienNhanDaThanhToan,
                    sum(case when DaThanhToan = 0 then 1 else 0 end) as SoBienNhanChuaThanhToan,
                   MAX(
                        CASE
                            WHEN IDctLenhXuatKhoHangNhapKhau IS NOT NULL
                            THEN CAST(DaThanhToan AS INT)
                            ELSE 0
                        END
                    ) AS BienNhanThanhToanGoc
                from ctBienNhanThanhToanHangNhapKhauTemp
                where IDctLenhNhapKhoHangNhapKhauChiTiet in ({inClause})
                group by IDctLenhNhapKhoHangNhapKhauChiTiet";

                using (var cmd = new SqlCommand(sql, sqlConnection))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var detailId = coreCommon.coreCommon.longParse(reader["IDctLenhNhapKhoHangNhapKhauChiTiet"]);
                            result[detailId] = (
                                coreCommon.coreCommon.intParse(reader["SoBienNhanDaThanhToan"]),
                                coreCommon.coreCommon.intParse(reader["SoBienNhanChuaThanhToan"]),
                                coreCommon.coreCommon.intParse(reader["BienNhanThanhToanGoc"]))
                                ;
                        }
                    }
                }
            }

            return result;
        }
    }
}
