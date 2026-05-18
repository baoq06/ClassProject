namespace ClassProject
{
    partial class UC_Approve
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvPending = new DataGridView();
            panelTop = new Panel();
            lblTitle = new Label();
            flowButtons = new FlowLayoutPanel();
            btnUploadExcel = new Button();
            btnApprove = new Button();
            btnReject = new Button();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPending).BeginInit();
            panelTop.SuspendLayout();
            flowButtons.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(52, 73, 94);
            panelTop.Controls.Add(flowButtons);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 64;
            panelTop.Padding = new Padding(8, 8, 8, 8);
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Padding = new Padding(4, 8, 0, 0);
            lblTitle.Size = new Size(380, 48);
            lblTitle.Text = "DANH SÁCH SINH VIÊN CHỜ DUYỆT";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowButtons
            // 
            flowButtons.Controls.Add(btnUploadExcel);
            flowButtons.Controls.Add(btnApprove);
            flowButtons.Controls.Add(btnReject);
            flowButtons.Controls.Add(btnRefresh);
            flowButtons.Dock = DockStyle.Right;
            flowButtons.FlowDirection = FlowDirection.LeftToRight;
            flowButtons.WrapContents = false;
            flowButtons.AutoSize = true;
            flowButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowButtons.Padding = new Padding(0, 4, 0, 0);
            // 
            // btnUploadExcel
            // 
            btnUploadExcel.BackColor = Color.ForestGreen;
            btnUploadExcel.FlatStyle = FlatStyle.Flat;
            btnUploadExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUploadExcel.ForeColor = Color.White;
            btnUploadExcel.Margin = new Padding(4, 0, 4, 0);
            btnUploadExcel.Size = new Size(160, 40);
            btnUploadExcel.Text = "Upload Excel";
            btnUploadExcel.UseVisualStyleBackColor = false;
            btnUploadExcel.Click += btnUploadExcel_Click;
            // 
            // btnApprove
            // 
            btnApprove.BackColor = Color.DodgerBlue;
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnApprove.ForeColor = Color.White;
            btnApprove.Margin = new Padding(4, 0, 4, 0);
            btnApprove.Size = new Size(110, 40);
            btnApprove.Text = "Approve";
            btnApprove.UseVisualStyleBackColor = false;
            btnApprove.Click += btnApprove_Click;
            // 
            // btnReject
            // 
            btnReject.BackColor = Color.Crimson;
            btnReject.FlatStyle = FlatStyle.Flat;
            btnReject.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReject.ForeColor = Color.White;
            btnReject.Margin = new Padding(4, 0, 4, 0);
            btnReject.Size = new Size(110, 40);
            btnReject.Text = "Reject";
            btnReject.UseVisualStyleBackColor = false;
            btnReject.Click += btnReject_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.Gray;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Margin = new Padding(4, 0, 4, 0);
            btnRefresh.Size = new Size(100, 40);
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // dgvPending
            // 
            dgvPending.AllowUserToAddRows = false;
            dgvPending.AllowUserToDeleteRows = false;
            dgvPending.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPending.BackgroundColor = Color.White;
            dgvPending.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPending.Dock = DockStyle.Fill;
            dgvPending.MultiSelect = false;
            dgvPending.ReadOnly = true;
            dgvPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // 
            // UC_Approve
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(dgvPending);
            Controls.Add(panelTop);
            Name = "UC_Approve";
            Size = new Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)dgvPending).EndInit();
            panelTop.ResumeLayout(false);
            flowButtons.ResumeLayout(false);
            flowButtons.PerformLayout();
            ResumeLayout(false);
        }

        private DataGridView dgvPending;
        private Panel panelTop;
        private Label lblTitle;
        private FlowLayoutPanel flowButtons;
        private Button btnUploadExcel;
        private Button btnApprove;
        private Button btnReject;
        private Button btnRefresh;
    }
}
