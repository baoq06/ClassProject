using ClassProject.DataAccess.Db;
using ClassProject.Models;
using ClassProject.Repositories;
using ClassProject.Services;
using ClosedXML.Excel;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.Data;
using System.Text;
using System.Text.Json;

namespace ClassProject.Forms
{
    public partial class AdminForm : Form
    {
        private Panel leftPanel;
        private WebView2 webView;
        private Button[] menuButtons;

        private readonly Color _sidebarColor = Color.FromArgb(26, 42, 108);
        private readonly Color _activeColor = Color.FromArgb(41, 128, 185);
        private readonly Color _hoverColor = Color.FromArgb(52, 73, 140);

        private readonly StudentRepository _studentRepo = new();
        private readonly PendingStudentRepository _pendingRepo = new();
        private readonly CourseRepository _courseRepo = new();
        private readonly ScoreRepository _scoreRepo = new();
        private readonly ContactRepository _contactRepo = new();
        private readonly DKMHRepository _dkmhRepo = new();
        private readonly AssignRepository _assignRepo = new();
        private readonly StudentService _studentService = new();

        private readonly string[] _menuItems =
        {
            "Duyệt tài khoản", "Sinh viên", "Thêm sinh viên",
            "Môn học", "Điểm số", "Danh bạ",
            "Thống kê", "Xuất báo cáo", "Đăng xuất"
        };

        private readonly string[] _htmlPages =
        {
            "UI/Html/approve.html", "UI/Html/viewstudents.html", "UI/Html/addstudent.html",
            "UI/Html/courses.html", "UI/Html/scores.html", "UI/Html/contacts.html",
            "UI/Html/statistics.html", "UI/Html/export.html", null
        };

        public AdminForm()
        {
            Text = "Admin - Quản Lý Sinh Viên";
            Size = new Size(1300, 750);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await InitializeUI();
        }

