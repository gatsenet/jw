using JW.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JW.DB
{
    public class Stock
    {
        public static bool StockInfo(string SupplyID, int Type, bool isOverWrite, string datajson, out string msg)
        {
            bool isok = false;
            DbParameters paras = new DbParameters();
            paras.Add("@SupplyID", SupplyID.Trim());
            paras.Add("@type", Type);
            paras.Add("@isOverWrite", isOverWrite);
            paras.Add("@datajson", datajson);
            paras.AddInputOutput("@isok", "bit");
            paras.AddInputOutput("@msg", "nvarchar", 200);
            SqlParameter[] p = new DbHelper().ExecuteReturn("p_Stock_Info", paras);
            isok = p[4].Value.ExObjBool();
            msg = p[5].Value.ExObjString();
            return isok;
        }

        public static bool UpdateStockByAPI(string SupplyID,string datajson,bool isOverWrite,out string msg)
        {
            return StockInfo(SupplyID, 11, isOverWrite, datajson, out msg);
        }

    }
}
