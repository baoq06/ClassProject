/* =========================================================
   ADD MustChangePassword FLAG TO USERS
   - Mục tiêu: ép sinh viên đổi mật khẩu lần đầu sau khi
     đăng nhập bằng tài khoản admin cấp (username/password = MSSV).
   - Cột MustChangePassword:
       1 = tài khoản BẮT BUỘC phải đổi mật khẩu trước khi vào hệ thống
       0 = không cần (đã đổi rồi, hoặc tài khoản admin/giảng viên)
   ========================================================= */

USE LoginDB;
GO

-- ----- 1. Thêm cột MustChangePassword nếu chưa có -----
IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE Name = N'MustChangePassword'
      AND Object_ID = Object_ID(N'dbo.Users')
)
BEGIN
    ALTER TABLE dbo.Users
    ADD MustChangePassword BIT NOT NULL CONSTRAINT DF_Users_MustChangePassword DEFAULT (0);
END
GO

-- ----- 2. Đánh dấu TẤT CẢ sinh viên hiện có cần đổi mật khẩu -----
-- Áp dụng cho RoleId = 1 (Student). Lần đăng nhập kế tiếp họ sẽ
-- được force vào ChangePasswordForm. Sau khi đổi thành công, flag
-- sẽ được app set về 0.
UPDATE dbo.Users
SET MustChangePassword = 1
WHERE RoleId = 1;
GO

-- ----- 3. Đảm bảo admin (RoleId = 0) và giảng viên (RoleId = 2) KHÔNG bị ép -----
UPDATE dbo.Users
SET MustChangePassword = 0
WHERE RoleId IN (0, 2);
GO

-- ----- 4. Kiểm tra nhanh -----
SELECT Id, Username, Email, RoleId, MustChangePassword, Created_At
FROM dbo.Users
ORDER BY RoleId, Id;
GO

-- Lệnh rollback nếu cần:
-- ALTER TABLE dbo.Users DROP CONSTRAINT DF_Users_MustChangePassword;
-- ALTER TABLE dbo.Users DROP COLUMN MustChangePassword;