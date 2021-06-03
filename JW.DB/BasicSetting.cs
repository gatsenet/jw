using JW.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JW.DB
{
    public class BasicSetting
    {
        public static DataTable GetSettingList(string SettingID = "")
        {
            DbParameters paras = new DbParameters();
            paras.Add("@type", 0);
            paras.Add("@SettingID", SettingID);   
            DataTable dt = new DbHelper().CreateDataTable("p_BasicSetting_Info", paras);
            return dt;
        }

        public static bool SettingInfo(string SettingID, int type, string datajson, out string msg)
        {
            bool isok = false;
            DbParameters paras = new DbParameters();
            paras.Add("@SettingID", SettingID.Trim());
            paras.Add("@type", type);
            paras.Add("@datajson", datajson);
            paras.AddInputOutput("@isok", "bit");
            paras.AddInputOutput("@msg", "nvarchar", 200);
            SqlParameter[] p = new DbHelper().ExecuteReturn("p_BasicSetting_Info", paras);
            isok = p[3].Value.ExObjBool();
            msg = p[4].Value.ExObjString();
            return isok;
        }

        public static bool UpdateSetting(string SettingID,string datajson,out string msg)
        {
            return SettingInfo(SettingID, 2, datajson, out msg);
        }
    }
}
