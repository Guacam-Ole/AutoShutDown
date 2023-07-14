using Serilog;

using System.Diagnostics;

namespace AutoShutDown.Backend
{
    public static class Execute
    {

        public static void RunCommand(string command, string parameters)
        {
            Process.Start(command, parameters);
            Log.Debug($"Executed {command} {parameters}");
        }
        public static void RunCommand(Settings settings)
        {
            Log.Debug("Running Shutdown-Command");
            if (settings.OnlyBeep)
            {
                Console.Beep(3000, 500);
                return;
            }

#if DEBUG
            Console.WriteLine($"Would call '{settings.ExecuteCommand} {settings.ExecuteParameters}' on Release");
#else
            RunCommand(settings.ExecuteCommand, settings.ExecuteParameters);
#endif
            Environment.Exit(0);
        }
    }
}
