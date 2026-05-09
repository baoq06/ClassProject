using System.Drawing;

namespace ClassProject
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblUsername;
        private Label lblEmail;
        private Label lblPassword;
        private Label lblConfirmPassword;

        private TextBox txtUsername;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;

        private Button btnRegister;
        private Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();

            lblUsername = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            lblConfirmPassword = new Label();

            txtUsername = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();

            btnRegister = new Button();
            btnBack = new Button();

            SuspendLayout();

            // =========================
            // lblTitle
            // =========================
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.Location = new Point(250, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(170, 46);
            lblTitle.Text = "Register";

            // =========================
            // lblUsername
            // =========================
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 12F);
            lblUsername.Location = new Point(80, 140);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(103, 28);
            lblUsername.Text = "Username";

            // =========================
            // txtUsername
            // =========================
            txtUsername.Location = new Point(300, 140);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(300, 27);

            // =========================
            // lblEmail
            // =========================
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 12F);
            lblEmail.Location = new Point(80, 220);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(59, 28);
            lblEmail.Text = "Email";

            // =========================
            // txtEmail
            // =========================
            txtEmail.Location = new Point(300, 220);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 27);

            // =========================
            // lblPassword
            // =========================
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 12F);
            lblPassword.Location = new Point(80, 300);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(93, 28);
            lblPassword.Text = "Password";

            // =========================
            // txtPassword
            // =========================
            txtPassword.Location = new Point(300, 300);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(300, 27);
            txtPassword.PasswordChar = '*';

            // =========================
            // lblConfirmPassword
            // =========================
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 12F);
            lblConfirmPassword.Location = new Point(80, 380);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(171, 28);
            lblConfirmPassword.Text = "Confirm Password";

            // =========================
            // txtConfirmPassword
            // =========================
            txtConfirmPassword.Location = new Point(300, 380);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(300, 27);
            txtConfirmPassword.PasswordChar = '*';

            // =========================
            // btnRegister
            // =========================
            btnRegister.Location = new Point(220, 480);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(160, 50);
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;

            // =========================
            // btnBack
            // =========================
            btnBack.Location = new Point(420, 480);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(160, 50);
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;

            // =========================
            // RegisterForm
            // =========================
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;

            ClientSize = new Size(700, 600);

            Controls.Add(lblTitle);

            Controls.Add(lblUsername);
            Controls.Add(txtUsername);

            Controls.Add(lblEmail);
            Controls.Add(txtEmail);

            Controls.Add(lblPassword);
            Controls.Add(txtPassword);

            Controls.Add(lblConfirmPassword);
            Controls.Add(txtConfirmPassword);

            Controls.Add(btnRegister);
            Controls.Add(btnBack);

            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Register";

            ResumeLayout(false);
            PerformLayout();
        }
    }
}