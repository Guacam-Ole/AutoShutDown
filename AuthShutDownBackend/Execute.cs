using Serilog;

using System.Diagnostics;

namespace AutoShutDown.Backend
{
    public static class Execute
    {

        public static void RunCommand(string command)
        {


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
            Process.Start(settings.ExecuteCommand, settings.ExecuteParameters);
            if (settings.ConsoleLog) Log($"Executed {settings.ExecuteCommand} {settings.ExecuteParameters}");
#endif
            Environment.Exit(0);
        }
    }
}
