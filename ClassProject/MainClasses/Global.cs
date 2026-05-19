namespace ClassProject
{
    /// <summary>
    /// Lưu thông tin phiên đăng nhập hiện tại của ứng dụng (in-memory).
    ///
    /// Mục đích:
    /// - Các form không phải query lại DB chỉ để biết Username / Role / Email
    ///   của user đang đăng nhập.
    /// - Tập trung 1 chỗ để quản lý "ai đang đăng nhập" -> dễ logout (ClearSession),
    ///   dễ check quyền (IsAdmin / IsStudent / IsLecturer).
    ///
    /// Cách dùng:
    ///     // Sau khi xác thực thành công trong LoginForm:
    ///     Globals.SetSession(userId, username, roleId, email);
    ///
    ///     // Trong bất kỳ form nào sau đó:
    ///     if (Globals.IsAdmin) { ... }
    ///     lblHello.Text = $"Xin chào {Globals.Username}";
    ///
    ///     // Khi đăng xuất:
    ///     Globals.ClearSession();
    ///
    /// Lưu ý:
    /// - Class này là static -> dữ liệu sống cho tới khi process kết thúc.
    /// - WinForms 1 user / 1 process nên KHÔNG cần thread-safe.
    /// - KHÔNG lưu password / hash ở đây. Nếu cần, password chỉ tồn tại
    ///   trong stack của LoginForm.btnLogin_Click rồi biến mất.
    /// </summary>
    public static class Globals
    {
        // ==================== STATE ====================

        /// <summary>Id của user đang đăng nhập. 0 nghĩa là CHƯA đăng nhập.</summary>
        public static int UserId { get; private set; }

        /// <summary>Tên đăng nhập (Username) đã dùng để login.</summary>
        public static string Username { get; private set; } = string.Empty;

        /// <summary>RoleId: 0 = Admin, 1 = Student, 2 = Lecturer. -1 = chưa login.</summary>
        public static int RoleId { get; private set; } = -1;

        /// <summary>Email của user (lấy từ bảng Users khi đăng nhập).</summary>
        public static string Email { get; private set; } = string.Empty;

        // ==================== DERIVED PROPERTIES ====================

        /// <summary>True nếu hiện đang có user đăng nhập.</summary>
        public static bool IsLoggedIn => UserId > 0;

        public static bool IsAdmin => RoleId == 0;
        public static bool IsStudent => RoleId == 1;
        public static bool IsLecturer => RoleId == 2;

        /// <summary>Tên role hiển thị (tiện cho UI). Tương ứng giá trị seed trong bảng Roles.</summary>
        public static string RoleName
        {
            get
            {
                switch (RoleId)
                {
                    case 0: return "Admin";
                    case 1: return "Sinh viên";
                    case 2: return "Giảng viên";
                    default: return string.Empty;
                }
            }
        }

        // ==================== ACTIONS ====================

        /// <summary>
        /// Lưu thông tin phiên sau khi xác thực thành công.
        /// Gọi trong LoginForm.btnLogin_Click sau BCrypt.Verify().
        /// </summary>
        public static void SetSession(int userId, string username, int roleId, string email)
        {
            UserId = userId;
            Username = username ?? string.Empty;
            RoleId = roleId;
            Email = email ?? string.Empty;
        }

        /// <summary>
        /// Xóa toàn bộ thông tin phiên (đăng xuất).
        /// Gọi trong handler "Đăng xuất" của AdminForm / StudentForm,
        /// hoặc khi form chính bị Close mà chưa logout.
        /// </summary>
        public static void ClearSession()
        {
            UserId = 0;
            Username = string.Empty;
            RoleId = -1;
            Email = string.Empty;
        }
    }
}