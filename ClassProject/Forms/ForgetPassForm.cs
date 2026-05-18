using ClassProject.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text.Json;

namespace ClassProject.Forms
{
    public class ForgetPassForm : HtmlFormBase
    {
        private string _otp;
        private string _verifiedEmail;

        public ForgetPassForm() : base("Quên mật khẩu", "UI/Html/forgotpass.html", 440, 520)
        {
        }

        protected override async void HandleAction(string action, Dictionary<string, JsonElement> data)
        {
            switch (action)
            {
                case "sendOTP":
                    string email = data.TryGetValue("email", out var e) ? e.GetString() ?? "" : "";
                    await SendOTP(email);
                    break;

                case "verifyOTP":
                    string vEmail = data.TryGetValue("email", out var em) ? em.GetString() ?? "" : "";
                    string otp = data.TryGetValue("otp", out var o) ? o.GetString() ?? "" : "";
                    string newPassword = data.TryGetValue("newPassword", out var np) ? np.GetString() ?? "" : "";
                    string confirmPassword = data.TryGetValue("confirmPassword", out var cp) ? cp.GetString() ?? "" : "";
                    await VerifyOTP(vEmail, otp, newPassword, confirmPassword);
                    break;

                case "backToLogin":
                    BackToLogin();
                    break;
            }
        }

        private async Task SendOTP(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                CallJs("onSendOTPResult(false, 'Vui lòng nhập email!')");
                return;
            }

            try
            {
                Users user = new Users();
                bool exists = await Task.Run(() => user.EmailExists(email));

                if (!exists)
                {
                    CallJs("onSendOTPResult(false, 'Email không tồn tại trong hệ thống!')");
                    return;
                }

                var rnd = new Random();
                _otp = rnd.Next(100000, 999999).ToString();
                _verifiedEmail = email;

                string senderEmail = Environment.GetEnvironmentVariable("SENDER_EMAIL") ?? "";
                string senderPassword = Environment.GetEnvironmentVariable("SENDER_PASSWORD") ?? "";

                if (string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(senderPassword))
                {
                    CallJs($"onSendOTPResult(true, ''); document.getElementById('lblOTPStatus').innerText = 'OTP: {_otp} (DEV MODE)';");
                    return;
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Student Management", senderEmail));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Mã OTP đặt lại mật khẩu";

                message.Body = new TextPart("plain")
                {
                    Text = $"Mã OTP của bạn là: {_otp}\n\n" +
                           $"Mã này có hiệu lực trong 10 phút.\n" +
                           $"Vui lòng không chia sẻ mã này với bất kỳ ai."
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(senderEmail, senderPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                CallJs("onSendOTPResult(true, '')");
            }
            catch (Exception ex)
            {
                CallJs($"onSendOTPResult(false, 'Lỗi gửi email: {EscapeJs(ex.Message)}')");
            }
        }

        private async Task VerifyOTP(string email, string otp, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(otp) ||
                string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                CallJs("onVerifyOTPResult(false, 'Vui lòng điền đầy đủ thông tin!')");
                return;
            }

            if (email != _verifiedEmail)
            {
                CallJs("onVerifyOTPResult(false, 'Email không khớp với email đã xác nhận OTP!')");
                return;
            }

            if (otp != _otp)
            {
                CallJs("onVerifyOTPResult(false, 'Mã OTP không chính xác!')");
                return;
            }

            if (newPassword != confirmPassword)
            {
                CallJs("onVerifyOTPResult(false, 'Mật khẩu xác nhận không khớp!')");
                return;
            }

            if (newPassword.Length < 6)
            {
                CallJs("onVerifyOTPResult(false, 'Mật khẩu phải có ít nhất 6 ký tự!')");
                return;
            }

            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                Users user = new Users();
                bool success = await Task.Run(() => user.ResetPassword(email, hashedPassword));

                if (success)
                {
                    _otp = null;
                    _verifiedEmail = null;
                    CallJs("onVerifyOTPResult(true, 'Đặt lại mật khẩu thành công!')");
                }
                else
                {
                    CallJs("onVerifyOTPResult(false, 'Không thể đặt lại mật khẩu!')");
                }
            }
            catch (Exception ex)
            {
                CallJs($"onVerifyOTPResult(false, 'Lỗi: {EscapeJs(ex.Message)}')");
            }
        }

        private void BackToLogin()
        {
            this.Invoke(() =>
            {
                LoginForm f = new LoginForm();
                f.Show();
                this.Close();
            });
        }
    }
}
