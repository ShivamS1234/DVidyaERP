using System;  
using System.ComponentModel;  
using System.Windows.Input;  
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Globalization;
using Plugin.Settings;
using DVidyaERP.Global_Method_Propertise;

namespace DVidyaERP
{
    public class NoticeViewModel : BaseViewModel
    {
        
       

        public NoticeViewModel()
        {
            if(string.IsNullOrEmpty(headerText))
            {
                headerText = "Notice is not available right now !" ;            
            }
        }

        private string headerText { get; set; }
        public string HeaderText
        {
            get { return headerText; }
            set
            {
                headerText = value;
                this.OnPropertyChanged("HeaderText");
            }
        }

        public ObservableCollection<Noitce> _lstNotice;
        public ObservableCollection<Noitce> lstNotice
        {
            get { return _lstNotice; }
            set
            {
                if (_lstNotice != value)
                {
                    _lstNotice = value;
                    this.OnPropertyChanged("lstNotice");
                }
            }
        }
    }
    public class Noitce
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
