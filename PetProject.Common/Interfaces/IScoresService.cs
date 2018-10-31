using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PetProject.Common.Helpers;

namespace PetProject.Common.Interfaces
{
    public interface IScoresService
    {
        int GetScore(string userName, GameType type);
        Dictionary<string, int> GetScores(GameType type);
        bool SaveScore(string userName, int score, GameType type);
    }
}
