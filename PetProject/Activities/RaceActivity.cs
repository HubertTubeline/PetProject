﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using PetProject.Presenters;

namespace PetProject.Activities
{
    [Activity(Label = "Race", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class RaceActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestedOrientation = ScreenOrientation.Landscape;

            SetContentView(Resource.Layout.activity_race);

            var presenter = new RacePresenter(this);
        }
    }
}