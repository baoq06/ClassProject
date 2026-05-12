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
            btnRefresh = new Button();
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
            dgvPending.RowTemplate.Height = 41;
            dgvPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPending.Size = new Size(1427, 880);
            dgvPending.TabIndex = 0;
            // 
            // btnBackToLogin
            // 
            btnBackToLogin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBackToLogin.BackColor = Color.DimGray;
            btnBackToLogin.FlatStyle = FlatStyle.Flat;
            btnBackToLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnBackToLogin.ForeColor = Color.White;
            btnBackToLogin.Location = new Point(920, 24);
            btnBackToLogin.Name = "btnBackToLogin";
            btnBackToLogin.Size = new Size(240, 52);
            btnBackToLogin.TabIndex = 4;
            btnBackToLogin.Text = "Quay về đăng nhập";
            btnBackToLogin.UseVisualStyleBackColor = false;
            btnBackToLogin.Click += btnBackToLogin_Click;
            // 
            // btnApprove
            // 
            btnApprove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnApprove.BackColor = Color.ForestGreen;
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnApprove.ForeColor = Color.White;
            btnApprove.Location = new Point(1175, 24);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(126, 52);
            btnApprove.TabIndex = 1;
            btnApprove.Text = "Duyệt";
            btnApprove.UseVisualStyleBackColor = false;
            btnApprove.Click += btnApprove_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.DodgerBlue;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1310, 24);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(126, 52);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Tải lại";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(24, 28);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(498, 51);
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
            Controls.Add(btnBackToLogin);
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
        private Button btnRefresh;
        private Label lblTitle;
    }
}