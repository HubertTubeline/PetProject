using System;
using System.Timers;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using PetProject.JSInterfaces;
using Object = Java.Lang.Object;
using String = Java.Lang.String;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace PetProject
{
    [Activity(Label = "Race", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class RaceActivity : AppCompatActivity
    {
        private WebView _webView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_race);
            
            _webView = FindViewById<WebView>(Resource.Id.web_view_main);
            _webView.Settings.JavaScriptEnabled = true;
            _webView.AddJavascriptInterface(new MyJsInterface(this), "CSharp");

            _webView.LoadUrl("file:///android_asset/race/v4.final.html");

            _webView.SetWebViewClient(new WebViewClient());
            _webView.SetWebChromeClient(new WebChromeClient());
            var leftFab = FindViewById<FloatingActionButton>(Resource.Id.fab_left);
            var rightFab = FindViewById<FloatingActionButton>(Resource.Id.fab_right);

            var upFab = FindViewById<FloatingActionButton>(Resource.Id.fab_up);
            var downFab = FindViewById<FloatingActionButton>(Resource.Id.fab_down);

            leftFab.Touch += LeftFab_Touch;
            rightFab.Touch += RightFab_Touch;
            upFab.Touch += UpFab_Touch;
            downFab.Touch += DownFab_Touch;
        }

        private void LeftFab_Touch(object sender, View.TouchEventArgs e)
        {
            _webView.EvaluateJavascript("keyFaster = true;", null);
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    _webView.EvaluateJavascript("keyLeft = true;", null);
                    break;
                case MotionEventActions.Up:
                    _webView.EvaluateJavascript("keyLeft = false;", null);
                    break;
            }
        }

        private void RightFab_Touch(object sender, View.TouchEventArgs e)
        {
            _webView.EvaluateJavascript("keyFaster = false;", null);
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    _webView.EvaluateJavascript("keyRight = true;", null);
                    break;
                case MotionEventActions.Up:
                    _webView.EvaluateJavascript("keyRight = false;", null);
                    break;
            }
        }

        private void UpFab_Touch(object sender, View.TouchEventArgs e)
        {
            _webView.EvaluateJavascript("keyFaster = true;", null);
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    _webView.EvaluateJavascript("keyFaster = true;", null);
                    break;
                case MotionEventActions.Up:
                    _webView.EvaluateJavascript("keyFaster = false;", null);
                    break;
            }
        }

        private void DownFab_Touch(object sender, View.TouchEventArgs e)
        {
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    _webView.EvaluateJavascript("keySlower = true;", null);
                    break;
                case MotionEventActions.Up:
                    _webView.EvaluateJavascript("keySlower = false;", null);
                    break;
            }
        }
    }
}