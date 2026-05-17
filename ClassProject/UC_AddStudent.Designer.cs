namespace ClassProject
{
    partial class UC_AddStudent
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
            panelForm = new Panel();
            lblTitle = new Label();
            picStudent = new PictureBox();
            btnChooseImage = new Button();
            lblMSSV = new Label();
            txtMSSV = new TextBox();
            lblLastName = new Label();
            txtLastName = new TextBox();
            lblFirstName = new Label();
            txtFirstName = new TextBox();
            lblDateOfBirth = new Label();
            dtpDateOfBirth = new DateTimePicker();
            lblGender = new Label();
            cboGender = new ComboBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblHometown = new Label();
            txtHometown = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            btnSave = new Button();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)picStudent).BeginInit();
            panelForm.SuspendLayout();
            SuspendLayout();

            Font lblFont = new Font("Segoe UI", 10F);
            Font txtFont = new Font("Segoe UI", 10F);
            int x1 = 280, x2 = 420, y = 60, w = 280, h = 28, gap = 44;

            panelForm.AutoScroll = true;
            panelForm.Dock = DockStyle.Fill;
            panelForm.BackColor = Color.White;

            lblTitle.Text = "THÊM SINH VIÊN MỚI";
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(280, 16);
            lblTitle.AutoSize = true;

            picStudent.BorderStyle = BorderStyle.FixedSingle;
            picStudent.Location = new Point(40, 60);
            picStudent.Size = new Size(160, 180);
            picStudent.SizeMode = PictureBoxSizeMode.StretchImage;

            btnChooseImage.Location = new Point(40, 250);
            btnChooseImage.Size = new Size(160, 32);
            btnChooseImage.Text = "Chọn ảnh";
            btnChooseImage.Click += btnChooseImage_Click;

            void AddRow(Label lbl, Control ctrl, string text)
            {
                lbl.Text = text;
                lbl.Font = lblFont;
                lbl.Location = new Point(x1, y + 4);
                lbl.AutoSize = true;
                ctrl.Font = txtFont;
                ctrl.Location = new Point(x2, y);
                ctrl.Size = new Size(w, h);
                panelForm.Controls.Add(lbl);
                panelForm.Controls.Add(ctrl);
                y += gap;
            }

            AddRow(lblMSSV, txtMSSV, "MSSV *");
            AddRow(lblLastName, txtLastName, "Họ và tên đệm *");
            AddRow(lblFirstName, txtFirstName, "Tên");
            AddRow(lblDateOfBirth, dtpDateOfBirth, "Ngày sinh");
            lblGender.Text = "Giới tính *";
            lblGender.Font = lblFont;
            lblGender.Location = new Point(x1, y + 4);
            lblGender.AutoSize = true;
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGender.Location = new Point(x2, y);
            cboGender.Size = new Size(w, h);
            panelForm.Controls.Add(lblGender);
            panelForm.Controls.Add(cboGender);
            y += gap;

            AddRow(lblPhone, txtPhone, "SĐT");
            AddRow(lblAddress, txtAddress, "Địa chỉ");
            AddRow(lblHometown, txtHometown, "Quê quán");
            AddRow(lblEmail, txtEmail, "Email");

            btnSave.BackColor = Color.ForestGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.Location = new Point(x2, y + 8);
            btnSave.Size = new Size(130, 40);
            btnSave.Text = "Lưu";
            btnSave.Click += btnSave_Click;

            btnClear.BackColor = Color.Gray;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.ForeColor = Color.White;
            btnClear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClear.Location = new Point(x2 + 150, y + 8);
            btnClear.Size = new Size(130, 40);
            btnClear.Text = "Xóa form";
            btnClear.Click += btnClear_Click;

            panelForm.Controls.Add(lblTitle);
            panelForm.Controls.Add(picStudent);
            panelForm.Controls.Add(btnChooseImage);
            panelForm.Controls.Add(btnSave);
            panelForm.Controls.Add(btnClear);

            dtpDateOfBirth.Value = DateTime.Now.AddYears(-20);

            Controls.Add(panelForm);
            Name = "UC_AddStudent";
            Size = new Size(1000, 600);

            ((System.ComponentModel.ISupportInitialize)picStudent).EndInit();
            panelForm.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel panelForm;
        private Label lblTitle;
        private PictureBox picStudent;
        private Button btnChooseImage;
        private Label lblMSSV, lblLastName, lblFirstName, lblDateOfBirth, lblGender, lblPhone, lblAddress, lblHometown, lblEmail;
        private TextBox txtMSSV, txtLastName, txtFirstName, txtPhone, txtAddress, txtHometown, txtEmail;
        private DateTimePicker dtpDateOfBirth;
        private ComboBox cboGender;
        private Button btnSave, btnClear;
    }
}
