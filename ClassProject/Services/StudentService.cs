using ClassProject.DataAccess.Db;
using ClassProject.Models;
using ClassProject.Repositories;
using Microsoft.Data.SqlClient;
using System;

namespace ClassProject.Services
{
    public class StudentService
    {
        private readonly My_DB _db = new My_DB();

        public bool ApprovePending(
            int pendingId,
            string mssv,
            out int newUserId)
        {
            newUserId = 0;

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string email = "";

                        using (SqlCommand readCmd = new SqlCommand(
                            "SELECT Email FROM dbo.PendingStudents WHERE Id = @Id",
                            conn,
                            tx))
                        {
                            readCmd.Parameters.AddWithValue("@Id", pendingId);

                            email = readCmd.ExecuteScalar()?.ToString() ?? "";

                            if (string.IsNullOrWhiteSpace(email))
                            {
                                email = $"{mssv}@student.local";
                            }
                        }

                        newUserId = Users.CreateUser(
                            mssv,
                            email,
                            BCrypt.Net.BCrypt.HashPassword(mssv),
                            1,
                            conn,
                            tx);

                        const string insertStudent = @"
INSERT INTO dbo.Students
(
    UserId,
    MSSV,
    FirstName,
    LastName,
    DateOfBirth,
    Gender,
    Phone,
    Address,
    Hometown,
    Email,
    Picture
)
SELECT
    @UserId,
    Mssv,
    FirstName,
    LastName,
    DateOfBirth,
    Gender,
    Phone,
    Address,
    Hometown,
    Email,
    Picture
FROM dbo.PendingStudents
WHERE Id = @PendingId";

                        using (SqlCommand cmd = new SqlCommand(
                            insertStudent,
                            conn,
                            tx))
                        {
                            cmd.Parameters.AddWithValue("@UserId", newUserId);
                            cmd.Parameters.AddWithValue("@PendingId", pendingId);

                            if (cmd.ExecuteNonQuery() != 1)
                            {
                                throw new Exception(
                                    "Không insert được vào Students.");
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand(
                            "DELETE FROM dbo.PendingStudents WHERE Id = @PendingId",
                            conn,
                            tx))
                        {
                            cmd.Parameters.AddWithValue("@PendingId", pendingId);

                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();

                        return true;
                    }
                    catch
                    {
                        try
                        {
                            tx.Rollback();
                        }
                        catch { }

                        throw;
                    }
                }
            }
        }

        public bool AddWithNewAccount(Student student)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string accountEmail =
                            string.IsNullOrWhiteSpace(student.Email)
                            ? $"{student.Mssv}@student.local"
                            : student.Email.Trim();

                        int userId = Users.CreateUser(
                            student.Mssv,
                            accountEmail,
                            BCrypt.Net.BCrypt.HashPassword(student.Mssv),
                            1,
                            conn,
                            tx);

                        const string sql = @"
INSERT INTO dbo.Students
(
    UserId,
    MSSV,
    FirstName,
    LastName,
    DateOfBirth,
    Gender,
    Phone,
    Address,
    Hometown,
    Email,
    Picture
)
VALUES
(
    @UserId,
    @Mssv,
    @FirstName,
    @LastName,
    @DateOfBirth,
    @Gender,
    @Phone,
    @Address,
    @Hometown,
    @Email,
    @Picture
)";

                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);

                            cmd.Parameters.AddWithValue("@Mssv", student.Mssv);

                            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);

                            cmd.Parameters.AddWithValue("@LastName", student.LastName);

                            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);

                            cmd.Parameters.AddWithValue("@Gender", student.Gender);

                            cmd.Parameters.AddWithValue("@Phone", student.Phone);

                            cmd.Parameters.AddWithValue(
                                "@Address",
                                string.IsNullOrWhiteSpace(student.Address)
                                ? DBNull.Value
                                : student.Address);

                            cmd.Parameters.AddWithValue(
                                "@Hometown",
                                string.IsNullOrWhiteSpace(student.Hometown)
                                ? DBNull.Value
                                : student.Hometown);

                            cmd.Parameters.AddWithValue(
                                "@Email",
                                string.IsNullOrWhiteSpace(student.Email)
                                ? DBNull.Value
                                : student.Email);

                            cmd.Parameters.Add(
                                Student.CreatePictureParameter(
                                    "@Picture",
                                    student.Picture));

                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();

                        return true;
                    }
                    catch
                    {
                        try
                        {
                            tx.Rollback();
                        }
                        catch { }

                        throw;
                    }
                }
            }
        }
    }
}