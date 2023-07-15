using System.Diagnostics;

namespace AutoShutDown.Backend
{
    public class Processes : Trigger
    {
        private readonly Settings _settings;

        public Processes(Settings settings)
        {
            _settings = settings;
        }

        public override string Status
        {
            get
            {
                return $"💻 Conditions met: {ConditionsMet} | The following processes may not be present :'{string.Join(',', _settings.LongRunningProcesses)}'";
            }
        }

        public override bool ConditionsMet
        {
            get
            {
                return !LongRunningProcessesFound();
            }
        }

        public static List<string> GetRunningProcesses()
        {
            return Process.GetProcesses().Select(q => q.ProcessName).Distinct().ToList();
        }

        public bool LongRunningProcessesFound()
        {
            if (_settings.LongRunningProcesses.Length == 0) return false;
            return GetRunningProcesses().Any(q => _settings.LongRunningProcesses.Contains(q));
        }

        public override void ShutDown()
        {
            
        }
    }
}