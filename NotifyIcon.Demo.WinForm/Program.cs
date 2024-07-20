using System.Diagnostics;
using System.Reflection;
using NotifyIcon = NotifyIconEx.NotifyIcon;

namespace WinFormsApp1;

internal static class Program
{
    [STAThread]
    public static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        Assembly.GetExecutingAssembly().GetManifestResourceNames().ToList().ForEach(name => Debug.WriteLine(name));

        var notifyIcon = new NotifyIcon()
        {
            Text = "NotifyIcon",
            Icon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule?.FileName!)!
        };
        notifyIcon.AddMenu("MenuItem1", new Bitmap(ResourceHelper.GetStream("NotifyIcon.Demo.WinForm.Assets.Images.Lock.png")));
        var toDisableItem = notifyIcon.AddMenu("MenuItem2", new Bitmap(ResourceHelper.GetStream("NotifyIcon.Demo.WinForm.Assets.Images.Lock.png")));
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
        notifyIcon.AddMenu("Exit", (_, _) => Application.Exit());
        notifyIcon.BalloonTipShown += OnBalloonTipShown;

        toDisableItem.Enabled = false;

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

file static class ResourceHelper
{
    public static Stream GetStream(string name, Assembly assembly = null!)
    {
        Stream stream = (assembly ?? Assembly.GetExecutingAssembly()).GetManifestResourceStream(name)!;
        return stream;
    }
}
