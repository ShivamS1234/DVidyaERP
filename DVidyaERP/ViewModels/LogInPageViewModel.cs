using System;  
using System.ComponentModel;  
using System.Windows.Input;  
using Xamarin.Forms;
using DVidyaERP;
using Plugin.Settings;
using DVidyaERP.Global_Method_Propertise;

namespace DVidyaERP.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        public Action CommendService, DisplayInvalidEmail, DisplayInvalidPassword,DisplayWrongEmail,DisplayWrongPassword,DisplayWrongUserType;
        private string emailID;
        private string cons_emailID;
        private string password;
        private string cons_password;
        private int usertype;
        private int cons_usertype;

        public LogInPageViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            setLogInData();
        }

        private void setLogInData()
        {
            var login = App.Database.GetLogInAsync().Result;
            if (login != null)
            {
                ProfileViewModel.id = login.Code;
                emailID = login.Email;
                usertype = login.UserType;

                cons_emailID = emailID;
                cons_password = login.Password;
                cons_usertype = CrossSettings.Current.GetValueOrDefault("UserType", 0);;
            }

        }

        public string Email
        {
            get { return emailID; }
            set
            {
                emailID = value;
                this.OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                this.OnPropertyChanged("Password");
            }
        }
        public int UserType
        {
            get { return usertype; }
            set
            {
                usertype = value;
                this.OnPropertyChanged("UserType");
            }
        }
        public ICommand SubmitCommand { protected set; get; }

        public void OnSubmit()
        {
            if (string.IsNullOrEmpty(emailID))
            {
                DisplayInvalidEmail();
            }
            else if (string.IsNullOrEmpty(password))
            {
                DisplayInvalidPassword();
            }
            else
            {
                CommendService();
               
            }
        }

        public bool OnOfflineValidation()
        {
            if (!cons_emailID.Equals(emailID))
            {
                DisplayWrongEmail();
                return false;
            }
            else if (!cons_password.Equals(password))
            {
                DisplayWrongPassword();
                return false;
            }
            else if (!cons_usertype.Equals(usertype))
            {
                DisplayWrongUserType();
                return false;
            }
            return true;
        }
       
    }
}
