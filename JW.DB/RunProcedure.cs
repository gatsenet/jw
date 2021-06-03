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
    public class RunProcedure
    {
        public static void pTushare_UpdateBaseStockInfo(string field, string datajson, string list_status, string exchange)
        {
            DbParameters paras = new DbParameters();
            paras.Add("@field", field);
            paras.Add("@datajson", datajson);
            paras.Add("@list_status", list_status);
            paras.Add("@exchange", exchange);
            new DbHelper().Execute("pTushare_UpdateBaseStockInfo", paras);
        }
    }
}
