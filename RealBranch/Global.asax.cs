using RealBranch.App_Start;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RealBranch
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
                                                     //WebApiConfig.Register(GlobalConfiguration.Configuration); // DEPRECATED

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AreaRegistration.RegisterAllAreas();

            // Manually installed WebAPI 2.2 after making an MVC project.
          
    
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
     {
       //Create culture info object 
       CultureInfo ci = new CultureInfo("ua");
            CultureInfo ci2 = new CultureInfo("ua");
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci2;
       System.Threading.Thread.CurrentThread.CurrentCulture = 
CultureInfo.CreateSpecificCulture(ci.Name);
     }
}
}
