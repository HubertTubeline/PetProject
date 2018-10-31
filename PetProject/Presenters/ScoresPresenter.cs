using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PetProject.Common.Interfaces;
using PetProject.Common.Services;

namespace PetProject.Presenters
{
    public class ScoresPresenter
    {
        private Activity _activity;
        private string _userName;
        private IScoresService _scoresService;

        public ScoresPresenter(Activity activity, string userName)
        {
            _activity = activity;
            _userName = userName;
            _scoresService = new ScoresService();
        }

        private void GetScores()
        {
            var scores = _scoresService.GetScores();
            foreach (var score in scores)
            {
                if (score.Key == _userName)
                {

                }
            }
        }
    }
}