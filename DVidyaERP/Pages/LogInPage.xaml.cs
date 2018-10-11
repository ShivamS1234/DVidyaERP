using System;
using System.Collections.Generic;
using DVidyaERP.Views;
using Xamarin.Forms;
using DVidyaERP.ViewModels;
using DVidyaERP.Global_Method_Propertise;
using DVidyaERP.Constant;
using System.Threading.Tasks;
using Plugin.Settings;
using Acr.UserDialogs;

namespace DVidyaERP
{
    public partial class LogInPage : ContentPage
    {
        #region private_Propertise
        Dictionary<string, int> nameToUserType = new Dictionary<string, int>
        {
            { "Management", 1 },
            { "Faculty", 2 }, 
            { "Student", 3 },
            { "Parent", 4 },
        };
        GeneralMethod generalMethod = new GeneralMethod();
        #endregion

        #region constructor
        public LogInPage()
        {
            try
            {
            InitializeComponent();
                var vm = new LogInPageViewModel()
                {
                    //Email="raja@gmail.com",
                    //Password="raju"    
                }; 

            vm.DisplayInvalidEmail += () =>
            {
                    DisplayAlert(ConstantsMSG.Warning, ConstantsMSG.LogInEmailEmpty_Warning, "OK");
            };
            vm.DisplayInvalidPassword += () =>
            {
                    DisplayAlert(ConstantsMSG.Warning, ConstantsMSG.LogInPasswordEmpty_Warning, "OK");
            };
                vm.DisplayWrongEmail += () =>
                {
                    DisplayAlert(ConstantsMSG.Warning, ConstantsMSG.LogInEmailFormat_Warning, "OK");
                };
                vm.DisplayWrongPassword += () =>
                {
                    DisplayAlert(ConstantsMSG.Warning, ConstantsMSG.LogInPasswordFormat_Warning, "OK");
                };
                vm.DisplayWrongUserType += () =>
                {
                    DisplayAlert(ConstantsMSG.Warning, ConstantsMSG.LogInUserType_Warning, "OK");
                };

            vm.CommendService += () =>
            {
              if(entryEmail.TextColor == Color.Red)   
              {
                       DisplayAlert(ConstantsMSG.Warning, ConstantsMSG.LogInEmailFormat_Warning, "OK");
              }
              else if (entryPassword.TextColor == Color.Red)
              {
                        DisplayAlert(ConstantsMSG.Warning, ConstantsMSG.LogInPasswordFormat_Warning, "OK");          
              }
              else
              {
                        if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                        {
                            OnClick();  
                        }
                        else
                        {
                            vm.UserType=(int)(UserType.enumUserType)pickerUserType.SelectedIndex + 1;
                        bool status = vm.OnOfflineValidation();
                            if (status)
                            {
                                offLine();  
                            }        
                        }
              }
                 
            };
            //set index here
            entryEmail.Completed += (object sender, EventArgs e) =>  
            {  
                    entryPassword.Focus();  
            };  
  
            entryPassword.Completed += (object sender, EventArgs e) =>  
            {  
                vm.SubmitCommand.Execute(null);  
            }; 

                this.BindingContext = vm; // No need for all the ceremony of a viewmodel in this case. Just bind to ourself.
                var backConnectionPage_tap = new TapGestureRecognizer();
                backConnectionPage_tap.Tapped += (s, e) =>
                {
                    backPage();
                };
                lblBack.GestureRecognizers.Add(backConnectionPage_tap);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        public async Task offLine()
        {
            var v = await DisplayAlert(Constant.ConstantsMSG.InternetConnectionTitle, Constant.ConstantsMSG.offlineMSG, "Yes", "No");
            if (v)
            {
                Constants.Offline = true;
                int userType = CrossSettings.Current.GetValueOrDefault("UserType", 0);
                UserType.currentUserType = (UserType.enumUserType)userType;
                OnClick();
            }
        }
        #endregion
        protected override void OnAppearing()
        {
            base.OnAppearing();
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    pickerUserType.Margin = new Thickness(0, 0, 0, 0);
                    break;
            }
            //if (string.IsNullOrEmpty(_email))
            //{ entryEmail.Text = _email; }

            pickerUserType.Items.Clear();
            foreach (string colorName in nameToUserType.Keys)
            {
                pickerUserType.Items.Add(colorName);
            }
            pickerUserType.SelectedIndex = 1;

        }

        async void backPage()
        {
            Constants.backPage = true;
            NavigationPage _connectionPage;
                _connectionPage = new NavigationPage(new connectionPage())
                {

                };

            //await Navigation.PopModalAsync();
            await Navigation.PushModalAsync(_connectionPage,true);  
        }

        async void OnClick()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Login.....");
                if (Constants.Offline == false)
                {
                    if (generalMethod.logInService(entryEmail.Text.Trim(), entryPassword.Text.Trim(), (UserType.enumUserType)pickerUserType.SelectedIndex + 1 ))
                    {
                        var rootpage = new NavigationPage(
                                    new RootPage())
                        {
                            BarBackgroundColor = App.BrandColor,
                            BarTextColor = Color.White,
                        };

                        if (Device.OS == TargetPlatform.Android)
                            Application.Current.MainPage = new NavigationPage(new RootPage());
                        else
                            await Navigation.PushModalAsync(rootpage, true);
                    }
                    else
                    {
                        DisplayAlert(ConstantsMSG.Warning, generalMethod.StrMSG, "OK");
                    }
                }
                else
                {
                    
                    if (Device.OS == TargetPlatform.Android)
                    {
                        Application.Current.MainPage = new NavigationPage(new RootPage());   
                    }
                    else
                    {
                        var rootpage = new NavigationPage(
                                   new RootPage())
                     {
                         BarBackgroundColor = App.BrandColor,
                         BarTextColor = Color.White,
                     };
                        await Navigation.PushModalAsync(rootpage, true);   
                    }     
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
