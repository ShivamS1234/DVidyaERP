using System;
using System.Collections.Generic;
using DVidyaERP.Global_Method_Propertise;
using DVidyaERP.Models;
using Plugin.Settings;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace DVidyaERP
{
    public partial class TimeTablePage : ContentPage
    {
        TimeTableModel _TimeTableModel;
        public TimeTablePage()
        {
            try
            {
                InitializeComponent();
                Title = "Time Table";
                _TimeTableModel = new TimeTableModel();
                this.BindingContext = _TimeTableModel;
                //auto start
                //MessagingCenter.Subscribe<string>("SyncTimeTable", "SyncTimeTable", (sender) => {
                //    int code = MyProfile.id;
                //    int usertype = CrossSettings.Current.GetValueOrDefault("UserType", 0);
                //    GeneralMethod.syncTimeTable(code, (UserType.enumUserType)usertype);
                //});
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);   
            }

        }

        public void HeaderTapped(object sender, EventArgs args)
        {
            var da=(TimeTableGroup)((Button)sender).CommandParameter;

           // int selectedIndex =_TimeTableModel._expandedGroups.IndexOf(da);
            int selectedIndex = TimeTableGroup.getWeakIndex(da.ShortName);
            if(selectedIndex >= 0 )
            {
                _TimeTableModel._allGroups[selectedIndex].Expanded = !_TimeTableModel._allGroups[selectedIndex].Expanded;
                _TimeTableModel.UpdateListContent();
                GroupedView.ItemsSource = _TimeTableModel.GroupedViewSource;  
            }
        }

      
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UserType.currentAttendanceType = UserType.attendanceType.TimeTable;
            if (MasterChooseDetails.ClassMaster_Code == 0 || MasterChooseDetails.SectionMaster_Code == 0 || MasterChooseDetails.StreamMaster_Code == 0)
            {
                OnImageClicked_ChangeMeals();
            }
            else
            {
                TimeTableGroup.LoadTimeTable();
                _TimeTableModel._allGroups = TimeTableGroup.All;
                _TimeTableModel.UpdateListContent();
                GroupedView.ItemsSource = _TimeTableModel.GroupedViewSource; 
            }

        }
        async void OnImageClicked_ChangeMeals()
        {
            var page = new GetStudentsPagePopUp();
            await Navigation.PushPopupAsync(page);
        }

        void OnTapImageButtonFOBTapped(object sender, EventArgs args)
        {
            OnImageClicked_ChangeMeals();
        }
    }
}
