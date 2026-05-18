using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ClassProject.Repositories
{
    public class ScoreRepository
    {
        private readonly My_DB _db = new My_DB();

        public bool AddOrUpdate(string mssv, string mamh, decimal diemqt, decimal diemck, string mota)
        {
            decimal diemtk = Math.Round(diemqt * 0.4m + diemck * 0.6m, 2);

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
IF EXISTS (SELECT 1 FROM dbo.Scores WHERE Mssv = @Mssv AND Mamh = @Mamh)
BEGIN
    UPDATE dbo.Scores
    SET
        Diemqt = @Diemqt,
        Diemck = @Diemck,
        Diemtk = @Diemtk,
        Mota   = @Mota
    WHERE Mssv = @Mssv AND Mamh = @Mamh
END
ELSE
BEGIN
    INSERT INTO dbo.Scores (Mssv, Mamh, Diemqt, Diemck, Diemtk, Mota)
    VALUES (@Mssv, @Mamh, @Diemqt, @Diemck, @Diemtk, @Mota)
END";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mssv", mssv);
                    cmd.Parameters.AddWithValue("@Mamh", mamh);
                    cmd.Parameters.AddWithValue("@Diemqt", diemqt);
                    cmd.Parameters.AddWithValue("@Diemck", diemck);
                    cmd.Parameters.AddWithValue("@Diemtk", diemtk);
                    cmd.Parameters.AddWithValue("@Mota",
                        string.IsNullOrWhiteSpace(mota) ? DBNull.Value : mota);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(string mssv, string mamh)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
DELETE FROM dbo.Scores
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
SELECT
    s.*,
    c.Tenmh,
    c.Sotc
FROM dbo.Scores s
JOIN dbo.Courses c ON s.Mamh = c.Mamh
WHERE s.Mssv = @Mssv
ORDER BY c.Tenmh";

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
SELECT
    s.*,
    st.LastName + ' ' + st.FirstName AS TenSV
FROM dbo.Scores s
JOIN dbo.Students st ON s.Mssv = st.MSSV
WHERE s.Mamh = @Mamh
ORDER BY st.LastName";

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

        public DataTable GetAverageByStudent(string mssv)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT
    s.Mssv,
    AVG(s.Diemtk) AS DTB,
    SUM(c.Sotc) AS TongTC
FROM dbo.Scores s
JOIN dbo.Courses c ON s.Mamh = c.Mamh
WHERE s.Mssv = @Mssv
GROUP BY s.Mssv";

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
    }
}
