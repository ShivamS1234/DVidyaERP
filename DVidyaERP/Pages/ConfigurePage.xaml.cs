using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DVidyaERP.ViewModels;


namespace DVidyaERP
{
    public partial class ConfigurePage : ContentPage
    {
        ConfigruePageViewModel vm;
        public ConfigurePage()
        {
            InitializeComponent();
            Title = "    Configure";
            vm= new ConfigruePageViewModel();
            vm.CommandCheckInterNetConnection += () =>
            {
                DisplayAlert(Constant.ConstantsMSG.InternetConnectionTitle, Constant.ConstantsMSG.InternetConnectionMSG, "OK");
            };

            BindingContext = vm;
        }
    }
}
