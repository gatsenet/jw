using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JW.Common;
using Newtonsoft.Json.Linq;

namespace JW.Order.Web.API
{
    [Route("api/Common/{action}", Name = "CommonApi")]
    public class CommonController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage BoolList()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            JArray arr = new JArray();
            arr = JArray.Parse("[{'ID':0,'Name':'失败'},{'ID':1,'Name':'成功'},{'ID':2,'Name':'全部'}]");
            response.Content = new StringContent(arr.ToString(), System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        [HttpGet]
        public HttpResponseMessage IsStopList()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            JArray arr = new JArray();
            arr = JArray.Parse("[{'ID':0,'Name':'停用'},{'ID':1,'Name':'启用'},{'ID':2,'Name':'全部'}]");
            response.Content = new StringContent(arr.ToString(), System.Text.Encoding.UTF8, "application/json");
            return response;
        }
    }
}
