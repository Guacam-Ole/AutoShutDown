using AutoShutDown.Backend;
using AutoShutDown.UI.Properties;


using System.Diagnostics;
using Serilog;
using System.Diagnostics.Contracts;

namespace AutoShutDown.UI
{
    public class BackgroundContext : ApplicationContext
    {
        private NotifyIcon _trayIcon;
        private WatchDogForm _watchDogForm;

        public BackgroundContext()
        {
            _trayIcon = new NotifyIcon
            {
                Icon = Resources.AppIcon,

                ContextMenuStrip = new ContextMenuStrip
                {
                    Items = {
                        { new ToolStripMenuItem("Change Settings", null, ShowSettings) },
                        { new ToolStripMenuItem("Show Status Overlay", null, ShowOverlay) },
                        { new ToolStripMenuItem("Open project on Github", null, ShowGithub) },
                        { new ToolStripSeparator() },
                        { new ToolStripMenuItem("Exit", null, Exit) },
                    },
                    ShowImageMargin = false,
                    ShowCheckMargin=true
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
            _watchDogForm = new WatchDogForm(settings);
            _watchDogForm.Show();
        }

    

        private void Exit(object? sender, EventArgs e)
        {
            _trayIcon.Dispose();
            Log.Information("Application shut down manually");
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


        private void ShowOverlay(object? sender, EventArgs e)
        {
            if (sender == null) return;
            var toolstrip = (ToolStripMenuItem)sender;
            toolstrip.Checked = !toolstrip.Checked;

            if (_watchDogForm != null)
            {
                if (toolstrip.Checked)
                {
                    _watchDogForm.EnableOverlay();
                }
                else
                {
                    _watchDogForm.DisableOverlay();
                }
            }
        }
    }
}