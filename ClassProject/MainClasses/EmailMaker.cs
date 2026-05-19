using System;
using System.Windows.Forms;

namespace ClassProject
{
    /// <summary>
    /// Helper ẩn một phần email khi hiển thị danh sách (bảo mật cho admin views).
    ///
    /// Quy ước mask:
    /// - Giữ 2 ký tự đầu của phần local (trước '@').
    /// - Thay phần còn lại của local bằng 4 dấu '*'.
    /// - Giữ nguyên '@' và toàn bộ domain.
    ///
    /// Ví dụ:
    /// - "22120999@student.hcmute.edu.vn"  ->  "22****@student.hcmute.edu.vn"
    /// - "nguyenvana@gmail.com"            ->  "ng****@gmail.com"
    /// - "ab@x.com" (local 2 ký tự)        ->  "**@x.com"     (mask hết)
    /// - "a@x.com" (local 1 ký tự)         ->  "*@x.com"
    /// - "" / null                          ->  ""
    /// - chuỗi không có '@'                ->  trả nguyên (không phải email)
    /// </summary>
    public static class EmailMasker
    {
        private const int VisiblePrefix = 2;  // số ký tự đầu giữ nguyên
        private const string MaskPart = "****"; // luôn hiện 4 dấu * cho gọn UI

        public static string Mask(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return string.Empty;

            int at = email.IndexOf('@');

            // Không có '@' hoặc bắt đầu bằng '@' -> không phải email hợp lệ,
            // trả nguyên để tránh "nuốt" chuỗi user input lạ.
            if (at <= 0) return email;

            string local = email.Substring(0, at);
            string domain = email.Substring(at);     // bao gồm cả '@'

            // Local quá ngắn -> mask hết phần local để không lộ ký tự nào
            if (local.Length <= VisiblePrefix)
            {
                return new string('*', local.Length) + domain;
            }

            string visible = local.Substring(0, VisiblePrefix);
            return visible + MaskPart + domain;
        }

        // ========== TIỆN ÍCH WIRE NHANH VÀO DATAGRIDVIEW ==========

        /// <summary>
        /// Gắn vào DataGridView để mask cột Email khi hiển thị.
        /// Underlying cell value KHÔNG bị thay đổi (chỉ formatting ở UI),
        /// nên data DB và logic copy vẫn nhận full email.
        ///
        /// Cách dùng:
        ///     EmailMasker.AttachTo(dgvStudents);             // mask cột "Email"
        ///     EmailMasker.AttachTo(dgvStudents, "Email");    // chỉ định tên cột
        /// </summary>
        public static void AttachTo(DataGridView dgv, string columnName = "Email")
        {
            if (dgv == null) return;

            dgv.CellFormatting += (sender, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                DataGridViewColumn col = dgv.Columns[e.ColumnIndex];
                if (col == null || col.Name != columnName) return;

                if (e.Value == null || e.Value == DBNull.Value) return;

                string raw = e.Value.ToString() ?? "";
                e.Value = Mask(raw);
                e.FormattingApplied = true;
            };
        }
    }
}