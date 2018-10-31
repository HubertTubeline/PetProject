using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PetProject.Common.Helpers;
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

        public int GetScore(string userName, GameType type)
        {
            var user = _repository.Get(userName);
            if (user == null) return 0;

            switch (type)
            {
                case GameType.Flappy:
                    return user.FlappyMaxScore;
                    
                case GameType.Race:
                    return user.RaceMaxScore;
                default:
                    return 0;
            }
        }

        public Dictionary<string,int> GetScores(GameType type)
        {
            var users = _repository.Get();
            var list = new Dictionary<string,int>();
            foreach (var user in users)
            {
                switch (type)
                {
                    case GameType.Flappy:
                        list.Add(user.UserName, user.FlappyMaxScore);
                        break;
                    case GameType.Race:
                        list.Add(user.UserName, user.RaceMaxScore);
                        break;
                }
            }
            
            return list;
        }

        public bool SaveScore(string userName, int score, GameType type)
        {
            try
            {
                var user = _repository.Get(userName);
                switch (type)
                {
                    case GameType.Flappy:
                        if (score > user.FlappyMaxScore)
                        {
                            user.FlappyMaxScore = score;
                            _repository.Update(user);
                        }
                        break;

                    case GameType.Race:
                        if (score > user.RaceMaxScore)
                        {
                            user.RaceMaxScore = score;
                            _repository.Update(user);
                        }

                        break;
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

