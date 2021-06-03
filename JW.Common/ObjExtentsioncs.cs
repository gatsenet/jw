using JW.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace JW.Common
{
    public static class ObjExtentsioncs
    {
        public static int ExStrInt32(this string value, int defaultValue = 0)
        {
            int result;
            if (!int.TryParse(value, out result))
            {
                return defaultValue;
            }
            return result;
        }

        public static bool ExStrIsNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool ExStrNotNull(this string value)
        {
            return !ExStrIsNull(value);
        }

        public static string ExStrRepeat(this string value, int num)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < num; i++)
                sb.Append(value);
            return sb.ToString();
        }

        public static bool ExIsStrEmail(this string value)
        {
            return Regex.IsMatch(value, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool ExIsStrCellPhone(this string value)
        {
            return Regex.IsMatch(value, @"^1\d{10}$");
        }

        public static int[] ExStrIntArray(this string[] value)
        {
            return Array.ConvertAll<string, int>(value, s => int.Parse(s));
        }

        private static string[] ExDefaultStringArr = new string[] { };

        public static int ExObjInt32(this object value, int defaultValue = 0)
        {
            int result;
            string tmpStr = value.ExObjString();
            if (tmpStr.Trim().Length > 0)
                if (tmpStr.LastIndexOf(".") > 0) tmpStr = tmpStr.Substring(0, tmpStr.LastIndexOf("."));
            if (!int.TryParse(tmpStr, out result))
            {
                return defaultValue;
            }
            return result;
        }

        public static decimal ExObjDecimal(this object value, decimal defaultValue = 0)
        {
            decimal result;
            if (!decimal.TryParse(value.ExObjString(), out result))
            {
                return defaultValue;
            }
            return result;
        }

        public static long ExObjLong(this object value, long defaultValue = 0)
        {
            long result;
            if (!long.TryParse(value.ExObjString(), out result))
            {
                return defaultValue;
            }
            return result;
        }

        public static double ExObjDouble(this object value, double defaultValue = 0)
        {
            double result;
            if (!double.TryParse(value.ExObjString(), out result))
            {
                return defaultValue;
            }
            return result;
        }

        public static bool ExObjBool(this object value, bool defaultValue = false)
        {
            bool result;
            if (!bool.TryParse(value.ExObjString(), out result))
            {
                if (value.ExObjString() == "1")
                    result = true;
                else if (value.ExObjString() == "0")
                    result = false;
                else
                    result = defaultValue;
            }
            return result;
        }

        public static string ExObjString(this object value, string defaultValue = "")
        {
            return value == null ? defaultValue : value.ToString();
        }

        public static string[] ExObjStringArr(this object value, string[] defaultValue = null)
        {
            if (value == null)
            {
                return defaultValue == null ? ExDefaultStringArr : defaultValue;
            }
            else if (typeof(ArrayList) == value.GetType())
            {
                return (string[])value.ExObjArrayList().ToArray(typeof(string));
            }
            return (string[])value;
        }

        public static DateTime ExObjDateTime(this object value, string defaultValue = "1900/1/1 0:0:0")
        {
            DateTime result;
            if (!DateTime.TryParse(value.ExObjString(), out result))
            {
                if (!DateTime.TryParse(defaultValue, out result))
                    return new DateTime(1900, 1, 1);
            }
            return result;
        }

        public static string ExObjDateTimeString(this object value, string format = "yyyy-MM-dd HH:mm:ss", string defaultValue = "1900/1/1 0:0:0")
        {
            return value.ExObjDateTime(defaultValue).ToString(format);
        }

        public static string ExObjDateString(this object value, string defaultValue = "1900-01-01")
        {
            return ExObjDateTimeString(value, "yyyy-MM-dd", defaultValue);
        }

        public static ArrayList ExObjArrayList(this object value, ArrayList defaultvalue = null)
        {
            try
            {
                return (ArrayList)value;
            }
            catch
            {
                return defaultvalue == null ? new ArrayList() : defaultvalue;
            }
        }

        public static Dictionary<string, object> ExObjDictionary(this object value, Dictionary<string, object> defaultvalue = null)
        {
            try
            {
                return (Dictionary<string, object>)value;
            }
            catch
            {
                return defaultvalue == null ? new Dictionary<string, object>() : defaultvalue;
            }
        }

        public static bool ExObjIsNumeric(this object value)
        {
            System.Text.RegularExpressions.Regex regx = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");
            return regx.IsMatch(value.ExObjString());
        }

        public static bool ExObjIsNull(this object value)
        {
            return value == null;
        }

        public static string ExDateTimeString(this DateTime value, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return value.ToString(format);
        }

        public static string ExDateString(this DateTime value, string format = "yyyy-MM-dd")
        {
            return ExDateTimeString(value, format);
        }

        public static long ExDateToTimeStamp(this DateTime value)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(value.AddHours(-8) - Jan1st1970).TotalSeconds;
        }

        public static DateTime ExLongTimeStampToDateTime(this long timeStamp)
         {
             var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddSeconds(timeStamp).AddHours(8);
         }

    public static string ExStringFormat(this string value, params object[] args)
        {
            return string.Format(value.ExObjString(), args);
        }

        public static string ExDictToJsonString<TKey, TValue>(this Dictionary<TKey, TValue> value)
        {
            return JsonUntity.SerializeDictionaryToJsonString(value);
        }

        /// </summary>
        /// <param name="array">集合对象</param>
        /// <returns>Json字符串</returns>
        public static string ExArrayToJson(this IEnumerable array)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(array);
        }

    }
}
public static class DataTableExtentsion
{

