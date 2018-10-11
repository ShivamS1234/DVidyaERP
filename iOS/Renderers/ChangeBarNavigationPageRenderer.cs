using System;
using Fitness_app.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(ChangeBarNavigationPageRenderer))]
namespace Fitness_app.iOS
{
    public class ChangeBarNavigationPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            try
            {
                base.OnElementChanged(e);
                if (e.NewElement != null)
                {
                    var att = new UITextAttributes();
                    att.Font = UIFont.FromName("Rockwell-Light", 24);
                    UINavigationBar.Appearance.SetTitleTextAttributes(att);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }  
        }
    }
}