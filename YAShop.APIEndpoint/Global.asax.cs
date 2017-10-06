using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YAShop.BusinessLogic;
using YAShop.BusinessLogic.Infrastr;

namespace YAShop.APIEndpoint
{
    public class WebCommonInfrastructureProvider : ICommonInfrastructureProvider
    {
        public object GetFromSession(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public void PutInSession(string key, object subj)
        {
            HttpContext.Current.Session.Add(key, subj);
        }

        public IDictionary IdentityMap => HttpContext.Current.Items;
    }
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Registry.Init(new WebCommonInfrastructureProvider());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
