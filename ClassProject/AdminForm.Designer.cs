namespace ClassProject
{
    partial class AdminForm
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
            dgvPending = new DataGridView();
            btnBackToLogin = new Button();
            btnApprove = new Button();
            btnReject = new Button();
            btnRefresh = new Button();
            btnUploadExcel = new Button();
            lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPending).BeginInit();
            SuspendLayout();
            // 
            // dgvPending
            // 
            dgvPending.AllowUserToAddRows = false;
            dgvPending.AllowUserToDeleteRows = false;
            dgvPending.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPending.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPending.BackgroundColor = Color.White;
            dgvPending.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPending.Location = new Point(24, 96);
            dgvPending.MultiSelect = false;
            dgvPending.Name = "dgvPending";
            dgvPending.ReadOnly = true;
            dgvPending.RowHeadersWidth = 82;
            dgvPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPending.Size = new Size(1427, 880);
            dgvPending.TabIndex = 0;
            // 
            // btnUploadExcel
            // 
            btnUploadExcel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUploadExcel.BackColor = Color.ForestGreen;
            btnUploadExcel.FlatStyle = FlatStyle.Flat;
            btnUploadExcel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUploadExcel.ForeColor = Color.White;
            btnUploadExcel.Location = new Point(730, 24);
            btnUploadExcel.Name = "btnUploadExcel";
            btnUploadExcel.Size = new Size(180, 52);
            btnUploadExcel.TabIndex = 5;
            btnUploadExcel.Text = "📂 Upload Excel";
            btnUploadExcel.UseVisualStyleBackColor = false;
            btnUploadExcel.Click += btnUploadExcel_Click;
            // 
            // btnBackToLogin
            // 
            btnBackToLogin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBackToLogin.BackColor = SystemColors.ActiveBorder;
            btnBackToLogin.FlatStyle = FlatStyle.Flat;
            btnBackToLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBackToLogin.ForeColor = Color.White;
            btnBackToLogin.Location = new Point(930, 24);
            btnBackToLogin.Name = "btnBackToLogin";
            btnBackToLogin.Size = new Size(120, 52);
            btnBackToLogin.TabIndex = 4;
            btnBackToLogin.Text = "Back";
            btnBackToLogin.UseVisualStyleBackColor = false;
            btnBackToLogin.Click += btnBackToLogin_Click;
            // 
            // btnReject
            // 
            btnReject.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnReject.BackColor = Color.Crimson;
            btnReject.FlatStyle = FlatStyle.Flat;
            btnReject.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReject.ForeColor = Color.White;
            btnReject.Location = new Point(1070, 24);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(120, 52);
            btnReject.TabIndex = 6;
            btnReject.Text = "❌ Reject";
            btnReject.UseVisualStyleBackColor = false;
            btnReject.Click += btnReject_Click;
            // 
            // btnApprove
            // 
            btnApprove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnApprove.BackColor = Color.DodgerBlue;
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnApprove.ForeColor = Color.White;
            btnApprove.Location = new Point(1210, 24);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(120, 52);
            btnApprove.TabIndex = 1;
            btnApprove.Text = "✅ Approve";
            btnApprove.UseVisualStyleBackColor = false;
            btnApprove.Click += btnApprove_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = SystemColors.ActiveBorder;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1350, 24);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 52);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(24, 28);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(668, 51);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "DANH SÁCH SINH VIÊN CHỜ DUYỆT";
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(1475, 1085);
            Controls.Add(lblTitle);
            Controls.Add(btnRefresh);
            Controls.Add(btnApprove);
            Controls.Add(btnReject);
            Controls.Add(btnBackToLogin);
            Controls.Add(btnUploadExcel);
            Controls.Add(dgvPending);
            Name = "AdminForm";
            Text = "AdminForm";
            Load += AdminForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPending).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPending;
        private Button btnBackToLogin;
        private Button btnApprove;
        private Button btnReject;
        private Button btnRefresh;
        private Button btnUploadExcel;
        private Label lblTitle;
    }
}

