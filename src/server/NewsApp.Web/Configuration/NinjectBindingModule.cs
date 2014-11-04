using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NewsApp.Model;
using NewsApp.Notifications;
using NewsAppModel.Services;
using NewsAppModel.Services.Providers;
using NHibernate;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Infrastructure;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using Ninject.Web.Common;

namespace NewsApp.Configuration
{
    public class NinjectBindingModule : NinjectModule
    {
        public override void Load()
        {
            Debug.WriteLine(Kernel.GetBindings().Count());
            Debug.WriteLine(Kernel.GetBindings());
            IEnumerable<Assembly> lst =
                AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("NewsApp"));
            Kernel.Bind(m => m.From(lst).SelectAllClasses().BindDefaultInterfaces().Configure(c => c.InRequestScope()));
            Bind<NotificationService>().ToSelf().InRequestScope();
            Bind<UserService>().ToSelf().InRequestScope();
            Bind<IDeviceProvider>().To<AppleNotifier>().InSingletonScope();
            Bind<ISession>().ToMethod(m => NHibernateSessionProvider.GetSession());
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