
using System;
using DVidyaERP.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]
namespace DVidyaERP.iOS
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            try
            {
            if (e.OldElement == null)
            {
                if (Control != null)
                {
                    UITextField vUpdatedEntry = (UITextField)Control;
                    var buttonRect = UIButton.FromType(UIButtonType.Custom);
                    //UITextView vtUpdatedEntry = (UITextView)Control;
                    //var v = vtUpdatedEntry.Text;
                    var econtrol = e.NewElement.Placeholder;

                    if (econtrol.Trim().Contains("IP"))
                    {
                        buttonRect.SetImage(new UIImage("server"), UIControlState.Normal);
                    }
                    else if (econtrol.Trim().Contains("Port"))
                    {
                        buttonRect.SetImage(new UIImage("Port"), UIControlState.Normal);
                    }
                    else  if (econtrol.Trim().Contains("Email.."))
                    { 
                        buttonRect.SetImage(new UIImage("email"), UIControlState.Normal);
                            //vUpdatedEntry.TextColor = UIColor.Black;
                    }
                    else if (econtrol.Trim().Contains("Password.."))
                    {
                        buttonRect.SetImage(new UIImage("HidePass"), UIControlState.Normal);
                        buttonRect.TouchUpInside += (object sender, EventArgs e1) =>
                        {
                            if (vUpdatedEntry.SecureTextEntry)
                            {
                                vUpdatedEntry.SecureTextEntry = false;
                                buttonRect.SetImage(new UIImage("ShowPass"), UIControlState.Normal);
                            }
                            else
                            {
                                vUpdatedEntry.SecureTextEntry = true;
                                buttonRect.SetImage(new UIImage("HidePass"), UIControlState.Normal);
                            }
                        };

                        vUpdatedEntry.ShouldChangeCharacters += (textField, range, replacementString) =>
                        {
                            string text = vUpdatedEntry.Text;
                            var result = text.Substring(0, (int)range.Location) + replacementString + text.Substring((int)range.Location + (int)range.Length);
                            vUpdatedEntry.Text = result;
                            return false;
                        };
                    }

                    buttonRect.Frame = new CoreGraphics.CGRect(10.0f, 0.0f, 15.0f, 15.0f);
                    buttonRect.ContentMode = UIViewContentMode.Right;
                    buttonRect.BackgroundColor = UIColor.Clear;
                    //buttonRect.Layer.BorderColor=UIColor.White.CGColor;


                    UIView paddingViewRight = new UIView(new System.Drawing.RectangleF(5.0f, -5.0f, 30.0f, 18.0f));
                    paddingViewRight.Add(buttonRect);
                    paddingViewRight.ContentMode = UIViewContentMode.BottomRight;

                    vUpdatedEntry.LeftView = paddingViewRight;
                    vUpdatedEntry.LeftViewMode = UITextFieldViewMode.Always;
                    vUpdatedEntry.BorderStyle = UITextBorderStyle.None;
                    vUpdatedEntry.Layer.BorderColor = UIColor.White.CGColor;

                    Control.Layer.CornerRadius = 20;
                    Control.Layer.BorderWidth = 1f;
                    Control.Layer.MasksToBounds = true;
                    vUpdatedEntry.TextAlignment = UITextAlignment.Left;
                }
            }
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());

			}
        }

    }
    }