using System;
using DVidyaERP.Views;
using Xamarin.Forms;

namespace DVidyaERP
{
    public class MenuCell : ViewCell
    {
        public string Text
        {
            get { return label.Text; }
            set { label.Text = value; }
        }
        Label label;

        public ImageSource ImageSrc
        {
            get { return image.Source; }
            set { image.Source = value; }
        }
        Image image;
         StackLayout sLayout;
        static StackLayout _previousSLayout;
        static bool _checkMenuFirstTime=true;
        public MenuPage Host { get; set; }

        public MenuCell()
        {
            image = new Image
            {
                HeightRequest = 20,
                WidthRequest = 20,
            };

            string fontfamily = string.Empty;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    fontfamily = "Rockwell-Light";
                    break;
                default:
                    fontfamily = "RCKWLL.ttf#RCKWLL";
                    break;

            }
            image.Opacity = 0.5;

            label = new Label
            {

                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.Gray,
                FontFamily=fontfamily,
                FontAttributes=FontAttributes.Bold,
            };


            sLayout = new StackLayout
            {
                // BackgroundColor = Color.FromHex("2C3E50"),
                BackgroundColor = Color.White,

                Padding = new Thickness(20, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                Spacing = 20,
                //HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { image, label }
            };
            //set intilization properties
            if (_checkMenuFirstTime == true)
            {
                sLayout.BackgroundColor = Color.WhiteSmoke;
                _previousSLayout = sLayout;
                _checkMenuFirstTime = false;
            }
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped +=
                (sender, e) =>
                {
                    var la =  (StackLayout)sender;
                    la.BackgroundColor = Color.WhiteSmoke;
                if (_previousSLayout != null && _previousSLayout != la)
                    {
                    _previousSLayout.BackgroundColor = Color.Default;
                    }
                    _previousSLayout = la;
                    OnTapped();
                };
            sLayout.GestureRecognizers.Add(tapGestureRecognizer);

            View = sLayout;



        }

        protected override void OnTapped()
        {
            base.OnTapped();

            Host.Selected(label.Text);
   
        }
      
    }
}
