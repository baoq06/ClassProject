using ClassProject.DataAccess.Db;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ClassProject
{
    internal class UserRepository
    {
        public DataTable GetUser(string userId)
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT Id, FirstName, LastName, Username, Email, Picture
FROM dbo.Users
WHERE Id = @Id;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = int.Parse(userId) });

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy thông tin user: " + ex.Message);
                }
            }

            return dt;
        }

        /// <summary>Cập nhật FirstName, LastName, Email, Picture theo Id.</summary>
        public void Edit_User(User user)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
UPDATE dbo.Users
SET FirstName = @FirstName,
    LastName  = @LastName,
    Email     = @Email,
    Picture   = @Picture
WHERE Id = @Id;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = int.Parse(user.Id) });
                        cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = user.FirstName ?? (object)DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = user.LastName ?? (object)DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = user.Email ?? (object)DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@Picture", SqlDbType.VarBinary) { Value = user.Picture ?? (object)DBNull.Value });

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                            MessageBox.Show("Cập nhật thông tin thành công!", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Không tìm thấy user để cập nhật.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật user: " + ex.Message);
                }
            }
        }

        /// <summary>Lấy Id, Password, RoleId theo Username (đăng nhập).</summary>
        public DataTable GetByUsername(string username)
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT Id, Password, RoleId
FROM dbo.Users
WHERE Username = @Username;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username });

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy thông tin user: " + ex.Message);
                }
            }

            return dt;
        }

        public bool EmailExists(string email)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = "SELECT COUNT(*) FROM dbo.Users WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email });
                        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kiểm tra email: " + ex.Message);
                    return false;
                }
            }
        }

        public bool ResetPassword(string email, string hashedPassword)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = "UPDATE dbo.Users SET Password = @Password WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = hashedPassword });
                        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email });
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đặt lại mật khẩu: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>Tạo user mới trong transaction (duyệt sinh viên).</summary>
        public static int CreateUser(string username, string email, string hashedPassword, int roleId,
                                     SqlConnection conn, SqlTransaction tx)
        {
            const string sql = @"
INSERT INTO dbo.Users (Username, Email, Password, RoleId)
OUTPUT INSERTED.Id
VALUES (@Username, @Email, @Password, @RoleId);";

            using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username });
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email });
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = hashedPassword });
                cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = roleId });

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static bool GetMustChangePassword(int userId)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = "SELECT MustChangePassword FROM dbo.Users WHERE Id = @Id;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = userId });
                        object result = cmd.ExecuteScalar();
                        if (result == null || result == DBNull.Value) return false;
                        return Convert.ToBoolean(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đọc trạng thái đổi mật khẩu: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Đổi mật khẩu cho user theo Id và clear cờ MustChangePassword.
        /// Chú ý: hashedPassword phải là chuỗi đã BCrypt.HashPassword,
        /// KHÔNG truyền plain text.
        /// </summary>
        public static bool ChangePassword(int userId, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("Hashed password không được rỗng.", nameof(hashedPassword));

            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
UPDATE dbo.Users
SET Password = @Password,
    MustChangePassword = 0
WHERE Id = @Id;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = hashedPassword });
                        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = userId });
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đổi mật khẩu: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
   
