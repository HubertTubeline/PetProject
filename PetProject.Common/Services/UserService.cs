using System;
using PetProject.Common.Interfaces;
using PetProject.Common.Models;
using PetProject.DAL.Entities;
using PetProject.DAL.Interfaces;
using PetProject.DAL.Repositories;

namespace PetProject.Common.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService()
        {
            _repository = new UserRepository();
        }

        public UserModel Get(string userName)
        {
            var user = _repository.Get(userName);
            return new UserModel
            {
                UserName = user.UserName,
                MaxScore = user.MaxScore
            };
        }

        public bool Create(UserModel model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                MaxScore = model.MaxScore
            };
            return _repository.Create(user);
        }

        public bool Update(UserModel model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                MaxScore = model.MaxScore
            };
            return _repository.Update(user);
        }

        public bool Delete(string userName)
        {
            return _repository.Delete(userName);
        }
    }
}