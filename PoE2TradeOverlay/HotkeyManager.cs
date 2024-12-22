using System;
using System.Windows.Forms;

namespace PoE2TradeOverlay
{
    public class HotkeyManager : IDisposable
    {
        private readonly IntPtr _handle;
        private Keys _currentHotkey;

        public event EventHandler<Keys> HotkeyChanged;

        public HotkeyManager(IntPtr handle)
        {
            _handle = handle;
            SetHotkey(Constants.TOGGLE_HOTKEY);
        }

        public void SetHotkey(Keys key)
        {
            UnregisterCurrentHotkey();
            _currentHotkey = key;
            NativeMethods.RegisterHotKey(_handle, Constants.TOGGLE_HOTKEY_ID, 0, key);
            HotkeyChanged?.Invoke(this, key);
        }

        private void UnregisterCurrentHotkey()
        {
            if (_currentHotkey != Keys.None)
            {
                NativeMethods.UnregisterHotKey(_handle, Constants.TOGGLE_HOTKEY_ID);
            }
        }

        public void Dispose()
        {
            UnregisterCurrentHotkey();
        }
    }
}