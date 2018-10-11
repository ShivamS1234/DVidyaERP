using System;
using DVidyaERP.DependencyServices;
using DVidyaERP.Droid.DependencyService_Android;
using DVidyaERP.Global_Method_Propertise;
using DVidyaERP.Core.Services;
using Plugin.Settings;
using static DVidyaERP.Global_Method_Propertise.UserType;

[assembly: Xamarin.Forms.Dependency(typeof(DependencyServiceForAutoRun))]
namespace DVidyaERP.DependencyServices
{
    public  class DependencyServiceForAutoRun : IDependencyServiceForAutoRun
    {
        #region method_AttendanceData
        public  void SyncAttendanceData(int userID, int UserType)
        {
            string servicestring = string.Empty;
                ///1. faculty
                ///shrink student attendance on server 
                //check server ip and port
                if (!string.IsNullOrEmpty(ServiceAPIInfo.serviceAPI))
                {
                    servicestring = ServiceAPIInfo.serviceAPI;
                    ///check entry of attendance in local memory
                    ///1. for faculty  
                if (enumUserType.Faculty == (enumUserType)UserType)
                    {
                    //App.Database.UpdateStudentStatus();
                        bool status = App.Database.getStudentStatus().Result;
                        if (status)
                        {
                        bool syncAttendanceStatus = GeneralMethod.syncAttendanceOnServer(userID,(int)UserType);
                            if (syncAttendanceStatus == true)
                            {
                                //sQ.UpdateStudentStatus();
                            }
                        }
                    }
                    //2. for student
                else if ((enumUserType)UserType == enumUserType.Student || (enumUserType)UserType == enumUserType.Parent)
                    {
                        if (GeneralMethod.syncStudent((int)currentUserType) == true)
                        {
                            //set status of studet attedance get
                        }
                    }
                //

            }
        }
        #endregion

        #region TimeTable
        public  void SyncTimeTableData(int userID, int UserType)
        {
            string servicestring = string.Empty; ///1. faculty
                ///shrink student attendance on server 
                //check server ip and port
                if (!string.IsNullOrEmpty(ServiceAPIInfo.serviceAPI))
                {
                  servicestring = ServiceAPIInfo.serviceAPI;

                GeneralMethod.syncTimeTable(userID,(enumUserType)UserType).ConfigureAwait(false);
                }
                //
        }
        #endregion

    }
}

