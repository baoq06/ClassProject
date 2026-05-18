using ClassProject.Models;
using ClassProject.Services;
using System.Text.Json;

namespace ClassProject.Forms
{
    public class AddStudentForm : HtmlFormBase
    {
        private int _userId;

        public AddStudentForm(int userId)
            : base("Thêm Sinh Viên - Quản Lý Sinh Viên", "UI/Html/addstudent.html", 860, 720)
        {
            _userId = userId;
        }

        protected override void HandleAction(string action, Dictionary<string, JsonElement> data)
        {
            switch (action)
            {
                case "addStudent":
                    DoAddStudent(data);
                    break;
            }
        }

        private async void DoAddStudent(Dictionary<string, JsonElement> data)
        {
            string mssv = data.TryGetValue("mssv", out var v) ? v.GetString() ?? "" : "";
            string firstName = data.TryGetValue("firstName", out v) ? v.GetString() ?? "" : "";
            string lastName = data.TryGetValue("lastName", out v) ? v.GetString() ?? "" : "";
            string dobStr = data.TryGetValue("dateOfBirth", out v) ? v.GetString() ?? "" : "";
            string gender = data.TryGetValue("gender", out v) ? v.GetString() ?? "" : "";
            string phone = data.TryGetValue("phone", out v) ? v.GetString() ?? "" : "";
            string address = data.TryGetValue("address", out v) ? v.GetString() ?? "" : "";
            string hometown = data.TryGetValue("hometown", out v) ? v.GetString() ?? "" : "";
            string email = data.TryGetValue("email", out v) ? v.GetString() ?? "" : "";
            string pictureBase64 = data.TryGetValue("pictureBase64", out v) ? v.GetString() ?? "" : "";

            if (string.IsNullOrEmpty(mssv) || string.IsNullOrEmpty(lastName))
            {
                CallJs("onAddResult(false, 'MSSV và Họ không được để trống!')");
                return;
            }

            if (gender != "" && gender != "Nam" && gender != "Nữ")
            {
                CallJs("onAddResult(false, 'Giới tính phải là Nam hoặc Nữ!')");
                return;
            }

            DateTime? dob = null;
            if (!string.IsNullOrEmpty(dobStr) && DateTime.TryParse(dobStr, out DateTime parsedDob))
            {
                if (parsedDob > DateTime.Now)
                {
                    CallJs("onAddResult(false, 'Ngày sinh không được lớn hơn ngày hiện tại!')");
                    return;
                }
                dob = parsedDob;
            }

            byte[]? picture = null;
            if (!string.IsNullOrEmpty(pictureBase64))
            {
                try
                {
                    string base64Data = pictureBase64.Contains(",")
                        ? pictureBase64.Split(',')[1]
                        : pictureBase64;
                    picture = Convert.FromBase64String(base64Data);
                }
                catch
                {
                    CallJs("onAddResult(false, 'Ảnh không hợp lệ!')");
                    return;
                }
            }

            try
            {
                Student student = new Student
                {
                    Mssv = mssv,
                    FirstName = string.IsNullOrEmpty(firstName) ? lastName : firstName,
                    LastName = lastName,
                    DateOfBirth = dob ?? DateTime.Now.AddYears(-20),
                    Gender = gender,
                    Phone = phone,
                    Address = address,
                    Hometown = hometown,
                    Email = email,
                    Picture = picture
                };

                StudentService service = new StudentService();
                bool ok = await Task.Run(() => service.AddWithNewAccount(student));

                if (ok)
                {
                    string msg = $"Thêm sinh viên thành công!\nTài khoản:\n" +
                                 $"- Username: {mssv}\n- Password: {mssv}";
                    CallJs($"onAddResult(true, '{EscapeJs(msg)}')");
                }
                else
                {
                    CallJs("onAddResult(false, 'Lỗi khi thêm sinh viên!')");
                }
            }
            catch (Exception ex)
            {
                CallJs($"onAddResult(false, 'Lỗi: {EscapeJs(ex.Message)}')");
            }
        }
    }
}
