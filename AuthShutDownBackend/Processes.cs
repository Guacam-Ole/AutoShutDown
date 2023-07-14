using AutoShutDown.Backend;

using System.Diagnostics;

namespace AuthShutDown.Backend
{
    public class Processes:Trigger
    {
        private readonly Settings _settings;

        public Processes(Settings settings)
        {
            _settings = settings;
        }

        public override bool ConditionsMet
        {
            get
            {
                return !LongRunningProcessesFound();
            }
        }

        public bool LongRunningProcessesFound()
        {
            if (_settings.LongRunningProcesses.Length == 0) return false;
            return Process.GetProcesses().Any(q => _settings.LongRunningProcesses.Contains(q.ProcessName));
        }
    }
}