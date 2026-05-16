using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
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

        // ==================== FORM LOAD: Tải dữ liệu từ Students ====================
        private void AddStudentForm_Load(object sender, EventArgs e)
        {
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT MSSV, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture
FROM dbo.Students
WHERE UserId = @UserId;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", currentUserId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Điền dữ liệu vào form
                                txtMSSV.Text = reader["MSSV"]?.ToString() ?? "";
                                txtMSSV.ReadOnly = true; // MSSV không cho sửa
                                txtMSSV.BackColor = Color.LightGray;

                                txtFirstName.Text = reader["FirstName"]?.ToString() ?? "";
                                txtLastName.Text = reader["LastName"]?.ToString() ?? "";

                                if (reader["DateOfBirth"] != DBNull.Value)
                                    dtpDateOfBirth.Value = Convert.ToDateTime(reader["DateOfBirth"]);

                                string gender = reader["Gender"]?.ToString() ?? "";
                                int genderIndex = cboGender.FindStringExact(gender);
                                if (genderIndex >= 0)
                                    cboGender.SelectedIndex = genderIndex;

                                txtPhone.Text = reader["Phone"]?.ToString() ?? "";
                                txtAddress.Text = reader["Address"]?.ToString() ?? "";
                                txtHometown.Text = reader["Hometown"]?.ToString() ?? "";
                                txtEmail.Text = reader["Email"]?.ToString() ?? "";

                                // Tải ảnh nếu có
                                if (reader["Picture"] != DBNull.Value)
                                {
                                    studentImage = (byte[])reader["Picture"];
                                    using (MemoryStream ms = new MemoryStream(studentImage))
                                    {
                                        picStudent.Image = Image.FromStream(ms);
                                    }
                                }

                                // Đổi text nút thành "CẬP NHẬT"
                                btnAdd.Text = "CẬP NHẬT";
                            }
                            else
                            {
                                // Không tìm thấy record → sinh viên chưa có dữ liệu
                                MessageBox.Show(
                                    "Không tìm thấy thông tin sinh viên cho tài khoản này.",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
                }
            }
        }

        // ==================== CHỌN ẢNH ====================
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

        // ==================== CẬP NHẬT THÔNG TIN ====================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validate
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

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && !IsValidPhone(txtPhone.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                return;
            }

            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string updateSql = @"
UPDATE dbo.Students
SET FirstName   = @FirstName,
    LastName    = @LastName,
    DateOfBirth = @DateOfBirth,
    Gender      = @Gender,
    Phone       = @Phone,
    Address     = @Address,
    Hometown    = @Hometown,
    Email       = @Email,
    Picture     = @Picture
WHERE UserId = @UserId;";

                    using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                        cmd.Parameters.AddWithValue("@DateOfBirth", dtpDateOfBirth.Value);
                        cmd.Parameters.AddWithValue("@Gender", cboGender.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Hometown", string.IsNullOrWhiteSpace(txtHometown.Text) ? (object)DBNull.Value : txtHometown.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Picture", studentImage ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UserId", currentUserId);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin thành công!", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy record để cập nhật.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi DB: " + ex.Message);
                }
            }
        }

        // ==================== CLEAR ====================
        private void btnClear_Click(object sender, EventArgs e)
        {
            // Reload dữ liệu gốc từ DB thay vì xóa trắng
            LoadStudentData();
        }

        // ==================== VALIDATION ====================
        bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^[0-9]{10}$");
        }
    }
}

