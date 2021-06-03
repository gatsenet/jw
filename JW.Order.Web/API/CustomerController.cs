using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JW.Order.Web.API
{
    [Route("api/Customer/{action}", Name = "CustomerApi")]
    public class CustomerController : ApiController
    {
        public HttpResponseMessage CustList()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            DataTable dt = DB.Customer.GetCustList();
            response.Content = new StringContent(dt.ExDataTableToJson(), System.Text.Encoding.UTF8, "application/json");
            return response;
        }
    }
}
