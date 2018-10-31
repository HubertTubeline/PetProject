using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Common.Interfaces
{
    public interface IScoresService
    {
        int GetScore(string userName);
        Dictionary<string, int> GetScores();
        bool SaveScore(string userName, int score);
    }
}
