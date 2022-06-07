namespace AutoShutDown
{
    public class Settings
    {
        private const string _githubUrl = "https://github.com/OleAlbers/AutoShutDown";

        public long MinBytesReceived { get; set; }
        public string ExecuteCommand { get; set; } = "shutdown";
        public string ExecuteParameters { get; set; } = "/s /t 30 /c autoshutdown";
        public string[] LongRunningProcesses { get; set; } = Array.Empty<string>();
        public int MouseMoveMinutes { get; set; }
        public bool Beep
        { get { return ExecuteCommand == "beep"; } }
        public bool ConsoleLog { get; set; } = true;

        public Settings(string[] args)
        {
            if (args.Length == 0) { ShowParameterInfo(); return; }
            foreach (var arg in args)
            {
                if (!arg.Contains('=')) InvalidParameterPair(arg);
                var pair = arg.Split('=');
                switch (pair[0])
                {
                    case "/mouse":
                        MouseMoveMinutes = Convert.ToInt32(pair[1]);
                        break;

                    case "/down":
                        MinBytesReceived = pair[1].DownloadLimitValue();
                        break;

                    case "/processes":
                        LongRunningProcesses = pair[1].Split(',');
                        break;

                    case "/command":
                        ExecuteCommand = pair[1];
                        break;

                    case "/params":
                        ExecuteParameters = pair[1];
                        break;

                    case "/log":
                        ConsoleLog = pair[1] == "true";
                        break;
                }
            }

            Console.WriteLine($"Autoshutdown started. [Mousemove: {MouseMoveMinutes} minutes | Downloadlimit: {MinBytesReceived.Fancy()} | beep: {Beep} | command:'{ExecuteCommand}' | parameters: '{ExecuteParameters}' processes:'{string.Join(",", LongRunningProcesses)}'");
            if (MouseMoveMinutes + MinBytesReceived <= 0) throw new Exception("/mouse or /down must be provided");
        }

        private void InvalidParameterPair(string arg)
        {
            throw new ArgumentException($"expected parameter pair of type 'key=value'. Got '{arg}' instead");
        }

        private void ShowParameterInfo()
        {
            Console.WriteLine("autoshutdown /mouse=[time] /down=[minspeed] /processes=[process1,process2] /command=[beep|(command)] /params=[parameters]");
            Console.WriteLine($"see {_githubUrl} for detailed help");
            Environment.Exit(0);
        }
    }
}