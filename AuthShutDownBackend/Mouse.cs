using System.Drawing;
using System.Runtime.InteropServices;

namespace AutoShutDown.Backend
{
    public class Mouse : Trigger
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point lpPoint);

        private readonly Network _network;
        private int _mouseIdleCount = 0;
        private Point _lastMousePosition;

        public Mouse(Settings settings, Network network) : base(settings)
        {
            _network = network;
        }

        public override void Init()
        {
            Execute.Log("Init Mouse");
            TriggerTimer = new Timer(TimerExpired, null, Settings.MouseMoveMinutes * 30000, Settings.MouseMoveMinutes * 30000);
            var minuteTimer = new Timer(MinuteTimerExpired, null, TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(10));
        }

        private void MinuteTimerExpired(object? state)
        {
            var currentMousePosition = GetCurrentMousePosition();
            if (currentMousePosition != _lastMousePosition)
            {
                _mouseIdleCount = 0;
            }
            if (Settings.ConsoleLog) Execute.Log($"10min Cursor:{currentMousePosition} IdleCount:{_mouseIdleCount}");
        }

        public override void TimerExpired(object? state)
        {
            if (LongRunningProcessesFound())
            {
                Execute.Log("Long running process found");
                return;
            }
            if (!MouseStuck())
            {
                Execute.Log("Mouse did move");
                return;
            }
            TriggerTimer.Change(-1, -1);    // disable timer

            Execute.Log("No Mousemovement detected");
            if (Settings.MinBytesReceived > 0)
            {
                _network.Init();
            }
            else
            {
                Execute.RunCommand(Settings);
            }
        }

        private bool MouseStuck()
        {
            var currentMousePosition = GetCurrentMousePosition();
            _mouseIdleCount = currentMousePosition == _lastMousePosition ? _mouseIdleCount + 1 : 0;
            _lastMousePosition = currentMousePosition;
            if (Settings.ConsoleLog) Execute.Log($"Cursor:{currentMousePosition} IdleCount:{_mouseIdleCount}");
            return _mouseIdleCount >= 2;
        }

        private static Point GetCurrentMousePosition()
        {
            var currentMousePosition = new Point();
            GetCursorPos(ref currentMousePosition);
            return currentMousePosition;
        }
    }
}