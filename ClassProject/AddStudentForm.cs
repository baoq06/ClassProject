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

        private void AddStudentForm_Load(object sender, EventArgs e)
        {
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            try
            {
                DataTable dt = Student.GetByUserId(currentUserId);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy thông tin sinh viên cho tài khoản này.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DataRow row = dt.Rows[0];

                txtMSSV.Text = row["MSSV"]?.ToString() ?? "";
                txtMSSV.ReadOnly = true;
                txtMSSV.BackColor = Color.LightGray;

                txtFirstName.Text = row["FirstName"]?.ToString() ?? "";
                txtLastName.Text = row["LastName"]?.ToString() ?? "";

                if (row["DateOfBirth"] != DBNull.Value)
                    dtpDateOfBirth.Value = Convert.ToDateTime(row["DateOfBirth"]);

                string gender = row["Gender"]?.ToString() ?? "";
                int genderIndex = cboGender.FindStringExact(gender);
                if (genderIndex >= 0)
                    cboGender.SelectedIndex = genderIndex;

                txtPhone.Text = row["Phone"]?.ToString() ?? "";
                txtAddress.Text = row["Address"]?.ToString() ?? "";
                txtHometown.Text = row["Hometown"]?.ToString() ?? "";
                txtEmail.Text = row["Email"]?.ToString() ?? "";

                if (row["Picture"] != DBNull.Value)
                {
                    studentImage = (byte[])row["Picture"];
                    using (MemoryStream ms = new MemoryStream(studentImage))
                    {
                        picStudent.Image = Image.FromStream(ms);
                    }
                }

                btnAdd.Text = "CẬP NHẬT";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
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

            try
            {
                bool ok = Student.UpdateByUserId(
                    currentUserId,
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    dtpDateOfBirth.Value,
                    cboGender.Text,
                    txtPhone.Text.Trim(),
                    txtAddress.Text.Trim(),
                    txtHometown.Text.Trim(),
                    txtEmail.Text.Trim(),
                    studentImage);

                if (ok)
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi DB: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LoadStudentData();
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
    }
}
