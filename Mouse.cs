using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoShutDown
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
            TriggerTimer = new Timer(TimerExpired, null, Settings.MouseMoveMinutes * 30000, Settings.MouseMoveMinutes * 30000);
        }

        public override void TimerExpired(object? state)
        {
            if (LongRunningProcessesFound()) return;
            if (!MouseMoved()) return;
            TriggerTimer.Change(-1, -1);    // disable timer

            if (Settings.MinBytesReceived>0)
            {
                _network.Init();
            } else
            {
                Execute.RunCommand(Settings);
            }
        }


        private bool MouseMoved()
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
