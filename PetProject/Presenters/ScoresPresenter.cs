using System.Linq;
using Android.App;
using Android.Widget;
using PetProject.Activities;
using PetProject.Common.Helpers;
using PetProject.Common.Interfaces;
using PetProject.Common.Services;

namespace PetProject.Presenters
{
    public class ScoresPresenter
    {
        private readonly Activity _activity;
        private readonly IScoresService _scoresService;
        private GameType _type;
        public ScoresPresenter(Activity activity, int score, GameType type)
        {
            _activity = activity;
            _type = type;

            _scoresService = new ScoresService();
            GetScores();
            GetPlayerScore(score);
            var button = activity.FindViewById<Button>(Resource.Id.scores_returnButton);
            button.Click += (sender, args) => { _activity.StartActivity(typeof(MainActivity)); };
        }

        private void GetPlayerScore(int score)
        {
            var scoreTextView = _activity.FindViewById<TextView>(Resource.Id.scores_player_score);
            scoreTextView.Text = score.ToString();
        }

        private void GetScores()
        {
            var text = "";
            var scores = _scoresService.GetScores(_type);

            var sorted = scores.OrderByDescending(x => x.Value);
            
            var counter = 1;
            foreach (var score in sorted)
            {
                if (counter == 10) break;
                text += counter++ + ". " + score.Key + ": " + score.Value + "\n";
            }

            var scoresText = _activity.FindViewById<TextView>(Resource.Id.scores_scoresList);
            scoresText.Text = text;
        }
    }
}