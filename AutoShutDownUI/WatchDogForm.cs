using AutoShutDown.Backend;

using Serilog;

namespace AutoShutDown.UI
{
    public partial class WatchDogForm : Form
    {
        private readonly Backend.Settings _settings;

        public WatchDogForm()
        {
            InitializeComponent();
        }

        public WatchDogForm(Backend.Settings settings) : this()
        {
            _settings = settings;
        }

        private async void WatchDogForm_Load(object sender, EventArgs e)
        {
            var watchDog = new WatchDog(_settings);
            watchDog.WarningEvent += WatchDog_WarningEvent;
            Log.Information("Watchdog started from UI application");
            await watchDog.RunWatchDog();
        }

        private void WatchDog_WarningEvent(object? sender, EventArgs e)
        {
            MessageBox .Show(new Form { TopMost = true }, "Computer will shutdown soon! You can close Autoshutdown through the icon in the system tray to prevent this.", "Autoshutdown");
        }
    }
}