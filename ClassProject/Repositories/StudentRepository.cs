using ClassProject.DataAccess.Db;
using ClassProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ClassProject.Repositories
{
    public class StudentRepository
    {
        private readonly My_DB _db = new My_DB();

        public bool Add(Student student)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
INSERT INTO dbo.Students
(UserId, MSSV, FirstName, LastName, DateOfBirth,
 Gender, Phone, Address, Hometown, Email, Picture)
VALUES
(@UserId, @Mssv, @FirstName, @LastName, @DateOfBirth,
 @Gender, @Phone, @Address, @Hometown, @Email, @Picture)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId",
                        student.UserId > 0 ? student.UserId : DBNull.Value);

                    cmd.Parameters.AddWithValue("@Mssv", student.Mssv);
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", student.Gender);
                    cmd.Parameters.AddWithValue("@Phone", student.Phone);

                    cmd.Parameters.AddWithValue("@Address",
                        string.IsNullOrWhiteSpace(student.Address)
                        ? DBNull.Value
                        : student.Address);

                    cmd.Parameters.AddWithValue("@Hometown",
                        string.IsNullOrWhiteSpace(student.Hometown)
                        ? DBNull.Value
                        : student.Hometown);

                    cmd.Parameters.AddWithValue("@Email",
                        string.IsNullOrWhiteSpace(student.Email)
                        ? DBNull.Value
                        : student.Email);

                    cmd.Parameters.Add(
                        Student.CreatePictureParameter("@Picture", student.Picture));

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable GetAll()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT *
FROM dbo.Students
ORDER BY MSSV";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public bool Delete(int studentId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql =
                    "DELETE FROM dbo.Students WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", studentId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable GetByUserId(int userId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT
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
FROM dbo.Students
WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public bool UpdateByUserId(
        int userId,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string gender,
        string phone,
        string address,
        string hometown,
        string email,
        byte[] picture)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
UPDATE dbo.Students
SET
    FirstName   = @FirstName,
    LastName    = @LastName,
    DateOfBirth = @DateOfBirth,
    Gender      = @Gender,
    Phone       = @Phone,
    Address     = @Address,
    Hometown    = @Hometown,
    Email       = @Email,
    Picture     = @Picture
WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    cmd.Parameters.AddWithValue("@FirstName", firstName);

                    cmd.Parameters.AddWithValue("@LastName", lastName);

                    cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                    cmd.Parameters.AddWithValue("@Gender", gender);

                    cmd.Parameters.AddWithValue("@Phone", phone);

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

                    cmd.Parameters.Add(
                        Student.CreatePictureParameter("@Picture", picture));

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable GetById(int studentId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT
    Id,
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
FROM dbo.Students
WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", studentId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public bool UpdateById(
    int studentId,
    string firstName,
    string lastName,
    DateTime dateOfBirth,
    string gender,
    string phone,
    string address,
    string hometown,
    string email,
    byte[] picture)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
UPDATE dbo.Students
SET
    FirstName   = @FirstName,
    LastName    = @LastName,
    DateOfBirth = @DateOfBirth,
    Gender      = @Gender,
    Phone       = @Phone,
    Address     = @Address,
    Hometown    = @Hometown,
    Email       = @Email,
    Picture     = @Picture
WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", studentId);

                    cmd.Parameters.AddWithValue("@FirstName", firstName);

                    cmd.Parameters.AddWithValue("@LastName", lastName);

                    cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                    cmd.Parameters.AddWithValue("@Gender", gender);

                    cmd.Parameters.AddWithValue("@Phone", phone);

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

                    cmd.Parameters.Add(
                        Student.CreatePictureParameter("@Picture", picture));

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable Search(string keyword)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT
    Id,
    MSSV,
    LastName,
    FirstName,
    DateOfBirth,
    Gender,
    Phone,
    Address,
    Hometown,
    Email
FROM dbo.Students
WHERE
    MSSV LIKE @kw OR
    FirstName LIKE @kw OR
    LastName LIKE @kw OR
    Email LIKE @kw
ORDER BY MSSV";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public bool DeleteById(int studentId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
DELETE FROM dbo.Students
WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", studentId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}