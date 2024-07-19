using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace NotifyIconEx;

internal class ModernToolStripRenderer : ToolStripProfessionalRenderer
{
    protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
    {
        var rect = e.ImageRectangle;
        using Pen pen = new(e.Item.ForeColor, 2);
        int x = rect.Left + 12;
        int y = rect.Top + (rect.Height - 10) / 2;
        Point[] points =
        [
            new Point(x + 2, y + 5),
            new Point(x + 6, y + 9),
            new Point(x + 15, y),
        ];
        e.Graphics.DrawLines(pen, points);
    }

    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        if (!e.Item.Selected) return;
        Color bgColor = ThemeListener.IsDarkMode ? Color.FromArgb(0x1A, 0xFF, 0xFF, 0xFF) : Color.FromArgb(0x1A, 0, 0, 0);

        Rectangle rect = new(4, 0, e.Item.Width - 8, e.Item.Height - 1);

        using SolidBrush brush = new(bgColor);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        GraphicsPath path = GetRoundedRect(rect, 3);
        e.Graphics.FillPath(brush, path);
    }

    private GraphicsPath GetRoundedRect(Rectangle rect, int cornerRadius)
    {
        int diameter = 2 * cornerRadius;
        Size size = new(diameter, diameter);
        Rectangle arcRect = new(rect.Location, size);
        GraphicsPath path = new();

        path.AddArc(arcRect, 180, 90);

        arcRect.X = rect.Right - diameter;
        path.AddArc(arcRect, 270, 90);

        arcRect.Y = rect.Bottom - diameter;
        path.AddArc(arcRect, 0, 90);

        arcRect.X = rect.Left;
        path.AddArc(arcRect, 90, 90);

        path.CloseFigure();
        return path;
    }

    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
        ///
    }

    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
        ///
    }

    protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
    {
        ///
    }
}
