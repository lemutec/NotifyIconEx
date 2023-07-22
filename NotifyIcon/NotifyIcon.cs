using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace PicaPico
{
    public class NotifyIcon
    {
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private readonly ToolStripRenderer _toolStripRenderer;
        protected EventHandlerList Events = new EventHandlerList();
        private static readonly object EVENT_MOUSEDOWN = new object();
        private static readonly object EVENT_MOUSEMOVE = new object();
        private static readonly object EVENT_MOUSEUP = new object();
        private static readonly object EVENT_CLICK = new object();
        private static readonly object EVENT_DOUBLECLICK = new object();
        private static readonly object EVENT_MOUSECLICK = new object();
        private static readonly object EVENT_MOUSEDOUBLECLICK = new object();
        private static readonly object EVENT_BALLOONTIPSHOWN = new object();
        private static readonly object EVENT_BALLOONTIPCLICKED = new object();
        private static readonly object EVENT_BALLOONTIPCLOSED = new object();
        private static readonly object EVENT_DISPOSED = new object();

        public NotifyIcon(ToolStripRenderer toolStripRenderer=null)
        {
            _toolStripRenderer = toolStripRenderer ?? new ModernToolStripRenderer();
            Control.CheckForIllegalCrossThreadCalls = false;
            notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = true
            };
            notifyIcon.BalloonTipClicked += (sender, e) => { Events[EVENT_BALLOONTIPCLICKED]?.DynamicInvoke(sender, e); };
            notifyIcon.BalloonTipClosed += (sender, e) => { Events[EVENT_BALLOONTIPCLOSED]?.DynamicInvoke(sender, e); }; ;
            notifyIcon.BalloonTipShown += (sender, e) => { Events[EVENT_BALLOONTIPSHOWN]?.DynamicInvoke(sender, e); }; ;
            notifyIcon.Click += (sender, e) => { Events[EVENT_CLICK]?.DynamicInvoke(sender, e); }; ;
            notifyIcon.Disposed += (sender, e) => { Events[EVENT_DISPOSED]?.DynamicInvoke(sender, e); }; ;
            notifyIcon.DoubleClick += (sender, e) => { Events[EVENT_DOUBLECLICK]?.DynamicInvoke(sender, e); }; ;
            notifyIcon.MouseClick += (sender, e) => { Events[EVENT_MOUSECLICK]?.DynamicInvoke(sender, e); }; ;
            notifyIcon.MouseDoubleClick += (sender, e) => { Events[EVENT_MOUSEDOUBLECLICK]?.DynamicInvoke(sender, e); }; ;
            notifyIcon.MouseDown += (sender, e) => { Events[EVENT_MOUSEDOWN]?.DynamicInvoke(sender, e); }; ;
            notifyIcon.MouseMove += (sender, e) => { Events[EVENT_MOUSEMOVE]?.DynamicInvoke(sender, e); }; ;
            notifyIcon.MouseUp += (sender, e) => { Events[EVENT_MOUSEUP]?.DynamicInvoke(sender, e); }; ;
            UpdateStyle();
            ProcessContextMenuStrip();
            ThemeListener.ThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged(bool isDark)
        {
            UpdateStyle();
        }

        public void AddMenu(IEnumerable<ToolStripMenuItem> menuItems)
        {
            if (notifyIcon.ContextMenuStrip == null)
            {
                ContextMenuStrip = new ContextMenuStrip();
            }
            foreach (var menuItem in menuItems)
            {
                notifyIcon.ContextMenuStrip.Items.Add(menuItem);
            }
            UpdateStyle();
        }

        public ToolStripMenuItem AddMenu(ToolStripMenuItem menuItem)
        {
            if (notifyIcon.ContextMenuStrip == null)
            {
                ContextMenuStrip = new ContextMenuStrip();
            }
            notifyIcon.ContextMenuStrip.Items.Add(menuItem);
            UpdateStyle();
            return menuItem;
        }

        public ToolStripMenuItem AddMenu(string text)
        {
            return AddMenu(new ToolStripMenuItem(text));
        }

        public ToolStripMenuItem AddMenu(string text, bool check)
        {
            return AddMenu(new ToolStripMenuItem(text) { Checked = check });
        }

        public ToolStripMenuItem AddMenu(string text, bool check, EventHandler onClick)
        {
            return AddMenu(new ToolStripMenuItem(text, null, onClick) { Checked = check });
        }

        public ToolStripMenuItem AddMenu(string text, EventHandler onClick)
        {
            return AddMenu(new ToolStripMenuItem(text, null, onClick));
        }

        public ToolStripMenuItem AddMenu(string text, Image image, EventHandler onClick)
        {
            return AddMenu(new ToolStripMenuItem(text, image, onClick));
        }

        public ToolStripMenuItem AddMenu(string text, Image image, EventHandler onClick, string name)
        {
            return AddMenu(new ToolStripMenuItem(text, image, onClick, name));
        }

        public ToolStripMenuItem AddMenu(string text, Image image, params ToolStripItem[] dropDownItems)
        {
            return AddMenu(new ToolStripMenuItem(text, image, dropDownItems));
        }

        public ToolStripMenuItem AddMenu(string text, Image image, EventHandler onClick, Keys shortcutKeys)
        {
            return AddMenu(new ToolStripMenuItem(text, image, onClick, shortcutKeys));
        }

        private void ProcessContextMenuStrip()
        {
            if (notifyIcon.ContextMenuStrip == null) return;
            notifyIcon.ContextMenuStrip.Renderer = _toolStripRenderer;
            notifyIcon.ContextMenuStrip.HandleCreated += ContextMenuStrip_HandleCreated;
        }

        public event EventHandler BalloonTipClicked
        {
            add
            {
                Events.AddHandler(EVENT_BALLOONTIPCLICKED, value);
            }
            remove
            {
                Events.RemoveHandler(EVENT_BALLOONTIPCLICKED, value);
            }
        }

        public event EventHandler Disposed
        {
            add
            {
                Events.AddHandler(EVENT_DISPOSED, value);
            }
            remove
            {
                Events.RemoveHandler(EVENT_DISPOSED, value);
            }
        }

        public event EventHandler BalloonTipClosed
        {
            add
            {
                Events.AddHandler(EVENT_BALLOONTIPCLOSED, value);
            }
            remove
            {
                Events.RemoveHandler(EVENT_BALLOONTIPCLOSED, value);
            }
        }

        public event EventHandler BalloonTipShown
        {
            add
            {
                Events.AddHandler(EVENT_BALLOONTIPSHOWN, value);
            }
            remove
            {
                Events.RemoveHandler(EVENT_BALLOONTIPSHOWN, value);
            }
        }

        public event EventHandler Click
        {
            add
            {
                Events.AddHandler(EVENT_CLICK, value);
            }
            remove
            {
                Events.RemoveHandler(EVENT_CLICK, value);
            }
        }

        public event EventHandler DoubleClick
        {
            add
            {
                Events.AddHandler(EVENT_DOUBLECLICK, value);
            }
            remove
            {
                Events.RemoveHandler(EVENT_DOUBLECLICK, value);
            }
        }

        public event MouseEventHandler MouseClick
        {
            add
            {
                Events.AddHandler(EVENT_MOUSECLICK, value);
            }
            remove
            {
                Events.RemoveHandler(EVENT_MOUSECLICK, value);
            }
        }

        public event MouseEventHandler MouseDoubleClick
        {
            add
            {
                Events.AddHandler(EVENT_MOUSEDOUBLECLICK, value);
            }
            remove
            {
                Events.RemoveHandler(EVENT_MOUSEDOUBLECLICK, value);
            }
        }

        public event MouseEventHandler MouseDown
        {
            add
            {
                Events.AddHandler(EVENT_MOUSEDOWN, value);
            }
            remove
            {
                Events.RemoveHandler(EVENT_MOUSEDOWN, value);
            }
        }

        public event MouseEventHandler MouseMove
        {
            add
            {
                Events.AddHandler(EVENT_MOUSEMOVE, value);
            }
            remove
            {
                Events.RemoveHandler(EVENT_MOUSEMOVE, value);
            }
        }

        public event MouseEventHandler MouseUp
        {
            add
            {
                Events.AddHandler(EVENT_MOUSEUP, value);
            }
            remove
            {
                Events.RemoveHandler(EVENT_MOUSEUP, value);
            }
        }

        public bool Visible
        {
            get { return notifyIcon.Visible; }
            set { notifyIcon.Visible = value; }
        }

        public string Text
        {
            get { return notifyIcon.Text; }
            set { notifyIcon.Text = value; }
        }

        public object Tag
        {
            get { return notifyIcon.Tag; }
            set { notifyIcon.Tag = value; }
        }

        public ISite Site
        {
            get { return notifyIcon.Site; }
            set { notifyIcon.Site = value; }
        }

        public Icon Icon
        {
            get { return notifyIcon.Icon; }
            set { notifyIcon.Icon = value; }
        }

        public ContextMenuStrip ContextMenuStrip
        {
            get { return notifyIcon.ContextMenuStrip; }
            set
            {
                if (notifyIcon.ContextMenuStrip != null)
                {
                    notifyIcon.ContextMenuStrip.HandleCreated -= ContextMenuStrip_HandleCreated;
                }
                notifyIcon.ContextMenuStrip = value;
                ProcessContextMenuStrip();
            }
        }

        public ToolTipIcon BalloonTipIcon
        {
            get { return notifyIcon.BalloonTipIcon; }
            set { notifyIcon.BalloonTipIcon = value; }
        }

        public IContainer Container => notifyIcon.Container;

        public string BalloonTipTitle
        {
            get { return notifyIcon.BalloonTipTitle; }
            set { notifyIcon.BalloonTipTitle = value; }
        }

        public string BalloonTipText
        {
            get { return notifyIcon.BalloonTipText; }
            set { notifyIcon.BalloonTipText = value; }
        }

        public void Dispose()
        {
            notifyIcon.Dispose();
        }

        /// <summary>
        ///  Displays a balloon tooltip in the taskbar.
        ///
        ///  The system enforces minimum and maximum timeout values. Timeout
        ///  values that are too large are set to the maximum value and values
        ///  that are too small default to the minimum value. The operating system's
        ///  default minimum and maximum timeout values are 10 seconds and 30 seconds,
        ///  respectively.
        ///
        ///  No more than one balloon ToolTip at at time is displayed for the taskbar.
        ///  If an application attempts to display a ToolTip when one is already being displayed,
        ///  the ToolTip will not appear until the existing balloon ToolTip has been visible for at
        ///  least the system minimum timeout value. For example, a balloon ToolTip with timeout
        ///  set to 30 seconds has been visible for seven seconds when another application attempts
        ///  to display a balloon ToolTip. If the system minimum timeout is ten seconds, the first
        ///  ToolTip displays for an additional three seconds before being replaced by the second ToolTip.
        /// </summary>
        public void ShowBalloonTip(int timeout)
        {
            notifyIcon.ShowBalloonTip(timeout);
        }

        private void ContextMenuStrip_HandleCreated(object sender, EventArgs e)
        {
            SetContextMenuRoundedCorner(notifyIcon.ContextMenuStrip.Handle);
        }

        public void UpdateStyle()
        {
            if (notifyIcon.ContextMenuStrip == null) return;
            var dark = ThemeListener.IsDarkMode;
            var backColor = dark ? Color.FromArgb(0x2B, 0x2B, 0x2B) : Color.FromArgb(0xF2, 0xF2, 0xF2);
            var foreColor = dark ? Color.FromArgb(0x99, 0xFF, 0xFF, 0xFF) : Color.FromArgb(0x99, 0, 0, 0);
            notifyIcon.ContextMenuStrip.BackColor = backColor;
            notifyIcon.ContextMenuStrip.ForeColor = foreColor;
            notifyIcon.ContextMenuStrip.Invalidate();
        }

        private static void SetContextMenuRoundedCorner(IntPtr handle)
        {
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUNDSMALL;
            DwmSetWindowAttribute(handle, attribute, ref preference, sizeof(uint));
        }

        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);
    }
}
