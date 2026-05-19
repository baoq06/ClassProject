/* =========================================================
   SETUP REMOTE ACCESS FOR CONTRIBUTORS (chạy trên máy HOST)

   Mục tiêu:
   - Tạo SQL Login dùng chung cho team: classproject_user
   - Tạo user trong DB LoginDB và cấp quyền đủ để chạy app
     + chạy migration (db_ddladmin để CREATE/ALTER TABLE)
   - KHÔNG cấp quyền sysadmin / server-wide

   Yêu cầu trước khi chạy:
   - SQL Server đã bật Mixed Mode Authentication (xem README_DB_SETUP.md)
   - Bạn đăng nhập SSMS bằng Windows Authentication (tài khoản có quyền sysadmin)
   - DB LoginDB đã tồn tại (chạy 001_init_schema.sql trước đó)

   CHÚ Ý: Hãy ĐỔI password bên dưới trước khi chạy!
   ========================================================= */

USE master;
GO

-- ----- 1. Tạo SQL Login dùng chung cho team -----
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = N'classproject_user')
BEGIN
    -- Đổi 'ChangeMe_Strong_Pass_123!' thành password mạnh của bạn.
    -- CHECK_POLICY=OFF để dễ thiết lập trong môi trường dev (KHÔNG dùng cho production).
    CREATE LOGIN [classproject_user]
        WITH PASSWORD       = N'ChangeMe_Strong_Pass_123!',
             DEFAULT_DATABASE = LoginDB,
             CHECK_EXPIRATION = OFF,
             CHECK_POLICY     = OFF;
END
ELSE
BEGIN
    -- Nếu đã có rồi, có thể reset password bằng lệnh sau (bỏ comment để dùng):
    -- ALTER LOGIN [classproject_user] WITH PASSWORD = N'NewStrongPass!';
    PRINT 'Login classproject_user đã tồn tại - bỏ qua bước CREATE.';
END
GO

-- ----- 2. Tạo user trong DB LoginDB liên kết tới login -----
USE LoginDB;
GO

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = N'classproject_user')
BEGIN
    CREATE USER [classproject_user] FOR LOGIN [classproject_user]
        WITH DEFAULT_SCHEMA = dbo;
END
ELSE
BEGIN
    PRINT 'User classproject_user đã tồn tại trong LoginDB - bỏ qua bước CREATE USER.';
END
GO

-- ----- 3. Cấp quyền vừa đủ để chạy app + migration -----
-- db_datareader: SELECT mọi bảng
-- db_datawriter: INSERT/UPDATE/DELETE mọi bảng
-- db_ddladmin  : CREATE/ALTER/DROP bảng (cần cho migration 002, 003...)
ALTER ROLE db_datareader ADD MEMBER [classproject_user];
ALTER ROLE db_datawriter ADD MEMBER [classproject_user];
ALTER ROLE db_ddladmin   ADD MEMBER [classproject_user];
GO

-- ----- 4. Kiểm tra nhanh -----
SELECT name, type_desc, default_database_name, is_disabled
FROM sys.server_principals
WHERE name = N'classproject_user';

SELECT dp.name AS UserName, r.name AS RoleName
FROM sys.database_role_members drm
JOIN sys.database_principals dp ON dp.principal_id = drm.member_principal_id
JOIN sys.database_principals r  ON r.principal_id  = drm.role_principal_id
WHERE dp.name = N'classproject_user';
GO

PRINT '==> Hoàn tất. Bây giờ contributors có thể connect bằng:';
PRINT '    Server  : <IP_CUA_BAN>,1433   (vd: 192.168.1.10,1433)';
PRINT '    Login   : classproject_user';
PRINT '    Password: (password bạn vừa đặt)';
PRINT '    Database: LoginDB';
GO

-- ----- LỆNH XÓA NẾU CẦN GỠ -----
-- USE LoginDB;
-- DROP USER classproject_user;
-- USE master;
-- DROP LOGIN classproject_user;