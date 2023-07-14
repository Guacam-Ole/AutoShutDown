using AutoShutDown.Backend;
using AutoShutDown.UI.Properties;


using System.Diagnostics;
using Serilog;

namespace AutoShutDown.UI
{
    public class BackgroundContext : ApplicationContext
    {
        private NotifyIcon _trayIcon;

        public BackgroundContext()
        {
            _trayIcon = new NotifyIcon
            {
                Icon = Resources.AppIcon,

                ContextMenuStrip = new ContextMenuStrip
                {
                    Items = {
                        { new ToolStripMenuItem("Change Settings", null, ShowSettings) },
                        { new ToolStripMenuItem("Show project on Github", null, ShowGithub) },
                        { new ToolStripSeparator() },
                        { new ToolStripMenuItem("Exit", null, Exit) },
                    },
                    ShowImageMargin = true
                },
                Text = "Autoshutdown is running",
                Visible = true
            };
            StartWatchDog();
        }

        private void StartWatchDog()
        {
            var settings = ConfigReader.ReadSettings();
            if (settings == null)
            {
                _trayIcon.BalloonTipTitle = "Configuration missing";
                _trayIcon.BalloonTipText = "The Configuration from Autoshutdown has not been set yet. Please check your Config and restart the program";
                _trayIcon.ShowBalloonTip(10000);
                Log.Information("Watchdog not started; Configuration missing");
                return;
            }

            _trayIcon.Text = $"Autoshutdown will shut down after {settings.MouseMoveMinutes} minutes if the mouse is not moved and downloads are below {settings.MinBytesReceived.Fancy()}/s ";

            new WatchDogForm(settings).Show();
            
        }

    

        private void Exit(object? sender, EventArgs e)
        {
            _trayIcon.Dispose();
            Application.Exit();
        }

        private void ShowSettings(object? sender, EventArgs e)
        {
            new Settings().ShowDialog();
        }

        private void ShowGithub(object? sender, EventArgs e)
        {
            Process.Start("explorer", "https://github.com/Guacam-Ole/AutoShutDown");
        }
    }
}