using Ninject;

namespace FRS.Ioc
{
    public class NinjaDependencyResolver
    {
        private static NinjectBindings _bindings = new NinjectBindings();
        public static StandardKernel Kernel = new StandardKernel(_bindings);
    }
}