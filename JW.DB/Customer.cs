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
    public class Customer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustID"></param>
        /// <param name="Type">0=列表，1=新增，2=更新，3=删除</param>
        /// <param name="IsStop">0=在用，1=停用，2=全部</param>
        /// <param name="datajson"></param>
        /// <param name="UserID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool CustomerInfo(string CustID,int Type,int IsStop,string datajson,string UserID,out string msg)
        {
            bool isok = false;
            DbParameters paras = new DbParameters();
            paras.Add("@CustID", CustID.Trim());
            paras.Add("@Type", Type);
            paras.Add("@IsStop", IsStop);
            paras.Add("@datajson", datajson);
            paras.Add("@UserID", UserID);
            paras.AddInputOutput("@isok", "bit");
            paras.AddInputOutput("@msg", "nvarchar", 200);
            SqlParameter[] p = new DbHelper().ExecuteReturn("p_Customer_Info", paras);
            isok = p[5].Value.ExObjBool();
            msg = p[6].Value.ExObjString();
            return isok;
        }

        public static DataTable GetCustList(string CustID = "", int IsStop = 2, string UserID = "")
        {
            DbParameters paras = new DbParameters();
            paras.Add("@CustID", CustID);
            paras.Add("@Type", 0);
            paras.Add("@IsStop", IsStop);
            paras.Add("@UserID", UserID.Trim());
            DataTable dt = new DbHelper().CreateDataTable("p_Customer_Info", paras);
            return dt;
        }

        public static bool AddCust(string datajson,  out string msg)
        {
            return CustomerInfo("", 1, 2, datajson, "", out msg);
        }

        public static bool UpdateCust(string CustID, string datajson, out string msg)
        {
            return CustomerInfo(CustID, 2, 2, datajson, "", out msg);
        }

        public static bool DelCust(string CustID, out string msg)
        {
            return CustomerInfo(CustID, 3, 2, "", "", out msg);
        }
    }
}
