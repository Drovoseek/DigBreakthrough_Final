using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using EdaVPuti.Droid;

namespace App7.Droid
{
    [Activity(Label = "Еда В Пути", Icon = "@drawable/eda", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}