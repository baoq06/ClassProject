using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ClassProject
{
    public class Users
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private string _username;
        private string _password;
        private string _email;
        private byte[] _picture;

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Họ và tên đệm không được để trống!");
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Tên không được để trống!");
                _lastName = value;
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Tên đăng nhập không được để trống!");
                _username = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Mật khẩu không được để trống!");
                _password = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.Contains("@"))
                    throw new Exception("Email không hợp lệ!");
                _email = value;
            }
        }

        public byte[] Picture
        {
            get => _picture;
            set
            {
                if (value != null && value.Length > 5 * 1024 * 1024)
                    throw new Exception("Ảnh không được vượt quá 5MB!");
                _picture = value;
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public Users() { }

        public Users(string id, string firstName, string lastName,
                     string username, string password, string email,
                     byte[] picture = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Email = email;
            Picture = picture;
        }

        public override string ToString()
        {
            return $"ID        : {Id}\n" +
                   $"Họ tên    : {FullName}\n" +
                   $"Username  : {Username}\n" +
                   $"Email     : {Email}\n" +
                   $"Ảnh       : {(Picture != null ? "Có" : "Chưa có")}";
        }

        /// <summary>Lấy thông tin user theo Id.</summary>
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
        public void Edit_User()
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
                        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = int.Parse(Id) });
                        cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = FirstName ?? (object)DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = LastName ?? (object)DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = Email ?? (object)DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@Picture", SqlDbType.VarBinary) { Value = Picture ?? (object)DBNull.Value });

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
    }
}
