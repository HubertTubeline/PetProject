using Android.App;
using PetProject.Common.Interfaces;
using PetProject.Common.Models;
using PetProject.Common.Services;

namespace PetProject.Presenters
{
    public class BasePresenter
    {
        protected static IUserService UserService = new UserService();
        protected static IScoresService ScoresService = new ScoresService();

        protected static UserModel User { get; set; }
        protected Activity Activity { get; set; }
    }
}