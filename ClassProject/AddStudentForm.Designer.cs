namespace ClassProject
{
    partial class AddStudentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pic_Student = new PictureBox();
            btnChooseImage = new Button();
            lbMSSV = new Label();
            lbFirstName = new Label();
            lbLastName = new Label();
            lbDateOfBirth = new Label();
            lbPhone = new Label();
            lbAddress = new Label();
            lbGender = new Label();
            btnAdd = new Button();
            btnClear = new Button();
            txtMSSV = new TextBox();
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            txtPhone = new TextBox();
            textBox6 = new TextBox();
            lbEmail = new Label();
            dtpDateOfBirth = new DateTimePicker();
            txtEmail = new TextBox();
            cboGender = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pic_Student).BeginInit();
            SuspendLayout();
            // 
            // pic_Student
            // 
            pic_Student.Location = new Point(35, 68);
            pic_Student.Name = "pic_Student";
            pic_Student.Size = new Size(164, 155);
            pic_Student.TabIndex = 0;
            pic_Student.TabStop = false;
            // 
            // btnChooseImage
            // 
            btnChooseImage.Location = new Point(35, 243);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(164, 30);
            btnChooseImage.TabIndex = 1;
            btnChooseImage.Text = "ChooseImage";
            btnChooseImage.UseVisualStyleBackColor = true;
            // 
            // lbMSSV
            // 
            lbMSSV.AutoSize = true;
            lbMSSV.Location = new Point(419, 69);
            lbMSSV.Name = "lbMSSV";
            lbMSSV.Size = new Size(37, 15);
            lbMSSV.TabIndex = 2;
            lbMSSV.Text = "MSSV";
            // 
            // lbFirstName
            // 
            lbFirstName.AutoSize = true;
            lbFirstName.Location = new Point(419, 101);
            lbFirstName.Name = "lbFirstName";
            lbFirstName.Size = new Size(85, 15);
            lbFirstName.TabIndex = 3;
            lbFirstName.Text = "Họ và tên đệm";
            // 
            // lbLastName
            // 
            lbLastName.AutoSize = true;
            lbLastName.Location = new Point(419, 136);
            lbLastName.Name = "lbLastName";
            lbLastName.Size = new Size(26, 15);
            lbLastName.TabIndex = 4;
            lbLastName.Text = "Tên";
            // 
            // lbDateOfBirth
            // 
            lbDateOfBirth.AutoSize = true;
            lbDateOfBirth.Location = new Point(419, 173);
            lbDateOfBirth.Name = "lbDateOfBirth";
            lbDateOfBirth.Size = new Size(60, 15);
            lbDateOfBirth.TabIndex = 5;
            lbDateOfBirth.Text = "Ngày sinh";
            // 
            // lbPhone
            // 
            lbPhone.AutoSize = true;
            lbPhone.Location = new Point(419, 214);
            lbPhone.Name = "lbPhone";
            lbPhone.Size = new Size(76, 15);
            lbPhone.TabIndex = 6;
            lbPhone.Text = "Số điện thoại";
            // 
            // lbAddress
            // 
            lbAddress.AutoSize = true;
            lbAddress.Location = new Point(420, 251);
            lbAddress.Name = "lbAddress";
            lbAddress.Size = new Size(46, 15);
            lbAddress.TabIndex = 7;
            lbAddress.Text = "Địa chỉ ";
            // 
            // lbGender
            // 
            lbGender.AutoSize = true;
            lbGender.Location = new Point(420, 287);
            lbGender.Name = "lbGender";
            lbGender.Size = new Size(52, 15);
            lbGender.TabIndex = 8;
            lbGender.Text = "Giới tính";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(245, 367);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(115, 40);
            btnAdd.TabIndex = 9;
            btnAdd.Text = "ADD";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(419, 367);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(115, 40);
            btnClear.TabIndex = 10;
            btnClear.Text = "CLEAR";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // txtMSSV
            // 
            txtMSSV.Location = new Point(553, 66);
            txtMSSV.Name = "txtMSSV";
            txtMSSV.Size = new Size(200, 23);
            txtMSSV.TabIndex = 11;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(553, 101);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(200, 23);
            txtFirstName.TabIndex = 12;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(553, 136);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(200, 23);
            txtLastName.TabIndex = 13;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(553, 206);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(200, 23);
            txtPhone.TabIndex = 14;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(553, 244);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(200, 23);
            textBox6.TabIndex = 16;
            // 
            // lbEmail
            // 
            lbEmail.AutoSize = true;
            lbEmail.Location = new Point(420, 322);
            lbEmail.Name = "lbEmail";
            lbEmail.Size = new Size(36, 15);
            lbEmail.TabIndex = 17;
            lbEmail.Text = "Email";
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.Location = new Point(553, 173);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(200, 23);
            dtpDateOfBirth.TabIndex = 18;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(553, 319);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 23);
            txtEmail.TabIndex = 19;
            // 
            // cboGender
            // 
            cboGender.FormattingEnabled = true;
            cboGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGender.Location = new Point(553, 284);
            cboGender.Name = "cboGender";
            cboGender.Size = new Size(198, 23);
            cboGender.TabIndex = 20;
            cboGender.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // AddStudentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cboGender);
            Controls.Add(txtEmail);
            Controls.Add(dtpDateOfBirth);
            Controls.Add(lbEmail);
            Controls.Add(textBox6);
            Controls.Add(txtPhone);
            Controls.Add(txtLastName);
            Controls.Add(txtFirstName);
            Controls.Add(txtMSSV);
            Controls.Add(btnClear);
            Controls.Add(btnAdd);
            Controls.Add(lbGender);
            Controls.Add(lbAddress);
            Controls.Add(lbPhone);
            Controls.Add(lbDateOfBirth);
            Controls.Add(lbLastName);
            Controls.Add(lbFirstName);
            Controls.Add(lbMSSV);
            Controls.Add(btnChooseImage);
            Controls.Add(pic_Student);
            Name = "AddStudentForm";
            Text = "AddStudentForm";
            Load += AddStudentForm_Load;
            ((System.ComponentModel.ISupportInitialize)pic_Student).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pic_Student;
        private Button btnChooseImage;
        private Label lbMSSV;
        private Label lbFirstName;
        private Label lbLastName;
        private Label lbDateOfBirth;
        private Label lbPhone;
        private Label lbAddress;
        private Label lbGender;
        private Button btnAdd;
        private Button btnClear;
        private TextBox txtMSSV;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtPhone;
        private TextBox textBox5;
        private TextBox textBox6;
        private Label lbEmail;
        private DateTimePicker dtpDateOfBirth;
        private TextBox txtEmail;
        private ComboBox cboGender;
    }
}