using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;

namespace ClassProject
{
    public partial class RegisterForm : Form
    {
        My_DB db = new My_DB();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Check empty fields
            if (
                username == "" ||
                email == "" ||
                password == "" ||
                confirmPassword == ""
            )
            {
                MessageBox.Show(
                    "Please fill all fields",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Check password match
            if (password != confirmPassword)
            {
                MessageBox.Show(
                    "Password does not match",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    using (SqlTransaction tx = conn.BeginTransaction())
                    {
                        try
                        {
                            const string checkQuery =
                                "SELECT COUNT(*) FROM dbo.Users " +
                                "WHERE Username = @username OR Email = @email";

                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, tx))
                            {
                                checkCmd.Parameters.AddWithValue("@username", username);
                                checkCmd.Parameters.AddWithValue("@email", email);

                                int existing = Convert.ToInt32(checkCmd.ExecuteScalar());

                                if (existing > 0)
                                {
                                    MessageBox.Show(
                                        "Username or Email already exists",
                                        "Warning",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning
                                    );

                                    return;
                                }
                            }

                            const string insertQuery =
                                "INSERT INTO dbo.Users " +
                                "(Username, Email, Password, RoleId) " +
                                "VALUES " +
                                "(@username, @email, @password, 2)";

                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, tx))
                            {
                                insertCmd.Parameters.AddWithValue("@username", username);
                                insertCmd.Parameters.AddWithValue("@email", email);
                                insertCmd.Parameters.AddWithValue("@password", password);
                                insertCmd.ExecuteNonQuery();
                            }

                            const string verifyQuery =
                                "SELECT COUNT(*) FROM dbo.Users " +
                                "WHERE Username = @username AND Email = @email";

                            using (SqlCommand verifyCmd = new SqlCommand(verifyQuery, conn, tx))
                            {
                                verifyCmd.Parameters.AddWithValue("@username", username);
                                verifyCmd.Parameters.AddWithValue("@email", email);

                                int saved = Convert.ToInt32(verifyCmd.ExecuteScalar());

                                if (saved != 1)
                                {
                                    tx.Rollback();
                                    MessageBox.Show(
                                        "Register failed: data was not saved to the database.",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error
                                    );

                                    return;
                                }
                            }

                            tx.Commit();
                        }
                        catch
                        {
                            try
                            {
                                tx.Rollback();
                            }
                            catch
                            {
                                // ignore rollback errors
                            }

                            throw;
                        }
                    }

                    MessageBox.Show(
                        "Register successful",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}