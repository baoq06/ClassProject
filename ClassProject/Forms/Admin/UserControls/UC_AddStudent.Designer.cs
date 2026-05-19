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

        #region Component Designer generated code

        /// <summary>
        /// Designer-friendly InitializeComponent.
        /// QUY TẮC: không local function, không local var (trừ các tham số
        /// kiểu literal trong constructor call), không loop / helper method.
        /// Mỗi control set property tường minh thì WinForms Designer mới
        /// parse được và mở được trong VS.
        /// </summary>
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
            panelForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picStudent).BeginInit();
            SuspendLayout();

            //
            // panelForm
            //
            panelForm.AutoScroll = true;
            panelForm.BackColor = Color.White;
            panelForm.Dock = DockStyle.Fill;
            panelForm.Location = new Point(0, 0);
            panelForm.Name = "panelForm";
            panelForm.Size = new Size(1000, 600);
            panelForm.TabIndex = 0;

            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(280, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(240, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÊM SINH VIÊN MỚI";

            //
            // picStudent
            //
            picStudent.BorderStyle = BorderStyle.FixedSingle;
            picStudent.Location = new Point(40, 60);
            picStudent.Name = "picStudent";
            picStudent.Size = new Size(160, 180);
            picStudent.SizeMode = PictureBoxSizeMode.StretchImage;
            picStudent.TabIndex = 1;
            picStudent.TabStop = false;

            //
            // btnChooseImage
            //
            btnChooseImage.Location = new Point(40, 250);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(160, 32);
            btnChooseImage.TabIndex = 2;
            btnChooseImage.Text = "Chọn ảnh";
            btnChooseImage.UseVisualStyleBackColor = true;
            btnChooseImage.Click += btnChooseImage_Click;

            //
            // lblMSSV
            //
            lblMSSV.AutoSize = true;
            lblMSSV.Font = new Font("Segoe UI", 10F);
            lblMSSV.Location = new Point(280, 64);
            lblMSSV.Name = "lblMSSV";
            lblMSSV.Size = new Size(60, 19);
            lblMSSV.TabIndex = 3;
            lblMSSV.Text = "MSSV *";

            //
            // txtMSSV
            //
            txtMSSV.Font = new Font("Segoe UI", 10F);
            txtMSSV.Location = new Point(420, 60);
            txtMSSV.Name = "txtMSSV";
            txtMSSV.Size = new Size(280, 28);
            txtMSSV.TabIndex = 4;

            //
            // lblLastName
            //
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Segoe UI", 10F);
            lblLastName.Location = new Point(280, 108);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(120, 19);
            lblLastName.TabIndex = 5;
            lblLastName.Text = "Họ và tên đệm *";

            //
            // txtLastName
            //
            txtLastName.Font = new Font("Segoe UI", 10F);
            txtLastName.Location = new Point(420, 104);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(280, 28);
            txtLastName.TabIndex = 6;

            //
            // lblFirstName
            //
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Segoe UI", 10F);
            lblFirstName.Location = new Point(280, 152);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(40, 19);
            lblFirstName.TabIndex = 7;
            lblFirstName.Text = "Tên";

            //
            // txtFirstName
            //
            txtFirstName.Font = new Font("Segoe UI", 10F);
            txtFirstName.Location = new Point(420, 148);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(280, 28);
            txtFirstName.TabIndex = 8;

            //
            // lblDateOfBirth
            //
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.Font = new Font("Segoe UI", 10F);
            lblDateOfBirth.Location = new Point(280, 196);
            lblDateOfBirth.Name = "lblDateOfBirth";
            lblDateOfBirth.Size = new Size(80, 19);
            lblDateOfBirth.TabIndex = 9;
            lblDateOfBirth.Text = "Ngày sinh";

            //
            // dtpDateOfBirth
            //
            dtpDateOfBirth.Font = new Font("Segoe UI", 10F);
            dtpDateOfBirth.Format = DateTimePickerFormat.Short;
            dtpDateOfBirth.Location = new Point(420, 192);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(280, 28);
            dtpDateOfBirth.TabIndex = 10;

            //
            // lblGender
            //
            lblGender.AutoSize = true;
            lblGender.Font = new Font("Segoe UI", 10F);
            lblGender.Location = new Point(280, 240);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(80, 19);
            lblGender.TabIndex = 11;
            lblGender.Text = "Giới tính *";

            //
            // cboGender
            //
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.Font = new Font("Segoe UI", 10F);
            cboGender.FormattingEnabled = true;
            cboGender.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGender.Location = new Point(420, 236);
            cboGender.Name = "cboGender";
            cboGender.Size = new Size(280, 27);
            cboGender.TabIndex = 12;

            //
            // lblPhone
            //
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 10F);
            lblPhone.Location = new Point(280, 284);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(40, 19);
            lblPhone.TabIndex = 13;
            lblPhone.Text = "SĐT";

            //
            // txtPhone
            //
            txtPhone.Font = new Font("Segoe UI", 10F);
            txtPhone.Location = new Point(420, 280);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(280, 28);
            txtPhone.TabIndex = 14;

            //
            // lblAddress
            //
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10F);
            lblAddress.Location = new Point(280, 328);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(60, 19);
            lblAddress.TabIndex = 15;
            lblAddress.Text = "Địa chỉ";

            //
            // txtAddress
            //
            txtAddress.Font = new Font("Segoe UI", 10F);
            txtAddress.Location = new Point(420, 324);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(280, 28);
            txtAddress.TabIndex = 16;

            //
            // lblHometown
            //
            lblHometown.AutoSize = true;
            lblHometown.Font = new Font("Segoe UI", 10F);
            lblHometown.Location = new Point(280, 372);
            lblHometown.Name = "lblHometown";
            lblHometown.Size = new Size(70, 19);
            lblHometown.TabIndex = 17;
            lblHometown.Text = "Quê quán";

            //
            // txtHometown
            //
            txtHometown.Font = new Font("Segoe UI", 10F);
            txtHometown.Location = new Point(420, 368);
            txtHometown.Name = "txtHometown";
            txtHometown.Size = new Size(280, 28);
            txtHometown.TabIndex = 18;

            //
            // lblEmail
            //
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.Location = new Point(280, 416);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 19);
            lblEmail.TabIndex = 19;
            lblEmail.Text = "Email";

            //
            // txtEmail
            //
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(420, 412);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(280, 28);
            txtEmail.TabIndex = 20;

            //
            // btnSave
            //
            btnSave.BackColor = Color.ForestGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(420, 464);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(130, 40);
            btnSave.TabIndex = 21;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            //
            // btnClear
            //
            btnClear.BackColor = Color.Gray;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(570, 464);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(130, 40);
            btnClear.TabIndex = 22;
            btnClear.Text = "Xóa form";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;

            //
            // Thêm các controls vào panelForm (panel này Dock.Fill nên scroll được)
            //
            panelForm.Controls.Add(lblTitle);
            panelForm.Controls.Add(picStudent);
            panelForm.Controls.Add(btnChooseImage);
            panelForm.Controls.Add(lblMSSV);
            panelForm.Controls.Add(txtMSSV);
            panelForm.Controls.Add(lblLastName);
            panelForm.Controls.Add(txtLastName);
            panelForm.Controls.Add(lblFirstName);
            panelForm.Controls.Add(txtFirstName);
            panelForm.Controls.Add(lblDateOfBirth);
            panelForm.Controls.Add(dtpDateOfBirth);
            panelForm.Controls.Add(lblGender);
            panelForm.Controls.Add(cboGender);
            panelForm.Controls.Add(lblPhone);
            panelForm.Controls.Add(txtPhone);
            panelForm.Controls.Add(lblAddress);
            panelForm.Controls.Add(txtAddress);
            panelForm.Controls.Add(lblHometown);
            panelForm.Controls.Add(txtHometown);
            panelForm.Controls.Add(lblEmail);
            panelForm.Controls.Add(txtEmail);
            panelForm.Controls.Add(btnSave);
            panelForm.Controls.Add(btnClear);

            //
            // UC_AddStudent
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelForm);
            Name = "UC_AddStudent";
            Size = new Size(1000, 600);
            panelForm.ResumeLayout(false);
            panelForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picStudent).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelForm;
        private Label lblTitle;
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
        private Button btnClear;
    }
}