using System;
using System.Collections.Generic;
using System.Text;
using PetProject.DAL.Entities;
using PetProject.DAL.Interfaces;
using PetProject.DAL.Repositories;
using Xunit;

namespace PetProject.Tests.DAL
{
    public class UserRepositoryTest
    {
        private readonly IUserRepository _repo = new UserRepository("test");
        private const string UserName = "UserRepoTest";

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
            _repo.Create(user);
            var result = _repo.Get(UserName);

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
            _repo.Create(user);

            // Act
            user.RaceMaxScore = 2048;
            user.FlappyMaxScore = 122;
            _repo.Update(user);

            // Assert
            var result = _repo.Get(UserName);
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
            _repo.Create(user);

            // Act
            _repo.Delete(UserName);

            // Assert
            var result = _repo.Get(UserName);
            Assert.Null(result);
        }
    }
}
