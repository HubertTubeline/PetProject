using Ninject;
using PetProject.Common.Utils;
using PetProject.DAL.Entities;
using PetProject.DAL.Interfaces;
using Xunit;

namespace PetProject.Tests.DAL
{
    public class UserRepositoryTest
    {
        private readonly IUserRepository _repository;
        private const string UserName = "UserRepoTest";

        public UserRepositoryTest()
        {
            var kernel = NinjectRegistrator.GetKernel("test");
            _repository = kernel.Get<IUserRepository>();
        }

        [Fact]
        public void CreateUser()
        {
            // Arrange
            var user = new User
            {
                UserName = UserName,
                RaceMaxScore = 128,
                FlappyMaxScore = 256
            };

            // Act
            _repository.Create(user);
            var result = _repository.Get(UserName);

            // Assert
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.RaceMaxScore, result.RaceMaxScore);
            Assert.Equal(user.FlappyMaxScore, result.FlappyMaxScore);
        }

        [Fact]
        public void EditUser()
        {
            // Arrange
            var user = new User
            {
                UserName = UserName,
                RaceMaxScore = 128,
                FlappyMaxScore = 256
            };
            _repository.Create(user);

            // Act
            user.RaceMaxScore = 2048;
            user.FlappyMaxScore = 122;
            _repository.Update(user);

            // Assert
            var result = _repository.Get(UserName);
            Assert.Equal(2048, result.RaceMaxScore);
            Assert.Equal(122, result.FlappyMaxScore);

        }

        [Fact]
        public void DeleteUser()
        {
            // Arrange
            var user = new User
            {
                UserName = UserName,
                RaceMaxScore = 128,
                FlappyMaxScore = 256
            };
            _repository.Create(user);

            // Act
            _repository.Delete(UserName);

            // Assert
            var result = _repository.Get(UserName);
            Assert.Null(result);
        }
    }
}
