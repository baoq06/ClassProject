namespace ClassProject
{
    partial class ViewGradesForm
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
            lblStats = new Label();
            lblGpa = new Label();
            dgvGrades = new DataGridView();
            btnRefresh = new Button();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvGrades).BeginInit();
            SuspendLayout();

            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            lblTitle.Location = new System.Drawing.Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(220, 37);
            lblTitle.Text = "BẢNG ĐIỂM CỦA TÔI";

            //
            // lblStats
            //
            lblStats.AutoSize = true;
            lblStats.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblStats.Location = new System.Drawing.Point(20, 60);
            lblStats.Name = "lblStats";
            lblStats.Text = "Tổng số môn đã đăng ký: 0   |   Đã có điểm: 0   |   Chưa có điểm: 0";

            //
            // dgvGrades
            //
            dgvGrades.AllowUserToAddRows = false;
            dgvGrades.AllowUserToDeleteRows = false;
            dgvGrades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrades.Location = new System.Drawing.Point(20, 95);
            dgvGrades.MultiSelect = false;
            dgvGrades.Name = "dgvGrades";
            dgvGrades.ReadOnly = true;
            dgvGrades.RowHeadersVisible = false;
            dgvGrades.RowTemplate.Height = 28;
            dgvGrades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrades.Size = new System.Drawing.Size(840, 380);
            dgvGrades.TabIndex = 0;

            //
            // lblGpa
            //
            lblGpa.AutoSize = true;
            lblGpa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblGpa.ForeColor = System.Drawing.Color.FromArgb(30, 100, 30);
            lblGpa.Location = new System.Drawing.Point(20, 490);
            lblGpa.Name = "lblGpa";
            lblGpa.Text = "Điểm trung bình: -";

            //
            // btnRefresh
            //
            btnRefresh.Location = new System.Drawing.Point(610, 485);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(120, 40);
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;

            //
            // btnClose
            //
            btnClose.Location = new System.Drawing.Point(740, 485);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(120, 40);
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;

            //
            // ViewGradesForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(880, 545);
            Controls.Add(btnClose);
            Controls.Add(btnRefresh);
            Controls.Add(lblGpa);
            Controls.Add(dgvGrades);
            Controls.Add(lblStats);
            Controls.Add(lblTitle);
            Name = "ViewGradesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bảng điểm";
            Load += ViewGradesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvGrades).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblStats;
        private Label lblGpa;
        private DataGridView dgvGrades;
        private Button btnRefresh;
        private Button btnClose;
    }
}