using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NewsApp.Configuration;
using NewsApp.Controllers;
using NewsApp.Notifications;
using NewsAppModel.Infrastructure;
using Ninject;
using Ninject.Web.Common;

namespace NewsApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var nv = new NameValueCollection { ConfigurationManager.AppSettings };
            for (var i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                nv.Add(ConfigurationManager.ConnectionStrings[i].Name,
                    ConfigurationManager.ConnectionStrings[i].ConnectionString);
            }
            AppSettings.Init(nv);
            
            AppController.Start();
            //DependencyResolver.SetResolver(new NinjectMvcDiResolver(Factory.kernel));
        }

        protected override IKernel CreateKernel()
        {
            Factory.kernel = new StandardKernel();
            Factory.kernel.Load(Assembly.GetAssembly(typeof(HomeController)));

            return Factory.kernel;
        }

        protected void Application_Error()
        {
            var err = Server.GetLastError();
            Console.WriteLine(Request.Form);
        }
        protected void Application_Stop()
        {
            AppController.End();
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader(
                        "Access-Control-Allow-Origin", "*");
            /* HttpContext.Current.Response.AddHeader(
      "Access-Control-Allow-Origin", 
      "http://AllowedDomain.com"); */
        }
    }
}