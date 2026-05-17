using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ClassProject
{
    public class Student
    {
        private int _userId;
        private string _mssv;
        private string _firstName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private string _gender;
        private string _phone;
        private string _address;
        private string _hometown;
        private string _email;
        private byte[] _picture;

        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public string Mssv
        {
            get => _mssv;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("MSSV không hợp lệ!");
                _mssv = value.Trim();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Tên không được để trống!");
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Họ không được để trống!");
                _lastName = value;
            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value > DateTime.Now)
                    throw new Exception("Ngày sinh không được lớn hơn hiện tại!");
                _dateOfBirth = value;
            }
        }

        public string Gender
        {
            get => _gender;
            set
            {
                if (value != "Nam" && value != "Nữ")
                    throw new Exception("Giới tính chỉ được là 'Nam' hoặc 'Nữ'!");
                _gender = value;
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 10 || value.Length > 11)
                    throw new Exception("Số điện thoại không hợp lệ!");
                _phone = value;
            }
        }

        public string Address
        {
            get => _address;
            set => _address = value;
        }

        public string Hometown
        {
            get => _hometown;
            set => _hometown = value;
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.Contains("@"))
                    throw new Exception("Email không hợp lệ!");
                _email = value;
            }
        }

        public byte[] Picture
        {
            get => _picture;
            set
            {
                if (value != null && value.Length > 5 * 1024 * 1024)
                    throw new Exception("Ảnh không được vượt quá 5MB!");
                _picture = value;
            }
        }

        public Student() { }

        public Student(string mssv, string firstName, string lastName, DateTime dateOfBirth,
                       string gender, string phone, string address, string hometown,
                       string email, byte[] picture = null, int userId = 0)
        {
            UserId = userId;
            Mssv = mssv;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Phone = phone;
            Address = address;
            Hometown = hometown;
            Email = email;
            Picture = picture;
        }

        public string FullName => $"{LastName} {FirstName}";

        private static SqlParameter CreatePictureParameter(string name, byte[]? picture)
        {
            return new SqlParameter(name, SqlDbType.VarBinary)
            {
                Value = picture ?? (object)DBNull.Value
            };
        }

        public override string ToString()
        {
            return $"MSSV      : {Mssv}\n" +
                   $"Họ tên    : {FullName}\n" +
                   $"Ngày sinh : {DateOfBirth:dd/MM/yyyy}\n" +
                   $"Giới tính : {Gender}\n" +
                   $"SĐT       : {Phone}\n" +
                   $"Địa chỉ   : {Address}\n" +
                   $"Quê quán  : {Hometown}\n" +
                   $"Email     : {Email}\n" +
                   $"Ảnh       : {(Picture != null ? "Có" : "Chưa có")}";
        }

        // ==================== SINH VIÊN CHÍNH THỨC ====================

        public static DataTable GetByUserId(int userId)
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT MSSV, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture
FROM dbo.Students
WHERE UserId = @UserId;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu sinh viên: " + ex.Message);
                }
            }

            return dt;
        }

        public static bool UpdateByUserId(int userId, string firstName, string lastName,
            DateTime dateOfBirth, string gender, string phone, string address,
            string hometown, string email, byte[] picture)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
UPDATE dbo.Students
SET FirstName   = @FirstName,
    LastName    = @LastName,
    DateOfBirth = @DateOfBirth,
    Gender      = @Gender,
    Phone       = @Phone,
    Address     = @Address,
    Hometown    = @Hometown,
    Email       = @Email,
    Picture     = @Picture
WHERE UserId = @UserId;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(address) ? DBNull.Value : address);
                        cmd.Parameters.AddWithValue("@Hometown", string.IsNullOrWhiteSpace(hometown) ? DBNull.Value : hometown);
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : email);
                        cmd.Parameters.Add(CreatePictureParameter("@Picture", picture));
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật sinh viên: " + ex.Message);
                    return false;
                }
            }
        }

        public bool AddStudent()
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
INSERT INTO dbo.Students
    (UserId, MSSV, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture)
VALUES
    (@UserId, @Mssv, @FirstName, @LastName, @DateOfBirth, @Gender, @Phone, @Address, @Hometown, @Email, @Picture);";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", UserId > 0 ? UserId : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Mssv", Mssv);
                        cmd.Parameters.AddWithValue("@FirstName", FirstName);
                        cmd.Parameters.AddWithValue("@LastName", LastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@Phone", Phone);
                        cmd.Parameters.AddWithValue("@Address", Address ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Hometown", Hometown ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.Add(CreatePictureParameter("@Picture", Picture));

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                    return false;
                }
            }
        }

        public static bool IsMssvExist(string mssv)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                const string sql = "SELECT COUNT(*) FROM dbo.Students WHERE MSSV = @Mssv";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mssv", mssv);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public static DataTable GetAllStudents()
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT s.Id, s.MSSV, s.LastName, s.FirstName, s.DateOfBirth, s.Gender,
       s.Phone, s.Address, s.Hometown, s.Email
