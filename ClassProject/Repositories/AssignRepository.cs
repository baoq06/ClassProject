using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClassProject.Repositories
{
    public class AssignRepository
    {
        private readonly My_DB _db = new My_DB();

        public bool Assign(string magv, string mamh)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
INSERT INTO Assign (MaGV, Mamh)
SELECT @MaGV, @Mamh
WHERE NOT EXISTS (
    SELECT 1 FROM Assign
    WHERE MaGV = @MaGV AND Mamh = @Mamh
)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", magv);
                    cmd.Parameters.AddWithValue("@Mamh", mamh);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Unassign(string magv, string mamh)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
DELETE FROM Assign
WHERE MaGV = @MaGV AND Mamh = @Mamh";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", magv);
                    cmd.Parameters.AddWithValue("@Mamh", mamh);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable GetByTeacher(string magv)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT a.*, c.Tenmh, c.Sotc
FROM Assign a
JOIN Courses c ON a.Mamh = c.Mamh
WHERE a.MaGV = @MaGV";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", magv);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GetAll()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT a.*, c.Tenmh, u.LastName + ' ' + u.FirstName AS TenGV
FROM Assign a
JOIN Courses c ON a.Mamh = c.Mamh
JOIN Users u ON a.MaGV = u.Id";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }
    }
}
