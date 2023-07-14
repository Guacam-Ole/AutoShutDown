namespace AutoShutDown.Backend
{
    public class Settings
    {
        public long MinBytesReceived { get; set; }
        public string ExecuteCommand { get; set; } = "shutdown";
        public string ExecuteParameters { get; set; } = "/s /t 10 /c autoshutdown";
        public string[] LongRunningProcesses { get; set; } = Array.Empty<string>();
        public int MouseMoveMinutes { get; set; }
        public int WarningSecondsBeforeShutdown { get; set; } = 30;

        public bool OnlyBeep
        { get { return ExecuteCommand == "beep"; } }

        public Settings()
        { }
    }
}