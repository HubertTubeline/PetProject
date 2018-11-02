using Android.App;
using PetProject.Common.Interfaces;
using PetProject.Common.Models;
using PetProject.Common.Services;

namespace PetProject.Presenters
{
    public class BasePresenter
    {
        protected static IUserService UserService = new UserService("database");
        protected static IScoresService ScoresService = new ScoresService("database");

        protected static UserModel User { get; set; }
        protected Activity Activity { get; set; }
    }
}