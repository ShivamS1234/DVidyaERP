using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DVidyaERP.DependencyServices;
using Xamarin.Forms;
using DVidyaERP.Droid.DependencyService_Android;
using Android.Net.Wifi;
using Android;
using Android.Telephony;
using Android.Support.V4.Content;

namespace DVidyaERP.Droid.DependencyService_Android
{
    class DependencyService_Android : IDependencyService
    {
        public void CloseApp()
        {
            global::Android.OS.Process.KillProcess(global::Android.OS.Process.MyPid());
        }
        public string GetNetworkName()
        {
            try
            {
                var wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);

                var ssid = wifiManager.ConnectionInfo.SSID;      //The SSID may be <unknown ssid> if there is no network currently connected.
                var bssid = wifiManager.ConnectionInfo.BSSID;    //The BSSID may be null if there is no network currently connected.

                if (!String.IsNullOrEmpty(ssid) && ssid != "<unknown ssid>")
                {
                    return ssid.Trim(' ', '"');
                }

                if (!String.IsNullOrEmpty(bssid))
                {
                    return bssid.Trim(' ', '"');
                }

                return "Not Connected";
            }
            catch (Exception ex)
            {
                return "Not Connected";
                Console.WriteLine(ex.Message);
            }

        }

        public string DeviceID()
        {
            TelephonyManager telephonyManager;
            object permissionCheck;
            string imei = "";
            permissionCheck = ContextCompat.CheckSelfPermission((Forms.Context), Manifest.Permission.ReadPhoneState);
            try
            {
                if (permissionCheck != null)
                {
                    telephonyManager = (TelephonyManager)Forms.Context.GetSystemService(Service.TelephonyService);
                    imei = telephonyManager.DeviceId;

                }
                else
                {
                    Toast.MakeText(Forms.Context, "Device ID is Not set in Your Mobile.. !", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                imei = "";
            }

            return imei;
        }
        public string getLocalPath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
