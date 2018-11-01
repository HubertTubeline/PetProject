using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Widget;
using PetProject.Activities;
using PetProject.Common.Helpers;

namespace PetProject.Presenters
{
    public class ScoresPresenter : BasePresenter
    {
        public ScoresPresenter(Activity activity, GameType type)
        {
            Activity = activity;

            InitScores(type);
            var button = activity.FindViewById<Button>(Resource.Id.scores_returnButton);
            button.Click += (sender, args) => { Activity.StartActivity(typeof(MainActivity)); };
        }

        private void InitPlayerScore(int score)
        {
            var scoreTextView = Activity.FindViewById<TextView>(Resource.Id.scores_player_score);
            scoreTextView.Text = score.ToString();
        }

        private void InitScores(GameType type)
        {
            var scores = ScoresService.GetScores(type);

            var userScore = scores[User.UserName];
            InitPlayerScore(userScore);

            var scoresText = CreateScoreText(scores);

            var scoresTextView = Activity.FindViewById<TextView>(Resource.Id.scores_scoresList);
            scoresTextView.Text = scoresText;
        }

        private string CreateScoreText(Dictionary<string, int> scores)
        {
            var sorted = scores.OrderByDescending(x => x.Value);

            var text = "";
            var counter = 1;

            foreach (var score in sorted)
            {
                if (counter == 10) break;
                text += counter++ + ". " + score.Key + ": " + score.Value + "\n";
            }

            return text;
        }
    }
}