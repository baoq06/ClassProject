using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using BCrypt.Net;
using ClosedXML.Excel;

namespace ClassProject
{
    public partial class AdminForm : Form
    {
        private readonly My_DB _db = new My_DB();

        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            try
            {
                EnsurePendingStudentsTable();
                LoadPendingStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPendingStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lại: " + ex.Message);
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            Close();
        }

        // ==================== UPLOAD EXCEL ====================
        private void btnUploadExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files|*.xlsx;*.xls";
                ofd.Title = "Chọn file Excel danh sách sinh viên";

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    int inserted = 0;
                    int skipped = 0;
                    var errors = new StringBuilder();

                    using (var workbook = new XLWorkbook(ofd.FileName))
                    {
                        var ws = workbook.Worksheets.First();
                        int lastRow = ws.LastRowUsed()?.RowNumber() ?? 0;

                        if (lastRow < 2)
                        {
                            MessageBox.Show("File Excel trống hoặc chỉ có header!");
                            return;
                        }

                        using (SqlConnection conn = _db.GetConnection())
                        {
                            conn.Open();

                            for (int row = 2; row <= lastRow; row++)
                            {
                                try
                                {
                                    string mssv = ws.Cell(row, 1).GetString().Trim();
                                    string lastName = ws.Cell(row, 2).GetString().Trim();
                                    string firstName = ws.Cell(row, 3).GetString().Trim();

                                    DateTime? dob = null;
                                    try { dob = ws.Cell(row, 4).GetDateTime(); } catch { }

                                    string gender = ws.Cell(row, 5).GetString().Trim();
                                    string phone = ws.Cell(row, 6).GetString().Trim();
                                    string address = ws.Cell(row, 7).GetString().Trim();
                                    string hometown = ws.Cell(row, 8).GetString().Trim();
                                    string email = ws.Cell(row, 9).GetString().Trim();

                                    if (string.IsNullOrEmpty(mssv) || string.IsNullOrEmpty(lastName))
                                    {
                                        skipped++;
                                        errors.AppendLine($"Dòng {row}: Thiếu MSSV hoặc Họ");
                                        continue;
                                    }

                                    // Kiểm tra trùng MSSV trong cả Students và PendingStudents
                                    if (IsMssvExists(mssv, conn))
                                    {
                                        skipped++;
                                        errors.AppendLine($"Dòng {row}: MSSV {mssv} đã tồn tại");
                                        continue;
                                    }

                                    const string sql = @"
INSERT INTO dbo.PendingStudents 
    (Mssv, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email)
VALUES 
    (@Mssv, @FirstName, @LastName, @DateOfBirth, @Gender, @Phone, @Address, @Hometown, @Email);";

                                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                                    {
                                        cmd.Parameters.AddWithValue("@Mssv", mssv);
                                        cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(firstName) ? (object)DBNull.Value : firstName);
                                        cmd.Parameters.AddWithValue("@LastName", lastName);
                                        cmd.Parameters.AddWithValue("@DateOfBirth", dob.HasValue ? (object)dob.Value : DBNull.Value);
                                        cmd.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender) ? (object)DBNull.Value : gender);
                                        cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                                        cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);
                                        cmd.Parameters.AddWithValue("@Hometown", string.IsNullOrEmpty(hometown) ? (object)DBNull.Value : hometown);
                                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);

                                        cmd.ExecuteNonQuery();
                                    }

                                    inserted++;
                                }
                                catch (Exception ex)
                                {
                                    skipped++;
                                    errors.AppendLine($"Dòng {row}: {ex.Message}");
                                }
                            }
                        }
                    }

                    string msg = $"Đã thêm: {inserted} sinh viên\nBỏ qua: {skipped}";
                    if (errors.Length > 0)
                        msg += $"\n\nChi tiết lỗi:\n{errors}";

                    MessageBox.Show(msg, "Kết quả upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPendingStudents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đọc file Excel: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==================== APPROVE ====================
        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvPending.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 sinh viên để duyệt.");
                return;
            }

            if (!int.TryParse(dgvPending.CurrentRow.Cells["PendingId"]?.Value?.ToString(), out int pendingId))
            {
                MessageBox.Show("Không đọc được PendingId.");
                return;
            }

            string mssv = dgvPending.CurrentRow.Cells["Mssv"]?.Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Duyệt sinh viên MSSV: {mssv}?\n\nHệ thống sẽ tự động tạo tài khoản:\n- Username: {mssv}\n- Password: {mssv}",
                "Xác nhận duyệt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Đọc email từ PendingStudents
                        string email = "";
                        using (SqlCommand readCmd = new SqlCommand(
                            "SELECT Email FROM dbo.PendingStudents WHERE Id = @id", conn, tx))
                        {
                            readCmd.Parameters.AddWithValue("@id", pendingId);
                            var result = readCmd.ExecuteScalar();
                            email = result?.ToString() ?? "";
                            if (string.IsNullOrEmpty(email))
                                email = $"{mssv}@student.local";
                        }

                        // 2. Tạo User account (Username = MSSV, Password = MSSV)
                        int newUserId;
                        const string insertUser = @"
