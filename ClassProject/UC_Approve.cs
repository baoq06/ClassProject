using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace ClassProject
{
    public partial class UC_Approve : UserControl
    {
        public UC_Approve()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            try
            {
                Student.EnsurePendingStudentsTable();
                LoadPendingStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPendingStudents();
        }

        private void btnUploadExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files|*.xlsx;*.xls";
                ofd.Title = "Chọn file Excel danh sách sinh viên";

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    int inserted = 0;
                    int skipped = 0;
                    var errors = new StringBuilder();

                    using (var workbook = new XLWorkbook(ofd.FileName))
                    {
                        var ws = workbook.Worksheets.First();
                        int lastRow = ws.LastRowUsed()?.RowNumber() ?? 0;

                        if (lastRow < 2)
                        {
                            MessageBox.Show("File Excel trống hoặc chỉ có header!");
                            return;
                        }

                        for (int row = 2; row <= lastRow; row++)
                        {
                            try
                            {
                                string mssv = ws.Cell(row, 1).GetString().Trim();
                                string lastName = ws.Cell(row, 2).GetString().Trim();
                                string firstName = ws.Cell(row, 3).GetString().Trim();

                                DateTime? dob = null;
                                try { dob = ws.Cell(row, 4).GetDateTime(); } catch { }

                                string gender = ws.Cell(row, 5).GetString().Trim();
                                string phone = ws.Cell(row, 6).GetString().Trim();
                                string address = ws.Cell(row, 7).GetString().Trim();
                                string hometown = ws.Cell(row, 8).GetString().Trim();
                                string email = ws.Cell(row, 9).GetString().Trim();

                                if (string.IsNullOrEmpty(mssv) || string.IsNullOrEmpty(lastName))
                                {
                                    skipped++;
                                    errors.AppendLine($"Dòng {row}: Thiếu MSSV hoặc Họ");
                                    continue;
                                }

                                if (Student.IsMssvExistsInSystem(mssv))
                                {
                                    skipped++;
                                    errors.AppendLine($"Dòng {row}: MSSV {mssv} đã tồn tại");
                                    continue;
                                }

                                Student.InsertPending(mssv, firstName, lastName, dob, gender, phone, address, hometown, email);
                                inserted++;
                            }
                            catch (Exception ex)
                            {
                                skipped++;
                                errors.AppendLine($"Dòng {row}: {ex.Message}");
                            }
                        }
                    }

                    string msg = $"Đã thêm: {inserted} sinh viên\nBỏ qua: {skipped}";
                    if (errors.Length > 0)
                        msg += $"\n\nChi tiết lỗi:\n{errors}";

                    MessageBox.Show(msg, "Kết quả upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPendingStudents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đọc file Excel: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

            string mssv = dgvPending.CurrentRow.Cells["Mssv"]?.Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Duyệt sinh viên MSSV: {mssv}?\n\nHệ thống sẽ tự động tạo tài khoản:\n- Username: {mssv}\n- Password: {mssv}",
                "Xác nhận duyệt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                Student.ApprovePending(pendingId, mssv, out _);

                MessageBox.Show(
                    $"Duyệt thành công!\n\nTài khoản đã tạo:\n- Username: {mssv}\n- Password: {mssv}",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadPendingStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi duyệt: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvPending.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 sinh viên để từ chối.");
                return;
            }

            if (!int.TryParse(dgvPending.CurrentRow.Cells["PendingId"]?.Value?.ToString(), out int pendingId))
            {
                MessageBox.Show("Không đọc được PendingId.");
                return;
            }

            string mssv = dgvPending.CurrentRow.Cells["Mssv"]?.Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Bạn chắc chắn muốn từ chối sinh viên MSSV: {mssv}?",
                "Xác nhận từ chối",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            Student.RejectPending(pendingId);
            MessageBox.Show("Đã từ chối và xóa khỏi danh sách pending.");
            LoadPendingStudents();
        }

        private void LoadPendingStudents()
        {
            DataTable dt = Student.GetAllPending();
            dgvPending.DataSource = dt;

            if (dgvPending.Columns.Contains("PendingId"))
                dgvPending.Columns["PendingId"].Visible = false;
        }
    }
}
