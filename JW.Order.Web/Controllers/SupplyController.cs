using JW.Order.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JW.Order.Web.Controllers
{
    public class SupplyController : Controller
    {
        // GET: Supply
        public ActionResult Index()
        {
            string name = User.Identity.Name;
            DataTable dt = DB.BasicUser.p_BasicUser_GetInfo(name, 3, 2, "Supply");
            Models.RoleDetail detail = MyUser.ConvertRoleDetail(dt);
            return View(detail);
        }

        public ActionResult SupplyList()
        {
            DataTable dt = DB.Supply.GetSupplyList();
            return Content(dt.ExDataTableToJson(true), "application/json");
        }

        public ActionResult SupplyUpdate(string SupplyID, string UserData)
        {
            bool isok = false; string msg = "";
            isok = DB.Supply.UpdateSupply(SupplyID, UserData, out msg);
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

        public ActionResult SupplyDel(string SupplyID)
        {
            bool isok = false; string msg = "";
            isok = DB.Supply.DelSupply(SupplyID, out msg);
            Response.StatusCode = isok ? 200 : 500;
            if (!isok)
            {
                Response.StatusDescription = JW.Common.MyMethod.StringToISO_8859_1(msg);
            }
            return new EmptyResult();
        }

        public ActionResult SupplyAdd(string UserData)
        {
            bool isok = false; string msg = "";
            isok = DB.Supply.AddSupply(UserData, out msg);
            Response.StatusCode = isok ? 200 : 500;
            if (!isok)
            {
                Response.StatusDescription = JW.Common.MyMethod.StringToISO_8859_1(msg);
            }
            return new EmptyResult();
        }
    }
}