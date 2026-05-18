namespace ClassProject
{
    partial class CourseRegistrationForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblAvailable = new Label();
            lblRegistered = new Label();
            dgvAvailable = new DataGridView();
            dgvRegistered = new DataGridView();
            btnRegister = new Button();
            btnUnregister = new Button();
            btnRefresh = new Button();
            btnClose = new Button();
            lblTotalCredits = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvAvailable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRegistered).BeginInit();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(285, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ĐĂNG KÝ MÔN HỌC";
            //
            // lblAvailable
            //
            lblAvailable.AutoSize = true;
            lblAvailable.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblAvailable.Location = new System.Drawing.Point(20, 70);
            lblAvailable.Name = "lblAvailable";
            lblAvailable.Size = new System.Drawing.Size(178, 25);
            lblAvailable.TabIndex = 1;
            lblAvailable.Text = "Môn học khả dụng";
            //
            // lblRegistered
            //
            lblRegistered.AutoSize = true;
            lblRegistered.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblRegistered.Location = new System.Drawing.Point(560, 70);
            lblRegistered.Name = "lblRegistered";
            lblRegistered.Size = new System.Drawing.Size(190, 25);
            lblRegistered.TabIndex = 2;
            lblRegistered.Text = "Môn đã đăng ký";
            //
            // dgvAvailable
            //
            dgvAvailable.AllowUserToAddRows = false;
            dgvAvailable.AllowUserToDeleteRows = false;
            dgvAvailable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAvailable.Location = new System.Drawing.Point(20, 100);
            dgvAvailable.MultiSelect = false;
            dgvAvailable.Name = "dgvAvailable";
            dgvAvailable.ReadOnly = true;
            dgvAvailable.RowTemplate.Height = 25;
            dgvAvailable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAvailable.Size = new System.Drawing.Size(520, 380);
            dgvAvailable.TabIndex = 3;
            //
            // dgvRegistered
            //
            dgvRegistered.AllowUserToAddRows = false;
            dgvRegistered.AllowUserToDeleteRows = false;
            dgvRegistered.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRegistered.Location = new System.Drawing.Point(560, 100);
            dgvRegistered.MultiSelect = false;
            dgvRegistered.Name = "dgvRegistered";
            dgvRegistered.ReadOnly = true;
            dgvRegistered.RowTemplate.Height = 25;
            dgvRegistered.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRegistered.Size = new System.Drawing.Size(520, 380);
            dgvRegistered.TabIndex = 4;
            //
            // btnRegister
            //
            btnRegister.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            btnRegister.ForeColor = System.Drawing.Color.White;
            btnRegister.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnRegister.Location = new System.Drawing.Point(20, 500);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new System.Drawing.Size(180, 45);
            btnRegister.TabIndex = 5;
            btnRegister.Text = ">> Đăng ký môn này";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            //
            // btnUnregister
            //
            btnUnregister.BackColor = System.Drawing.Color.FromArgb(180, 50, 50);
            btnUnregister.ForeColor = System.Drawing.Color.White;
            btnUnregister.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnUnregister.Location = new System.Drawing.Point(560, 500);
            btnUnregister.Name = "btnUnregister";
            btnUnregister.Size = new System.Drawing.Size(180, 45);
            btnUnregister.TabIndex = 6;
            btnUnregister.Text = "<< Hủy đăng ký";
            btnUnregister.UseVisualStyleBackColor = false;
            btnUnregister.Click += btnUnregister_Click;
            //
            // btnRefresh
            //
            btnRefresh.Location = new System.Drawing.Point(770, 500);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(120, 45);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            //
            // btnClose
            //
            btnClose.Location = new System.Drawing.Point(900, 500);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(120, 45);
            btnClose.TabIndex = 8;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            //
            // lblTotalCredits
            //
            lblTotalCredits.AutoSize = true;
            lblTotalCredits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblTotalCredits.ForeColor = System.Drawing.Color.FromArgb(30, 100, 30);
            lblTotalCredits.Location = new System.Drawing.Point(560, 560);
            lblTotalCredits.Name = "lblTotalCredits";
            lblTotalCredits.Size = new System.Drawing.Size(220, 23);
            lblTotalCredits.TabIndex = 9;
            lblTotalCredits.Text = "Tổng số môn: 0   |   Tổng tín chỉ: 0";
            //
            // CourseRegistrationForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1100, 600);
            Controls.Add(lblTotalCredits);
            Controls.Add(btnClose);
            Controls.Add(btnRefresh);
            Controls.Add(btnUnregister);
            Controls.Add(btnRegister);
            Controls.Add(dgvRegistered);
            Controls.Add(dgvAvailable);
            Controls.Add(lblRegistered);
            Controls.Add(lblAvailable);
            Controls.Add(lblTitle);
            Name = "CourseRegistrationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng ký môn học";
            Load += CourseRegistrationForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAvailable).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRegistered).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblAvailable;
        private Label lblRegistered;
        private DataGridView dgvAvailable;
        private DataGridView dgvRegistered;
        private Button btnRegister;
        private Button btnUnregister;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblTotalCredits;
    }
}