using Microsoft.Data.SqlClient;
using ClassProject.DataAccess.Db;
using BCrypt.Net;


namespace ClassProject
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
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

        private void lblAccountLogin_Click(object sender, EventArgs e)
        {

        }
    }
}