FROM dbo.Students s
ORDER BY s.MSSV;";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lấy danh sách sinh viên: " + ex.Message);
                }
            }

            return dt;
        }

        public static DataTable GetById(int studentId)
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT Id, UserId, MSSV, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture
FROM dbo.Students
WHERE Id = @Id;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", studentId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải sinh viên: " + ex.Message);
                }
            }

            return dt;
        }

        public static bool UpdateById(int studentId, string firstName, string lastName,
            DateTime dateOfBirth, string gender, string phone, string address,
            string hometown, string email, byte[] picture)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
UPDATE dbo.Students
SET FirstName   = @FirstName,
    LastName    = @LastName,
    DateOfBirth = @DateOfBirth,
    Gender      = @Gender,
    Phone       = @Phone,
    Address     = @Address,
    Hometown    = @Hometown,
    Email       = @Email,
    Picture     = @Picture
WHERE Id = @Id;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", studentId);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(address) ? DBNull.Value : address);
                        cmd.Parameters.AddWithValue("@Hometown", string.IsNullOrWhiteSpace(hometown) ? DBNull.Value : hometown);
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : email);
                        cmd.Parameters.Add(CreatePictureParameter("@Picture", picture));

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật sinh viên: " + ex.Message);
                    return false;
                }
            }
        }

        public static bool DeleteById(int studentId)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        int? userId = null;
                        using (SqlCommand readCmd = new SqlCommand(
                            "SELECT UserId FROM dbo.Students WHERE Id = @Id", conn, tx))
                        {
                            readCmd.Parameters.AddWithValue("@Id", studentId);
                            object result = readCmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                                userId = Convert.ToInt32(result);
                        }

                        using (SqlCommand cmd = new SqlCommand(
                            "DELETE FROM dbo.Students WHERE Id = @Id", conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Id", studentId);
                            if (cmd.ExecuteNonQuery() == 0)
                                throw new Exception("Không tìm thấy sinh viên để xóa.");
                        }

                        if (userId.HasValue)
                        {
                            using (SqlCommand cmd = new SqlCommand(
                                "DELETE FROM dbo.Users WHERE Id = @UserId", conn, tx))
                            {
                                cmd.Parameters.AddWithValue("@UserId", userId.Value);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        try { tx.Rollback(); } catch { }
                        MessageBox.Show("Lỗi xóa sinh viên: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        /// <summary>Thêm sinh viên mới kèm tạo tài khoản (Username/Password = MSSV).</summary>
        public static bool AddWithNewAccount(string mssv, string firstName, string lastName,
            DateTime dateOfBirth, string gender, string phone, string address,
            string hometown, string email, byte[] picture)
        {
            if (IsMssvExistsInSystem(mssv))
            {
                MessageBox.Show($"MSSV {mssv} đã tồn tại trong hệ thống.");
                return false;
            }

            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string accountEmail = string.IsNullOrWhiteSpace(email)
                            ? $"{mssv}@student.local"
                            : email.Trim();

                        int userId = Users.CreateUser(
                            mssv, accountEmail, BCrypt.Net.BCrypt.HashPassword(mssv), 1, conn, tx);

                        const string sql = @"
INSERT INTO dbo.Students
    (UserId, MSSV, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture)
VALUES
    (@UserId, @Mssv, @FirstName, @LastName, @DateOfBirth, @Gender, @Phone, @Address, @Hometown, @Email, @Picture);";

                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@Mssv", mssv);
                            cmd.Parameters.AddWithValue("@FirstName", firstName);
                            cmd.Parameters.AddWithValue("@LastName", lastName);
                            cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                            cmd.Parameters.AddWithValue("@Gender", gender);
                            cmd.Parameters.AddWithValue("@Phone", phone);
                            cmd.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(address) ? DBNull.Value : address);
                            cmd.Parameters.AddWithValue("@Hometown", string.IsNullOrWhiteSpace(hometown) ? DBNull.Value : hometown);
                            cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : email);
                            cmd.Parameters.Add(CreatePictureParameter("@Picture", picture));

                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        try { tx.Rollback(); } catch { }
                        MessageBox.Show("Lỗi thêm sinh viên: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public static DataTable SearchStudents(string keyword)
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = @"
SELECT s.Id, s.MSSV, s.LastName, s.FirstName, s.DateOfBirth, s.Gender,
       s.Phone, s.Address, s.Hometown, s.Email
FROM dbo.Students s
WHERE s.MSSV LIKE @keyword
   OR s.FirstName LIKE @keyword
   OR s.LastName LIKE @keyword
   OR s.Email LIKE @keyword
ORDER BY s.MSSV;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tìm kiếm sinh viên: " + ex.Message);
                }
            }

            return dt;
        }

        // ==================== PENDING STUDENTS (ADMIN DUYỆT) ====================

        public static void EnsurePendingStudentsTable()
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                EnsurePendingStudentsTable(conn);
            }
        }

        private static void EnsurePendingStudentsTable(SqlConnection conn)
        {
            const string dropOld = @"
IF OBJECT_ID('dbo.PendingStudents', 'U') IS NOT NULL
AND EXISTS (SELECT 1 FROM sys.columns
            WHERE object_id = OBJECT_ID('dbo.PendingStudents') AND name = 'UserId')
BEGIN
    DROP TABLE dbo.PendingStudents;
END";

            using (SqlCommand cmd = new SqlCommand(dropOld, conn))
            {
                cmd.ExecuteNonQuery();
            }

            const string sql = @"
IF OBJECT_ID('dbo.PendingStudents', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PendingStudents (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Mssv NVARCHAR(30) NOT NULL,
        FirstName NVARCHAR(100) NULL,
        LastName NVARCHAR(100) NOT NULL,
        DateOfBirth DATETIME NULL,
        Gender NVARCHAR(10) NULL,
        Phone NVARCHAR(20) NULL,
        Address NVARCHAR(255) NULL,
        Hometown NVARCHAR(255) NULL,
        Email NVARCHAR(255) NULL,
        Picture VARBINARY(MAX) NULL,
        SubmittedAt DATETIME NOT NULL CONSTRAINT DF_PendingStudents_SubmittedAt DEFAULT(GETDATE())
    );

    CREATE UNIQUE INDEX UX_PendingStudents_Mssv ON dbo.PendingStudents(Mssv);
END";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable GetAllPending()
        {
            DataTable dt = new DataTable();
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT Id AS PendingId, Mssv, LastName, FirstName, DateOfBirth, Gender,
       Phone, Address, Hometown, Email, SubmittedAt
FROM dbo.PendingStudents
ORDER BY SubmittedAt DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static bool IsMssvExistsInSystem(string mssv)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT
    (SELECT COUNT(*) FROM dbo.Students WHERE MSSV = @mssv) +
    (CASE WHEN OBJECT_ID('dbo.PendingStudents', 'U') IS NOT NULL
          THEN (SELECT COUNT(*) FROM dbo.PendingStudents WHERE Mssv = @mssv)
          ELSE 0 END);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@mssv", mssv);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public static bool InsertPending(string mssv, string firstName, string lastName,
            DateTime? dateOfBirth, string gender, string phone, string address,
            string hometown, string email)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                const string sql = @"
INSERT INTO dbo.PendingStudents
    (Mssv, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email)
VALUES
    (@Mssv, @FirstName, @LastName, @DateOfBirth, @Gender, @Phone, @Address, @Hometown, @Email);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mssv", mssv);
                    cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(firstName) ? DBNull.Value : firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth.HasValue ? dateOfBirth.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender) ? DBNull.Value : gender);
                    cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phone) ? DBNull.Value : phone);
                    cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(address) ? DBNull.Value : address);
                    cmd.Parameters.AddWithValue("@Hometown", string.IsNullOrEmpty(hometown) ? DBNull.Value : hometown);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? DBNull.Value : email);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool RejectPending(int pendingId)
        {
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                const string sql = "DELETE FROM dbo.PendingStudents WHERE Id = @PendingId;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PendingId", pendingId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>Duyệt pending: tạo Users + Students, xóa pending.</summary>
        public static bool ApprovePending(int pendingId, string mssv, out int newUserId)
        {
            newUserId = 0;
            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string email = "";
                        using (SqlCommand readCmd = new SqlCommand(
                            "SELECT Email FROM dbo.PendingStudents WHERE Id = @id", conn, tx))
                        {
                            readCmd.Parameters.AddWithValue("@id", pendingId);
                            email = readCmd.ExecuteScalar()?.ToString() ?? "";
                            if (string.IsNullOrEmpty(email))
                                email = $"{mssv}@student.local";
                        }

                        newUserId = Users.CreateUser(
                            mssv, email, BCrypt.Net.BCrypt.HashPassword(mssv), 1, conn, tx);

                        const string insertStudent = @"
INSERT INTO dbo.Students (UserId, MSSV, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture)
SELECT @userId, Mssv, FirstName, LastName, DateOfBirth, Gender, Phone, Address, Hometown, Email, Picture
FROM dbo.PendingStudents
WHERE Id = @pendingId;";

                        using (SqlCommand cmd = new SqlCommand(insertStudent, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@userId", newUserId);
                            cmd.Parameters.AddWithValue("@pendingId", pendingId);

                            if (cmd.ExecuteNonQuery() != 1)
                                throw new Exception("Không insert được vào Students.");
                        }

                        using (SqlCommand cmd = new SqlCommand(
                            "DELETE FROM dbo.PendingStudents WHERE Id = @pendingId;", conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@pendingId", pendingId);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        return true;
                    }
                    catch
                    {
                        try { tx.Rollback(); } catch { }
                        throw;
                    }
                }
            }
        }
    }
}
