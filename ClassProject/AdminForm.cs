using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClassProject
{
    public partial class AdminForm : Form
    {
        private Button? _activeButton;

        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            ShowUserControl(new UC_Approve());
            SetActiveButton(btnApprove);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UC_Approve());
            SetActiveButton(btnApprove);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UC_ViewStudents());
            SetActiveButton(btnView);
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UC_AddStudent());
            SetActiveButton(btnAddStudent);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            Close();
        }

        private void ShowUserControl(UserControl uc)
        {
            panelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }

        private void SetActiveButton(Button btn)
        {
            if (_activeButton != null)
                _activeButton.BackColor = Color.FromArgb(44, 62, 80);

            _activeButton = btn;
            _activeButton.BackColor = Color.FromArgb(41, 128, 185);
        }
    }
}
