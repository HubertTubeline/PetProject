using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V7.Widget;
using PetProject.Common.Helpers;
using PetProject.Presenters;

namespace PetProject.Activities
{
    [Activity(Label = "Race", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class ScoresActivity : AppCompatActivity
    {
        ScoresPresenter _presenter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_scores);
            var toolbar =
                FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            var type = Intent.GetStringExtra("gameType");
            Enum.TryParse(type, out GameType gameType);
            var score = Intent.GetIntExtra("score", 0);

            _presenter = new ScoresPresenter(this, score, gameType);
        }
    }
}