using System.Web;
using System.Web.Mvc;

namespace JW.Order.Web {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
           // У���û��Ƿ��½��Ĭ��ΪУ��
             filters.Add(new Models.LoginCheckFilterAttribute() { IsChecked = true });
        }
    }
}
