using System;
using DVidyaERP.CustomControl;
using DVidyaERP.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace DVidyaERP.iOS
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var element = (CustomPicker)this.Element;
            try
            {
                if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                {
                    var downarrow = UIImage.FromBundle(element.Image);
                    Control.RightViewMode = UITextFieldViewMode.Always;
                    Control.RightView = new UIImageView(downarrow);
                }
            }
            catch(Exception ex)
            {
                throw new ArgumentException(ex.ToString(), "Picker Renderer Exception");
            }
        }
    }
}
