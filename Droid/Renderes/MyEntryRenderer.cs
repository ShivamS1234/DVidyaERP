using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using DVidyaERP.Droid;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Support.V4.Content;

[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]

namespace DVidyaERP.Droid
{
    class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var nativeEditText = (global::Android.Widget.EditText)Control;
                var element = (Entry)this.Element;
                string strleftBlankSpace= nativeEditText.Hint;
                nativeEditText?.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.style_Entry));
            }
        }
    }
}