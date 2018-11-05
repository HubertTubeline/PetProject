using Android.App;
using Ninject;
using PetProject.Common.Interfaces;
using PetProject.Common.Models;
using PetProject.Common.Utils;

namespace PetProject.Presenters
{
    public class BasePresenter
    {
        protected static readonly IUserService UserService;
        protected static readonly IScoresService ScoresService;

        protected static UserModel User;
        protected Activity Activity;

        static BasePresenter()
        {
            var kernel = NinjectRegistrator.GetKernel();
            UserService = kernel.Get<IUserService>();
            ScoresService = kernel.Get<IScoresService>();
        }
    }
}