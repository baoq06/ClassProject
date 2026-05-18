using System;
using System.Data;
using System.Windows.Forms;

namespace ClassProject
{
    /// <summary>
    /// Form đăng ký môn học của sinh viên.
    /// - Bên trái: môn học khả dụng (chưa đăng ký) -> click "Đăng ký".
    /// - Bên phải: môn đã đăng ký -> click "Hủy đăng ký".
    /// </summary>
    public partial class CourseRegistrationForm : Form
    {
        private readonly int _userId;
        private int _studentId;

        public CourseRegistrationForm(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void CourseRegistrationForm_Load(object sender, EventArgs e)
        {
            // Quy đổi UserId -> StudentId. Mọi thao tác đăng ký dùng StudentId.
            _studentId = Course.GetStudentIdByUserId(_userId);

            if (_studentId == 0)
            {
                MessageBox.Show(
                    "Không tìm thấy hồ sơ sinh viên cho tài khoản này.\n" +
                    "Vui lòng liên hệ admin để được duyệt hồ sơ trước khi đăng ký môn.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            ReloadGrids();
        }

        private void ReloadGrids()
        {
            // Môn khả dụng
            DataTable available = Course.GetAvailableForStudent(_studentId);
            dgvAvailable.DataSource = available;
            BeautifyAvailableGrid();

            // Môn đã đăng ký
            DataTable registered = Course.GetRegisteredByStudentId(_studentId);
            dgvRegistered.DataSource = registered;
            BeautifyRegisteredGrid();

            // Cập nhật label tổng tín chỉ đã đăng ký
            int totalCredits = 0;
            foreach (DataRow r in registered.Rows)
            {
                if (r["Credits"] != DBNull.Value)
                    totalCredits += Convert.ToInt32(r["Credits"]);
            }
            lblTotalCredits.Text = $"Tổng số môn: {registered.Rows.Count}   |   Tổng tín chỉ: {totalCredits}";
        }

        private void BeautifyAvailableGrid()
        {
            if (dgvAvailable.Columns["Id"] != null)
                dgvAvailable.Columns["Id"].Visible = false;
            if (dgvAvailable.Columns["Code"] != null)
                dgvAvailable.Columns["Code"].HeaderText = "Mã môn";
            if (dgvAvailable.Columns["Name"] != null)
            {
                dgvAvailable.Columns["Name"].HeaderText = "Tên môn";
                dgvAvailable.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvAvailable.Columns["Credits"] != null)
                dgvAvailable.Columns["Credits"].HeaderText = "Số TC";
            if (dgvAvailable.Columns["Description"] != null)
                dgvAvailable.Columns["Description"].HeaderText = "Mô tả";
        }

        private void BeautifyRegisteredGrid()
        {
            if (dgvRegistered.Columns["Id"] != null)
                dgvRegistered.Columns["Id"].Visible = false;
            if (dgvRegistered.Columns["Code"] != null)
                dgvRegistered.Columns["Code"].HeaderText = "Mã môn";
            if (dgvRegistered.Columns["Name"] != null)
            {
                dgvRegistered.Columns["Name"].HeaderText = "Tên môn";
                dgvRegistered.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvRegistered.Columns["Credits"] != null)
                dgvRegistered.Columns["Credits"].HeaderText = "Số TC";
            if (dgvRegistered.Columns["Description"] != null)
                dgvRegistered.Columns["Description"].Visible = false;
            if (dgvRegistered.Columns["RegisteredAt"] != null)
            {
                dgvRegistered.Columns["RegisteredAt"].HeaderText = "Ngày ĐK";
                dgvRegistered.Columns["RegisteredAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (dgvAvailable.CurrentRow == null || dgvAvailable.CurrentRow.Cells["Id"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn một môn học để đăng ký.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int courseId = Convert.ToInt32(dgvAvailable.CurrentRow.Cells["Id"].Value);
            string courseName = dgvAvailable.CurrentRow.Cells["Name"].Value?.ToString() ?? "";

            DialogResult confirm = MessageBox.Show(
                $"Xác nhận đăng ký môn:\n\n{courseName}?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            if (Course.Register(_studentId, courseId))
            {
                MessageBox.Show("Đăng ký môn học thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadGrids();
            }
        }

        private void btnUnregister_Click(object sender, EventArgs e)
        {
            if (dgvRegistered.CurrentRow == null || dgvRegistered.CurrentRow.Cells["Id"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn môn học cần hủy đăng ký.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int courseId = Convert.ToInt32(dgvRegistered.CurrentRow.Cells["Id"].Value);
            string courseName = dgvRegistered.CurrentRow.Cells["Name"].Value?.ToString() ?? "";

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc muốn hủy đăng ký môn:\n\n{courseName}?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            if (Course.Unregister(_studentId, courseId))
            {
                MessageBox.Show("Đã hủy đăng ký môn học.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadGrids();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadGrids();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}