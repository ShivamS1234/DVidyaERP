using System;
using System.Collections.Generic;
using System.Linq;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace DVidyaERP.CustomControl
{
    public class NonSelectableViewCell : ViewCell
    {

      public NonSelectableViewCell()
        {
            string fontfamily = string.Empty;
            Color _colorBoxViw = Color.Transparent;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    fontfamily = "MAISONNEUE-BOOK";
                    _colorBoxViw = App.BrandColor;
                    break;
                default:
                    fontfamily = "MAISONNEUE-BOOK.TTF#MAISONNEUE-BOOK";
                    _colorBoxViw = App.BrandColor;
                    break;
            }

            try
            {
                //declare here for student image
                var imgcircle = new CircleImage
                {
                    BorderColor = Color.White,
                    BorderThickness = 2,
                    HeightRequest = 40,
                    WidthRequest = 40,
                    BackgroundColor = Color.Transparent,
                    Aspect = Aspect.Fill,
                    HorizontalOptions = LayoutOptions.Start,
                    //Source= ImageSource.FromFile("shivam.png"),
                    FillColor = Color.Black,
                };
               
               
                //here we are declare child grid
                var childGrid = new Grid
                {
                    Padding = new Thickness(5, 5, 5, 5),
                    RowDefinitions = {
                                new RowDefinition{ Height=new GridLength(1, GridUnitType.Auto) },
                                new RowDefinition{ Height=new GridLength(15, GridUnitType.Star) }
                            },
                    ColumnDefinitions = {
                                new ColumnDefinition { Width = GridLength.Auto },
                                new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Star },
                            },
                    BackgroundColor = Color.WhiteSmoke,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                var _status = new Label()
                {
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    TextColor = App.BrandColor
                };
                //declare here for student name
                var _StudentNametext = new Label()
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };
                _StudentNametext.FontFamily = App.fontFamilyHead;
                _StudentNametext.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                _StudentNametext.TextColor = Color.Black;

                //declare here for student id
                var _StudentIDtext = new Label()
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.Center
                };
                _StudentIDtext.FontFamily = App.fontFamilyHead;
                _StudentIDtext.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                _StudentIDtext.TextColor = Color.Black;

                //imgcircle.SetBinding(Image.SourceProperty, new Binding("image",BindingMode.Default,source:));
                imgcircle.SetBinding(Image.SourceProperty, "ImageUri");
                //imgcircle.Source = GetDefaultImageSource();
                _StudentNametext.SetBinding(Label.TextProperty, new Binding("name"));
                _StudentIDtext.SetBinding(Label.TextProperty, new Binding("id"));
                _status.SetBinding(Label.TextProperty,"status");

                childGrid.Children.Add(imgcircle, 0, 0);
                Grid.SetRowSpan(imgcircle, 2);
                childGrid.Children.Add(_StudentIDtext, 1, 0);
                childGrid.Children.Add(_StudentNametext, 1, 1);
                childGrid.Children.Add(_status, 2, 0);
                Grid.SetRowSpan(_status, 2);

                View = childGrid;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

       
        const int avgCharsInRow = 49;
        const int defaultHeight = 50;
        const int extraLineHeight = 30;
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (Device.OS == TargetPlatform.iOS)
            {
                object b = View.GetValue(BindingContextProperty);
                string text = Convert.ToString(b);
                if (string.IsNullOrEmpty(text))
                    return;
                var len = text.Length;
                if (len < (avgCharsInRow * 2))
                {
                    // fits in one cell
                    Height = defaultHeight;
                }
                else
                {
                    len = len - (avgCharsInRow * 2);
                    var extraRows = len / 49;
                    Height = defaultHeight + extraRows * extraLineHeight;
                }
            }
        }

    }
  
}
