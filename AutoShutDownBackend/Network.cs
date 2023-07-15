using Serilog;

using System.Net.NetworkInformation;

namespace AutoShutDown.Backend
{
    public class Network : Trigger
    {
        private long _bytesReceivedTotal = 0;
        private readonly Timer _minuteTimer;
        private long _avgDiffPerSecond = long.MaxValue; 
        private readonly Settings _settings;

        public override bool ConditionsMet
        {
            get { return _avgDiffPerSecond < _settings.MinBytesReceived; }
        }

        public Network(Settings settings)
        {
            _bytesReceivedTotal = GetReceivedBytesFromAllInterfaces();
            _minuteTimer = new Timer(MinuteDownloadCount, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            _settings = settings;
        }

        public void MinuteDownloadCount(object? state)
        {
            long newBytesReceiveTotald = GetReceivedBytesFromAllInterfaces();
            _avgDiffPerSecond = (newBytesReceiveTotald - _bytesReceivedTotal) / 60;
            Log.Debug($"Avg/s over 60s: {_avgDiffPerSecond.Fancy()}");
            _bytesReceivedTotal = newBytesReceiveTotald;
        }

        private static long GetReceivedBytesFromAllInterfaces()
        {
            long bytesReceived = 0;
            foreach (var networkinterface in NetworkInterface.GetAllNetworkInterfaces()) bytesReceived += networkinterface.GetIPStatistics().BytesReceived;
            return bytesReceived;
        }

        public override void ShutDown()
        {
            _minuteTimer.Dispose(); 
        }
    }
}