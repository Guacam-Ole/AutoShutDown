using AutoShutDown.UI.Properties;

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
                        { new ToolStripMenuItem("Display Current Status", null, ShowSettings) },
                        { new ToolStripSeparator() },

                        { new ToolStripMenuItem("Exit", null, Exit) },
                    },
                    ShowImageMargin = true
                },
                Text = "Autoshutdown is running",
                Visible = true
            };
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

        private void ShowStatus(object? sender, EventArgs e)
        {
            new Settings().ShowDialog();
        }
    }
}