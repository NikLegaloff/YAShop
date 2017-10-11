using System.Collections;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YAShop.BusinessLogic;
using YAShop.BusinessLogic.Infrastr;

namespace YASop.AdminUI
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

    public class MvcApplication : HttpApplication
    {

        protected void Application_Start()
        {
            Registry.Init(new WebCommonInfrastructureProvider());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}