using PetProject.Common.Models;
using PetProject.DAL.Entities;

namespace PetProject.Common.Helpers
{
    public static class Mapper
    {
        public static User MapUser(UserModel model)
        {
            if (model == null) return null;
            return new User
            {
                UserName = model.UserName,
                FlappyMaxScore = model.FlappyMaxScore,
                RaceMaxScore = model.RaceMaxScore
            };
        }

        public static UserModel MapUser(User model)
        {
            if (model == null) return null;
            return new UserModel
            {
                UserName = model.UserName,
                RaceMaxScore = model.RaceMaxScore,
                FlappyMaxScore = model.FlappyMaxScore
            };
        }
    }
}