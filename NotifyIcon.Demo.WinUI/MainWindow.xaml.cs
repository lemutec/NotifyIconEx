using Microsoft.UI.Xaml;

namespace WinUIApp1;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ExtendsContentIntoTitleBar = true;
        Title = "WinUIApp1";
    }
}
