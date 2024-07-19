# NotifyIcon [![NuGet](https://img.shields.io/nuget/v/NotifyIconEx.svg)](https://nuget.org/packages/NotifyIconEx) [![Actions](https://github.com/lemutec/NotifyIconEx/actions/workflows/library.nuget.yml/badge.svg)](https://github.com/lemutec/NotifyIconEx/actions/workflows/library.nuget.yml)

NotifyIcon is an easy-to-use library for displaying NotifyIcon (notification icon) in both WPF and WinForms applications, offering non-intrusive system notifications and quick access functionality in the taskbar.

## Usage

-------

NotifyIcon is available as [NuGet package](https://www.nuget.org/packages/NotifyIconEx).

```csharp
using NotifyIconEx;

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
```

## Repository

-------

The source code for NotifyIcon is hosted on GitHub. You can find it at the following URL: [https://github.com/lemutec/NotifyIconEx](https://github.com/lemutec/NotifyIconEx)

## License

-------

NotifyIcon is released under the MIT license. This means you are free to use and modify it, as long as you comply with the terms of the license.
