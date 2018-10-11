

using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;

[assembly: ExportRenderer(typeof(Fitness_app.Views.MenuTableView), typeof(Fitness_app.iOS.MenuTableViewRenderer))]
namespace Fitness_app.iOS
{
    public class MenuTableViewRenderer : TableViewRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            var tableView = Control as UITableView;
            //tableView.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.ScrollableAxes;

            var deviceVersion = NSProcessInfo.ProcessInfo.Environment["SIMULATOR_MODEL_IDENTIFIER"];
            var IdeviceVersion = Xamarin.iOS.DeviceHardware.Version;
            string dVersion = Convert.ToString(deviceVersion);
            if (dVersion.Contains("iPhone10")  || IdeviceVersion.Contains("iPhone10") )
            {
                tableView.ContentInset = new UIEdgeInsets(-25, 0, -20, 0);
            }

            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            tableView.BackgroundColor = UIColor.FromRGB(0xff, 0xff, 0xff);
        }
    }
}