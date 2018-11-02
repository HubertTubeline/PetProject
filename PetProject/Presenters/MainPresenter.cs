using System;
using Android.App;
using Android.Content;
using Android.Widget;
using PetProject.Activities;
using PetProject.Common.Models;

namespace PetProject.Presenters
{
    public class MainPresenter : BasePresenter
    {
        public MainPresenter(Activity activity)
        {
            Activity = activity;
            var button = Activity.FindViewById<Button>(Resource.Id.confirmButton);
            button.Click += OnConfirm;
        }

        private void OnConfirm(object sender, EventArgs eventArgs)
        {
            var userName = Activity.FindViewById<EditText>(Resource.Id.userName);
            if (string.IsNullOrWhiteSpace(userName.Text)) return;

            CreateUser(userName.Text);
            GoToGameActivity();
        }

        private void CreateUser(string name)
        {
            User = new UserModel {UserName = name};
            UserService.Create(User);
        }

        private void GoToGameActivity()
        {
            var radioGroup = Activity.FindViewById<RadioGroup>(Resource.Id.main_radio_group);

            var intent = radioGroup.CheckedRadioButtonId == Resource.Id.main_radio_flappy
                ? new Intent(Activity, typeof(FlappyActivity))
                : new Intent(Activity, typeof(RaceActivity));

            Activity.StartActivity(intent);
            Activity.Finish();
        }
    }
}