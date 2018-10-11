//using System;
using System;
using System.Collections.Generic;
using System.Linq;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace DVidyaERP.CustomControl
{
    [ContentProperty(nameof(DataView))]
    public class SelectableViewCell : ViewCell
    {
        private Grid rootGrid;
        private View dataView;
        private View checkView;

        public SelectableViewCell()
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
                            rootGrid = new Grid
                            {
                                Padding = new Thickness(5, 5, 5, 5),
                                ColumnDefinitions = {
                                new ColumnDefinition { Width = GridLength.Star },
                                new ColumnDefinition { Width = GridLength.Auto }
                            },
                                BackgroundColor = Color.WhiteSmoke
                            };
                            View = rootGrid;

                            var check = new BoxView
                            {
                                Color=_colorBoxViw,
                                HeightRequest=12,
                                WidthRequest=12
                            };
                            CheckView = check;

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
                                new ColumnDefinition { Width = GridLength.Auto }
                            },
                                BackgroundColor = Color.Red,
                                Margin = new Thickness(0, 5, 0, 0)
                            };

                            //declare here for student name
                            var _StudentNametext = new Label()
                            {
                                HorizontalOptions = LayoutOptions.StartAndExpand,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                TextColor = App.BrandColor
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

                            childGrid.Children.Add(imgcircle, 0, 0);
                            Grid.SetRowSpan(imgcircle, 2);
                            childGrid.Children.Add(_StudentIDtext, 1, 0);
                            childGrid.Children.Add(_StudentNametext, 1, 1);
                            DataView = childGrid;
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
        }

        public View CheckView
        {
            get { return checkView; }
            set
            {
                // jump out if the value is the same or something happened to our layout
                if (checkView == value || View != rootGrid)
                    return;

                OnPropertyChanging();

                // remove the old binding
                if (checkView != null)
                {
                    checkView.RemoveBinding(VisualElement.IsVisibleProperty);
                    rootGrid.Children.Remove(checkView);
                    View.BackgroundColor = Color.White;
                }

                checkView = value;

                // add the new binding
                if (checkView != null)
                {
                    checkView.SetBinding(VisualElement.IsVisibleProperty, nameof(SelectableItem.IsSelected));
                    //set background color
                    View.SetBinding(VisualElement.BackgroundColorProperty, nameof(SelectableItem.BackgroundColor));
                    Grid.SetColumn(checkView, 1);
                    Grid.SetColumnSpan(checkView, 1);
                    Grid.SetRow(checkView, 0);
                    Grid.SetRowSpan(checkView, 1);
                    checkView.HorizontalOptions = LayoutOptions.End;
                    checkView.VerticalOptions = LayoutOptions.Center;
                    rootGrid.Children.Add(checkView);

                }

                OnPropertyChanged();
            }
        }


        public View DataView
        {
            get { return dataView; }
            set
            {
                // jump out if the value is the same or something happened to our layout
                if (dataView == value || View != rootGrid)
                    return;

                OnPropertyChanging();

                // remove the old view
                if (dataView != null)
                {
                    dataView.RemoveBinding(BindingContextProperty);
                    rootGrid.Children.Remove(dataView);
                }

                dataView = value;

                // add the new view
                if (dataView != null)
                {
                    dataView.SetBinding(BindingContextProperty, nameof(SelectableItem.Data));
                    //set background color
                    dataView.SetBinding(VisualElement.BackgroundColorProperty, nameof(SelectableItem.BackgroundColor));
                    Grid.SetColumn(dataView, 0);
                    Grid.SetColumnSpan(dataView, 1);
                    Grid.SetRow(dataView, 0);
                    Grid.SetRowSpan(dataView, 1);
                    dataView.HorizontalOptions = LayoutOptions.StartAndExpand;
                    dataView.VerticalOptions = LayoutOptions.CenterAndExpand;
                    rootGrid.Children.Add(dataView);
                }

                OnPropertyChanged();
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
                object b = dataView.GetValue(BindingContextProperty);
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
