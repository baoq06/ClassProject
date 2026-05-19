using ClassProject.Repositories;
using System;
using System.Data;
using System.Windows.Forms;

namespace ClassProject
{
    public partial class UC_ViewStudents : UserControl
    {
        public UC_ViewStudents()
        {
            InitializeComponent();
            EmailMasker.AttachTo(dgvStudents, "Email");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            LoadStudents();
        }

        private void LoadStudents()
        {
            StudentRepository repo = new StudentRepository();
            DataTable dt = repo.GetAll();
            dgvStudents.DataSource = dt;
            ConfigureGrid();
            lblStudentCount.Text = $"Tổng số sinh viên: {dt.Rows.Count}"; // ← thêm
        }

        private void SearchStudents()
        {
            StudentRepository repo = new StudentRepository();
            string keyword = txtSearch.Text.Trim();
            DataTable dt = string.IsNullOrEmpty(keyword)
                ? repo.GetAll()
                : repo.Search(keyword);
            dgvStudents.DataSource = dt;
            ConfigureGrid();
            lblStudentCount.Text = $"Tổng số sinh viên: {dt.Rows.Count}"; // ← thêm
        }

        private void ConfigureGrid()
        {
            if (dgvStudents.Columns.Contains("Id"))
                dgvStudents.Columns["Id"].Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchStudents();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SearchStudents();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadStudents();
        }

        private int? GetSelectedStudentId()
        {
            if (dgvStudents.CurrentRow == null || dgvStudents.CurrentRow.IsNewRow)
                return null;

            if (!dgvStudents.Columns.Contains("Id"))
                return null;

            object val = dgvStudents.CurrentRow.Cells["Id"].Value;
            if (val == null || val == DBNull.Value)
                return null;

            return Convert.ToInt32(val);
        }

        private void EditSelectedStudent()
        {
            int? studentId = GetSelectedStudentId();
            if (!studentId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new AdminEditStudentForm(studentId.Value))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    SearchStudents();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedStudent();
        }

        private void dgvStudents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                EditSelectedStudent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int? studentId = GetSelectedStudentId();
            if (!studentId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mssv = dgvStudents.CurrentRow?.Cells["MSSV"]?.Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Xóa sinh viên MSSV: {mssv}?\n\nTài khoản đăng nhập liên kết cũng sẽ bị xóa.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            StudentRepository repo = new StudentRepository();
            if (repo.DeleteById(studentId.Value))
            {
                MessageBox.Show("Đã xóa sinh viên.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SearchStudents();
            }
        }
    }
}
