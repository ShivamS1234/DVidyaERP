using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Fitness_app.iOS;
using System.Linq;
using UIKit;
using CoreGraphics;
using Fitness_app;

[assembly:ExportRenderer (typeof(MealsTabPage), typeof(MainPageRenderer))]
namespace Fitness_app.iOS
{
	public class MainPageRenderer : TabbedRenderer
	{
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			var pages = this.ViewController.ChildViewControllers
										   .OfType<IVisualElementRenderer>()
										   .Select(e => e.Element as ITabPage)
										   .ToArray();

			if (pages.Length != this.TabBar.Items.Length)
			{
				throw new Exception("Uh oh! Inconsistent number of pages and tabbar items!");
			}

			for (var i = 0; i < pages.Length; i++)
			{
				var tabItem = this.TabBar.Items[i];
				if (tabItem.Image == null)
				{
					tabItem.Image = ImageHelper.ImageFromFont(pages[i].TabIcon, UIColor.Black, new CGSize(40, 35));
					//tabItem.SelectedImage = ImageHelper.ImageFromFont(pages[i].SelectedTabIcon, UIColor.Black, new CGSize(35, 35));
				}
			}
		}
	}
}