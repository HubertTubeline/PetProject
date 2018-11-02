using System;
using System.Collections.Generic;
using PetProject.Common.Helpers;
using PetProject.Common.Interfaces;
using PetProject.Common.Models;
using PetProject.DAL.Entities;
using PetProject.DAL.Interfaces;
using PetProject.DAL.Repositories;

namespace PetProject.Common.Services
{
    public class ScoresService : IScoresService
    {
        private readonly IUserRepository _repository;

        public ScoresService(string databaseName)
        {
            _repository = new UserRepository(databaseName);
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

        public Dictionary<string, int> GetScores(GameType type)
        {
            var users = _repository.Get();

            var list = new Dictionary<string, int>();
            foreach (var user in users)
                switch (type)
                {
                    case GameType.Flappy:
                        list.Add(user.UserName, user.FlappyMaxScore);
                        break;
                    case GameType.Race:
                        list.Add(user.UserName, user.RaceMaxScore);
                        break;
                    default:
                        return null;
                }

            return list;
        }

        public bool SaveScore(UserModel model)
        {
            try
            {
                var user = _repository.Get(model.UserName);

                UpdateScores(model, user);
                _repository.Update(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        private void UpdateScores(UserModel model, User entity)
        {
            if (entity.FlappyMaxScore < model.FlappyMaxScore)
                entity.FlappyMaxScore = model.FlappyMaxScore;

            if (entity.RaceMaxScore < model.RaceMaxScore)
                entity.RaceMaxScore = model.RaceMaxScore;
        }
    }
}