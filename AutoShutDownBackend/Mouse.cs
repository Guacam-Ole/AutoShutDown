﻿using Serilog;

using System.Drawing;
using System.Runtime.InteropServices;

namespace AutoShutDown.Backend
{
    public class Mouse : Trigger
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point lpPoint);

        private Point _lastMousePosition;
        private DateTime _lastMouseMovement;
        private readonly Settings _settings;

        public override bool ConditionsMet
        {
            get
            {
                return _lastMouseMovement.AddMinutes(_settings.MouseMoveMinutes) < DateTime.Now;
            }
        }

        public Mouse(Settings settings)
        {
            var minuteTimer = new Timer(MinuteTimerExpired, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
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
    }
}