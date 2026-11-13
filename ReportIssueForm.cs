using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MunicipalIssueReport
{
    public partial class ReportIssueForm : Form
    {
        private string selectedAttachmentPath = "";

        public ReportIssueForm()
        {
            InitializeComponent();

            // Modern look shared across the application
            Theme.Apply(this);
            Theme.StyleGhost(btnBack);          // sits on the dark header
            Theme.StyleSecondary(btnAttachment);
            lblEngagement.ForeColor = Theme.Primary;
            lblProgress.ForeColor = Theme.Primary;

            // Keep the form card centred and the Back button on the right.
            pnlBody.Resize += (s, e) => LayoutCard();
            pnlHeader.Resize += (s, e) => PositionBackButton();
            Load += (s, e) => { PositionBackButton(); LayoutCard(); };

            // Add issue categories
            cmbCategory.Items.Add("Roads");
            cmbCategory.Items.Add("Sanitation");
            cmbCategory.Items.Add("Water");
            cmbCategory.Items.Add("Electricity");
            cmbCategory.Items.Add("Waste Management");
            cmbCategory.Items.Add("Public Safety");
            cmbCategory.Items.Add("Other");

            // Set initial progress
            progressBarReport.Minimum = 0;
            progressBarReport.Maximum = 100;
            progressBarReport.Value = 0;
            lblProgress.Text = "0%";

            // Connect controls to progress updates
            txtLocation.TextChanged += txtLocation_TextChanged;
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
            rtbDescription.TextChanged += rtbDescription_TextChanged;

            // Connect Back button
            btnBack.Click += btnBack_Click;
        }

        private void UpdateProgress()
        {
            int progress = 0;

            // Location = 25%
            if (!string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                progress += 25;
            }

            // Category = 25%
            if (cmbCategory.SelectedIndex != -1)
            {
                progress += 25;
            }

            // Description = 25%
            if (!string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                progress += 25;
            }

            // Attachment = 25%
            if (!string.IsNullOrWhiteSpace(selectedAttachmentPath))
            {
                progress += 25;
            }

            progressBarReport.Value = progress;
            lblProgress.Text = progress + "%";

            // Update engagement message
            if (progress == 0)
            {
                lblEngagement.Text =
                    "Let's improve our community together!";
            }
            else if (progress == 25)
            {
                lblEngagement.Text =
                    "Great start! Keep going.";
            }
            else if (progress == 50)
            {
                lblEngagement.Text =
                    "You're halfway there!";
            }
            else if (progress == 75)
            {
                lblEngagement.Text =
                    "Almost done! Thank you for helping.";
            }
            else if (progress == 100)
            {
                lblEngagement.Text =
                    "Excellent! Your report is ready to submit.";
            }
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void rtbDescription_TextChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select an Image or Document";

            openFileDialog.Filter =
                "Image Files|*.jpg;*.jpeg;*.png;*.gif|" +
                "Document Files|*.pdf;*.doc;*.docx|" +
                "All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedAttachmentPath = openFileDialog.FileName;

                lblFileName.Text = openFileDialog.FileName;

                UpdateProgress();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validate location
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show(
                    "Please enter the location of the issue.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtLocation.Focus();
                return;
            }

            // Validate category
            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select an issue category.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbCategory.Focus();
                return;
            }

            // Validate description
            if (string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                MessageBox.Show(
                    "Please provide a description of the issue.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                rtbDescription.Focus();
                return;
            }

            // Create a new Issue object. The shared repository assigns the
            // reference number and stores it so it can be tracked in
            // Service Request Status.
            Issue issue = new Issue
            {
                Location = txtLocation.Text,
                Category = cmbCategory.SelectedItem.ToString(),
                Description = rtbDescription.Text,
                AttachmentPath = selectedAttachmentPath,
                DateReported = DateTime.Now
            };

            IssueRepository.Add(issue);

            // Set progress to 100%
            progressBarReport.Value = 100;

            // Update engagement message
            lblEngagement.Text =
                "Thank you for helping improve your community!";

            // Show success message
            MessageBox.Show(
            "Your issue has been reported successfully!\n\n" +
            "Your Issue Reference Number is: " + issue.IssueId + "\n\n" +
            "You can track it under Service Request Status.",
            "Report Submitted",
             MessageBoxButtons.OK,
             MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PositionBackButton()
        {
            btnBack.Location = new System.Drawing.Point(
                pnlHeader.ClientSize.Width - btnBack.Width - 28, 29);
        }

        private void LayoutCard()
        {
            int left = Math.Max(24, (pnlBody.ClientSize.Width - pnlCardBorder.Width) / 2);
            pnlCardBorder.Location = new System.Drawing.Point(
                left + pnlBody.AutoScrollPosition.X,
                24 + pnlBody.AutoScrollPosition.Y);
        }
    }
}
