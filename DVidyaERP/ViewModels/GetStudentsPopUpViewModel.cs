using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using DVidyaERP.Global_Method_Propertise;
using DVidyaERP.Models;
using Xamarin.Forms;
namespace DVidyaERP.ViewModels
{
    public class GetStudentsPopUpViewModel : BaseViewModel
    {
        public Action CommendService,DisplayInvalidClass, DisplayInvalidStream,DisplayInvalidSection,CommendClose;
         PickerItem classname;
         PickerItem streamname;
         PickerItem sectionname;
        private ObservableCollection<PickerItem> classnamelist=new ObservableCollection<PickerItem>();
        private ObservableCollection<PickerItem> streamnamelist = new ObservableCollection<PickerItem>();
        private ObservableCollection<PickerItem> sectionnamelist = new ObservableCollection<PickerItem>();
        INavigation navigation;

        public GetStudentsPopUpViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            loadData();
        }
        public GetStudentsPopUpViewModel(INavigation _navigation)
        {
            this.navigation = _navigation;
            SubmitCommand = new Command(OnSubmit);
            loadData();
        }
        private async void loadData()
        {
            try
            {
                var classnamel = await App.Database.GetClassAsync();
                if (!string.IsNullOrEmpty(classnamel.ToString()))
                {
                    if (classnamel.Count > 0)
                    {
                        foreach (var element in classnamel)
                        {
                            PickerItem pickerItem = new PickerItem()
                            {
                                Id= element.Code,
                                Name= element.ClassName
                            };
                            classnamelist.Add(pickerItem);
                        }
                        //ClassName = classnamelist.FirstOrDefault();
                    }
                }
                var streamnamel = await App.Database.GetStreamAsync();
                if (!string.IsNullOrEmpty(streamnamel.ToString()))
                {
                    if (streamnamel.Count > 0)
                    {
                        foreach (var element in streamnamel)
                        {
                            PickerItem pickerItem = new PickerItem()
                            {
                                Id = element.Code,
                                Name = element.StreamName
                            };
                            streamnamelist.Add(pickerItem);
                        }
                        //StreamName = streamnamelist.FirstOrDefault();
                    }
                }
                var sectionnamel = await App.Database.GetSectionAsync();
                if (!string.IsNullOrEmpty(sectionnamel.ToString()))
                {
                    if (sectionnamel.Count > 0)
                    {
                        foreach (var element in sectionnamel)
                        {
                            PickerItem pickerItem = new PickerItem()
                            {
                                Id = element.Code,
                                Name = element.SectionName
                            };
                            sectionnamelist.Add(pickerItem);
                        }
                        //SectionName = sectionnamelist.FirstOrDefault();
                    }
                }
                LoadData();
            }
            catch(Exception ex)
            {

            }
        }
       
        public PickerItem ClassName
        {
            get { return classname; }
            set
            {
                classname = value;
                this.OnPropertyChanged("ClassName");
            }
        }  
        public ObservableCollection<PickerItem> ClassNameList
        {
            set
            {
                if (classnamelist != value)
                {
                    classnamelist = value;
                    OnPropertyChanged("ClassNameList");
                }
            }
            get
            {
                return classnamelist;
            }
        }


        public PickerItem StreamName
        {
            get { return streamname; }
            set
            {
                streamname = value;
                this.OnPropertyChanged("StreamName");
            }
        }
        public ObservableCollection<PickerItem> StreamNameList
        {
            set
            {
                if (streamnamelist != value)
                {
                    streamnamelist = value;
                    OnPropertyChanged("StreamNameList");
                }
            }
            get
            {
                return streamnamelist;
            }
        }

        public PickerItem SectionName
        {
            get { return sectionname; }
            set
            {
                sectionname = value;
                this.OnPropertyChanged("SectionName");
            }
        }
        public ObservableCollection<PickerItem> SectionNameList
        {
            set
            {
                if (sectionnamelist != value)
                {
                    sectionnamelist = value;
                    OnPropertyChanged("SectionNameList");
                }
            }
            get
            {
                return sectionnamelist;
            }
        }


        public ICommand SubmitCommand { protected set; get; }

        public  void OnSubmit()
        {
            if (ClassName == null)
                {
                    DisplayInvalidClass();
                }
            else if (StreamName == null)
                {
                    DisplayInvalidStream();
                }
            else if (SectionName == null)
                {
                    DisplayInvalidSection();
                }
                else
                {
                try
                {
                    MasterChooseDetails.ClassMaster_Code = ClassName.Id;
                    MasterChooseDetails.SectionMaster_Code = SectionName.Id;
                    MasterChooseDetails.StreamMaster_Code = StreamName.Id;

                    GeneralMethod glMethod = new GeneralMethod();

                    if(UserType.currentAttendanceType != UserType.attendanceType.TimeTable)
                    {
                        TakeAttendanceViewModel.bindAttendanceData();
                    }
                    else
                    {
                        TimeTableGroup.LoadTimeTable();

                    }
                    UserDialogs.Instance.HideLoading();
                    CommendClose();
                }
                catch (Exception ex)
                {

                }
                }
        }
        public static PickerItem
GetSelectedItemFromCollection(ObservableCollection<PickerItem> collection, int FindById)
        {
            PickerItem filteredItems =
            collection.FirstOrDefault(x => x.Id.Equals(FindById));
            return filteredItems;
        }
        private void LoadData()
        {
            if (MasterChooseDetails.ClassMaster_Code > 0)
            {
                ClassName = GetSelectedItemFromCollection(ClassNameList, MasterChooseDetails.ClassMaster_Code);
            }
            if (MasterChooseDetails.SectionMaster_Code > 0)
            {
                SectionName = GetSelectedItemFromCollection(SectionNameList, MasterChooseDetails.SectionMaster_Code);
            }
            if (MasterChooseDetails.StreamMaster_Code > 0)
            {
                StreamName = GetSelectedItemFromCollection(StreamNameList, MasterChooseDetails.StreamMaster_Code);
            }
        }
    }

    public class PickerItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

   
}
