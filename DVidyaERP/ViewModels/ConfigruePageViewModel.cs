using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using DVidyaERP.Global_Method_Propertise;
using Plugin.Settings;
using Xamarin.Forms;
using static DVidyaERP.Global_Method_Propertise.UserType;

namespace DVidyaERP.ViewModels
{
    public class ConfigruePageViewModel : BaseViewModel
    {
        public Action CommandCheckInterNetConnection;
        GeneralMethod generalMethod = new GeneralMethod();
        public static bool StatusStudent;
        public static bool StatusMetaData;
        public ConfigruePageViewModel()
        {
            MetaDataCommand = new Command(OnMetadata);
            StudentDataCommand = new Command(OnStudentData);
        }

       
        public ICommand MetaDataCommand { protected set; get; }
        public ICommand StudentDataCommand { protected set; get; }

        public void OnMetadata()
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                OnMetadataClick();
                Constants.Offline = false;
            }
            else if (Constants.Offline)
            {
                UserDialogs.Instance.Toast("This time !You are Offline so you can't download data !");

            }
            else
            {
                CommandCheckInterNetConnection();
                Constants.Offline = true;

            }
        }

        public void OnStudentData()
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                OnStudentdataClick();
                Constants.Offline = false;
            }
            else if (Constants.Offline)
            {
                UserDialogs.Instance.Toast("This time !You are Offline so you can't download data !");
            }
            else
            {
                CommandCheckInterNetConnection();
                Constants.Offline = true;

            }
        }

        public async void OnMetadataClick()
        {
            try
            {
                //Static_method.ShowLoadingDialog(false);
               UserDialogs.Instance.ShowLoading("Downloading.....");
                await Task.Delay(100);
                var status=generalMethod.GetMetadataService('M');
                UserDialogs.Instance.HideLoading();

                if (status==true)
                {
                    UserDialogs.Instance.Toast("Successful Download Meta Data !");
                    CrossSettings.Current.AddOrUpdateValue("StatusMetaData", true);
                    StatusMetaData = true;  
                }
                else
                {
                    UserDialogs.Instance.Toast("Fail Download Meta Data !");
                    CrossSettings.Current.AddOrUpdateValue("StatusMetaData", false);
                    StatusMetaData = false;  
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                UserDialogs.Instance.Toast(ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
           
        }

        public async void OnStudentdataClick()
        {
            try
            {
                var id = CrossSettings.Current.GetValueOrDefault("FacultyId", 0);
                var userType = CrossSettings.Current.GetValueOrDefault("UserType", 0);
                UserDialogs.Instance.ShowLoading("Downloading.....");
                await Task.Delay(100);
                var status= generalMethod.GetMetadataService('S');
                UserDialogs.Instance.HideLoading();
                if (status==true)
                {
                    GeneralMethod.syncTimeTable(id, (enumUserType)userType, false).ConfigureAwait(false);
                    UserDialogs.Instance.Toast("Successful Download Student Data !");
                    CrossSettings.Current.AddOrUpdateValue("StatusStudentData", true);
                    StatusStudent = true;  
                }
                else
                {
                    UserDialogs.Instance.Toast("Fail Download Student Data !");
                    CrossSettings.Current.AddOrUpdateValue("StatusStudentData", false);
                    StatusStudent = false;  
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Console.WriteLine(ex.Message);
                UserDialogs.Instance.Toast(ex.Message);

            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
