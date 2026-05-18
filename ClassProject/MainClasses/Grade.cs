using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ClassProject
{
    /// <summary>
    /// Đại diện cho điểm của sinh viên trong 1 môn học và các thao tác
    /// CSDL liên quan (xem, nhập/cập nhật, xóa).
    /// Chỉ admin (RoleId = 0) hoặc giảng viên (RoleId = 2, sau này) được
    /// gọi các phương thức ghi.
    /// </summary>
    public class Grade
    {
        private int _id;
        private int _studentId;
        private int _courseId;
        private decimal? _score;
        private DateTime _gradedAt;
        private int? _gradedBy;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int StudentId
        {
            get => _studentId;
            set => _studentId = value;
        }

        public int CourseId
        {
            get => _courseId;
            set => _courseId = value;
        }

        public decimal? Score
        {
            get => _score;
            set
            {
                if (value.HasValue && (value.Value < 0 || value.Value > 10))
                    throw new Exception("Điểm phải nằm trong khoảng 0 - 10!");
                _score = value;
            }
        }

        public DateTime GradedAt
        {
            get => _gradedAt;
            set => _gradedAt = value;
        }

        public int? GradedBy
        {
            get => _gradedBy;
            set => _gradedBy = value;
        }

        public Grade() { }

        public override string ToString()
        {
            string s = Score.HasValue ? Score.Value.ToString("0.00") : "(chưa có)";
            return $"StudentId={StudentId}, CourseId={CourseId}, Score={s}";
        }

        // ==================== TRUY VẤN ĐIỂM ====================

        /// <summary>
        /// Lấy danh sách môn ĐÃ ĐĂNG KÝ của 1 sinh viên kèm điểm (nếu có).
        /// Dùng cho cả màn hình admin nhập điểm và màn hình sinh viên xem điểm.
        /// LEFT JOIN sang Grades để môn chưa có điểm vẫn xuất hiện với Score = NULL.
        /// </summary>
        public static DataTable GetRegisteredWithGradesByStudentId(int studentId)
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT sc.CourseId,
       c.Code,
       c.Name,
       c.Credits,
       g.Id        AS GradeId,
       g.Score,
       g.GradedAt
FROM dbo.StudentCourses sc
INNER JOIN dbo.Courses c ON c.Id = sc.CourseId
LEFT  JOIN dbo.Grades  g ON g.StudentId = sc.StudentId AND g.CourseId = sc.CourseId
WHERE sc.StudentId = @StudentId
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
                    MessageBox.Show("Lỗi tải bảng điểm: " + ex.Message);
                }
            }

            return dt;
        }

        /// <summary>
        /// Lấy danh sách môn + điểm theo UserId (cho màn hình sinh viên đang đăng nhập).
        /// </summary>
        public static DataTable GetRegisteredWithGradesByUserId(int userId)
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT sc.CourseId,
       c.Code,
       c.Name,
       c.Credits,
       g.Id        AS GradeId,
       g.Score,
       g.GradedAt
FROM dbo.Students s
INNER JOIN dbo.StudentCourses sc ON sc.StudentId = s.Id
INNER JOIN dbo.Courses c          ON c.Id = sc.CourseId
LEFT  JOIN dbo.Grades  g          ON g.StudentId = sc.StudentId AND g.CourseId = sc.CourseId
WHERE s.UserId = @UserId
ORDER BY c.Code;";

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
                    MessageBox.Show("Lỗi tải bảng điểm: " + ex.Message);
                }
            }

            return dt;
        }

        // ==================== NHẬP / SỬA / XÓA ĐIỂM ====================

        /// <summary>
        /// Insert mới hoặc update điểm đã có (theo cặp StudentId + CourseId).
        /// Trả về true nếu thành công.
        /// gradedBy: UserId của admin/giảng viên thực hiện thao tác (nullable).
        /// </summary>
        public static bool Upsert(int studentId, int courseId, decimal score, int? gradedBy)
        {
            if (score < 0 || score > 10)
            {
                MessageBox.Show("Điểm phải nằm trong khoảng 0 - 10!",
                    "Sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Đảm bảo sinh viên đã đăng ký môn học mới cho phép nhập điểm
            if (!IsCourseRegistered(studentId, courseId))
            {
                MessageBox.Show("Sinh viên chưa đăng ký môn học này, không thể nhập điểm.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
IF EXISTS (SELECT 1 FROM dbo.Grades WHERE StudentId = @StudentId AND CourseId = @CourseId)
BEGIN
    UPDATE dbo.Grades
       SET Score    = @Score,
           GradedAt = GETDATE(),
           GradedBy = @GradedBy
     WHERE StudentId = @StudentId AND CourseId = @CourseId;
END
ELSE
BEGIN
    INSERT INTO dbo.Grades (StudentId, CourseId, Score, GradedBy)
    VALUES (@StudentId, @CourseId, @Score, @GradedBy);
END";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        cmd.Parameters.AddWithValue("@Score", score);
                        cmd.Parameters.AddWithValue("@GradedBy",
                            gradedBy.HasValue ? (object)gradedBy.Value : DBNull.Value);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL khi nhập điểm: " + ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nhập điểm: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Xóa điểm 1 môn của sinh viên (không xóa record đăng ký).
        /// </summary>
        public static bool Delete(int studentId, int courseId)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
DELETE FROM dbo.Grades
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
                    MessageBox.Show("Lỗi xóa điểm: " + ex.Message);
                    return false;
                }
            }
        }

        // ==================== HELPERS ====================

        /// <summary>
        /// Kiểm tra sinh viên đã đăng ký môn học hay chưa.
        /// </summary>
        public static bool IsCourseRegistered(int studentId, int courseId)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT COUNT(*) FROM dbo.StudentCourses
WHERE StudentId = @StudentId AND CourseId = @CourseId;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        /// <summary>
        /// Tính điểm trung bình có trọng số theo số tín chỉ (GPA thang 10)
        /// cho 1 sinh viên. Bỏ qua môn chưa có điểm.
        /// </summary>
        public static decimal? GetWeightedAverageByStudentId(int studentId)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT CAST(SUM(g.Score * c.Credits) AS DECIMAL(10,4))
       / NULLIF(SUM(c.Credits), 0) AS Gpa
FROM dbo.Grades  g
INNER JOIN dbo.Courses c ON c.Id = g.CourseId
WHERE g.StudentId = @StudentId AND g.Score IS NOT NULL;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        return null;
                    return Convert.ToDecimal(result);
                }
            }
        }
    }
}