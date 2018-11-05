using System;
using Ninject;
using PetProject.Common.Interfaces;
using PetProject.Common.Models;
using PetProject.Common.Utils;
using Xunit;

namespace PetProject.Tests.Common
{
    public class UserServiceTest
    {
        private readonly IUserService _service;
        
        public UserServiceTest()
        {
            var kernel = NinjectRegistrator.GetKernel("test");
            _service = kernel.Get<IUserService>();
        }

        [Fact]
        public void CreateUser()
        {
            // Arrange
            var user = new UserModel
            {
                UserName = "CreateUser",
                RaceMaxScore = 128,
                FlappyMaxScore = 256
            };

            // Act
            _service.Create(user);
            var result = _service.Get("CreateUser");

            // Assert
            Assert.NotNull(result);
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
                UserName = "EditUser",
                RaceMaxScore = 128,
                FlappyMaxScore = 256
            };
            _service.Create(user);

            // Act
            user.RaceMaxScore = 2048;
            user.FlappyMaxScore = 122;
            _service.Update(user);

            // Assert
            var result = _service.Get("EditUser");
            Assert.Equal(2048, result.RaceMaxScore);
            Assert.Equal(122, result.FlappyMaxScore);

        }

        [Fact]
        public void DeleteUser()
        {
            // Arrange
            var user = new UserModel
            {
                UserName = "DeleteUser",
                RaceMaxScore = 128,
                FlappyMaxScore = 256
            };
            _service.Create(user);

            // Act
            _service.Delete("DeleteUser");

            // Assert
            var result = _service.Get("DeleteUser");
            Assert.Null(result);
        }
    }
}
