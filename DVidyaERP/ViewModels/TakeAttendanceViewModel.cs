using System;  
using System.ComponentModel;  
using System.Windows.Input;  
using Xamarin.Forms;
using DVidyaERP;
using DVidyaERP.Global_Method_Propertise;
using DVidyaERP.Models;
using DVidyaERP.CustomControl;
using System.Globalization;
using DVidyaERP.Core.Models.Tables;
using Plugin.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SQLite.Net;
using DVidyaERP.Core.Global_Method_Propertise;
using Acr.UserDialogs;

namespace DVidyaERP.ViewModels
{
    public class TakeAttendanceViewModel : BaseViewModel
    {
        public Action CommendService;
        private static DateTime date,todayDate;
        private string totalstring;
        Propertise _propertise;
        public static  SelectableObservableCollection<ItemCell> studentlist=new SelectableObservableCollection<ItemCell>();
        public static ObservableCollection<ItemCell> studentlistshow = new ObservableCollection<ItemCell>();
        public static UserType.attendanceType AttendanceType;
        string getAttendanceMasterCode = string.Empty;

        public TakeAttendanceViewModel()
        {
            DateTime now = DateTime.Now.ToLocalTime();
            //todayDate = now.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
            todayDate = now;
            date = now;
            SubmitCommand = new Command(OnSubmit);
            if(AttendanceType==UserType.attendanceType.Take)
            {
                bindAttendanceData();
            }
            else
            {
                bindShowAttendanceData();
            }
        }

        public static async void bindAttendanceData()
        {
            var studentData = await App.Database.GetAttendanceAsync();
            studentlist.Clear();
            if (studentData.Count > 0)
            {
                foreach(var element in studentData)
                {
                    studentlist.Add(element);
                }
            }
        }
        public static async void bindShowAttendanceData()
        {
            var studentData = await App.Database.GetShowAttendanceAsync(Convert.ToDateTime(date).ToString("yyyy-MM-dd"),todayDate.ToString());
            studentlistshow.Clear();
            if (studentData.Count > 0)
            {
                foreach (var element in studentData)
                {
                    studentlistshow.Add(element);

                }
            }
        }


        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                this.OnPropertyChanged("Date");
            }
        }

        public string Total
        {
            get { return totalstring; }
            set
            {
                totalstring = value;
                this.OnPropertyChanged("Total");
            }
        }

        public ObservableCollection<ItemCell> StudentListShow
        {
            get { return studentlistshow; }
            set
            {
                studentlistshow = value;
                this.OnPropertyChanged("StudentListShow");
            }
        }

        public SelectableObservableCollection<ItemCell> StudentList
        {
            get { return studentlist; }
            set
            {
                studentlist = value;
                this.OnPropertyChanged("StudentList");
            }
        }
       
        public ICommand SubmitCommand { protected set; get; }

        public void OnSubmit()
        {
            if (!string.IsNullOrEmpty(date.ToString()))
            {
                if (AttendanceType==UserType.attendanceType.Take)
                {
                    if (checkAuthorizedofdate(todayDate.ToString(), date.ToString()) == false)
                    {
                        UserDialogs.Instance.Toast("Are you ! not take attendance " + todayDate + " is greater thant date " + date.ToString() + ".");
                        return;
                    }
                    var check = App.Database.checkClassIncharge(MasterChooseDetails.ClassMaster_Code, MasterChooseDetails.SectionMaster_Code, MasterChooseDetails.StreamMaster_Code);
                    if (check == false)
                    {
                        UserDialogs.Instance.Toast("Are you ! not applicable for this take attendance !" + date.ToString() + ".");
                        return;
                    } 
                }
                try
                {
                    string MasterCode = "";
                    var status = GetAttendanceMasterCode();
                    MasterCode = getAttendanceMasterCode;
                    int count = StudentList.Count;
                    if (count > 0)
                    {
                        //here we are bind AttendanceMasterTable
                        int code = MasterCode != "" ? Convert.ToInt32(MasterCode) : 0;
                        int FCode = CrossSettings.Current.GetValueOrDefault("FacultyId", 0);
                        //int.TryParse(CrossSettings.Current.GetValueOrDefault("FacultyId", 0),out FCode) ;

                        AttendanceMasterTable _attendanceMasterTable = new AttendanceMasterTable()
                        {
                            Id = code > 0 ?  code : App.Database.GetMaxCode("AttendanceMasterTable"),
                            ClassMaster_Code = MasterChooseDetails.ClassMaster_Code,
                            FacultyMaster_Code = FCode,
                            SectionMaster_Code = MasterChooseDetails.SectionMaster_Code,
                            StreamMaster_Code = MasterChooseDetails.StreamMaster_Code,
                            SubjectMaster_Code = MasterChooseDetails.SubjectMaster_Code,
                            AttendanceDate = Convert.ToDateTime(Date).ToString("yyyy-MM-dd"),
                        };
                        if (MasterCode == "")
                        {
                            App.Database.saveAttandanceMasterData(MasterCode, _attendanceMasterTable);
                            GetAttendanceMasterCode();
                            MasterCode = getAttendanceMasterCode;
                        }
                        else
                        {
                            App.Database.saveAttandanceMasterData(MasterCode, _attendanceMasterTable);
                        }
                        if (!string.IsNullOrEmpty(MasterCode))
                        {
                            var checkSave = App.Database.saveAttandanceTransactionData(MasterCode, StudentList);
                            if (checkSave == true)
                            {
                                UserDialogs.Instance.Toast("Save Attandance Successfull...");
                            }
                        }
                    }
                    else
                    {
                        UserDialogs.Instance.Toast("not any student exist for attendance !");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
         }
        }

        //check entry of attendace in database
        public bool checkAuthorizedofdate(string currentdate, string attendancedate)
        {
            try
            {
                //S form type is denoted show attandance
                _propertise = new Propertise();
                if (!string.IsNullOrEmpty(currentdate) && !string.IsNullOrEmpty(attendancedate))
                {
                    if (DateTime.Parse(_propertise.ConsoleDateTimeFormat(attendancedate)) <= DateTime.Parse(_propertise.ConsoleDateTimeFormat(currentdate)))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //end 
        private string GetAttendanceMasterCode()
        {
            if (AttendanceType == UserType.attendanceType.Show)
            {
                getAttendanceMasterCode =App.Database.checkStudentAttendance(MasterChooseDetails.ClassMaster_Code,MasterChooseDetails.SectionMaster_Code, MasterChooseDetails.StreamMaster_Code, Date, todayDate);
            }
            else
            {
                getAttendanceMasterCode =App.Database.checkStudentAttendance(MasterChooseDetails.ClassMaster_Code, MasterChooseDetails.SectionMaster_Code, MasterChooseDetails.StreamMaster_Code, Date, todayDate);
            }
            return getAttendanceMasterCode;
        }

    }
}

