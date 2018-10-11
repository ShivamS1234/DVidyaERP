using System.Collections.Generic;
using System.Linq;
using System.Text;
using DVidyaERP.Droid.Helpers;
using DVidyaERP.Helpers;
using System.Threading.Tasks;
using Java.IO;
using Android.OS;
using Android.Provider;
using Android.Content;
using Android.Net;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(Picture))]

namespace DVidyaERP.Droid.Helpers
{
    public class Picture : IPicture
    {
        public void SavePictureToDisk(string filename, byte[] imageData)
        {
            //here we are create folder in location
            var pictures = GetlocalStoragefilePath();
            
            //var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
            //var pictures = dir.AbsolutePath;
            //adding a time stamp time file name to allow saving more than one image... otherwise it overwrites the previous saved image of the same name
           // string name = filename + System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";

            string name = filename + ".png";
            string filePath = System.IO.Path.Combine(pictures , name);
            try
            {
                System.IO.File.WriteAllBytes(filePath, imageData);
                //mediascan adds the saved image into the gallery
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(filePath)));
                Xamarin.Forms.Forms.Context.SendBroadcast(mediaScanIntent);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

        }
        public string GetPictureFromDisk(string Id)
        {
            //var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
            //var pictures = dir.AbsolutePath;
            var pictures = GetlocalStoragefilePath();

            string name = Id + ".png";
            string filePath = System.IO.Path.Combine(pictures , name);
            return filePath;
        }
        private string GetlocalStoragefilePath()
        {
            try
            {
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var dirToCreate = Path.Combine(path, "DvidyaPic");
                if (!System.IO.Directory.Exists(dirToCreate))
                    System.IO.Directory.CreateDirectory(dirToCreate);
                return dirToCreate;  
            }
            catch(Exception ex)
            {
                    
            }
            return ""; 
        }
    }
}