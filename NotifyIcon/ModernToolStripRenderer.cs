﻿using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace NotifyIconEx;

public class ModernToolStripRenderer : ToolStripProfessionalRenderer
{
    protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
    {
        ToolStripItem item = e.Item!;

        if (item is ToolStripDropDownItem)
        {
            e.ArrowColor = item.Enabled ? NotifyIconColors.ForeColor : SystemColors.ControlDark;
        }

        base.OnRenderArrow(e);
    }

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

        Rectangle rect = new(4, 0, e.Item.Width - 8, e.Item.Height - 1);
        using SolidBrush brush = new(NotifyIconColors.HoverBackColor);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        GraphicsPath path = GetRoundedRect(rect, 3);
        e.Graphics.FillPath(brush, path);
    }

    private static GraphicsPath GetRoundedRect(Rectangle rect, int cornerRadius)
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
        ToolStrip toolStrip = e.ToolStrip;

        if (toolStrip is ContextMenuStrip)
        {
            ///
        }
        else if (toolStrip is ToolStripDropDownMenu)
        {
            DwmApi.SetContextMenuRoundedCorner(toolStrip.Handle);
            toolStrip.ForeColor = NotifyIconColors.ForeColor;
            toolStrip.BackColor = NotifyIconColors.BackColor;
        }
    }

    protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
    {
        // No plans to repaint `Separator` nowaday.
        base.OnRenderSeparator(e);
    }

    protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
    {
        ///
    }

    protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
    {
        Rectangle imageRect = e.ImageRectangle;
        Image image = e.Image!;

        if (imageRect != Rectangle.Empty && image is not null)
        {
            if (!e.Item.Enabled)
            {
                float[][] matrixItems = [
                    [1, 0, 0, 0, 0],
                    [0, 1, 0, 0, 0],
                    [0, 0, 1, 0, 0],
                    [0, 0, 0, 0.5f, 0],
                    [0, 0, 0, 0, 1]
                ];

                ColorMatrix colorMatrix = new(matrixItems);
                ImageAttributes imageAttributes = new();
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                imageRect.Offset(6, 0);
                e.Graphics.DrawImage(
                    image,
                    imageRect,
                    0,
                    0,
                    image.Width,
                    image.Height,
                    GraphicsUnit.Pixel,
                    imageAttributes);
                return;
            }

            // Since office images don't scoot one px we have to override all painting but enabled = false;
            if (e.Item.ImageScaling == ToolStripItemImageScaling.None)
            {
                e.Graphics.DrawImage(image, imageRect, new Rectangle(new Point(6, 0), imageRect.Size), GraphicsUnit.Pixel);
            }
            else
            {
                imageRect.Offset(6, 0);
                e.Graphics.DrawImage(image, imageRect);
            }
        }
    }
}
