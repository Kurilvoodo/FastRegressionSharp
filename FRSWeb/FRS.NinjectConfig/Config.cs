using FRS.BLL.Interfaces;
using FRS.BLL.Services;
using FRS.DAL.Interfaces;
using FRS.DAO;
using Ninject;

namespace FRS.NinjectConfig
{
    public class Config
    {
        public static void RegisterServices(IKernel kernel)
        {
            kernel
                .Bind<IUserDao>().
                To<UserDao>()
                .InSingletonScope();
            kernel
                .Bind<IRegressionAnalyzeDao>()
                .To<RegressionAnalyzeDao>()
                .InSingletonScope();
            kernel
                .Bind<IUserService>()
                .To<UserService>()
                .InSingletonScope();
            kernel
                .Bind<IRegressionAnalyzeService>()
                .To<RegressionAnalyzeService>()
                .InSingletonScope();
            kernel
                .Bind<ICorrelationService>().
                To<CorrelationService>()
                .InSingletonScope();
        }
    }
}
