using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;

namespace FoodieVisionClient
{
    [Activity(Label = "Main", Theme = "@style/android:Theme.Holo.Light.NoActionBar")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.MainLayout);

            //Initializing button from layout
            Button login = FindViewById<Button>(Resource.Id.CameraButton);
            Button timeLine = FindViewById<Button>(Resource.Id.TimeLineButton);

            //Login button click action
            login.Click += (object sender, EventArgs e) => {

                Android.Widget.Toast.MakeText(this, "Camera Button Clicked!", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(CameraActivity));

            };
            timeLine.Click += (object sender, EventArgs e) => {

                Android.Widget.Toast.MakeText(this, "TimeLine Button Clicked!", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(TimeLineActivity));

            };
        }
       
    }
}