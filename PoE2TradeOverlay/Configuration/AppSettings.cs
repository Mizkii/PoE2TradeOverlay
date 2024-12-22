using System.Text.Json;

namespace PoE2TradeOverlay.Configuration
{
    public class AppSettings
    {
        public Keys Hotkey { get; set; } = Keys.F1;
        public Point WindowLocation { get; set; }
        public Size WindowSize { get; set; }

        private const string CONFIG_FILE = "config.json";

        public static AppSettings Load()
        {
            try
            {
                return File.Exists(CONFIG_FILE)
                    ? JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(CONFIG_FILE))
                    : new AppSettings();
            }
            catch
            {
                return new AppSettings();
            }
        }

        public void Save()
        {
            File.WriteAllText(CONFIG_FILE, JsonSerializer.Serialize(this));
        }
    }
}