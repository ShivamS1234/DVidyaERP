using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using DVidyaERP.Droid.DependencyService_Android;
using FFImageLoading.Forms.Droid;
using Acr.UserDialogs;

namespace DVidyaERP.Droid
{
    [Activity(Label = "DVidyaERP.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.Forms.DependencyService.Register<DependencyService_Android.DependencyService_Android>();
           
            LoadApplication(new App());
            ImageCircleRenderer.Init();
            CachedImageRenderer.Init(true);
            UserDialogs.Init(this);
            //start service and set data in services
            Intent downloadIntentServices = new Intent(this, typeof(ERPservice));
            StartService(downloadIntentServices);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}
