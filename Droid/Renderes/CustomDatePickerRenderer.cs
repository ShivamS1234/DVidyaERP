using System;
using DVidyaERP.Droid.Renderes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePicker), typeof(CustomDatePickerRenderer))]
namespace DVidyaERP.Droid.Renderes
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            // Control is Android.Widget.EditText
            Control.Typeface = Android.Graphics.Typeface.CreateFromAsset(Forms.Context.Assets, "MAISONNEUE-BOOK.TTF");
            Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.style_DatePicker, 0);
            Control.TextSize = 14;
        }
    }
}