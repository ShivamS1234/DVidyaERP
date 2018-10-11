using System;
using System.Net;
using System.Net.Http;
using System.Text;
using DVidyaERP;
using Newtonsoft.Json;
using DVidyaERP.Core.Services;
using DVidyaERP.Global_Method_Propertise;
using System.Collections.Generic;
using Xamarin.Forms;
using DVidyaERP.Core.Services.Request;
using DVidyaERP.Core.Services.Response;
using DVidyaERP.Core.Models.Tables;
using Plugin.Settings;
using System.Threading.Tasks;
using DVidyaERP.Core.Services.Entity;
using DVidyaERP.Core.Request;
using DVidyaERP.Core.Global_Method_Propertise;
using Acr.UserDialogs;

namespace DVidyaERP
{
    public class GeneralMethod
    {

        #region constructor
        public GeneralMethod()
        {
        }
        #endregion

        #region General_Propertise
        public string StrMSG { get; set; }
        //For Faculty data download
        DownloadFacultyMetadataResponse download_FacultyMetadataResponse = new DownloadFacultyMetadataResponse();
        //For Student data download
        DownloadStudentMetadataResponse download_StudentMetadaaResponse = new DownloadStudentMetadataResponse();
        //for Parent data download
        DownloadParentMetadataResponse download_ParentMetadataResponse = new DownloadParentMetadataResponse();
        //FOR Download Student all data
        StudentAttendanceResponse responseStudentAttendance = new StudentAttendanceResponse();
        #endregion

        #region General_Method
        public string ErrorMsg(string execeptionMSG)
        {
#if DEBUG
            return execeptionMSG;
#else
            return  Constant.ConstantsMSG.serverFalseMSG;
#endif
        }
        public void setConnectionDataInCollector(string ipAddress, string port)
        {
            Application.Current.Properties["IpAddress"] = ipAddress.Trim();
            Application.Current.Properties["Port"] = port.Trim();
        }

