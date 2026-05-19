using ClassProject.Repositories;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClassProject
{
    public partial class AdminEditStudentForm : Form
    {
        private readonly int _studentId;
        private byte[]? _studentImage;

        public AdminEditStudentForm(int studentId)
        {
            _studentId = studentId;
            InitializeComponent();
        }

        private void AdminEditStudentForm_Load(object sender, EventArgs e)
        {
            StudentRepository repo = new StudentRepository();
            DataTable dt = repo.GetById(_studentId);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sinh viên.");
                DialogResult = DialogResult.Cancel;
                Close();
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
            int idx = cboGender.FindStringExact(gender);
            if (idx >= 0) cboGender.SelectedIndex = idx;

            txtPhone.Text = row["Phone"]?.ToString() ?? "";
            txtAddress.Text = row["Address"]?.ToString() ?? "";
            txtHometown.Text = row["Hometown"]?.ToString() ?? "";
            txtEmail.Text = row["Email"]?.ToString() ?? "";

            if (row["Picture"] != DBNull.Value)
            {
                _studentImage = (byte[])row["Picture"];
                using (MemoryStream ms = new MemoryStream(_studentImage))
                {
                    picStudent.Image = Image.FromStream(ms);
                }
            }
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
            if (txtLastName.Text.Trim() == "")
            {
                MessageBox.Show("Họ không được để trống.");
                return;
            }

            if (dtpDateOfBirth.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ.");
                return;
            }

            string gender = cboGender.SelectedItem?.ToString() ?? "";
            if (gender != "Nam" && gender != "Nữ")
            {
                MessageBox.Show("Chọn giới tính.");
                return;
            }

            StudentRepository repo = new StudentRepository();
            bool ok = repo.UpdateById(
                _studentId,
                txtFirstName.Text.Trim(),
                txtLastName.Text.Trim(),
                dtpDateOfBirth.Value,
                gender,
                txtPhone.Text.Trim(),
                txtAddress.Text.Trim(),
                txtHometown.Text.Trim(),
                txtEmail.Text.Trim(),
                _studentImage);

            if (ok)
            {
                MessageBox.Show("Cập nhật thành công.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}