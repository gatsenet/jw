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
    public class Goods
    {
        public static DataTable p_Goods_Info(int type = 0, string StyleID = "")
        {
            DbParameters paras = new DbParameters();
            paras.Add("@type", type);
            paras.Add("@StyleID", StyleID);
            DataTable dt = new DbHelper().CreateDataTable("p_Goods_Info", paras);
            return dt;
        }

        public static DataTable GoodsWeightList()
        {
            return p_Goods_Info(10);
        }

        public static bool GoodsWeightUpdate(string datajson,out string msg)
        {
            bool isok = false;msg = "";
            DbParameters paras = new DbParameters();
            paras.Add("@type", 11);
            paras.Add("@datajson", datajson);
            paras.AddInputOutput("@isok", "bit");
            paras.AddInputOutput("@msg", "nvarchar", 200);
            SqlParameter[] p = new DbHelper().ExecuteReturn("p_Goods_Info", paras);
            isok = p[2].Value.ExObjBool();
            msg = p[3].Value.ExObjString();
            return isok;
        }
    }
}
