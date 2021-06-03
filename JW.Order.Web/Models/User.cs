using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel;
using JW.Common;

namespace JW.Order.Web.Models
{
    public class MyUser
    {        
        public static RoleDetail ConvertRoleDetail(DataTable dt)
        {
            RoleDetail rd = new RoleDetail();
            if (dt.ExDataTableNotNullEmpty())
            {
                rd.ModuleID = dt.Rows[0]["ModuleID"].ExObjString();
                rd.ModuleName = dt.Rows[0]["ModuleName"].ExObjString();
                rd.ModuleLink = dt.Rows[0]["ModuleLink"].ExObjString();
                rd.IsMenu = dt.Rows[0]["IsMenu"].ExObjBool();
                rd.InAdd = dt.Rows[0]["InAdd"].ExObjBool();
                rd.InDel = dt.Rows[0]["InDel"].ExObjBool();
                rd.InPrint = dt.Rows[0]["InPrint"].ExObjBool();
                rd.InQuery = dt.Rows[0]["InQuery"].ExObjBool();
                rd.InSave = dt.Rows[0]["InSave"].ExObjBool();
                rd.InUsed = dt.Rows[0]["InUsed"].ExObjBool();
            }
            return rd;
        }

        public static List<BasicUser> ConvertBasicUser(DataSet ds)
        {
            List<BasicUser> list = new List<BasicUser>();
            if (ds.ExDataSetNotNullEmpty())
            {
                BasicUser user;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    user = new BasicUser();
                    user.UserID = row["UserID"].ExObjString();
                    user.UserName = row["UserName"].ExObjString();
                    user.UserPS = row["UserPS"].ExObjString();
                    user.RoleID = row["RoleID"].ExObjString();
                    user.IsStop = row["IsStop"].ExObjBool();
                    user.IsAdmin = row["IsAdmin"].ExObjBool();
                    user.IsAllCustomer = row["IsAllCustomer"].ExObjBool();
                    user.IsAllSupply = row["IsAllSupply"].ExObjBool();
                    user.CustList = ds.Tables[1].Rows.Cast<DataRow>().Where(p => p["UserID"].ExObjString() == user.UserID).Select(p => p["UnitID"].ExObjString()).ToArray();
                    user.SupplyList= ds.Tables[2].Rows.Cast<DataRow>().Where(p => p["UserID"].ExObjString() == user.UserID).Select(p => p["UnitID"].ExObjString()).ToArray();
                    list.Add(user);
                }
            }
            return list;
        }
    }

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }

    public class MenuItem
    {
        public string text { get; set; }
        public bool disabled { get; set; }
        public string icon { get; set; }
        //public int price { get; set; }
        public bool beginGroup { get; set; }

        public string link { get; set; }
        public IEnumerable<MenuItem> items { get; set; }
    }

    public class RoleDetail
    {
        [DefaultValue("")]
        public string ModuleID { get; set; }
        [DefaultValue("")]
        public string ModuleName { get; set; }
        [DefaultValue("")]
        public string ModuleLink { get; set; }
        [DefaultValue(true)]
        public bool IsMenu { get; set; }
        [DefaultValue(false)]
        public bool InUsed { get; set; }
        [DefaultValue(false)]
        public bool InAdd { get; set; }
        //public bool InEdit { get; set; }
        [DefaultValue(false)]
        public bool InSave { get; set; }
        [DefaultValue(false)]
        public bool InDel { get; set; }
        [DefaultValue(false)]
        public bool InQuery { get; set; }
        [DefaultValue(false)]
        public bool InPrint { get; set; }

    }

    public class BasicUser
    {
        [DefaultValue("")]        
        public string UserID { get; set; }

        [DefaultValue("")]
        public string UserName { get; set; }

        [DefaultValue("")]
        public string UserPS { get; set; }

        [DefaultValue("")]
        public string RoleID { get; set; }

        [DefaultValue(false)]
        public bool IsStop { get; set; }

        [DefaultValue(false)]
        public bool IsAdmin { get; set; }

        [DefaultValue(false)]
        public bool IsAllCustomer { get; set; }

        [DefaultValue(false)]
        public bool IsAllSupply { get; set; }

        [DefaultValue(new string[] { })]
        public string[] CustList { get; set; }

        [DefaultValue(new string[] { })]
        public string[] SupplyList { get; set; }
    }
}