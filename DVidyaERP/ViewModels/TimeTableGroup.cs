//using Java.Lang;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVidyaERP.Core.Global_Method_Propertise;
using DVidyaERP.Models;
using Xamarin.Forms;

namespace DVidyaERP
{
    /// <summary>
    /// A list of lists
    /// </summary>
    
    public class TimeTableGroup : ObservableCollection<Weak>, INotifyPropertyChanged
    {
        private bool _expanded;

        public string Title { get; set; }

        public string TitleWithItemCount{
            get {return string.Format("{0} ({1})", Title,FoodCount);}
        }

        public string ShortName { get; set; }

        public bool Expanded{
            get { return _expanded;}
            set{ if (_expanded != value){
                    _expanded = value;
                    OnPropertyChanged("Expanded");
                    OnPropertyChanged("StateIcon");
                }
            }
        }
        
        public string StateIcon{
            get{return Expanded ? "expanded_blue.png" : "collapsed_blue.png";}
        }
        
        public int FoodCount { get; set; }

        public TimeTableGroup(string title, string shortName, bool expanded = true){
            Title = title;
            ShortName = shortName;
            Expanded = expanded;
        }

        public static ObservableCollection<TimeTableGroup> All { private set; get; }
        public static IList<TimeTableModelCell> _timeTableList { get; set; }

      
         static TimeTableGroup()
        {
            try
            {
                string className = string.Empty;
                _timeTableList = new List<TimeTableModelCell>();
                ObservableCollection<TimeTableGroup> Groups;
                TimeTableGroup timeTablegroup;
                _timeTableList = App.Database.getTimeTableData(Propertise.todayDate(), MasterChooseDetails.ClassMaster_Code, MasterChooseDetails.StreamMaster_Code, MasterChooseDetails.SectionMaster_Code).Result;
                if (_timeTableList != null)
                {
                    if (_timeTableList.Count > 0)
                    {
                        Groups = new ObservableCollection<TimeTableGroup>();
                        for (int j = 0; j < _timeTableList.Count(); j++)
                        {
                            //check week day not blank
                            if (!string.IsNullOrEmpty(_timeTableList[j].DayofWeek))
                            {
                                string shortname = string.Empty;
                                string weakName = string.Empty;
                                //here set weak day
                                weakName = getWeakName(j, ref shortname);
                                timeTablegroup = new TimeTableGroup(weakName, shortname, false);
                                #region bind_Lecture
                                //-1 for Holiday and 0 for Break
                                //check subject not blank
                                //for 1 lecture
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture1);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture1);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-1", Description = className });
                                //for 2 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture2);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture2);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-2", Description = className });
                                //for 3 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture3);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture3);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-3", Description = className });
                                //for 4 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture4);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture4);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-4", Description = className });
                                //for 5 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture5);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture5);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-5", Description = className });
                                //for 6 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture6);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture6);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-6", Description = className });
                                //for 7 lecture
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture7);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture7);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-7", Description = className });
                                //for 8 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture8);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture8);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-8", Description = className });
                                //for 9 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture9);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture9);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-9", Description = className });

                                Groups.Add(timeTablegroup);
                                #endregion
                            }
                        }
                        All = Groups;
                    }
                }
                else
                {
                    Groups = new ObservableCollection<TimeTableGroup>
                {
                 new TimeTableGroup("Sunday","S"){
                        new Weak { Name = "Not Avilable" },
                },
                new TimeTableGroup("Monday","M",true){
                        new Weak { Name = "Not Avilable"},
                    },
                new TimeTableGroup("Tuesday","T"){
                        new Weak { Name = "Not Avilable"},
                },
                new TimeTableGroup("Wednesday","W"){
                        new Weak { Name = "Not Avilable"},
                    },
                new TimeTableGroup("Thursday","Th"){
                        new Weak { Name = "Not Avilable"},
                    },
                new TimeTableGroup("Friday","F"){
                        new Weak { Name = "Not Avilable"},
                    },
                new TimeTableGroup("Saturday","Sat"){
                        new Weak { Name = "Not Avilable"},
                }
            };
                    All = Groups;
                } 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        public static void LoadTimeTable()
        {
            try
            {
                string className = string.Empty;
                _timeTableList = new List<TimeTableModelCell>();
                ObservableCollection<TimeTableGroup> Groups;
                TimeTableGroup timeTablegroup;
                _timeTableList = App.Database.getTimeTableData(Propertise.todayDate(), MasterChooseDetails.ClassMaster_Code, MasterChooseDetails.StreamMaster_Code, MasterChooseDetails.SectionMaster_Code).Result;
                if (_timeTableList != null)
                {
                    if (_timeTableList.Count > 0)
                    {
                        Groups = new ObservableCollection<TimeTableGroup>();
                        for (int j = 0; j < _timeTableList.Count(); j++)
                        {
                            //check week day not blank
                            if (!string.IsNullOrEmpty(_timeTableList[j].DayofWeek))
                            {
                                string shortname = string.Empty;
                                string weakName = string.Empty;
                                //here set weak day
                                weakName = getWeakName(j, ref shortname);
                                timeTablegroup = new TimeTableGroup(weakName, shortname, false);
                                #region bind_Lecture
                                //-1 for Holiday and 0 for Break
                                //check subject not blank
                                //for 1 lecture
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture1);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture1);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-1", Description = className });
                                //for 2 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture2);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture2);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-2", Description = className });
                                //for 3 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture3);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture3);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-3", Description = className });
                                //for 4 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture4);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture4);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-4", Description = className });
                                //for 5 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture5);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture5);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-5", Description = className });
                                //for 6 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture6);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture6);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-6", Description = className });
                                //for 7 lecture
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture7);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture7);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-7", Description = className });
                                //for 8 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture8);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture8);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-8", Description = className });
                                //for 9 lecture 
                                className = "";
                                className = App.Database.t_GetSubjectCode(_timeTableList[j].Lecture9);
                                if (string.IsNullOrEmpty(className))
                                {
                                    className = getClassEmptyData(_timeTableList[j].Lecture9);
                                }
                                timeTablegroup.Add(new Weak() { Name = "Lecture-9", Description = className });

                                Groups.Add(timeTablegroup);
                                #endregion
                            }
                        }
                        All = Groups;
                    }
                }
                else
                {
                    Groups = new ObservableCollection<TimeTableGroup>
                {
                 new TimeTableGroup("Sunday","S"){
                        new Weak { Name = "Not Avilable" },
                },
                new TimeTableGroup("Monday","M",true){
                        new Weak { Name = "Not Avilable"},
                    },
                new TimeTableGroup("Tuesday","T"){
                        new Weak { Name = "Not Avilable"},
                },
                new TimeTableGroup("Wednesday","W"){
                        new Weak { Name = "Not Avilable"},
                    },
                new TimeTableGroup("Thursday","Th"){
                        new Weak { Name = "Not Avilable"},
                    },
                new TimeTableGroup("Friday","F"){
                        new Weak { Name = "Not Avilable"},
                    },
                new TimeTableGroup("Saturday","Sat"){
                        new Weak { Name = "Not Avilable"},
                }
            };
                    All = Groups;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static string getClassEmptyData(int Code)
        {
            if (Code == -1)
            {
                return "Holiday";
            }
            else if (Code == 0)
            {
                return "Break";
            }
            return "Empty";
        }
        public static string getWeakName(int index,ref string shortName)
        {
            if(index==0)
            {
                shortName = "M";
                return "Monday";
            }
            else if (index == 1)
            {
                shortName = "T";
                return "Tuesday";
            }
            else if (index == 2)
            {
                shortName = "W";
                return "Wednesday";
            }
            else if (index == 3)
            {
                shortName = "Th";
                return "Thursday";
            }
            else if (index == 4)
            {
                shortName = "F";
                return "Friday";
            }
            else if(index == 5)
            {
                shortName = "Sat";
                return "Saturday";
            }
            else if (index == 6)
            {
                shortName = "Sund";
                return "Sunday";
            }
            return "None";
        }
        public static int getWeakIndex(string shortName)
        {
            if (shortName == "M")
            {
                return 0;
            }
            else if (shortName == "T")
            {
                return 1;
            }
            else if (shortName == "W")
            {
                return 2;
            }
            else if (shortName == "Th")
            {
                return 3;
            }
            else if (shortName == "F")
            {
                return 4;
            }
            else if (shortName == "Sat")
            {
                return 5;
            }
            else if (shortName == "Sund")
            {
                return 6;
            }
            return -1;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    
}
