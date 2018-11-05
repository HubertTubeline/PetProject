using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using PetProject.Common.Utils;
using PetProject.Presenters;

namespace PetProject.Activities
{
    [Activity(Label = "Scores", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class ScoresActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_scores);

            var toolbar =
                FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var type = Intent.GetStringExtra("gameType");
            Enum.TryParse(type, out GameType gameType);

            var presenter = new ScoresPresenter(this, gameType);
        }

        public override void OnBackPressed()
        {
            StartActivity(typeof(MainActivity));
        }
    }
}