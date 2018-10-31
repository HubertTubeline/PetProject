using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Webkit;
using PetProject.JSInterfaces;
using PetProject.Presenters;

namespace PetProject
{
    [Activity(Label = "Flappy", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class FlappyActivity : AppCompatActivity
    {
        private FlappyPresenter _presenter;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            var userName = Intent.GetStringExtra("userName");
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_flappy);

            _presenter = new FlappyPresenter(this, userName);
        }
    }
}