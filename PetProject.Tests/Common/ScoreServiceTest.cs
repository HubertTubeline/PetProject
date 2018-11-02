using System;
using System.Collections.Generic;
using System.Text;
using PetProject.Common.Helpers;
using PetProject.Common.Interfaces;
using PetProject.Common.Models;
using PetProject.Common.Services;
using Xunit;

namespace PetProject.Tests.Common
{
    public class ScoreServiceTest
    {
        private readonly IScoresService _scoresService = new ScoresService("test");
        private readonly IUserService _userService = new UserService("test");
        private const string UserName = "ScoreServiceTest"; 

        [Fact]
        public void SendLargerFlappyScore()
        {
            // Arrange
            const int initialScore = 128;
            const int modifiedScore = 1024;

            var user = new UserModel()
            {
                UserName = UserName,
                RaceMaxScore = 128,
                FlappyMaxScore = initialScore
            };
            _userService.Create(user);

            // Act
            user.FlappyMaxScore = modifiedScore;
            _scoresService.SaveScore(user);
            var scores = _scoresService.GetScore(UserName, GameType.Flappy);

            // Assert
            Assert.Equal(modifiedScore, scores);
        }

        [Fact]
        public void SendLargerRaceScore()
        {
            // Arrange
            const int initialScore = 128;
            const int modifiedScore = 10960;

            var user = new UserModel()
            {
                UserName = UserName,
                RaceMaxScore = initialScore,
                FlappyMaxScore = 256
            };
            _userService.Create(user);

            // Act
            user.RaceMaxScore = modifiedScore;
            _scoresService.SaveScore(user);
            var scores = _scoresService.GetScore(UserName, GameType.Race);

            // Assert
            Assert.Equal(modifiedScore, scores);
        }

        [Fact]
        public void SendSmallerFlappyScore()
        {
            // Arrange
            const int initialScore = 1024;
            const int modifiedScore = 128;

            var user = new UserModel()
            {
                UserName = UserName,
                RaceMaxScore = 128,
                FlappyMaxScore = initialScore
            };
            _userService.Create(user);

            // Act
            user.FlappyMaxScore = modifiedScore;
            _scoresService.SaveScore(user);
            var scores = _scoresService.GetScore(UserName, GameType.Flappy);

            // Assert
            Assert.Equal(initialScore, scores);
        }

        [Fact]
        public void SendSmallerRaceScore()
        {
            // Arrange
            const int initialScore = 1024;
            const int modifiedScore = 128;

            var user = new UserModel()
            {
                UserName = UserName,
                RaceMaxScore = initialScore,
                FlappyMaxScore = 256
            };
            _userService.Create(user);

            // Act
            user.RaceMaxScore = modifiedScore;
            _scoresService.SaveScore(user);
            var scores = _scoresService.GetScore(UserName, GameType.Race);

            // Assert
            Assert.Equal(initialScore, scores);
        }
    }
}
