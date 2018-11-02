using System;
using Android.App;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using PetProject.Activities;
using PetProject.JSInterfaces;

namespace PetProject.Presenters
{
    public class RacePresenter : BasePresenter
    {
        private GameJsInterface _interface;
        private WebView _webView;

        public RacePresenter(Activity activity)
        {
            
            Activity = activity;

            InitJavaScriptInterface();
            InitWebView();
            InitControlButtons();
        }

        private void InitJavaScriptInterface()
        {
            _interface = new GameJsInterface(Activity);
            _interface.OnGameEnded += OnEndGame;
        }

        private void InitWebView()
        {
            _webView = Activity.FindViewById<WebView>(Resource.Id.web_view_main);

            _webView.Settings.JavaScriptEnabled = true;
            _webView.LoadUrl("file:///android_asset/race/index.html");

            _webView.AddJavascriptInterface(_interface, "CSharp");

            _webView.SetWebViewClient(new WebViewClient());
            _webView.SetWebChromeClient(new WebChromeClient());
        }

        private void InitControlButtons()
        {
            var leftFab = Activity.FindViewById<FloatingActionButton>(Resource.Id.fab_left);
            var rightFab = Activity.FindViewById<FloatingActionButton>(Resource.Id.fab_right);

            var upFab = Activity.FindViewById<FloatingActionButton>(Resource.Id.fab_up);
            var downFab = Activity.FindViewById<FloatingActionButton>(Resource.Id.fab_down);

            leftFab.Touch += LeftFab_Touch;
            rightFab.Touch += RightFab_Touch;

            upFab.Touch += UpFab_Touch;
            downFab.Touch += DownFab_Touch;
        }

        private void OnEndGame(object sender, EventArgs args)
        {
            _interface.OnGameEnded -= OnEndGame;
            SaveScores();
            GoToScoresActivity();
        }

        private void SaveScores()
        {
            var scoreView = Activity.FindViewById<TextView>(Resource.Id.scoreValue);
            var score = int.Parse(scoreView.Text);

            User.RaceMaxScore = score;
            ScoresService.SaveScore(User);
        }

        private void GoToScoresActivity()
        {
            var scores = new Intent(Activity, typeof(ScoresActivity));
            scores.PutExtra("gameType", "Race");

            Activity.StartActivity(scores);
            Activity.Finish();
        }


        private void LeftFab_Touch(object sender, View.TouchEventArgs e)
        {
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