using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVidyaERP.Core.Services
{
    public class ServiceAPIInfo
    {
        public static string httpAddress { get; set; }
        public static string port  { get; set; }
        public static string serviceExtraString { get; set; }
        public static string serviceAPI 
        { 
            get { return @"http://"+ httpAddress + ":" + port + "/" + serviceExtraString;  }
        }

        public static string connectionMethod = "Test";

        public static string loginMethod = "LogIn";
        public static int service_TimeOut = 3000;


        public static string SyncClassAttendenceMethod = "SyncClassAttendence";
        public static string download_FacultydataMethod = "DownloadFacultyMetadata";
        public static string MetaDataMethod = "";

        public static string download_ManagementMetadata_Method = "DownloadManagementMetadata";

        public static string download_ParentMetadata_Method = "DownloadParentMetadata";

        public static string download_StudentMetadata = "GetClassAttendence";
        public static string request_Studentid = "SetAcknowledgementStatus";
        public static string request_TimeTableAcknowledgementStatus = "SetTimeTableAcknowledgementStatus";
        public static string SyncTimeTable = "GetTimeTable";
        public static string EditUser = "EditUser";
        //get student attendance statuse
        public static string download_getStudentStatus = "";
        public static string ContentMediaType = "application/json";

        //public static string ContentMediaType = "";
    }
}
