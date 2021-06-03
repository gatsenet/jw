using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using JW.Common;

namespace JW.Order.Web.Controllers
{
    [Route("api/Test/{action}", Name = "TestApi")]
    public class TestController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetSetting(string GroupName)
        {
            DataTable dt = new JW.Common.DbHelper().CreateSqlDataTable(string.Format("SELECT * FROM dbo.LogSystem where 1=1 {0} Order By DateUpdate DESC", GroupName.ExStrNotNull() ? " And BizData Like '%" + GroupName + "%'" : ""));
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(dt.ExDataTableToJson(), System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetRoleList()
        {
            DataTable dt = DB.BasicRole.GetRoleList();
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(dt.ExDataTableToJson(true), System.Text.Encoding.UTF8, "application/json");
            return response;
        }
    }
}
