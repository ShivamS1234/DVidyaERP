using System;
using Fitness_app.iOS;
using Fitness_app;
using Xamarin.Forms;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(ListViewAdjustmentRenderer))]
[assembly: ExportRenderer(typeof(ViewCell), typeof(CellViewAdjustmentRenderer))]

namespace Fitness_app.iOS
{
    public class ListViewAdjustmentRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            try
            {
                
                if (Control == null)
                    return;
                //here condition is back call menu tableViewRendrer

                Control.SeparatorInset = UIEdgeInsets.Zero;
            }
            catch(Exception ex)
            {
                
            }
        }
    }
    public class CellViewAdjustmentRenderer : ViewCellRenderer
    {
        public override UIKit.UITableViewCell GetCell(Xamarin.Forms.Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            //cell.sw
            cell.SelectionStyle = UITableViewCellSelectionStyle.Gray;
            return cell;
        }
    }
}

