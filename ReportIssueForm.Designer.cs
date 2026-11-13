namespace MunicipalIssueReport
{
    partial class ReportIssueForm
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
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlCardBorder = new System.Windows.Forms.Panel();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.pnlDivider1 = new System.Windows.Forms.Panel();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.btnAttachment = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.pnlDivider2 = new System.Windows.Forms.Panel();
            this.lblProgressCaption = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressBarReport = new MunicipalIssueReport.ModernProgressBar();
            this.lblEngagement = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlCardBorder.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.pnlHeader.Controls.Add(this.lblSubHeading);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.btnBack);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 96);
            this.pnlHeader.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(420, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Report a Municipal Issue";
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubHeading.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSubHeading.Location = new System.Drawing.Point(31, 56);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(430, 19);
            this.lblSubHeading.TabIndex = 1;
            this.lblSubHeading.Text = "Please provide the details below so that the municipality can assist you.";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(840, 29);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(130, 38);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "\u2190  Back";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // pnlBody
            // 
            this.pnlBody.AutoScroll = true;
            this.pnlBody.AutoScrollMinSize = new System.Drawing.Size(780, 660);
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.pnlBody.Controls.Add(this.pnlCardBorder);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 96);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1000, 674);
            this.pnlBody.TabIndex = 0;
            // 
            // pnlCardBorder
            // 
            this.pnlCardBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlCardBorder.Controls.Add(this.pnlCard);
            this.pnlCardBorder.Location = new System.Drawing.Point(140, 24);
            this.pnlCardBorder.Name = "pnlCardBorder";
            this.pnlCardBorder.Padding = new System.Windows.Forms.Padding(1);
            this.pnlCardBorder.Size = new System.Drawing.Size(722, 614);
            this.pnlCardBorder.TabIndex = 0;
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.Controls.Add(this.lblLocation);
            this.pnlCard.Controls.Add(this.txtLocation);
            this.pnlCard.Controls.Add(this.lblCategory);
            this.pnlCard.Controls.Add(this.cmbCategory);
            this.pnlCard.Controls.Add(this.lblDescription);
            this.pnlCard.Controls.Add(this.rtbDescription);
            this.pnlCard.Controls.Add(this.pnlDivider1);
            this.pnlCard.Controls.Add(this.lblAttachment);
            this.pnlCard.Controls.Add(this.btnAttachment);
            this.pnlCard.Controls.Add(this.lblFileName);
            this.pnlCard.Controls.Add(this.pnlDivider2);
            this.pnlCard.Controls.Add(this.lblProgressCaption);
            this.pnlCard.Controls.Add(this.lblProgress);
            this.pnlCard.Controls.Add(this.progressBarReport);
            this.pnlCard.Controls.Add(this.lblEngagement);
            this.pnlCard.Controls.Add(this.btnSubmit);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Location = new System.Drawing.Point(1, 1);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(720, 612);
            this.pnlCard.TabIndex = 0;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocation.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            this.lblLocation.Location = new System.Drawing.Point(32, 28);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(60, 15);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Location";
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtLocation.Location = new System.Drawing.Point(32, 50);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(656, 27);
            this.txtLocation.TabIndex = 0;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            this.lblCategory.Location = new System.Drawing.Point(32, 98);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(63, 15);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(32, 120);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(320, 28);
            this.cmbCategory.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            this.lblDescription.Location = new System.Drawing.Point(32, 168);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(74, 15);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description";
            // 
            // rtbDescription
            // 
            this.rtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbDescription.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.rtbDescription.Location = new System.Drawing.Point(32, 190);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(656, 120);
            this.rtbDescription.TabIndex = 2;
            this.rtbDescription.Text = "";
            // 
            // pnlDivider1
            // 
            this.pnlDivider1.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlDivider1.Location = new System.Drawing.Point(32, 334);
            this.pnlDivider1.Name = "pnlDivider1";
            this.pnlDivider1.Size = new System.Drawing.Size(656, 1);
            this.pnlDivider1.TabIndex = 6;
            // 
            // lblAttachment
            // 
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAttachment.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            this.lblAttachment.Location = new System.Drawing.Point(32, 354);
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(122, 15);
            this.lblAttachment.TabIndex = 7;
            this.lblAttachment.Text = "Attachment (optional)";
            // 
            // btnAttachment
            // 
            this.btnAttachment.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAttachment.Location = new System.Drawing.Point(32, 376);
            this.btnAttachment.Name = "btnAttachment";
            this.btnAttachment.Size = new System.Drawing.Size(140, 38);
            this.btnAttachment.TabIndex = 3;
            this.btnAttachment.Text = "Choose File";
            this.btnAttachment.UseVisualStyleBackColor = true;
            this.btnAttachment.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoEllipsis = true;
            this.lblFileName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFileName.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblFileName.Location = new System.Drawing.Point(184, 376);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(504, 38);
            this.lblFileName.TabIndex = 9;
            this.lblFileName.Text = "No file selected";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDivider2
            // 
            this.pnlDivider2.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlDivider2.Location = new System.Drawing.Point(32, 438);
            this.pnlDivider2.Name = "pnlDivider2";
            this.pnlDivider2.Size = new System.Drawing.Size(656, 1);
            this.pnlDivider2.TabIndex = 10;
            // 
            // lblProgressCaption
            // 
            this.lblProgressCaption.AutoSize = true;
            this.lblProgressCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProgressCaption.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            this.lblProgressCaption.Location = new System.Drawing.Point(32, 458);
            this.lblProgressCaption.Name = "lblProgressCaption";
            this.lblProgressCaption.Size = new System.Drawing.Size(116, 15);
            this.lblProgressCaption.TabIndex = 11;
            this.lblProgressCaption.Text = "Reporting progress";
            // 
            // lblProgress
            // 
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblProgress.Location = new System.Drawing.Point(608, 455);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(80, 20);
            this.lblProgress.TabIndex = 12;
            this.lblProgress.Text = "0%";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBarReport
            // 
            this.progressBarReport.Location = new System.Drawing.Point(32, 484);
            this.progressBarReport.Name = "progressBarReport";
            this.progressBarReport.Size = new System.Drawing.Size(656, 12);
            this.progressBarReport.TabIndex = 13;
            // 
            // lblEngagement
            // 
            this.lblEngagement.AutoEllipsis = true;
            this.lblEngagement.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEngagement.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblEngagement.Location = new System.Drawing.Point(32, 506);
            this.lblEngagement.Name = "lblEngagement";
            this.lblEngagement.Size = new System.Drawing.Size(656, 22);
            this.lblEngagement.TabIndex = 14;
            this.lblEngagement.Text = "Let\'s improve our community together!";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.Location = new System.Drawing.Point(508, 548);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(180, 44);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit Report";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // ReportIssueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(1000, 770);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.MinimumSize = new System.Drawing.Size(820, 600);
            this.Name = "ReportIssueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report an Issue";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlCardBorder.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSubHeading;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlCardBorder;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.Panel pnlDivider1;
        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.Button btnAttachment;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Panel pnlDivider2;
        private System.Windows.Forms.Label lblProgressCaption;
        private System.Windows.Forms.Label lblProgress;
        private ModernProgressBar progressBarReport;
        private System.Windows.Forms.Label lblEngagement;
        private System.Windows.Forms.Button btnSubmit;
    }
}
