﻿using System.Drawing;

namespace NotifyIconEx;

internal static class NotifyIconColors
{
    public static Color ForeColor => ThemeListener.IsDarkMode ? ForeColorDark : ForeColorLight;
    public static Color BackColor => ThemeListener.IsDarkMode ? BackColorDark : BackColorLight;
    public static Color HoverBackColor => ThemeListener.IsDarkMode ? HoverBackColorDark : HoverBackColorLight;
    public static Color SeparatorColor => ThemeListener.IsDarkMode ? SeparatorColorDark : SeparatorColorLight;

    private static Color ForeColorLight => Color.FromArgb(0x99, 0x00, 0x00, 0x00);
    private static Color ForeColorDark => Color.FromArgb(0x99, 0xFF, 0xFF, 0xFF);

    private static Color BackColorLight => Color.FromArgb(0xF9, 0xF9, 0xF9);
    private static Color BackColorDark => Color.FromArgb(0x2C, 0x2C, 0x2C);

    private static Color HoverBackColorLight => Color.FromArgb(0x09, 0x00, 0x00, 0x00);
    private static Color HoverBackColorDark => Color.FromArgb(0x0B, 0xFF, 0xFF, 0xFF);

    private static Color SeparatorColorLight => Color.FromArgb(0xFF, 0xDA, 0xDA, 0xDA);
    private static Color SeparatorColorDark => Color.FromArgb(0xFF, 0x3E, 0x3E, 0x3E);
}
