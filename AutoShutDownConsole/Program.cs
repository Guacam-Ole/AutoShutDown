using AutoShutDown.Backend;

using AutoShutDown.Backend;

using Newtonsoft.Json;

using Serilog;

namespace AutoShutDown.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
                    .WriteTo.File("autoshutdown.log", Serilog.Events.LogEventLevel.Debug)
                    .CreateLogger();

                var settings = Parser.GetSettingsByArgs(args);
         
                if (settings == null) return;
                var watchDog = new WatchDog(settings);
                watchDog.WarningEvent += WatchDog_WarningEvent;
                Log.Information("Autoshutdown Console started");
                watchDog.RunWatchDog().Wait();
                Log.Information("Autoshutdown Console stopped");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "error in console application");
            }
        }

        private static void WatchDog_WarningEvent(object? sender, EventArgs e)
        {
            Log.Warning("Will Shutdown soon");
        }
    }
}