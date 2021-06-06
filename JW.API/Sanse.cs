using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JW.Common;

namespace JW.API
{
    public class Sanse
    {
        public static bool GetCustomerOnhandByStyleList(string url,string key,string customerID,string styleList,out string datajson)
        {
            bool isok = false;datajson = "";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("customerID", customerID);
            paras.Add("styleList", styleList);
            paras.Add("MD5", JW.Common.DEncrypt.DEncrypt.GenerateMD5(string.Format("{0}{1}{2}", customerID, styleList, key)).ToUpper());
            datajson = MyMethod.ToGetApiBackString(string.Format("http://{0}/OpenAPI.svc/rest/GetCustomerOnhandByStyleList", url), paras, true, true, 5);
            isok = datajson.ExStrNotNull();
            return isok;
        }
    }
}
