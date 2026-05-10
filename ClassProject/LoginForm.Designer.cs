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
            pctBoxLogo = new PictureBox();
            lblAccountLogin = new Label();
            lblName = new Label();
            lblPassword = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            lblForgetPassword = new Label();
            lblRegister = new Label();
            chkRememberMe = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pctBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // pctBoxLogo
            // 
            pctBoxLogo.BackgroundImageLayout = ImageLayout.Center;
            pctBoxLogo.BorderStyle = BorderStyle.FixedSingle;
            pctBoxLogo.Image = Properties.Resources.Login_ico;
            pctBoxLogo.Location = new Point(0, 0);
            pctBoxLogo.Name = "pctBoxLogo";
            pctBoxLogo.Size = new Size(108, 108);
            pctBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pctBoxLogo.TabIndex = 13;
            pctBoxLogo.TabStop = false;
            // 
            // lblAccountLogin
            // 
            lblAccountLogin.AutoSize = true;
            lblAccountLogin.BackColor = Color.Transparent;
            lblAccountLogin.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblAccountLogin.ForeColor = Color.White;
            lblAccountLogin.Location = new Point(188, 61);
            lblAccountLogin.Margin = new Padding(2, 0, 2, 0);
            lblAccountLogin.Name = "lblAccountLogin";
            lblAccountLogin.Size = new Size(258, 47);
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
            lblName.Location = new Point(56, 169);
            lblName.Name = "lblName";
            lblName.Size = new Size(111, 30);
            lblName.TabIndex = 2;
            lblName.Text = "Username";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.Transparent;
            lblPassword.Font = new Font("Segoe UI", 13F);
            lblPassword.ForeColor = SystemColors.Window;
            lblPassword.Location = new Point(56, 241);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(103, 30);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 13F);
            txtUsername.Location = new Point(197, 174);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(343, 36);
            txtUsername.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 13F);
            txtPassword.Location = new Point(197, 241);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(343, 36);
            txtPassword.TabIndex = 12;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.Location = new Point(275, 345);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(112, 45);
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
            lblForgetPassword.Location = new Point(390, 309);
            lblForgetPassword.Name = "lblForgetPassword";
            lblForgetPassword.Size = new Size(145, 23);
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
            lblRegister.Location = new Point(188, 393);
            lblRegister.Name = "lblRegister";
            lblRegister.Size = new Size(300, 28);
            lblRegister.TabIndex = 9;
            lblRegister.Text = "Don't have an account? Register";
            lblRegister.Click += lblRegister_Click;
            // 
            // chkRememberMe
            // 
            chkRememberMe.AutoSize = true;
            chkRememberMe.BackColor = Color.Transparent;
            chkRememberMe.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkRememberMe.ForeColor = Color.White;
            chkRememberMe.Location = new Point(114, 307);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(145, 27);
            chkRememberMe.TabIndex = 10;
            chkRememberMe.Text = "Remember me";
            chkRememberMe.UseVisualStyleBackColor = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(641, 436);
            Controls.Add(chkRememberMe);
            Controls.Add(lblRegister);
            Controls.Add(lblForgetPassword);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(lblName);
            Controls.Add(lblAccountLogin);
            Controls.Add(pctBoxLogo);
            Margin = new Padding(2, 2, 2, 2);
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)pctBoxLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pctBoxLogo;
        private Label lblAccountLogin;
        private Label lblName;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblForgetPassword;
        private Label lblRegister;
        private CheckBox chkRememberMe;
    }
}
