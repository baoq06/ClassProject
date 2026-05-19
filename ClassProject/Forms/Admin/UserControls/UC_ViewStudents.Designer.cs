namespace ClassProject
{
    partial class UC_ViewStudents
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
            panelTop = new Panel();
            lblTitle = new Label();
            flowToolbar = new FlowLayoutPanel();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnRefresh = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            dgvStudents = new DataGridView();
            panelStatus = new Panel();
            lblStudentCount = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            panelTop.SuspendLayout();
            flowToolbar.SuspendLayout();
            panelStatus.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(52, 73, 94);
            panelTop.Controls.Add(flowToolbar);
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
            lblTitle.Size = new Size(280, 48);
            lblTitle.Text = "DANH SÁCH SINH VIÊN";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowToolbar
            // 
            flowToolbar.Controls.Add(txtSearch);
            flowToolbar.Controls.Add(btnSearch);
            flowToolbar.Controls.Add(btnRefresh);
            flowToolbar.Controls.Add(btnEdit);
            flowToolbar.Controls.Add(btnDelete);
            flowToolbar.Dock = DockStyle.Right;
            flowToolbar.FlowDirection = FlowDirection.LeftToRight;
            flowToolbar.WrapContents = false;
            flowToolbar.AutoSize = true;
            flowToolbar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowToolbar.Padding = new Padding(0, 4, 0, 0);
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Margin = new Padding(4, 4, 4, 0);
            txtSearch.PlaceholderText = "Tìm MSSV, tên, email...";
            txtSearch.Size = new Size(220, 31);
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.DodgerBlue;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Margin = new Padding(4, 2, 4, 0);
            btnSearch.Size = new Size(80, 36);
            btnSearch.Text = "Tìm";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.Gray;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Margin = new Padding(4, 2, 4, 0);
            btnRefresh.Size = new Size(90, 36);
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.Orange;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEdit.ForeColor = Color.White;
            btnEdit.Margin = new Padding(4, 2, 4, 0);
            btnEdit.Size = new Size(80, 36);
            btnEdit.Text = "Sửa";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Crimson;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Margin = new Padding(4, 2, 4, 0);
            btnDelete.Size = new Size(80, 36);
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Dock = DockStyle.Fill;
            dgvStudents.MultiSelect = false;
            dgvStudents.ReadOnly = true;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.CellDoubleClick += dgvStudents_CellDoubleClick;
            // 
            // panelStatus
            // 
            panelStatus.BackColor = Color.FromArgb(236, 240, 241);
            panelStatus.BorderStyle = BorderStyle.FixedSingle;
            panelStatus.Controls.Add(lblStudentCount);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Height = 32;
            panelStatus.Padding = new Padding(8, 0, 12, 0);
            // 
            // lblStudentCount
            // 
            lblStudentCount.Dock = DockStyle.Right;
            lblStudentCount.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblStudentCount.ForeColor = Color.FromArgb(52, 73, 94);
            lblStudentCount.AutoSize = false;
            lblStudentCount.Size = new Size(220, 30);
            lblStudentCount.Text = "Tổng số sinh viên: 0";
            lblStudentCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // UC_ViewStudents
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(dgvStudents);
            Controls.Add(panelStatus);
            Controls.Add(panelTop);
            Name = "UC_ViewStudents";
            Size = new Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            panelTop.ResumeLayout(false);
            flowToolbar.ResumeLayout(false);
            flowToolbar.PerformLayout();
            panelStatus.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel panelTop;
        private Label lblTitle;
        private FlowLayoutPanel flowToolbar;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnRefresh;
        private Button btnEdit;
        private Button btnDelete;
        private DataGridView dgvStudents;
        private Panel panelStatus;
        private Label lblStudentCount;
    }
}