using AutoShutDown.Backend;

namespace AutoShutDown.UI
{
    public partial class ProcessList : Form
    {
        public List<string> SelectedProcesses = new();
        public ProcessList()
        {
            InitializeComponent();
        }

        private void ProcessList_Load(object sender, EventArgs e)
        {
            ProcessesList.Items.AddRange(Processes.GetRunningProcesses().OrderBy(q => q).Select(q => new ListViewItem(q)).ToArray());
        }

        private void ProcessesList_DoubleClick(object sender, EventArgs e)
        {
            var view = (ListView)sender;
            foreach (var item in view.SelectedItems)
            {
                SelectedProcesses.Add(((ListViewItem)item).Text);
            }
            this.Close();
        }
    }
}