using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using JW.Order.Web.Models;
using JW.Order.Web.Repository;
using JW.Common;
using Newtonsoft.Json.Linq;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using System.Data;

namespace JW.Order.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AccountController : Controller
    {
        private UserRepository repository = new UserRepository();

        [AllowAnonymous]
        [LoginCheckFilterAttribute(IsChecked = false)]
        public ActionResult LogOn()
        {
            Models.LogOnModel logOn = new LogOnModel();
            logOn.ReturnUrl = Request.QueryString["ReturnUrl"].ExObjString();
            return View(logOn);
        }

        [AllowAnonymous]
        [LoginCheckFilterAttribute(IsChecked = false)]
        [HttpPost]
        public ActionResult LogOn(Models.LogOnModel model, string ReturnUrl)
        {
            
            if (ModelState.IsValid)
            {
                string msg = "";
                if (repository.ValidateUser(model.UserName, model.Password, out msg))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    Session["loginUser"] = model.UserName;
                    if (!String.IsNullOrEmpty(ReturnUrl)) return Redirect(ReturnUrl);
                    else return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", msg);
            }
            return View(model);
        }

        [AllowAnonymous]
        [LoginCheckFilterAttribute(IsChecked = false)]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Remove("loginUser");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ToChangePassword(string pw1,string pw2)
        {
            bool isok = false;string msg = "";
            string name = User.Identity.Name;
            if(name.ExStrNotNull() & pw1.ExStrNotNull() & pw2.ExStrNotNull() & pw1 == pw2)
            {
                JObject jObject = new JObject { { "pw", pw1 } };
                isok = DB.BasicUser.p_BasicUser_Update(name, 0, jObject.ToString(), out msg);
            }
            else
            {
                msg = "用户名为空或重复密码校验失败";
            }
            return Json(new { status = isok, msg = msg });
        }

        //public ActionResult EditUser()
        //{
        //    //ViewData["dt"] = Dal.UserGetList();
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult UserDel(string name)
        //{
        //    bool isok = false; string msg = "";
        //    //isok = Dal.UserToDel(name, out msg);
        //    isok = true;
        //    return Json(new { status = isok, msg = msg });
        //}

        //public ActionResult AddUser() 
        //{
        //    LogOnModel logOn = new LogOnModel();
        //    return View(logOn);
        //}

        //[HttpPost]
        //public ActionResult AddUser(LogOnModel logOn,string roles)
        //{
        //    bool isok = false; string msg = "";
        //    //isok = Dal.UserToAdd(logOn.UserName, logOn.Password, roles, out msg);
        //    isok = true;
        //    if (isok)
        //    {
        //        return RedirectToAction("EditUser", "Account");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", msg);
        //        return View(logOn);
        //    }
        //}

        public ActionResult Role()
        {
            string name = User.Identity.Name;
            DataTable dt = DB.BasicUser.p_BasicUser_GetInfo(name, 3, 2, "Role");
            Models.RoleDetail detail = MyUser.ConvertRoleDetail(dt);
            return View(detail);
        }

        [HttpGet]
        public ActionResult GetRoleList(DataSourceLoadOptions loadOptions)
        {
            string name = User.Identity.Name;
            DataTable dt = DB.BasicRole.GetRoleList();
            return Content(dt.ExDataTableToJson(), "application/json");
            //return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SampleData.Products, loadOptions)), "application/json");
        }

        [HttpPost]
        public ActionResult DelRole(string roleid)
        {
            bool isok = false; string msg = "";
            isok = DB.BasicRole.DelRole(roleid, out msg);
            return Json(new { status = isok, msg = msg });
        }

        public ActionResult RoleDetail(string id)
        {
            string name = User.Identity.Name;
            DataTable dt = DB.BasicUser.p_BasicUser_GetInfo(name, 3, 2, "Role");
            Models.RoleDetail detail = MyUser.ConvertRoleDetail(dt);
            string type = Request.QueryString["type"].ExObjString().ToLower();
            if (type == "add")
            {
                if (!detail.InAdd)
                    return RedirectToAction("Role");
                else
                {
                    ViewData["IsNew"] = true;
                    ViewData["jsonRole"] = DB.BasicRole.GetRoleList("").ExDataTableToJson();
                    ViewData["jsonDetail"] = DB.BasicRole.GetRoleDetailList("").ExDataTableToJson();
                    return View(detail);
                }
            }
            else
            {
                if (id.ExStrNotNull())
                {
                    ViewData["IsNew"] = false;
                    ViewData["jsonRole"] = DB.BasicRole.GetRoleList(id).ExDataTableToJson();
                    ViewData["jsonDetail"] = DB.BasicRole.GetRoleDetailList(id).ExDataTableToJson();
                    return View(detail);
                }
                else
                {
                    return RedirectToAction("Role");
                }
            }
        }

        [HttpPost]
        public ActionResult SaveRole(string roleid, bool isnew, string datajson)
        {
            bool isok = false; string msg = "";
            isok = isnew ? DB.BasicRole.AddRole(roleid, datajson, out msg) : DB.BasicRole.UpdateRole(roleid, datajson, out msg);
            return Json(new { status = isok, msg = msg });
        }

        public ActionResult UserList()
        {
            string name = User.Identity.Name;
            DataTable dt = DB.BasicUser.p_BasicUser_GetInfo(name, 3, 2, "User");
            Models.RoleDetail detail = MyUser.ConvertRoleDetail(dt);
            return View(detail);
        }

        public ActionResult GetUserList()
        {
            DataSet ds = DB.BasicUser.p_BasicUser_GetList();
            List<BasicUser> list = MyUser.ConvertBasicUser(ds);
            return Content(list.ExArrayToJson(), "application/json");
        }

        public ActionResult UserUpdate(string UserID,string UserData)
        {
            bool isok = false; string msg = "";
            isok = DB.BasicUser.p_BasicUser_Update(UserID, 2, UserData, out msg);
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

        public ActionResult UserDel(string UserID)
        {
            bool isok = false; string msg = "";
            isok = DB.BasicUser.p_BasicUser_Update(UserID, 3, "", out msg);
            Response.StatusCode = isok ? 200 : 500;
            if (!isok)
            {
                Response.StatusDescription = JW.Common.MyMethod.StringToISO_8859_1(msg);
            }
            return new EmptyResult();
        }

        public ActionResult UserAdd(string UserData)
        {
            bool isok = false; string msg = "";
            isok = DB.BasicUser.p_BasicUser_Update("", 1, UserData, out msg);
            Response.StatusCode = isok ? 200 : 500;
            if (!isok)
            {
                Response.StatusDescription = JW.Common.MyMethod.StringToISO_8859_1(msg);
            }
            return new EmptyResult();
        }

        public JsonResult CheckUserID(string UserID)
        {
            bool isValid = false;
            return Json(new { isValid, message = "你的失败" });
        }

        //// **************************************
        //// URL: /Account/Register
        //// **************************************

        //public ActionResult Register()
        //{
        //    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Attempt to register the user
        //        MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

        //        if (createStatus == MembershipCreateStatus.Success)
        //        {
        //            FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
        //    return View(model);
        //}

        //// **************************************
        //// URL: /Account/ChangePassword
        //// **************************************

        //[Authorize]
        //public ActionResult ChangePassword()
        //{
        //    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
        //    return View();
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult ChangePassword(ChangePasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
        //        {
        //            return RedirectToAction("ChangePasswordSuccess");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
        //    return View(model);
        //}

        //// **************************************
        //// URL: /Account/ChangePasswordSuccess
        //// **************************************

        //public ActionResult ChangePasswordSuccess()
        //{
        //    return View();
        //}
    }
}