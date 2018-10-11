
using System;

using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Abstractions;
using Plugin.Settings;
using DVidyaERP.Helpers;

namespace DVidyaERP
{
    public class SettingsUserView : ContentView
    {
        public ProfileViewModel profileViewModel;
        public Button btnProfile;
        public CircleImage imgCircle;
        public SettingsUserView()
        {
       
            try
            {
                profileViewModel = new ProfileViewModel();
                profileViewModel.ExecuteGetCPFeedCommand();
                BindingContext = profileViewModel;

                int profileBorderRadius = 13, profileHeight = 25, profileWidth = 70, TOPPadding = 30;
                if (Device.OS == TargetPlatform.Android)
                {
                    profileBorderRadius = 20;
                    profileHeight = 25;
                    profileWidth = 70;
                    TOPPadding = 0;
                }

                btnProfile = new Button
                {
                    Text = "Profile",
                    TextColor = Color.WhiteSmoke,
                    Margin = new Thickness(0, 2, 2, 0),
                    BackgroundColor = App.BrandColor,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HeightRequest = profileHeight,
                    WidthRequest = profileWidth,
                    Image = new FileImageSource { File = "pencilicon.png" },
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    BorderWidth = 2,
                    BorderColor = Color.White,
                    Command = profileViewModel.EditCommand,
                    BorderRadius = profileBorderRadius,
                    FontSize = 12,
                    ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left, 0),
                };
                //for profile image
                imgCircle = new CircleImage
                {
                    BorderColor = Color.White,
                    BorderThickness = 3,
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Aspect = Aspect.Fill,
                    HorizontalOptions = LayoutOptions.Center,
                    Source = ImageSource.FromFile("logojpg.jpg"),
                    FillColor=Color.White,

                };

                //imgCircle.SetBinding(CircleImage.SourceProperty,new Binding("imageSource"));

                var imageName = CrossSettings.Current.GetValueOrDefault("UserImage", "");
                if (!string.IsNullOrEmpty(imageName))
                {
                    var imagepath = DependencyService.Get<IPicture>().GetPictureFromDisk(imageName);
                    imgCircle.Source = ImageSource.FromFile(imagepath);
                }
                //for emailID
                var labelEmailID = new Label()
                {
                    Text = "Demo@DVidya.com",
                    TextColor = Color.White,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };
                labelEmailID.SetBinding(Label.TextProperty,new Binding("DisplayemailID"));

                //for Display name
                var labelDisplayName = new Label()
                {
                    Text = "DVidya ERP",
                    TextColor = Color.WhiteSmoke,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };
                var DisplayName = CrossSettings.Current.GetValueOrDefault("UserDisplayName", "");
                labelDisplayName.SetBinding(Label.TextProperty, new Binding("DisplayName"));

                Content = new StackLayout()
                {
                    Padding = new Thickness(0, TOPPadding, 0, 0),
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = 
                    {
                    btnProfile,
                    imgCircle,
                    labelEmailID,
                    labelDisplayName,
                    }
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
