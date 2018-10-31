using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
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
    public class RacePresenter
    {
        private readonly Activity _activity;
        private readonly WebView _webView;
        private string _userName;
        private IScoresService _scoresService;
        private MyJsInterface _interface;

        public RacePresenter(Activity activity, string userName)
        {
            _activity = activity;
            _scoresService = new ScoresService();
            _userName = userName;
            _webView = activity.FindViewById<WebView>(Resource.Id.web_view_main);
            _webView.Settings.JavaScriptEnabled = true;
            _interface = new MyJsInterface(activity);
            _webView.AddJavascriptInterface(_interface, "CSharp");
            _interface.OnGameEnded += OnEndGame;

            _webView.LoadUrl("file:///android_asset/race/index.html");

            _webView.SetWebViewClient(new WebViewClient());
            _webView.SetWebChromeClient(new WebChromeClient());

            var leftFab = activity.FindViewById<FloatingActionButton>(Resource.Id.fab_left);
            var rightFab = activity.FindViewById<FloatingActionButton>(Resource.Id.fab_right);

            var upFab = activity.FindViewById<FloatingActionButton>(Resource.Id.fab_up);
            var downFab = activity.FindViewById<FloatingActionButton>(Resource.Id.fab_down);

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


        private void OnEndGame(object sender, EventArgs args)
        {
            var score = _activity.FindViewById<TextView>(Resource.Id.scoreValue);
            _scoresService.SaveScore(_userName, int.Parse(score.Text), GameType.Race);
            _interface.OnGameEnded -= OnEndGame;
            Intent scores = new Intent(_activity, typeof(ScoresActivity));
            scores.PutExtra("gameType", "Race");
            scores.PutExtra("score", int.Parse(score.Text));
            _activity.StartActivity(scores);
        }

    }
}