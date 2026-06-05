using cenDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace cenBUS
{
    public class ConfigHDDTBUS
    {
        public static DataTable List(string connectionString, object id, object nam, object idDanhMucDonVi, object idDanhMucLoaiDoiTuong)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
select
    ID,
    Nam,
    URLHDDT,
    Account,
    ACPass,
    UserName,
    Pass,
    Pattern,
    Serial,
    CreateDate,
    EditDate,
    IDDanhMucNguoiSuDungCreate,
    IDDanhMucNguoiSuDungEdit,
    IDDanhMucDonVi,
    IDDanhMucLoaiDoiTuong
from ctConfigHDDT
where (@ID is null or ID = @ID)
  and (@Nam is null or Nam = @Nam)
  and (@IDDanhMucDonVi is null or IDDanhMucDonVi = @IDDanhMucDonVi)
  and (@IDDanhMucLoaiDoiTuong is null or IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong)
order by ID desc";

                        cmd.Parameters.AddWithValue("@ID", (object)id ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Nam", (object)nam ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IDDanhMucDonVi", (object)idDanhMucDonVi ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IDDanhMucLoaiDoiTuong", (object)idDanhMucLoaiDoiTuong ?? DBNull.Value);

                        DataTable dt = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Insert(string connectionString, ConfigHDDTSaveRequest model, object idDanhMucNguoiSuDungCreate, out object id)
        {
            try
            {
                id = null;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
insert into ctConfigHDDT
(
    Nam,
    URLHDDT,
    Account,
    ACPass,
    UserName,
    Pass,
    Pattern,
    Serial,
    CreateDate,
    IDDanhMucNguoiSuDungCreate,
    IDDanhMucDonVi,
    IDDanhMucLoaiDoiTuong
)
output inserted.ID
values
(
    @Nam,
    @URLHDDT,
    @Account,
    @ACPass,
    @UserName,
    @Pass,
    @Pattern,
    @Serial,
    getdate(),
    @IDDanhMucNguoiSuDungCreate,
    @IDDanhMucDonVi,
    @IDDanhMucLoaiDoiTuong
)";

                        BindSaveParameters(cmd, model, false, idDanhMucNguoiSuDungCreate, null);
                        id = cmd.ExecuteScalar();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Update(string connectionString, ConfigHDDTSaveRequest model, object idDanhMucNguoiSuDungEdit)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
update ctConfigHDDT
set Nam = @Nam,
    URLHDDT = @URLHDDT,
    Account = @Account,
    ACPass = @ACPass,
    UserName = @UserName,
    Pass = @Pass,
    Pattern = @Pattern,
    Serial = @Serial,
    EditDate = getdate(),
    IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
    IDDanhMucDonVi = @IDDanhMucDonVi,
    IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong
where ID = @ID";

                        BindSaveParameters(cmd, model, true, null, idDanhMucNguoiSuDungEdit);
                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
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
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"delete from ctConfigHDDT where ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", id);
                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void BindSaveParameters(
            SqlCommand cmd,
            ConfigHDDTSaveRequest model,
            bool includeId,
            object idDanhMucNguoiSuDungCreate,
            object idDanhMucNguoiSuDungEdit)
        {
            if (includeId)
            {
                cmd.Parameters.AddWithValue("@ID", model.ID.HasValue ? (object)model.ID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@IDDanhMucNguoiSuDungEdit", idDanhMucNguoiSuDungEdit ?? DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IDDanhMucNguoiSuDungCreate", idDanhMucNguoiSuDungCreate ?? DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@Nam", (object)model.Nam ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@URLHDDT", (object)model.URLHDDT ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Account", (object)model.Account ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ACPass", (object)model.ACPass ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UserName", (object)model.UserName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Pass", (object)model.Pass ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Pattern", (object)model.Pattern ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Serial", (object)model.Serial ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IDDanhMucDonVi", (object)model.IDDanhMucDonVi ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IDDanhMucLoaiDoiTuong", (object)model.IDDanhMucLoaiDoiTuong ?? DBNull.Value);
        }
    }
}
