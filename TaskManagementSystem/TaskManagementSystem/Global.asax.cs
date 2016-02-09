using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Web.Optimization;
namespace TaskManagementSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
       // public object Database { get; private set; } // this will not let use Database.SetInitializer

        protected void Application_Start()
        {
            Database.SetInitializer<TaskManagementSystem.Models.TaskManagementSystemDB>(null);
           // System.Data.Entity.Database.SetInitializer<TaskManagementSystem.Models.TaskManagementSystemDB>(null);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
