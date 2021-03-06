﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Interop;

namespace PetProject.JSInterfaces
{
    public class GameJsInterface : Java.Lang.Object
    {
        private readonly Activity _context;
        private TextView _scoreValue;

        public event EventHandler OnGameEnded;

        public GameJsInterface(Activity context)
        {
            _context = context;
            _scoreValue = context.FindViewById<TextView>(Resource.Id.scoreValue);
        }

        [Export]
        [JavascriptInterface]
        public void SendScore(string score)
        {
            try
            {
                _context.RunOnUiThread(() =>
                {
                    _scoreValue.Text = score;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Export]
        [JavascriptInterface]
        public void EndGame()
        {
            try
            {
                OnGameEnded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}