using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Parse;
using Observations.Parse;

namespace Observations.Android
{
    [Activity(Label = "Observations.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += button_Click;

            //button.Click += delegate { p.CreateUser(user); };
            //button.Click += delegate { button.Text = string.Format(Observations.App.GetMainPageLabel(), count++); };
        }

        void button_Click(object sender, EventArgs e)
        {
            UserManagement userManagement = new UserManagement();

            var user = new ParseUser()
            {
                Username = "mwaresAndroid",
                Password = "password",
                Email = "markrwaresAndroid@gmail.com"
            };

            userManagement.CreateUser(user);
        }

    }
}

