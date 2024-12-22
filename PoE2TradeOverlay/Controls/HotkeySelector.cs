using System;
using System.Windows.Forms;

namespace PoE2TradeOverlay.Controls
{
    public class HotkeySelector : TextBox
    {
        public event EventHandler<Keys> HotkeySelected;
        private Keys _currentKey;

        public HotkeySelector()
        {
            ReadOnly = true;
            Width = 100;
            Text = Constants.TOGGLE_HOTKEY.ToString();

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            _currentKey = e.KeyCode;
            Text = _currentKey.ToString();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            HotkeySelected?.Invoke(this, _currentKey);
        }
    }
}