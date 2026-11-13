using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MunicipalIssueReport
{
    /// <summary>
    /// Slim, rounded progress bar. It exposes the same Minimum, Maximum and
    /// Value members as the standard ProgressBar, so it is a drop-in
    /// replacement, but it is painted in the application's colours and turns
    /// green when it reaches 100%.
    /// </summary>
    public class ModernProgressBar : Control
    {
        private int minimum;
        private int maximum = 100;
        private int value;

        public ModernProgressBar()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            Height = 12;
            TabStop = false;
        }

        public int Minimum
        {
            get { return minimum; }
            set { minimum = value; Invalidate(); }
        }

        public int Maximum
        {
            get { return maximum; }
            set { maximum = value; Invalidate(); }
        }

        public int Value
        {
            get { return this.value; }
            set
            {
                this.value = Math.Max(minimum, Math.Min(maximum, value));
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent != null ? Parent.BackColor : Theme.Surface);

            if (Width < 4 || Height < 4)
            {
                return;
            }

            Rectangle track = new Rectangle(0, 0, Width - 1, Height - 1);
            int radius = track.Height / 2;

            using (GraphicsPath trackPath = Theme.RoundRect(track, radius))
            using (SolidBrush trackBrush = new SolidBrush(Color.FromArgb(226, 232, 240)))
            {
                g.FillPath(trackBrush, trackPath);
            }

            int range = maximum - minimum;

            if (range <= 0 || value <= minimum)
            {
                return;
            }

            int fillWidth = (int)Math.Round((double)(value - minimum) / range * track.Width);
            fillWidth = Math.Max(track.Height, Math.Min(track.Width, fillWidth));

            Rectangle fill = new Rectangle(0, 0, fillWidth, track.Height);
            Color colour = value >= maximum ? Theme.Success : Theme.Primary;

            using (GraphicsPath fillPath = Theme.RoundRect(fill, radius))
            using (SolidBrush fillBrush = new SolidBrush(colour))
            {
                g.FillPath(fillBrush, fillPath);
            }
        }
    }
}
