using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AutoShutDown
{
    public class Network : Trigger
    {
        private long _bytesReceivedTotal = 0;
        public Network(Settings settings) : base(settings)
        {
        }

        public override void Init()
        {
            throw new NotImplementedException();
        }


        private void StartDownloadTrigger()
        {
            _bytesReceivedTotal = GetReceivedBytesFromAllInterfaces();
            TriggerTimer = new Timer(TimerExpired, null, 60000, 60000);
        }

        public override void TimerExpired(object? state)
        {
            if (LongRunningProcessesFound()) return;

            long newBytesReceiveTotald = GetReceivedBytesFromAllInterfaces();
            var avgDiffPerSecond = (newBytesReceiveTotald - _bytesReceivedTotal) / 60;
            if (Settings.ConsoleLog) Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Avg. Rate/s over 60s: {avgDiffPerSecond.Fancy()}");
            _bytesReceivedTotal = newBytesReceiveTotald;
            if (avgDiffPerSecond< Settings.MinBytesReceived) Execute.RunCommand(Settings);
        }

        private static long GetReceivedBytesFromAllInterfaces()
        {
            long bytesReceived = 0;
            foreach (var networkinterface in NetworkInterface.GetAllNetworkInterfaces()) bytesReceived += networkinterface.GetIPStatistics().BytesReceived;
            return bytesReceived;
        }
    }
}