INSERT INTO dbo.Users (Username, Email, Password, RoleId)
OUTPUT INSERTED.Id
VALUES (@username, @email, @password, @roleId);";

                        using (SqlCommand cmd = new SqlCommand(insertUser, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@username", mssv);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(mssv));
                            cmd.Parameters.AddWithValue("@roleId", 1); // Student
                            newUserId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 3. Chuyển từ PendingStudents → Students (kèm UserId)
                        const string insertStudent = @"
INSERT INTO dbo.Students (UserId, MSSV, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture)
SELECT @userId, Mssv, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture
FROM dbo.PendingStudents
WHERE Id = @pendingId;";

                        using (SqlCommand cmd = new SqlCommand(insertStudent, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@userId", newUserId);
                            cmd.Parameters.AddWithValue("@pendingId", pendingId);
                            int inserted = cmd.ExecuteNonQuery();
                            if (inserted != 1)
                                throw new Exception("Không insert được vào Students.");
                        }

                        // 4. Xóa khỏi PendingStudents
                        const string deletePending = "DELETE FROM dbo.PendingStudents WHERE Id = @pendingId;";
                        using (SqlCommand cmd = new SqlCommand(deletePending, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@pendingId", pendingId);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        try { tx.Rollback(); } catch { }
                        throw;
                    }
                }
            }

            MessageBox.Show(
                $"Duyệt thành công!\n\nTài khoản đã tạo:\n- Username: {mssv}\n- Password: {mssv}\n\nSinh viên có thể đăng nhập ngay.",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            LoadPendingStudents();
        }

        // ==================== REJECT ====================
        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvPending.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 sinh viên để từ chối.");
                return;
            }

            if (!int.TryParse(dgvPending.CurrentRow.Cells["PendingId"]?.Value?.ToString(), out int pendingId))
            {
                MessageBox.Show("Không đọc được PendingId.");
                return;
            }

            string mssv = dgvPending.CurrentRow.Cells["Mssv"]?.Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Bạn chắc chắn muốn từ chối sinh viên MSSV: {mssv}?",
                "Xác nhận từ chối",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = "DELETE FROM dbo.PendingStudents WHERE Id = @pendingId;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pendingId", pendingId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Đã từ chối và xóa khỏi danh sách pending.");
            LoadPendingStudents();
        }

        // ==================== LOAD DATA ====================
        private void LoadPendingStudents()
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = @"
SELECT 
    Id AS PendingId,
    Mssv,
    LastName,
    FirstName,
    DateOfBirth,
    Gender,
    Phone,
    Address,
    Hometown,
    Email,
    SubmittedAt
FROM dbo.PendingStudents
ORDER BY SubmittedAt DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPending.DataSource = dt;

                    if (dgvPending.Columns.Contains("PendingId"))
                        dgvPending.Columns["PendingId"].Visible = false;
                }
            }
        }

        // ==================== HELPERS ====================
        private void EnsurePendingStudentsTable()
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                EnsurePendingStudentsTable(conn);
            }
        }

        private void EnsurePendingStudentsTable(SqlConnection conn)
        {
            // Xóa bảng cũ nếu có schema cũ (có cột UserId)
            const string dropOld = @"
IF OBJECT_ID('dbo.PendingStudents', 'U') IS NOT NULL
AND EXISTS (SELECT 1 FROM sys.columns 
            WHERE object_id = OBJECT_ID('dbo.PendingStudents') AND name = 'UserId')
BEGIN
    DROP TABLE dbo.PendingStudents;
END";

            using (SqlCommand cmd = new SqlCommand(dropOld, conn))
            {
                cmd.ExecuteNonQuery();
            }

            // Tạo bảng mới (không có UserId)
            const string sql = @"
IF OBJECT_ID('dbo.PendingStudents', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PendingStudents (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Mssv NVARCHAR(30) NOT NULL,
        FirstName NVARCHAR(100) NULL,
        LastName NVARCHAR(100) NOT NULL,
        DateOfBirth DATETIME NULL,
        Gender NVARCHAR(10) NULL,
        Phone NVARCHAR(20) NULL,
        Address NVARCHAR(255) NULL,
        Hometown NVARCHAR(255) NULL,
        Email NVARCHAR(255) NULL,
        Picture VARBINARY(MAX) NULL,
        SubmittedAt DATETIME NOT NULL CONSTRAINT DF_PendingStudents_SubmittedAt DEFAULT(GETDATE())
    );

    CREATE UNIQUE INDEX UX_PendingStudents_Mssv ON dbo.PendingStudents(Mssv);
END";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private static bool IsMssvExists(string mssv, SqlConnection conn)
        {
            const string sql = @"
SELECT
    (SELECT COUNT(*) FROM dbo.Students WHERE MSSV = @mssv) +
    (CASE WHEN OBJECT_ID('dbo.PendingStudents', 'U') IS NOT NULL
          THEN (SELECT COUNT(*) FROM dbo.PendingStudents WHERE Mssv = @mssv)
          ELSE 0 END);";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@mssv", mssv);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
    }
}

