using coreDAO;
using cenDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace cenBUS
{
    public class DanhMucKhachHangDoiLenhBUS
    {
        public static DataTable List(string ConnectionString, object IDDanhMucDonVi, object IDDanhMucLoaiDoiTuong, object ID)
        {
            try
            {

                ConnectionDAO dao = new ConnectionDAO(ConnectionString);
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", ID),
                    new SqlParameter("@IDDanhMucDonVi", IDDanhMucDonVi),
                    new SqlParameter("@IDDanhMucLoaiDoiTuong", IDDanhMucLoaiDoiTuong),
                };
                DataTable dt = dao.tableList(sqlParameters, DanhMucKhachHangDoiLenh.listProcedureName, DanhMucKhachHangDoiLenh.tableName);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable ListLogin(string ConnectionString, object IDDanhMucDonVi, object IDDanhMucLoaiDoiTuong, object Email, object Password)
        {
            DataTable dt = new DataTable();
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = DanhMucKhachHangDoiLenh.listLoginProcedureName;

                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("@ID", DBNull.Value)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Size = 8
                        });
                        sqlParameters.Add(new SqlParameter("@IDDanhMucDonVi", IDDanhMucDonVi));
                        sqlParameters.Add(new SqlParameter("@IDDanhMucLoaiDoiTuong", IDDanhMucLoaiDoiTuong));
                        sqlParameters.Add(new SqlParameter("@Email", Email));
                        sqlParameters.Add(new SqlParameter("@Password", Password));

                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                        sqlDataAdapter.SelectCommand = cmd;
                        sqlDataAdapter.Fill(dt);
                        sqlConnection.Close();
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GenMaKichHoat(int tokenLength)
        {
            var rng = new Random(); // Use System.Random for numeric tokens

            // Build the numeric token
            string token = "";
            for (int i = 0; i < tokenLength; i++)
            {
                token += rng.Next(0, 10).ToString(); // Generates a random digit (0-9)
            }

            return token;
        }
        public static bool Insert(string ConnectionString, DataRow dataRow, out object ID, out string MaKichHoat)
        {
            try
            {
                MaKichHoat = GenMaKichHoat(6);

                ID = null;
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    dataRow["MaKichHoat"] = MaKichHoat;
                    dataRow["ThoiGianTaoMaKichHoat"] = DateTime.Now;
                    bool OK = DataRowDAO.updateRow(dataRow, DanhMucKhachHangDoiLenh.insertProcedureName, sqlConnection, null, out ID);
                    return OK;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool Update(string ConnectionString, DataRow dataRow, out object ID)
        {
            try
            {
                ID = null;
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    bool OK = DataRowDAO.updateRow(dataRow, DanhMucKhachHangDoiLenh.updateProcedureName, sqlConnection, null, out ID);
                    return OK;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool UpdateKichHoat(string ConnectionString, object ID, object MaKichHoat)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = DanhMucKhachHangDoiLenh.UpdateKichHoatAccountProcedure;

                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("@ID", ID));
                        sqlParameters.Add(new SqlParameter("@MaKichHoat", MaKichHoat));

                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        cmd.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool getMaKichHoatByEmail(string ConnectionString, object Email,  out object ID, out object MaKichHoat)
        {
            try
            {
                ID = null;
                MaKichHoat = null;
               object MaKichHoatMoi = GenMaKichHoat(6);      
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = DanhMucKhachHangDoiLenh.getMaKichHoatByEmail;
                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("@Email", Email));
                        sqlParameters.Add(new SqlParameter("@MaKichHoatMoi", MaKichHoatMoi));
                        sqlParameters.Add(new SqlParameter("@ID", ID)
                        {
                            Size = 8,
                            Direction = ParameterDirection.InputOutput
                        });
                        sqlParameters.Add(new SqlParameter("@MaKichHoat", MaKichHoat)
                        {
                            Size = 6,
                            Direction = ParameterDirection.InputOutput
                        });
                      
                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        cmd.ExecuteNonQuery();
                        ID = sqlParameters[2].Value;
                        MaKichHoat = sqlParameters[3].Value;
                      
                    }
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool getMaXacNhanByEmail(string ConnectionString, object Email, out object ID, out object MaXacNhan)
        {
            try
            {
                ID = null;
                MaXacNhan = null;
                object MaXacNhanMoi = GenMaKichHoat(6);
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = DanhMucKhachHangDoiLenh.getMaXacNhanByEmail;
                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("@Email", Email));
                        sqlParameters.Add(new SqlParameter("@MaXacNhanMoi", MaXacNhanMoi));
                        sqlParameters.Add(new SqlParameter("@ID", ID)
                        {
                            Size = 8,
                            Direction = ParameterDirection.InputOutput
                        });
                        sqlParameters.Add(new SqlParameter("@MaXacNhan", MaXacNhan)
                        {
                            Size = 6,
                            Direction = ParameterDirection.InputOutput
                        });

                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        cmd.ExecuteNonQuery();
                        ID = sqlParameters[2].Value;
                        MaXacNhan = sqlParameters[3].Value;

                    }
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool xacNhanDoiMatKhau(string ConnectionString, object MatKhau, object XacNhanMatKhau, object ID, object MaXacNhan)
        {
            try
            {
                
                
             
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = DanhMucKhachHangDoiLenh.updateXacNhanDoiMatKhau;
                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("@ID", ID));
                        sqlParameters.Add(new SqlParameter("@MatKhau", MatKhau));
                        sqlParameters.Add(new SqlParameter("@XacNhanMatKhau", XacNhanMatKhau));
                        sqlParameters.Add(new SqlParameter("@MaXacNhan", MaXacNhan));                      
                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        cmd.ExecuteNonQuery();
                       

                    }
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool UpdatePassword(string ConnectionString, object Email, object OldPassword, object NewPassword, object NewPasswordConfirm, object IDDanhMucNguoiSuDungEdit)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = DanhMucKhachHangDoiLenh.updatePasswordProcedureName;

                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("@Email", Email));
                        sqlParameters.Add(new SqlParameter("@OldPassword", OldPassword));
                        sqlParameters.Add(new SqlParameter("@NewPassword", NewPassword));
                        sqlParameters.Add(new SqlParameter("@NewPasswordConfirm", NewPasswordConfirm));
                        sqlParameters.Add(new SqlParameter("@IDDanhMucNguoiSuDungEdit", IDDanhMucNguoiSuDungEdit));
                        sqlParameters.Add(new SqlParameter("@EditDate", DBNull.Value)
                        {
                            Direction = ParameterDirection.Output,
                            Size = 8
                        });

                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        cmd.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool ResetPassword(string ConnectionString, object Email, object NewPassword, object NewPasswordConfirm, object IDDanhMucNguoiSuDungEdit)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
update DanhMucKhachHangDoiLenh
set [Password] = @NewPassword,
    IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
    EditDate = getdate()
where Email = @Email";

                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("@Email", Email));
                        sqlParameters.Add(new SqlParameter("@NewPassword", NewPassword));
                        sqlParameters.Add(new SqlParameter("@NewPasswordConfirm", NewPasswordConfirm));
                        sqlParameters.Add(new SqlParameter("@IDDanhMucNguoiSuDungEdit", IDDanhMucNguoiSuDungEdit));

                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        cmd.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool Delete(string ConnectionString, object ID)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = DanhMucKhachHangDoiLenh.deleteProcedureName;
                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("@ID", ID));
                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        cmd.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool InsertRecoverPasswordLog(string ConnectionString, object IDDanhMucDonVi, object Email, object Password, object IDDanhMucNguoiSuDung)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = DanhMucKhachHangDoiLenh.insertRecoverPasswordLogProcedureName;

                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("@IDDanhMucDonVi", IDDanhMucDonVi));
                        sqlParameters.Add(new SqlParameter("@Email", Email));
                        sqlParameters.Add(new SqlParameter("@Password", Password));
                        sqlParameters.Add(new SqlParameter("@IDDanhMucNguoiSuDungCreate", IDDanhMucNguoiSuDung));
                        sqlParameters.Add(new SqlParameter("@CreateDate", DBNull.Value)
                        {
                            Direction = ParameterDirection.Output,
                            Size = 8
                        });
                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        cmd.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static object GetPartnerGUIDByEmail(string ConnectionString, object IDDanhMucDonVi, object IDDanhMucLoaiDoiTuong, object Email)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = DanhMucKhachHangDoiLenh.getPartnerGUIDByEmailProcedureName;
                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("@IDDanhMucDonVi", IDDanhMucDonVi));
                        sqlParameters.Add(new SqlParameter("@IDDanhMucLoaiDoiTuong", IDDanhMucLoaiDoiTuong));
                        sqlParameters.Add(new SqlParameter("@Email", Email));
                        sqlParameters.Add(new SqlParameter("@PartnerGUID", DBNull.Value)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Size = 36
                        });
                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        cmd.ExecuteNonQuery();

                        sqlConnection.Close();

                        return sqlParameters[3].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
