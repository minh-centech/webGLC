using cenDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace cenBUS
{
    public static class LenhOnlineChiTietBUS
    {
        public static bool Upsert(string connectionString, LenhOnlineChiTietUpsertRequest model)
        {
            try
            {
                if (model == null)
                {
                    throw new Exception("Du lieu khong hop le.");
                }

                if (model.IDLenhOnline <= 0)
                {
                    throw new Exception("IDLenhOnline khong hop le.");
                }

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = LenhOnlineChiTiet.tableUpsertProcedureName;

                        List<SqlParameter> sqlParameters = BuildParameters(model);
                        sqlParameters.Add(new SqlParameter("@ID", DBNull.Value)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Size = 8
                        });
                        sqlParameters.Add(new SqlParameter("@CreateDate", DBNull.Value)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Size = 8
                        });
                        sqlParameters.Add(new SqlParameter("@EditDate", DBNull.Value)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Size = 8
                        });

                        cmd.Parameters.AddRange(sqlParameters.ToArray());
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

        private static List<SqlParameter> BuildParameters(LenhOnlineChiTietUpsertRequest model)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@IDLenhOnline", model.IDLenhOnline));
            sqlParameters.Add(new SqlParameter("@PhiLuuKho", model.PhiLuuKho));
            sqlParameters.Add(new SqlParameter("@PhiGiaoNhan", model.PhiGiaoNhan));
            sqlParameters.Add(new SqlParameter("@PhiBocXep", model.PhiBocXep));
            sqlParameters.Add(new SqlParameter("@VAT", model.VAT));
            sqlParameters.Add(new SqlParameter("@TrangThaiThanhToan", model.TrangThaiThanhToan));
            sqlParameters.Add(new SqlParameter("@TrangThaiThongQuan", model.TrangThaiThongQuan));
            sqlParameters.Add(new SqlParameter("@ThuKho", (object)model.ThuKho ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@Forwarder", (object)model.Forwarder ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@TenTau", (object)model.TenTau ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@ChuHang", (object)model.ChuHang ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@SoKien", model.SoKien.HasValue ? (object)model.SoKien.Value : DBNull.Value));
            sqlParameters.Add(new SqlParameter("@SoChuyen", (object)model.SoChuyen ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@SoHouseBill", (object)model.SoHouseBill ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@NgayTauCap", model.NgayTauCap.HasValue ? (object)model.NgayTauCap.Value : DBNull.Value));
            sqlParameters.Add(new SqlParameter("@TrongLuong", model.TrongLuong.HasValue ? (object)model.TrongLuong.Value : DBNull.Value));
            sqlParameters.Add(new SqlParameter("@SoCont", (object)model.SoCont ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@SoKhoi", model.SoKhoi.HasValue ? (object)model.SoKhoi.Value : DBNull.Value));
            sqlParameters.Add(new SqlParameter("@LinkTaiHoaDon", (object)model.LinkTaiHoaDon ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@DuongDanFileHoaDon", (object)model.DuongDanFileHoaDon ?? DBNull.Value));
            sqlParameters.Add(new SqlParameter("@IsHoanThanh", model.IsHoanThanh));
            return sqlParameters;
        }
    }
}
