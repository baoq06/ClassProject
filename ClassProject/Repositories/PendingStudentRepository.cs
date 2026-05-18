using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ClassProject.Repositories
{
    public class PendingStudentRepository
    {
        private readonly My_DB _db = new My_DB();

        // =========================
        // CREATE TABLE
        // =========================

        public void EnsureTable()
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string dropOld = @"
IF OBJECT_ID('dbo.PendingStudents', 'U') IS NOT NULL
AND EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.PendingStudents')
    AND name = 'UserId'
)
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
    CREATE TABLE dbo.PendingStudents
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,

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

        SubmittedAt DATETIME NOT NULL
            CONSTRAINT DF_PendingStudents_SubmittedAt
            DEFAULT(GETDATE())
    );

    CREATE UNIQUE INDEX UX_PendingStudents_Mssv
    ON dbo.PendingStudents(Mssv);
END";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // INSERT PENDING
        // =========================

        public bool Insert(
            string mssv,
            string firstName,
            string lastName,
            DateTime? dateOfBirth,
            string gender,
            string phone,
            string address,
            string hometown,
            string email
        )
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
INSERT INTO dbo.PendingStudents
(
    Mssv,
    FirstName,
    LastName,
    DateOfBirth,
    Gender,
    Phone,
    Address,
    Hometown,
    Email
)
VALUES
(
    @Mssv,
    @FirstName,
    @LastName,
    @DateOfBirth,
    @Gender,
    @Phone,
    @Address,
    @Hometown,
    @Email
)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Mssv", mssv);

                    cmd.Parameters.AddWithValue(
                        "@FirstName",
                        string.IsNullOrWhiteSpace(firstName)
                        ? DBNull.Value
                        : firstName);

                    cmd.Parameters.AddWithValue("@LastName", lastName);

                    cmd.Parameters.AddWithValue(
                        "@DateOfBirth",
                        dateOfBirth.HasValue
                        ? dateOfBirth.Value
                        : DBNull.Value);

                    cmd.Parameters.AddWithValue(
                        "@Gender",
                        string.IsNullOrWhiteSpace(gender)
                        ? DBNull.Value
                        : gender);

                    cmd.Parameters.AddWithValue(
                        "@Phone",
                        string.IsNullOrWhiteSpace(phone)
                        ? DBNull.Value
                        : phone);

                    cmd.Parameters.AddWithValue(
                        "@Address",
                        string.IsNullOrWhiteSpace(address)
                        ? DBNull.Value
                        : address);

                    cmd.Parameters.AddWithValue(
                        "@Hometown",
                        string.IsNullOrWhiteSpace(hometown)
                        ? DBNull.Value
                        : hometown);

                    cmd.Parameters.AddWithValue(
                        "@Email",
                        string.IsNullOrWhiteSpace(email)
                        ? DBNull.Value
                        : email);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // =========================
        // GET ALL PENDING
        // =========================

        public DataTable GetAll()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT
    Id AS PendingId,
    Mssv,
    LastName,
    FirstName,
    DateOfBirth,
    Gender,
    Phone,
    Address,
    Hometown,
    Email,
    SubmittedAt
FROM dbo.PendingStudents
ORDER BY SubmittedAt DESC";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // =========================
        // DELETE / REJECT
        // =========================

        public bool Reject(int pendingId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql =
                    "DELETE FROM dbo.PendingStudents WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", pendingId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // =========================
        // CHECK MSSV
        // =========================

        public bool IsMssvExists(string mssv)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT
(
    SELECT COUNT(*)
    FROM dbo.Students
    WHERE MSSV = @mssv
)
+
(
    CASE
        WHEN OBJECT_ID('dbo.PendingStudents', 'U') IS NOT NULL
        THEN
        (
            SELECT COUNT(*)
            FROM dbo.PendingStudents
            WHERE Mssv = @mssv
        )
        ELSE 0
    END
)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@mssv", mssv);

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }
    }
}