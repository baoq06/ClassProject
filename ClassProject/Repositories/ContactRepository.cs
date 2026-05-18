using ClassProject.DataAccess.Db;
using ClassProject.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClassProject.Repositories
{
    public class ContactRepository
    {
        private readonly My_DB _db = new My_DB();

        private static SqlParameter CreatePictureParameter(string name, byte[]? picture)
        {
            return new SqlParameter(name, SqlDbType.VarBinary)
            {
                Value = picture ?? (object)DBNull.Value
            };
        }

        public DataTable GetAllGroups(int userId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT Id, GroupName
FROM dbo.ContactGroups
WHERE UserId = @UserId
ORDER BY GroupName";

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

        public bool AddGroup(string name, int userId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
INSERT INTO dbo.ContactGroups (GroupName, UserId)
VALUES (@GroupName, @UserId)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GroupName", name);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteGroup(int id)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = "DELETE FROM dbo.ContactGroups WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable GetAll(int userId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT c.*, g.GroupName
FROM dbo.Contacts c
LEFT JOIN dbo.ContactGroups g ON c.GroupId = g.Id
WHERE c.UserId = @UserId
ORDER BY c.Lname";

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

        public DataTable GetById(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT c.*, g.GroupName
FROM dbo.Contacts c
LEFT JOIN dbo.ContactGroups g ON c.GroupId = g.Id
WHERE c.Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public bool Add(Contact contact)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
INSERT INTO dbo.Contacts
(Fname, Lname, Dob, Gender, GroupId, Phone, Address, Email, UserId, Picture)
VALUES
(@Fname, @Lname, @Dob, @Gender, @GroupId, @Phone, @Address, @Email, @UserId, @Picture)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Fname",
                        string.IsNullOrWhiteSpace(contact.Fname)
                        ? DBNull.Value
                        : contact.Fname);

                    cmd.Parameters.AddWithValue("@Lname", contact.Lname);

                    cmd.Parameters.AddWithValue("@Dob",
                        contact.Dob.HasValue
                        ? contact.Dob.Value
                        : DBNull.Value);

                    cmd.Parameters.AddWithValue("@Gender",
                        string.IsNullOrWhiteSpace(contact.Gender)
                        ? DBNull.Value
                        : contact.Gender);

                    cmd.Parameters.AddWithValue("@GroupId",
                        contact.GroupId > 0
                        ? contact.GroupId
                        : DBNull.Value);

                    cmd.Parameters.AddWithValue("@Phone",
                        string.IsNullOrWhiteSpace(contact.Phone)
                        ? DBNull.Value
                        : contact.Phone);

                    cmd.Parameters.AddWithValue("@Address",
                        string.IsNullOrWhiteSpace(contact.Address)
                        ? DBNull.Value
                        : contact.Address);

                    cmd.Parameters.AddWithValue("@Email",
                        string.IsNullOrWhiteSpace(contact.Email)
                        ? DBNull.Value
                        : contact.Email);

                    cmd.Parameters.AddWithValue("@UserId", contact.UserId);

                    cmd.Parameters.Add(
                        CreatePictureParameter("@Picture", contact.Picture));

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(Contact contact)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
UPDATE dbo.Contacts
SET
    Fname   = @Fname,
    Lname   = @Lname,
    Dob     = @Dob,
    Gender  = @Gender,
    GroupId = @GroupId,
    Phone   = @Phone,
    Address = @Address,
    Email   = @Email,
    UserId  = @UserId,
    Picture = @Picture
WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", contact.Id);

                    cmd.Parameters.AddWithValue("@Fname",
                        string.IsNullOrWhiteSpace(contact.Fname)
                        ? DBNull.Value
                        : contact.Fname);

                    cmd.Parameters.AddWithValue("@Lname", contact.Lname);

                    cmd.Parameters.AddWithValue("@Dob",
                        contact.Dob.HasValue
                        ? contact.Dob.Value
                        : DBNull.Value);

                    cmd.Parameters.AddWithValue("@Gender",
                        string.IsNullOrWhiteSpace(contact.Gender)
                        ? DBNull.Value
                        : contact.Gender);

                    cmd.Parameters.AddWithValue("@GroupId",
                        contact.GroupId > 0
                        ? contact.GroupId
                        : DBNull.Value);

                    cmd.Parameters.AddWithValue("@Phone",
                        string.IsNullOrWhiteSpace(contact.Phone)
                        ? DBNull.Value
                        : contact.Phone);

                    cmd.Parameters.AddWithValue("@Address",
                        string.IsNullOrWhiteSpace(contact.Address)
                        ? DBNull.Value
                        : contact.Address);

                    cmd.Parameters.AddWithValue("@Email",
                        string.IsNullOrWhiteSpace(contact.Email)
                        ? DBNull.Value
                        : contact.Email);

                    cmd.Parameters.AddWithValue("@UserId", contact.UserId);

                    cmd.Parameters.Add(
                        CreatePictureParameter("@Picture", contact.Picture));

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = "DELETE FROM dbo.Contacts WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable Search(int userId, string keyword)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                const string sql = @"
SELECT c.*, g.GroupName
FROM dbo.Contacts c
LEFT JOIN dbo.ContactGroups g ON c.GroupId = g.Id
WHERE c.UserId = @UserId
AND (
    c.Fname LIKE @kw OR
    c.Lname LIKE @kw OR
    c.Phone LIKE @kw OR
    c.Email LIKE @kw
)
ORDER BY c.Lname";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}
