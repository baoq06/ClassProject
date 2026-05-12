using ClassProject.DataAccess.Db;
using BCrypt.Net;
using ClassProject.Presentation.Forms;
using Microsoft.Data.SqlClient;

namespace ClassProject
{
    public partial class LoginForm : Form
    {
        public LoginForm(string registeredUser = "")
        {
            InitializeComponent();
            this.Load += LoginForm_Load;
            if (!string.IsNullOrEmpty(registeredUser))
            {
                txtUsername.Text = registeredUser;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (username == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Username và Password!");
                return;
            }

            My_DB db = new My_DB();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Lấy password hash từ DB thay vì so sánh thẳng
                    string query = "SELECT Password FROM Users WHERE Username = @user";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", username);

                    string? hashedPassword = cmd.ExecuteScalar()?.ToString();

                    // Kiểm tra username tồn tại và verify password
                    if (hashedPassword != null && BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                    {
                        if (chkRememberMe.Checked)
                        {
                            Properties.Settings.Default.Username = username;
                            Properties.Settings.Default.Password = password;
                            Properties.Settings.Default.RememberMe = true;
                        }
                        else
                        {
                            Properties.Settings.Default.Username = "";
                            Properties.Settings.Default.Password = "";
                            Properties.Settings.Default.RememberMe = false;
                        }
                        Properties.Settings.Default.Save();

                        MessageBox.Show("Đăng nhập thành công!");

                        // TODO: mở form chính
                        // MainForm f = new MainForm();
                        // f.Show();
                        // this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối DB: " + ex.Message);
                }
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                if (Properties.Settings.Default.RememberMe)
                {
                    txtUsername.Text = Properties.Settings.Default.Username;
                    txtPassword.Text = Properties.Settings.Default.Password;
                    chkRememberMe.Checked = true;
                }
            }
            if (!string.IsNullOrEmpty(txtUsername.Text))
            {
                txtPassword.Focus();
            }
            else
            {
                txtUsername.Focus();
            }
        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            RegisterForm f = new RegisterForm();
            f.Show();
            this.Hide();
        }

        private void lblForgetPassword_Click(object sender, EventArgs e)
        {
            ForgetPassForm f = new ForgetPassForm();
            f.Show();
            this.Hide();
        }

        private bool _showPassword = false;

        private void picEye_Click(object sender, EventArgs e)
        {
            _showPassword = !_showPassword;

            if (_showPassword)
            {
                txtPassword.UseSystemPasswordChar = false;
                // Icon mắt mở - dùng text thay icon tạm
                picEye.Image = Image.FromFile(@"C:\ClassProject\ClassProject\images\eye.png");
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                picEye.Image = Image.FromFile(@"C:\ClassProject\ClassProject\images\hide.png");
            }
        }
    }
}