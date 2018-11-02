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
    public class FlappyPresenter : BasePresenter
    {
        private GameJsInterface _interface;
        private WebView _webView;

        public FlappyPresenter(Activity activity)
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
            _webView.LoadUrl("file:///android_asset/flappy/index.html");

            _webView.AddJavascriptInterface(_interface, "CSharp");

            _webView.SetWebViewClient(new WebViewClient());
            _webView.SetWebChromeClient(new WebChromeClient());
        }

        private void InitControlButtons()
        {
            var upFab = Activity.FindViewById<FloatingActionButton>(Resource.Id.flappy_fab_up);
            upFab.Touch += UpFab_Touch;
        }

        private void OnEndGame(object sender, EventArgs args)
        {
            _interface.OnGameEnded -= OnEndGame;

            SaveScores();
            StartScoresActivity();
        }

        private void SaveScores()
        {
            var scoreView = Activity.FindViewById<TextView>(Resource.Id.scoreValue);
            var score = int.Parse(scoreView.Text);

            User.FlappyMaxScore = score;
            ScoresService.SaveScore(User);
        }

        private void StartScoresActivity()
        {
            var scores = new Intent(Activity, typeof(ScoresActivity));
            scores.PutExtra("gameType", "Flappy");
            Activity.StartActivity(scores);
        }

        private void UpFab_Touch(object sender, View.TouchEventArgs e)
        {
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    _webView.EvaluateJavascript("accelerate(-0.08);", null);
                    break;
                case MotionEventActions.Up:
                    _webView.EvaluateJavascript("accelerate(0.08);", null);
                    break;
            }
        }
    }
}