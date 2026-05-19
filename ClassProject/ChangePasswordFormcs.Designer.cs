namespace ClassProject
{
    partial class ChangePasswordForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblHello = new Label();
            lblNew = new Label();
            txtNewPassword = new TextBox();
            lblConfirm = new Label();
            txtConfirmPassword = new TextBox();
            chkShowPassword = new CheckBox();
            lblRulesTitle = new Label();
            lblRuleLength = new Label();
            lblRuleUppercase = new Label();
            lblRuleDigit = new Label();
            lblRuleSpecial = new Label();
            lblMatch = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();

            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(24, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(220, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ĐỔI MẬT KHẨU";

            //
            // lblHello
            //
            lblHello.AutoSize = true;
            lblHello.Font = new Font("Segoe UI", 9.5F);
            lblHello.ForeColor = Color.FromArgb(80, 80, 80);
            lblHello.Location = new Point(24, 58);
            lblHello.Name = "lblHello";
            lblHello.Size = new Size(360, 18);
            lblHello.TabIndex = 1;
            lblHello.Text = "Bạn cần đổi mật khẩu trước khi sử dụng hệ thống.";

            //
            // lblNew
            //
            lblNew.AutoSize = true;
            lblNew.Font = new Font("Segoe UI", 10F);
            lblNew.Location = new Point(24, 100);
            lblNew.Name = "lblNew";
            lblNew.Size = new Size(105, 19);
            lblNew.TabIndex = 2;
            lblNew.Text = "Mật khẩu mới";

            //
            // txtNewPassword
            //
            txtNewPassword.Font = new Font("Segoe UI", 10F);
            txtNewPassword.Location = new Point(180, 96);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(280, 27);
            txtNewPassword.TabIndex = 3;
            txtNewPassword.UseSystemPasswordChar = true;
            txtNewPassword.TextChanged += txtNewPassword_TextChanged;

            //
            // lblConfirm
            //
            lblConfirm.AutoSize = true;
            lblConfirm.Font = new Font("Segoe UI", 10F);
            lblConfirm.Location = new Point(24, 140);
            lblConfirm.Name = "lblConfirm";
            lblConfirm.Size = new Size(120, 19);
            lblConfirm.TabIndex = 4;
            lblConfirm.Text = "Xác nhận mật khẩu";

            //
            // txtConfirmPassword
            //
            txtConfirmPassword.Font = new Font("Segoe UI", 10F);
            txtConfirmPassword.Location = new Point(180, 136);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(280, 27);
            txtConfirmPassword.TabIndex = 5;
            txtConfirmPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.TextChanged += txtConfirmPassword_TextChanged;

            //
            // chkShowPassword
            //
            chkShowPassword.AutoSize = true;
            chkShowPassword.Font = new Font("Segoe UI", 9F);
            chkShowPassword.Location = new Point(180, 172);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(135, 19);
            chkShowPassword.TabIndex = 6;
            chkShowPassword.Text = "Hiện mật khẩu";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;

            //
            // lblMatch
            //
            lblMatch.AutoSize = true;
            lblMatch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMatch.ForeColor = Color.Gray;
            lblMatch.Location = new Point(330, 172);
            lblMatch.Name = "lblMatch";
            lblMatch.Size = new Size(140, 18);
            lblMatch.TabIndex = 7;
            lblMatch.Text = "(chưa nhập xác nhận)";

            //
            // lblRulesTitle
            //
            lblRulesTitle.AutoSize = true;
            lblRulesTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRulesTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblRulesTitle.Location = new Point(24, 210);
            lblRulesTitle.Name = "lblRulesTitle";
            lblRulesTitle.Size = new Size(180, 19);
            lblRulesTitle.TabIndex = 8;
            lblRulesTitle.Text = "Yêu cầu mật khẩu mới:";

            //
            // lblRuleLength
            //
            lblRuleLength.AutoSize = true;
            lblRuleLength.Font = new Font("Segoe UI", 9.5F);
            lblRuleLength.ForeColor = Color.Firebrick;
            lblRuleLength.Location = new Point(40, 238);
            lblRuleLength.Name = "lblRuleLength";
            lblRuleLength.Size = new Size(160, 18);
            lblRuleLength.TabIndex = 9;
            lblRuleLength.Text = "✗  Tối thiểu 8 ký tự";

            //
            // lblRuleUppercase
            //
            lblRuleUppercase.AutoSize = true;
            lblRuleUppercase.Font = new Font("Segoe UI", 9.5F);
            lblRuleUppercase.ForeColor = Color.Firebrick;
            lblRuleUppercase.Location = new Point(40, 262);
            lblRuleUppercase.Name = "lblRuleUppercase";
            lblRuleUppercase.Size = new Size(200, 18);
            lblRuleUppercase.TabIndex = 10;
            lblRuleUppercase.Text = "✗  Có ít nhất 1 chữ HOA (A-Z)";

            //
            // lblRuleDigit
            //
            lblRuleDigit.AutoSize = true;
            lblRuleDigit.Font = new Font("Segoe UI", 9.5F);
            lblRuleDigit.ForeColor = Color.Firebrick;
            lblRuleDigit.Location = new Point(40, 286);
            lblRuleDigit.Name = "lblRuleDigit";
            lblRuleDigit.Size = new Size(190, 18);
            lblRuleDigit.TabIndex = 11;
            lblRuleDigit.Text = "✗  Có ít nhất 1 chữ số (0-9)";

            //
            // lblRuleSpecial
            //
            lblRuleSpecial.AutoSize = true;
            lblRuleSpecial.Font = new Font("Segoe UI", 9.5F);
            lblRuleSpecial.ForeColor = Color.Firebrick;
            lblRuleSpecial.Location = new Point(40, 310);
            lblRuleSpecial.Name = "lblRuleSpecial";
            lblRuleSpecial.Size = new Size(220, 18);
            lblRuleSpecial.TabIndex = 12;
            lblRuleSpecial.Text = "✗  Có ít nhất 1 ký tự đặc biệt";

            //
            // btnSave
            //
            btnSave.BackColor = Color.FromArgb(180, 180, 180);
            btnSave.Enabled = false;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(180, 360);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 42);
            btnSave.TabIndex = 13;
            btnSave.Text = "Đổi mật khẩu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            //
            // btnCancel
            //
            btnCancel.BackColor = Color.Gray;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(340, 360);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 42);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;

            //
            // ChangePasswordForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(500, 430);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lblRuleSpecial);
            Controls.Add(lblRuleDigit);
            Controls.Add(lblRuleUppercase);
            Controls.Add(lblRuleLength);
            Controls.Add(lblRulesTitle);
            Controls.Add(lblMatch);
            Controls.Add(chkShowPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirm);
            Controls.Add(txtNewPassword);
            Controls.Add(lblNew);
            Controls.Add(lblHello);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangePasswordForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đổi mật khẩu lần đầu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblHello;
        private Label lblNew;
        private TextBox txtNewPassword;
        private Label lblConfirm;
        private TextBox txtConfirmPassword;
        private CheckBox chkShowPassword;
        private Label lblRulesTitle;
        private Label lblRuleLength;
        private Label lblRuleUppercase;
        private Label lblRuleDigit;
        private Label lblRuleSpecial;
        private Label lblMatch;
        private Button btnSave;
        private Button btnCancel;
    }
}