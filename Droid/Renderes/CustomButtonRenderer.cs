
using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using DVidyaERP.Droid;

[assembly: ExportRenderer(typeof(Button), typeof(CustomButtonRenderer))]

namespace DVidyaERP.Droid
{
    public class CustomButtonRenderer : ButtonRenderer
        {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            try
            {
                base.OnElementChanged(e);
                var btn = this.Control as Android.Widget.Button;
                if (btn.Text.Contains("Profile"))
                {
                    //btn?.SetBackgroundResource(Resource.Drawable.style_ProfileButton);

                    btn.SetPadding(btn.PaddingLeft, 9, btn.PaddingRight, 0);
                    btn.SetAllCaps(false);
                }
                else
                {
                    btn?.SetBackgroundResource(Resource.Drawable.style_Button);
                } 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}