
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace JW.Common
{
    public class UtilConf
    {
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                }
            }

            // Open App.Config of executable
            Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // You need to remove the old settings object before you can replace it
            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            // Add an Application Setting.
            config.AppSettings.Settings.Add(newKey, newValue);
            // Save the changes in App.config file.
            config.Save(ConfigurationSaveMode.Modified);
            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string GetAppConfigValue(string key)
        {
            string _value = "";
            bool isOK = System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains(key);            
            //ET199 et = null;
            //try
            //{
            //    et = new ET199();
            //    int nDevCount = et.Enum();
            //    if (nDevCount == 0)
            //    {
            //        isOK = false;
            //    }
            //    else
            //    {
            //        et.Open(0);
            //        if (!string.Format("{0:x08}", et.Customer).ToUpper().Equals("A43FE198"))
            //        {
            //            isOK = false;
            //        }
            //    }
            //    if (et != null) et.Close();
            //}
            //catch 
            //{
            //    isOK = false;
            //}
            try
            {
                if (isOK)
                    _value = System.Configuration.ConfigurationManager.AppSettings[key].ToString();
            }
            catch
            {
                _value = "";
            }
            return _value;
        }

        public static string GetAppConfigDBPassword(string key)
        {
            string _pass = GetAppConfigValue(key);
            return DEncrypt.DESEncrypt.Decrypt(_pass, "xmj13500001541");
        }

        public static void UpdateAppConfigDBPassword(string newKey, string newValue)
        {
            UpdateAppConfig(newKey, DEncrypt.DESEncrypt.Encrypt(newValue, "xmj13500001541"));
        }
    }
}
