using System;
using System.Data;
using System.Windows.Forms;
using ClassProject.Models;
using ClassProject.Repositories;
using ClassProject.Services;

namespace ClassProject
{
    /// <summary>
    /// UserControl cho admin nhập / sửa / xóa điểm cho từng sinh viên.
    /// Flow: chọn sinh viên (lưới trái) -> grid phải hiện môn đã đăng ký
    /// + cột Điểm cho phép sửa -> bấm "Lưu thay đổi" để upsert toàn bộ.
    /// </summary>
    public partial class UC_ManageGrades : UserControl
    {
        private int _selectedStudentId;
        private string _selectedMssv = "";
        private string _selectedFullName = "";

        public UC_ManageGrades()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            LoadStudentList();
        }

        // ==================== TẢI DANH SÁCH SINH VIÊN ====================

        private void LoadStudentList()
        {
            StudentRepository repo = new StudentRepository();
            DataTable dt = repo.GetAll();
            dgvStudents.AutoGenerateColumns = true;
            dgvStudents.DataSource = dt;
            ConfigureStudentGrid();

            if (dt.Rows.Count > 0)
            {
                dgvStudents.ClearSelection();
                dgvStudents.Rows[0].Selected = true;
                SelectStudentFromRow(0);
            }
            else
            {
                ClearGradeGrid();
            }
        }

        private void ConfigureStudentGrid()
        {
            if (dgvStudents.Columns.Contains("Id"))
                dgvStudents.Columns["Id"].Visible = false;

            foreach (string col in new[] { "DateOfBirth", "Address", "Hometown", "Email", "Gender", "Phone" })
            {
                if (dgvStudents.Columns.Contains(col))
                    dgvStudents.Columns[col].Visible = false;
            }

            if (dgvStudents.Columns.Contains("MSSV"))
                dgvStudents.Columns["MSSV"].HeaderText = "MSSV";
            if (dgvStudents.Columns.Contains("FirstName"))
                dgvStudents.Columns["FirstName"].HeaderText = "Tên đệm";
            if (dgvStudents.Columns.Contains("LastName"))
                dgvStudents.Columns["LastName"].HeaderText = "Tên";
        }

