using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MunicipalIssueReport
{
    /// <summary>
    /// Four-step progress tracker (Submitted - Under Review - In Progress -
    /// Resolved) painted as connected circles.
    /// </summary>
    public class StatusStepper : Control
    {
        private static readonly RequestStatus[] Steps =
        {
            RequestStatus.Submitted,
            RequestStatus.UnderReview,
            RequestStatus.InProgress,
            RequestStatus.Resolved
        };

        private RequestStatus current = RequestStatus.Submitted;
        private bool hasValue;

        public StatusStepper()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            BackColor = Theme.Surface;
            Height = 84;
        }

        public void SetStatus(RequestStatus status)
        {
            current = status;
            hasValue = true;
            Invalidate();
        }

        public void Clear()
        {
            hasValue = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(BackColor);

            int margin = 54;
            int radius = 12;
            int cy = 26;
            int span = Math.Max(1, Width - 2 * margin);
            int currentIndex = hasValue ? (int)current : -1;

            // Connecting lines
            for (int i = 0; i < Steps.Length - 1; i++)
            {
                int x1 = margin + i * span / (Steps.Length - 1) + radius;
                int x2 = margin + (i + 1) * span / (Steps.Length - 1) - radius;
                Color lineColor = i < currentIndex ? Theme.StatusColor(current) : Theme.Border;

                using (Pen pen = new Pen(lineColor, 3f))
                {
                    g.DrawLine(pen, x1, cy, x2, cy);
                }
            }

            using (Font labelFont = Theme.Ui(8.25F))
            using (Font labelBold = Theme.Ui(8.25F, FontStyle.Bold))
            using (Font tickFont = new Font("Segoe UI Symbol", 9F, FontStyle.Bold, GraphicsUnit.Point))
            {
                for (int i = 0; i < Steps.Length; i++)
                {
                    int cx = margin + i * span / (Steps.Length - 1);
                    Rectangle circle = new Rectangle(cx - radius, cy - radius, radius * 2, radius * 2);

                    bool done = i < currentIndex || (i == currentIndex && current == RequestStatus.Resolved);
                    bool isCurrent = i == currentIndex && !done;

                    if (done)
                    {
                        using (SolidBrush brush = new SolidBrush(Theme.StatusColor(current)))
                        {
                            g.FillEllipse(brush, circle);
                        }

                        TextRenderer.DrawText(g, "\u2713", tickFont, circle, Color.White,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                            TextFormatFlags.NoPadding);
                    }
                    else if (isCurrent)
                    {
                        using (SolidBrush brush = new SolidBrush(Color.White))
                        using (Pen ring = new Pen(Theme.StatusColor(current), 3.5f))
                        using (SolidBrush dot = new SolidBrush(Theme.StatusColor(current)))
                        {
                            g.FillEllipse(brush, circle);
                            g.DrawEllipse(ring, circle);
                            g.FillEllipse(dot, new Rectangle(cx - 4, cy - 4, 8, 8));
                        }
                    }
                    else
                    {
                        using (SolidBrush brush = new SolidBrush(Color.White))
                        using (Pen ring = new Pen(Color.FromArgb(203, 213, 225), 2f))
                        {
                            g.FillEllipse(brush, circle);
                            g.DrawEllipse(ring, circle);
                        }
                    }

                    Rectangle labelRect = new Rectangle(cx - 46, cy + radius + 6, 92, 22);
                    TextRenderer.DrawText(g, Issue.StatusName(Steps[i]),
                        i == currentIndex ? labelBold : labelFont, labelRect,
                        i <= currentIndex ? Theme.Ink : Theme.Muted,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.Top |
                        TextFormatFlags.NoPadding);
                }
            }
        }
    }
}
