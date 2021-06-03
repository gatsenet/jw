using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using JW.Common;
using System.Net;
using System.IO;
using System.Data;

namespace JW.Common
{
    public class MyMethod
    {
        public static bool IsInt(string value)
        {
            bool isok = false;long _long = 0;
            isok=Int64.TryParse(value, out _long);
            return isok;
        }

        public static string ToReadApiBackString(string url, Dictionary<string, object> postData)
        {
            string returnStr = "";
            try
            {
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = string.Format("{0}", url),//URL     必需项
                    Method = "POST",//URL     可选项 默认为Get
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = "",//字符串Cookie     可选项
                    UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",//用户的浏览器类型，版本，操作系统     可选项有默认值
                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    ContentType = "application/json",//返回类型    可选项有默认值
                    //Referer = ApiUrlTextEdit.Text.Trim(),//来源URL     可选项
                    //Allowautoredirect = true,//是否根据３０１跳转     可选项
                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                    Postdata = postData.ExDictToJsonString(),//Post数据     可选项GET时不需要写
                    //ProxyIp = "192.168.1.105",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                    //ProxyPwd = "123456",//代理服务器密码     可选项
                    //ProxyUserName = "administrator",//代理服务器账户名     可选项
                    //ResultType = ResultType.String,//返回数据类型，是Byte还是String
                };
                HttpResult result = http.GetHtml(item);
                returnStr = result.Html;
            }
            catch (Exception ex)
            {
                returnStr = "";
            }
            return returnStr;
        }

