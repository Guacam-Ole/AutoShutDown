﻿using AutoShutDown.Backend;

using Serilog;

using Timer = System.Threading.Timer;

namespace AutoShutDown.UI
{
    public partial class WatchDogForm : Form
    {
        private readonly Backend.Settings _settings;
        private Overlay _overlay;
        private WatchDog _watchDog;

        public WatchDogForm()
        {
            InitializeComponent();
            _overlay = new Overlay();   
        }

        public WatchDogForm(Backend.Settings settings) : this()
        {
            _settings = settings;
        }

        private async void WatchDogForm_Load(object sender, EventArgs e)
        {
            _watchDog = new WatchDog(_settings);
            _watchDog.WarningEvent += WatchDog_WarningEvent;
            _watchDog.UpdateStatusEvent += WatchDog_UpdateStatusEvent;
            _watchDog.RunWatchDog().Wait();
        }


        private void WatchDog_UpdateStatusEvent(object? sender, EventArgs e)
        {
            if (sender==null || !_overlay.Visible) return;
            var watchdog = (WatchDog)sender;
            string overlayMessage = "AutoShutDown:\r\n\r\n";
            foreach     (var trigger in watchdog.Triggers)
            {
                overlayMessage += $"{trigger.Status}\r\n";
            }
            _overlay.UpdateText(overlayMessage);
        }

        private void WatchDog_WarningEvent(object? sender, EventArgs e)
        {
            string message = "Computer will shutdown soon! You can close Autoshutdown through the icon in the system tray to prevent this.";
            if (_overlay.Visible)
            {
                _overlay.UpdateText(message);
            }
            MessageBox .Show(new Form { TopMost = true }, "Computer will shutdown soon! You can close Autoshutdown through the icon in the system tray to prevent this.", "Autoshutdown");
        }


        public void EnableOverlay()
        {
            _overlay.Show();
        }

        public void DisableOverlay() { 
        _overlay.Hide();
        }
    }
}