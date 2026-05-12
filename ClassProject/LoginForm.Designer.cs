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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            lblAccountLogin = new Label();
            lblName = new Label();
            lblPassword = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            lblForgetPassword = new Label();
            lblRegister = new Label();
            chkRememberMe = new CheckBox();
            picEye = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picEye).BeginInit();
            SuspendLayout();
            // 
            // lblAccountLogin
            // 
            lblAccountLogin.AutoSize = true;
            lblAccountLogin.BackColor = Color.Transparent;
            lblAccountLogin.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            lblAccountLogin.ForeColor = Color.White;
            lblAccountLogin.Location = new Point(367, 136);
            lblAccountLogin.Margin = new Padding(4, 0, 4, 0);
            lblAccountLogin.Name = "lblAccountLogin";
            lblAccountLogin.Size = new Size(492, 89);
            lblAccountLogin.TabIndex = 1;
            lblAccountLogin.Text = "Account Login";
            lblAccountLogin.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Segoe UI", 13F);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(120, 295);
            lblName.Margin = new Padding(6, 0, 6, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(175, 47);
            lblName.TabIndex = 2;
            lblName.Text = "Username";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.Transparent;
            lblPassword.Font = new Font("Segoe UI", 13F);
            lblPassword.ForeColor = SystemColors.Window;
            lblPassword.Location = new Point(120, 426);
            lblPassword.Margin = new Padding(6, 0, 6, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(166, 47);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 13F);
            txtUsername.Location = new Point(324, 295);
            txtUsername.Margin = new Padding(6, 4, 6, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(556, 54);
            txtUsername.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 13F);
            txtPassword.Location = new Point(324, 426);
            txtPassword.Margin = new Padding(6, 4, 6, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(556, 54);
            txtPassword.TabIndex = 12;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.Location = new Point(505, 599);
            btnLogin.Margin = new Padding(6, 4, 6, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(182, 73);
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
            lblForgetPassword.ForeColor = Color.White;
            lblForgetPassword.Location = new Point(685, 516);
            lblForgetPassword.Margin = new Padding(6, 0, 6, 0);
            lblForgetPassword.Name = "lblForgetPassword";
            lblForgetPassword.Size = new Size(241, 38);
            lblForgetPassword.TabIndex = 8;
            lblForgetPassword.Text = "Forget password?";
            lblForgetPassword.Click += lblForgetPassword_Click;
            // 
            // lblRegister
            // 
            lblRegister.AutoSize = true;
            lblRegister.BackColor = Color.Transparent;
            lblRegister.Cursor = Cursors.Hand;
            lblRegister.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblRegister.ForeColor = Color.LightBlue;
            lblRegister.Location = new Point(340, 708);
            lblRegister.Margin = new Padding(6, 0, 6, 0);
            lblRegister.Name = "lblRegister";
            lblRegister.Size = new Size(484, 45);
            lblRegister.TabIndex = 9;
            lblRegister.Text = "Don't have an account? Sign up.";
            lblRegister.Click += lblRegister_Click;
            // 
            // chkRememberMe
            // 
            chkRememberMe.AutoSize = true;
            chkRememberMe.BackColor = Color.Transparent;
            chkRememberMe.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkRememberMe.ForeColor = Color.White;
            chkRememberMe.Location = new Point(229, 516);
            chkRememberMe.Margin = new Padding(6, 4, 6, 4);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(235, 42);
            chkRememberMe.TabIndex = 10;
            chkRememberMe.Text = "Remember me";
            chkRememberMe.UseVisualStyleBackColor = false;
            // 
            // picEye
            // 
            picEye.Cursor = Cursors.Hand;
            picEye.Image = Image.FromFile(@"C:\ClassProject\ClassProject\images\hide.png");
            picEye.Location = new Point(831, 433);
            picEye.Name = "picEye";
            picEye.Size = new Size(40, 40);
            picEye.SizeMode = PictureBoxSizeMode.Zoom;
            picEye.TabIndex = 0;
            picEye.TabStop = false;
            picEye.Click += picEye_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1206, 797);
            Controls.Add(picEye);
            Controls.Add(chkRememberMe);
            Controls.Add(lblRegister);
            Controls.Add(lblForgetPassword);
            Controls.Add(btnLogin); 
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(lblName);
            Controls.Add(lblAccountLogin);
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)picEye).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblAccountLogin;
        private Label lblName;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblForgetPassword;
        private Label lblRegister;
        private CheckBox chkRememberMe;
        private PictureBox picEye;
    }
}
