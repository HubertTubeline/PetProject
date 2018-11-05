using System.Collections.Generic;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using PetProject.Common.Interfaces;
using PetProject.Common.Services;
using PetProject.DAL.Interfaces;
using PetProject.DAL.Repositories;

namespace PetProject.Common.Utils
{
    public static class NinjectRegistrator
    {
        private static readonly Dictionary<string, IKernel> Kernels = new Dictionary<string, IKernel>();

        public static IKernel GetKernel(string databaseName = "database")
        {
            if (Kernels.ContainsKey(databaseName)) return Kernels[databaseName];

            RegisterKernel(databaseName);
            return Kernels[databaseName];
        }

        private static async void RegisterKernel(string databaseName)
        {
            NinjectModule registrations = new ServiceModule(databaseName);

            var kernel = new StandardKernel(registrations);
            Kernels.Add(databaseName, kernel);
        }
    }

    public class ServiceModule : NinjectModule
    {
        private string _databaseName;

        public ServiceModule(string databaseName)
        {
            _databaseName = databaseName;
        }

        public override void Load()
        {
            Bind<IScoresService>().To<ScoresService>().WithConstructorArgument(_databaseName);
            Bind<IUserService>().To<UserService>().WithConstructorArgument(_databaseName);
            Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument(_databaseName);
        }
    }
}
