using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ClassProject
{
    /// <summary>
    /// Đại diện cho một môn học và các thao tác CSDL liên quan
    /// (lấy danh sách, đăng ký, hủy đăng ký, ...).
    /// </summary>
    public class Course
    {
        private int _id;
        private string _code;
        private string _name;
        private int _credits;
        private string _description;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Code
        {
            get => _code;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Mã môn học không được để trống!");
                _code = value.Trim();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Tên môn học không được để trống!");
                _name = value.Trim();
            }
        }

        public int Credits
        {
            get => _credits;
            set
            {
                if (value <= 0)
                    throw new Exception("Số tín chỉ phải lớn hơn 0!");
                _credits = value;
            }
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public Course() { }

        public Course(int id, string code, string name, int credits, string description = null)
        {
            Id = id;
            Code = code;
            Name = name;
            Credits = credits;
            Description = description;
        }

        public override string ToString()
        {
            return $"[{Code}] {Name} - {Credits} tín chỉ";
        }

        // ==================== TRUY VẤN MÔN HỌC ====================

        /// <summary>Lấy toàn bộ danh sách môn học trong hệ thống.</summary>
        public static DataTable GetAll()
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT Id, Code, Name, Credits, Description
FROM dbo.Courses
ORDER BY Code;";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải danh sách môn học: " + ex.Message);
                }
            }

            return dt;
        }

        /// <summary>
        /// Lấy danh sách môn KHẢ DỤNG (chưa đăng ký) cho 1 sinh viên.
        /// </summary>
        public static DataTable GetAvailableForStudent(int studentId)
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT c.Id, c.Code, c.Name, c.Credits, c.Description
FROM dbo.Courses c
WHERE NOT EXISTS (
    SELECT 1
    FROM dbo.StudentCourses sc
    WHERE sc.CourseId = c.Id AND sc.StudentId = @StudentId
)
ORDER BY c.Code;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải môn khả dụng: " + ex.Message);
                }
            }

            return dt;
        }

        /// <summary>
        /// Lấy danh sách môn ĐÃ ĐĂNG KÝ của 1 sinh viên (theo StudentId).
        /// </summary>
        public static DataTable GetRegisteredByStudentId(int studentId)
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT c.Id, c.Code, c.Name, c.Credits, c.Description, sc.RegisteredAt
FROM dbo.StudentCourses sc
INNER JOIN dbo.Courses c ON c.Id = sc.CourseId
WHERE sc.StudentId = @StudentId
ORDER BY sc.RegisteredAt DESC;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải môn đã đăng ký: " + ex.Message);
                }
            }

            return dt;
        }

        /// <summary>
        /// Lấy danh sách môn ĐÃ ĐĂNG KÝ của 1 sinh viên theo UserId
        /// (tiện cho LoginForm và các form chỉ có userId).
        /// </summary>
        public static DataTable GetRegisteredByUserId(int userId)
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT c.Id, c.Code, c.Name, c.Credits, c.Description, sc.RegisteredAt
FROM dbo.StudentCourses sc
INNER JOIN dbo.Courses  c ON c.Id = sc.CourseId
INNER JOIN dbo.Students s ON s.Id = sc.StudentId
WHERE s.UserId = @UserId
ORDER BY sc.RegisteredAt DESC;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải môn đã đăng ký: " + ex.Message);
                }
            }

            return dt;
        }

        // ==================== ĐĂNG KÝ / HỦY ĐĂNG KÝ ====================

        /// <summary>
        /// Tra StudentId từ UserId (mỗi tài khoản sinh viên gắn với 1 record trong Students).
        /// </summary>
        public static int GetStudentIdByUserId(int userId)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                const string sql = "SELECT Id FROM dbo.Students WHERE UserId = @UserId;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        return 0;
                    return Convert.ToInt32(result);
                }
            }
        }

        /// <summary>Sinh viên đăng ký 1 môn (theo StudentId + CourseId).</summary>
        public static bool Register(int studentId, int courseId)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Tránh trùng - dù đã có UNIQUE constraint vẫn check để hiện message thân thiện
                    const string checkSql = @"
SELECT COUNT(*) FROM dbo.StudentCourses
WHERE StudentId = @StudentId AND CourseId = @CourseId;";

                    using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@StudentId", studentId);
                        checkCmd.Parameters.AddWithValue("@CourseId", courseId);

                        if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Bạn đã đăng ký môn học này rồi!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }

                    const string sql = @"
INSERT INTO dbo.StudentCourses (StudentId, CourseId)
VALUES (@StudentId, @CourseId);";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL khi đăng ký môn: " + ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đăng ký môn: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>Sinh viên hủy đăng ký 1 môn.</summary>
        public static bool Unregister(int studentId, int courseId)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
DELETE FROM dbo.StudentCourses
WHERE StudentId = @StudentId AND CourseId = @CourseId;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hủy đăng ký: " + ex.Message);
                    return false;
                }
            }
        }
    }
}