        public bool CheckConnection(out string strMSG, string IP, string Port)
        {
            bool CheckLogInAuthentication = false;
            strMSG = "";
            try
            {
                var rxcui = "0";
                var Client = new HttpClient();
                ServiceAPIInfo.httpAddress = IP;
                ServiceAPIInfo.port = Port;
                GeneralProperties.ServicesMethodName = ServiceAPIInfo.connectionMethod;

                string url = ServiceAPIInfo.serviceAPI + GeneralProperties.ServicesMethodName;
                //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                Client.BaseAddress = new Uri(url);
                Client.Timeout = TimeSpan.FromMilliseconds(ServiceAPIInfo.service_TimeOut);
                var content = new FormUrlEncodedContent(new[]
                {
                   new KeyValuePair<string, string>("", "")
                }); HttpResponseMessage responseAPI = Client.PostAsync(url, content).Result;
                if (responseAPI.IsSuccessStatusCode == true)
                {

                    ConnectionTable _connectionTable = new ConnectionTable();
                    _connectionTable.Id = 1;
                    _connectionTable.ServerName = IP;
                    _connectionTable.PortNo = Port;

                    SaveConnectionData(_connectionTable);

                    CheckLogInAuthentication = true;

                }
                else
                {
                    strMSG = "IpAddres or Port does not exist !";
                    CheckLogInAuthentication = false;
                }
                return CheckLogInAuthentication;
            }
            catch (WebException ex)
            {
                strMSG = ErrorMsg(ex.ToString());
            }
            catch (ProtocolViolationException ex)
            {
                strMSG = ErrorMsg(ex.ToString());
            }
            catch (AggregateException ex)
            {
                strMSG = ErrorMsg("Server is not Responding..");
            }
            catch (TimeoutException ex)
            {
                strMSG = ErrorMsg(ex.ToString());
            }
            return CheckLogInAuthentication;
        }
        public bool logInService(string _emailID, string _password, UserType.enumUserType _userType)
        {
            bool CheckLogInAuthentication = false;
            try
            {
                var Client = new HttpClient();
                string url = ServiceAPIInfo.serviceAPI + ServiceAPIInfo.loginMethod;
                LoginRequest loginR = new LoginRequest()
                {
                    Email = _emailID,
                    Password = _password,
                    //here + 1 because 
                    UserType = (int)_userType ,
                    DeviceID = DependencyService.Get<DependencyServices.IDependencyService>().DeviceID()
                    //DeviceID = "1234"
                };

                string Json = JsonConvert.SerializeObject(loginR, Formatting.Indented);
                var contentjson = new StringContent(Json, Encoding.UTF8, ServiceAPIInfo.ContentMediaType);
                //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                Client.BaseAddress = new Uri(url);

                HttpResponseMessage responseAPI = Client.PostAsync(url, contentjson).Result;
                if (responseAPI.IsSuccessStatusCode == true)
                {
                    var test = responseAPI.Content.ReadAsStringAsync();
                    var stringmsg = (object)test.Result;
                    var result = JsonConvert.DeserializeObject<LogInResponse>(test.Result);
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        UserType.currentUserType = _userType ;
                        ProfileViewModel.id = result.MobileUser.Code;
                        //save data in logInTable using sqlite
                        SaveLogInData(result);
                        //here is bind itemlist

                        //SaveData(responseData.data.email);
                        CheckLogInAuthentication = true;
                    }
                    else
                    {
                        CheckLogInAuthentication = false;
                    }
                    CheckLogInAuthentication = true;

                }
                else
                {
                    StrMSG = "Unauthorized User !";
                    CheckLogInAuthentication = false;
                }
                return CheckLogInAuthentication;
            }
            catch (WebException ex)
            {
                StrMSG = ErrorMsg(ex.ToString());
            }
            catch (ProtocolViolationException ex)
            {
                StrMSG = ErrorMsg(ex.ToString());
            }
            catch (AggregateException ex)
            {
                StrMSG = ErrorMsg("Server is not responding..");
                Console.WriteLine(ex.InnerException.Message);
            }
            return CheckLogInAuthentication;
        }

