using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MunicipalIssueReport
{
    /// <summary>
    /// A large, rounded "card" used for the main menu options. It is still a
    /// normal Button, so Click, Enabled and keyboard focus all work as usual.
    /// </summary>
    public class CardButton : Button
    {
        private bool hovered;
        private string title = "";
        private string description = "";
        private string glyph = "";
        private Color accent = Theme.Primary;

        public CardButton()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            FlatStyle = FlatStyle.Flat;
            Cursor = Cursors.Hand;
            TabStop = true;
        }

        [DefaultValue("")]
        public string Title
        {
            get { return title; }
            set { title = value ?? ""; Invalidate(); }
        }

        [DefaultValue("")]
        public string Description
        {
            get { return description; }
            set { description = value ?? ""; Invalidate(); }
        }

        /// <summary>A single symbol shown inside the coloured badge.</summary>
        [DefaultValue("")]
        public string Glyph
        {
            get { return glyph; }
            set { glyph = value ?? ""; Invalidate(); }
        }

        public Color Accent
        {
            get { return accent; }
            set { accent = value; Invalidate(); }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hovered = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            Invalidate();
            base.OnEnabledChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent != null ? Parent.BackColor : Theme.Background);

            Rectangle card = new Rectangle(1, 1, Width - 3, Height - 3);
            bool active = Enabled && (hovered || Focused);

            Color fill = !Enabled
                ? Color.FromArgb(241, 245, 249)
                : (hovered ? Color.FromArgb(248, 250, 255) : Theme.Surface);

            Color border = active ? accent : Theme.Border;
            Color badgeColor = Enabled ? accent : Color.FromArgb(203, 213, 225);

            using (GraphicsPath path = Theme.RoundRect(card, 14))
            using (SolidBrush fillBrush = new SolidBrush(fill))
            using (Pen borderPen = new Pen(border, active ? 2f : 1f))
            {
                g.FillPath(fillBrush, path);
                g.DrawPath(borderPen, path);
            }

            // Badge
            int badgeSize = Math.Min(56, card.Height - 32);
            Rectangle badge = new Rectangle(
                card.X + 22,
                card.Y + (card.Height - badgeSize) / 2,
                badgeSize,
                badgeSize);

            using (GraphicsPath badgePath = Theme.RoundRect(badge, 12))
            using (SolidBrush badgeBrush = new SolidBrush(badgeColor))
            {
                g.FillPath(badgeBrush, badgePath);
            }

            using (Font glyphFont = new Font("Segoe UI Symbol", 17F, FontStyle.Regular, GraphicsUnit.Point))
            {
                TextRenderer.DrawText(g, glyph, glyphFont, badge, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                    TextFormatFlags.NoPadding);
            }

            // Text
            int textLeft = badge.Right + 20;
            int textWidth = card.Right - textLeft - 50;

            using (Font titleFont = Theme.Ui(12.5F, FontStyle.Bold))
            using (Font descFont = Theme.Ui(9.5F))
            {
                Rectangle titleRect = new Rectangle(textLeft, card.Y + card.Height / 2 - 28, textWidth, 28);
                Rectangle descRect = new Rectangle(textLeft, card.Y + card.Height / 2 + 2, textWidth, 24);

                TextRenderer.DrawText(g, title, titleFont, titleRect,
                    Enabled ? Theme.Ink : Theme.Muted,
                    TextFormatFlags.Left | TextFormatFlags.Bottom |
                    TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);

                TextRenderer.DrawText(g, description, descFont, descRect,
                    Theme.Muted,
                    TextFormatFlags.Left | TextFormatFlags.Top |
                    TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
            }

            // Chevron
            if (Enabled)
            {
                int cx = card.Right - 30;
                int cy = card.Y + card.Height / 2;

                using (Pen chevron = new Pen(active ? accent : Color.FromArgb(148, 163, 184), 2.2f))
                {
                    chevron.StartCap = LineCap.Round;
                    chevron.EndCap = LineCap.Round;
                    g.DrawLine(chevron, cx - 4, cy - 7, cx + 3, cy);
                    g.DrawLine(chevron, cx + 3, cy, cx - 4, cy + 7);
                }
            }
        }
    }
}
