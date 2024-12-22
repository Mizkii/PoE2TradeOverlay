using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace PoE2TradeOverlay.Controls
{
    public class TitleBar : Panel
    {
        public event EventHandler OnRefresh;

        public TitleBar(int width)
        {
            Dock = DockStyle.Top;
            Height = 30;
            BackColor = Color.FromArgb(200, 40, 40, 40);
            InitializeComponents(width);
            SetupDragging();
        }

        private void InitializeComponents(int width)
        {
            var titleLabel = new Label
            {
                Text = "PoE2 Trade",
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(10, 0),
                Size = new Size(200, 30)
            };

            var closeButton = new OverlayButton("×", width - 30);
            var minimizeButton = new OverlayButton("−", width - 60);
            var refreshButton = new OverlayButton("↻", width - 90);

            closeButton.Click += (s, e) => FindForm()?.Hide();
            minimizeButton.Click += (s, e) =>
            {
                var form = FindForm();
                if (form != null)
                    form.WindowState = FormWindowState.Minimized;
            };
            refreshButton.Click += (s, e) => OnRefresh?.Invoke(this, EventArgs.Empty);
            closeButton.Click += (s, e) => OnClose?.Invoke(this, EventArgs.Empty);

            var hotkeySelector = new HotkeySelector { Location = new Point(width - 330, 2) };
            hotkeySelector.HotkeySelected += (s, key) => OnHotkeyChanged?.Invoke(this, key);


            Controls.AddRange(new Control[]
            {
                titleLabel,
                closeButton,
                minimizeButton,
                refreshButton
            });

            Controls.Add(hotkeySelector);


        }

        public event EventHandler OnClose;
        public event EventHandler<Keys> OnHotkeyChanged;


        private void SetupDragging()
        {
            MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    NativeMethods.ReleaseCapture();
                    NativeMethods.SendMessage(
                        FindForm().Handle,
                        NativeMethods.WM_NCLBUTTONDOWN,
                        NativeMethods.HT_CAPTION,
                        0
                    );
                }
            };
        }
    }
}