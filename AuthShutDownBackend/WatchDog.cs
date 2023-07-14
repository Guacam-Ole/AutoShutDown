using AutoShutDown.Backend;

using Newtonsoft.Json;

using Serilog;

namespace AuthShutDown.Backend
{
    public class WatchDog
    {
        private readonly Settings _settings;
        private readonly List<Trigger> _triggers = new();

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
            foreach (var trigger in _triggers)
            {
                Log.Debug($"{trigger.GetType().Name}.ConditionsMet: {trigger.ConditionsMet}");
            }

            if (_triggers.Any(q => !q.ConditionsMet)) return;
            if (_settings.WarningSecondsBeforeShutdown > 0)
            {
                // Todo send Event for warning
                WarningEvent?.Invoke(this, new EventArgs());
                var _ = new Timer(ShutDownForReal, null, TimeSpan.Zero, TimeSpan.FromSeconds(_settings.WarningSecondsBeforeShutdown));
            }
            else
            {
                Execute.RunCommand(_settings);
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