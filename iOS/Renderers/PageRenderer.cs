using System;
using DVidyaERP.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(StartPageRenderer))]
namespace DVidyaERP.iOS
{
	public class StartPageRenderer : PageRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
            try
            {
				var page = e.NewElement as ContentPage;
                if (!string.IsNullOrEmpty(page.BackgroundImage))
                {

					UIGraphics.BeginImageContext(View.Frame.Size);
					//TODO: if this is an iphone5, then use an iphone5 pic (the default 2x picture would not have the same aspect ratio)    
					UIImage i = UIImage.FromFile(page.BackgroundImage);
					i = i.Scale(View.Frame.Size);

					View.BackgroundColor = UIColor.FromPatternImage(i);
				}
				
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
			
		}
	}
}