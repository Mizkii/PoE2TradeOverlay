using System.Windows.Forms;

namespace PoE2TradeOverlay
{
    public static class Constants
    {
        public const string TRADE_URL = "https://www.pathofexile.com/trade2/search/poe2/Standard";
        public const int TOGGLE_HOTKEY_ID = 1;
        public const Keys TOGGLE_HOTKEY = Keys.F1;
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;
    }
    public static class ViewportSettings
    {
        public static readonly Dictionary<string, (int width, int height)> Devices = new()
    {
        { "iPhone 14", (390, 844) },
        { "iPhone 14 Pro", (393, 852) },
        { "iPhone 14 Pro Max", (430, 932) },
        { "iPhone 13", (390, 844) },
        { "iPhone SE", (375, 667) }
    };
    }
}