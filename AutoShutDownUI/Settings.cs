using AutoShutDown.UI;

using Newtonsoft.Json;

namespace AutoShutDownUI
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void SelectTasks_Click(object sender, EventArgs e)
        {
            var processesForm = new ProcessList();
            processesForm.ShowDialog();
            if (processesForm.SelectedProcesses.Count > 0)
            {
                foreach (var process in processesForm.SelectedProcesses)
                {
                    if (!TasksList.Text.Contains(process))
                    {
                        TasksList.Text += " " + process;
                        TasksList.Text = TasksList.Text.Trim();
                    }
                }
            }
        }

        private void ReadSettings()
        {
            var settings = JsonConvert.DeserializeObject<AutoShutDown.Backend.Settings>(File.ReadAllText("settings.json")) ?? new AutoShutDown.Backend.Settings();
            MouseMinuteSelect.Value = settings.MouseMoveMinutes;
            WarningSeconds.Value = settings.WarningSecondsBeforeShutdown;
            var multiIndex = 0;
            var bytes = settings.MinBytesReceived;
            if (bytes > 1024)
            {
                multiIndex = 1;
                bytes = bytes / 1024;
            }
            if (bytes > 1024)
            {
                multiIndex = 2;
                bytes = bytes / 1024;
            }
            DownloadRate.Value = bytes;
            DownloadMultiplyer.SelectedIndex = multiIndex;
            ShutdownCommand.Text = settings.ExecuteCommand + " " + settings.ExecuteParameters;
            TasksList.Text = string.Join(' ', settings.LongRunningProcesses);
        }

        private void WriteSettings()
        {
            var settings = new AutoShutDown.Backend.Settings();
            settings.MouseMoveMinutes = (int)MouseMinuteSelect.Value;
            settings.WarningSecondsBeforeShutdown = (int)WarningSeconds.Value;
            int multiplicator = ((MultiPlyer)DownloadMultiplyer.SelectedItem).Value;
            settings.MinBytesReceived = multiplicator * (int)DownloadRate.Value;
            var processList = TasksList.Text.Split(' ');
            var command=ShutdownCommand.Text.Trim();
            if (command.Contains(' '))
            {
                settings.ExecuteCommand=command[..command.IndexOf(" ")];
                settings.ExecuteParameters = command[command.IndexOf(" ")..].Trim();
            } else
            {
                settings.ExecuteCommand= command;
                settings.ExecuteParameters = string.Empty;
            }
            if (processList.Length > 0)
            {
                settings.LongRunningProcesses = processList;
            }
            File.WriteAllText("settings.json", JsonConvert.SerializeObject(settings, Formatting.Indented));
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            DownloadMultiplyer.Items.Clear();
            DownloadMultiplyer.Items.Add(new MultiPlyer { Value = 1, Text = "Bytes/s" });
            DownloadMultiplyer.Items.Add(new MultiPlyer { Value = 1024, Text = "KBytes/s" });
            DownloadMultiplyer.Items.Add(new MultiPlyer { Value = 1024 * 1024, Text = "MBytes/s" });
            DownloadMultiplyer.SelectedIndex = 0;

            ReadSettings();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            WriteSettings();
        }
    }
}