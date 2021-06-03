using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JW.Common;

namespace JW.DB
{
    public class BasicUser
    {
        public static bool p_BasicUser_Verify(string UserID, string UserPS, out string msg)
        {
            bool isok = false;
            DbParameters paras = new DbParameters();
            paras.Add("@UserID", UserID.Trim());
            paras.Add("@UserPS", UserPS.Trim());
            paras.AddInputOutput("@isok", "bit");
            paras.AddInputOutput("@msg", "nvarchar", 200);
            SqlParameter[] p = new DbHelper().ExecuteReturn("p_BasicUser_Verify", paras);
            isok = p[2].Value.ExObjBool();
            msg = p[3].Value.ExObjString();
            return isok;
        }

        /// <summary>
        /// 用户信息更新
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UpdateType">0=更新密码，1=新增，2=更新，3=删除</param>
        /// <param name="datajson"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool p_BasicUser_Update(string UserID,int UpdateType,string datajson,out string msg)
        {
            bool isok = false;
            DbParameters paras = new DbParameters();
            paras.Add("@UserID", UserID.Trim());
            paras.Add("@UpdateType", UpdateType);
            paras.Add("@datajson", datajson);
            paras.AddInputOutput("@isok", "bit");
            paras.AddInputOutput("@msg", "nvarchar", 200);
            SqlParameter[] p = new DbHelper().ExecuteReturn("p_BasicUser_Update", paras);
            isok = p[3].Value.ExObjBool();
            msg = p[4].Value.ExObjString();
            return isok;
        }

        /// <summary>
        /// 获取用户全部相关信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TypeIs">0=在用，1=停用，2=全部</param>
        /// <returns></returns>
        public static DataSet p_BasicUser_GetInfo_ALL(string UserID, int TypeIs = 2, string UnitID = "")
        {
            DbParameters paras = new DbParameters();
            paras.Add("@UserID", UserID.Trim());
            paras.Add("@Type", 0);
            paras.Add("@TypeIs", TypeIs);
            paras.Add("@UnitID", UnitID);
            DataSet ds = new DbHelper().CreateDataSet("p_BasicUser_GetInfo", paras);
            return ds;
        }

        /// <summary>
        /// 获取用户相关信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Type">1=渠道，2=供应商，3=模块，4=渠道的客户</param>
        /// <param name="TypeIs">0=在用，1=停用，2=全部</param>
        /// <returns></returns>
        public static DataTable p_BasicUser_GetInfo(string UserID, int Type, int TypeIs = 2, string UnitID = "")
        {
            if (Type < 1) Type = 1;
            DbParameters paras = new DbParameters();
            paras.Add("@UserID", UserID.Trim());
            paras.Add("@Type", Type);
            paras.Add("@TypeIs", TypeIs);
            paras.Add("@UnitID", UnitID);
            DataTable dt = new DbHelper().CreateDataTable("p_BasicUser_GetInfo", paras);
            return dt;
        }

        /// <summary>
        /// 获取用户列表和相关渠道、供应商设置列表
        /// </summary>
        /// <returns></returns>
        public static DataSet p_BasicUser_GetList()
        {
            DbParameters paras = new DbParameters();
            paras.Add("@Type", 10);
            DataSet ds = new DbHelper().CreateDataSet("p_BasicUser_GetInfo", paras);
            return ds;
        }
    }
}