        #region Metadata
        //Bind Data In Server
        /// <summary>
        /// Gets the metadata service.
        /// we are se S for student and M for Metadata
        /// </summary>
        /// <returns><c>true</c>, if metadata service was gotten, <c>false</c> otherwise.</returns>
        /// <param name="MetaDataType">Meta data type.</param>
        public bool GetMetadataService(char MetaDataType)
        {
            bool check_ServiceStatus = false;
            var Client = new HttpClient();
            try
            {
                if (MetaDataType == 'M')
                {
                    if (UserType.currentUserType == UserType.enumUserType.Faculty)
                    {
                        ServiceAPIInfo.MetaDataMethod = "DownloadFacultyMetadata";
                    }
                    else if (UserType.currentUserType == UserType.enumUserType.Student)
                    {
                        ServiceAPIInfo.MetaDataMethod = "DownloadStudentMetadata";
                    }
                    else if (UserType.currentUserType == UserType.enumUserType.Parent)
                    {
                        ServiceAPIInfo.MetaDataMethod = "DownloadParentMetadata";
                    }
                    else if (UserType.currentUserType == UserType.enumUserType.Management)
                    {
                        ServiceAPIInfo.MetaDataMethod = "DownloadManagementMetadata";
                    }
                }
                else if (MetaDataType == 'S')
                {
                    ServiceAPIInfo.MetaDataMethod = "GetStudentAllAttendence";
                }
                DownloadUserMetadataRequest cngRequest = new DownloadUserMetadataRequest();
                StudentAttendanceRequest lgR = new StudentAttendanceRequest();
                GeneralProperties.ServicesMethodName = ServiceAPIInfo.MetaDataMethod;

                string url = ServiceAPIInfo.serviceAPI + GeneralProperties.ServicesMethodName;
                //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                Client.BaseAddress = new Uri(url);
                //Client.Timeout = TimeSpan.FromMilliseconds(ServiceAPIInfo.service_TimeOut);

                string json = "";
                var id = CrossSettings.Current.GetValueOrDefault("FacultyId", 0);
                var userType = CrossSettings.Current.GetValueOrDefault("UserType", 0);

                if (MetaDataType == 'M')
                {
                    cngRequest.UserID = id;
                    //cngRequest.UserID = 1;
                    cngRequest.UserType = userType;
                    /* Encode class data */
                    json = JsonConvert.SerializeObject(cngRequest, Formatting.Indented);
                }
                else if (MetaDataType == 'S')
                {
                    lgR.UserID = id;
                    lgR.UserType = userType;
                    lgR.DateString = "";
                    json = JsonConvert.SerializeObject(lgR, Formatting.Indented);
                }

                var contentjson = new StringContent(json, Encoding.UTF8, ServiceAPIInfo.ContentMediaType);
                HttpResponseMessage responseAPI = Client.PostAsync(url, contentjson).Result;
                if (responseAPI.IsSuccessStatusCode == true)
                {
                    var test = responseAPI.Content.ReadAsStringAsync();
                    var stringmsg = (object)test.Result;
                    if (MetaDataType == 'M')
                    {
                        if (UserType.currentUserType == UserType.enumUserType.Faculty)
                        {
                            download_FacultyMetadataResponse = JsonConvert.DeserializeObject<DownloadFacultyMetadataResponse>(test.Result);
                            MetaDataBind(download_FacultyMetadataResponse);
                        }
                        else if (UserType.currentUserType == UserType.enumUserType.Student)
                        {
                            download_StudentMetadaaResponse = JsonConvert.DeserializeObject<DownloadStudentMetadataResponse>(test.Result);
                            MetaDataBindForStudent(download_StudentMetadaaResponse);
                        }
                        else if (UserType.currentUserType == UserType.enumUserType.Parent)
                        {
                            download_ParentMetadataResponse = JsonConvert.DeserializeObject<DownloadParentMetadataResponse>(test.Result);
                            MetaDataBindForParent(download_ParentMetadataResponse);
                        }
                        else if (UserType.currentUserType == UserType.enumUserType.Management)
                        {
                            //here we are defind for management
                        }
                    }
                    else if (MetaDataType == 'S')
                    {
                        responseStudentAttendance = JsonConvert.DeserializeObject<StudentAttendanceResponse>(test.Result);
                        StudentDataBind(responseStudentAttendance);
                    }
                    var result = JsonConvert.DeserializeObject<LogInResponse>(test.Result);
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        check_ServiceStatus = true;
                    }
                }
                else
                {
                    StrMSG = ErrorMsg("Server is not responding..");
                    check_ServiceStatus = false;
                }
            }
            catch (WebException ex)
            {
                StrMSG = ErrorMsg(ex.ToString());
                check_ServiceStatus = false;
            }
            catch (ProtocolViolationException ex)
            {
                StrMSG = ErrorMsg(ex.ToString());
                check_ServiceStatus = false;
            }
            catch (AggregateException ex)
            {
                StrMSG = ErrorMsg("Server is not responding..");
                Console.WriteLine(ex.InnerException.Message);
                check_ServiceStatus = false;
            }
            catch (Exception ex)
            {
                StrMSG = ErrorMsg("Error on Downloading..");
                Console.WriteLine(ex.InnerException.Message);
                check_ServiceStatus = false;
            }
            return check_ServiceStatus;
        }
        #endregion

        async void SaveConnectionData(ConnectionTable connectionResponse)
        {
            try
            {
                App.Database.SaveConnectionAsync(connectionResponse);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// here we are saveing data of user
        /// </summary>
        /// <param name="logInResponse">Log in response.</param>
        async void SaveLogInData(LogInResponse logInResponse)
        {
            try
            {
                //BindMyProfile(logInResponse);
                var loginCount =App.Database.GetLogInAsync();
                //var checkdata = (LogInTable)loginPage.BindingContext;
                //logInResponse.MobileUser.ID = 1;
                CrossSettings.Current.AddOrUpdateValue("FacultyId",(int)logInResponse.MobileUser.Code);
                CrossSettings.Current.AddOrUpdateValue("UserType", (int)UserType.currentUserType);
                if (loginCount != null)
                    await  App.Database.SaveItemAsync(logInResponse.MobileUser, update: true).ConfigureAwait(false);
                else
                    await  App.Database.SaveItemAsync(logInResponse.MobileUser, update: false).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Saves the metadata.
        /// </summary>
        /// <param name="MetaDataBind"></param>
        async void MetaDataBind(DownloadFacultyMetadataResponse MetaDataResponse)
        {
            try
            {
                //saves the faculty data
                bool status = await App.Database.SaveFacultyAsync(MetaDataResponse.FacultyDetails.Faculty, MetaDataResponse.FacultyDetails.FacultyTransaction,
                                              MetaDataResponse.FacultyDetails.FacultyClass, MetaDataResponse.FacultyDetails.FacultySections,
                                              MetaDataResponse.FacultyDetails.FacultyStream, MetaDataResponse.FacultyDetails.FacultyStudentParent,
                                                                  MetaDataResponse.FacultyDetails.FacultyStudents, MetaDataResponse.FacultyDetails.FacultySubject,null,null,null);
                
                if (status == false)
                {
                    StrMSG = App.Database.MSGFailure;
                    Console.WriteLine(App.Database.MSGFailure);
                    UserDialogs.Instance.Toast(StrMSG);
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Saves the metadata.
        /// </summary>
        /// <param name="MetaDataBind"></param>
        async void MetaDataBindForStudent(DownloadStudentMetadataResponse MetaDataResponse)
        {
            try
            {
                //saves the faculty data
                bool status = await App.Database.SaveFacultyAsync(null,null ,
                                                                  MetaDataResponse.StudentDetails.StudentClass,MetaDataResponse.StudentDetails.StudentSections,
                                                                  MetaDataResponse.StudentDetails.StudentStream,MetaDataResponse.StudentDetails.StudentParent,
                                                                  null, MetaDataResponse.StudentDetails.StudentSubject,MetaDataResponse.StudentDetails.StudentFaculty,MetaDataResponse.StudentDetails.Student,null);

                if (status == false)
                {
                    StrMSG = App.Database.MSGFailure;
                    Console.WriteLine(App.Database.MSGFailure);
                    UserDialogs.Instance.Toast(StrMSG);
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Saves the metadata.
        /// </summary>
        /// <param name="MetaDataBind"></param>
        async void MetaDataBindForParent(DownloadParentMetadataResponse MetaDataResponse)
        {
            try
            {
                //saves the faculty data
                bool status = await App.Database.SaveFacultyAsync(null, null, MetaDataResponse.ParentDetails.ParentChildrenClass,
                                                                  MetaDataResponse.ParentDetails.ParentChildrenSections, MetaDataResponse.ParentDetails.ParentChildrenStream,
                                                                  null, MetaDataResponse.ParentDetails.ParentChildren, MetaDataResponse.ParentDetails.ParentChildrenSubject
                                                                  , MetaDataResponse.ParentDetails.ParentChildrenFaculty, null, MetaDataResponse.ParentDetails.Parent);

                if (status == false)
                {
                    StrMSG = App.Database.MSGFailure;
                    Console.WriteLine(App.Database.MSGFailure);
                    UserDialogs.Instance.Toast(StrMSG);
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Binds my profile.
        /// </summary>
        /// <param name="StudentDataBind">Log in response.</param>
        async void StudentDataBind(StudentAttendanceResponse studentResponse)
        {
            try
            {
                //saves the faculty data
                bool status = await App.Database.SaveStudentAsync(studentResponse.Students);
                if (status == false)
                {
                    StrMSG = App.Database.MSGFailure;
                    Console.WriteLine(App.Database.MSGFailure);
                    UserDialogs.Instance.Toast(StrMSG);
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Auto_Run_ServiceMethod
        public static bool syncAttendanceOnServer(int userId,int userType)
        {
            Propertise gbl = new Propertise();
            string ServerPortNo = string.Empty;
            var Client = new HttpClient();
            int _UserID=userId;
            int usertype=userType;
            try
            {
                //var login = App.Database.GetLogInAsync();
                //if (login.Result.Count > 0)
                //{
                //    _UserID = login.Result[0].Code;
                //    usertype = 2;
                //}

                if (_UserID == 0 || usertype==0)
                {
                    return false;
                }
                //call student bind class and set data
                var classAttendace = App.Database.bindStudentsData().Result;
                if(classAttendace == null || classAttendace.Count  == 0)
                {
                    return false;
                }
                ClassAttendanceRequest classAttendanceRequest = new ClassAttendanceRequest()
                {
                    UserID = _UserID,
                    UserType = usertype,
                    ClassAttendance=classAttendace,
                };


                GeneralProperties.ServicesMethodName = ServiceAPIInfo.SyncClassAttendenceMethod;
                string url = ServiceAPIInfo.serviceAPI + GeneralProperties.ServicesMethodName;
                Client.BaseAddress = new Uri(url);
                Client.Timeout = TimeSpan.FromMilliseconds(ServiceAPIInfo.service_TimeOut);

                var json = JsonConvert.SerializeObject(classAttendanceRequest, Formatting.Indented);
                var contentjson = new StringContent(json, Encoding.UTF8, ServiceAPIInfo.ContentMediaType);
                HttpResponseMessage responseAPI = Client.PostAsync(url, contentjson).Result;
                if (responseAPI.IsSuccessStatusCode == true)
                {
                    var test = responseAPI.Content.ReadAsStringAsync();
                    var stringmsg = (object)test.Result;
                    var download_Response = JsonConvert.DeserializeObject<ClassAttendanceResponse>(test.Result);
                    if (download_Response.SyncAttendanceStatus)
                    {
                        App.Database.UpdateStudentStatus();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Attendance have not found !");
                        return false;
                    }
                }
                else
                {
                    Console.Write(responseAPI.RequestMessage);
                }

            }
            catch (WebException Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public static bool syncStudent(int usertype = 0)
        {
            Propertise gbl = new Propertise();
            string ServerPortNo = string.Empty;
            var Client = new HttpClient();
            int _UserID = 0;
            try
            {
                var login = App.Database.GetLogInAsync();
                if (login != null)
                {
                    _UserID = login.Result.Code;
                }
                if (_UserID == 0 || usertype == 0)
                {
                    return false;
                }
                //call student bind class and set data
                StudentAttendanceRequest studentRequest = new StudentAttendanceRequest()
                {
                    UserID = _UserID,
                    UserType = usertype,
                    DateString = gbl.ConsoleDateTimeFormat(Propertise.todayDate())
                };
                StudentAcknowledgementRequest studentAcknowledgementRequest = new StudentAcknowledgementRequest();

                GeneralProperties.ServicesMethodName = ServiceAPIInfo.download_StudentMetadata;
                string url = ServiceAPIInfo.serviceAPI + GeneralProperties.ServicesMethodName;
                Client.BaseAddress = new Uri(url);
                Client.Timeout = TimeSpan.FromMilliseconds(ServiceAPIInfo.service_TimeOut);

                var json = JsonConvert.SerializeObject(studentRequest, Formatting.Indented);
                var contentjson = new StringContent(json, Encoding.UTF8, ServiceAPIInfo.ContentMediaType);
                HttpResponseMessage responseAPI = Client.PostAsync(url, contentjson).Result;
                if (responseAPI.IsSuccessStatusCode == true)
                {
                    var test = responseAPI.Content.ReadAsStringAsync();
                    var stringmsg = (object)test.Result;
                    var download_Response = JsonConvert.DeserializeObject<StudentAttendanceResponse>(test.Result);
                    if (string.IsNullOrEmpty(download_Response.ToString()) || download_Response.Students.StudentAttendance.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        int status = App.Database.syncAttandanceData(download_Response).Result;
                        if (status > 0)
                        {
                            //after save data send student data in server
                            studentAcknowledgementRequest.UserID = _UserID;
                            studentAcknowledgementRequest.UserType = usertype;
                            IList<EAttendanceTransaction> transactions = new List<EAttendanceTransaction>();
                            if (download_Response.Students.StudentAttendance.Count > 0)
                            {
                                for (int i = 0; i < download_Response.Students.StudentAttendance.Count; i++)
                                {
                                    int transactiontableCount = download_Response.Students.StudentAttendance[i].AttendanceTransaction.Count;
                                    for (int j = 0; j < transactiontableCount; j++)
                                    {
                                        EAttendanceTransaction transaction = new EAttendanceTransaction();
                                        transaction.Code = download_Response.Students.StudentAttendance[i].AttendanceTransaction[j].Code;
                                        transaction.AttendanceMaster_Code = download_Response.Students.StudentAttendance[i].AttendanceTransaction[j].AttendanceMaster_Code;
                                        transaction.FacultyMaster_Code = download_Response.Students.StudentAttendance[i].AttendanceTransaction[j].FacultyMaster_Code;
                                        transaction.StudentMaster_Code = download_Response.Students.StudentAttendance[i].AttendanceTransaction[j].StudentMaster_Code;
                                        transaction.StudentName = download_Response.Students.StudentAttendance[i].AttendanceTransaction[j].StudentName;
                                        transaction.Status = download_Response.Students.StudentAttendance[i].AttendanceTransaction[j].Status;
                                        transactions.Add(transaction);
                                    }
                                }
                                studentAcknowledgementRequest.Transactions = transactions;
                            }

                            //here we again call for acknowledgement
                            GeneralProperties.ServicesMethodName = ServiceAPIInfo.request_Studentid;
                            url = ServiceAPIInfo.serviceAPI + GeneralProperties.ServicesMethodName;
                            Client.BaseAddress = new Uri(url);
                            Client.Timeout = TimeSpan.FromMilliseconds(ServiceAPIInfo.service_TimeOut);

                            json = JsonConvert.SerializeObject(studentAcknowledgementRequest, Formatting.Indented);
                            contentjson = new StringContent(json, Encoding.UTF8, ServiceAPIInfo.ContentMediaType);
                            responseAPI = Client.PostAsync(url, contentjson).Result;
                            if (responseAPI.IsSuccessStatusCode == true)
                            {
                                test = responseAPI.Content.ReadAsStringAsync();
                                stringmsg = (object)test.Result;
                                if (!string.IsNullOrEmpty(stringmsg.ToString()))
                                {
                                    Console.WriteLine("Successfull download student data !");
                                    return true;
                                }
                                else
                                {
                                    Console.WriteLine("Fail on the Acknowledge request !");
                                    return false;
                                }
                            }   
                        }
                    }
                }
                else
                {
                    Console.Write(responseAPI.RequestMessage);
                }

            }
            catch (WebException Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// method declare for TimeTable sync
        /// </summary>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public static async Task syncTimeTable(int _UserID,UserType.enumUserType _userType,bool _FromStudentData=true)
        {
            Propertise gbl = new Propertise();
            string ServerPortNo = string.Empty;
            var Client = new HttpClient();
            try
            {
                GeneralProperties.ServicesMethodName = ServiceAPIInfo.SyncTimeTable;

                string url = ServiceAPIInfo.serviceAPI + GeneralProperties.ServicesMethodName;
                Client.BaseAddress = new Uri(url);
                Client.Timeout = TimeSpan.FromMilliseconds(ServiceAPIInfo.service_TimeOut);

                //call student bind class and set data
                TimeTableRequest timetableRequest = new TimeTableRequest()
                {
                    UserID = _UserID,
                    UserType = (int)_userType ,
                    DateString = gbl.ConsoleDateTimeFormat(Propertise.todayDate()),
                    CheckStatus = _FromStudentData              
                };

                TimeTableResponse download_TimeTableResponse = new TimeTableResponse();

                var json = JsonConvert.SerializeObject(timetableRequest, Formatting.Indented);
                var contentjson = new StringContent(json, Encoding.UTF8, ServiceAPIInfo.ContentMediaType);
                HttpResponseMessage responseAPI = Client.PostAsync(url, contentjson).Result;
                if (responseAPI.IsSuccessStatusCode == true)
                {
                    var test = responseAPI.Content.ReadAsStringAsync();
                        var stringmsg = (object)test.Result;
                    download_TimeTableResponse = JsonConvert.DeserializeObject<TimeTableResponse>(test.Result);
                        if (download_TimeTableResponse.TimeTable.Count == 0)
                        {
                            return;
                        }
                    int codeMasterTimeTable =await App.Database.save_DownloadTimeTableResponse(download_TimeTableResponse);
                    if (codeMasterTimeTable > 0)
                    {
                        TimeTableAcknowledgementRequest timeTableAcknowledgementRequest = new TimeTableAcknowledgementRequest()
                        {
                            UserID = _UserID,
                            UserType = (int)_userType ,
                            TimeTableMasterCode = codeMasterTimeTable
                        };
                        GeneralProperties.ServicesMethodName = ServiceAPIInfo.request_TimeTableAcknowledgementStatus;

                        url = ServiceAPIInfo.serviceAPI + GeneralProperties.ServicesMethodName;
                        Client.BaseAddress = new Uri(url);
                        Client.Timeout = TimeSpan.FromMilliseconds(ServiceAPIInfo.service_TimeOut);
                        json = JsonConvert.SerializeObject(timeTableAcknowledgementRequest, Formatting.Indented);
                        contentjson = new StringContent(json, Encoding.UTF8, ServiceAPIInfo.ContentMediaType);
                        responseAPI = Client.PostAsync(url, contentjson).Result;
                        if (responseAPI.IsSuccessStatusCode == true)
                        {
                            test = responseAPI.Content.ReadAsStringAsync();
                            stringmsg = (object)test.Result;
                        }
                        else
                        {
                            Console.WriteLine("Time Table Acknowledgement status {0}",responseAPI.StatusCode);
                        }
                    }
              }
                else
                {
                    Console.Write(responseAPI.RequestMessage);
                }
            }
            catch (WebException Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static bool EditProfile(EditUserRequest request)
        {
            bool CheckLogInAuthentication = false;
            try
            {
                var Client = new HttpClient();
                string url = ServiceAPIInfo.serviceAPI + ServiceAPIInfo.EditUser;

                string Json = JsonConvert.SerializeObject(request, Formatting.Indented);
                var contentjson = new StringContent(Json, Encoding.UTF8, ServiceAPIInfo.ContentMediaType);
                //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                Client.BaseAddress = new Uri(url);

                HttpResponseMessage responseAPI = Client.PostAsync(url, contentjson).Result;
                if (responseAPI.IsSuccessStatusCode == true)
                {
                    var test = responseAPI.Content.ReadAsStringAsync();
                    var stringmsg = (object)test.Result;
                    var result = JsonConvert.DeserializeObject<ProfileResponse>(test.Result);
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        //SaveData(responseData.data.email);
                        CheckLogInAuthentication = true;
                    }
                    else
                    {
                        CheckLogInAuthentication = false;
                    }
                    CheckLogInAuthentication = true;

                }
                else
                {
                    Console.WriteLine("Unauthorized User !");
                    CheckLogInAuthentication = false;
                }
                return CheckLogInAuthentication;
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (ProtocolViolationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Server is not responding..");
                Console.WriteLine(ex.InnerException.Message);
            }
            return CheckLogInAuthentication;
        }
        #endregion
    }

}
