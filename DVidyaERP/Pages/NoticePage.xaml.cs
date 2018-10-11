using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DVidyaERP.Pages
{
    public partial class NoticePage : ContentPage
    {
        NoticeViewModel _NoticeViewModel;
        public NoticePage()
        {
            try
            {
                InitializeComponent();
                this.Title = "Notice Board";
                _NoticeViewModel = new NoticeViewModel();
                BindingContext = _NoticeViewModel;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
