/* =========================================================
   GRADES SCHEMA
   - Bảng Grades: lưu điểm của sinh viên theo môn học
   - Mỗi (StudentId, CourseId) chỉ có 1 record điểm (UNIQUE)
   - Sinh viên phải đăng ký môn học (có record trong StudentCourses)
     trước khi được nhập điểm.
   ========================================================= */

USE LoginDB;
GO

-- Command: tạo bảng Grades nếu chưa có
IF OBJECT_ID(N'dbo.Grades', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Grades
    (
        Id          INT             NOT NULL PRIMARY KEY IDENTITY(1,1),
        StudentId   INT             NOT NULL,
        CourseId    INT             NOT NULL,
        Score       DECIMAL(4,2)    NULL,        -- thang điểm 10, ví dụ 8.50
        GradedAt    DATETIME        NOT NULL DEFAULT GETDATE(),
        GradedBy    INT             NULL,        -- UserId của admin/giảng viên nhập điểm

        CONSTRAINT FK_Grades_Students
            FOREIGN KEY (StudentId) REFERENCES dbo.Students(Id) ON DELETE CASCADE,
        CONSTRAINT FK_Grades_Courses
            FOREIGN KEY (CourseId) REFERENCES dbo.Courses(Id) ON DELETE CASCADE,
        CONSTRAINT FK_Grades_Users
            FOREIGN KEY (GradedBy) REFERENCES dbo.Users(Id),

        CONSTRAINT UQ_Grades_StudentCourse UNIQUE (StudentId, CourseId),
        CONSTRAINT CK_Grades_Score CHECK (Score IS NULL OR (Score >= 0 AND Score <= 10))
    );
END
GO

-- Command: kiểm tra nhanh dữ liệu
SELECT TOP 50 g.Id, s.MSSV, c.Code AS CourseCode, c.Name AS CourseName,
              g.Score, g.GradedAt, u.Username AS GradedByUser
FROM dbo.Grades g
INNER JOIN dbo.Students s ON s.Id = g.StudentId
INNER JOIN dbo.Courses  c ON c.Id = g.CourseId
LEFT  JOIN dbo.Users    u ON u.Id = g.GradedBy
ORDER BY g.GradedAt DESC;
GO

--DROP TABLE Grades;