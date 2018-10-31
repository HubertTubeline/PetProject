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
            SetContentView(Resource.Layout.activity_main);
            var toolbar =
                FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _presenter = new ScoresPresenter(this);
        }
    }
}