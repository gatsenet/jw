using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Web.Configuration;
using System.Configuration;

namespace JW.Common
{
   public class WebConfig
    {
       public static void SaveAppSetting(string key, string value, string webPath = "~")
       {

           //XmlDocument config = new XmlDocument();
           //string XPath = "/configuration/appSettings/add[@key='?']";

           //config.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

           //XmlNode addKey = config.SelectSingleNode((XPath.Replace("?", key)));

           //if (addKey != null)
           //{
           //    addKey.Attributes["value"].InnerText = value;
           //    config.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
           //}
           Configuration config = WebConfigurationManager.OpenWebConfiguration(webPath);
           AppSettingsSection app = config.AppSettings;
           app.Settings.Remove(key);
           app.Settings.Add(key, value);
           config.Save(ConfigurationSaveMode.Modified);
       }

       public static string ReadAppSetting(string key, string webPath = "~")
       {
           string _vale = "";
           Configuration config = WebConfigurationManager.OpenWebConfiguration(webPath);
           AppSettingsSection app = config.AppSettings;
           try
           {
               //_vale = System.Configuration.ConfigurationManager.AppSettings[key].ToString();
               _vale = app.Settings[key].Value;
           }
           catch { }
           return _vale;
       }
    }
}
