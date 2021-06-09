using JW.Order.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JW.Common;

namespace JW.Order.Web.Controllers
{
    public class GoodsController : Controller
    {
        // GET: Goods
        public ActionResult Index()
        {
            string name = User.Identity.Name;
            DataTable dt = DB.BasicUser.p_BasicUser_GetInfo(name, 3, 2, "Goods");
            Models.RoleDetail detail = MyUser.ConvertRoleDetail(dt);
            return View(detail);
        }

        public ActionResult Weight()
        {
            string name = User.Identity.Name;
            DataTable dt = DB.BasicUser.p_BasicUser_GetInfo(name, 3, 2, "GoodsWeight");
            Models.RoleDetail detail = MyUser.ConvertRoleDetail(dt);
            return View(detail);
        }

        public ActionResult GetWeightList()
        {
            DataTable dt = DB.Goods.GoodsWeightList();
            return Content(dt.ExDataTableToJson(), "application/json");
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, string chunkMetadata)
        {

            // Removes temporary files
            //RemoveTempFilesAfterDelay(tempPath, new TimeSpan(0, 5, 0));
            var myFile = Request.Files["myFile"];
            var targetLocation = Server.MapPath("~/Content/Files/");
            bool isok = false; string msg = "";
            try
            {
                MyMethod.CheckFileExtensionValid(myFile.FileName);
                var path = Path.Combine(targetLocation, myFile.FileName);
                string fileType = Path.GetExtension(myFile.FileName);
                Common.Xls.ExcelHelper excelHelper = new Common.Xls.ExcelHelper();
                string[] cols = DB.BasicSetting.ImportXls_GoodsWeight.Split(new char[] { ',' });
                DataTable dt = excelHelper.ExcelImport(myFile.InputStream, fileType);
                if (dt.ExDataTableNotNullEmpty())
                {
                    if (MyMethod.IsCheckCol(dt.Columns, cols))
                    {
                        for (int i = dt.Columns.Count - 1; i >= 0; i--)
                        {
                            if (!cols.Contains(dt.Columns[i].ColumnName))
                            {
                                dt.Columns.RemoveAt(i);
                            }
                        }
                        isok = DB.Goods.GoodsWeightUpdate(dt.ExDataTableToJson(), out msg);                        
                    }
                    else
                    {
                        isok = false; msg = "导入文档字段不完整";
                    }
                }
                else
                {
                    isok = false; msg = "导入文档无数据";
                }

                //Uncomment to save the file
                //myFile.SaveAs(path);
            }
            catch (Exception ex)
            {
                //Response.StatusCode = 400;
                isok = false; msg = ex.Message;
            }
            Response.StatusCode = isok ? 200 : 500;
            Response.StatusDescription = MyMethod.StringToISO_8859_1(msg);
            //Response.HeaderEncoding = Encoding.UTF8;
            //HttpStatusCodeResult httpStatusCodeResult = new HttpStatusCodeResult(isok ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, msg);
            //return httpStatusCodeResult;
            return new EmptyResult();
        }

        
    }
}