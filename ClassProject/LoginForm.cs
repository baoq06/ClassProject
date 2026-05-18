using BCrypt.Net;
using ClassProject.Presentation.Forms;
using System.Data;

namespace ClassProject
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Load += LoginForm_Load;
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

            try
            {
                UserRepository user = new UserRepository();
                DataTable dt = user.GetByUsername(username);

                string? hashedPassword = null;
                int userId = 0;
                int roleId = -1;

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    hashedPassword = row["Password"].ToString();
                    userId = Convert.ToInt32(row["Id"]);
                    roleId = Convert.ToInt32(row["RoleId"]);
                }

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

                    if (roleId == 0)
                    {
                        MessageBox.Show("Đăng nhập Admin thành công!");

                        AdminForm f = new AdminForm();
                        f.Show();

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thành công!");
                        StudentForm f = new StudentForm(userId);
                        f.Show();
                        this.Hide();
                    }
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
                string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                string eyePath = Path.Combine(projectDir, "images", "hide.png");
                picEye.Image = Image.FromFile(eyePath);
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                string eyePath = Path.Combine(projectDir, "images", "hide.png");
                picEye.Image = Image.FromFile(eyePath);
            }
        }
    }
}
