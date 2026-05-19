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

        #region Windows Form Designer generated code

        /// <summary>
        /// Designer-friendly InitializeComponent (flat, không local function).
        /// </summary>
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

            //
            // picStudent
            //
            picStudent.BorderStyle = BorderStyle.FixedSingle;
            picStudent.Location = new Point(24, 24);
            picStudent.Name = "picStudent";
            picStudent.Size = new Size(140, 160);
            picStudent.SizeMode = PictureBoxSizeMode.StretchImage;
            picStudent.TabIndex = 0;
            picStudent.TabStop = false;

            //
            // btnChooseImage
            //
            btnChooseImage.Location = new Point(24, 192);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(140, 32);
            btnChooseImage.TabIndex = 1;
            btnChooseImage.Text = "Chọn ảnh";
            btnChooseImage.UseVisualStyleBackColor = true;
            btnChooseImage.Click += btnChooseImage_Click;

            //
            // lblMSSV
            //
            lblMSSV.AutoSize = true;
            lblMSSV.Location = new Point(200, 28);
            lblMSSV.Name = "lblMSSV";
            lblMSSV.Size = new Size(46, 15);
            lblMSSV.TabIndex = 2;
            lblMSSV.Text = "MSSV";

            //
            // txtMSSV
            //
            txtMSSV.Location = new Point(320, 24);
            txtMSSV.Name = "txtMSSV";
            txtMSSV.Size = new Size(360, 27);
            txtMSSV.TabIndex = 3;

            //
            // lblLastName
            //
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(200, 68);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(108, 15);
            lblLastName.TabIndex = 4;
            lblLastName.Text = "Họ và tên đệm";

            //
            // txtLastName
            //
            txtLastName.Location = new Point(320, 64);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(360, 27);
            txtLastName.TabIndex = 5;

            //
            // lblFirstName
            //
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(200, 108);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(30, 15);
            lblFirstName.TabIndex = 6;
            lblFirstName.Text = "Tên";

            //
            // txtFirstName
            //
            txtFirstName.Location = new Point(320, 104);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(360, 27);
            txtFirstName.TabIndex = 7;

            //
            // lblDateOfBirth
            //
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.Location = new Point(200, 148);
            lblDateOfBirth.Name = "lblDateOfBirth";
            lblDateOfBirth.Size = new Size(68, 15);
            lblDateOfBirth.TabIndex = 8;
            lblDateOfBirth.Text = "Ngày sinh";

            //
            // dtpDateOfBirth
            //
            dtpDateOfBirth.Format = DateTimePickerFormat.Short;
            dtpDateOfBirth.Location = new Point(320, 144);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(360, 27);
            dtpDateOfBirth.TabIndex = 9;

            //
            // lblGender
            //
            lblGender.AutoSize = true;
            lblGender.Location = new Point(200, 188);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(60, 15);
            lblGender.TabIndex = 10;
            lblGender.Text = "Giới tính";

            //
            // cboGender
            //
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.FormattingEnabled = true;
            cboGender.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGender.Location = new Point(320, 184);
            cboGender.Name = "cboGender";
            cboGender.Size = new Size(360, 27);
            cboGender.TabIndex = 11;

            //
            // lblPhone
            //
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(200, 228);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(34, 15);
            lblPhone.TabIndex = 12;
            lblPhone.Text = "SĐT";

            //
            // txtPhone
            //
            txtPhone.Location = new Point(320, 224);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(360, 27);
            txtPhone.TabIndex = 13;

            //
            // lblAddress
            //
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(200, 268);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(50, 15);
            lblAddress.TabIndex = 14;
            lblAddress.Text = "Địa chỉ";

            //
            // txtAddress
            //
            txtAddress.Location = new Point(320, 264);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(360, 27);
            txtAddress.TabIndex = 15;

            //
            // lblHometown
            //
            lblHometown.AutoSize = true;
            lblHometown.Location = new Point(200, 308);
            lblHometown.Name = "lblHometown";
            lblHometown.Size = new Size(64, 15);
            lblHometown.TabIndex = 16;
            lblHometown.Text = "Quê quán";

            //
            // txtHometown
            //
            txtHometown.Location = new Point(320, 304);
            txtHometown.Name = "txtHometown";
            txtHometown.Size = new Size(360, 27);
            txtHometown.TabIndex = 17;

            //
            // lblEmail
            //
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(200, 348);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(40, 15);
            lblEmail.TabIndex = 18;
            lblEmail.Text = "Email";

            //
            // txtEmail
            //
            txtEmail.Location = new Point(320, 344);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(360, 27);
            txtEmail.TabIndex = 19;

            //
            // btnSave
            //
            btnSave.BackColor = Color.ForestGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(320, 396);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 36);
            btnSave.TabIndex = 20;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            //
            // btnCancel
            //
            btnCancel.BackColor = Color.Gray;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(460, 396);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 36);
            btnCancel.TabIndex = 21;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;

            //
            // AdminEditStudentForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(720, 520);
            Controls.Add(picStudent);
            Controls.Add(btnChooseImage);
            Controls.Add(lblMSSV);
            Controls.Add(txtMSSV);
            Controls.Add(lblLastName);
            Controls.Add(txtLastName);
            Controls.Add(lblFirstName);
            Controls.Add(txtFirstName);
            Controls.Add(lblDateOfBirth);
            Controls.Add(dtpDateOfBirth);
            Controls.Add(lblGender);
            Controls.Add(cboGender);
            Controls.Add(lblPhone);
            Controls.Add(txtPhone);
            Controls.Add(lblAddress);
            Controls.Add(txtAddress);
            Controls.Add(lblHometown);
            Controls.Add(txtHometown);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AdminEditStudentForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sửa thông tin sinh viên";
            Load += AdminEditStudentForm_Load;
            ((System.ComponentModel.ISupportInitialize)picStudent).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picStudent;
        private Button btnChooseImage;
        private Label lblMSSV;
        private TextBox txtMSSV;
        private Label lblLastName;
        private TextBox txtLastName;
        private Label lblFirstName;
        private TextBox txtFirstName;
        private Label lblDateOfBirth;
        private DateTimePicker dtpDateOfBirth;
        private Label lblGender;
        private ComboBox cboGender;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblHometown;
        private TextBox txtHometown;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnSave;
        private Button btnCancel;
    }
}