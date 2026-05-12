using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClassProject.Presentation.Forms
{
    /// <summary>
    /// Form nhỏ chọn vai trò khi đăng ký: 1 = Sinh viên, 2 = Giảng viên.
    /// </summary>
    public partial class RegisterRoleForm : Form
    {
        public RegisterRoleForm()
        {
            InitializeComponent();
        }

        /// <summary>1 = Student, 2 = Lecturer</summary>
        public int SelectedRoleId { get; private set; } = 1;

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedRoleId = radLecturer.Checked ? 2 : 1;
            DialogResult = DialogResult.OK;
            if(SelectedRoleId == 1)
            {
                AddStudentForm f = new AddStudentForm(SelectedRoleId);
                f.Show();
                this.Hide();
            }
        }
    }
}