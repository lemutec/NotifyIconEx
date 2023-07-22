using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PicaPico
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
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
        }
    }
}
