using System.Collections.Generic;
using PetProject.Common.Utils;
using PetProject.Common.Models;

namespace PetProject.Common.Interfaces
{
    public interface IScoresService
    {
        int GetScore(string userName, GameType type);
        Dictionary<string, int> GetScores(GameType type);
        bool SaveScore(UserModel user);
    }
}
