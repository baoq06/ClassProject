using Microsoft.Data.SqlClient;

namespace ClassProject
{
    public partial class LoginForm : Form
    {
        /// <summary>
        /// private UserService userService = new UserService();
        /// </summary>

        public LoginForm()
        {
            InitializeComponent();
            //this.Load += LoginForm_Load;
        }

        //private void btnLogin_Click(object sender, EventArgs e)
        //{
        //    string username = txtUsername.Text.Trim();
        //    string password = txtPassword.Text.Trim();

        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //    {
        //        MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
        //        return;
        //    }

        //    try
        //    {
        //        bool isLogin = userService.Login(username, password);

        //        if (isLogin)
        //        {
        //            if (chkRememberMe.Checked)
        //            {
        //                Properties.Settings.Default.Username = username;
        //                Properties.Settings.Default.Password = password;
        //                Properties.Settings.Default.RememberMe = true;
        //            }
        //            else
        //            {
        //                Properties.Settings.Default.Username = "";
        //                Properties.Settings.Default.Password = "";
        //                Properties.Settings.Default.RememberMe = false;
        //            }
        //            Properties.Settings.Default.Save();

        //            MessageBox.Show("Đăng nhập thành công!");

        //            //MainForm main = new MainForm();
        //            //main.Show();
        //            //this.Hide();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
        //            txtPassword.Clear();
        //            txtPassword.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi hệ thống: " + ex.Message);
        //    }
        //}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Kiểm tra nhập liệu
            if (username == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Username và Password!");
                return;
            }

            string connectionString = @"Data Source=DESKTOP-P9JRTKL\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM Users WHERE Username=@user AND Password=@pass";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    int result = (int)cmd.ExecuteScalar();

                    if (result > 0)
                    {
                        MessageBox.Show("Đăng nhập thành công!");

                        // TODO: mở form chính
                        // MainForm f = new MainForm();
                        // f.Show();
                        // this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối DB: " + ex.Message);
                }
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        //private void btnCancel_Click(object sender, EventArgs e)
        //{
        //    Application.Exit();
        //}

        //private void txtPassword_KeyDown(object sender, KeyEventArgs e) { 
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        btnLogin.PerformClick();
        //    }
        //}
        //private void LoginForm_Load(object sender, EventArgs e)
        //{
        //    if (Properties.Settings.Default.RememberMe)
        //    {
        //        txtUsername.Text = Properties.Settings.Default.Username;
        //        txtPassword.Text = Properties.Settings.Default.Password;
        //        chkRememberMe.Checked = true;
        //    }

        //    txtUsername.Focus();
        //}
    }
}
