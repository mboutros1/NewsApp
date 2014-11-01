using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NewsApp.Configuration;
using NewsAppModel.Infrastructure;
using Ninject;

namespace NewsApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Factory.kernel = new StandardKernel(new NinjectBindingModule());
            var nv = new NameValueCollection { ConfigurationManager.AppSettings };
            for (var i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                nv.Add(ConfigurationManager.ConnectionStrings[i].Name,
                    ConfigurationManager.ConnectionStrings[i].ConnectionString);
            }
            AppSettings.Init(nv);
            DependencyResolver.SetResolver(new NinjectMvcDiResolver(Factory.kernel));
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.AddHeader(
            //            "Access-Control-Allow-Origin", "*");  
            /* HttpContext.Current.Response.AddHeader(
      "Access-Control-Allow-Origin", 
      "http://AllowedDomain.com"); */
        }
    }
}