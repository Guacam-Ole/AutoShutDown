using Newtonsoft.Json;

using Serilog;

namespace AutoShutDown.Backend
{
    public class WatchDog
    {
        private readonly Settings _settings;
        public readonly List<Trigger> Triggers = new();
        private bool _conditionsMet = false;
        private Timer _minuteTimer;

        public event EventHandler? WarningEvent;

        public event EventHandler? UpdateStatusEvent;

        public WatchDog(Settings settings)
        {
            _settings = settings;
            if (_settings.MouseMoveMinutes > 0) Triggers.Add(new Mouse(_settings));
            if (_settings.MinBytesReceived > 0) Triggers.Add(new Network(_settings));
            if (_settings.LongRunningProcesses.Length > 0) Triggers.Add(new Processes(_settings));
        }

        public async Task RunWatchDog()
        {
            Log.Debug($"Watchdog started. Configuration:\n {JsonConvert.SerializeObject(_settings, Formatting.Indented)}");
            _minuteTimer = new Timer(MinuteTimerElapsed, null, TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(1));
        }

        private void ShutdownIfConditionsMet()
        {
            try
            {
                if (UpdateStatusEvent != null)
                {
                    Task.Run(() => UpdateStatusEvent(this, new EventArgs()));
                }
                if (_conditionsMet) return;
                foreach (var trigger in Triggers)
                {
                    Log.Debug($"{trigger.GetType().Name}.ConditionsMet: {trigger.ConditionsMet}");
                }

                if (Triggers.Any(q => !q.ConditionsMet)) return;
                _conditionsMet = true;
                Triggers.ForEach(q => q.ShutDown());
                Triggers.Clear();
                Log.Information("All Conditions met");
                if (_settings.WarningSecondsBeforeShutdown > 0)
                {
                    Log.Debug("Showing Warning");
                    // Todo send Event for warning
                    if (WarningEvent != null)
                    {
                        Task.Run(() => WarningEvent(this, new EventArgs()));
                    }
                    var _ = new Timer(ShutDownForReal, null, TimeSpan.FromSeconds(_settings.WarningSecondsBeforeShutdown), Timeout.InfiniteTimeSpan);
                }
                else
                {
                    Execute.RunCommand(_settings);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error on Conditioncheck");
            }
        }

        private void ShutDownForReal(object? state)
        {
            Execute.RunCommand(_settings);
        }

        private void MinuteTimerElapsed(object? state)
        {
            Log.Debug("minutetimer alive");
            ShutdownIfConditionsMet();
        }
    }
}