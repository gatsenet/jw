using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
//using JW.Order.Web.Models;
using JW.Common;

namespace JW.Order.Web.Repository
{
    public class UserRepository
    {
        //private static User[] usersForTest = new[]{
        //new User{ ID = 1, Name = "001", Password = "001", Roles = new []{"user"}},
        //new User{ ID = 2, Name = "tom", Password = "tom", Roles = new []{"user"}},
        //new User{ ID = 3, Name = "admin", Password = "admin", Roles = new[]{"admin"}},
        //};

        public bool ValidateUser(string userName, string password, out string msg)
        {
            msg = "";           
            return DB.BasicUser.p_BasicUser_Verify(userName, password, out msg);
            //return usersForTest
            //.Any(u => u.Name == userName && u.Password == password);
        }

        public string[] GetRoles(string userName)
        {
            //DataTable dt = Dal.UserGetOne(userName);
            //if (dt.ExDataTableNotNullEmpty())
            //{
            //    return new string[] { dt.Rows[0]["Roles"].ExObjString() };
            //}
            //else
            //{
                return new string[] { "admin" };
            //}
            //return usersForTest
            //    .Where(u => u.Name == userName)
            //    .Select(u => u.Roles)
            //    .FirstOrDefault();
        }

        //public User GetByNameAndPassword(string name, string password)
        //{
        //    return usersForTest
        //        .FirstOrDefault(u => u.Name == name && u.Password == password);
        //}
    }
}