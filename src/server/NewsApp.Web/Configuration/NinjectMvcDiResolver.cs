using System.Web.Mvc;
using Ninject;

namespace NewsApp.Configuration
{
    public class NinjectMvcDiResolver : NinjectDependencySCope, IDependencyResolver
    {
        public NinjectMvcDiResolver(IKernel kernel)
            : base(kernel)
        {
        }
    }
}