using System;
using System.Data;
using System.Windows.Forms;

namespace ClassProject
{
    /// <summary>
    /// Form chỉ-đọc cho sinh viên xem điểm các môn đã đăng ký.
    /// Nhận UserId của tài khoản đang đăng nhập, tự tra StudentId và bảng điểm.
    /// </summary>
    public partial class ViewGradesForm : Form
    {
        private readonly int _userId;
        private int _studentId;

        public ViewGradesForm(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void ViewGradesForm_Load(object sender, EventArgs e)
        {
            _studentId = Course.GetStudentIdByUserId(_userId);

            if (_studentId == 0)
            {
                MessageBox.Show(
                    "Không tìm thấy hồ sơ sinh viên cho tài khoản này.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LoadGrades();
        }

        private void LoadGrades()
        {
            DataTable dt = Grade.GetRegisteredWithGradesByUserId(_userId);
            dgvGrades.AutoGenerateColumns = true;
            dgvGrades.DataSource = dt;
            ConfigureGrid();

            // Thống kê
            int total = dt.Rows.Count;
            int graded = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["Score"] != DBNull.Value) graded++;
            }
            lblStats.Text = $"Tổng số môn đã đăng ký: {total}   |   Đã có điểm: {graded}   |   Chưa có điểm: {total - graded}";

            decimal? gpa = Grade.GetWeightedAverageByStudentId(_studentId);
            lblGpa.Text = gpa.HasValue
                ? $"Điểm trung bình (theo tín chỉ): {gpa.Value:0.00} / 10"
                : "Điểm trung bình: - (chưa có điểm nào)";
        }

        private void ConfigureGrid()
        {
            if (dgvGrades.Columns.Contains("CourseId"))
                dgvGrades.Columns["CourseId"].Visible = false;
            if (dgvGrades.Columns.Contains("GradeId"))
                dgvGrades.Columns["GradeId"].Visible = false;

            if (dgvGrades.Columns.Contains("Code"))
                dgvGrades.Columns["Code"].HeaderText = "Mã môn";

            if (dgvGrades.Columns.Contains("Name"))
            {
                dgvGrades.Columns["Name"].HeaderText = "Tên môn";
                dgvGrades.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dgvGrades.Columns.Contains("Credits"))
                dgvGrades.Columns["Credits"].HeaderText = "Số TC";

            if (dgvGrades.Columns.Contains("Score"))
            {
                dgvGrades.Columns["Score"].HeaderText = "Điểm";
                dgvGrades.Columns["Score"].DefaultCellStyle.Format = "0.##";
                dgvGrades.Columns["Score"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvGrades.Columns.Contains("GradedAt"))
            {
                dgvGrades.Columns["GradedAt"].HeaderText = "Cập nhật";
                dgvGrades.Columns["GradedAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            dgvGrades.ReadOnly = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrades();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}