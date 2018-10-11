using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(DVidyaERP.Droid.Effects.ShowHidePassEffect), "ShowHidePassEffect")]
namespace DVidyaERP.Droid.Effects
{
    public class ShowHidePassEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            ConfigureControl();
        }

        protected override void OnDetached()
        {
        }

        private void ConfigureControl()
        {
            EditText editText = ((EditText)Control);
            var name = editText.Hint;
            if (name.Trim().Contains("Email.."))
            {
                editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.style_Emailicon, 0, 0, 0);
            }
            else if (name.Trim().Contains("Password.."))
            {
                editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.style_PasswordIcon, 0, Resource.Drawable.style_ForgetHidePass, 0);
                editText.SetOnTouchListener(new OnDrawableTouchListener());
            }
            else if (name.Trim().Contains("IP"))
            {
                editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.style_IPAddress, 0, 0, 0);
            }
            else if (name.Trim().Contains("Port"))
            {
                editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.style_PortIcon, 0, 0, 0);
            }
            else
            {
                //editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.style_LeftSpace, 0, 0, 0);

            }


        }
    }

    public class OnDrawableTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (v is EditText && e.Action == MotionEventActions.Up)
            {
                EditText editText = (EditText)v;
                if (e.RawX >= (editText.Right - editText.GetCompoundDrawables()[2].Bounds.Width()))
                {
                    if (editText.TransformationMethod == null)
                    {
                        editText.TransformationMethod = PasswordTransformationMethod.Instance;
                        var name = editText.Hint;
                        if (name.Trim().Contains("password"))
                        {
                            editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.style_PasswordIcon, 0, Resource.Drawable.style_ForgetShowPass, 0);
                            editText.SetOnTouchListener(new OnDrawableTouchListener());
                        }
                    }
                    else
                    {
                        editText.TransformationMethod = null;
                        var name = editText.Hint;
                        if (name.Trim().Contains("password"))
                        {
                            editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.style_PasswordIcon, 0, Resource.Drawable.style_ForgetHidePass, 0);
                            editText.SetOnTouchListener(new OnDrawableTouchListener());
                        }
                    }

                    return true;
                }
            }

            return false;
        }
    }


}

