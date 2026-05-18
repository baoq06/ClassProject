namespace ClassProject
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelSidebar = new Panel();
            lblAdmin = new Label();
            btnApprove = new Button();
            btnView = new Button();
            btnAddStudent = new Button();
            btnGrades = new Button();
            btnLogout = new Button();
            panelMain = new Panel();
            panelSidebar.SuspendLayout();
            SuspendLayout();
            //
            // panelSidebar
            //
            panelSidebar.BackColor = Color.FromArgb(44, 62, 80);
            panelSidebar.Controls.Add(lblAdmin);
            panelSidebar.Controls.Add(btnLogout);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Width = 200;
            panelSidebar.Name = "panelSidebar";
            //
            // lblAdmin
            //
            lblAdmin.Dock = DockStyle.Top;
            lblAdmin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblAdmin.ForeColor = Color.White;
            lblAdmin.Height = 56;
            lblAdmin.Text = "ADMIN";
            lblAdmin.TextAlign = ContentAlignment.MiddleCenter;
            //
            // btnApprove
            //
            ConfigureSidebarButton(btnApprove, "Approve", 64);
            btnApprove.Click += btnApprove_Click;
            //
            // btnView
            //
            ConfigureSidebarButton(btnView, "View", 120);
            btnView.Click += btnView_Click;
            //
            // btnAddStudent
            //
            ConfigureSidebarButton(btnAddStudent, "Add Student", 176);
            btnAddStudent.Click += btnAddStudent_Click;
            //
            // btnGrades
            //
            ConfigureSidebarButton(btnGrades, "Chỉnh sửa điểm", 232);
            btnGrades.Click += btnGrades_Click;
            //
            // btnLogout
            //
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.BackColor = Color.FromArgb(192, 57, 43);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Height = 48;
            btnLogout.Margin = new Padding(12, 8, 12, 12);
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            //
            // panelMain
            //
            panelMain.BackColor = Color.White;
            panelMain.Dock = DockStyle.Fill;
            panelMain.Name = "panelMain";
            //
            // AdminForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1200, 700);
            Controls.Add(panelMain);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(900, 600);
            Name = "AdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản trị hệ thống";
            Load += AdminForm_Load;
            panelSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void ConfigureSidebarButton(Button btn, string text, int top)
        {
            btn.BackColor = Color.FromArgb(44, 62, 80);
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.Location = new Point(12, top);
            btn.Size = new Size(176, 48);
            btn.Text = text;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.UseVisualStyleBackColor = false;
            panelSidebar.Controls.Add(btn);
        }

        private Panel panelSidebar;
        private Panel panelMain;
        private Label lblAdmin;
        private Button btnApprove;
        private Button btnView;
        private Button btnAddStudent;
        private Button btnGrades;
        private Button btnLogout;
    }
}