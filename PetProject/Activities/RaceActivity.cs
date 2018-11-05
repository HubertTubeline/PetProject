using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using PetProject.Presenters;
using AlertDialog = Android.Support.V7.App.AlertDialog;

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

        public override void OnBackPressed()
        {
            var dialog = new AlertDialog.Builder(this);
            dialog.SetMessage("Are you sure?");
            dialog.SetCancelable(true);
            dialog.SetPositiveButton("YES", (sender, args) =>
            {
                FinishAffinity();
                StartActivity(typeof(MainActivity));
            });
            dialog.SetNegativeButton("NO", (sender, args) => { });
            dialog.Create().Show();
        }
    }
}