        // ==================== TƯƠNG TÁC LƯỚI SINH VIÊN ====================

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null) return;
            SelectStudentFromRow(dgvStudents.CurrentRow.Index);
        }

        private void SelectStudentFromRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvStudents.Rows.Count) return;

            DataGridViewRow row = dgvStudents.Rows[rowIndex];
            if (!dgvStudents.Columns.Contains("Id")) return;

            if (row.Cells["Id"].Value == null || row.Cells["Id"].Value == DBNull.Value)
                return;

            _selectedStudentId = Convert.ToInt32(row.Cells["Id"].Value);
            _selectedMssv = row.Cells["MSSV"]?.Value?.ToString() ?? "";
            string first = row.Cells["FirstName"]?.Value?.ToString() ?? "";
            string last = row.Cells["LastName"]?.Value?.ToString() ?? "";
            _selectedFullName = $"{last} {first}".Trim();

            lblSelectedStudent.Text = $"Sinh viên đang chọn: {_selectedMssv}  -  {_selectedFullName}";
            LoadGrades();
        }

        // ==================== TẢI BẢNG ĐIỂM ====================

        private void LoadGrades()
        {
            if (_selectedStudentId == 0)
            {
                ClearGradeGrid();
                return;
            }

            DataTable dt = Grade.GetRegisteredWithGradesByStudentId(_selectedStudentId);
            dgvGrades.AutoGenerateColumns = true;
            dgvGrades.DataSource = dt;
            ConfigureGradeGrid();

            UpdateGpaLabel();
        }

        private void ClearGradeGrid()
        {
            dgvGrades.DataSource = null;
            lblGpa.Text = "GPA: -";
        }

        private void ConfigureGradeGrid()
        {
            if (dgvGrades.Columns.Contains("CourseId"))
                dgvGrades.Columns["CourseId"].Visible = false;
            if (dgvGrades.Columns.Contains("GradeId"))
                dgvGrades.Columns["GradeId"].Visible = false;

            if (dgvGrades.Columns.Contains("Code"))
            {
                dgvGrades.Columns["Code"].HeaderText = "Mã môn";
                dgvGrades.Columns["Code"].ReadOnly = true;
            }
            if (dgvGrades.Columns.Contains("Name"))
            {
                dgvGrades.Columns["Name"].HeaderText = "Tên môn";
                dgvGrades.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvGrades.Columns["Name"].ReadOnly = true;
            }
            if (dgvGrades.Columns.Contains("Credits"))
            {
                dgvGrades.Columns["Credits"].HeaderText = "Số TC";
                dgvGrades.Columns["Credits"].ReadOnly = true;
            }
            if (dgvGrades.Columns.Contains("Score"))
            {
                dgvGrades.Columns["Score"].HeaderText = "Điểm (0-10)";
                dgvGrades.Columns["Score"].DefaultCellStyle.Format = "0.##";
                dgvGrades.Columns["Score"].ReadOnly = false;
            }
            if (dgvGrades.Columns.Contains("GradedAt"))
            {
                dgvGrades.Columns["GradedAt"].HeaderText = "Lần nhập gần nhất";
                dgvGrades.Columns["GradedAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvGrades.Columns["GradedAt"].ReadOnly = true;
            }
        }

        private void UpdateGpaLabel()
        {
            decimal? gpa = Grade.GetWeightedAverageByStudentId(_selectedStudentId);
            lblGpa.Text = gpa.HasValue
                ? $"GPA (trọng số theo tín chỉ): {gpa.Value:0.00}"
                : "GPA: - (chưa có điểm)";
        }

        // ==================== TÌM KIẾM SINH VIÊN ====================

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

        private void SearchStudents()
        {
            StudentRepository repo = new StudentRepository();
            string keyword = txtSearch.Text.Trim();
            DataTable dt = string.IsNullOrEmpty(keyword)
                ? repo.GetAll()
                : repo.Search(keyword);

            dgvStudents.DataSource = dt;
            ConfigureStudentGrid();

            if (dt.Rows.Count > 0)
            {
                dgvStudents.ClearSelection();
                dgvStudents.Rows[0].Selected = true;
                SelectStudentFromRow(0);
            }
            else
            {
                lblSelectedStudent.Text = "Sinh viên đang chọn: (không tìm thấy)";
                ClearGradeGrid();
            }
        }

        // ==================== LƯU / XÓA / LÀM MỚI ĐIỂM ====================

        /// <summary>
        /// Lưu tất cả các dòng điểm có giá trị trong lưới hiện tại bằng Upsert.
        /// Bỏ qua dòng có ô Score trống.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_selectedStudentId == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên trước.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Đảm bảo commit ô đang edit
            dgvGrades.EndEdit();

            // GradedBy: tạm thời để null (chưa có session admin trong app).
            // Sau này có thể bổ sung 1 SessionContext lưu UserId hiện tại.
            int? gradedBy = null;

            int success = 0;
            int failed = 0;
            int skipped = 0;

            foreach (DataGridViewRow row in dgvGrades.Rows)
            {
                if (row.IsNewRow) continue;

                object courseIdObj = row.Cells["CourseId"].Value;
                object scoreObj = row.Cells["Score"].Value;

                if (courseIdObj == null || courseIdObj == DBNull.Value) continue;

                int courseId = Convert.ToInt32(courseIdObj);

                if (scoreObj == null || scoreObj == DBNull.Value ||
                    string.IsNullOrWhiteSpace(scoreObj.ToString()))
                {
                    skipped++;
                    continue;
                }

                string raw = scoreObj.ToString().Replace(",", ".").Trim();

                if (!decimal.TryParse(raw,
                        System.Globalization.NumberStyles.Number,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal score))
                {
                    failed++;
                    continue;
                }

                if (Grade.Upsert(_selectedStudentId, courseId, score, gradedBy))
                    success++;
                else
                    failed++;
            }

            MessageBox.Show(
                $"Đã lưu: {success}\nThất bại: {failed}\nBỏ qua (trống): {skipped}",
                "Kết quả lưu điểm",
                MessageBoxButtons.OK,
                success > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            LoadGrades();
        }

        /// <summary>
        /// Xóa điểm của môn đang được chọn (không xóa record đăng ký môn).
        /// </summary>
        private void btnDeleteGrade_Click(object sender, EventArgs e)
        {
            if (_selectedStudentId == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvGrades.CurrentRow == null ||
                dgvGrades.CurrentRow.Cells["CourseId"].Value == null ||
                dgvGrades.CurrentRow.Cells["CourseId"].Value == DBNull.Value)
            {
                MessageBox.Show("Vui lòng chọn môn cần xóa điểm.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int courseId = Convert.ToInt32(dgvGrades.CurrentRow.Cells["CourseId"].Value);
            string courseName = dgvGrades.CurrentRow.Cells["Name"].Value?.ToString() ?? "";

            DialogResult confirm = MessageBox.Show(
                $"Xóa điểm môn:\n\n{courseName}\n\ncủa sinh viên {_selectedMssv} - {_selectedFullName}?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            if (Grade.Delete(_selectedStudentId, courseId))
            {
                MessageBox.Show("Đã xóa điểm.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrades();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrades();
        }
    }
}