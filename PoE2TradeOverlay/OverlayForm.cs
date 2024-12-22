using System;
using System.Drawing;
using System.Windows.Forms;
using PoE2TradeOverlay.Configuration;
using PoE2TradeOverlay.Controls;

namespace PoE2TradeOverlay
{
    public partial class OverlayForm : Form
    {
        private readonly WebViewManager webViewManager;
        private readonly TitleBar titleBar;
        private readonly HotkeyManager hotkeyManager;
        private readonly WindowManager windowManager;
        private readonly AppSettings settings;

        public OverlayForm()
        {
            settings = AppSettings.Load();
            InitializeComponent();

            windowManager = new WindowManager(this);
            hotkeyManager = new HotkeyManager(Handle);

            titleBar = new TitleBar(Width);
            titleBar.OnRefresh += (s, e) => webViewManager?.Refresh();
            titleBar.OnHotkeyChanged += (s, key) =>
            {
                hotkeyManager.SetHotkey(key);
                settings.Hotkey = key;
            };

            titleBar.OnClose += (s, e) => Application.Exit();

            Controls.Add(titleBar);
            titleBar = new TitleBar(Width);
            RegisterHotKey();

            webViewManager = new WebViewManager(this);

            Location = settings.WindowLocation;
            //Size = settings.WindowSize;
            if (settings.Hotkey != Keys.None)
                hotkeyManager.SetHotkey(settings.Hotkey);


        }

        private void SetInitialSize()
        {
            int width = (int)(Screen.PrimaryScreen.WorkingArea.Width * 0.75);
            int height = (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.80);
            Size = new Size(width, height);
            StartPosition = FormStartPosition.Manual;
            Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - height) / 2
            );
        }

        private void RegisterHotKey()
        {
            NativeMethods.RegisterHotKey(
                Handle,
                Constants.TOGGLE_HOTKEY_ID,
                0,
                Constants.TOGGLE_HOTKEY
            );
        }



        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_HOTKEY && m.WParam.ToInt32() == Constants.TOGGLE_HOTKEY_ID)
            {
                Visible = !Visible;
                windowManager.SetClickThrough(!Visible);
            }
            base.WndProc(ref m);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            settings.WindowLocation = Location;
            settings.WindowSize = Size;
            settings.Save();

            webViewManager?.Dispose();
            hotkeyManager?.Dispose();
            base.OnFormClosing(e);
            Application.Exit();
        }
    }
}