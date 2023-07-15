using Serilog;

using System.Drawing;
using System.Runtime.InteropServices;

namespace AutoShutDown.Backend
{
    public class Mouse : Trigger
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point lpPoint);

        private Point _lastMousePosition;
        private Timer _minuteTimer;
        private DateTime _lastMouseMovement;
        private readonly Settings _settings;

        public override bool ConditionsMet
        {
            get
            {
                return _lastMouseMovement.AddMinutes(_settings.MouseMoveMinutes) < DateTime.Now;
            }
        }

        public override string Status
        {
            get
            {
                return $"🐭 Conditions met: {ConditionsMet} | Mouse not moved since {(DateTime.Now-_lastMouseMovement).TotalMinutes:0.##} minutes. Minutes-Config: {_settings.MouseMoveMinutes}";
            }
        }

        public Mouse(Settings settings)
        {
            _minuteTimer = new Timer(MinuteTimerExpired, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            _lastMouseMovement = DateTime.Now;
            _settings = settings;
        }

        private void MinuteTimerExpired(object? state)
        {
            var currentMousePosition = GetCurrentMousePosition();
            if (currentMousePosition != _lastMousePosition)
            {
                _lastMouseMovement = DateTime.Now;
            }
            _lastMousePosition = currentMousePosition;
            Log.Debug(($"1min Cursor:{currentMousePosition} Last movement:{_lastMouseMovement}"));
        }

        private static Point GetCurrentMousePosition()
        {
            var currentMousePosition = new Point();
            GetCursorPos(ref currentMousePosition);
            return currentMousePosition;
        }

        public override void ShutDown()
        {
            _minuteTimer.Dispose();
            
        }

    }
}