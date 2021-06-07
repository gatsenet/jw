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
        public static DataTable GetInfo(string dateb="",string datee="",string logtype="",string action ="",int result=2, string like="")
        {
            DbParameters paras = new DbParameters();
            paras.Add("@type", 0);
            paras.Add("@dateb", dateb.Trim());
            paras.Add("@datee", datee);
            paras.Add("@logtype", logtype);
            paras.Add("@action", action);
            paras.Add("@result", result);
            paras.Add("@like", like);
            DataTable dt = new DbHelper().CreateDataTable("p_LogSystem_Info", paras);
            return dt;
        }

        public static void AddLog(bool Result,string LogType,string LogAction,string BizData,string NewData,string OldData,DateTime DateBegin,DateTime DateEnd)
        {
            DbParameters paras = new DbParameters();
            paras.Add("@Result", Result);
            paras.Add("@LogType", LogType);
            paras.Add("@LogAction", LogAction);
            paras.Add("@BizData", BizData);
            paras.Add("@NewData", NewData);
            paras.Add("@OldData", OldData);
            paras.Add("@DateBegin", DateBegin);
            paras.Add("@DateEnd", DateEnd);
            new DbHelper().Execute("p_LogSystem_Add", paras);
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
