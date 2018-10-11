using System;
using System.IO;
using DVidyaERP.Helpers;
using DVidyaERP.iOS.Helpers;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Picture))]

namespace DVidyaERP.iOS.Helpers
{
    public class Picture : IPicture
    {
        public void SavePictureToDisk(string filename, byte[] imageData)
        {
            var chartImage = new UIImage(NSData.FromArray(imageData));
            chartImage.SaveToPhotosAlbum((image, error) =>
            {
                //you can retrieve the saved UI Image as well if needed using
                //var i = image as UIImage;
                if (error != null)
                {
                    Console.WriteLine(error.ToString());
                }
            });
        }

        public string GetPictureFromDisk(string Id)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string jpgFilename = System.IO.Path.Combine(path, Id.ToString() + ".jpg");
            return jpgFilename;
        }

    }
}