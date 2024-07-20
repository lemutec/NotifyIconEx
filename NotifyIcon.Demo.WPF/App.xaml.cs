using NotifyIconEx;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Resources;
using ToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem;

namespace WpfApp1;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var notifyIcon = new NotifyIcon()
        {
            Text = "NotifyIcon",
            Icon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule?.FileName!)!
        };
        notifyIcon.AddMenu("MenuItem1", new Bitmap(ResourceHelper.GetStream("pack://application:,,,/Assets/Images/Lock.png")));
        var toDisableItem = notifyIcon.AddMenu("MenuItem2", new Bitmap(ResourceHelper.GetStream("pack://application:,,,/Assets/Images/Lock.png")));
        notifyIcon.AddMenu("-");
        notifyIcon.AddMenu("MenuItem3");
        notifyIcon.AddMenu("MenuItem4", true);
        notifyIcon.AddMenu("MenuItem5", OnClick);
        notifyIcon.AddMenu("-");
        notifyIcon.AddMenu("SubMenu", null!,
        [
            new ToolStripMenuItem("SubMenuItem1"),
            new ToolStripMenuItem("SubMenuItem2"),
            new ToolStripMenuItem("SubMenuItem3"),
            new ToolStripMenuItem("SubSubMenu", null!,
            [
                new ToolStripMenuItem("SubSubMenuItem1"),
                new ToolStripMenuItem("SubSubMenuItem2"),
                new ToolStripMenuItem("SubSubMenuItem3")
            ])
        ]);
        notifyIcon.AddMenu("-");
        notifyIcon.AddMenu("Exit", (_, _) => Current.Shutdown());
        notifyIcon.BalloonTipShown += OnBalloonTipShown;

        toDisableItem.Enabled = false;

        void OnBalloonTipShown(object? sender, EventArgs e)
        {
            Debug.WriteLine("OnBalloonTipShown");
        }

        void OnClick(object? sender, EventArgs e)
        {
            notifyIcon.BalloonTipTitle = "Title";
            notifyIcon.BalloonTipText = "This Balloon Tips";
            notifyIcon.ShowBalloonTip(5);
        }
    }
}

file static class ResourceHelper
{
    static ResourceHelper()
    {
        if (!UriParser.IsKnownScheme("pack"))
            _ = PackUriHelper.UriSchemePack;
    }

    public static Stream GetStream(string uriString)
    {
        Uri uri = new(uriString);
        StreamResourceInfo info = Application.GetResourceStream(uri);
        return info?.Stream!;
    }
}
