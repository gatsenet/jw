using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Globalization;
using JW.Common;

namespace JW.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            //string str = "{\"Items\":[{\"Barcode\":\"6941149628394\",\"Brand\":\"鬼冢虎\",\"Discount\":1.0000,\"Long\":\"0   \",\"SizeID\":\"605\",\"StoreUsable\":0,\"StyleID\":\"1182A014-100\",\"TagPrice\":690.0000,\"TomorrowDiscount\":10.0000,\"Usable\":99,\"WarehouseUsable\":0,\"updateTime\":\"\\/Date(1609315920000+0800)\\/\"},{\"Barcode\":\"6941273832049\",\"Brand\":\"鬼冢虎\",\"Discount\":1.0000,\"Long\":\"0   \",\"SizeID\":\"505\",\"StoreUsable\":2,\"StyleID\":\"1182A030-400\",\"TagPrice\":690.0000,\"TomorrowDiscount\":10.0000,\"Usable\":2,\"WarehouseUsable\":0,\"updateTime\":\"\\/Date(1613614140000+0800)\\/\"},{\"Barcode\":\"6941273832056\",\"Brand\":\"鬼冢虎\",\"Discount\":1.0000,\"Long\":\"0   \",\"SizeID\":\"6\",\"StoreUsable\":4,\"StyleID\":\"1182A030-400\",\"TagPrice\":690.0000,\"TomorrowDiscount\":10.0000,\"Usable\":4,\"WarehouseUsable\":0,\"updateTime\":\"\\/Date(1613787000000+0800)\\/\"}],\"ResultCode\":\"0\",\"ResultMessage\":\"\",\"TotalPage\":0}";
            //JObject jObject = JObject.Parse(str);
            //JArray items = (JArray)jObject["Items"];
            ////JW.API.Sanse.GetCustomerOnhandByStyleList("data.sanse.com.cn:9000", "84d1cf38-de5a-4283-902e-a12448cf3bd7", "X31", "", out str);
            //JsonSerializer serializer = new JsonSerializer();
            //serializer.Converters.Add(new JavaScriptDateTimeConverter());//指定转化日期的格式
            //IsoDateTimeConverter iso = new IsoDateTimeConverter();
            //iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //iso.DateTimeStyles = DateTimeStyles.AssumeLocal;
            //serializer.Converters.Add(iso);
            //JsonReader reader = new JsonTextReader(new System.IO.StringReader(jObject["Items"].ToString()));
            //JArray jo = (JArray)serializer.Deserialize(reader);

            //JW.API.Sanse.GetCustomerOnhandByStyleList('data.sanse.com.cn:9000', '84d1cf38-de5a-4283-902e-a12448cf3bd7', 'X31', '', out str);
            API.ToRun.UpdateStock();
        }

        private static void Init()
        {
            //MyProperty.SqlConnetString = MyProperty.SqlConnetStringByWeb;
            JW.Common.MyProperty.LoggerPath = UtilConf.GetAppConfigValue("LoggerPath");
            JW.Common.MyProperty.LoggerIsOpen = UtilConf.GetAppConfigValue("LoggerIsOpen").ExObjBool();
        }
    }
}
