using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Windows.Storage;
using Windows.Storage.Streams;
using Application = Microsoft.UI.Xaml.Application;
using NotifyIcon = NotifyIconEx.NotifyIcon;

namespace WinUIApp1;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host { get; }

    public static T GetService<T>() where T : class
    {
        if ((Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static Window MainWindow { get; } = new MainWindow();

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            ///
        }).
        Build();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var notifyIcon = new NotifyIcon()
        {
            Text = "NotifyIcon",
            Icon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule?.FileName!)!,
        };
        notifyIcon.AddMenu("MenuItem1", new Bitmap(ResourceHelper.GetStream("ms-appx:///Assets/Images/Lock.png")));
        var toDisableItem = notifyIcon.AddMenu("MenuItem2", new Bitmap(ResourceHelper.GetStream("ms-appx:///Assets/Images/Lock.png")));
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
        notifyIcon.AddMenu("Exit", (_, _) => Current.Exit());
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

        base.OnLaunched(args);
        MainWindow.AppWindow.Show();
    }
}

file static class ResourceHelper
{
    public static Stream GetStream(string uriString)
    {
        StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString)).AsTask().GetAwaiter().GetResult();
        IRandomAccessStream randomAccessStream = file.OpenAsync(FileAccessMode.Read).AsTask().GetAwaiter().GetResult();
        Stream stream = randomAccessStream.AsStreamForRead();
        return stream;
    }
}
