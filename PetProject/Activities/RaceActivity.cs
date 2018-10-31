using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Webkit;
using PetProject.Presenters;

namespace PetProject.Activities
{
    [Activity(Label = "Race", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class RaceActivity : AppCompatActivity
    {
        private WebView _webView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestedOrientation = ScreenOrientation.Landscape;
            SetContentView(Resource.Layout.activity_race);
            var userName = Intent.GetStringExtra("userName");
            var racePresenter = new RacePresenter(this, userName);
        }
    }
}