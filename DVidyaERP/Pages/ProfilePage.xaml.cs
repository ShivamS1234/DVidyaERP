using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using DVidyaERP.Helpers;
using DVidyaERP.Views;
using Plugin.Settings;
using Xamarin.Forms;

namespace DVidyaERP
{
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel _profileViewModel;
        public ProfilePage()
        {
            InitializeComponent();
            _profileViewModel = new ProfileViewModel();
            BindingContext = _profileViewModel;
            _profileViewModel.ExecuteGetCPFeedCommand();

            var imageName = CrossSettings.Current.GetValueOrDefault("UserImage", "");
            if (!string.IsNullOrEmpty(imageName))
            {
                var imagepath = DependencyService.Get<IPicture>().GetPictureFromDisk(imageName);
                ProfilePic.Source = ImageSource.FromFile(imagepath);
            }

            Title = "Profile Page";
            this.btn1.Text = "Edit";
            entryName.IsEnabled = false;
            entryEmail.IsEnabled = false;
            entryMobileNo.IsEnabled = false;
        }
        public ProfilePage(bool IsEdit, ProfileViewModel _vm)
        {
            _profileViewModel = _vm;
            InitializeComponent();
            this.btn1.Text = "Save";
            var imageName = CrossSettings.Current.GetValueOrDefault("UserImage", "");
            if (!string.IsNullOrEmpty(imageName))
            {
                var imagepath = DependencyService.Get<IPicture>().GetPictureFromDisk(imageName);
                ProfilePic.Source = ImageSource.FromFile(imagepath);
            }
            Title = "Profile Page";
            entryName.IsEnabled = true;
            entryEmail.IsEnabled = false;
            entryMobileNo.IsEnabled = true;
            BindingContext = _profileViewModel;
            _profileViewModel.ExecuteGetCPFeedCommand();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                _profileViewModel.SelectPhoto();
            };
            ProfilePic.GestureRecognizers.Add(tapGestureRecognizer);
        }
        async void Handle_EditClicked(object sender, EventArgs e)
        {
            if (this.btn1.Text == "Edit")
            {
                await Navigation.PushAsync(new ProfilePage(true,_profileViewModel));
            }
            else if (this.btn1.Text == "Save")
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Saving.....");
                    var Status = await _profileViewModel.CheckValidation();
                    UserDialogs.Instance.HideLoading();
                    if (Status)
                    {
                        UserDialogs.Instance.Alert("Your profile has updated successful!", "Alert", "Ok");
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("Your profile is not updated please try again", "Alert", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }
    }
}
