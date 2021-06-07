using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JW.Common;

namespace JW.API
{
    public class ToRun
    {
        public static void UpdateStock()
        {
            DataTable dt = DB.Supply.GetSupplyList("", 0, "");bool isok = false;
            string url = "", key = "", custid = "", datajson = "", supplyid = "", msg = "";
            foreach (DataRow row in dt.Rows)
            {
                if (row["IsApiStock"].ExObjBool())
                {
                    if(row["ApiName"].ExObjString("Sanse") == "Sanse")
                    {
                        supplyid = row["SupplyID"].ExObjString();
                        url = row["ApiUrl"].ExObjString();
                        key= row["ApiKey"].ExObjString();
                        custid= row["ApiCustID"].ExObjString();
                        if(JW.API.Sanse.GetCustomerOnhandByStyleList(supplyid,url, key, custid, "", out datajson))
                        {
                            isok = DB.Stock.UpdateStockByAPI(supplyid, datajson, false, out msg);
                        }
                    }
                }
            }
        }
    }
}
