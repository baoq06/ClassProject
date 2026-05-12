namespace ClassProject.Presentation.Forms
{
    partial class RegisterRoleForm
    {
        private System.ComponentModel.IContainer components = null;

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
            lblPrompt = new Label();
            radStudent = new RadioButton();
            radLecturer = new RadioButton();
            btnOk = new Button();
            btnCancel = new Button();
            SuspendLayout();
            //
            // lblPrompt
            //
            lblPrompt.AutoSize = true;
            lblPrompt.Font = new Font("Segoe UI", 11F);
            lblPrompt.Location = new Point(20, 18);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(280, 30);
            lblPrompt.Text = "Chọn vai trò tài khoản:";
            //
            // radStudent
            //
            radStudent.AutoSize = true;
            radStudent.Checked = true;
            radStudent.Location = new Point(24, 58);
            radStudent.Name = "radStudent";
            radStudent.Size = new Size(110, 29);
            radStudent.TabStop = true;
            radStudent.Text = "Sinh viên";
            radStudent.UseVisualStyleBackColor = true;
            //
            // radLecturer
            //
            radLecturer.AutoSize = true;
            radLecturer.Location = new Point(24, 96);
            radLecturer.Name = "radLecturer";
            radLecturer.Size = new Size(120, 29);
            radLecturer.TabStop = true;
            radLecturer.Text = "Giảng viên";
            radLecturer.UseVisualStyleBackColor = true;
            //
            // btnOk
            //
            btnOk.DialogResult = DialogResult.None;
            btnOk.Location = new Point(48, 148);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(110, 36);
            btnOk.Text = "Tiếp tục";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            //
            // btnCancel
            //
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(178, 148);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 36);
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            //
            // RegisterRoleForm
            //
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(338, 208);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(radLecturer);
            Controls.Add(radStudent);
            Controls.Add(lblPrompt);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegisterRoleForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chọn vai trò";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblPrompt;
        private RadioButton radStudent;
        private RadioButton radLecturer;
        private Button btnOk;
        private Button btnCancel;
    }
}