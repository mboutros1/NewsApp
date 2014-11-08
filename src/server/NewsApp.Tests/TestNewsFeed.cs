using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Data;
using NewsApp.Model;
using NewsApp.Notifications;
using NewsAppModel.Infrastructure;
using NewsAppModel.Model;
using NewsAppModel.Services;
using NewsAppModel.Services.Providers;
using NHibernate;
using Ninject;
using Ninject.Infrastructure;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using Ninject.Extensions;
using Ninject.Extensions.Conventions;
using NLog;
using NUnit.Framework;

namespace NewsApp.Tests
{
    [SetUpFixture]
    public class TestNewsFeed
    {
        [SetUp]
        public void SetThisUp()
        {
            try
            {
                LogManager.GetCurrentClassLogger();
                Factory.kernel = new StandardKernel(new NinjectBindingModule());
                var nv = new NameValueCollection { ConfigurationManager.AppSettings };
                for (var i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
                {
                    nv.Add(ConfigurationManager.ConnectionStrings[i].Name,
                        ConfigurationManager.ConnectionStrings[i].ConnectionString);
                }
                AppSettings.Init(nv);
                
            }
            catch (Exception e)
            {
                throw;
            }
        }


    }
    public class Factory
    {
        public static StandardKernel kernel;

        public static T Get<T>()
        {
            return kernel.Get<T>();
        }
    }
    public class NinjectBindingModule : NinjectModule
    {
        public override void Load()
        {
            Debug.WriteLine(Kernel.GetBindings().Count());
            Debug.WriteLine(Kernel.GetBindings());
            IEnumerable<Assembly> lst =
                AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("NewsApp"));
            Kernel.Bind(m => m.From(lst).SelectAllClasses().BindDefaultInterfaces().Configure(c => c.InThreadScope()));
            Bind<NotificationService>().ToSelf().InThreadScope();
            Bind<UserService>().ToSelf().InThreadScope();
            Bind<IDeviceProvider>().To<AppleNotifier>().InSingletonScope();
            Bind<ISession>().ToMethod(m => NHibernateSessionProvider.GetSession()).InThreadScope();
            Rebind<INewsFeedRepository>().To<NewsFeedRepository>().InThreadScope();
        }
    }

    public static class Extension
    {
        public static Type[] GetBindings(this IKernel kernel)
        {
            return ((Multimap<Type, IBinding>)typeof(KernelBase)
                .GetField("bindings", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(kernel)).Select(x => x.Key).ToArray();
        }
    }
}
