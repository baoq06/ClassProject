using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClassProject
{
    /// <summary>
    /// Form ép sinh viên đổi mật khẩu lần đầu sau khi đăng nhập.
    /// Mở bằng ShowDialog() ngay trong LoginForm khi
    /// Users.GetMustChangePassword(userId) trả về true.
    ///
    /// Tiêu chuẩn mật khẩu mới (kiểm tra realtime bằng Regex):
    /// - Tối thiểu 8 ký tự
    /// - Có ít nhất 1 chữ HOA
    /// - Có ít nhất 1 chữ SỐ
    /// - Có ít nhất 1 ký tự đặc biệt (không phải chữ / số)
    /// - Có ô Confirm Password so sánh realtime
    ///
    /// Trả DialogResult.OK khi đổi thành công, Cancel khi user hủy.
    /// </summary>
    public partial class ChangePasswordForm : Form
    {
        private readonly int _userId;
        private readonly string _username;

        // 4 regex tách rời để check từng tiêu chí riêng (hiện checklist).
        private static readonly Regex RxLength = new Regex(@".{8,}");
        private static readonly Regex RxUppercase = new Regex(@"[A-Z]");
        private static readonly Regex RxDigit = new Regex(@"\d");
        private static readonly Regex RxSpecial = new Regex(@"[^A-Za-z0-9]");

        public ChangePasswordForm(int userId, string username)
        {
            InitializeComponent();
            _userId = userId;
            _username = username ?? "";

            lblHello.Text = string.IsNullOrEmpty(_username)
                ? "Bạn cần đổi mật khẩu trước khi sử dụng hệ thống."
                : "Xin chào! Bạn cần đổi mật khẩu trước khi tiếp tục.";

            // Validate ban đầu để disable nút Save và set màu đỏ
            ValidateAll();
        }

        // ==================== REALTIME VALIDATE ====================

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateAll();
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateAll();
        }

        /// <summary>
        /// Kiểm tra toàn bộ điều kiện và cập nhật UI (checklist + match label
        /// + enable/disable btnSave). Gọi mỗi lần text thay đổi.
        /// </summary>
        private void ValidateAll()
        {
            string pwd = txtNewPassword.Text ?? "";
            string confirm = txtConfirmPassword.Text ?? "";

            bool okLength = RxLength.IsMatch(pwd);
            bool okUppercase = RxUppercase.IsMatch(pwd);
            bool okDigit = RxDigit.IsMatch(pwd);
            bool okSpecial = RxSpecial.IsMatch(pwd);

            SetCheckLabel(lblRuleLength, okLength, "Tối thiểu 8 ký tự");
            SetCheckLabel(lblRuleUppercase, okUppercase, "Có ít nhất 1 chữ HOA (A-Z)");
            SetCheckLabel(lblRuleDigit, okDigit, "Có ít nhất 1 chữ số (0-9)");
            SetCheckLabel(lblRuleSpecial, okSpecial, "Có ít nhất 1 ký tự đặc biệt");

            // Match check chỉ hiển thị khi user đã nhập gì đó vào ô confirm
            bool okMatch;
            if (confirm.Length == 0)
            {
                lblMatch.Text = "(chưa nhập xác nhận)";
                lblMatch.ForeColor = Color.Gray;
                okMatch = false;
            }
            else if (pwd == confirm)
            {
                lblMatch.Text = "✓ Mật khẩu khớp";
                lblMatch.ForeColor = Color.ForestGreen;
                okMatch = true;
            }
            else
            {
                lblMatch.Text = "✗ Mật khẩu không khớp";
                lblMatch.ForeColor = Color.Firebrick;
                okMatch = false;
            }

            bool allOk = okLength && okUppercase && okDigit && okSpecial && okMatch;
            btnSave.Enabled = allOk;
            btnSave.BackColor = allOk
                ? Color.ForestGreen
                : Color.FromArgb(180, 180, 180);
        }

        private static void SetCheckLabel(Label lbl, bool ok, string ruleText)
        {
            if (ok)
            {
                lbl.Text = "✓  " + ruleText;
                lbl.ForeColor = Color.ForestGreen;
            }
            else
            {
                lbl.Text = "✗  " + ruleText;
                lbl.ForeColor = Color.Firebrick;
            }
        }

        // ==================== HIỆN/ẨN MẬT KHẨU ====================

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtNewPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            txtConfirmPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        // ==================== LƯU ====================

        private void btnSave_Click(object sender, EventArgs e)
        {
            string pwd = txtNewPassword.Text;
            string confirm = txtConfirmPassword.Text;

            // Double-check ở server-side (đề phòng ai đó enable nút bằng tool khác)
            if (!IsStrongPassword(pwd))
            {
                MessageBox.Show("Mật khẩu không đủ mạnh. Hãy kiểm tra lại các yêu cầu.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pwd != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string hash = BCrypt.Net.BCrypt.HashPassword(pwd);
                bool ok = Users.ChangePassword(_userId, hash);

                if (!ok)
                {
                    MessageBox.Show("Không cập nhật được mật khẩu trên DB.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Đổi mật khẩu thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đổi mật khẩu: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Cùng tiêu chí với UI: tối thiểu 8 ký tự, có hoa, có số, có ký tự đặc biệt.
        /// </summary>
        public static bool IsStrongPassword(string pwd)
        {
            if (string.IsNullOrEmpty(pwd)) return false;
            return RxLength.IsMatch(pwd)
                && RxUppercase.IsMatch(pwd)
                && RxDigit.IsMatch(pwd)
                && RxSpecial.IsMatch(pwd);
        }
    }
}