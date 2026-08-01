using cenDTO;
using coreDAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace cenBUS
{
    public class LenhOnlinesBUS
    {
        public static DataTable List(string connectionString, object id, object idDanhMucKhachHangDoiLenh, object tuNgay, object denNgay, object houseBill, object soCont, object maSoThue, object page, object pageSize, int trangThaiThanhToanBNG = -1)
        {
            try
            {
                ConnectionDAO dao = new ConnectionDAO(connectionString);
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", id ?? DBNull.Value),
                    new SqlParameter("@IDDanhMucKhachHangDoiLenh", idDanhMucKhachHangDoiLenh ?? DBNull.Value),
                    new SqlParameter("@TuNgay", tuNgay ?? DBNull.Value),
                    new SqlParameter("@DenNgay", denNgay ?? DBNull.Value),
                    new SqlParameter("@HouseBill", houseBill ?? DBNull.Value),
                    new SqlParameter("@SoCont", soCont ?? DBNull.Value),
                    new SqlParameter("@MaSoThue", maSoThue ?? DBNull.Value),
                    new SqlParameter("@Page", page ?? DBNull.Value),
                    new SqlParameter("@PageSize", pageSize ?? DBNull.Value),
                    new SqlParameter("@TrangThaiThanhToanBNG", trangThaiThanhToanBNG )
                };

                return dao.tableList(sqlParameters, LenhOnlines.listProcedureName, LenhOnlines.tableName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable ExportExcel(string connectionString, string tuNgay, string denNgay, int trangThaiThanhToanBNG = 1)
        {
            try
            {
                ConnectionDAO dao = new ConnectionDAO(connectionString);

                // Chuyển đổi chuỗi ngày dạng string thành DateTime?
                DateTime? dtTuNgay = string.IsNullOrWhiteSpace(tuNgay)
                ? (DateTime?)null
                : DateTime.ParseExact(tuNgay, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                DateTime? dtDenNgay = string.IsNullOrWhiteSpace(denNgay)
                    ? (DateTime?)null
                    : DateTime.ParseExact(denNgay, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@TuNgay", (object)dtTuNgay ?? DBNull.Value),
                    new SqlParameter("@DenNgay", (object)dtDenNgay ?? DBNull.Value),
                    new SqlParameter("@TrangThaiThanhToanBNG", trangThaiThanhToanBNG)
                };

                return dao.tableList(sqlParameters, LenhOnlines.exportExelProcedureName, LenhOnlines.tableName);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xuất dữ liệu Excel: " + ex.Message);
            }
        }

        public static bool Insert(string connectionString, LenhOnlinesSaveRequest model, out object id, out object ngayLamLenh)
        {
            try
            {
                id = null;
                ngayLamLenh = null;

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = LenhOnlines.insertProcedureName;

                        List<SqlParameter> sqlParameters = BuildSaveParameters(model, false);
                        sqlParameters.Insert(0, new SqlParameter("@ID", DBNull.Value)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Size = 8
                        });
                        sqlParameters.Insert(1, new SqlParameter("@SoThuTuLenh", DBNull.Value)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Size = 8
                        });
                        sqlParameters.Add(new SqlParameter("@NgayLamLenh", DBNull.Value)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Size = 8
                        });
                        sqlParameters.Add(new SqlParameter("@CreateDate", DBNull.Value)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Size = 8
                        });

                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        cmd.ExecuteNonQuery();

                        id = sqlParameters[0].Value;
                        ngayLamLenh = sqlParameters[sqlParameters.Count - 2].Value;
                        ApplyIdctLenhNhapKhoHangNhapKhauChiTiet(
                            sqlConnection,
                            id,
                            model.IDctLenhNhapKhoHangNhapKhauChiTiet);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Update(string connectionString, LenhOnlinesSaveRequest model)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = LenhOnlines.updateProcedureName;

                        List<SqlParameter> sqlParameters = BuildSaveParameters(model, true);
                        sqlParameters.Add(new SqlParameter("@EditDate", DBNull.Value)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Size = 8
                        });

                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        cmd.ExecuteNonQuery();
                        ApplyIdctLenhNhapKhoHangNhapKhauChiTiet(
                            sqlConnection,
                            model.ID,
                            model.IDctLenhNhapKhoHangNhapKhauChiTiet);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Delete(string connectionString, object id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = LenhOnlines.deleteProcedureName;
                        cmd.Parameters.Add(new SqlParameter("@ID", id));
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static List<SqlParameter> BuildSaveParameters(LenhOnlinesSaveRequest model, bool includeId)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            if (includeId)
            {
                sqlParameters.Add(new SqlParameter("@ID", model.ID));
            }

            sqlParameters.Add(new SqlParameter("@HoVaTen", (object)model.HoVaTen ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@SoDienThoai", (object)model.SoDienThoai ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@SoCMND", (object)model.SoCMND ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@SoXe", (object)model.SoXe ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@MaSoThue", (object)model.MaSoThue ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@TenCongTy", (object)model.TenCongTy ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@DiaChi", (object)model.DiaChi ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@Email", (object)model.Email ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@HouseBill", (object)model.HouseBill ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@SoCont", (object)model.SoCont ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@NgayLayHang", (object)model.NgayLayHang ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@SoToKhai", (object)model.SoToKhai ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@TrangThai", model.TrangThai));
            sqlParameters.Add(new SqlParameter("@HoanThanh", (object)model.HoanThanh ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@IDDanhMucKhachHangDoiLenh", model.IDDanhMucKhachHangDoiLenh));

            return sqlParameters;
        }

        private static void ApplyIdctLenhNhapKhoHangNhapKhauChiTiet(
            SqlConnection sqlConnection,
            object id,
            long? idctLenhNhapKhoHangNhapKhauChiTiet)
        {
            if (!idctLenhNhapKhoHangNhapKhauChiTiet.HasValue || idctLenhNhapKhoHangNhapKhauChiTiet.Value <= 0)
            {
                return;
            }

            using (SqlCommand updateCmd = sqlConnection.CreateCommand())
            {
                updateCmd.CommandType = CommandType.Text;
                updateCmd.CommandText = @"
update tblLenhOnlines
set IDctLenhNhapKhoHangNhapKhauChiTiet = @IDctLenhNhapKhoHangNhapKhauChiTiet
where ID = @ID";
                updateCmd.Parameters.AddWithValue("@ID", id ?? DBNull.Value);
                updateCmd.Parameters.AddWithValue("@IDctLenhNhapKhoHangNhapKhauChiTiet", idctLenhNhapKhoHangNhapKhauChiTiet.Value);
                updateCmd.ExecuteNonQuery();
            }
        }
    }
}
