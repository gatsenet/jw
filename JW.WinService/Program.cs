using JW.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace JW.WinService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            Init();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MyService()
            };
            ServiceBase.Run(ServicesToRun);
        }

        static void Init()
        {
            JW.Common.MyProperty.LoggerPath = UtilConf.GetAppConfigValue("LoggerPath");
            JW.Common.MyProperty.LoggerIsOpen = UtilConf.GetAppConfigValue("LoggerIsOpen").ExObjBool();
        }
    }
}
