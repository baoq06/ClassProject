using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ClassProject
{
    public class Admin
    {
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT u.Id, u.FirstName, u.LastName, u.Username, u.Email, r.RoleName, u.RoleId, u.Created_At
FROM dbo.Users u
LEFT JOIN dbo.Roles r ON u.RoleId = r.Id
ORDER BY u.Id;";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy danh sách users: " + ex.Message);
                }
            }

            return dt;
        }

        public static bool GrantAdminRole(int userId)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    const string sql = "UPDATE dbo.Users SET RoleId = 0 WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", userId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cấp quyền admin: " + ex.Message);
                    return false;
                }
            }
        }

        public static bool RevokeAdminRole(int userId)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    const string sql = "UPDATE dbo.Users SET RoleId = 1 WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", userId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thu hồi quyền admin: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