        private async Task InitializeUI()
        {
            leftPanel = new Panel
            {
                Width = 220,
                Dock = DockStyle.Left,
                BackColor = _sidebarColor
            };

            var lblLogo = new Label
            {
                Text = "QUẢN LÝ SINH VIÊN",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(18, 34, 90)
            };
            leftPanel.Controls.Add(lblLogo);

            menuButtons = new Button[_menuItems.Length];
            for (int i = 0; i < _menuItems.Length; i++)
            {
                int idx = i;
                var btn = new Button
                {
                    Text = _menuItems[i],
                    Dock = DockStyle.Top,
                    Height = 48,
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = _sidebarColor
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = _hoverColor;
                btn.FlatAppearance.MouseDownBackColor = _activeColor;
                btn.MouseEnter += (s, ev) =>
                {
                    if (btn.BackColor != _activeColor)
                        btn.BackColor = _hoverColor;
                };
                btn.MouseLeave += (s, ev) =>
                {
                    if (btn.BackColor != _activeColor)
                        btn.BackColor = _sidebarColor;
                };

                if (i == _menuItems.Length - 1)
                {
                    btn.Click += (s, ev) => Logout();
                }
                else
                {
                    btn.Click += (s, ev) =>
                    {
                        SetActiveButton(idx);
                        LoadHtml(_htmlPages[idx]);
                    };
                }

                leftPanel.Controls.Add(btn);
                menuButtons[i] = btn;
            }

            Controls.Add(leftPanel);

            webView = new WebView2 { Dock = DockStyle.Fill };
            Controls.Add(webView);

            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.WebMessageReceived += WebView_WebMessageReceived;

            SetActiveButton(0);
            LoadHtml(_htmlPages[0]);
        }

        private void SetActiveButton(int index)
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].BackColor = i == index ? _activeColor : _sidebarColor;
            }
        }

        private void LoadHtml(string relativePath)
        {
            if (relativePath == null) return;
            string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string fullPath = Path.Combine(projectDir, relativePath.Replace('/', '\\'));

            if (File.Exists(fullPath))
                webView.CoreWebView2.Navigate(new Uri(fullPath).AbsoluteUri);
            else
                webView.CoreWebView2.NavigateToString(
                    $"<html><body style='font-family:sans-serif;text-align:center;padding-top:100px;color:red'>" +
                    $"<h2>UI file not found</h2><p>{fullPath}</p></body></html>");
        }

        private void Logout()
        {
            var login = new LoginForm();
            login.Show();
            Close();
        }

        private void WebView_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                string json = e.TryGetWebMessageAsString();
                if (string.IsNullOrEmpty(json)) return;

                var msg = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
                if (msg == null || !msg.ContainsKey("action")) return;

                string action = msg["action"].GetString() ?? "";
                HandleAction(action, msg);
            }
            catch (Exception ex)
            {
                RunJs("showToast('Lỗi: " + EscapeJs(ex.Message) + "', 'error')");
            }
        }

        private async void HandleAction(string action, Dictionary<string, JsonElement> data)
        {
            try
            {
                switch (action)
                {
                    case "loadPending": await LoadPending(); break;
                    case "approveStudent": await ApproveStudent(data); break;
                    case "rejectStudent": await RejectStudent(data); break;
                    case "uploadExcel": await UploadExcel(data); break;

                    case "searchStudents": await SearchStudents(data); break;
                    case "getStudent": await GetStudent(data); break;
                    case "addStudent": await AddStudent(data); break;
                    case "editStudent": await EditStudent(data); break;
                    case "deleteStudent": await DeleteStudent(data); break;

                    case "loadCourses": await LoadCourses(); break;
                    case "getCourse": await GetCourse(data); break;
                    case "addCourse": await AddCourse(data); break;
                    case "editCourse": await EditCourse(data); break;
                    case "deleteCourse": await DeleteCourse(data); break;

                    case "loadStudents": await LoadAllStudents(); break;
                    case "loadScoresByCourse": await LoadScoresByCourse(data); break;
                    case "loadScoresByStudent": await LoadScoresByStudent(data); break;
                    case "addScore": await AddScore(data); break;
                    case "deleteScore": await DeleteScore(data); break;

                    case "loadGroups": await LoadGroups(data); break;
                    case "addGroup": await AddGroup(data); break;
                    case "deleteGroup": await DeleteGroup(data); break;
                    case "loadContacts": await LoadContacts(data); break;
                    case "getContact": await GetContact(data); break;
                    case "addContact": await AddContact(data); break;
                    case "editContact": await EditContact(data); break;
                    case "deleteContact": await DeleteContact(data); break;

                    case "loadStatistics": await LoadStatistics(); break;

                    case "exportExcel": await ExportExcel(data); break;
                }
            }
            catch (Exception ex)
            {
                RunJs("showToast('" + EscapeJs(ex.Message) + "', 'error')");
            }
        }

        // ============ PENDING / APPROVE ============

        private async Task LoadPending()
        {
            var dt = await Task.Run(() => _pendingRepo.GetAll());
            await RunJsAsync("onDataLoaded(" + DataTableToJson(dt) + ")");
        }

        private async Task ApproveStudent(Dictionary<string, JsonElement> data)
        {
            int pendingId = GetInt(data, "pendingId");
            string mssv = GetStr(data, "mssv");
            bool ok = await Task.Run(() => _studentService.ApprovePending(pendingId, mssv, out _));
            if (ok) await LoadPending();
            await RunJsAsync("showToast('" + (ok ? "Duyệt thành công" : "Duyệt thất bại") + "', '" + (ok ? "success" : "error") + "')");
        }

        private async Task RejectStudent(Dictionary<string, JsonElement> data)
        {
            int pendingId = GetInt(data, "pendingId");
            bool ok = await Task.Run(() => _pendingRepo.Reject(pendingId));
            if (ok) await LoadPending();
            await RunJsAsync("showToast('" + (ok ? "Đã từ chối" : "Từ chối thất bại") + "', '" + (ok ? "success" : "error") + "')");
        }

        private async Task UploadExcel(Dictionary<string, JsonElement> data)
        {
            string fileData = GetStr(data, "data");
            string fileName = GetStr(data, "fileName");
            if (string.IsNullOrEmpty(fileData))
            {
                await RunJsAsync("onUploadResult(false, 'Không có dữ liệu file')");
                return;
            }

            try
            {
                int commaIdx = fileData.IndexOf(',');
                if (commaIdx >= 0) fileData = fileData.Substring(commaIdx + 1);
                byte[] bytes = Convert.FromBase64String(fileData);

                int inserted = 0, skipped = 0;
                var errors = new StringBuilder();

                await Task.Run(() =>
                {
                    string tempPath = Path.GetTempFileName() + ".xlsx";
                    File.WriteAllBytes(tempPath, bytes);
                    try
                    {
                        using var wb = new XLWorkbook(tempPath);
                        var ws = wb.Worksheets.First();
                        int lastRow = ws.LastRowUsed()?.RowNumber() ?? 0;

                        for (int row = 2; row <= lastRow; row++)
                        {
                            try
                            {
                                string mssv = ws.Cell(row, 1).GetString().Trim();
                                string lastName = ws.Cell(row, 2).GetString().Trim();
                                string firstName = ws.Cell(row, 3).GetString().Trim();
                                DateTime? dob = null;
                                try { dob = ws.Cell(row, 4).GetDateTime(); } catch { }
                                string gender = ws.Cell(row, 5).GetString().Trim();
                                string phone = ws.Cell(row, 6).GetString().Trim();
                                string address = ws.Cell(row, 7).GetString().Trim();
                                string hometown = ws.Cell(row, 8).GetString().Trim();
                                string email = ws.Cell(row, 9).GetString().Trim();

                                if (string.IsNullOrEmpty(mssv) || string.IsNullOrEmpty(lastName))
                                { skipped++; errors.AppendLine($"Dòng {row}: Thiếu MSSV hoặc Họ"); continue; }
                                if (_pendingRepo.IsMssvExists(mssv))
                                { skipped++; errors.AppendLine($"Dòng {row}: MSSV {mssv} đã tồn tại"); continue; }

                                _pendingRepo.Insert(mssv, firstName, lastName, dob, gender, phone, address, hometown, email);
                                inserted++;
                            }
                            catch (Exception ex)
                            { skipped++; errors.AppendLine($"Dòng {row}: {ex.Message}"); }
                        }
                    }
                    finally { try { File.Delete(tempPath); } catch { } }
                });

                string msg = $"Đã thêm: {inserted} sinh viên\nBỏ qua: {skipped}";
                await RunJsAsync("onUploadResult(true, '" + EscapeJs(msg) + "')");
                await LoadPending();
            }
            catch (Exception ex)
            {
                await RunJsAsync("onUploadResult(false, '" + EscapeJs(ex.Message) + "')");
            }
        }

        // ============ STUDENTS ============

        private async Task SearchStudents(Dictionary<string, JsonElement> data)
        {
            string kw = GetStr(data, "keyword");
            var dt = await Task.Run(() => string.IsNullOrEmpty(kw) ? _studentRepo.GetAll() : _studentRepo.Search(kw));
            await RunJsAsync("onStudentsLoaded(" + DataTableToJson(dt) + ")");
        }

        private async Task GetStudent(Dictionary<string, JsonElement> data)
        {
            int id = GetInt(data, "id");
            var dt = await Task.Run(() => _studentRepo.GetById(id));
            await RunJsAsync("onStudentLoaded(" + DataTableToJson(dt) + ")");
        }

        private async Task AddStudent(Dictionary<string, JsonElement> data)
        {
            var student = new Student
            {
                Mssv = GetStr(data, "mssv"),
                FirstName = GetStr(data, "firstName"),
                LastName = GetStr(data, "lastName"),
                DateOfBirth = DateTime.TryParse(GetStr(data, "dateOfBirth"), out var dob) ? dob : DateTime.Now,
                Gender = GetStr(data, "gender"),
                Phone = GetStr(data, "phone"),
                Address = GetStr(data, "address"),
                Hometown = GetStr(data, "hometown"),
                Email = GetStr(data, "email"),
                Picture = GetPicture(data, "pictureBase64")
            };
            bool ok = await Task.Run(() => _studentService.AddWithNewAccount(student));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? $"Thêm sinh viên {student.Mssv} thành công" : "Thêm thất bại") + "')");
        }

        private async Task EditStudent(Dictionary<string, JsonElement> data)
        {
            int id = GetInt(data, "id");
            bool ok = await Task.Run(() => _studentRepo.UpdateById(
                id, GetStr(data, "firstName"), GetStr(data, "lastName"),
                DateTime.TryParse(GetStr(data, "dateOfBirth"), out var dob) ? dob : DateTime.Now,
                GetStr(data, "gender"), GetStr(data, "phone"),
                GetStr(data, "address"), GetStr(data, "hometown"),
                GetStr(data, "email"), GetPicture(data, "picture")));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Cập nhật thành công" : "Cập nhật thất bại") + "')");
        }

        private async Task DeleteStudent(Dictionary<string, JsonElement> data)
        {
            int id = GetInt(data, "id");
            bool ok = await Task.Run(() => _studentRepo.DeleteById(id));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Xoá thành công" : "Xoá thất bại") + "')");
        }

        // ============ COURSES ============

        private async Task LoadCourses()
        {
            var dt = await Task.Run(() => _courseRepo.GetAll());
            await RunJsAsync("onCoursesLoaded(" + DataTableToJson(dt) + ")");
        }

        private async Task GetCourse(Dictionary<string, JsonElement> data)
        {
            string mamh = GetStr(data, "mamh");
            var dt = await Task.Run(() => _courseRepo.GetById(mamh));
            await RunJsAsync("onCourseDetail(" + DataTableToJson(dt) + ")");
        }

        private async Task AddCourse(Dictionary<string, JsonElement> data)
        {
            var course = new Course
            {
                Mamh = GetStr(data, "mamh"),
                Tenmh = GetStr(data, "tenmh"),
                Sotc = GetInt(data, "sotc"),
                Tuan = GetInt(data, "tuan"),
                Hocky = GetInt(data, "hocky"),
                Description = GetStr(data, "description")
            };
            bool ok = await Task.Run(() => _courseRepo.Add(course));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Thêm môn học thành công" : "Thêm thất bại") + "')");
        }

        private async Task EditCourse(Dictionary<string, JsonElement> data)
        {
            var course = new Course
            {
                Mamh = GetStr(data, "mamh"),
                Tenmh = GetStr(data, "tenmh"),
                Sotc = GetInt(data, "sotc"),
                Tuan = GetInt(data, "tuan"),
                Hocky = GetInt(data, "hocky"),
                Description = GetStr(data, "description")
            };
            bool ok = await Task.Run(() => _courseRepo.Update(course));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Cập nhật môn học thành công" : "Cập nhật thất bại") + "')");
        }

        private async Task DeleteCourse(Dictionary<string, JsonElement> data)
        {
            string mamh = GetStr(data, "mamh");
            bool ok = await Task.Run(() => _courseRepo.Delete(mamh));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Xoá môn học thành công" : "Xoá thất bại") + "')");
        }

        // ============ SCORES ============

        private async Task LoadAllStudents()
        {
            var dt = await Task.Run(() => _studentRepo.GetAll());
            await RunJsAsync("onStudentsLoaded(" + DataTableToJson(dt) + ")");
        }

        private async Task LoadScoresByCourse(Dictionary<string, JsonElement> data)
        {
            string mamh = GetStr(data, "mamh");
            var dt = await Task.Run(() => _scoreRepo.GetByCourse(mamh));
            await RunJsAsync("onScoresLoaded(" + DataTableToJson(dt) + ")");
        }

        private async Task LoadScoresByStudent(Dictionary<string, JsonElement> data)
        {
            string mssv = GetStr(data, "mssv");
            var dt = await Task.Run(() => _scoreRepo.GetByStudent(mssv));
            await RunJsAsync("onScoresLoaded(" + DataTableToJson(dt) + ")");
        }

        private async Task AddScore(Dictionary<string, JsonElement> data)
        {
            string mssv = GetStr(data, "mssv");
            string mamh = GetStr(data, "mamh");
            decimal diemqt = GetDecimal(data, "diemqt");
            decimal diemck = GetDecimal(data, "diemck");
            string mota = GetStr(data, "mota");
            bool ok = await Task.Run(() => _scoreRepo.AddOrUpdate(mssv, mamh, diemqt, diemck, mota));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Lưu điểm thành công" : "Lưu điểm thất bại") + "')");
        }

        private async Task DeleteScore(Dictionary<string, JsonElement> data)
        {
            string mssv = GetStr(data, "mssv");
            string mamh = GetStr(data, "mamh");
            bool ok = await Task.Run(() => _scoreRepo.Delete(mssv, mamh));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Xoá điểm thành công" : "Xoá điểm thất bại") + "')");
        }

        // ============ CONTACTS ============

        private async Task LoadGroups(Dictionary<string, JsonElement> data)
        {
            int userId = GetInt(data, "userId");
            var dt = await Task.Run(() => _contactRepo.GetAllGroups(userId));
            await RunJsAsync("onGroupsLoaded(" + DataTableToJson(dt) + ")");
        }

        private async Task AddGroup(Dictionary<string, JsonElement> data)
        {
            string name = GetStr(data, "groupName");
            int userId = GetInt(data, "userId");
            bool ok = await Task.Run(() => _contactRepo.AddGroup(name, userId));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Thêm nhóm thành công" : "Thêm nhóm thất bại") + "')");
        }

        private async Task DeleteGroup(Dictionary<string, JsonElement> data)
        {
            int id = GetInt(data, "id");
            bool ok = await Task.Run(() => _contactRepo.DeleteGroup(id));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Xoá nhóm thành công" : "Xoá nhóm thất bại") + "')");
        }

        private async Task LoadContacts(Dictionary<string, JsonElement> data)
        {
            int userId = GetInt(data, "userId");
            int groupId = GetInt(data, "groupId");
            var dt = await Task.Run(() => _contactRepo.GetAll(userId));
            await RunJsAsync("onContactsLoaded(" + DataTableToJson(dt) + ")");
        }

        private async Task GetContact(Dictionary<string, JsonElement> data)
        {
            int id = GetInt(data, "id");
            var dt = await Task.Run(() => _contactRepo.GetById(id));
            await RunJsAsync("onContactLoaded(" + DataTableToJson(dt) + ")");
        }

        private async Task AddContact(Dictionary<string, JsonElement> data)
        {
            var contact = new Contact
            {
                Fname = GetStr(data, "fname"),
                Lname = GetStr(data, "lname"),
                Dob = DateTime.TryParse(GetStr(data, "dob"), out var dob) ? dob : null,
                Gender = GetStr(data, "gender"),
                GroupId = GetInt(data, "groupId"),
                Phone = GetStr(data, "phone"),
                Address = GetStr(data, "address"),
                Email = GetStr(data, "email"),
                UserId = GetInt(data, "userId"),
                Picture = GetPicture(data, "picture")
            };
            bool ok = await Task.Run(() => _contactRepo.Add(contact));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Thêm liên hệ thành công" : "Thêm thất bại") + "')");
        }

        private async Task EditContact(Dictionary<string, JsonElement> data)
        {
            var contact = new Contact
            {
                Id = GetInt(data, "id"),
                Fname = GetStr(data, "fname"),
                Lname = GetStr(data, "lname"),
                Dob = DateTime.TryParse(GetStr(data, "dob"), out var dob) ? dob : null,
                Gender = GetStr(data, "gender"),
                GroupId = GetInt(data, "groupId"),
                Phone = GetStr(data, "phone"),
                Address = GetStr(data, "address"),
                Email = GetStr(data, "email"),
                UserId = GetInt(data, "userId"),
                Picture = GetPicture(data, "picture")
            };
            bool ok = await Task.Run(() => _contactRepo.Update(contact));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Cập nhật liên hệ thành công" : "Cập nhật thất bại") + "')");
        }

        private async Task DeleteContact(Dictionary<string, JsonElement> data)
        {
            int id = GetInt(data, "id");
            bool ok = await Task.Run(() => _contactRepo.Delete(id));
            await RunJsAsync("onOperationResult(" + (ok ? "true" : "false") + ", '" +
                EscapeJs(ok ? "Xoá liên hệ thành công" : "Xoá thất bại") + "')");
        }

        // ============ STATISTICS ============

        private async Task LoadStatistics()
        {
            string json = await Task.Run(() =>
            {
                var dtStudents = _studentRepo.GetAll();
                int total = dtStudents.Rows.Count;
                int male = 0, female = 0;
                foreach (DataRow row in dtStudents.Rows)
                {
                    string g = row["Gender"]?.ToString() ?? "";
                    if (g == "Nam") male++;
                    else if (g == "Nữ") female++;
                }

                var sb = new StringBuilder();
                sb.Append("{\"totalStudents\":").Append(total);
                sb.Append(",\"maleStudents\":").Append(male);
                sb.Append(",\"femaleStudents\":").Append(female);
                sb.Append(",\"otherStudents\":").Append(total - male - female);
                sb.Append(",\"scoreSummary\":[");
                bool first = true;
                foreach (DataRow row in dtStudents.Rows)
                {
                    string mssv = row["MSSV"]?.ToString() ?? "";
                    if (string.IsNullOrEmpty(mssv)) continue;
                    var dtScore = _scoreRepo.GetByStudent(mssv);
                    if (dtScore.Rows.Count == 0) continue;
                    decimal sum = 0, count = 0;
                    foreach (DataRow sr in dtScore.Rows)
                    {
                        if (sr["Diemtk"] != DBNull.Value)
                        { sum += Convert.ToDecimal(sr["Diemtk"]); count++; }
                    }
                    if (count == 0) continue;
                    decimal dtb = Math.Round(sum / count, 2);
                    string rank = dtb >= 8 ? "Giỏi" : dtb >= 6.5m ? "Khá" : dtb >= 5 ? "TB" : "Yếu";
                    if (!first) sb.Append(',');
                    first = false;
                    sb.Append("{\"mssv\":\"").Append(EscapeJsonStr(mssv)).Append("\",");
                    sb.Append("\"tenSV\":\"").Append(EscapeJsonStr(row["LastName"] + " " + row["FirstName"])).Append("\",");
                    sb.Append("\"dtb\":").Append(dtb.ToString(System.Globalization.CultureInfo.InvariantCulture)).Append(",");
                    sb.Append("\"xeploai\":\"").Append(rank).Append("\"}");
                }
                sb.Append("]}");
                return sb.ToString();
            });
            await RunJsAsync("onStatisticsLoaded(" + json + ")");
        }

        // ============ EXPORT ============

        private async Task ExportExcel(Dictionary<string, JsonElement> data)
        {
            string exportType = GetStr(data, "type");
            if (string.IsNullOrEmpty(exportType)) return;

            DataTable dt = exportType switch
            {
                "students" => await Task.Run(() => _studentRepo.GetAll()),
                "courses" => await Task.Run(() => _courseRepo.GetAll()),
                "scores" => await Task.Run(() =>
                {
                    var dtAll = _studentRepo.GetAll();
                    var result = new DataTable();
                    result.Columns.Add("MSSV");
                    result.Columns.Add("Họ tên");
                    result.Columns.Add("ĐTB");
                    result.Columns.Add("Xếp loại");
                    foreach (DataRow row in dtAll.Rows)
                    {
                        string mssv = row["MSSV"]?.ToString() ?? "";
                        var dtScore = _scoreRepo.GetByStudent(mssv);
                        decimal sum = 0, count = 0;
                        foreach (DataRow sr in dtScore.Rows)
                        {
                            if (sr["Diemtk"] != DBNull.Value)
                            { sum += Convert.ToDecimal(sr["Diemtk"]); count++; }
                        }
                        if (count == 0) continue;
                        decimal dtb = Math.Round(sum / count, 2);
                        string rank = dtb >= 8 ? "Giỏi" : dtb >= 6.5m ? "Khá" : dtb >= 5 ? "TB" : "Yếu";
                        result.Rows.Add(mssv, row["LastName"] + " " + row["FirstName"], dtb, rank);
                    }
                    return result;
                }),
                _ => new DataTable()
            };

            if (dt.Rows.Count == 0)
            {
                await RunJsAsync("onExportResult(false, 'Không có dữ liệu để xuất')");
                return;
            }

            string filePath = await Task.Run(() =>
            {
                using var wb = new XLWorkbook();
                wb.Worksheets.Add(dt, exportType);
                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Exports");
                Directory.CreateDirectory(dir);
                string path = Path.Combine(dir, $"{exportType}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
                wb.SaveAs(path);
                return path;
            });

            await RunJsAsync("onExportResult(true, '" + EscapeJs("Đã xuất: " + filePath) + "')");
        }

        // ============ HELPERS ============

        private string DataTableToJson(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return "[]";
            var sb = new StringBuilder();
            sb.Append('[');
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0) sb.Append(',');
                sb.Append('{');
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j > 0) sb.Append(',');
                    sb.Append('"');
                    sb.Append(EscapeJsonStr(dt.Columns[j].ColumnName));
                    sb.Append("\":");
                    object val = dt.Rows[i][j];
                    if (val == DBNull.Value || val == null)
                        sb.Append("null");
                    else if (val is byte[] bytes)
                        sb.Append('"').Append(Convert.ToBase64String(bytes)).Append('"');
                    else if (val is DateTime dtVal)
                        sb.Append('"').Append(dtVal.ToString("yyyy-MM-ddTHH:mm:ss")).Append('"');
                    else if (val is bool b)
                        sb.Append(b ? "true" : "false");
                    else if (val is int || val is long || val is short || val is byte || val is float || val is double || val is decimal)
                        sb.Append(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}", val));
                    else
                        sb.Append('"').Append(EscapeJsonStr(val.ToString() ?? "")).Append('"');
                }
                sb.Append('}');
            }
            sb.Append(']');
            return sb.ToString();
        }

        private static string EscapeJsonStr(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            var sb = new StringBuilder(s.Length);
            foreach (char c in s)
            {
                switch (c)
                {
                    case '"': sb.Append("\\\""); break;
                    case '\\': sb.Append("\\\\"); break;
                    case '\n': sb.Append("\\n"); break;
                    case '\r': sb.Append("\\r"); break;
                    case '\t': sb.Append("\\t"); break;
                    default: sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        private static string EscapeJs(string s)
        {
            return s.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "");
        }

        private static string GetStr(Dictionary<string, JsonElement> data, string key)
            => data.TryGetValue(key, out var v) ? v.GetString() ?? "" : "";

        private static int GetInt(Dictionary<string, JsonElement> data, string key)
            => data.TryGetValue(key, out var v) && v.ValueKind == JsonValueKind.Number ? v.GetInt32() : 0;

        private static decimal GetDecimal(Dictionary<string, JsonElement> data, string key)
            => data.TryGetValue(key, out var v) && v.ValueKind == JsonValueKind.Number ? v.GetDecimal() : 0m;

        private static byte[]? GetPicture(Dictionary<string, JsonElement> data, string key)
        {
            if (!data.TryGetValue(key, out var v)) return null;
            string base64 = v.GetString() ?? "";
            if (string.IsNullOrEmpty(base64)) return null;
            int commaIdx = base64.IndexOf(',');
            if (commaIdx >= 0) base64 = base64.Substring(commaIdx + 1);
            try { return Convert.FromBase64String(base64); }
            catch { return null; }
        }

        private async void RunJs(string script)
        {
            try { await webView.CoreWebView2.ExecuteScriptAsync(script); }
            catch { }
        }

        private async Task RunJsAsync(string script)
        {
            try { await webView.CoreWebView2.ExecuteScriptAsync(script); }
            catch { }
        }
    }
}
