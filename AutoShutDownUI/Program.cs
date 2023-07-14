using Serilog;

namespace AutoShutDown.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Debug()
                  .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
                  .WriteTo.File("autoshutdown.log", Serilog.Events.LogEventLevel.Debug)
                  .CreateLogger();


            ApplicationConfiguration.Initialize();
            Application.Run(new BackgroundContext());
        }



    }
}