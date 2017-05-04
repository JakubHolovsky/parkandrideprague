﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace ParkAndRidePrague.Droid
{
    [Activity(Label = "Prague P+R", Icon = "@drawable/icon", Theme = "@style/SplashTheme", MainLauncher = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}