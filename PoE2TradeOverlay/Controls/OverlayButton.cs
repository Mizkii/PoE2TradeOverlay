using System.Drawing;
using System.Windows.Forms;

namespace PoE2TradeOverlay.Controls
{
    public class OverlayButton : Button
    {
        public OverlayButton(string text, int xPosition)
        {
            Text = text;
            Size = new Size(25, 25);
            Location = new Point(xPosition, 2);
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Cursor = Cursors.Hand;
            FlatAppearance.BorderSize = 0;
        }
    }
}