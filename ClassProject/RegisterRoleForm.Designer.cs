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
            lblPrompt.Location = new Point(26, 23);
            lblPrompt.Margin = new Padding(4, 0, 4, 0);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(316, 41);
            lblPrompt.TabIndex = 4;
            lblPrompt.Text = "Chọn vai trò tài khoản:";
            // 
            // radStudent
            // 
            radStudent.AutoSize = true;
            radStudent.Checked = true;
            radStudent.Location = new Point(31, 74);
            radStudent.Margin = new Padding(4, 4, 4, 4);
            radStudent.Name = "radStudent";
            radStudent.Size = new Size(144, 36);
            radStudent.TabIndex = 3;
            radStudent.TabStop = true;
            radStudent.Text = "Sinh viên";
            radStudent.UseVisualStyleBackColor = true;
            // 
            // radLecturer
            // 
            radLecturer.AutoSize = true;
            radLecturer.Location = new Point(31, 123);
            radLecturer.Margin = new Padding(4, 4, 4, 4);
            radLecturer.Name = "radLecturer";
            radLecturer.Size = new Size(159, 36);
            radLecturer.TabIndex = 2;
            radLecturer.TabStop = true;
            radLecturer.Text = "Giảng viên";
            radLecturer.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(62, 189);
            btnOk.Margin = new Padding(4, 4, 4, 4);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(143, 46);
            btnOk.TabIndex = 1;
            btnOk.Text = "Tiếp tục";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(231, 189);
            btnCancel.Margin = new Padding(4, 4, 4, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(143, 46);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // RegisterRoleForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(439, 266);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(radLecturer);
            Controls.Add(radStudent);
            Controls.Add(lblPrompt);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 4, 4, 4);
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