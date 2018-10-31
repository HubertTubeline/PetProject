using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using PetProject.Activities;
using PetProject.Common.Helpers;
using PetProject.Common.Interfaces;
using PetProject.Common.Services;
using PetProject.JSInterfaces;

namespace PetProject.Presenters
{
    public class FlappyPresenter
    {
        private Activity _activity;
        private WebView _webView;
        private string _userName;
        private IScoresService _scoresService;
        private MyJsInterface _interface;

        public FlappyPresenter(Activity activity, string userName)
        {
            _activity = activity;
            _userName = userName;
            _scoresService = new ScoresService();

            _interface = new MyJsInterface(_activity);
            _interface.OnGameEnded += OnEndGame;

            _webView = _activity.FindViewById<WebView>(Resource.Id.web_view_main);
            _webView.Settings.JavaScriptEnabled = true;
            _webView.AddJavascriptInterface(_interface, "CSharp");

            _webView.LoadUrl("file:///android_asset/flappy/index.html");

            _webView.SetWebViewClient(new WebViewClient());
            _webView.SetWebChromeClient(new WebChromeClient());

            var upFab = _activity.FindViewById<FloatingActionButton>(Resource.Id.flappy_fab_up);
            var downFab = _activity.FindViewById<FloatingActionButton>(Resource.Id.flappy_fab_down);

            upFab.Touch += UpFab_Touch;
            downFab.Touch += DownFab_Touch;
        }

        private void UpFab_Touch(object sender, View.TouchEventArgs e)
        {
            _webView.EvaluateJavascript("accelerate(-0.08);", null);
        }

        private void DownFab_Touch(object sender, View.TouchEventArgs e)
        {
            _webView.EvaluateJavascript("accelerate(0.08);", null);
        }

        private void OnEndGame(object sender, EventArgs args)
        {
            var score = _activity.FindViewById<TextView>(Resource.Id.scoreValue);
            _scoresService.SaveScore(_userName, int.Parse(score.Text), GameType.Flappy);
            _interface.OnGameEnded -= OnEndGame;
            Intent scores = new Intent(_activity, typeof(ScoresActivity));
            scores.PutExtra("gameType", "Flappy");
            scores.PutExtra("score", int.Parse(score.Text));
            _activity.StartActivity(scores);
        }
    }
}