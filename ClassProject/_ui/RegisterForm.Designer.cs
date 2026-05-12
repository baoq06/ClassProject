namespace ClassProject.Presentation.Forms
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            pictureBox1 = new PictureBox();
            lblCreatAcount = new Label();
            txtUsername = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            txtConfirm = new TextBox();
            chkAgree = new CheckBox();
            btnRegister = new Button();
            pnlCreateAccount = new Panel();
            lblBacktoLogin = new Label();
            lblConfirmPassword = new Label();
            lblPassword = new Label();
            lblEmail = new Label();
            lblUsername = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlCreateAccount.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(214, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(158, 147);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // lblCreatAcount
            // 
            lblCreatAcount.AutoSize = true;
            lblCreatAcount.BackColor = Color.Transparent;
            lblCreatAcount.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCreatAcount.ForeColor = Color.Black;
            lblCreatAcount.Location = new Point(103, 158);
            lblCreatAcount.Name = "lblCreatAcount";
            lblCreatAcount.Size = new Size(411, 71);
            lblCreatAcount.TabIndex = 2;
            lblCreatAcount.Text = "Create Account";
            lblCreatAcount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(63, 287);
            txtUsername.Margin = new Padding(5, 5, 5, 5);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Username";
            txtUsername.Size = new Size(454, 39);
            txtUsername.TabIndex = 5;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(63, 385);
            txtEmail.Margin = new Padding(5, 5, 5, 5);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email";
            txtEmail.Size = new Size(454, 39);
            txtEmail.TabIndex = 6;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(63, 490);
            txtPassword.Margin = new Padding(5, 5, 5, 5);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Password";
            txtPassword.Size = new Size(204, 39);
            txtPassword.TabIndex = 7;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtConfirm
            // 
            txtConfirm.Location = new Point(316, 490);
            txtConfirm.Margin = new Padding(5, 5, 5, 5);
            txtConfirm.Name = "txtConfirm";
            txtConfirm.PlaceholderText = "Confirm Password";
            txtConfirm.Size = new Size(204, 39);
            txtConfirm.TabIndex = 8;
            txtConfirm.UseSystemPasswordChar = true;
            // 
            // chkAgree
            // 
            chkAgree.AutoSize = true;
            chkAgree.BackColor = Color.Transparent;
            chkAgree.ForeColor = Color.Red;
            chkAgree.Location = new Point(63, 548);
            chkAgree.Margin = new Padding(5, 5, 5, 5);
            chkAgree.Name = "chkAgree";
            chkAgree.Size = new Size(215, 36);
            chkAgree.TabIndex = 9;
            chkAgree.Text = "I agree to terms";
            chkAgree.UseVisualStyleBackColor = false;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(63, 598);
            btnRegister.Margin = new Padding(5, 5, 5, 5);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(457, 46);
            btnRegister.TabIndex = 10;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // pnlCreateAccount
            // 
            pnlCreateAccount.BorderStyle = BorderStyle.FixedSingle;
            pnlCreateAccount.Controls.Add(lblBacktoLogin);
            pnlCreateAccount.Controls.Add(lblConfirmPassword);
            pnlCreateAccount.Controls.Add(lblPassword);
            pnlCreateAccount.Controls.Add(lblEmail);
            pnlCreateAccount.Controls.Add(lblUsername);
            pnlCreateAccount.Controls.Add(lblCreatAcount);
            pnlCreateAccount.Controls.Add(txtPassword);
            pnlCreateAccount.Controls.Add(pictureBox1);
            pnlCreateAccount.Controls.Add(btnRegister);
            pnlCreateAccount.Controls.Add(txtUsername);
            pnlCreateAccount.Controls.Add(chkAgree);
            pnlCreateAccount.Controls.Add(txtEmail);
            pnlCreateAccount.Controls.Add(txtConfirm);
            pnlCreateAccount.Location = new Point(335, 19);
            pnlCreateAccount.Margin = new Padding(5, 5, 5, 5);
            pnlCreateAccount.Name = "pnlCreateAccount";
            pnlCreateAccount.Size = new Size(585, 720);
            pnlCreateAccount.TabIndex = 12;
            // 
            // lblBacktoLogin
            // 
            lblBacktoLogin.AutoSize = true;
            lblBacktoLogin.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            lblBacktoLogin.Location = new Point(214, 658);
            lblBacktoLogin.Margin = new Padding(5, 0, 5, 0);
            lblBacktoLogin.Name = "lblBacktoLogin";
            lblBacktoLogin.Size = new Size(158, 32);
            lblBacktoLogin.TabIndex = 19;
            lblBacktoLogin.Text = "Back to Login";
            lblBacktoLogin.Click += lblBacktoLogin_Click;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new Point(314, 453);
            lblConfirmPassword.Margin = new Padding(5, 0, 5, 0);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(204, 32);
            lblConfirmPassword.TabIndex = 15;
            lblConfirmPassword.Text = "Confirm Password";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(63, 453);
            lblPassword.Margin = new Padding(5, 0, 5, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(111, 32);
            lblPassword.TabIndex = 14;
            lblPassword.Text = "Password";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(60, 348);
            lblEmail.Margin = new Padding(5, 0, 5, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(71, 32);
            lblEmail.TabIndex = 13;
            lblEmail.Text = "Email";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(60, 250);
            lblUsername.Margin = new Padding(5, 0, 5, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(121, 32);
            lblUsername.TabIndex = 12;
            lblUsername.Text = "Username";
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1242, 811);
            Controls.Add(pnlCreateAccount);
            Margin = new Padding(5, 5, 5, 5);
            MaximizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Register";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlCreateAccount.ResumeLayout(false);
            pnlCreateAccount.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Label lblCreatAcount;
        private TextBox txtUsername;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirm;
        private CheckBox chkAgree;
        private Button btnRegister;
        private Panel pnlCreateAccount;
        private Label lblConfirmPassword;
        private Label lblPassword;
        private Label lblEmail;
        private Label lblUsername;
        private Label lblBacktoLogin;
    }
}