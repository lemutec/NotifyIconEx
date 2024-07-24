# NotifyIconEx [![NuGet](https://img.shields.io/nuget/v/NotifyIconEx.svg)](https://nuget.org/packages/NotifyIconEx) [![Actions](https://github.com/lemutec/NotifyIconEx/actions/workflows/library.nuget.yml/badge.svg)](https://github.com/lemutec/NotifyIconEx/actions/workflows/library.nuget.yml) [![Platform](https://img.shields.io/badge/platform-Windows-blue?logo=windowsxp&color=1E9BFA)](https://dotnet.microsoft.com/zh-cn/download/dotnet/latest/runtime)

NotifyIconEx is an easy-to-use library for displaying NotifyIcon (notification icon) in `WPF` / `WinForms` / `Avalonia` / `WinUI` / `MAUI` / `Wice` applications, offering non-intrusive system notifications and quick access functionality in the taskbar.

> Support dark mode / show icon / checkable / submenus.
>
> You should enable the HiDPI in your Application for better rendering.

## Usage

NotifyIconEx is available as [NuGet package](https://www.nuget.org/packages/NotifyIconEx).

```csharp
using NotifyIconEx;

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
```

You can set the context menu theme like following:

```csharp
// NotifyIcon default theme follows the system theme.
// Change it to Dark theme.
NotifyIcon.Theme = NotifyIconTheme.Dark;
```

## Demo

1. [NotifyIcon.Demo.Avalonia](https://github.com/lemutec/NotifyIconEx/tree/master/NotifyIcon.Demo.Avalonia) for [Avalonia](https://github.com/AvaloniaUI/Avalonia) Application.

2. [NotifyIcon.Demo.Maui](https://github.com/lemutec/NotifyIconEx/tree/master/NotifyIcon.Demo.Maui) for [MAUI](https://github.com/dotnet/maui) Application.
3. [NotifyIcon.Demo.WPF](https://github.com/lemutec/NotifyIconEx/tree/master/NotifyIcon.Demo.WPF) for [WPF](https://github.com/dotnet/wpf) Application.
4. [NotifyIcon.Demo.Wice](https://github.com/lemutec/NotifyIconEx/tree/master/NotifyIcon.Demo.Wice) for [Wice](https://github.com/aelyo-softworks/Wice) Application.
5. [NotifyIcon.Demo.WinForm](https://github.com/lemutec/NotifyIconEx/tree/master/NotifyIcon.Demo.WinForm) for [WinForms](https://github.com/dotnet/winforms) Application.
6. [NotifyIcon.Demo.WinUI](https://github.com/lemutec/NotifyIconEx/tree/master/NotifyIcon.Demo.WinUI) for [WinUI](https://github.com/microsoft/microsoft-ui-xaml) Application.

## Repository

The source code for NotifyIcon is hosted on GitHub. You can find it at the following URL: [https://github.com/lemutec/NotifyIconEx](https://github.com/lemutec/NotifyIconEx)

## License

NotifyIconEx is released under the MIT license. This means you are free to use and modify it, as long as you comply with the terms of the license.
