using FRS.BLL.Interfaces;
using FRS.BLL.Services;
using FRS.DAL.Interfaces;
using FRS.DAO;
using Ninject.Modules;

namespace FRS.Ioc
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserDao>().To<UserDao>();
            Bind<IRegressionAnalyzeDao>().To<RegressionAnalyzeDao>();

            Bind<IUserService>().To<UserService>();
            Bind<IRegressionAnalyzeService>().To<RegressionAnalyzeService>();
            Bind<ICorrelationService>().To<CorrelationService>();
        }
    }
}