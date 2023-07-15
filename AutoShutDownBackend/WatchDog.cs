using Newtonsoft.Json;

using Serilog;

namespace AutoShutDown.Backend
{
    public class WatchDog
    {
        private readonly Settings _settings;
        public readonly List<Trigger> _triggers = new();
        private bool _conditionsMet = false;

        public event EventHandler? WarningEvent;

        public WatchDog(Settings settings)
        {
            _settings = settings;
            if (_settings.MouseMoveMinutes > 0) _triggers.Add(new Mouse(_settings));
            if (_settings.MinBytesReceived > 0) _triggers.Add(new Network(_settings));
            if (_settings.LongRunningProcesses.Length > 0) _triggers.Add(new Processes(_settings));
        }

        public async Task RunWatchDog()
        {
            Log.Debug($"Watchdog started. Configuration:\n {JsonConvert.SerializeObject(_settings, Formatting.Indented)}");
            var minuteTimer = new Timer(MinuteTimerElapsed, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            while (true) await Task.Delay(1000);
        }

        private void ShutdownIfConditionsMet()
        {
            try
            {
                if (_conditionsMet) return;
                foreach (var trigger in _triggers)
                {
                    Log.Debug($"{trigger.GetType().Name}.ConditionsMet: {trigger.ConditionsMet}");
                }

                if (_triggers.Any(q => !q.ConditionsMet)) return;
                _conditionsMet = true;
                _triggers.ForEach(q => q.ShutDown());
                _triggers.Clear();
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
            ShutdownIfConditionsMet();
        }
    }
}