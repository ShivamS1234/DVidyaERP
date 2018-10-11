using System;
using DVidyaERP.Views;
using Xamarin.Forms;
using DVidyaERP.Core.Services;
using DVidyaERP.DependencyServices;
using DVidyaERP.Models;
using DVidyaERP.Global_Method_Propertise;
using Plugin.Settings;
using DVidyaERP.ViewModels;

namespace DVidyaERP
{
    public partial class App : Application
    {
        static DVidyaDataBase database;
        public static string BackendUrl = "https://localhost:52025";
        static Color brandColor = Color.FromHex("#EA3175");
        static string _fontfamilyContent = string.Empty;
        static string _fontfamilyHead = string.Empty;
        GeneralMethod _generalMethod = new GeneralMethod();

        public App()
        {
            //InitializeComponent();
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    _fontfamilyHead = "Rockwell-Light";
                    _fontfamilyContent = "Maison Neue";
                    break;
                default:
                    _fontfamilyHead = "RCKWLL.ttf#RCKWLL";
                    _fontfamilyContent = "MAISONNEUE-BOOK.TTF#MAISONNEUE-BOOK";
                    break;
            }
            //bind connection data
            getConnectionData();

            //ServiceAPIInfo.httpAddress = "192.168.0.103";
            //ServiceAPIInfo.port = "52025";
            ServiceAPIInfo.serviceExtraString = "/DVidya/Data/DataServiceForMobile/";
            //GeneralMethod.syncAttendanceOnServer();

            //MasterChooseDetails.ClassMaster_Code = 4;
            //MasterChooseDetails.SectionMaster_Code = 1;
            //MasterChooseDetails.StreamMaster_Code = 1;
            UserType.currentAttendanceType = UserType.attendanceType.Take;
            //set 

            ConfigruePageViewModel.StatusStudent = CrossSettings.Current.GetValueOrDefault("StatusStudentData", false);
            ConfigruePageViewModel.StatusMetaData = CrossSettings.Current.GetValueOrDefault("StatusMetaData", false);
            //end
            Page _page;
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                if (Device.RuntimePlatform == Device.iOS)
                    _page = new connectionPage();
                else
                    _page = new NavigationPage(new connectionPage());
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                    _page = new LogInPage();
                else
                    _page = new NavigationPage(new LogInPage());
            }

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = _page;
            else
                MainPage = _page;
            
        }

        async  void getConnectionData()
        {
            try
            {
                var v = Database.GetConnectionData();

                if (v.Result.Count >0 )
                {
                    _generalMethod.setConnectionDataInCollector(v.Result[0].ServerName, v.Result[0].PortNo);
                }
                else
                {
                    _generalMethod.setConnectionDataInCollector("", "");
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static Color BrandColor
        {
            get
            {
                return brandColor;
            }
        }
        public static string fontFamilyContent
        {
            get
            {
                return _fontfamilyContent;
            }
        }
        public static string fontFamilyHead
        {
            get
            {
                return _fontfamilyHead;
            }
        }

        public static DVidyaDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new DVidyaDataBase(DependencyService.Get<IDependencyService>().getLocalPath("DVidyaERPDB.db3"));
                }
                return database;
            }
        }
    }
}
