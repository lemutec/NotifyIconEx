using System.Diagnostics;

namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            string GetExePath()
            {
                Process currentProcess = Process.GetCurrentProcess();
                return currentProcess.MainModule?.FileName ?? string.Empty;
            }

            var notifyIcon = new PicaPico.NotifyIcon()
            {
                Text = "NotifyIcon",
                Icon = Icon.ExtractAssociatedIcon(GetExePath())
            };
            var menuFirst = notifyIcon.AddMenu("MenuItem1");
            menuFirst.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            notifyIcon.AddMenu("MenuItem2", true);
            notifyIcon.AddMenu("MenuItem3", onClick);
            var menuLast = notifyIcon.AddMenu("Exit", (sender, e) => { Application.Exit(); });
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

            Application.Run();
        }
    }
}