using AutoShutDown.UI;

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
    }
}