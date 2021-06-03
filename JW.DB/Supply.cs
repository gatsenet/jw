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
    public class Supply
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplyID"></param>
        /// <param name="Type">0=列表，1=新增，2=更新，3=删除</param>
        /// <param name="IsStop">0=在用，1=停用，2=全部</param>
        /// <param name="datajson"></param>
        /// <param name="UserID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool SupplyInfo(string SupplyID, int Type, int IsStop, string datajson, string UserID, out string msg)
        {
            bool isok = false;
            DbParameters paras = new DbParameters();
            paras.Add("@SupplyID", SupplyID.Trim());
            paras.Add("@Type", Type);
            paras.Add("@IsStop", IsStop);
            paras.Add("@datajson", datajson);
            paras.Add("@UserID", UserID);
            paras.AddInputOutput("@isok", "bit");
            paras.AddInputOutput("@msg", "nvarchar", 200);
            SqlParameter[] p = new DbHelper().ExecuteReturn("p_Supply_Info", paras);
            isok = p[5].Value.ExObjBool();
            msg = p[6].Value.ExObjString();
            return isok;
        }

        public static DataTable GetSupplyList(string SupplyID="" , int IsStop = 2 ,string UserID="")
        {
            DbParameters paras = new DbParameters();
            paras.Add("@SupplyID", SupplyID);            
            paras.Add("@Type", 0);
            paras.Add("@IsStop", IsStop);
            paras.Add("@UserID", UserID.Trim());
            DataTable dt = new DbHelper().CreateDataTable("p_Supply_Info", paras);
            return dt;
        }

        public static bool AddSupply(string datajson, out string msg)
        {
            return SupplyInfo("", 1, 2, datajson, "", out msg);
        }

        public static bool UpdateSupply(string SupplyID, string datajson, out string msg)
        {
            return SupplyInfo(SupplyID, 2, 2, datajson, "", out msg);
        }

        public static bool DelSupply(string SupplyID, out string msg)
        {
            return SupplyInfo(SupplyID, 3, 2, "", "", out msg);
        }
    }
}
