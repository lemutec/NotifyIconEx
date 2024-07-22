namespace WiceApp1;

internal class Program
{
    public static void Main()
    {
        using var dw = new App();
        using var win = new MainWindow();
        win.Center();
        win.Show();
        dw.Run();
    }
}
