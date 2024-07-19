using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace NotifyIconEx;

public class NotifyIcon
{
    private readonly System.Windows.Forms.NotifyIcon notifyIcon = null;
    private readonly ToolStripRenderer toolStripRenderer = null;
    protected EventHandlerList Events = new();
    private static readonly object EVENT_MOUSEDOWN = new();
    private static readonly object EVENT_MOUSEMOVE = new();
    private static readonly object EVENT_MOUSEUP = new();
    private static readonly object EVENT_CLICK = new();
    private static readonly object EVENT_DOUBLECLICK = new();
    private static readonly object EVENT_MOUSECLICK = new();
    private static readonly object EVENT_MOUSEDOUBLECLICK = new();
    private static readonly object EVENT_BALLOONTIPSHOWN = new();
    private static readonly object EVENT_BALLOONTIPCLICKED = new();
    private static readonly object EVENT_BALLOONTIPCLOSED = new();
    private static readonly object EVENT_DISPOSED = new();

    public NotifyIcon(ToolStripRenderer toolStripRenderer = null)
    {
        this.toolStripRenderer = toolStripRenderer ?? new ModernToolStripRenderer();
        Control.CheckForIllegalCrossThreadCalls = false;
        notifyIcon = new System.Windows.Forms.NotifyIcon()
        {
            Visible = true,
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

    public void AddMenu(IEnumerable<ToolStripItem> menuItems)
    {
        if (notifyIcon.ContextMenuStrip == null)
        {
            ContextMenuStrip = new ContextMenuStrip();
        }
        foreach (ToolStripItem menuItem in menuItems)
        {
            if (menuItem.Text == "-")
            {
                notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
                continue;
            }
            menuItem.Margin = new(8);
            notifyIcon.ContextMenuStrip.Items.Add(menuItem);
        }
        UpdateStyle();
    }

    public ToolStripItem AddMenu(ToolStripItem menuItem)
    {
        if (notifyIcon.ContextMenuStrip == null)
        {
            ContextMenuStrip = new ContextMenuStrip();
        }

        notifyIcon.ContextMenuStrip.Items.Add(menuItem);
        UpdateStyle();
        return menuItem;
    }

    public ToolStripItem AddMenu(string text)
    {
        if (text == "-")
        {
            ToolStripSeparator item = new();
            notifyIcon.ContextMenuStrip.Items.Add(item);
            return item;
        }
        return AddMenu(new ToolStripMenuItem(text));
    }

    public ToolStripItem AddMenu(string text, bool check)
    {
        return AddMenu(new ToolStripMenuItem(text) { Checked = check });
    }

    public ToolStripItem AddMenu(string text, bool check, EventHandler onClick)
    {
        return AddMenu(new ToolStripMenuItem(text, null, onClick) { Checked = check });
    }

    public ToolStripItem AddMenu(string text, EventHandler onClick)
    {
        return AddMenu(new ToolStripMenuItem(text, null, onClick));
    }

    public ToolStripItem AddMenu(string text, Image image, EventHandler onClick)
    {
        return AddMenu(new ToolStripMenuItem(text, image, onClick));
    }

    public ToolStripItem AddMenu(string text, Image image, EventHandler onClick, string name)
    {
        return AddMenu(new ToolStripMenuItem(text, image, onClick, name));
    }

    public ToolStripItem AddMenu(string text, Image image, params ToolStripItem[] dropDownItems)
    {
        return AddMenu(new ToolStripMenuItem(text, image, dropDownItems));
    }

    public ToolStripItem AddMenu(string text, Image image, EventHandler onClick, Keys shortcutKeys)
    {
        return AddMenu(new ToolStripMenuItem(text, image, onClick, shortcutKeys));
    }

    private void ProcessContextMenuStrip()
    {
        if (notifyIcon.ContextMenuStrip == null)
        {
            return;
        }
        SetContextMenuRoundedCorner(notifyIcon.ContextMenuStrip.Handle);
        notifyIcon.ContextMenuStrip.Renderer = toolStripRenderer;
        notifyIcon.ContextMenuStrip.HandleCreated += ContextMenuStrip_HandleCreated;
    }

    public event EventHandler BalloonTipClicked
    {
        add => Events.AddHandler(EVENT_BALLOONTIPCLICKED, value);
        remove => Events.RemoveHandler(EVENT_BALLOONTIPCLICKED, value);
    }

    public event EventHandler Disposed
    {
        add => Events.AddHandler(EVENT_DISPOSED, value);
        remove => Events.RemoveHandler(EVENT_DISPOSED, value);
    }

    public event EventHandler BalloonTipClosed
    {
        add => Events.AddHandler(EVENT_BALLOONTIPCLOSED, value);
        remove => Events.RemoveHandler(EVENT_BALLOONTIPCLOSED, value);
    }

    public event EventHandler BalloonTipShown
    {
        add => Events.AddHandler(EVENT_BALLOONTIPSHOWN, value);
        remove => Events.RemoveHandler(EVENT_BALLOONTIPSHOWN, value);
    }

    public event EventHandler Click
    {
        add => Events.AddHandler(EVENT_CLICK, value);
        remove => Events.RemoveHandler(EVENT_CLICK, value);
    }

    public event EventHandler DoubleClick
    {
        add => Events.AddHandler(EVENT_DOUBLECLICK, value);
        remove => Events.RemoveHandler(EVENT_DOUBLECLICK, value);
    }

    public event MouseEventHandler MouseClick
    {
        add => Events.AddHandler(EVENT_MOUSECLICK, value);
        remove => Events.RemoveHandler(EVENT_MOUSECLICK, value);
    }

    public event MouseEventHandler MouseDoubleClick
    {
        add => Events.AddHandler(EVENT_MOUSEDOUBLECLICK, value);
        remove => Events.RemoveHandler(EVENT_MOUSEDOUBLECLICK, value);
    }

    public event MouseEventHandler MouseDown
    {
        add => Events.AddHandler(EVENT_MOUSEDOWN, value);
        remove => Events.RemoveHandler(EVENT_MOUSEDOWN, value);
    }

    public event MouseEventHandler MouseMove
    {
        add => Events.AddHandler(EVENT_MOUSEMOVE, value);
        remove => Events.RemoveHandler(EVENT_MOUSEMOVE, value);
    }

    public event MouseEventHandler MouseUp
    {
        add => Events.AddHandler(EVENT_MOUSEUP, value);
        remove => Events.RemoveHandler(EVENT_MOUSEUP, value);
    }

    public bool Visible
    {
        get => notifyIcon.Visible;
        set => notifyIcon.Visible = value;
    }

    public string Text
    {
        get => notifyIcon.Text;
        set => notifyIcon.Text = value;
    }

    public object Tag
    {
        get => notifyIcon.Tag;
        set => notifyIcon.Tag = value;
    }

    public ISite Site
    {
        get => notifyIcon.Site;
        set => notifyIcon.Site = value;
    }

    public Icon Icon
    {
        get => notifyIcon.Icon;
        set => notifyIcon.Icon = value;
    }

    public ContextMenuStrip ContextMenuStrip
    {
        get => notifyIcon.ContextMenuStrip;
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
        get => notifyIcon.BalloonTipIcon;
        set => notifyIcon.BalloonTipIcon = value;
    }

    public IContainer Container => notifyIcon.Container;

    public string BalloonTipTitle
    {
        get => notifyIcon.BalloonTipTitle;
        set => notifyIcon.BalloonTipTitle = value;
    }

    public string BalloonTipText
    {
        get => notifyIcon.BalloonTipText;
        set => notifyIcon.BalloonTipText = value;
    }

    public void Dispose()
    {
        notifyIcon?.Dispose();
    }

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
        if (notifyIcon.ContextMenuStrip == null)
        {
            return;
        }

        for (int i = default; i < notifyIcon.ContextMenuStrip.Items.Count; i++)
        {
            var item = notifyIcon.ContextMenuStrip.Items[i];

            if (i == default || i == notifyIcon.ContextMenuStrip.Items.Count - 1)
            {
                if (i == default && i == notifyIcon.ContextMenuStrip.Items.Count - 1)
                {
                    item.Margin = new Padding(0, 2, 0, 2);
                }
                else if (i == default)
                {
                    item.Margin = new Padding(0, 2, 0, 0);
                }
                else if (i == notifyIcon.ContextMenuStrip.Items.Count - 1)
                {
                    item.Margin = new Padding(0, 0, 0, 2);
                }
            }
            else
            {
                item.Margin = new Padding(0);
            }
        }

        bool dark = ThemeListener.IsDarkMode;
        Color backColor = dark ? Color.FromArgb(0x2B, 0x2B, 0x2B) : Color.FromArgb(0xF2, 0xF2, 0xF2);
        Color foreColor = dark ? Color.FromArgb(0x99, 0xFF, 0xFF, 0xFF) : Color.FromArgb(0x99, 0x00, 0x00, 0x00);
        notifyIcon.ContextMenuStrip.BackColor = backColor;
        notifyIcon.ContextMenuStrip.ForeColor = foreColor;
        notifyIcon.ContextMenuStrip.Invalidate();
    }

    private static void SetContextMenuRoundedCorner(nint handle)
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

    [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern long DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute);
}
