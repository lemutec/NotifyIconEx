# NotifyIcon [![NuGet](https://img.shields.io/nuget/v/PicaPico.NotifyIcon.svg)](https://nuget.org/packages/PicaPico.NotifyIcon) [![Build AutoUpdate](https://github.com/HeHang0/NotifyIcon/actions/workflows/library.nuget.yml/badge.svg)](https://github.com/HeHang0/NotifyIcon/actions/workflows/library.nuget.yml)

NotifyIcon is an easy-to-use library for displaying NotifyIcon (notification icon) in both WPF and WinForms applications, offering non-intrusive system notifications and quick access functionality in the taskbar.

## Usage

-------

NotifyIcon is available as [NuGet package](https://www.nuget.org/packages/PicaPico.NotifyIcon).

```csharp
using PicaPico;

string GetExePath()
{
    Process currentProcess = Process.GetCurrentProcess();
    return currentProcess.MainModule?.FileName ?? string.Empty;
}

var notifyIcon = new NotifyIcon()
{
    Text = "NotifyIcon",
    Icon = System.Drawing.Icon.ExtractAssociatedIcon(GetExePath())
};
var menuFirst = notifyIcon.AddMenu("MenuItem1");
menuFirst.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
notifyIcon.AddMenu("MenuItem2", true);
notifyIcon.AddMenu("MenuItem3", onClick);
var menuLast = notifyIcon.AddMenu("Exit", (sender, e) => { Current.Shutdown(); });
menuLast.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
notifyIcon.BalloonTipShown += OnBalloonTipShown;

void OnBalloonTipShown(object? sender, EventArgs e)
{
    MessageBox.Show("OnBalloonTipShown");
}
void onClick(object? sender, EventArgs e)
{
    notifyIcon.BalloonTipTitle = "Title";
    notifyIcon.BalloonTipText = "This Balloon Tips";
    notifyIcon.ShowBalloonTip(5);
}
```

## Repository

-------

The source code for NotifyIcon is hosted on GitHub. You can find it at the following URL: [https://github.com/HeHang0/NotifyIcon](https://github.com/HeHang0/NotifyIcon)

## License

-------

NotifyIcon is released under the MIT license. This means you are free to use and modify it, as long as you comply with the terms of the license.
