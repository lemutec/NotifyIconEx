using DirectN;
using Wice;
using Wice.Effects;

namespace WiceApp1;

public class MainWindow : Window, IDisposable
{
    public MainWindow()
    {
        // we draw our own titlebar using Wice itself
        WindowsFrameMode = WindowsFrameMode.None;

        // resize to 66% of the screen
        var monitor = GetMonitor().Bounds;
        ResizeClient(monitor.Width * 2 / 3, monitor.Height * 2 / 3);

        // the EnableBlurBehind call may be necessary when using the Windows' acrylic depending on Windows version
        // otherwise the window will be (almost) black
        Native.EnableBlurBehind();
        RenderBrush = AcrylicBrush.CreateAcrylicBrush(
            CompositionDevice,
            _D3DCOLORVALUE.White,
            0.2f,
            useWindowsAcrylic: true
            );
    }

    public void Dispose()
    {
    }
}
