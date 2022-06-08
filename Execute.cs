using System.Diagnostics;

namespace AutoShutDown
{
    public static class Execute
    {
        public static void RunCommand(Settings settings)
        {
            if (settings.Beep)
            {
                Console.Beep(3000, 500);
                if (settings.ConsoleLog) Log("beep");
                return;
            }

#if DEBUG
            Console.WriteLine($"Would call '{settings.ExecuteCommand} {settings.ExecuteParameters}' on Release");
#else
            Process.Start(settings.ExecuteCommand, settings.ExecuteParameters);
            if (settings.ConsoleLog) Log("Executed command, exiting program");
#endif
            Environment.Exit(0);
        }

        public static void Log(string contents)
        {
            Console.WriteLine($"[{DateTime.Now}] {contents}");
        }
    }
}
