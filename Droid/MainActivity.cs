using Android.App;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using FFImageCleanCache;
using FFImageLoading.Forms.Platform;
using Plugin.CurrentActivity;

namespace Droid
{
    [Activity(MainLauncher = true)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Forms.Init(this, savedInstanceState);

            CachedImageRenderer.Init(true);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}