        public static string ToGetApiBackString(string url)
        {
            string returnStr = "";
            try
            {
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = url,//URL     必需项
                    Method = "Get",//URL     可选项 默认为Get
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = "",//字符串Cookie     可选项
                    UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",//用户的浏览器类型，版本，操作系统     可选项有默认值
                    //Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    //ContentType = "application/json",//返回类型    可选项有默认值
                    //Referer = ApiUrlTextEdit.Text.Trim(),//来源URL     可选项
                    //Allowautoredirect = true,//是否根据３０１跳转     可选项
                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                    //ProxyIp = "192.168.1.105",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                    //ProxyPwd = "123456",//代理服务器密码     可选项
                    //ProxyUserName = "administrator",//代理服务器账户名     可选项
                    //ResultType = ResultType.String,//返回数据类型，是Byte还是String
                };
                HttpResult result = http.GetHtml(item);
                returnStr = result.Html;
            }
            catch (Exception ex)
            {
                returnStr = "";
            }
            return returnStr;
        }

        public static JObject ToReadAPI(string url, Dictionary<string,object> postData)
        {
            JObject resultObj = new JObject();
            try
            {
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = string.Format("{0}", url),//URL     必需项
                    Method = "POST",//URL     可选项 默认为Get
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = "",//字符串Cookie     可选项
                    UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",//用户的浏览器类型，版本，操作系统     可选项有默认值
                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    ContentType = "application/json",//返回类型    可选项有默认值
                    //Referer = ApiUrlTextEdit.Text.Trim(),//来源URL     可选项
                    //Allowautoredirect = true,//是否根据３０１跳转     可选项
                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                    Postdata = postData.ExDictToJsonString(),//Post数据     可选项GET时不需要写
                    //ProxyIp = "192.168.1.105",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                    //ProxyPwd = "123456",//代理服务器密码     可选项
                    //ProxyUserName = "administrator",//代理服务器账户名     可选项
                    //ResultType = ResultType.String,//返回数据类型，是Byte还是String
                };
                HttpResult result = http.GetHtml(item);
                resultObj = Newtonsoft.Json.Linq.JObject.Parse(result.Html);
            }
            catch (Exception ex)
            {
                resultObj = new JObject();
                resultObj.Add("status", false);
                resultObj.Add("msg", ex.Message);
            }
            return resultObj;
        }

        public static double TimeInterval(DateTime dateb, DateTime datee, string type = "s")
        {
            double value = 0;
            switch (type.ToLower())
            {
                case "d":
                    value = TimeSpan.FromTicks(datee.Ticks).Subtract(TimeSpan.FromTicks(dateb.Ticks)).Duration().TotalDays;
                    break;
                case "h":
                    value = TimeSpan.FromTicks(datee.Ticks).Subtract(TimeSpan.FromTicks(dateb.Ticks)).Duration().TotalHours;
                    break;
                case "m":
                    value = TimeSpan.FromTicks(datee.Ticks).Subtract(TimeSpan.FromTicks(dateb.Ticks)).Duration().TotalMinutes;
                    break;
                case "s":
                default:
                    value = TimeSpan.FromTicks(datee.Ticks).Subtract(TimeSpan.FromTicks(dateb.Ticks)).Duration().TotalSeconds;
                    break;
            }
            return value;
        }

        /// <summary>
        /// Unicode转字符串
        /// </summary>
        /// <param name="source">经过Unicode编码的字符串</param>
        /// <returns>正常字符串</returns>
        public static string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                         source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }

        public static bool ToCheckUrl(ref string Url)
        {
            bool isok = false;
            if (Url.ExStrNotNull())
            {
                if (!Url.Equals("about:blank"))
                {
                    if (!Url.StartsWith("http://") && !Url.StartsWith("https://")) { Url = "http://" + Url; }
                    isok = true;
                }
            }
            return isok;
        }

        public static bool ToCheckUrl(string Url)
        {
            bool isok = false;
            string _url = Url;
            isok = ToCheckUrl(ref _url);
            return isok;
        }



        public static void ToPlayWav(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    if (Path.GetExtension(FilePath).ToLower() == ".wav")
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(FilePath);
                        player.Play();
                        player.Dispose();
                    }
                }
                catch
                {
                }
            }
        }

        public static bool ToSendcUrl(string url,string query)
        {
            bool isok = false;
            // 请求示例：
            //curl -u 账号:密码 -H "Accept: application/json" -X POST -H "Content-Type: application/x-www-form-urlencoded" -d parentId=父文件夹ID -d path=文件夹名称 
            // url 为请求地址，也就是curl指向的地址
            try
            {
                HttpWebRequest http = (HttpWebRequest)WebRequest.Create(url);
                // -H 表示http请求中需要有一个Header信息
                // -H "Accept: application/json"
                http.Accept = "application/json";
                // -u 账号:密码，提交身份验证信息
                //http.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("账号:密码")));
                // -X POST 使用 POST 方式请求
                http.Method = "POST";
                // -d 表示提交的表单中的信息
                // -d parentId=父文件夹ID
                //JObject jObject = new JObject();
                //JObject jObject1 = new JObject();
                //jObject.Add("msgtype", "text");
                //jObject1.Add("content", "hello world");
                //jObject.Add("text", jObject1);
                byte[] pd = null;
                string QueryString = query; // 如果有多个 -d ，用 & 拼接每一个参数
                pd = new UTF8Encoding().GetBytes(QueryString);
                http.ContentLength = pd.Length;
                Stream ps = http.GetRequestStream();
                ps.Write(pd, 0, pd.Length);  // 将表单请求写入到请求头中
                ps.Close();
                ps.Dispose();
                isok = true;
            }
            catch { }
            return isok;
        }

        public static bool IsCheckCol(DataColumnCollection listCols, string[] strCols)
        {
            foreach (string col in strCols)
            {
                if (!listCols.Contains(col))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 转换为ISO_8859_1
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        public static string StringToISO_8859_1(string srcText)
        {
            string dst = "";
            char[] src = srcText.ToCharArray();
            for (int i = 0; i < src.Length; i++)
            {
                string str = @"&#" + (int)src[i] + ";";
                dst += str;
            }
            return dst;
        }
        /// <summary>
        /// 转换为原始字符串
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        public static string ISO_8859_1ToString(string srcText)
        {
            string dst = "";
            string[] src = srcText.Split(';');
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i].Length > 0)
                {
                    string str = ((char)int.Parse(src[i].Substring(2))).ToString();
                    dst += str;
                }
            }
            return dst;
        }
    }
}
