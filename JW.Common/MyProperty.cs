using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace JW.Common
{
    public class MyProperty
    {
        private static string _connectionString = "";
        public static string SqlConnetString
        {
            get
            {
                try
                {
                    if (_connectionString == "")
                    {
                        _connectionString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", UtilConf.GetAppConfigValue("DBSERVER"), UtilConf.GetAppConfigValue("DBNAME"), UtilConf.GetAppConfigValue("DBUSER"), UtilConf.GetAppConfigDBPassword("DBPASSWORD"));
                    }

                }
                catch
                {
                    _connectionString = "";
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public static int SqlTimeout
        {
            get
            {
                return UtilConf.GetAppConfigValue("sqlTimeout").ExObjInt32(180);
            }
        }

        [DefaultValue("C:/")]
        public static string LoggerPath { get; set; }

        [DefaultValue(false)]
        public static bool LoggerIsOpen { get; set; }

        public static string SqlConnetStringByWeb
        {
            get
            {

                try
                {
                    if (_connectionString == "")
                    {
                        _connectionString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", WebConfig.ReadAppSetting("DBSERVER"), WebConfig.ReadAppSetting("DBNAME"), WebConfig.ReadAppSetting("DBUSER"), WebConfig.ReadAppSetting("DBPASSWORD"));
                    }

                }
                catch
                {
                    _connectionString = "";
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }

        }
    }
}
