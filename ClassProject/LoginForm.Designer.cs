using Microsoft.Data.SqlClient;

namespace ClassProject
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            lblAccountLogin = new Label();
            lblName = new Label();
            lblPassword = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnCancel = new Button();
            btnLogin = new Button();
            lblForgetPassword = new Label();
            lblRegister = new Label();
            chkRememberMe = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(5, 5, 5, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(162, 80);
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // lblAccountLogin
            // 
            lblAccountLogin.AutoSize = true;
            lblAccountLogin.BackColor = Color.Transparent;
            lblAccountLogin.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAccountLogin.ForeColor = Color.White;
            lblAccountLogin.Location = new Point(512, 82);
            lblAccountLogin.Name = "lblAccountLogin";
            lblAccountLogin.Size = new Size(388, 71);
            lblAccountLogin.TabIndex = 1;
            lblAccountLogin.Text = "Account Login";
            lblAccountLogin.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(174, 302);
            lblName.Margin = new Padding(5, 0, 5, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(186, 50);
            lblName.TabIndex = 2;
            lblName.Text = "Username";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.Transparent;
            lblPassword.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPassword.ForeColor = SystemColors.Window;
            lblPassword.Location = new Point(174, 395);
            lblPassword.Margin = new Padding(5, 0, 5, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(177, 50);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(544, 312);
            txtUsername.Margin = new Padding(5, 5, 5, 5);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(373, 39);
            txtUsername.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(544, 406);
            txtPassword.Margin = new Padding(5, 5, 5, 5);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(373, 39);
            txtPassword.TabIndex = 12;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(14, 24);
            btnCancel.Margin = new Padding(5, 5, 5, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(122, 37);
            btnCancel.TabIndex = 11;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.Location = new Point(637, 541);
            btnLogin.Margin = new Padding(5, 5, 5, 5);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(182, 72);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblForgetPassword
            // 
            lblForgetPassword.AutoSize = true;
            lblForgetPassword.BackColor = Color.Transparent;
            lblForgetPassword.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblForgetPassword.ForeColor = Color.Peru;
            lblForgetPassword.Location = new Point(661, 483);
            lblForgetPassword.Margin = new Padding(5, 0, 5, 0);
            lblForgetPassword.Name = "lblForgetPassword";
            lblForgetPassword.Size = new Size(241, 38);
            lblForgetPassword.TabIndex = 8;
            lblForgetPassword.Text = "Forget password?";
            // 
            // lblRegister
            // 
            lblRegister.AutoSize = true;
            lblRegister.BackColor = Color.Transparent;
            lblRegister.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblRegister.ForeColor = Color.Peru;
            lblRegister.Location = new Point(332, 638);
            lblRegister.Margin = new Padding(5, 0, 5, 0);
            lblRegister.Name = "lblRegister";
            lblRegister.Size = new Size(482, 45);
            lblRegister.TabIndex = 9;
            lblRegister.Text = "Don't have an account? Register";
            // 
            // chkRememberMe
            // 
            chkRememberMe.AutoSize = true;
            chkRememberMe.BackColor = Color.Transparent;
            chkRememberMe.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkRememberMe.ForeColor = Color.Peru;
            chkRememberMe.Location = new Point(247, 477);
            chkRememberMe.Margin = new Padding(5, 5, 5, 5);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(235, 42);
            chkRememberMe.TabIndex = 10;
            chkRememberMe.Text = "Remember me";
            chkRememberMe.UseVisualStyleBackColor = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1042, 698);
            Controls.Add(chkRememberMe);
            Controls.Add(lblRegister);
            Controls.Add(lblForgetPassword);
            Controls.Add(btnLogin);
            Controls.Add(btnCancel);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(lblName);
            Controls.Add(lblAccountLogin);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label lblAccountLogin;
        private Label lblName;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnCancel;
        private Button btnLogin;
        private Label lblForgetPassword;
        private Label lblRegister;
        private CheckBox chkRememberMe;
    }
}
