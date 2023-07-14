using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShutDown.Backend
{
    public abstract class Trigger
    {
        protected Settings Settings { get; set; }  
        public  Timer TriggerTimer { get; set; } 

        public Trigger(Settings settings)
        {
            Settings = settings;
        }

        public abstract void Init();
        public abstract void TimerExpired(object? state);

        public bool LongRunningProcessesFound()
        {
            if (Settings.LongRunningProcesses.Length == 0) return false;
            return Process.GetProcesses().Any(q => Settings.LongRunningProcesses.Contains(q.ProcessName)); 
        }
    }
}
