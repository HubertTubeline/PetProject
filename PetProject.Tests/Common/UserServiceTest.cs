using System;
using System.Collections.Generic;
using System.Text;
using PetProject.Common.Interfaces;
using PetProject.Common.Models;
using PetProject.Common.Services;
using Xunit;

namespace PetProject.Tests.Common
{
    public class UserServiceTest
    {
        private readonly IUserService _service = new UserService("test");
        private const string UserName = "UserServiceTest";

        [Fact]
        public void CreateUserTest()
        {
            // Arrange
            var user = new UserModel
            {
                UserName = UserName,
                RaceMaxScore = 128,
                FlappyMaxScore = 256
            };

            // Act
            _service.Create(user);
            var result = _service.Get(UserName);

            // Assert
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.RaceMaxScore, result.RaceMaxScore);
            Assert.Equal(user.FlappyMaxScore, result.FlappyMaxScore);
        }


        [Fact]
        public void EditUser()
        {
            // Arrange
            var user = new UserModel
            {
                UserName = UserName,
                RaceMaxScore = 128,
                FlappyMaxScore = 256
            };
            _service.Create(user);

            // Act
            user.RaceMaxScore = 2048;
            user.FlappyMaxScore = 122;
            _service.Update(user);

            // Assert
            var result = _service.Get(UserName);
            Assert.Equal(2048, result.RaceMaxScore);
            Assert.Equal(122, result.FlappyMaxScore);

        }

        [Fact]
        public void DeleteUser()
        {
            // Arrange
            var user = new UserModel
            {
                UserName = UserName,
                RaceMaxScore = 128,
                FlappyMaxScore = 256
            };
            _service.Create(user);

            // Act
            _service.Delete(UserName);

            // Assert
            var result = _service.Get(UserName);
            Assert.Null(result);
        }
    }
}
