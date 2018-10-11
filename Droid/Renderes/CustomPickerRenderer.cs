using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Widget;
using DVidyaERP.CustomControl;
using DVidyaERP.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace DVidyaERP.Droid
{
    public class CustomPickerRenderer : PickerRenderer
    {
        CustomPicker element;
        private IElementController ElementController => Element as IElementController;
        private AlertDialog _dialog;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null || e.OldElement != null)
                return;


            element = (CustomPicker)this.Element;

            if (Control != null && this.Element != null)
            {
                //Control.Background = AddPickerStyles(element.Image);
                Control.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.style_Entry)); 
                Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(0,0, Resource.Drawable.style_PickerControl, 0);
                Control.SetPadding(10, 4, 4, 4);
                Control.Gravity=Android.Views.GravityFlags.Center;
                var MAISONNEUEFont = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "MAISONNEUE-BOOK.TTF");
                Control.Typeface = MAISONNEUEFont;

            }
          
            Control.Click += Control_Click;
        }
       

        protected override void Dispose(bool disposing)
        {
            Control.Click -= Control_Click;
            base.Dispose(disposing);
        }

        private void Control_Click(object sender, EventArgs e)
        {
            Picker model = Element;
            var RCKWLLFont = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "RCKWLL.ttf");

            var picker = new NumberPicker(Context);
            if (model.Items != null && model.Items.Any())
            {
                picker.MaxValue = model.Items.Count - 1;
                picker.MinValue = 0;
                picker.SetDisplayedValues(model.Items.ToArray());
                picker.WrapSelectorWheel = false;
                picker.DescendantFocusability = Android.Views.DescendantFocusability.BlockDescendants;
                picker.Value = model.SelectedIndex;
            }

            var layout = new LinearLayout(Context) { Orientation = Orientation.Vertical };
            layout.AddView(picker);

            ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, true);
            //Create a custom title element 
            TextView title = new TextView(Context)
            {
                Text = model.Title,
                Typeface=RCKWLLFont,
                Gravity=Android.Views.GravityFlags.Center,
            };
            title.TextSize=20;
            title.SetTextColor(Android.Graphics.Color.White);
            title.SetBackgroundColor(Android.Graphics.Color.Rgb(234,49,117));
            title.SetHeight(100);

            var builder = new AlertDialog.Builder(Context);
            builder.SetView(layout);
            builder.SetCustomTitle(title);

            builder.SetNegativeButton("CANCEL", (s, a) =>
            {
                ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                // It is possible for the Content of the Page to be changed when Focus is changed.
                // In this case, we'll lose our Control.
                Control?.ClearFocus();
                _dialog = null;
            });

            builder.SetPositiveButton("OK", (s, a) =>
            {
                ElementController.SetValueFromRenderer(Picker.SelectedIndexProperty, picker.Value);
                // It is possible for the Content of the Page to be changed on SelectedIndexChanged.
                // In this case, the Element & Control will no longer exist.
                if (Element != null)
                {
                    if (model.Items.Count > 0 && Element.SelectedIndex >= 0)
                        Control.Text = model.Items[Element.SelectedIndex];
                    ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                    // It is also possible for the Content of the Page to be changed when Focus is changed.
                    // In this case, we'll lose our Control.
                    Control?.ClearFocus();
                }
                _dialog = null;
            });

            _dialog = builder.Create();
            _dialog.DismissEvent += (ssender, args) =>
            {
                ElementController?.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
            };
            _dialog.Show();
        }

        //public LayerDrawable AddPickerStyles(string imagePath)
        //{
        //    ShapeDrawable border = new ShapeDrawable();
        //    border.Paint.Color = Android.Graphics.Color.White;
        //    border.SetPadding(10, 10, 10, 10);
        //    border.Paint.SetStyle(Paint.Style.Stroke);

        //    Drawable[] layers = { border, GetDrawable(imagePath) };
        //    LayerDrawable layerDrawable = new LayerDrawable(layers);
        //    layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

        //    return layerDrawable;
        //}

        //private BitmapDrawable GetDrawable(string imagePath)
        //{
        //    int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
        //    var drawable = ContextCompat.GetDrawable(this.Context, resID);
        //    var bitmap = ((BitmapDrawable)drawable).Bitmap;

        //    var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 70, 70, true));
        //    result.Gravity = Android.Views.GravityFlags.Right;

        //    return result;
        //}

    }
}
