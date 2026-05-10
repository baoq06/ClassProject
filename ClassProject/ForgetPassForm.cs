using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassProject.Presentation.Forms
{
    public partial class ForgetPassForm : Form
    {
        public ForgetPassForm()
        {
            InitializeComponent();
        }

        private void lblBacktoLogin_Click(object sender, EventArgs e)
        {
            LoginForm f = new LoginForm();
            f.Show();
            this.Hide();
        }

        private void btnSendOTP_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OTP sent (demo)");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Password reset (demo)");
        }
    }
}
