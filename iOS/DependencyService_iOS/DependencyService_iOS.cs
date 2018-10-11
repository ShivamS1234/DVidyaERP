using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using DVidyaERP.DependencyServices;
using Xamarin.Forms;
using DVidyaERP.iOS.DependencyService_iOS;
using SystemConfiguration;
using BigTed;
using static BigTed.ProgressHUD;
using System.IO;

namespace DVidyaERP.iOS.DependencyService_iOS
{
    class DependencyService_iOS : IDependencyService
    {

        public string GetNetworkName()
        {
            String[] interfaces;
            CaptiveNetwork.TryGetSupportedInterfaces(out interfaces);

            if (interfaces != null && interfaces.Length >= 1)
            {
                NSDictionary dict;
                CaptiveNetwork.TryCopyCurrentNetworkInfo(interfaces[0], out dict);

                if (dict != null)
                {
                    var bssid = (NSString)dict[CaptiveNetwork.NetworkInfoKeyBSSID];
                    var ssid = (NSString)dict[CaptiveNetwork.NetworkInfoKeySSID];


                    if (!String.IsNullOrEmpty(ssid))
                    {
                        return ssid;
                    }

                    if (!String.IsNullOrEmpty(bssid))
                    {
                        return bssid;
                    }
                }
            }

            return "Not Connected";
        }

        public void CloseApp()
        {

        }

        public string DeviceID()
        {
            return  UIDevice.CurrentDevice.IdentifierForVendor.ToString();
        }
        public string getLocalPath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }

}
