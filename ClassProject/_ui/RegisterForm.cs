using ClassProject;
using ClassProject._db;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BCrypt.Net;

namespace ClassProject.Presentation.Forms
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
            string confirmPassword = txtConfirm.Text.Trim();

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

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show(
                    "Invalid email format",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

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

            int roleId;
            using (RegisterRoleForm roleForm = new RegisterRoleForm())
            {
                roleForm.StartPosition = FormStartPosition.CenterParent;
                if (roleForm.ShowDialog(this) != DialogResult.OK)
                    return;

                roleId = roleForm.SelectedRoleId;
            }

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    int newUserId;

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
                                "OUTPUT INSERTED.Id " +
                                "VALUES " +
                                "(@username, @email, @password, @roleId)";

                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, tx))
                            {
                                insertCmd.Parameters.AddWithValue("@username", username);
                                insertCmd.Parameters.AddWithValue("@email", email);
                                insertCmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(password));
                                insertCmd.Parameters.AddWithValue("@roleId", roleId);

                                object scalar = insertCmd.ExecuteScalar();
                                if (scalar == null || scalar == DBNull.Value)
                                {
                                    tx.Rollback();
                                    MessageBox.Show(
                                        "Register failed: could not read new user id.",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error
                                    );
                                    return;
                                }

                                newUserId = Convert.ToInt32(scalar);
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

                    if (roleId == 1)
                    {
                        AddStudentForm addStudent = new AddStudentForm(newUserId);
                        addStudent.Show();
                    }
                    else
                    {
                        // TODO: khi có AddLecturerForm
                        // AddLecturerForm addLecturer = new AddLecturerForm(newUserId);
                        // addLecturer.Show();
                        MessageBox.Show(
                            "Đăng ký giảng viên thành công. Form bổ sung thông tin (AddLecturerForm) sẽ thêm sau.",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }

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

        private void lblBacktoLogin_Click(object sender, EventArgs e)
        {
            LoginForm f = new LoginForm();
            f.Show();
            this.Hide();
        }
    }
}