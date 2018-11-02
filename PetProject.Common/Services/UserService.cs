using Ninject;
using PetProject.Common.Utils;
using PetProject.Common.Interfaces;
using PetProject.Common.Models;
using PetProject.DAL.Interfaces;

namespace PetProject.Common.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(string databaseName = "database")
        {
            _repository = NinjectRegistrator.GetKernel(databaseName).Get<IUserRepository>();
        }

        public UserModel Get(string userName)
        {
            var user = _repository.Get(userName);
            return Mapper.MapUser(user);
        }

        public bool Create(UserModel model)
        {
            return _repository.Create(Mapper.MapUser(model));
        }

        public bool Update(UserModel model)
        {
            return _repository.Update(Mapper.MapUser(model));
        }

        public bool Delete(string userName)
        {
            return _repository.Delete(userName);
        }
    }
}