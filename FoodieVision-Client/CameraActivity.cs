using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;
using Java.IO;
using Android.Graphics;
using System.Diagnostics.Contracts;

namespace FoodieVisionClient
{
    
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Graphics;
    using Android.OS;
    using Android.Provider;
    using Android.Widget;
    using Android.Net;
    using Java.IO;
    using Environment = Android.OS.Environment;
    using Uri = Android.Net.Uri;
    using System.Net.Http;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;

    public static class App
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
    }

    [Activity(Label = "Camera view", Theme = "@style/android:Theme.Holo.Light.NoActionBar")]
    public class CameraActivity : Activity
    {

        private ImageView _imageView;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display
            // Loading the full sized image will consume to much memory 
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            //SendToSever();
            if (App.bitmap != null)
            {
                _imageView.SetImageBitmap(App.bitmap);
                SendToSever();
                App.bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CameraLayout);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

               // Button pic = FindViewById<Button>(Resource.Id.myButton);
                _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
               

                 TakeAPicture();





                }

        }

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "FoodieVision");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void TakeAPicture()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));


            StartActivityForResult(intent, 0);
        }
        private async void SendToSever()
        {
            
            using (var stream = new System.IO.MemoryStream())
            {
                App.bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);

                var bytes = stream.ToArray();
                var str = Convert.ToBase64String(bytes);
                string sUrl = "http://34.232.146.205/foodies/is_food";
                string sContentType = "application/json"; // or application/xml

                JObject oJsonObject = new JObject();
                oJsonObject.Add("image", str);
                //string test = "";

               // Android.Widget.Toast.MakeText(this, oJsonObject.ToString(), Android.Widget.ToastLength.Long).Show();

               /// System.Console.WriteLine("Sent=  "+ str ); 




                HttpClient oHttpClient = new HttpClient();
                //HttpResponseMessage oTaskPostAsync = await oHttpClient.PostAsync(sUrl, new StringContent(oJsonObject.ToString(), Encoding.UTF8, sContentType));

                using (var client = new HttpClient())
                {
                    var result = client.PostAsync(sUrl, new StringContent(oJsonObject.ToString(), Encoding.UTF8, sContentType)).Result;
                    result.EnsureSuccessStatusCode();
                    Android.Widget.Toast.MakeText(this, result.Content.ReadAsStringAsync().Result, Android.Widget.ToastLength.Long).Show();
                    TextView tv = FindViewById<TextView>(Resource.Id.textView1);

                    tv.Text = result.Content.ReadAsStringAsync().Result;
                }


           
            }
           
        }
    }
}