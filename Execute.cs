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
                if (settings.ConsoleLog) Console.WriteLine("beep");
                return;
            }

#if DEBUG
            Console.WriteLine($"Would call '{settings.ExecuteCommand} {settings.ExecuteParameters}' on Release");
#else
            Process.Start(settings.ExecuteCommand, settings.ExecuteParameters);
            if (settings.ConsoleLog) Console.WriteLine("Executing command, exiting program");
#endif
            Environment.Exit(0);
        }
    }
}
