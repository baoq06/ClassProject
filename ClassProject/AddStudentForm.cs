using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClassProject
{
    public partial class AddStudentForm : Form
    {
        byte[] studentImage = null;
        private int currentUserId;
        public AddStudentForm(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {

        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files|*.jpg;*.png;*.jpeg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picStudent.Image = Image.FromFile(ofd.FileName);

                MemoryStream ms = new MemoryStream();

                picStudent.Image.Save(ms, picStudent.Image.RawFormat);

                studentImage = ms.ToArray();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMSSV.Text.Trim() == "")
            {
                MessageBox.Show("Nhập MSSV");
                return;
            }

            if (!int.TryParse(txtMSSV.Text, out _))
            {
                MessageBox.Show("MSSV phải là số");
                return;
            }

            if (txtLastName.Text.Trim() == "")
            {
                MessageBox.Show("Nhập tên");
                return;
            }

            if (dtpDateOfBirth.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại!", "Cảnh báo");
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ");
                return;
            }

            if (!IsValidPhone(txtPhone.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                return;
            }

            if (studentImage == null)
            {
                MessageBox.Show("Chọn ảnh");
                return;
            }
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    if (IsMssvExistsInAnyTable(int.Parse(txtMSSV.Text), conn))
                    {
                        MessageBox.Show("Mã số sinh viên này đã tồn tại (Students hoặc Pending)!");
                        return;
                    }

                    EnsurePendingStudentsTable(conn);

                    const string insertPending = @"
INSERT INTO dbo.PendingStudents
(UserId, Mssv, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture)
VALUES
(@UserId, @Mssv, @FirstName, @LastName, @DateOfBirth, @Gender, @Phone, @Address, @Hometown, @Email, @Picture);";

                    using (SqlCommand cmd = new SqlCommand(insertPending, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", currentUserId);
                        cmd.Parameters.AddWithValue("@Mssv", int.Parse(txtMSSV.Text));
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dtpDateOfBirth.Value);
                        cmd.Parameters.AddWithValue("@Gender", cboGender.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text);
                        cmd.Parameters.AddWithValue("@Hometown", string.IsNullOrWhiteSpace(txtHometown.Text) ? (object)DBNull.Value : txtHometown.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Picture", studentImage ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Đã gửi thông tin. Vui lòng chờ Admin duyệt.");
                    btnClear.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi DB: " + ex.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMSSV.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtHometown.Clear();
            txtEmail.Clear();

            cboGender.SelectedIndex = -1;
            cboGender.Text = "";

            dtpDateOfBirth.Value = DateTime.Now;

            picStudent.Image = null;

            studentImage = null;
        }

        bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, pattern);
        }
        bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^[0-9]{10}$");
        }

        private static bool IsMssvExistsInAnyTable(int mssv, SqlConnection conn)
        {
            const string sql = @"
SELECT
    (SELECT COUNT(*) FROM dbo.Students WHERE MSSV = @mssv) +
    (SELECT COUNT(*) FROM dbo.PendingStudents WHERE Mssv = @mssv);";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@mssv", mssv);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private static void EnsurePendingStudentsTable(SqlConnection conn)
        {
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
