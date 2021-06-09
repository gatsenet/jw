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

        public static string GetSettingValue(string SettingID)
        {
            DataTable dt = GetSettingList(SettingID);
            string value = "";
            if (dt.ExDataTableNotNullEmpty())
            {
                value = dt.Rows[0]["SettingValue"].ExObjString();
            }
            return value;
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

        /// <summary>
        /// 供应商全局库存更新时间点(小时点)
        /// </summary>
        public static int Stock_FullUpdate_Period
        {
            get { return GetSettingValue("Stock_FullUpdate_Period").ExObjInt32(1); }
        }

        /// <summary>
        /// 供应商时段库存更新间隔(每N小时)
        /// </summary>
        public static int Stock_TimeUpdate_Period
        {
            get { return GetSettingValue("Stock_TimeUpdate_Period").ExObjInt32(1); }
        }

        /// <summary>
        /// 供应商时段库存更新上次更新时间
        /// </summary>
        public static DateTime Stock_TimeUpdate_LastTime
        {
            get { return GetSettingValue("Stock_TimeUpdate_LastTime").ExObjDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); }
        }

        /// <summary>
        /// 供应商全局库存更新启用
        /// </summary>
        public static bool Stock_FullUpdate_Enable
        {
            get { return GetSettingValue("Stock_FullUpdate_Enable").ExObjBool(); }
        }

        /// <summary>
        /// 供应商时段库存更新启用
        /// </summary>
        public static bool Stock_TimeUpdate_Enable
        {
            get { return GetSettingValue("Stock_TimeUpdate_Enable").ExObjBool(); }
        }

        /// <summary>
        /// 商品重量导入字段
        /// </summary>
        public static string ImportXls_GoodsWeight
        {
            get { return GetSettingValue("ImportXls_GoodsWeight").ExObjString(); }
        }
    }
}
