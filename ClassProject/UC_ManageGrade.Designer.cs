namespace ClassProject
{
    partial class UC_ManageGrades
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            panelTop = new Panel();
            lblTitle = new Label();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            splitContainer = new SplitContainer();
            dgvStudents = new DataGridView();
            panelGrade = new Panel();
            lblSelectedStudent = new Label();
            dgvGrades = new DataGridView();
            panelGradeButtons = new Panel();
            btnSave = new Button();
            btnDeleteGrade = new Button();
            btnRefresh = new Button();
            lblGpa = new Label();

            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            panelGrade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGrades).BeginInit();
            panelGradeButtons.SuspendLayout();
            SuspendLayout();

            //
            // panelTop
            //
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 64;
            panelTop.BackColor = Color.White;
            panelTop.Padding = new Padding(16, 12, 16, 8);
            panelTop.Controls.Add(btnSearch);
            panelTop.Controls.Add(txtSearch);
            panelTop.Controls.Add(lblSearch);
            panelTop.Controls.Add(lblTitle);
            panelTop.Name = "panelTop";

            //
            // lblTitle
            //
            lblTitle.Text = "QUẢN LÝ ĐIỂM SINH VIÊN";
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(16, 14);

            //
            // lblSearch
            //
            lblSearch.Text = "Tìm sinh viên:";
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F);
            lblSearch.Location = new Point(360, 18);

            //
            // txtSearch
            //
            txtSearch.Location = new Point(478, 14);
            txtSearch.Size = new Size(220, 27);
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Name = "txtSearch";
            txtSearch.KeyDown += txtSearch_KeyDown;

            //
            // btnSearch
            //
            btnSearch.Text = "Tìm";
            btnSearch.Location = new Point(704, 12);
            btnSearch.Size = new Size(80, 30);
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.BackColor = Color.FromArgb(41, 128, 185);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;

            //
            // splitContainer
            //
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.SplitterDistance = 380;
            splitContainer.Panel1.Controls.Add(dgvStudents);
            splitContainer.Panel2.Controls.Add(panelGrade);
            splitContainer.Name = "splitContainer";

            //
            // dgvStudents
            //
            dgvStudents.Dock = DockStyle.Fill;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.ReadOnly = true;
            dgvStudents.MultiSelect = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Name = "dgvStudents";
            dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;

            //
            // panelGrade
            //
            panelGrade.Dock = DockStyle.Fill;
            panelGrade.BackColor = Color.White;
            panelGrade.Padding = new Padding(12);
            panelGrade.Controls.Add(dgvGrades);
            panelGrade.Controls.Add(panelGradeButtons);
            panelGrade.Controls.Add(lblSelectedStudent);
            panelGrade.Name = "panelGrade";

            //
            // lblSelectedStudent
            //
            lblSelectedStudent.Dock = DockStyle.Top;
            lblSelectedStudent.Height = 36;
            lblSelectedStudent.TextAlign = ContentAlignment.MiddleLeft;
            lblSelectedStudent.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSelectedStudent.ForeColor = Color.FromArgb(41, 128, 185);
            lblSelectedStudent.Text = "Sinh viên đang chọn: -";
            lblSelectedStudent.Padding = new Padding(4, 0, 0, 0);

            //
            // dgvGrades
            //
            dgvGrades.Dock = DockStyle.Fill;
            dgvGrades.AllowUserToAddRows = false;
            dgvGrades.AllowUserToDeleteRows = false;
            dgvGrades.MultiSelect = false;
            dgvGrades.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            dgvGrades.RowHeadersVisible = false;
            dgvGrades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrades.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dgvGrades.Name = "dgvGrades";

            //
            // panelGradeButtons
            //
            panelGradeButtons.Dock = DockStyle.Bottom;
            panelGradeButtons.Height = 64;
            panelGradeButtons.BackColor = Color.White;
            panelGradeButtons.Padding = new Padding(0, 8, 0, 8);
            panelGradeButtons.Controls.Add(lblGpa);
            panelGradeButtons.Controls.Add(btnRefresh);
            panelGradeButtons.Controls.Add(btnDeleteGrade);
            panelGradeButtons.Controls.Add(btnSave);

            //
            // btnSave
            //
            btnSave.Text = "Lưu thay đổi";
            btnSave.Location = new Point(0, 12);
            btnSave.Size = new Size(150, 40);
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            //
            // btnDeleteGrade
            //
            btnDeleteGrade.Text = "Xóa điểm môn";
            btnDeleteGrade.Location = new Point(160, 12);
            btnDeleteGrade.Size = new Size(150, 40);
            btnDeleteGrade.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDeleteGrade.BackColor = Color.FromArgb(192, 57, 43);
            btnDeleteGrade.ForeColor = Color.White;
            btnDeleteGrade.FlatStyle = FlatStyle.Flat;
            btnDeleteGrade.FlatAppearance.BorderSize = 0;
            btnDeleteGrade.UseVisualStyleBackColor = false;
            btnDeleteGrade.Click += btnDeleteGrade_Click;

            //
            // btnRefresh
            //
            btnRefresh.Text = "Làm mới";
            btnRefresh.Location = new Point(320, 12);
            btnRefresh.Size = new Size(120, 40);
            btnRefresh.Font = new Font("Segoe UI", 10F);
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;

            //
            // lblGpa
            //
            lblGpa.Text = "GPA: -";
            lblGpa.AutoSize = true;
            lblGpa.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblGpa.ForeColor = Color.FromArgb(30, 100, 30);
            lblGpa.Location = new Point(460, 20);

            //
            // UC_ManageGrades
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(splitContainer);
            Controls.Add(panelTop);
            Name = "UC_ManageGrades";
            Size = new Size(1000, 600);

            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            panelGrade.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvGrades).EndInit();
            panelGradeButtons.ResumeLayout(false);
            panelGradeButtons.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblTitle;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private SplitContainer splitContainer;
        private DataGridView dgvStudents;
        private Panel panelGrade;
        private Label lblSelectedStudent;
        private DataGridView dgvGrades;
        private Panel panelGradeButtons;
        private Button btnSave;
        private Button btnDeleteGrade;
        private Button btnRefresh;
        private Label lblGpa;
    }
}