using System;
using System.Windows.Forms;

namespace PoE2TradeOverlay
{
    public class WindowManager
    {
        private const int WS_EX_TRANSPARENT = 0x20;
        private readonly Form _form;
        private bool _isClickThrough;

        public WindowManager(Form form)
        {
            _form = form;
        }

        public void SetClickThrough(bool enable)
        {
            if (_isClickThrough == enable) return;

            _isClickThrough = enable;
            var exStyle = NativeMethods.GetWindowLong(_form.Handle, Constants.GWL_EXSTYLE);

            if (enable)
                exStyle |= WS_EX_TRANSPARENT;
            else
                exStyle &= ~WS_EX_TRANSPARENT;

            NativeMethods.SetWindowLong(_form.Handle, Constants.GWL_EXSTYLE, exStyle);
        }
    }
}