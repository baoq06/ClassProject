using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClassProject
{
    public partial class UC_AddStudent : UserControl
    {
        private byte[]? _studentImage;

        public UC_AddStudent()
        {
            InitializeComponent();
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.png;*.jpeg";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                picStudent.Image = Image.FromFile(ofd.FileName);
                using (MemoryStream ms = new MemoryStream())
                {
                    picStudent.Image.Save(ms, picStudent.Image.RawFormat);
                    _studentImage = ms.ToArray();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string mssv = txtMSSV.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string firstName = txtFirstName.Text.Trim();

            if (string.IsNullOrEmpty(mssv) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("MSSV và Họ không được để trống.");
                return;
            }

            if (dtpDateOfBirth.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && !IsValidPhone(txtPhone.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ (10 số).");
                return;
            }

            string gender = cboGender.SelectedItem?.ToString() ?? "";
            if (gender != "Nam" && gender != "Nữ")
            {
                MessageBox.Show("Chọn giới tính Nam hoặc Nữ.");
                return;
            }

            bool ok = Student.AddWithNewAccount(
                mssv,
                string.IsNullOrEmpty(firstName) ? lastName : firstName,
                lastName,
                dtpDateOfBirth.Value,
                gender,
                txtPhone.Text.Trim(),
                txtAddress.Text.Trim(),
                txtHometown.Text.Trim(),
                txtEmail.Text.Trim(),
                _studentImage);

            if (ok)
            {
                MessageBox.Show(
                    $"Thêm sinh viên thành công!\nTài khoản:\n- Username: {mssv}\n- Password: {mssv}",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                ClearForm();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtMSSV.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtHometown.Clear();
            txtEmail.Clear();
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-20);
            cboGender.SelectedIndex = -1;
            picStudent.Image = null;
            _studentImage = null;
        }

        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private static bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^[0-9]{10}$");
        }
    }
}
