using System;  
using System.ComponentModel;  
using System.Windows.Input;  
using Xamarin.Forms;  
namespace DVidyaERP.ViewModels  
{  
    public class ConnectionPageViewModel : BaseViewModel 
    {  
        public Action CommendService,DisplayInvalidIpAddress,DisplayInvalidPort;  
        private string ipAddress;  
        private string port; 

        public ConnectionPageViewModel()  
        {  
            SubmitCommand = new Command(OnSubmit);  
        } 

        public string IPAddress
        {  
            get { return ipAddress; }  
            set  
            {  
                ipAddress = value;  
                this.OnPropertyChanged("IPAddress");  
            }  
        }  
           
        public string Port
        {  
            get { return port; }  
            set  
            {  
                port = value;  
                this.OnPropertyChanged("Port");  
            }  
        }  
        public ICommand SubmitCommand { protected set; get; }  
 
        public void OnSubmit()  
        {
            if (string.IsNullOrEmpty(ipAddress))
            {
                DisplayInvalidIpAddress();
            }
            else if (string.IsNullOrEmpty(port))
            {
                DisplayInvalidPort();
            }
            else
            {
                CommendService();  
            }  
        }  
    }  
}  
