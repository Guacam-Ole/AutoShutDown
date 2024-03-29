﻿using Newtonsoft.Json;

namespace AutoShutDown.UI
{
    public static class ConfigReader
    {
        private const string _settingsFile = "settings.json";

        public static Backend.Settings? ReadSettings()
        {
            if (!File.Exists(_settingsFile)) return null;
            return JsonConvert.DeserializeObject<AutoShutDown.Backend.Settings>(File.ReadAllText("settings.json"));
        }

        public static void WriteSettings(Backend.Settings settings)
        {
            File.WriteAllText("settings.json", JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
    }
}