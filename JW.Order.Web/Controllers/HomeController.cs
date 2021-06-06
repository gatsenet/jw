using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using JW.Order.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JW.Common;

namespace JW.Order.Web.Controllers {
    public class HomeController : Controller {

        [LoginCheckFilterAttribute(IsChecked = false)]
        public ActionResult Index() {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        private MenuItem MyMenu(DataRow dataRow, DataTable dataTable)
        {
            MenuItem menu = new MenuItem() { text = dataRow["ModuleCaption"].ExObjString(), icon = dataRow["ModuleIcon"].ExObjString(), link = dataRow["ModuleLink"].ExObjString(), beginGroup = dataRow["IsGroup"].ExObjBool() };
            if (dataTable.Rows.Cast<DataRow>().Where(p => p["ParentAutoID"].ExObjInt32() == dataRow["AutoID"].ExObjInt32() & dataRow["InUsed"].ExObjBool()).Count() > 0)
            {                
                List<MenuItem> items = new List<MenuItem>();
                foreach (DataRow row in dataTable.Rows.Cast<DataRow>().Where(p => p["ParentAutoID"].ExObjInt32() == dataRow["AutoID"].ExObjInt32() & dataRow["InUsed"].ExObjBool()))
                {
                    items.Add(MyMenu(row, dataTable));
                }
                menu.items = items;                
            }
            return menu;
        }

        [HttpGet]
        public ActionResult GetMenu(DataSourceLoadOptions loadOptions)
        {
            List<MenuItem> menuList = new List<MenuItem>();
            if (User.Identity.IsAuthenticated)
            {
                string user = User.Identity.Name;
                DataTable dt = DB.BasicUser.p_BasicUser_GetInfo(user, 3, 1);
                foreach (DataRow row in dt.Select("ParentAutoID=0 And InUsed=1"))
                {
                    menuList.Add(MyMenu(row, dt));
                }
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(menuList, loadOptions)), "application/json");
            //IEnumerable<MenuItem> Products = new[] {
            //new MenuItem {
            //    text = "Video Players",
            //    items = new[] {
            //        new MenuItem {
            //            text = "HD Video Player",
            //            price = 220,
            //            icon = "../../Content/Images/ProductsLarge/1.png"
            //        },
            //        new MenuItem {
            //            text = "SuperHD Video Player",
            //            price = 270,
            //            icon = "../../Content/Images/ProductsLarge/2.png"
            //        }
            //    }
            //},
            //new MenuItem {
            //    text = "Televisions",
            //    items = new[] {
            //        new MenuItem {
            //            text = "SuperLCD 42",
            //            price = 1200,
            //            icon = "../../Content/Images/ProductsLarge/7.png"
            //        },
            //        new MenuItem {
            //            text = "SuperLED 42",
            //            price = 1450,
            //            icon = "../../Content/Images/ProductsLarge/5.png"
            //        },
            //        new MenuItem {
            //            text = "SuperLED 50",
            //            price = 1600,
            //            icon = "../../Content/Images/ProductsLarge/4.png"
            //        },
            //        new MenuItem {
            //            text = "SuperLCD 55 (Not available)",
            //            price = 1350,
            //            icon = "../../Content/Images/ProductsLarge/6.png",
            //            disabled = true
            //        },
            //        new MenuItem {
            //            text = "SuperLCD 70",
            //            price = 4000,
            //            icon = "../../Content/Images/ProductsLarge/9.png"
            //        }
            //    }
            //},
            //new MenuItem {
            //    text = "Monitors",
            //    items = new[] {
            //        new MenuItem {
            //            text = "19\"",
            //            items = new[] {
            //                new MenuItem {
            //                    text = "DesktopLCD 19",
            //                    price = 160,
            //                    icon = "../../Content/Images/ProductsLarge/10.png"
            //                }
            //            }
            //        },
            //        new MenuItem {
            //            text = "21\"",
            //            items = new[] {
            //                new MenuItem {
            //                    text = "DesktopLCD 21",
            //                    price = 170,
            //                    icon = "../../Content/Images/ProductsLarge/12.png"
            //                },
            //                new MenuItem {
            //                    text = "DesktopLED 21",
            //                    price = 175,
            //                    icon = "../../Content/Images/ProductsLarge/13.png"
            //                }
            //            }
            //        }
            //    }
            //},
            //new MenuItem {
            //    text = "Projectors",
            //    items = new[] {
            //        new MenuItem {
            //            text = "Projector Plus",
            //            price = 550,
            //            icon = "../../Content/Images/ProductsLarge/14.png"
            //        },
            //        new MenuItem {
            //            text = "Projector PlusHD",
            //            price = 750,
            //            icon = "../../Content/Images/ProductsLarge/15.png"
            //        }
            //    }
            //} };

            //return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Products, loadOptions)), "application/json");
        }

        public ActionResult Setting()
        {
            string name = User.Identity.Name;
            DataTable dt = DB.BasicUser.p_BasicUser_GetInfo(name, 3, 2, "BasicSetting");
            Models.RoleDetail detail = MyUser.ConvertRoleDetail(dt);
            return View(detail);
        }

        public ActionResult SettingList()
        {
            DataTable dt = DB.BasicSetting.GetSettingList();
            return Content(dt.ExDataTableToJson(), "application/json");
        }

        public ActionResult SettingUpdate(string SettingID,string UserData)
        {
            bool isok = false; string msg = "";
            isok = DB.BasicSetting.UpdateSetting(SettingID,UserData, out msg);
            Response.StatusCode = isok ? 200 : 500;
            if (!isok)
            {
                Response.StatusDescription = JW.Common.MyMethod.StringToISO_8859_1(msg);
            }
            return new EmptyResult();
        }

        public ActionResult SysLog()
        {
            string name = User.Identity.Name;
            DataTable dt = DB.BasicUser.p_BasicUser_GetInfo(name, 3, 2, "SysLog");
            Models.RoleDetail detail = MyUser.ConvertRoleDetail(dt);
            return View(detail);
        }

        public ActionResult LogTypeList()
        {
            DataTable dt = DB.LogSystem.GetLogType();
            return Content(dt.ExDataTableToJson(), "application/json");
        }

        public ActionResult LogActionList()
        {
            DataTable dt = DB.LogSystem.GetLogAction();
            return Content(dt.ExDataTableToJson(), "application/json");
        }

        public ActionResult GetLogList(string dateb, string datee, string logtype, string logaction,int result, string like)
        {
            DataTable dt = DB.LogSystem.GetInfo(dateb, datee, logtype, logaction, result, like);
            return Content(dt.ExDataTableToJson(), "application/json");
        }
    }
}