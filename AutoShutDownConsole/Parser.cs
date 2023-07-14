using AutoShutDown.Backend;

using Serilog;

namespace AutoShutDown.Console
{
    public static class Parser
    {
        private const string _githubUrl = "https://github.com/OleAlbers/AutoShutDown";

        public static Settings? GetSettingsByArgs(string[] args)
        {
            var settings = new Settings();
            if (args.Length == 0) { ShowParameterInfo(); return null; }
            foreach (var arg in args)
            {
                if (!arg.Contains('=')) InvalidParameterPair(arg);
                var pair = arg.Split('=');
                switch (pair[0])
                {
                    case "/mouse":
                        settings.MouseMoveMinutes = Convert.ToInt32(pair[1]);
                        break;

                    case "/down":
                        settings.MinBytesReceived = pair[1].DownloadLimitValue();
                        break;

                    case "/processes":
                        settings.LongRunningProcesses = pair[1].Split(',');
                        break;

                    case "/command":
                        settings.ExecuteCommand = pair[1];
                        break;

                    case "/params":
                        settings.ExecuteParameters = pair[1];
                        break;
                }
            }

            Log.Debug($"Autoshutdown started.");
            //Log.Debug($"[Mousemove: {settiMouseMoveMinutes} minutes | Downloadlimit: {MinBytesReceived.Fancy()} | beep: {Beep} | command:'{ExecuteCommand}' | parameters: '{ExecuteParameters}' | processes:'{string.Join(",", LongRunningProcesses)}']");

            if (settings.MouseMoveMinutes + settings.MinBytesReceived <= 0) throw new Exception("/mouse or /down must be provided");
            return settings;
        }

        private static void InvalidParameterPair(string arg)
        {
            throw new ArgumentException($"expected parameter pair of type 'key=value'. Got '{arg}' instead");
        }

        private static void ShowParameterInfo()
        {
            System.Console.WriteLine("autoshutdown /mouse=[time] /down=[minspeed] /processes=[process1,process2] /command=[beep|(command)] /params=[parameters]");
            System.Console.WriteLine($"see '{_githubUrl}' for detailed help");
            Environment.Exit(0);
        }
    }
}