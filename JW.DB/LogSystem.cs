using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JW.Common;

namespace JW.DB
{
    public class LogSystem
    {
        public static DataTable GetInfo(string dateb="",string datee="",string logtype="",string action ="",string like="")
        {
            DbParameters paras = new DbParameters();
            paras.Add("@type", 0);
            paras.Add("@dateb", dateb.Trim());
            paras.Add("@datee", datee);
            paras.Add("@logtype", logtype);
            paras.Add("@action", action);
            DataTable dt = new DbHelper().CreateDataTable("p_LogSystem_Info", paras);
            return dt;
        }

        public static DataTable GetLogType()
        {
            DbParameters paras = new DbParameters();
            paras.Add("@type", 1);
            DataTable dt = new DbHelper().CreateDataTable("p_LogSystem_Info", paras);
            return dt;
        }

        public static DataTable GetLogAction()
        {
            DbParameters paras = new DbParameters();
            paras.Add("@type", 2);
            DataTable dt = new DbHelper().CreateDataTable("p_LogSystem_Info", paras);
            return dt;
        }
    }
}
