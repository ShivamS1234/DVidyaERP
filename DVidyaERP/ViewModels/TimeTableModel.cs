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
    public class TimeTableModel : BaseViewModel
    {
        public ObservableCollection<TimeTableGroup> _allGroups;
        public ObservableCollection<TimeTableGroup> _expandedGroups { get; set; }
        private string headerText { get; set; }
        string todayDate = string.Empty;

        public TimeTableModel()
        {
            _allGroups = TimeTableGroup.All;
            DateTime now = DateTime.Now.ToLocalTime();
            todayDate = now.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
            var ymonth = Convert.ToDateTime(todayDate).Month;
            var mweak = (Convert.ToDateTime(todayDate).Day-1) / 7 + 1;
            headerText = "Current Available Time Table for this Month: " + ymonth + " and Weak : " + mweak;
            //MessagingCenter.Send<string>("SyncTimeTable", "SyncTimeTable");
            UpdateListContent();
        }
      

        public  void UpdateListContent()
        {
            try
            {
                _expandedGroups = new ObservableCollection<TimeTableGroup>();
                foreach (TimeTableGroup group in _allGroups)
                {
                    //Create new FoodGroups so we do not alter original list
                    TimeTableGroup newGroup = new TimeTableGroup(group.Title, group.ShortName, group.Expanded);
                    //Add the count of food items for Lits Header Titles to use
                    newGroup.FoodCount = group.Count;
                    if (group.Expanded)
                    {
                        foreach (Weak lecture in group)
                        {
                            newGroup.Add(lecture);
                        }
                    }
                    _expandedGroups.Add(newGroup);
                }  
            }
            catch(Exception ex)
            {
                
            }
        }

        public string HeaderText
        {
            get { return headerText; }
            set
            {
                    headerText = value;
                    this.OnPropertyChanged("HeaderText");
            }
        }

        public ObservableCollection<TimeTableGroup> GroupedViewSource
        {
            get { return _expandedGroups; }
            set
            {
                if (_expandedGroups != value)
                {
                    _expandedGroups = value;
                    this.OnPropertyChanged("GroupedViewSource");
                }
            }
        }
    }
}
