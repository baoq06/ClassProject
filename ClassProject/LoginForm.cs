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
                Users user = new Users();
                DataTable dt = user.GetByUsername(username);

                string? hashedPassword = null;
                int userId = 0;
                int roleId = -1;
                bool mustChangePassword = false;

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    hashedPassword = row["Password"].ToString();
                    userId = Convert.ToInt32(row["Id"]);
                    roleId = Convert.ToInt32(row["RoleId"]);

                    // Cột MustChangePassword chỉ có sau khi đã chạy migration
                    // 005_must_change_password.sql. Dùng Contains để an toàn nếu
                    // bản DB chưa migrate (sẽ coi như false).
                    if (dt.Columns.Contains("MustChangePassword")
                        && row["MustChangePassword"] != DBNull.Value)
                    {
                        mustChangePassword = Convert.ToBoolean(row["MustChangePassword"]);
                    }
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

                    // ----- ÉP ĐỔI MẬT KHẨU LẦN ĐẦU (chỉ áp cho Student) -----
                    // Sinh viên đăng nhập bằng tài khoản admin cấp (password = MSSV)
                    // sẽ có flag MustChangePassword = 1. Bắt buộc đổi trước khi vào hệ thống.
                    if (mustChangePassword && roleId == 1)
                    {
                        using (var cp = new ChangePasswordForm(userId, username))
                        {
                            var result = cp.ShowDialog(this);
                            if (result != DialogResult.OK)
                            {
                                // User hủy hoặc đóng form -> KHÔNG cho vào hệ thống,
                                // ở lại login để họ có cơ hội thử lại.
                                MessageBox.Show(
                                    "Bạn cần đổi mật khẩu để có thể đăng nhập.",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                                txtPassword.Clear();
                                txtPassword.Focus();
                                return;
                            }

                            // Đổi password thành công -> Properties.Settings.Password
                            // (nếu RememberMe) bị stale. Xóa luôn để lần sau user phải nhập lại.
                            if (Properties.Settings.Default.RememberMe)
                            {
                                Properties.Settings.Default.Password = "";
                                Properties.Settings.Default.Save();
                            }
                        }
                    }
                    // ----- HẾT phần đổi mật khẩu -----

                    // ----- LƯU PHIÊN ĐĂNG NHẬP VÀO Globals -----
                    // Email đã có sẵn trong DataTable dt từ GetByUsername (đã bổ sung
                    // cột Email ở bước 1). Đọc thẳng từ row đầu tiên.
                    string email = "";
                    if (dt.Rows.Count > 0 && dt.Columns.Contains("Email")
                        && dt.Rows[0]["Email"] != DBNull.Value)
                    {
                        email = dt.Rows[0]["Email"].ToString() ?? "";
                    }
                    Globals.SetSession(userId, username, roleId, email);
                    // ----- HẾT phần Globals -----

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