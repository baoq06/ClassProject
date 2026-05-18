using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClassProject.Repositories
{
    public class DKMHRepository
    {
        private readonly My_DB _db = new My_DB();

        public bool Register(string mssv, string mamh)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
INSERT INTO DKMH (Mssv, Mamh)
SELECT @Mssv, @Mamh
WHERE NOT EXISTS (
    SELECT 1 FROM DKMH
    WHERE Mssv = @Mssv AND Mamh = @Mamh
)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mssv", mssv);
                    cmd.Parameters.AddWithValue("@Mamh", mamh);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Unregister(string mssv, string mamh)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
DELETE FROM DKMH
WHERE Mssv = @Mssv AND Mamh = @Mamh";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mssv", mssv);
                    cmd.Parameters.AddWithValue("@Mamh", mamh);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable GetByStudent(string mssv)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT d.*, c.Tenmh, c.Sotc, c.Hocky
FROM DKMH d
JOIN Courses c ON d.Mamh = c.Mamh
WHERE d.Mssv = @Mssv";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mssv", mssv);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GetByCourse(string mamh)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT d.*, s.LastName, s.FirstName
FROM DKMH d
JOIN Students s ON d.Mssv = s.MSSV
WHERE d.Mamh = @Mamh";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mamh", mamh);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public bool IsRegistered(string mssv, string mamh)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT COUNT(*)
FROM DKMH
WHERE Mssv = @Mssv AND Mamh = @Mamh";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mssv", mssv);
                    cmd.Parameters.AddWithValue("@Mamh", mamh);

                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }
    }
}
