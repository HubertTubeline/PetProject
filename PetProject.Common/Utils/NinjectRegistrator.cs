using System.Collections.Generic;
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
        private static readonly Dictionary<string, StandardKernel> Kernels = new Dictionary<string, StandardKernel>();

        public static StandardKernel GetKernel(string databaseName = "database")
        {
            if (Kernels.ContainsKey(databaseName)) return Kernels[databaseName];

            NinjectModule registrations = new ServiceModule(databaseName);

            var kernel = new StandardKernel(registrations);
            Kernels.Add(databaseName, kernel);

            return kernel;
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
