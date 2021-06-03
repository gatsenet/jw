using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using JW.Common;
using System.Security.Principal;

namespace JW.Order.Web {

    public class MvcApplication : HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DevExtremeBundleConfig.RegisterBundles(BundleTable.Bundles);
            InitSys();
        }

        private void InitSys()
        {
            MyProperty.SqlConnetString = MyProperty.SqlConnetStringByWeb;
            JW.Common.MyProperty.LoggerPath = JW.Common.WebConfig.ReadAppSetting("LoggerPath");
            JW.Common.MyProperty.LoggerIsOpen = JW.Common.WebConfig.ReadAppSetting("LoggerIsOpen").ExObjBool();
        }

        public MvcApplication()
        {
            AuthorizeRequest += new EventHandler(MvcApplication_AuthorizeRequest);
        }

        void MvcApplication_AuthorizeRequest(object sender, EventArgs e)
        {
            IIdentity id = Context.User.Identity;
            if (id.IsAuthenticated)
            {
                var roles = new Repository.UserRepository().GetRoles(id.Name);
                Context.User = new GenericPrincipal(id, roles);
            }
        }
    }
}
