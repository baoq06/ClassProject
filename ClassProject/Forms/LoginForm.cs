using ClassProject.Models;
using ClassProject.Services;
using System.Text.Json;

namespace ClassProject.Forms
{
    public class LoginForm : HtmlFormBase
    {
        public LoginForm() : base("Đăng Nhập - Quản Lý Sinh Viên", "UI/Html/login.html", 440, 560)
        {
        }

        protected override async void HandleAction(string action, Dictionary<string, JsonElement> data)
        {
            switch (action)
            {
                case "loaded":
                    LoadSavedCredentials();
                    break;

                case "login":
                    string username = data.TryGetValue("username", out var u) ? u.GetString() ?? "" : "";
                    string password = data.TryGetValue("password", out var p) ? p.GetString() ?? "" : "";
                    bool remember = data.TryGetValue("remember", out var r) && r.GetBoolean();
                    await DoLogin(username, password, remember);
                    break;

                case "forgotPassword":
                    ShowForgotPassword();
                    break;
            }
        }

        private void LoadSavedCredentials()
        {
            if (Properties.Settings.Default.RememberMe)
            {
                string user = Properties.Settings.Default.Username;
                string pass = Properties.Settings.Default.Password;
                CallJs($"document.getElementById('txtUsername').value='{EscapeJs(user)}';" +
                       $"document.getElementById('txtPassword').value='{EscapeJs(pass)}';" +
                       $"document.getElementById('chkRemember').checked=true;");
            }
        }

        private async Task DoLogin(string username, string password, bool remember)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await Task.Run(() => MessageBox.Show("Vui lòng nhập đầy đủ Username và Password!"));
                CallJs($"onLoginResult(false, 'Vui lòng nhập đầy đủ Username và Password!')");
                return;
            }

            try
            {
                Users user = new Users();
                var dt = await Task.Run(() => user.GetByUsername(username));

                string? hashedPassword = null;
                int userId = 0;
                int roleId = -1;

                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    hashedPassword = row["Password"]?.ToString();
                    userId = Convert.ToInt32(row["Id"]);
                    roleId = Convert.ToInt32(row["RoleId"]);
                }

                if (hashedPassword != null && BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                {
                    if (remember)
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

                    string roleName = roleId == 0 ? "Admin" : (roleId == 2 ? "HR" : "Sinh viên");

                    this.Invoke(() =>
                    {
                        if (roleId == 0)
                        {
                            MessageBox.Show($"Đăng nhập Admin thành công! ({roleName})",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            AdminForm f = new AdminForm();
                            f.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show($"Đăng nhập thành công! ({roleName})",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            AddStudentForm f = new AddStudentForm(userId);
                            f.Show();
                            this.Hide();
                        }
                    });

                    CallJs($"onLoginResult(true, 'Đăng nhập thành công! ({roleName})')");
                }
                else
                {
                    CallJs("onLoginResult(false, 'Sai tài khoản hoặc mật khẩu!')");
                }
            }
            catch (Exception ex)
            {
                string msg = "Lỗi kết nối DB: " + ex.Message;
                CallJs($"onLoginResult(false, '{EscapeJs(msg)}')");
            }
        }

        private void ShowForgotPassword()
        {
            this.Invoke(() =>
            {
                ForgetPassForm f = new ForgetPassForm();
                f.Show();
                this.Hide();
            });
        }
    }
}
