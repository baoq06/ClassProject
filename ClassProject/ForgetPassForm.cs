using System;
using System.Windows.Forms;
using MimeKit;
using MailKit.Net.Smtp;
using BCrypt.Net;

namespace ClassProject.Presentation.Forms
{
    public partial class ForgetPassForm : Form
    {
        private string _otp = "";
        private string _verifiedEmail = "";

        public ForgetPassForm()
        {
            InitializeComponent();
            SetOTPSectionVisible(false);
        }

        private void btnSendOTP_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (email == "")
            {
                MessageBox.Show("Vui lòng nhập email!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Users user = new Users();
            if (!user.EmailExists(email))
            {
                MessageBox.Show("Email không tồn tại trong hệ thống!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _otp = new Random().Next(100000, 999999).ToString();
            _verifiedEmail = email;

            if (SendOTPEmail(email, _otp))
            {
                MessageBox.Show("OTP đã được gửi đến email của bạn!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetOTPSectionVisible(true);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string inputOTP = txtOTP.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirm.Text.Trim();

            if (inputOTP != _otp || _otp == "")
            {
                MessageBox.Show("OTP không đúng!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword == "" || confirmPassword == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu không khớp!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Users user = new Users();
                string hashed = BCrypt.Net.BCrypt.HashPassword(newPassword);

                if (user.ResetPassword(_verifiedEmail, hashed))
                {
                    MessageBox.Show("Đặt lại mật khẩu thành công!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoginForm f = new LoginForm();
                    f.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không cập nhật được mật khẩu.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblBacktoLogin_Click(object sender, EventArgs e)
        {
            LoginForm f = new LoginForm();
            f.Show();
            this.Hide();
        }

        private bool SendOTPEmail(string toEmail, string otp)
        {
            try
            {
                string senderEmail = Environment.GetEnvironmentVariable("SENDER_EMAIL");
                string senderPassword = Environment.GetEnvironmentVariable("SENDER_PASSWORD");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("ClassProject", senderEmail));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = "Mã OTP đặt lại mật khẩu";
                message.Body = new TextPart("plain")
                {
                    Text = $"Mã OTP của bạn là: {otp}\n\nMã này chỉ có hiệu lực trong 5 phút."
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(senderEmail, senderPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi email: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void SetOTPSectionVisible(bool visible)
        {
            lblOTP.Visible = visible;
            txtOTP.Visible = visible;
            lblNewPassword.Visible = visible;
            txtNewPassword.Visible = visible;
            lblConfirmPassword.Visible = visible;
            txtConfirm.Visible = visible;
            btnReset.Visible = visible;
        }
    }
}
