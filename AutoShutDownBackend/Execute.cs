using Serilog;

using System.Diagnostics;

namespace AutoShutDown.Backend
{
    public static class Execute
    {
        public static void RunCommand(string command, string parameters)
        {
            if (Debugger.IsAttached)
            {
                Log.Debug($"Disabled execution in DebugMode");
            }
            else
            {
                Process.Start(command, parameters);
            }

            Log.Information($"Executed {command} {parameters}");
        }

        public static void RunCommand(Settings settings)
        {
            Log.Debug("Running Shutdown-Command");
            if (settings.OnlyBeep)
            {
                Console.Beep(3000, 500);
                return;
            }

            RunCommand(settings.ExecuteCommand, settings.ExecuteParameters);
            Environment.Exit(0);
        }
    }
}