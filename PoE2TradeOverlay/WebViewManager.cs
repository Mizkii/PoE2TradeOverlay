using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace PoE2TradeOverlay
{
    public class WebViewManager : IDisposable
    {
        private readonly WebView2 webView;

        public WebViewManager(Control parent)
        {
            webView = new WebView2() { Dock = DockStyle.Fill };
            InitializeWebView(parent);
        }

        private async void InitializeWebView(Control parent)
        {
            var webView2Environment = await CoreWebView2Environment
                .CreateAsync(null, "WebView2Cache");
            await webView.EnsureCoreWebView2Async(webView2Environment);

            ConfigureWebView();
            webView.CoreWebView2.Navigate(Constants.TRADE_URL);

            webView.CoreWebView2.DOMContentLoaded += InjectCustomStyles;

            parent.Controls.Add(webView);
            webView.BringToFront();
        }

        private void ConfigureWebView()
        {
            webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
            webView.CoreWebView2.Settings.IsStatusBarEnabled = false;
        }

        private async void InjectCustomStyles(object sender, EventArgs e)
        {
            await webView.CoreWebView2.ExecuteScriptAsync(@"
                var style = document.createElement('style');
                style.textContent = `
                    body { 
                        background: rgba(30, 30, 30, 0.95) !important;
                    }
                `;
                document.head.appendChild(style);
            ");
        }

        public async void Refresh()
        {
            if (webView?.CoreWebView2 != null)
            {
                await webView.CoreWebView2.ExecuteScriptAsync("window.location.reload();");
            }
        }

        public async void Dispose()
        {
            if (webView?.CoreWebView2 != null)
            {
                try
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("window.close();");
                    webView.Dispose();
                    Environment.Exit(0);
                }
                catch { }
            }
        }

        public async void SetViewport(int width, int height)
        {
            if (webView?.CoreWebView2 != null)
            {
                await webView.CoreWebView2.ExecuteScriptAsync($@"
            document.querySelector('meta[name=""viewport""]')?.remove();
            const viewport = document.createElement('meta');
            viewport.name = 'viewport';
            viewport.content = 'width={width}, height={height}, initial-scale=1';
            document.head.appendChild(viewport);
            document.body.style.width = '{width}px';
            document.body.style.height = '{height}px';
        ");
            }
        }
    }
}