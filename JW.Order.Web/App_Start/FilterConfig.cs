using System.Web;
using System.Web.Mvc;

namespace JW.Order.Web {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
           // 校验用户是否登陆，默认为校验
             filters.Add(new Models.LoginCheckFilterAttribute() { IsChecked = true });
        }
    }
}
