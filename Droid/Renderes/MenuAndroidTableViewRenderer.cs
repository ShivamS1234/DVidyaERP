
using Xamarin.Forms;
using DVidyaERP.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using DVidyaERP.Views;
using View = Android.Views.View;
using Android.Content;

[assembly: ExportRenderer(typeof(MenuTableView), typeof(MenuAndroidTableViewRenderer))]
[assembly: ExportRenderer(typeof(ViewCell), typeof(ViewCellTransparent))]

namespace DVidyaERP.Droid
{
    public class MenuAndroidTableViewRenderer : TableViewRenderer 
    {
        private bool FirstElementAdded = false;
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);
            var tableView = Control as global::Android.Widget.ListView;
            tableView.DividerHeight = 0;
            tableView.SetHeaderDividersEnabled(false);
            //this will remove blue line in header but leave space
            //tableView.ChildViewAdded += (sender, args) =>
            //{
            //    //if (!FirstElementAdded)
            //    //{

            //    //    args.Child.Visibility = ViewStates.Gone;

            //    //    FirstElementAdded = true;

            //    //}
            //};

            tableView.LongClickable = false;
            //tableView.SetBackgroundColor(new global::Android.Graphics.Color(0xff, 0xff, 0xff));
            tableView.SetBackgroundColor(new global::Android.Graphics.Color(0xff, 0xff, 0xff));
        }
    }

    public class ViewCellTransparent : ViewCellRenderer
    {
        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context)
        {
            var cell = base.GetCellCore(item, convertView, parent, context);
            var listView = parent as Android.Widget.ListView;
        

            if (listView != null)
            {
                // Disable native cell selection color style - set as *Transparent*
                listView.SetSelector(Android.Resource.Color.DarkerGray);
                listView.CacheColorHint = Android.Graphics.Color.Transparent;
            }
           
            return cell;
        }
    }
}