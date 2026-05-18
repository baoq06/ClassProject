using ClassProject.DataAccess.Db;
using ClassProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ClassProject.Repositories
{
    public class CourseRepository
    {
        private readonly My_DB _db = new My_DB();

        public DataTable GetAll()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT *
FROM dbo.Courses
ORDER BY Mamh";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GetById(string mamh)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT *
FROM dbo.Courses
WHERE Mamh = @Mamh";

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

        public bool Add(Course course)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
INSERT INTO dbo.Courses
(Mamh, Tenmh, Sotc, Tuan, Hocky, Description)
VALUES
(@Mamh, @Tenmh, @Sotc, @Tuan, @Hocky, @Description)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mamh", course.Mamh);
                    cmd.Parameters.AddWithValue("@Tenmh", course.Tenmh);
                    cmd.Parameters.AddWithValue("@Sotc", course.Sotc);
                    cmd.Parameters.AddWithValue("@Tuan", course.Tuan);
                    cmd.Parameters.AddWithValue("@Hocky", course.Hocky);

                    cmd.Parameters.AddWithValue("@Description",
                        string.IsNullOrWhiteSpace(course.Description)
                        ? DBNull.Value
                        : course.Description);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(Course course)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
UPDATE dbo.Courses
SET
    Tenmh       = @Tenmh,
    Sotc        = @Sotc,
    Tuan        = @Tuan,
    Hocky       = @Hocky,
    Description = @Description
WHERE Mamh = @Mamh";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mamh", course.Mamh);
                    cmd.Parameters.AddWithValue("@Tenmh", course.Tenmh);
                    cmd.Parameters.AddWithValue("@Sotc", course.Sotc);
                    cmd.Parameters.AddWithValue("@Tuan", course.Tuan);
                    cmd.Parameters.AddWithValue("@Hocky", course.Hocky);

                    cmd.Parameters.AddWithValue("@Description",
                        string.IsNullOrWhiteSpace(course.Description)
                        ? DBNull.Value
                        : course.Description);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(string mamh)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql =
                    "DELETE FROM dbo.Courses WHERE Mamh = @Mamh";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mamh", mamh);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable Search(string keyword)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT *
FROM dbo.Courses
WHERE
    Mamh LIKE @kw OR
    Tenmh LIKE @kw
ORDER BY Mamh";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

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
