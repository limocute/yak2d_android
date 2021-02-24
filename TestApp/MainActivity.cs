using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Veldrid;
using Yak2D;
using Yak2D.Droid;

namespace TestApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Yak2DActivity
    {
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override IApplication Yak2DApplication => new App();

    }
}