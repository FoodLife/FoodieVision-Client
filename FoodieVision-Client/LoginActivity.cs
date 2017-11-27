using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.Http;

namespace FoodieVisionClient
{
    [Activity(Label = "FoodieVision", MainLauncher = true, Theme = "@style/android:Theme.Holo.Light.NoActionBar")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginLayout);
            //SetContentView(Resource.Drawable.logo);

            //Initializing button from layout
            Button login = FindViewById<Button>(Resource.Id.login);


            //Login button click action
            login.Click += (object sender, EventArgs e) => {
                
                Android.Widget.Toast.MakeText(this, "Login Button Clicked!", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(MainActivity));

            };
        }
    }
}
