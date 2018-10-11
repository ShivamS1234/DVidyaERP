using System;
using DVidyaERP.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.Internals;


[assembly: ExportRenderer(typeof(DatePicker), typeof(CustomDatePickerRenderer))]
namespace DVidyaERP.iOS.Renderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            try
            {
                //Control is UITextField
                UIButton myButton = new UIButton(UIButtonType.Custom);

                var downarrow = UIImage.FromBundle("iconsCalendar.png");
                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.RightView = new UIImageView(downarrow);

                var someFontWithName = UIFont.FromName("Maison Neue", 14);
                UIFont font = Control.Font.WithSize(14);
                Control.Font = font;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }
    }
}