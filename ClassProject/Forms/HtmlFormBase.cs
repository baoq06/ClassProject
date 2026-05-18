using Microsoft.Web.WebView2.WinForms;
using System.Text.Json;

namespace ClassProject.Forms
{
    public class HtmlFormBase : Form
    {
        protected WebView2 webView;
        protected string htmlPath;

        public HtmlFormBase(string title, string htmlFilePath, int width = 900, int height = 650)
        {
            Text = title;
            Size = new Size(width, height);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            htmlPath = htmlFilePath;

            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            webView = new WebView2
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(webView);

            await webView.EnsureCoreWebView2Async(null);

            string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string fullPath = Path.Combine(projectDir, htmlPath.Replace('/', '\\'));

            if (!File.Exists(fullPath))
            {
                webView.CoreWebView2.NavigateToString(
                    $"<html><body style='font-family:sans-serif;text-align:center;padding-top:100px;color:red'>" +
                    $"<h2>UI file not found</h2><p>{fullPath}</p></body></html>");
                return;
            }

            webView.CoreWebView2.Navigate(new Uri(fullPath).AbsoluteUri);

            RegisterBridge();
        }

        private void RegisterBridge()
        {
            webView.CoreWebView2.WebMessageReceived += (s, e) =>
            {
                try
                {
                    string json = e.TryGetWebMessageAsString();
                    if (string.IsNullOrEmpty(json)) return;

                    var msg = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
                    if (msg == null || !msg.ContainsKey("action")) return;

                    string action = msg["action"].GetString() ?? "";
                    HandleAction(action, msg);
                }
                catch { }
            };
        }

        protected virtual void HandleAction(string action, Dictionary<string, JsonElement> data)
        {
        }

        protected async void SendToJs(string functionName, params object[] args)
        {
            try
            {
                string json = JsonSerializer.Serialize(args);
                await webView.CoreWebView2.ExecuteScriptAsync($"{functionName}(...{json})");
            }
            catch { }
        }

        protected async void CallJs(string script)
        {
            try
            {
                await webView.CoreWebView2.ExecuteScriptAsync(script);
            }
            catch { }
        }

        protected async void ShowMessage(string message, string type = "info")
        {
            await webView.CoreWebView2.ExecuteScriptAsync(
                $"showToast('{EscapeJs(message)}', '{type}')");
        }

        protected static string EscapeJs(string s)
        {
            return s.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "");
        }
    }
}
