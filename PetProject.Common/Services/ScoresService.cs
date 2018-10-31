using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PetProject.Common.Interfaces;
using PetProject.DAL.Entities;
using PetProject.DAL.Interfaces;
using PetProject.DAL.Repositories;

namespace PetProject.Common.Services
{
    public class ScoresService : IScoresService
    {
        private IUserRepository _repository;

        public ScoresService()
        {
            _repository = new UserRepository();
        }

        public int GetScore(string userName)
        {
            var user = _repository.Get(userName);
            return user.MaxScore;
        }

        public Dictionary<string,int> GetScores()
        {
            var users = _repository.Get();
            var list = new Dictionary<string,int>();
            foreach (var user in users)
            {
                list.Add(user.UserName, user.MaxScore);
            }
            
            return list;
        }

        public bool SaveScore(string userName, int score)
        {
            try
            {
                var user = _repository.Get(userName);
                if (score > user.MaxScore)
                {
                    user.MaxScore = score;
                    _repository.Update(user);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
    }
}

