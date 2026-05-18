namespace ClassProject
{
    partial class AdminEditStudentForm
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
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)picStudent).BeginInit();
            SuspendLayout();

            Text = "Sửa thông tin sinh viên";
            ClientSize = new Size(720, 520);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            picStudent.BorderStyle = BorderStyle.FixedSingle;
            picStudent.Location = new Point(24, 24);
            picStudent.Size = new Size(140, 160);
            picStudent.SizeMode = PictureBoxSizeMode.StretchImage;

            btnChooseImage.Location = new Point(24, 192);
            btnChooseImage.Size = new Size(140, 32);
            btnChooseImage.Text = "Chọn ảnh";
            btnChooseImage.Click += btnChooseImage_Click;

            int x1 = 200, x2 = 320, y = 28, gap = 40;

            void Row(Label lbl, Control c, string t)
            {
                lbl.Text = t; lbl.Location = new Point(x1, y); lbl.AutoSize = true;
                c.Location = new Point(x2, y - 4); c.Size = new Size(360, 27);
                Controls.Add(lbl); Controls.Add(c);
                y += gap;
            }

            Row(lblMSSV, txtMSSV, "MSSV");
            Row(lblLastName, txtLastName, "Họ và tên đệm");
            Row(lblFirstName, txtFirstName, "Tên");
            Row(lblDateOfBirth, dtpDateOfBirth, "Ngày sinh");
            lblGender.Text = "Giới tính"; lblGender.Location = new Point(x1, y); lblGender.AutoSize = true;
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGender.Location = new Point(x2, y - 4); cboGender.Size = new Size(360, 27);
            Controls.Add(lblGender); Controls.Add(cboGender);
            y += gap;
            Row(lblPhone, txtPhone, "SĐT");
            Row(lblAddress, txtAddress, "Địa chỉ");
            Row(lblHometown, txtHometown, "Quê quán");
            Row(lblEmail, txtEmail, "Email");

            btnSave.BackColor = Color.ForestGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(x2, y + 8);
            btnSave.Size = new Size(120, 36);
            btnSave.Text = "Lưu";
            btnSave.Click += btnSave_Click;

            btnCancel.BackColor = Color.Gray;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(x2 + 140, y + 8);
            btnCancel.Size = new Size(120, 36);
            btnCancel.Text = "Hủy";
            btnCancel.Click += btnCancel_Click;

            Controls.Add(picStudent);
            Controls.Add(btnChooseImage);
            Load += AdminEditStudentForm_Load;

            ((System.ComponentModel.ISupportInitialize)picStudent).EndInit();
            ResumeLayout(false);
        }

        private PictureBox picStudent;
        private Button btnChooseImage;
        private Label lblMSSV, lblLastName, lblFirstName, lblDateOfBirth, lblGender, lblPhone, lblAddress, lblHometown, lblEmail;
        private TextBox txtMSSV, txtLastName, txtFirstName, txtPhone, txtAddress, txtHometown, txtEmail;
        private DateTimePicker dtpDateOfBirth;
        private ComboBox cboGender;
        private Button btnSave, btnCancel;
    }
}
