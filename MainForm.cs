using System;
using System.Windows.Forms;

namespace MunicipalIssueReport
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Keep the three option cards centred when the window is resized.
            pnlBody.Resize += (s, e) => LayoutCards();
            Load += (s, e) => LayoutCards();
        }

        private void LayoutCards()
        {
            CardButton[] cards = { btnReportIssue, btnLocalEvents, btnServiceStatus };

            int cardHeight = 96;
            int gap = 22;
            int cardWidth = Math.Min(680, pnlBody.ClientSize.Width - 80);
            int totalHeight = cards.Length * cardHeight + (cards.Length - 1) * gap;

            int left = (pnlBody.ClientSize.Width - cardWidth) / 2;
            int top = Math.Max(24, (pnlBody.ClientSize.Height - totalHeight) / 2 - 10);

            foreach (CardButton card in cards)
            {
                card.SetBounds(left, top, cardWidth, cardHeight);
                top += cardHeight + gap;
            }
        }

        private void btnReportIssue_Click(object sender, EventArgs e)
        {
            using (ReportIssueForm reportForm = new ReportIssueForm())
            {
                reportForm.ShowDialog(this);
            }
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            using (LocalEventsForm localEventsForm = new LocalEventsForm())
            {
                localEventsForm.ShowDialog(this);
            }
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            using (ServiceRequestStatusForm statusForm = new ServiceRequestStatusForm())
            {
                statusForm.ShowDialog(this);
            }
        }
    }
}
