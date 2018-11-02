using Ninject;
using PetProject.Common.Interfaces;
using PetProject.Common.Models;
using PetProject.Common.Utils;
using Xunit;

namespace PetProject.Tests.Common
{
    public class ScoreServiceTest
    {
        private readonly IScoresService _scoresService;
        private readonly IUserService _userService;
        private const string UserName = "ScoreServiceTest";

        public ScoreServiceTest()
        {
            var kernel = NinjectRegistrator.GetKernel("test");
            _scoresService = kernel.Get<IScoresService>();
            _userService = kernel.Get<IUserService>();
        }

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
