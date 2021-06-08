using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JW.Common;
using Newtonsoft.Json.Linq;

namespace JW.API
{
    public class Sanse
    {
        public static bool GetCustomerOnhandByStyleList(string url,string key,string customerID,string styleList,out string datajson)
        {
            bool isok = false;datajson = "";DateTime dateb = DateTime.Now; string msg = "Sanse;";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("url", string.Format("http://{0}/OpenAPI.svc/rest/GetCustomerOnhandByStyleList", url));
            paras.Add("customerID", customerID);
            paras.Add("styleList", styleList);
            paras.Add("MD5", JW.Common.DEncrypt.DEncrypt.GenerateMD5(string.Format("{0}{1}{2}", customerID, styleList, key)).ToUpper());
            Dictionary<string, string> parasOld = new Dictionary<string, string>(paras);
            try
            {
                datajson = MyMethod.ToGetApiBackString(paras, true, true, 5);
                isok = datajson.ExStrNotNull();
                if (isok)
                {
                    JObject jObject = JObject.Parse(datajson);
                    isok = jObject["ResultCode"].ExObjString() == "0";
                    if (isok)
                    {
                        JArray items = (JArray)jObject["Items"];
                        datajson = items.ToString();
                    }
                    msg += (isok ? "成功" : "失败") + ";" + jObject["ResultMessage"].ToString();
                }
                else
                {
                    msg+= (isok ? "成功" : "失败") + ";供应商无数据返回";
                }
            }
            catch(Exception ex)
            {
                isok = false;
                msg += (isok ? "成功" : "失败") + ";" + ex.Message;
            }
            finally
            {
                DB.LogSystem.AddLog(isok, "供应商库存接口", "按款号获取", msg, datajson, parasOld.ExDictToJsonString(), dateb, DateTime.Now);
            }
            return isok;
        }

        public static bool GetCustomerOnhandByDate(string url, string key, string customerID, DateTime fromDate,DateTime toDate, out string datajson)
        {
            bool isok = false; datajson = ""; DateTime dateb = DateTime.Now; string msg = "Sanse;";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("url", string.Format("http://{0}/OpenAPI.svc/rest/GetCustomerOnhandByDate", url));
            paras.Add("customerID", customerID);
            paras.Add("fromDate", fromDate.ToString("yyyyMMddHH"));
            paras.Add("toDate", toDate.ToString("yyyyMMddHH"));
            paras.Add("MD5", JW.Common.DEncrypt.DEncrypt.GenerateMD5(string.Format("{0}{1}{2}{3}", customerID, fromDate.ToString("yyyyMMddHH"), toDate.ToString("yyyyMMddHH"), key)).ToUpper());
            Dictionary<string, string> parasOld = new Dictionary<string, string>(paras);
            try
            {
                datajson = MyMethod.ToGetApiBackString(paras, true, true, 5);
                isok = datajson.ExStrNotNull();
                if (isok)
                {
                    JObject jObject = JObject.Parse(datajson);
                    isok = jObject["ResultCode"].ExObjString() == "0";
                    if (isok)
                    {
                        JArray items = (JArray)jObject["Items"];
                        datajson = items.ToString();
                    }
                    msg += (isok ? "成功" : "失败") + ";" + jObject["ResultMessage"].ToString();
                }
                else
                {
                    msg += (isok ? "成功" : "失败") + ";供应商无数据返回";
                }
            }
            catch (Exception ex)
            {
                isok = false;
                msg += (isok ? "成功" : "失败") + ";" + ex.Message;
            }
            finally
            {
                DB.LogSystem.AddLog(isok, "供应商库存接口", "按时间获取", msg, datajson, parasOld.ExDictToJsonString(), dateb, DateTime.Now);
            }
            return isok;
        }
    }
}
