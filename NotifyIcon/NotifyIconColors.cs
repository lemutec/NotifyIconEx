using System.Drawing;

namespace NotifyIconEx;

internal static class NotifyIconColors
{
    public static Color ForeColorLight => Color.FromArgb(0x99, 0x00, 0x00, 0x00);
    public static Color ForeColorDark => Color.FromArgb(0x99, 0xFF, 0xFF, 0xFF);

    public static Color ForeColor => ThemeListener.IsDarkMode ? ForeColorDark : ForeColorLight;

    public static Color BackColorLight => Color.FromArgb(0xF2, 0xF2, 0xF2);
    public static Color BackColorDark => Color.FromArgb(0x2B, 0x2B, 0x2B);

    public static Color BackColor => ThemeListener.IsDarkMode ? BackColorDark : BackColorLight;

    public static Color HoverBackColorLight => Color.FromArgb(0x1A, 0x00, 0x00, 0x00);
    public static Color HoverBackColorDark => Color.FromArgb(0x1A, 0xFF, 0xFF, 0xFF);

    public static Color HoverBackColor => ThemeListener.IsDarkMode ? HoverBackColorDark : HoverBackColorLight;
}
