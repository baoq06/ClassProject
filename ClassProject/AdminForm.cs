using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;

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

            var confirm = MessageBox.Show(
                "Bạn chắc chắn muốn duyệt sinh viên này?",
                "Xác nhận",
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
                        const string insertStudent = @"
INSERT INTO dbo.Students (Mssv, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture)
SELECT Mssv, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture
FROM dbo.PendingStudents
WHERE Id = @pendingId;";

                        using (SqlCommand cmd = new SqlCommand(insertStudent, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@pendingId", pendingId);
                            int inserted = cmd.ExecuteNonQuery();
                            if (inserted != 1)
                                throw new Exception("Duyệt thất bại: không insert được vào Students.");
                        }

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

            MessageBox.Show("Duyệt thành công. Đã chuyển sang bảng Students.");
            LoadPendingStudents();
        }

        private void LoadPendingStudents()
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = @"
SELECT 
    p.Id AS PendingId,
    p.UserId,
    u.Username,
    u.Email AS AccountEmail,
    p.Mssv,
    p.LastName,
    p.FirstName,
    p.DateOfBirth,
    p.Gender,
    p.Phone,
    p.Address,
    p.Hometown,
    p.Email AS StudentEmail,
    p.SubmittedAt
FROM dbo.PendingStudents p
INNER JOIN dbo.Users u ON u.Id = p.UserId
ORDER BY p.SubmittedAt DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPending.DataSource = dt;

                    if (dgvPending.Columns.Contains("PendingId"))
                        dgvPending.Columns["PendingId"].Visible = false;

                    if (dgvPending.Columns.Contains("UserId"))
                        dgvPending.Columns["UserId"].Visible = false;
                }
            }
        }

        private void EnsurePendingStudentsTable()
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = @"
IF OBJECT_ID('dbo.PendingStudents', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PendingStudents (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        UserId INT NOT NULL,
        Mssv INT NOT NULL,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        DateOfBirth DATETIME NOT NULL,
        Gender NVARCHAR(10) NOT NULL,
        Phone NVARCHAR(20) NOT NULL,
        Address NVARCHAR(255) NULL,
        Hometown NVARCHAR(255) NULL,
        Email NVARCHAR(255) NOT NULL,
        Picture VARBINARY(MAX) NULL,
        SubmittedAt DATETIME NOT NULL CONSTRAINT DF_PendingStudents_SubmittedAt DEFAULT(GETDATE())
    );

    CREATE UNIQUE INDEX UX_PendingStudents_UserId ON dbo.PendingStudents(UserId);
    CREATE UNIQUE INDEX UX_PendingStudents_Mssv ON dbo.PendingStudents(Mssv);
END";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
