using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DVidyaERP.ViewModels;
using System.Threading.Tasks;
using DVidyaERP.Global_Method_Propertise;
using DVidyaERP.Constant;
using DVidyaERP.Core.Services;
using Acr.UserDialogs;

namespace DVidyaERP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class connectionPage : ContentPage
    {
        GeneralMethod generalMethod = new GeneralMethod();
        ConnectionPageViewModel vm = new ConnectionPageViewModel();

        public connectionPage()
        {
            try
            {
                //for testing purpose
                vm.IPAddress = ServiceAPIInfo.httpAddress;
                vm.Port = ServiceAPIInfo.port;
                //end
            vm.DisplayInvalidIpAddress += () =>
            {
                    DisplayAlert(ConstantsMSG.Warning, ConstantsMSG.IPAddressEmpty_Warning, "OK");
            };
            vm.DisplayInvalidPort += () =>
            {
                    DisplayAlert(ConstantsMSG.Warning, ConstantsMSG.PortEmpty_Warning, "OK");
            };
            vm.CommendService += () =>
            {
              if(entryIPAdress.TextColor == Color.Red)   
              {
                        DisplayAlert(ConstantsMSG.Warning, ConstantsMSG.IPAddressformat_Warning, "OK");
              }
              else if (entryPort.TextColor == Color.Red)
              {
                        DisplayAlert(ConstantsMSG.Warning, ConstantsMSG.PortFormat_Warning, "OK");          
              }
              else
              {
                    if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                    {
                            Connect();
                    }
                    else
                    {
                        DisplayAlert(Constant.ConstantsMSG.InternetConnectionTitle, Constant.ConstantsMSG.InternetConnectionMSG, "OK");
                    }

              }
                 
            };
            InitializeComponent(); 
            this.BindingContext = vm;
             
            
            entryIPAdress.Completed += (object sender, EventArgs e) =>  
            {  
                entryPort.Focus();  
            };  
  
            entryPort.Completed += (object sender, EventArgs e) =>  
            {  
                vm.SubmitCommand.Execute(null);  
            };  
            }
            catch(Exception ex)
            {
                
            }
           
        }

        async void Connect()
        {
            string msg="";
            if (string.IsNullOrEmpty(entryIPAdress.Text) || string.IsNullOrEmpty(entryPort.Text))
            {
                return;
            }
            try
            {
                UserDialogs.Instance.ShowLoading("Connect....");
                if (generalMethod.CheckConnection(out msg, entryIPAdress.Text.Trim(), entryPort.Text.Trim()))
                {
                    generalMethod.setConnectionDataInCollector(entryIPAdress.Text, entryPort.Text);
                    NavigationPage logIn;
                    logIn = new NavigationPage(new LogInPage())
                    {

                    };
                    await Navigation.PushModalAsync(logIn, true);
                }
                else
                {
                    DisplayAlert(ConstantsMSG.Warning, msg, "OK");
                } 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Constants.backPage==false && (!string.IsNullOrEmpty(Application.Current.Properties["IpAddress"].ToString()) &&  !string.IsNullOrEmpty(Application.Current.Properties["Port"].ToString())))
            {
                vm.IPAddress = Application.Current.Properties["IpAddress"].ToString();
                vm.Port=Application.Current.Properties["Port"].ToString();
                Connect();
            }
            Constants.backPage = false;
        }



    }

   
}
