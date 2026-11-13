namespace MunicipalIssueReport
{
    partial class LocalEventsForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlGridBorder = new System.Windows.Forms.Panel();
            this.dgvEvents = new System.Windows.Forms.DataGridView();
            this.pnlGapBottom = new System.Windows.Forms.Panel();
            this.pnlRecBorder = new System.Windows.Forms.Panel();
            this.pnlRecCard = new System.Windows.Forms.Panel();
            this.lstRecommendations = new System.Windows.Forms.ListBox();
            this.lblRecommendations = new System.Windows.Forms.Label();
            this.pnlGapTop = new System.Windows.Forms.Panel();
            this.pnlFilterBorder = new System.Windows.Forms.Panel();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpEventDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlGridBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            this.pnlRecBorder.SuspendLayout();
            this.pnlRecCard.SuspendLayout();
            this.pnlFilterBorder.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnBack);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1180, 96);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(28, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(470, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Local Events and Announcements";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSubtitle.Location = new System.Drawing.Point(31, 56);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(420, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Find out what is happening in your community and get recommendations.";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(990, 29);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(160, 38);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "\u2190  Back To Main Menu";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.pnlBody.Controls.Add(this.pnlGridBorder);
            this.pnlBody.Controls.Add(this.pnlGapBottom);
            this.pnlBody.Controls.Add(this.pnlRecBorder);
            this.pnlBody.Controls.Add(this.pnlGapTop);
            this.pnlBody.Controls.Add(this.pnlFilterBorder);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 96);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(28, 22, 28, 24);
            this.pnlBody.Size = new System.Drawing.Size(1180, 624);
            this.pnlBody.TabIndex = 0;
            // 
            // pnlFilterBorder
            // 
            this.pnlFilterBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlFilterBorder.Controls.Add(this.pnlFilter);
            this.pnlFilterBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterBorder.Location = new System.Drawing.Point(28, 22);
            this.pnlFilterBorder.Name = "pnlFilterBorder";
            this.pnlFilterBorder.Padding = new System.Windows.Forms.Padding(1);
            this.pnlFilterBorder.Size = new System.Drawing.Size(1124, 94);
            this.pnlFilterBorder.TabIndex = 0;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.lblCategory);
            this.pnlFilter.Controls.Add(this.cmbCategory);
            this.pnlFilter.Controls.Add(this.lblDate);
            this.pnlFilter.Controls.Add(this.dtpEventDate);
            this.pnlFilter.Controls.Add(this.btnSearch);
            this.pnlFilter.Controls.Add(this.btnClear);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilter.Location = new System.Drawing.Point(1, 1);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1122, 92);
            this.pnlFilter.TabIndex = 0;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            this.lblCategory.Location = new System.Drawing.Point(24, 14);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(63, 15);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "Category";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(24, 36);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(260, 27);
            this.cmbCategory.TabIndex = 0;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            this.lblDate.Location = new System.Drawing.Point(312, 14);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(34, 15);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Date";
            // 
            // dtpEventDate
            // 
            this.dtpEventDate.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dtpEventDate.Location = new System.Drawing.Point(312, 36);
            this.dtpEventDate.Name = "dtpEventDate";
            this.dtpEventDate.Size = new System.Drawing.Size(240, 27);
            this.dtpEventDate.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(584, 34);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 32);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClear.Location = new System.Drawing.Point(716, 34);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(110, 32);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // pnlGapTop
            // 
            this.pnlGapTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGapTop.Location = new System.Drawing.Point(28, 116);
            this.pnlGapTop.Name = "pnlGapTop";
            this.pnlGapTop.Size = new System.Drawing.Size(1124, 16);
            this.pnlGapTop.TabIndex = 1;
            // 
            // pnlRecBorder
            // 
            this.pnlRecBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlRecBorder.Controls.Add(this.pnlRecCard);
            this.pnlRecBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRecBorder.Location = new System.Drawing.Point(28, 392);
            this.pnlRecBorder.Name = "pnlRecBorder";
            this.pnlRecBorder.Padding = new System.Windows.Forms.Padding(1);
            this.pnlRecBorder.Size = new System.Drawing.Size(1124, 208);
            this.pnlRecBorder.TabIndex = 2;
            // 
            // pnlRecCard
            // 
            this.pnlRecCard.BackColor = System.Drawing.Color.White;
            this.pnlRecCard.Controls.Add(this.lstRecommendations);
            this.pnlRecCard.Controls.Add(this.lblRecommendations);
            this.pnlRecCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRecCard.Location = new System.Drawing.Point(1, 1);
            this.pnlRecCard.Name = "pnlRecCard";
            this.pnlRecCard.Padding = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.pnlRecCard.Size = new System.Drawing.Size(1122, 206);
            this.pnlRecCard.TabIndex = 0;
            // 
            // lblRecommendations
            // 
            this.lblRecommendations.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRecommendations.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblRecommendations.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblRecommendations.Location = new System.Drawing.Point(8, 0);
            this.lblRecommendations.Name = "lblRecommendations";
            this.lblRecommendations.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblRecommendations.Size = new System.Drawing.Size(1106, 38);
            this.lblRecommendations.TabIndex = 1;
            this.lblRecommendations.Text = "Recommended Events";
            this.lblRecommendations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstRecommendations
            // 
            this.lstRecommendations.BackColor = System.Drawing.Color.White;
            this.lstRecommendations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstRecommendations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRecommendations.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstRecommendations.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstRecommendations.FormattingEnabled = true;
            this.lstRecommendations.IntegralHeight = false;
            this.lstRecommendations.ItemHeight = 25;
            this.lstRecommendations.Location = new System.Drawing.Point(8, 38);
            this.lstRecommendations.Name = "lstRecommendations";
            this.lstRecommendations.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstRecommendations.Size = new System.Drawing.Size(1106, 160);
            this.lstRecommendations.TabIndex = 0;
            // 
            // pnlGapBottom
            // 
            this.pnlGapBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlGapBottom.Location = new System.Drawing.Point(28, 376);
            this.pnlGapBottom.Name = "pnlGapBottom";
            this.pnlGapBottom.Size = new System.Drawing.Size(1124, 16);
            this.pnlGapBottom.TabIndex = 3;
            // 
            // pnlGridBorder
            // 
            this.pnlGridBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlGridBorder.Controls.Add(this.dgvEvents);
            this.pnlGridBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridBorder.Location = new System.Drawing.Point(28, 132);
            this.pnlGridBorder.Name = "pnlGridBorder";
            this.pnlGridBorder.Padding = new System.Windows.Forms.Padding(1);
            this.pnlGridBorder.Size = new System.Drawing.Size(1124, 244);
            this.pnlGridBorder.TabIndex = 4;
            // 
            // dgvEvents
            // 
            this.dgvEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEvents.Location = new System.Drawing.Point(1, 1);
            this.dgvEvents.Name = "dgvEvents";
            this.dgvEvents.RowHeadersWidth = 51;
            this.dgvEvents.Size = new System.Drawing.Size(1122, 242);
            this.dgvEvents.TabIndex = 4;
            // 
            // LocalEventsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(1180, 720);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.MinimumSize = new System.Drawing.Size(960, 640);
            this.Name = "LocalEventsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Local Events and Announcements";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlGridBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            this.pnlRecBorder.ResumeLayout(false);
            this.pnlRecCard.ResumeLayout(false);
            this.pnlFilterBorder.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlFilterBorder;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpEventDate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlGapTop;
        private System.Windows.Forms.Panel pnlGridBorder;
        private System.Windows.Forms.DataGridView dgvEvents;
        private System.Windows.Forms.Panel pnlGapBottom;
        private System.Windows.Forms.Panel pnlRecBorder;
        private System.Windows.Forms.Panel pnlRecCard;
        private System.Windows.Forms.ListBox lstRecommendations;
        private System.Windows.Forms.Label lblRecommendations;
    }
}
