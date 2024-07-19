using NotifyIconEx;
using System;
using System.Diagnostics;
using System.Windows;

namespace WPFApp1;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var notifyIcon = new NotifyIcon()
        {
            Text = "NotifyIcon",
            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule?.FileName!)
        };
        notifyIcon.AddMenu("MenuItem1");
        notifyIcon.AddMenu("MenuItem2", true);
        notifyIcon.AddMenu("MenuItem3", OnClick);
        notifyIcon.AddMenu("-");
        notifyIcon.AddMenu("Exit", (sender, e) => Current.Shutdown());
        notifyIcon.BalloonTipShown += OnBalloonTipShown;

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
