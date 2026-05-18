/* =========================================================
   COURSE REGISTRATION SCHEMA
   - Bảng Courses: danh mục môn học
   - Bảng StudentCourses: đăng ký môn học (many-to-many)
   ========================================================= */

USE LoginDB;
GO

-- Command: tạo bảng Courses nếu chưa có
IF OBJECT_ID(N'dbo.Courses', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Courses
    (
        Id          INT             NOT NULL PRIMARY KEY IDENTITY(1,1),
        Code        NVARCHAR(20)    NOT NULL UNIQUE,
        Name        NVARCHAR(150)   NOT NULL,
        Credits     INT             NOT NULL DEFAULT 3,
        Description NVARCHAR(500)   NULL,
        Created_At  DATETIME        DEFAULT GETDATE()
    );
END
GO

-- Command: tạo bảng StudentCourses (đăng ký) nếu chưa có
IF OBJECT_ID(N'dbo.StudentCourses', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.StudentCourses
    (
        Id            INT       NOT NULL PRIMARY KEY IDENTITY(1,1),
        StudentId     INT       NOT NULL,
        CourseId      INT       NOT NULL,
        RegisteredAt  DATETIME  NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_StudentCourses_Students
            FOREIGN KEY (StudentId) REFERENCES dbo.Students(Id) ON DELETE CASCADE,
        CONSTRAINT FK_StudentCourses_Courses
            FOREIGN KEY (CourseId) REFERENCES dbo.Courses(Id) ON DELETE CASCADE,

        CONSTRAINT UQ_StudentCourses_StudentCourse UNIQUE (StudentId, CourseId)
    );
END
GO

/* Command: seed một vài môn học mẫu
   - MERGE để chạy lại nhiều lần không bị insert trùng
*/
MERGE dbo.Courses AS target
USING (VALUES
    (N'CS101', N'Nhập môn lập trình',             3, N'Giới thiệu về lập trình với C#'),
    (N'CS102', N'Cấu trúc dữ liệu và giải thuật', 4, N'Các cấu trúc dữ liệu cơ bản'),
    (N'CS201', N'Cơ sở dữ liệu',                  3, N'Thiết kế và truy vấn CSDL với SQL Server'),
    (N'CS202', N'Lập trình hướng đối tượng',      3, N'Nguyên lý OOP với C#/.NET'),
    (N'MA101', N'Toán cao cấp 1',                 4, N'Giải tích một biến'),
    (N'MA102', N'Toán rời rạc',                   3, N'Logic, tập hợp, đồ thị'),
    (N'EN101', N'Tiếng Anh cơ bản',               2, N'Tiếng Anh giao tiếp')
) AS src(Code, Name, Credits, Description)
ON target.Code = src.Code
WHEN NOT MATCHED THEN
    INSERT (Code, Name, Credits, Description)
    VALUES (src.Code, src.Name, src.Credits, src.Description);
GO

-- Command: kiểm tra nhanh dữ liệu
SELECT * FROM dbo.Courses ORDER BY Code;
SELECT TOP 50 * FROM dbo.StudentCourses ORDER BY RegisteredAt DESC;
GO

--DROP TABLE StudentCourses;
--DROP TABLE Courses;