using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JW.Common;
using System.Data.SqlClient;

namespace JW.DB
{
    public class BasicRole
    {
        public static DataTable GetRoleList(string RoleID = "")
        {
            DbParameters paras = new DbParameters();
            paras.Add("@pType", 0);
            paras.Add("@RoleID", RoleID);
            DataTable dt = new DbHelper().CreateDataTable("p_BasicRole_Info", paras);
            return dt;
        }

        public static DataTable GetRoleDetailList(string RoleID)
        {
            DbParameters paras = new DbParameters();
            paras.Add("@pType", 1);
            paras.Add("@RoleID", RoleID);
            DataTable dt = new DbHelper().CreateDataTable("p_BasicRole_Info", paras);
            return dt;
        }

        public static bool DelRole(string RoleID, out string msg)
        {
            bool isok = false;
            DbParameters paras = new DbParameters();
            paras.Add("@pType", 4);
            paras.Add("@RoleID", RoleID);
            paras.AddInputOutput("@isok", "bit");
            paras.AddInputOutput("@msg", "nvarchar", 200);
            SqlParameter[] p = new DbHelper().ExecuteReturn("p_BasicRole_Info", paras);
            isok = p[2].Value.ExObjBool();
            msg = p[3].Value.ExObjString();
            return isok;
        }

        public static bool AddRole(string RoleID, string datajson, out string msg)
        {
            bool isok = false;
            DbParameters paras = new DbParameters();
            paras.Add("@pType", 2);
            paras.Add("@RoleID", RoleID);
            paras.Add("@datajson", datajson);
            paras.AddInputOutput("@isok", "bit");
            paras.AddInputOutput("@msg", "nvarchar", 200);
            SqlParameter[] p = new DbHelper().ExecuteReturn("p_BasicRole_Info", paras);
            isok = p[3].Value.ExObjBool();
            msg = p[4].Value.ExObjString();
            return isok;
        }

        public static bool UpdateRole(string RoleID, string datajson, out string msg)
        {
            bool isok = false;
            DbParameters paras = new DbParameters();
            paras.Add("@pType", 3);
            paras.Add("@RoleID", RoleID);
            paras.Add("@datajson", datajson);
            paras.AddInputOutput("@isok", "bit");
            paras.AddInputOutput("@msg", "nvarchar", 200);
            SqlParameter[] p = new DbHelper().ExecuteReturn("p_BasicRole_Info", paras);
            isok = p[3].Value.ExObjBool();
            msg = p[4].Value.ExObjString();
            return isok;
        }
    }
}
