using Ninject;

namespace NewsApp.Configuration
{
    public class Factory
    {
        public static StandardKernel kernel;

        public static T Get<T>()
        {
            return kernel.Get<T>();
        }
    }
}