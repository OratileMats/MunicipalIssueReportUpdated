using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MunicipalIssueReport
{
    /// <summary>
    /// Central place for the application's modern look: colours, fonts and
    /// helpers that restyle standard WinForms controls (flat buttons, clean
    /// grids, soft backgrounds).
    /// </summary>
    public static class Theme
    {
        public const string FontFamily = "Segoe UI";

        public static readonly Color Background = Color.FromArgb(244, 246, 250);
        public static readonly Color Surface = Color.White;
        public static readonly Color Navy = Color.FromArgb(15, 23, 42);
        public static readonly Color NavyLight = Color.FromArgb(30, 41, 59);
        public static readonly Color Ink = Color.FromArgb(31, 41, 55);
        public static readonly Color Muted = Color.FromArgb(107, 114, 128);
        public static readonly Color OnNavyMuted = Color.FromArgb(148, 163, 184);
        public static readonly Color Border = Color.FromArgb(226, 232, 240);
        public static readonly Color Primary = Color.FromArgb(37, 99, 235);
        public static readonly Color PrimaryDark = Color.FromArgb(29, 78, 216);
        public static readonly Color PrimarySoft = Color.FromArgb(219, 234, 254);
        public static readonly Color Success = Color.FromArgb(22, 163, 74);
        public static readonly Color Warning = Color.FromArgb(217, 119, 6);
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);

        public static Font Ui(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font(FontFamily, size, style, GraphicsUnit.Point);
        }

        public static Color StatusColor(RequestStatus status)
        {
            switch (status)
            {
                case RequestStatus.UnderReview: return Warning;
                case RequestStatus.InProgress: return Primary;
                case RequestStatus.Resolved: return Success;
                default: return Color.FromArgb(100, 116, 139);
            }
        }

        public static GraphicsPath RoundRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // ---------- Buttons ----------

        public static void StylePrimary(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.UseVisualStyleBackColor = false;
            b.BackColor = Primary;
            b.ForeColor = Color.White;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = PrimaryDark;
            b.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);
            b.Cursor = Cursors.Hand;
        }

        public static void StyleSecondary(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.UseVisualStyleBackColor = false;
            b.BackColor = Surface;
            b.ForeColor = Ink;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(241, 245, 249);
            b.FlatAppearance.MouseDownBackColor = Color.FromArgb(226, 232, 240);
            b.Cursor = Cursors.Hand;
        }

        /// <summary>Subtle button for use on the dark header bar.</summary>
        public static void StyleGhost(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.UseVisualStyleBackColor = false;
            b.BackColor = NavyLight;
            b.ForeColor = Color.White;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
            b.FlatAppearance.MouseDownBackColor = Color.FromArgb(71, 85, 105);
            b.Cursor = Cursors.Hand;
        }

        // ---------- Grids ----------

        public static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Border;
            grid.EnableHeadersVisualStyles = false;
            grid.RowHeadersVisible = false;
            grid.AllowUserToResizeRows = false;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 42;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.RowTemplate.Height = 38;

            DataGridViewCellStyle header = new DataGridViewCellStyle();
            header.BackColor = Color.FromArgb(248, 250, 252);
            header.ForeColor = Muted;
            header.Font = Ui(9F, FontStyle.Bold);
            header.SelectionBackColor = header.BackColor;
            header.SelectionForeColor = header.ForeColor;
            header.Padding = new Padding(8, 0, 8, 0);
            grid.ColumnHeadersDefaultCellStyle = header;

            DataGridViewCellStyle cells = new DataGridViewCellStyle();
            cells.BackColor = Surface;
            cells.ForeColor = Ink;
            cells.Font = Ui(9.5F);
            cells.SelectionBackColor = PrimarySoft;
            cells.SelectionForeColor = Ink;
            cells.Padding = new Padding(8, 0, 8, 0);
            grid.DefaultCellStyle = cells;
        }

        // ---------- Whole-form styling ----------

        /// <summary>
        /// Restyles an existing form (and all of its controls) without
        /// changing layout, so the older Task 2 screens match the new look.
        /// </summary>
        public static void Apply(Form form)
        {
            form.BackColor = Background;
            ApplyToChildren(form);
        }

        private static void ApplyToChildren(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                RestyleFont(c);

                if (c is CardButton)
                {
                    // already custom drawn
                }
                else if (c is Button)
                {
                    Button b = (Button)c;
                    bool secondary = b.Name.IndexOf("Back", StringComparison.OrdinalIgnoreCase) >= 0
                        || b.Name.IndexOf("Clear", StringComparison.OrdinalIgnoreCase) >= 0;

                    if (secondary)
                    {
                        StyleSecondary(b);
                    }
                    else
                    {
                        StylePrimary(b);
                    }
                }
                else if (c is DataGridView)
                {
                    StyleGrid((DataGridView)c);
                }
                else if (c is TextBox || c is RichTextBox)
                {
                    c.BackColor = Surface;
                    c.ForeColor = Ink;
                }
                else if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.BackColor = Surface;
                    cb.ForeColor = Ink;
                }
                else if (c is ListBox)
                {
                    ListBox lb = (ListBox)c;
                    lb.BackColor = Surface;
                    lb.ForeColor = Ink;
                }
                else if (c is GroupBox)
                {
                    c.ForeColor = Ink;
                }
                else if (c is Label)
                {
                    if (c.ForeColor == SystemColors.ControlText)
                    {
                        c.ForeColor = Ink;
                    }
                }

                if (c.HasChildren)
                {
                    ApplyToChildren(c);
                }
            }
        }

        private static void RestyleFont(Control c)
        {
            if (c is DataGridView || c is CardButton)
            {
                return;
            }

            if (string.Equals(c.Font.Name, FontFamily, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            c.Font = new Font(FontFamily, c.Font.SizeInPoints, c.Font.Style, GraphicsUnit.Point);
        }
    }
}
