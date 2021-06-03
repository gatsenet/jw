using JW.Order.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JW.Order.Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            string name = User.Identity.Name;
            DataTable dt = DB.BasicUser.p_BasicUser_GetInfo(name, 3, 2, "Customer");
            Models.RoleDetail detail = MyUser.ConvertRoleDetail(dt);
            return View(detail);
        }

        public ActionResult CustList()
        {
            DataTable dt = DB.Customer.GetCustList();
            return Content(dt.ExDataTableToJson(true), "application/json");
        }

        public ActionResult CustUpdate(string CustID, string UserData)
        {
            bool isok = false; string msg = "";
            isok = DB.Customer.UpdateCust(CustID, UserData, out msg);
            Response.StatusCode = isok ? 200 : 500;
            if (!isok)
            {
                Response.StatusDescription = JW.Common.MyMethod.StringToISO_8859_1(msg);
            }
            //Response.HeaderEncoding = Encoding.UTF8;
            //HttpStatusCodeResult httpStatusCodeResult = new HttpStatusCodeResult(isok ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, msg);
            //return httpStatusCodeResult;
            return new EmptyResult();
        }

        public ActionResult CustDel(string CustID)
        {
            bool isok = false; string msg = "";
            isok = DB.Customer.DelCust(CustID, out msg);
            Response.StatusCode = isok ? 200 : 500;
            if (!isok)
            {
                Response.StatusDescription = JW.Common.MyMethod.StringToISO_8859_1(msg);
            }
            return new EmptyResult();
        }

        public ActionResult CustAdd(string UserData)
        {
            bool isok = false; string msg = "";
            isok = DB.Customer.AddCust(UserData, out msg);
            Response.StatusCode = isok ? 200 : 500;
            if (!isok)
            {
                Response.StatusDescription = JW.Common.MyMethod.StringToISO_8859_1(msg);
            }
            return new EmptyResult();
        }
    }
}