    public static bool ExDataTableIsNullEmpty(this DataTable source)
    {
        if (source == null) return true;
        else if (source.Rows.Count == 0) return true;
        return false;
    }

    public static bool ExDataTableNotNullEmpty(this DataTable source)
    {
        return !ExDataTableIsNullEmpty(source);
    }

    public static string ExDataTableToJson(this DataTable source,bool AddEmpty=false)
    {        
        object resultObj = null;
        //if (source.ExDataTableNotNullEmpty())
        //{
        //    //判断 数据源是否原始数据 DataTable，如果是先进行List<Dictionary<string, object>> 转换            

        //}
        resultObj = ToList(source as DataTable, AddEmpty);
        var res = Newtonsoft.Json.JsonConvert.SerializeObject(resultObj);
        return resultObj.ExObjIsNull() ? "" : res;
    }

    /// <summary>
    /// 转化为List<Dictionary<string, object>>
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static List<Dictionary<string, object>> ToList(DataTable dt, bool AddEmpty = false)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        if (dt.ExDataTableNotNullEmpty())
        {
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
        }
        else
        {
            if (AddEmpty)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                if (dt != null)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        result.Add(dc.ColumnName, "");
                    }
                }
                else
                {
                    result.Add("ID", "");
                }
                list.Add(result);
            }
        }
        return list;
    }
}

public static class DataSetExtentsion
{
    public static bool ExDataSetIsNullEmpty(this DataSet ds)
    {
        if (ds == null) return true;
        else if (ds.Tables.Count == 0) return true;
        return false;
    }
    public static bool ExDataSetNotNullEmpty(this DataSet ds)
    {
        return !ExDataSetIsNullEmpty(ds);
    }

    public static string ExDataSetToJson(this DataSet ds)
    {        
        object resultObj = null;
        resultObj = ds.ExDataSetToJsonObj();
        var res = Newtonsoft.Json.JsonConvert.SerializeObject(resultObj);
        return resultObj.ExObjIsNull() ? "" : res;
    }

    public static object ExDataSetToJsonObj(this DataSet ds)
    {
        object resultObj = null;
        if (ds.ExDataSetNotNullEmpty())
        {
            //判断 数据源是否原始数据 DataSet，如果是先进行List<Dictionary<string, object>> 转换            
            if (ds.Tables.Count == 1)
                resultObj = DataTableExtentsion.ToList(ds.Tables[0]);
            else if (ds.Tables.Count > 1)
            {
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                Dictionary<string, object> result;
                int i = 0;
                foreach (DataTable table in ds.Tables)
                {
                    result = new Dictionary<string, object>();
                    result.Add(string.Format("table{0}", i.ToString()), DataTableExtentsion.ToList(table));
                    i++;
                    list.Add(result);
                }
                resultObj = list;
            }
            else
                resultObj = null;

        }
        return resultObj;
    }

    public static List<Dictionary<string, object>> ExDataSetToList(this DataSet ds, bool AddEmpty = false)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        if (ds.ExDataSetNotNullEmpty())
        {
            if (ds.Tables.Count == 1)
                list = DataTableExtentsion.ToList(ds.Tables[0], AddEmpty);
            else
            {
                Dictionary<string, object> result;
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    result = new Dictionary<string, object>();
                    result.Add(string.Format("table{0}", i.ToString()), DataTableExtentsion.ToList(ds.Tables[i], AddEmpty));
                    list.Add(result);
                }
            }
        }
        return list;
    }

}
