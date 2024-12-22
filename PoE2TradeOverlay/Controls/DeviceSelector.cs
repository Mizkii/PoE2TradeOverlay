using System;
using System.Windows.Forms;

namespace PoE2TradeOverlay.Controls
{
    public class DeviceSelector : ComboBox
    {
        public event EventHandler<(int width, int height)> ViewportChanged;

        public DeviceSelector()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            Items.AddRange(ViewportSettings.Devices.Keys.ToArray());
            Width = 120;
            SelectedIndexChanged += OnDeviceSelected;
        }

        private void OnDeviceSelected(object sender, EventArgs e)
        {
            if (SelectedItem != null && ViewportSettings.Devices.TryGetValue(SelectedItem.ToString(), out var dimensions))
            {
                ViewportChanged?.Invoke(this, dimensions);
            }
        }
    }
}