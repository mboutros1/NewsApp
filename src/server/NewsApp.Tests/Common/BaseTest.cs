using System;
using System.Linq;
using NewsApp.Model;

namespace NewsApp.Tests.Common
{
    public class BaseTest:IDisposable
    {
        public static T First<T>()
        {
            return Factory.Get<IRepository<T>>()
                .All()
                .FirstOrDefault();
        }

        public static T First<T>(Func<T, bool> where)
        {
            return Factory.Get<IRepository<T>>()
                .All()
                .FirstOrDefault(where);
        }

        public static T G<T>()
        {
            return Factory.Get<T>();
        }

        public static IQueryable<T> Q<T>()
        {
            return Factory.Get<IRepository<T>>()
                .All();
        }

        public static IRepository<T> R<T>()
        {
            return Factory.Get<IRepository<T>>();
        }

        public virtual void Dispose()
        {

        }
    }
}