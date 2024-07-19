using System.Diagnostics;

namespace WinFormsApp1;

internal static class Program
{
    [STAThread]
    public static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        var notifyIcon = new NotifyIconEx.NotifyIcon()
        {
            Text = "NotifyIcon",
            Icon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule?.FileName!)
        };
        notifyIcon.AddMenu("MenuItem1");
        notifyIcon.AddMenu("MenuItem2", true);
        notifyIcon.AddMenu("MenuItem3", OnClick);
        notifyIcon.AddMenu("-");
        notifyIcon.AddMenu("Exit", (sender, e) => { Application.Exit(); });
        notifyIcon.BalloonTipShown += OnBalloonTipShown;

        Application.Run();

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
