using Pampa.InSol.Common.Extensions;
using System;
using System.Configuration;

namespace Pampa.InSol.Common.Utils
{
    /// <summary>
    /// Resuelve la recuperacion de configuraciones de la aplicacion
    /// </summary>
    public static class AppSettings
    {
        public static bool AppSettingsParser(string appSettings)
        {
            bool enabled = false;
            var settingToParse = ConfigurationManager.AppSettings[appSettings];
            if (settingToParse.IsNull())
            {
                return false;
            }

            bool.TryParse(settingToParse.ToString(), out enabled);
            return enabled;
        }

        public static string AppSettingToString(string appSetting)
        {
            var appSettingValue = ConfigurationManager.AppSettings[appSetting];
            if (appSettingValue.IsNotNull())
            {
                return appSettingValue;
            }

            throw new ConfigurationErrorsException(string.Format("AppSetting with key {0} not found", appSetting));
        }

        private static int AppSettingToInt(string appSetting)
        {
            var appSettingValue = ConfigurationManager.AppSettings[appSetting];
            if (appSettingValue.IsNotNull())
            {
                return Convert.ToInt16(appSettingValue);
            }

            throw new ConfigurationErrorsException(string.Format("AppSetting with key {0} not found", appSetting));
        }
    }
}