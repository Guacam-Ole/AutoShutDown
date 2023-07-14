using AutoShutDown.Backend;

namespace AutoShutDown.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Execute.Log("Started");
                var settings = new Settings(args);
                var network = new Network(settings);
                var mouse = new Mouse(settings, network);

                if (settings.MouseMoveMinutes > 0)
                {
                    mouse.Init();
                }
                else
                {
                    network.Init();
                }

                while (true) Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Execute.Log(ex.ToString());
            }
        }
    }
}