using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using System;
using DVidyaERP.Models;
using DVidyaERP.Core.Models.Tables;
using System.Collections;
using DVidyaERP.Core.Services.Entity;
using Xamarin.Forms;
using System.Drawing;
using System.IO;
using DVidyaERP.Helpers;
using System.Linq;
using DVidyaERP.Global_Method_Propertise;
using DVidyaERP.CustomControl;
using Plugin.Settings;
using DVidyaERP.Core.Services.Response;
using System.Text;
using DVidyaERP.Core.Global_Method_Propertise;

namespace DVidyaERP
{
    public class DVidyaDataBase
    {
        readonly SQLiteAsyncConnection database;
        readonly SQLiteConnection NonAsyncdatabaseConnection;
        Propertise _propertise = new Propertise();
        public bool checkTableInDatabase = false;
        public string MSGFailure { get; set; }
        public DVidyaDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            NonAsyncdatabaseConnection = new SQLiteConnection(dbPath);
            //here is write code for table creation
            try
            {
                CheckTableInDatabase();
                //checkTableInDatabase = false;
                if (checkTableInDatabase == false)
                {
                    DropTable();
                    CreateTable();
                }
                else
                {
                    //DropTable();
                    CreateTable();
                    //database.ExecuteAsync("delete from [AttendanceMasterTable]");
                    //database.QueryAsync<AttendanceTransactionTable>("Delete from [AttendanceTransactionTable] ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        /// <summary>
        /// Drops the table.
        /// </summary>
        void DropTable()
        {
            database.DropTableAsync<LogInTable>().Wait();
            database.DropTableAsync<ConnectionTable>().Wait();
            database.DropTableAsync<FacultyMasterTable>().Wait();
            database.DropTableAsync<AttendanceMasterTable>().Wait();
            database.DropTableAsync<AttendanceTransactionTable>().Wait();
            database.DropTableAsync<ClassMasterTable>().Wait();
            database.DropTableAsync<ClassTransactionTable>().Wait();
            database.DropTableAsync<FacultyMasterTable>().Wait();
            database.DropTableAsync<FacultyTransactionTable>().Wait();
            database.DropTableAsync<ParentMasterTable>().Wait();
            database.DropTableAsync<SectionMasterTable>().Wait();
            database.DropTableAsync<StreamMasterTable>().Wait();
            database.DropTableAsync<StudentMasterTable>().Wait();
            database.DropTableAsync<SubjectMasterTable>().Wait();
            database.DropTableAsync<TimeTableMaster>().Wait();
            database.DropTableAsync<TimeTableTransaction>().Wait();
            NonAsyncdatabaseConnection.DropTable<LogInTable>();
            NonAsyncdatabaseConnection.DropTable<ConnectionTable>();
        }
        /// <summary>
        /// Creates the table.
        /// </summary>
        void CreateTable()
        {
            database.CreateTablesAsync<LogInTable, ConnectionTable>().Wait();
            database.CreateTablesAsync<FacultyMasterTable,AttendanceMasterTable>().Wait(); 
            database.CreateTablesAsync<AttendanceTransactionTable,ClassMasterTable>().Wait();
            database.CreateTablesAsync<ClassTransactionTable,FacultyMasterTable>().Wait();
            database.CreateTablesAsync<FacultyTransactionTable,ParentMasterTable>().Wait();
            database.CreateTablesAsync<SectionMasterTable,StreamMasterTable>().Wait();
            database.CreateTablesAsync<StudentMasterTable,SubjectMasterTable>().Wait();
            database.CreateTablesAsync<TimeTableMaster, TimeTableTransaction>().Wait();
            NonAsyncdatabaseConnection.CreateTable<LogInTable>();
            NonAsyncdatabaseConnection.CreateTable<ConnectionTable>();

        }

        async void CheckTableInDatabase()
        {
            checkTableInDatabase = false;
            try
            {
                if (await tableExistsAsync(Constants.LogInTableName))
                {
                    checkTableInDatabase = true;
                }
            }
            catch (Exception ex)
            {

            }

        }

        public async Task<bool> tableExistsAsync(string tableName)
        {
            //return database.Table<LogInTable>().Where(i => i.ID == email).FirstOrDefaultAsync();
            //ExecuteAsync("SELECT COUNT(*) FROM sqlite_master WHERE type ='table'  AND name ='" + tableName + "';");
            try
            {

                var login = NonAsyncdatabaseConnection.GetTableInfo(Constants.LogInTableName);
                var connect= NonAsyncdatabaseConnection.GetTableInfo(Constants.ConnectTableName);
                var facultymaster = NonAsyncdatabaseConnection.GetTableInfo(nameof(FacultyMasterTable));
                var facultyt = NonAsyncdatabaseConnection.GetTableInfo(nameof(FacultyTransactionTable));
                var attendancemaster = NonAsyncdatabaseConnection.GetTableInfo(nameof(AttendanceMasterTable));
                var attendancet = NonAsyncdatabaseConnection.GetTableInfo(nameof(AttendanceTransactionTable));
                var classMaster = NonAsyncdatabaseConnection.GetTableInfo(nameof(ClassMasterTable));
                var classT = NonAsyncdatabaseConnection.GetTableInfo(nameof(ClassTransactionTable));
                var parentmaster = NonAsyncdatabaseConnection.GetTableInfo(nameof(ParentMasterTable));
                var sectionmastertable = NonAsyncdatabaseConnection.GetTableInfo(nameof(SectionMasterTable));
                var streammastertable = NonAsyncdatabaseConnection.GetTableInfo(nameof(StreamMasterTable));
                var studentmaster = NonAsyncdatabaseConnection.GetTableInfo(nameof(StudentMasterTable));
                var subjectmaster = NonAsyncdatabaseConnection.GetTableInfo(nameof(SubjectMasterTable));

                if (login.Count == 0)
                { return false; }
                else if (connect.Count == 0)
                { return false; }
                else if (facultymaster.Count == 0 || facultyt.Count==0 || attendancemaster.Count ==0 || attendancet.Count==0 || classMaster.Count==0 || classT.Count==0 || parentmaster.Count == 0 || sectionmastertable.Count == 0 || streammastertable.Count ==0 || studentmaster.Count == 0 || subjectmaster.Count == 0)
                {
                    return false;  
                }
                else
                { return true; }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public  Task<LogInTable>  GetLogInAsync()
        {
            return  database.Table<LogInTable>().FirstOrDefaultAsync();
        }

        //public async  Task<List<LogInTable>>  GetLogInAsync()
        //{
        //    return await database.Table<LogInTable>().ToListAsync();
        //}

        public async Task<List<ItemCell>> GetAttendanceAsync()
        {
            IList<ItemCell> listattendanceModel = new List<ItemCell>();
            try
            {
            var studentTable=await database.QueryAsync<StudentMasterTable>("SELECT * FROM [StudentMasterTable] where ClassMaster_Code="+ MasterChooseDetails.ClassMaster_Code +" and StreamMaster_Code= " + MasterChooseDetails.StreamMaster_Code +  " and SectionMaster_Code=" + MasterChooseDetails.SectionMaster_Code + " ");
                //var studentTable = await database.Table<StudentMasterTable>().ToListAsync();
                if (studentTable.Count > 0)
                {
                    for (int i = 0; i < studentTable.Count; i++)
                    {
                        ItemCell _attendanceModel = new ItemCell();
                        _attendanceModel.code = studentTable[i].Code;
                        _attendanceModel.id = studentTable[i].Code.ToString();
                        _attendanceModel.name = studentTable[i].StudentName;
                        var imageName = studentTable[i].Code.ToString() + "_" + studentTable[i].StudentName;
                        var imagepath = DependencyService.Get<IPicture>().GetPictureFromDisk(imageName);
                        _attendanceModel.ImageUri = imagepath;
                        listattendanceModel.Add(_attendanceModel);
                    }
                }
            }
            catch(Exception ex)
            {
                 
            }
            return listattendanceModel.ToList();
        }
        class MergeObj
        {
            public AttendanceMasterTable Obj1 { get; set; }
            public AttendanceTransactionTable Obj2 { get; set; }
        }
        class MergeTimeTableObj
        {
            public TimeTableMaster Obj1 { get; set; }
            public TimeTableTransaction Obj2 { get; set; }
        }
#region Show_Attendance
        public async Task<List<ItemCell>> GetShowAttendanceAsync(string formDate,string toDate)
        {
            IList<ItemCell> listattendanceModel = new List<ItemCell>();
            try
            {
                List<AttendanceMasterTable> t1 = NonAsyncdatabaseConnection.Table<AttendanceMasterTable>().ToList();
                List<AttendanceTransactionTable> t2 = NonAsyncdatabaseConnection.Table<AttendanceTransactionTable>().ToList();
                var studentTable = t1.Join(t2, outer => outer.Id,
                           inner => inner.AttendanceMaster_Code,
                           (outer, inner) => new MergeObj { Obj1 = outer, Obj2 = inner });
                studentTable.ToList();
                var studentTableData = studentTable.Where(a => a.Obj1.ClassMaster_Code == MasterChooseDetails.ClassMaster_Code && a.Obj1.SectionMaster_Code == MasterChooseDetails.SectionMaster_Code
                                                          && a.Obj1.StreamMaster_Code == MasterChooseDetails.StreamMaster_Code && a.Obj1.AttendanceDate == formDate).ToList();
                
                //var studentTable = await database.Table<StudentMasterTable>().ToListAsync();
                if (studentTableData.Count() > 0)
                {
                    for (int i = 0; i < studentTableData.Count(); i++)
                    {
                        ItemCell _attendanceModel = new ItemCell();
                        _attendanceModel.code = studentTableData[i].Obj1.Id;
                        _attendanceModel.id = studentTableData[i].Obj2.StudentMaster_Code.ToString();
                        _attendanceModel.name = studentTableData[i].Obj2.StudentName;
                        var imageName = studentTableData[i].Obj2.StudentMaster_Code.ToString()+"_"+studentTableData[i].Obj2.StudentName;
                        var imagepath = DependencyService.Get<IPicture>().GetPictureFromDisk(imageName);
                        _attendanceModel.ImageUri = imagepath;

                        if (studentTableData[i].Obj2.Status == true)
                        {
                            _attendanceModel.status = "Present" ;
                        }
                        else
                        {
                            _attendanceModel.status = "Absent";
                        }

                        listattendanceModel.Add(_attendanceModel);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listattendanceModel.ToList();
        }
#endregion

        public Task<List<ConnectionTable>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<ConnectionTable>("SELECT * FROM [ConnectionTable]");
        }

        public Task<T> GetItemsbyId<T>(string tableName,string columnName,int code)
        {
            
            var name_value = database.ExecuteScalarAsync<T>("SELECT "+ columnName  +" FROM ["+ nameof(T) +"] where code=" + code+ " ");
            //if (name_value)
            //{
                
            //}
            return name_value;
        }

        public Task<LogInTable> GetItemAsync(int code)
        {
            return database.Table<LogInTable>().Where(i => i.Code == code).FirstOrDefaultAsync();
        }


        public Task<List<LogInTable>> GetCodeAsync(string tableName, string fieldName, string fieldValue)
        {
            //return database.Table<LogInTable>().Where(i => i.ID == email).FirstOrDefaultAsync();
            return database.QueryAsync<LogInTable>("SELECT * FROM " + tableName + " WHERE " + fieldName + "='" + fieldValue + "'");

        }

        public async Task<List<ClassMasterTable>> GetClassAsync()
        {
            //return database.Table<LogInTable>().Where(i => i.ID == email).FirstOrDefaultAsync();
            try
            {
                return await database.Table<ClassMasterTable>().Where(i => i.ClassName != "").ToListAsync();   
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public async Task<List<StreamMasterTable>> GetStreamAsync()
        {
            //return database.Table<LogInTable>().Where(i => i.ID == email).FirstOrDefaultAsync();
            try
            {
                return await database.Table<StreamMasterTable>().Where(i => i.StreamName != "").ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<SectionMasterTable>> GetSectionAsync()
        {
            //return database.Table<LogInTable>().Where(i => i.ID == email).FirstOrDefaultAsync();
            try
            {
                return await database.Table<SectionMasterTable>().Where(i => i.SectionName != "").ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<LogInTable>> GetLastRowData(string tableName)
        {
            return NonAsyncdatabaseConnection.Query<LogInTable>("SELECT * FROM [" + tableName + "] order by ID desc limit 1");
        }

        public async void UpdateLogInAuthAsync(string AuthID, string Email)
        {
            try
            {
                await database.QueryAsync<LogInTable>("Update [logInTable] set APItoken='" + AuthID + "' WHERE Email='" + Email + "'");
            }
            catch (SQLiteException ex)
            {

            }
        }

        #region connection table
        public async void SaveConnectionAsync(ConnectionTable details)
        {
            try
            {
                if (database.Table<ConnectionTable>().CountAsync().Result >0)
                {
                    await database.ExecuteAsync("delete from [ConnectionTable]");  
                }
                    await database.InsertAsync(details);
            }
            catch (SQLiteException ex)
            {

            }
        }

        public Task<List<ConnectionTable>> GetConnectionData()
        {
            return database.QueryAsync<ConnectionTable>("Select * from [ConnectionTable]");
        }
        #endregion


        public async Task SaveItemAsync(LogInTable details, Boolean update)
        {
            try
            {
                byte[] bitmap = details.Image; //Whatever you do to get your image into a byte array
                string imageName=string.Empty;
                if (bitmap != null)
                {
                    imageName = details.Code.ToString() + "_" + details.Name.ToString();
                    DependencyService.Get<IPicture>().SavePictureToDisk(imageName, bitmap);
                    details.ImageName = imageName;
                }
                CrossSettings.Current.AddOrUpdateValue("UserImage", imageName);
                CrossSettings.Current.AddOrUpdateValue("UserEmailID", details.Email);
                CrossSettings.Current.AddOrUpdateValue("UserDisplayName", details.Name);
                //await database.ExecuteAsync("delete from [LogInTable]");
                //await database.InsertAsync(details);
                NonAsyncdatabaseConnection.Execute("delete from [LogInTable]");
                NonAsyncdatabaseConnection.Insert(details);
            }
            catch (SQLiteException ex)
            {

            }
        }

        public async Task<bool> SaveFacultyAsync(EMobileFaculty facultyMaster,
                                           IList<EMobileFacultyTransaction> facultyTransaction,
                                           IList<EMobileClass> facultyClass,
                                           IList<EMobileSection> facultySections,
                                           IList<EMobileStream> facultyStream,
                                           IList<EMobileParent> facultyStudentParent,
                                           IList<EMobileStudent> facultyStudents,
                                                 IList<EMobileSubject> facultySubject,IList<EMobileFaculty> facultyMasters,EMobileStudent facultyStudent,EMobileParent Parent)
        {
            
            bool SaveStatus = false;
            await database.ExecuteAsync("Delete from FacultyMasterTable");
            await database.ExecuteAsync("Delete from FacultyTransactionTable");
            await database.ExecuteAsync("Delete from ClassMasterTable");
            await database.ExecuteAsync("Delete from ClassTransactionTable");
            await database.ExecuteAsync("Delete from SectionMasterTable");
            await database.ExecuteAsync("Delete from StreamMasterTable");
            await database.ExecuteAsync("Delete from ParentMasterTable");
            await database.ExecuteAsync("Delete from StudentMasterTable");
            await database.ExecuteAsync("Delete from SubjectMasterTable");

            try
            {
                if (facultyMaster != null)
            {
                FacultyMasterTable FMTable = new FacultyMasterTable();
                FMTable.Code = facultyMaster.Code;
                FMTable.ConfirmPassword = facultyMaster.ConfirmPassword;
                FMTable.EmailAddress = facultyMaster.EmailAddress;
                FMTable.EntryDate = facultyMaster.JoinDate;
                FMTable.FacultyName = facultyMaster.FacultyName;
                FMTable.FacultyPassword = facultyMaster.Password;
                FMTable.ID = facultyMaster.Code;
                FMTable.IsClassIncharge = Convert.ToBoolean(facultyMaster.IsClassIncharge);
                FMTable.PhoneNo = facultyMaster.MobileNo;
                FMTable.Qualification = facultyMaster.Qualification;
                byte[] bitmap = facultyMaster.FacultyImage; //Whatever you do to get your image into a byte array
                //var image=ImageSource.FromStream(() => new MemoryStream(bitmap));
                var imageName = facultyMaster.Code.ToString();
                    if (bitmap != null)
                    {
                        DependencyService.Get<IPicture>().SavePictureToDisk(imageName, bitmap);
                        FMTable.ImageName = imageName;
                    }
                
                
                await database.InsertAsync(FMTable);

                    if (facultyTransaction.Count > 0)
                    {
                        for (int i = 0; i < facultyTransaction.Count; i++)
                        {
                            FacultyTransactionTable FMTTable = new FacultyTransactionTable()
                            {
                             FacultyMaster_Code = facultyMaster.Code,
                              ClassMaster_Code = facultyTransaction[i].ClassMaster_Code,
                                SubjectMaster_Code = facultyTransaction[i].StreamMaster_Code,
                                SectionMaster_Code = facultyTransaction[i].SectionMaster_Code,
                                IsClassIncharge =facultyTransaction[i].IsClassIncharge
                            };
                            await database.InsertAsync(FMTTable);
                        }
                    }

                    //here we save class
                    if (facultyClass.Count >0)
                    {
                        
                    }
            }
            }
            catch (Exception ex)
            {
                MSGFailure = "Fail from faculty data saving";
            }
            //here we save faculty class
            try
            {
                    if (facultyClass != null )
                    {
                        IList<ClassMasterTable> listClassMTable=new List<ClassMasterTable>();
                        IList<ClassTransactionTable> listClassMTTable = new List<ClassTransactionTable>();
                        int count = facultyClass.Count;
                        for (int i = 0; i < facultyClass.Count; i++)
                        {
                            ClassMasterTable ClassMTable = new ClassMasterTable()
                            {
                                Code = facultyClass[i].Code,
                                BoardMaster_Code = facultyClass[i].SubjectCode,
                                ClassName = facultyClass[i].ClassName,
                                SectionMaster_Code = facultyClass[i].SectionCode,
                                StreamMaster_Code = facultyClass[i].StreamCode,
                                EntryDate = facultyClass[i].EntryDate
                            };
                            listClassMTable.Add(ClassMTable);
                            Console.WriteLine(ClassMTable);

                            ClassTransactionTable ClassMTTable = new ClassTransactionTable()
                            {
                                ClassMaster_Code = facultyClass[i].StreamCode,
                                SubjectMaster_Code = facultyClass[i].SubjectCode
                            };
                            listClassMTTable.Add(ClassMTTable);
                        }
                        await database.InsertAllAsync(listClassMTable);
                        await database.InsertAllAsync(listClassMTTable);
                    }
            }
            catch (Exception ex)
            {
                MSGFailure = "Fail from faculty class saveing";
            }
            //here we save sections data
            try
            {
                    if (facultySections != null)
                    {
                        IList<SectionMasterTable> listSectionMTable = new List<SectionMasterTable>();

                        for (int i = 0; i < facultySections.Count; i++)
                        {
                            SectionMasterTable SectionMTable = new SectionMasterTable()
                            {
                                Code = facultySections[i].Code,
                                SectionName = facultySections[i].SectionName,
                                SortOrder = facultySections[i].SortOrder
                            };
                            listSectionMTable.Add(SectionMTable);
                        }
                        await database.InsertAllAsync(listSectionMTable);
                    }
            }
            catch (Exception ex)
            {
                MSGFailure = "Fail from faculty section saveing";
            }
            //here we save stream data
            try
            {
                    if (facultyStream != null)
                    {
                        IList<StreamMasterTable> listStreamMTable = new List<StreamMasterTable>();

                        for (int i = 0; i < facultyStream.Count; i++)
                        {
                            StreamMasterTable StreamMTable = new StreamMasterTable()
                            {
                                Code = facultyStream[i].Code,
                                StreamName = facultyStream[i].StreamName,
                                SortOrder = facultyStream[i].SortOrder
                            };
                            listStreamMTable.Add(StreamMTable);
                        }
                        await database.InsertAllAsync(listStreamMTable);
                    }
            }
            catch (Exception ex)
            {
                MSGFailure = "Fail from faculty stream saveing";
            }
            //here we save studen parent data
            try
            {
                    if (facultyStudentParent != null)
                    {
                        IList<ParentMasterTable> listParentMTable = new List<ParentMasterTable>();

                        for (int i = 0; i < facultyStudentParent.Count; i++)
                        {
                            ParentMasterTable ParentMTable = new ParentMasterTable()
                            {
                                SNo = i + 1,
                                Code = facultyStudentParent[i].Code,
                                Address = facultyStudentParent[i].Address,
                                Email = facultyStudentParent[i].EmailAddress,
                                GaurdianName = facultyStudentParent[i].GaurdianName,
                                MobileNo = facultyStudentParent[i].MobileNo
                            };
                            listParentMTable.Add(ParentMTable);
                        }
                        await database.InsertAllAsync(listParentMTable);
                    }
            }
            catch (Exception ex)
            {
                MSGFailure = "Fail from faculty student parent saveing";
            }
            try
            {
                if (Parent != null)
                {
                        ParentMasterTable listParentMTable = new ParentMasterTable();

                    ParentMasterTable ParentMTable = new ParentMasterTable()
                    {
                        SNo = 1,
                        Code = Parent.Code,
                        Address = Parent.Address,
                        Email = Parent.EmailAddress,
                        GaurdianName = Parent.GaurdianName,
                        MobileNo = Parent.MobileNo
                    };
                    await database.InsertAsync(ParentMTable);
                }
            }
            catch (Exception ex)
            {
                MSGFailure = "Fail from faculty student parent saveing";
            }
            //here we save faculty student
            try
            {
                    if (facultyStudents != null)
                    {
                        IList<StudentMasterTable> listStudentMTable = new List<StudentMasterTable>();

                        for (int i = 0; i < facultyStudents.Count; i++)
                        {

                            byte[] bitmap = facultyStudents[i].Image; //Whatever you do to get your image into a byte array
                            string imageName = string.Empty;
                            if (bitmap != null)
                            {
                                imageName = facultyStudents[i].Code.ToString() + "_" + facultyStudents[i].StudentName;
                                DependencyService.Get<IPicture>().SavePictureToDisk(imageName, bitmap);
                            }
                            StudentMasterTable StudentMTable = new StudentMasterTable()
                            {
                                SNo = i + 1,
                                Code = facultyStudents[i].Code,
                                Address = facultyStudents[i].Address,
                                Email = facultyStudents[i].EmailAddress,
                                ClassMaster_Code = facultyStudents[i].ClassCode,
                                MobileNo = facultyStudents[i].MobileNo,
                                ParentMaster_Code = facultyStudents[i].ParentCode,
                                SectionMaster_Code = facultyStudents[i].SectionCode,
                                StreamMaster_Code = facultyStudents[i].StreamCode,
                                StudentName = facultyStudents[i].StudentName,
                                ImageName = imageName
                            };
                            listStudentMTable.Add(StudentMTable);
                        }
                        await database.InsertAllAsync(listStudentMTable);
                    }
            }
            catch (Exception ex)
            {
                MSGFailure = "Fail from faculty student saveing";
            }
             try
            {
                if (facultyStudent != null)
                {
                        StudentMasterTable listStudentMTable = new StudentMasterTable();

                            byte[] bitmap = facultyStudent.Image; //Whatever you do to get your image into a byte array
                            string imageName = string.Empty;
                    if (bitmap != null)
                    {
                        imageName = facultyStudent.Code.ToString() + "_" + facultyStudent.StudentName;
                        DependencyService.Get<IPicture>().SavePictureToDisk(imageName, bitmap);
                    }
                    StudentMasterTable StudentMTable = new StudentMasterTable()
                    {
                        SNo = 1,
                        Code = facultyStudent.Code,
                        Address = facultyStudent.Address,
                        Email = facultyStudent.EmailAddress,
                        ClassMaster_Code = facultyStudent.ClassCode,
                        MobileNo = facultyStudent.MobileNo,
                        ParentMaster_Code = facultyStudent.ParentCode,
                        SectionMaster_Code = facultyStudent.SectionCode,
                        StreamMaster_Code = facultyStudent.StreamCode,
                        StudentName = facultyStudent.StudentName,
                        ImageName = imageName
                    };
                    await database.InsertAsync(StudentMTable);
                    }
            }
            catch (Exception ex)
            {
                MSGFailure = "Fail from faculty student saveing";
            }
            //here we save faculty subject
            try
            {
                    if (facultySubject != null)
                    {
                        IList<SubjectMasterTable> listSubjectMTable = new List<SubjectMasterTable>();

                        for (int i = 0; i < facultySubject.Count; i++)
                        {
                            SubjectMasterTable SubjectMTable = new SubjectMasterTable()
                            {
                                Code = facultySubject[i].Code,
                                SubjectName = facultySubject[i].SubjectName,
                                SortOrder = facultySubject[i].SortOrder
                            };
                            listSubjectMTable.Add(SubjectMTable);
                        }
                        await database.InsertAllAsync(listSubjectMTable);
                    }
                SaveStatus = true;
            }
            catch (Exception ex)
            {
                MSGFailure = "Fail from faculty subject saveing";
            }

            try
            {
                if (facultyMasters != null)
                {
                    IList<FacultyMasterTable> lstFMTable = new List<FacultyMasterTable>();
                    for (int i = 0; i < facultyMasters.Count(); i++)
                    {
                        if (facultyMasters[i].Code > 0)
                        {
                            FacultyMasterTable FMTable = new FacultyMasterTable();
                            FMTable.Code = facultyMasters[i].Code;
                            FMTable.ConfirmPassword = facultyMasters[i].ConfirmPassword;
                            FMTable.EmailAddress = facultyMasters[i].EmailAddress;
                            FMTable.EntryDate = facultyMasters[i].JoinDate;
                            FMTable.FacultyName = facultyMasters[i].FacultyName;
                            FMTable.FacultyPassword = facultyMasters[i].Password;
                            FMTable.ID = facultyMasters[i].Code;
                            FMTable.IsClassIncharge = Convert.ToBoolean(facultyMasters[i].IsClassIncharge);
                            FMTable.PhoneNo = facultyMasters[i].MobileNo;
                            FMTable.Qualification = facultyMasters[i].Qualification;
                            byte[] bitmap = facultyMasters[i].FacultyImage; //Whatever you do to get your image into a byte array
                                                                            //var image=ImageSource.FromStream(() => new MemoryStream(bitmap));
                            if (bitmap != null)
                            {
                                var imageName = facultyMasters[i].Code.ToString();
                                DependencyService.Get<IPicture>().SavePictureToDisk(imageName, bitmap);
                                FMTable.ImageName = imageName; 
                            }
                            //await database.InsertAsync(FMTable);
                            if (facultyTransaction != null)
                            {
                                if (facultyTransaction.Count > 0)
                                {
                                    IList<FacultyTransactionTable> lstT = new List<FacultyTransactionTable>();
                                    for (int j = 0; j < facultyTransaction.Count; j++)
                                    {
                                        FacultyTransactionTable FMTTable = new FacultyTransactionTable()
                                        {
                                            FacultyMaster_Code = facultyMaster.Code,
                                            ClassMaster_Code = facultyTransaction[j].ClassMaster_Code,
                                            SubjectMaster_Code = facultyTransaction[j].StreamMaster_Code,
                                            SectionMaster_Code = facultyTransaction[j].SectionMaster_Code,
                                            IsClassIncharge = facultyTransaction[j].IsClassIncharge
                                        };
                                        lstT.Add(FMTTable);
                                    }
                                    await database.InsertAllAsync(lstT);
                                }
                            }
                            lstFMTable.Add(FMTable);
                        }
                    }
                    await database.InsertAllAsync(lstFMTable);
                }
            }
            catch (Exception ex)
            {
                MSGFailure = "Fail from faculty data saving";
            }

            return SaveStatus;
        }
        /// <summary>
        /// Ts the get subject code.
        /// </summary>
        /// <returns>The get subject code.</returns>
        /// <param name="subjectCode">Subject code.</param>
        public  string t_GetSubjectCode(int subjectCode)
        {
            try
            {
                var asyncSqldb_query = database.Table<SubjectMasterTable>().Where(a => a.Code == subjectCode).FirstOrDefaultAsync();
                if (asyncSqldb_query.Result != null)
                {
                    if (!string.IsNullOrEmpty(asyncSqldb_query.Result.SubjectName))
                    {
                        return asyncSqldb_query.Result.SubjectName;
                    }
                    else
                    {
                        return "Break";
                    }
                }
            }
            catch(Exception ex)
            {
                
            }
            return null;
        }
        public async Task<bool> SaveStudentAsync(EStudentAttendance students)
        {
            bool SaveStatus = false;
            await database.ExecuteAsync("Delete from " + Constants.attendanceMasterTable);
            await database.ExecuteAsync("Delete from " + Constants.attendanceTransactionTable);
            try
            {
                
                if (students.StudentAttendance.Count > 0)
                {
                    IList<AttendanceMasterTable> ListAttendanceMTable = new List<AttendanceMasterTable>();
                    IList<AttendanceTransactionTable> ListAttendanceTTable = new List<AttendanceTransactionTable>();

                    for (int i = 0; i < students.StudentAttendance.Count; i++)
                    {
                        //save attendance master code
                        AttendanceMasterTable AttendanceMTable = new AttendanceMasterTable()
                        {
                            Id = students.StudentAttendance[i].Code,
                        ClassMaster_Code = students.StudentAttendance[i].ClassMaster_Code,
                        FacultyMaster_Code = students.StudentAttendance[i].FacultyMaster_Code,
                        SectionMaster_Code = students.StudentAttendance[i].SectionMaster_Code,
                        StreamMaster_Code = students.StudentAttendance[i].StreamMaster_Code,
                        SubjectMaster_Code = students.StudentAttendance[i].SubjectMaster_Code,
                        AttendanceDate = Convert.ToDateTime(students.StudentAttendance[i].AttendanceDateString).ToString("yyyy-MM-dd")
                        };
                        for (int j = 0; j < students.StudentAttendance[i].AttendanceTransaction.Count; j++)
                        {
                            //save attendace transaction
                            AttendanceTransactionTable AttendanceTTable = new AttendanceTransactionTable()
                            {
                                Code=students.StudentAttendance[i].AttendanceTransaction[j].Code,
                                AttendanceMaster_Code =students.StudentAttendance[i].AttendanceTransaction[j].AttendanceMaster_Code,
                                FacultyMaster_Code = students.StudentAttendance[i].AttendanceTransaction[j].FacultyMaster_Code,
                                StudentMaster_Code = students.StudentAttendance[i].AttendanceTransaction[j].StudentMaster_Code,
                                StudentName = students.StudentAttendance[i].AttendanceTransaction[j].StudentName,
                                Status = students.StudentAttendance[i].AttendanceTransaction[j].Status,
                                //here we are set attendace status true because all ready its come form server
                                AttandanceStatus=0,
                            };
                            ListAttendanceTTable.Add(AttendanceTTable);
                        }
                        ListAttendanceMTable.Add(AttendanceMTable);   
                    }
                    await database.InsertAllAsync(ListAttendanceMTable);
                    await database.InsertAllAsync(ListAttendanceTTable);
                }
                SaveStatus = true;
            }
            catch (Exception ex)
            {
                MSGFailure = "Fail from faculty data saving";
            }

            return SaveStatus;
        }

        //check class incharge
        public bool checkClassIncharge(int classMasterCode, int sectionMasterCode = 0, int subjectMasterCode = 0)
        {
            try
            {
                var data = database.Table<FacultyTransactionTable>().Where(a => a.ClassMaster_Code==classMasterCode && a.SectionMaster_Code==sectionMasterCode && a.SubjectMaster_Code==subjectMasterCode && a.IsClassIncharge==true ).ToListAsync();
                if ((data != null))
                {
                    if (data.Result.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error on checkClassIncharge : "+ex.Message);
            }
            return false;
        }
        //check entry of attendace in database
        public  string checkStudentAttendance(int classMaster_Code, int sectionMaster_Code, int streamMaster_Code, DateTime datetime, DateTime todatetime)
        {
            string codeString = "";
            try
            {
                //if (todatetime == "")
                //{
                string sqldb_query = "select * from AttendanceMasterTable where ClassMaster_Code=" + classMaster_Code + " And SectionMaster_Code=" + sectionMaster_Code + " And StreamMaster_Code=" + streamMaster_Code + " And AttendanceDate='" + Convert.ToDateTime(datetime).ToString("yyyy-MM-dd") + "' group by Id ;";
                //}
                //else
                //{
                //string  sqldb_query = "select code from AttendanceMaster where ClassMaster_Code=" + classMaster_Code + " And SectionMaster_Code=" + sectionMaster_Code + " And StreamMaster_Code=" + streamMaster_Code + " And AttendanceDate between '" + Convert.ToDateTime(datetime).ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + Convert.ToDateTime(todatetime).ToString("yyyy-MM-dd HH:mm:ss") + "' group by code ;";
                //}
                //var data = database.Table<AttendanceMasterTable>().Where(q => q.ClassMaster_Code == classMaster_Code && q.SectionMaster_Code == sectionMaster_Code && q.StreamMaster_Code == streamMaster_Code ).ToListAsync();
                var data = database.QueryAsync<AttendanceMasterTable> (sqldb_query); //Table<AttendanceMasterTable>().Where(q => q.ClassMaster_Code == classMaster_Code && q.SectionMaster_Code == sectionMaster_Code && q.StreamMaster_Code == streamMaster_Code && q.AttendanceDate.Date ==Convert.ToDateTime(datetime.Date).ToString("yyyy-MM-dd HH:mm:ss") ).ToListAsync();
                if (data.Result.Count > 0)
                {
                    if (data.Result.Count > 0)
                    {
                        for (int i = 0; i < data.Result.Count; i++)
                        {
                            codeString = codeString + Convert.ToString(data.Result[i].Id) + ",";
                        }
                        codeString = codeString.Remove(codeString.Length - 1);
                        return codeString;
                    }
                } 
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }
        //get max code
        public int GetMaxCode(string tableName)
        {
            try
            {
                var data = database.Table<AttendanceMasterTable>().ToListAsync();
                var datat=database.Table<AttendanceMasterTable>().ToListAsync();
                if (data.Result.Count > 0)
                {
                    int max = data.Result.Max(a => a.Id);
                    if (max > 0)
                    {
                        return max == 0 ? 1 : (max + 1);
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 1;
                }
                //var data = database.Table<AttendanceMasterTable>().Where(a => a. );
               
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        public int GetCode(string tableName,DateTime date)
        {
            try
            {
                var data = database.QueryAsync<AttendanceMasterTable>("SELECT Id FROM " + tableName + " where AttendanceDate='" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "' ; ");
                //var data = database.Table<AttendanceMasterTable>().Where(a => a. );
                if (data.Result.Count > 0)
                {
                    return data.Result[0].Id;
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        //end 
        /// <summary>
        /// Saves the attandance master data.
        /// </summary>
        /// <returns>The attandance master data.</returns>
        /// <param name="Code">Code.</param>
        /// <param name="_AttendanceMasterTable">Attendance master table.</param>
        public  void saveAttandanceMasterData(string Code,AttendanceMasterTable _AttendanceMasterTable)
        {
            try
            {
                string _query=string.Empty;
                //database.ExecuteAsync("delete from [AttendanceMasterTable]");  
                var data = database.Table<AttendanceMasterTable>().ToListAsync();
                //data.Result;
                if (string.IsNullOrEmpty(Code))
                {
                     database.InsertAsync(_AttendanceMasterTable);
                }
                else
                {
                    _query = "Update AttendanceMasterTable set UpdateDate=";
                    _query += "'" + Convert.ToDateTime(_AttendanceMasterTable.AttendanceDate).ToString("yyyy-MM-dd") + "'  where Id=" + Code + "  ;";
                    var announcementToUpdate =database.QueryAsync<AttendanceMasterTable>(_query);
                    if (announcementToUpdate.Result.Count > 0)
                    {
                        Console.WriteLine("UPDATED");
                    }
                }
               // return Code;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
         //   return "";
        }
        public bool saveAttandanceTransactionData(string AttandanceMaster_Code,SelectableObservableCollection<ItemCell> _student)
        {
            try
            {
                string sqldb_query = string.Empty;
                IList<AttendanceTransactionTable> listAttendanceTransactionTable=new List<AttendanceTransactionTable>();

                var data = database.Table<AttendanceTransactionTable>().ToListAsync().Result;
                var checkData = database.QueryAsync<AttendanceTransactionTable>("Delete from AttendanceTransactionTable where AttendanceMaster_Code=" + AttandanceMaster_Code + "; ");
                int i = 1;
                foreach (var element in _student)
                {
                    AttendanceTransactionTable _AttendanceTransactionTable = new AttendanceTransactionTable();
                    _AttendanceTransactionTable.Code = i;
                    _AttendanceTransactionTable.AttandanceStatus = 0;
                    _AttendanceTransactionTable.AttendanceMaster_Code = Convert.ToInt32(AttandanceMaster_Code);
                    _AttendanceTransactionTable.FacultyMaster_Code =Convert.ToInt32(CrossSettings.Current.GetValueOrDefault("FacultyId", 0));
                    _AttendanceTransactionTable.StudentMaster_Code = element.Data.code;
                    _AttendanceTransactionTable.StudentName = element.Data.name;
                    _AttendanceTransactionTable.Status = element.IsSelected;
                    listAttendanceTransactionTable.Add(_AttendanceTransactionTable);
                    //int checkCount =await database.DeleteAsync(_AttendanceTransactionTable);
                    i++;
                }
                var status = database.InsertAllAsync(listAttendanceTransactionTable);
                //get Code
                return true;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //save data of time table
        public async Task<int>   save_DownloadTimeTableResponse(TimeTableResponse df_Metadata)
        {
            int timetablemasterCode = 0;
            StringBuilder query = new StringBuilder();
            try
            {
                //Clear data from time table master
                await   database.ExecuteAsync("delete from [TimeTableMaster]");
                await database.ExecuteAsync("delete from [TimeTableTransaction]");
                if (!string.IsNullOrWhiteSpace(df_Metadata.TimeTable.ToString()))
                {
                    int count = df_Metadata.TimeTable.Count;
                    for (int i = 0; i < count; i++)
                    {
                        query.Clear();
                        int SNo = i + 1;
                        query.Append("INSERT INTO TimeTableMaster(Code,ClassMaster_Code,StreamMaster_Code,SectionMaster_Code,EntryDate,IsSent)");
                        query.Append("values(" + df_Metadata.TimeTable[i].Code + "," + df_Metadata.TimeTable[i].ClassMaster_Code + "," + df_Metadata.TimeTable[i].StreamMaster_Code + "," + df_Metadata.TimeTable[i].SectionMaster_Code + ",'" + Convert.ToDateTime(df_Metadata.TimeTable[i].EntryDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + df_Metadata.TimeTable[i].IsSent + "')");
                        await database.ExecuteAsync(query.ToString());
                        timetablemasterCode = df_Metadata.TimeTable[i].Code;
                        //here save time table transaction data
                        int tcount = df_Metadata.TimeTable[i].TableTransactions.Count;

                        IList<TimeTableTransaction> TTTransaction = new List<TimeTableTransaction>();
                        for (int j = 0; j < tcount; j++)
                        {
                            TimeTableTransaction Transaction = new TimeTableTransaction()
                            {
                                Code = df_Metadata.TimeTable[i].TableTransactions[j].Code,
                                TimeTableMaster_Code=df_Metadata.TimeTable[i].TableTransactions[j].TimeTableMaster_Code,
                                DayofWeek=df_Metadata.TimeTable[i].TableTransactions[j].DayofWeek,
                                Description=df_Metadata.TimeTable[i].TableTransactions[j].Description,
                                RowNo=df_Metadata.TimeTable[i].TableTransactions[j].RowNo,
                                Lecture1=df_Metadata.TimeTable[i].TableTransactions[j].Lecture1,
                                Lecture2= df_Metadata.TimeTable[i].TableTransactions[j].Lecture2,
                                Lecture3= df_Metadata.TimeTable[i].TableTransactions[j].Lecture3,
                                Lecture4= df_Metadata.TimeTable[i].TableTransactions[j].Lecture4,
                                Lecture5= df_Metadata.TimeTable[i].TableTransactions[j].Lecture5,
                                Lecture6= df_Metadata.TimeTable[i].TableTransactions[j].Lecture6,
                                Lecture7= df_Metadata.TimeTable[i].TableTransactions[j].Lecture7,
                                Lecture8= df_Metadata.TimeTable[i].TableTransactions[j].Lecture8,
                                Lecture9= df_Metadata.TimeTable[i].TableTransactions[j].Lecture9
                            };
                            TTTransaction.Add(Transaction);
                        }
                        await database.InsertAllAsync(TTTransaction);
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return timetablemasterCode;
        }

        public async Task<List<TimeTableModelCell>> getTimeTableData(string datetime, int classmasterCode, int streamMasterCode, int sectionMasterCode)
        {
            int mweak, ymonth;
            Convert.ToDateTime(datetime).ToString("yyyy-MM-dd HH:mm:ss");
            //get month
            ymonth = Convert.ToDateTime(datetime).Month;
            //get weak
            mweak = Convert.ToDateTime(datetime).Day / 7 + 1;
            try
            {
                
                var timetablemaster =await database.Table<TimeTableMaster>().ToListAsync().ConfigureAwait(false);
                var timetableTransaction =await database.Table<TimeTableTransaction>().ToListAsync();
                var timetable =timetableTransaction.Join(timetablemaster, outer => outer.TimeTableMaster_Code,
                                                          inner => inner.Code,
                                                         (outer, inner) => new MergeTimeTableObj { Obj1 = inner, Obj2 = outer });
                
                timetable.ToList();
                var studentTableData = timetable.Where(a => a.Obj1.ClassMaster_Code == classmasterCode && a.Obj1.SectionMaster_Code == sectionMasterCode
                                                       && a.Obj1.StreamMaster_Code == streamMasterCode).OrderBy(a=>a.Obj2.RowNo).ToList();
                List<TimeTableModelCell> _lstTimeTable = new List<TimeTableModelCell>();
                if (studentTableData.Count() > 0)
                {
                    for (int i = 0; i < studentTableData.Count(); i++)
                    {
                        TimeTableModelCell _attendanceModel = new TimeTableModelCell()
                        {
                            DayofWeek  = studentTableData[i].Obj2.DayofWeek,
                            Description = studentTableData[i].Obj2.Description,
                            Lecture1  = studentTableData[i].Obj2.Lecture1,
                            Lecture2 = studentTableData[i].Obj2.Lecture2,
                            Lecture3 = studentTableData[i].Obj2.Lecture3,
                            Lecture4 = studentTableData[i].Obj2.Lecture4,
                            Lecture5 = studentTableData[i].Obj2.Lecture5,
                            Lecture6 = studentTableData[i].Obj2.Lecture6,
                            Lecture7 = studentTableData[i].Obj2.Lecture7,
                            Lecture8 = studentTableData[i].Obj2.Lecture8,
                            Lecture9 = studentTableData[i].Obj2.Lecture9
                        };
                        _lstTimeTable.Add(_attendanceModel);
                    }
                    return _lstTimeTable;
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        #region sync_attendance
        //get student status
        public async Task<bool> getStudentStatus()
        {
            var attendacestatus =await database.Table<AttendanceTransactionTable>().Where(a => a.AttandanceStatus == 0).CountAsync();
            try
            {
                if (attendacestatus > 0)
                {
                    return  true;
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// Binds the students data.
        /// </summary>
        /// <returns>The students data.</returns>
        public async  Task<IList<EClassAttendance>>  bindStudentsData()
        {
            //int FacultyMaster_Code = CrossSettings.Current.GetValueOrDefault("FacultyId", 0);


            var AttendanceMasterT = await database.Table<AttendanceMasterTable>().ToListAsync().ConfigureAwait(false);
            var AttendanceTransactionT = await database.Table<AttendanceTransactionTable>().Where( a => a.AttandanceStatus==0).ToListAsync().ConfigureAwait(false);
            var studentTable = AttendanceMasterT.Join(AttendanceTransactionT, outer => outer.Id,
                       inner => inner.AttendanceMaster_Code,
                       (outer, inner) => new MergeObj { Obj1 = outer, Obj2 = inner });
            var attendaceData = studentTable.OrderBy(a => a.Obj1.Id).ToList();

            var studentMasterTableData = attendaceData.GroupBy(a => a.Obj1).ToList();
            IList<EClassAttendance> masters = new List<EClassAttendance>();

            if (studentMasterTableData.Count > 0)
            {
                for (int j = 0; j < studentMasterTableData.Count; j++)
                    {
                    EClassAttendance classAttendence = new EClassAttendance();
                    classAttendence.Code = studentMasterTableData[j].First().Obj1.Id;
                    classAttendence.ClassMaster_Code = studentMasterTableData[j].First().Obj1.ClassMaster_Code;
                    classAttendence.StreamMaster_Code = studentMasterTableData[j].First().Obj1.StreamMaster_Code;
                    classAttendence.SectionMaster_Code = studentMasterTableData[j].First().Obj1.SectionMaster_Code;
                    classAttendence.FacultyMaster_Code = studentMasterTableData[j].First().Obj1.FacultyMaster_Code;
                    classAttendence.AttendanceDateString = _propertise.ConsoleDateTimeFormat(studentMasterTableData[j].First().Obj1.AttendanceDate);
                    classAttendence.CreatedDateString = _propertise.ConsoleDateTimeFormat(studentMasterTableData[j].First().Obj1.AttendanceDate);
                    classAttendence.UpdateDateString = _propertise.ConsoleDateTimeFormat(studentMasterTableData[j].First().Obj1.UpdateDate);

                    IList<EAttendanceTransaction> transactions = new List<EAttendanceTransaction>();
                    //set transaction data
                        if (attendaceData[j].Obj2.Code > 0)
                        {
                        var attendaceTData = attendaceData.Where(a => a.Obj2.AttendanceMaster_Code == studentMasterTableData[j].First().Obj1.Id).ToList();
                        for (int i = 0; i < attendaceTData.Count; i++)
                                {
                                    EAttendanceTransaction transaction = new EAttendanceTransaction();
                                     transaction.Code = attendaceTData[i].Obj2.Code;
                                    transaction.AttendanceMaster_Code = attendaceTData[i].Obj2.AttendanceMaster_Code;
                                     transaction.FacultyMaster_Code = attendaceTData[i].Obj2.FacultyMaster_Code;
                                     transaction.StudentMaster_Code = attendaceTData[i].Obj2.StudentMaster_Code;
                                     transaction.StudentName = attendaceTData[i].Obj2.StudentName;
                                     transaction.Status = attendaceTData[i].Obj2.Status;
                                    transactions.Add(transaction);;
                                }
                                classAttendence.AttendanceTransaction = transactions;
                        }
                        masters.Add(classAttendence);
                }
            }
            return masters;
        }
        //update student attandance
        public void UpdateStudentStatus()
        {   
            try
            {
                var status = database.QueryAsync<AttendanceTransactionTable>("Update AttendanceTransactionTable set AttandanceStatus=1 WHERE AttandanceStatus=0 ;").Result;
                if(status.Count > 0)
                {
                    Console.WriteLine("Udate AttendanceTransactionTable status total :"+status.Count);
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        public Task<int> DeleteItemAsync(LogInTable details)
        {
            return database.DeleteAsync(details);
        }

        #region save student attendace data from server to mobile
        public async Task<int> syncAttandanceData(StudentAttendanceResponse syncAttedanceData, Boolean deleteAll = false)
        {
            int transactiontableCount = 0;
            string sqldb_query = string.Empty;
            Propertise _propertise = new Propertise();
            if (deleteAll == true)
            {
                await database.ExecuteAsync("Delete  From " + Constants.attendanceMasterTable);
                await database.ExecuteAsync("Delete  From " + Constants.attendanceTransactionTable);
            }
            IList<AttendanceMasterTable> ListAttendanceMTable = new List<AttendanceMasterTable>();
            IList<AttendanceTransactionTable> ListAttendanceTTable = new List<AttendanceTransactionTable>();
            try
            {
                //count attendance student data
                if (syncAttedanceData.Students.StudentAttendance.Count > 0)
                {
                    for (int i = 0; i < syncAttedanceData.Students.StudentAttendance.Count; i++)
                    {
                        var data =await database.Table<AttendanceMasterTable>().ToListAsync();
                        //for delete exist attendace in thise table
                        int fcode = syncAttedanceData.Students.StudentAttendance[i].FacultyMaster_Code;
                        sqldb_query = "Delete  from " + Constants.attendanceMasterTable + " where Id=";
                        sqldb_query += "" + syncAttedanceData.Students.StudentAttendance[i].Code + " AND FacultyMaster_Code=" + fcode + "  AND AttendanceDate='" + Convert.ToDateTime(syncAttedanceData.Students.StudentAttendance[i].AttendanceDateString).ToString("yyyy-MM-dd")  + "' ;";
                        await database.ExecuteAsync(sqldb_query);
                        sqldb_query = "";
                        //save attendance master code
                        AttendanceMasterTable AttendanceMTable = new AttendanceMasterTable()
                        {
                            Id = syncAttedanceData.Students.StudentAttendance[i].Code,
                            ClassMaster_Code = syncAttedanceData.Students.StudentAttendance[i].ClassMaster_Code,
                            FacultyMaster_Code = syncAttedanceData.Students.StudentAttendance[i].FacultyMaster_Code,
                            SectionMaster_Code = syncAttedanceData.Students.StudentAttendance[i].SectionMaster_Code,
                            StreamMaster_Code = syncAttedanceData.Students.StudentAttendance[i].StreamMaster_Code,
                            SubjectMaster_Code = syncAttedanceData.Students.StudentAttendance[i].SubjectMaster_Code,
                            AttendanceDate = Convert.ToDateTime(syncAttedanceData.Students.StudentAttendance[i].AttendanceDateString).ToString("yyyy-MM-dd")
                        };
                        //ListAttendanceMTable.Add(AttendanceMTable);
                        await database.InsertAsync(AttendanceMTable);

                        transactiontableCount = syncAttedanceData.Students.StudentAttendance[i].AttendanceTransaction.Count;
                        for (int j = 0; j < transactiontableCount; j++)
                        {
                            //save attendace transaction 
                            sqldb_query = "Delete from " + Constants.attendanceTransactionTable + " where AttendanceMaster_Code=";
                            sqldb_query += "" + syncAttedanceData.Students.StudentAttendance[i].Code + " AND StudentMaster_Code=" + syncAttedanceData.Students.StudentAttendance[i].AttendanceTransaction[j].StudentMaster_Code + " AND Facultymaster_code=" + syncAttedanceData.Students.StudentAttendance[i].AttendanceTransaction[j].FacultyMaster_Code + "; ";
                            await database.ExecuteAsync(sqldb_query);
                            //save attendace transaction
                            AttendanceTransactionTable AttendanceTTable = new AttendanceTransactionTable()
                            {
                                Code = syncAttedanceData.Students.StudentAttendance[i].AttendanceTransaction[j].Code,
                                AttendanceMaster_Code = syncAttedanceData.Students.StudentAttendance[i].AttendanceTransaction[j].AttendanceMaster_Code,
                                FacultyMaster_Code = syncAttedanceData.Students.StudentAttendance[i].AttendanceTransaction[j].FacultyMaster_Code,
                                StudentMaster_Code = syncAttedanceData.Students.StudentAttendance[i].AttendanceTransaction[j].StudentMaster_Code,
                                StudentName = syncAttedanceData.Students.StudentAttendance[i].AttendanceTransaction[j].StudentName,
                                Status = syncAttedanceData.Students.StudentAttendance[i].AttendanceTransaction[j].Status,
                                //here we are set attendace status true because all ready its come form server
                                AttandanceStatus = 0,
                            };
                            //ListAttendanceTTable.Add(AttendanceTTable);
                            await database.InsertAsync(AttendanceTTable);
                        }
                    }
                    var mtable = await database.Table<AttendanceMasterTable>().ToListAsync().ConfigureAwait(false);
                    var ttable = await database.Table<AttendanceTransactionTable>().ToListAsync().ConfigureAwait(false);

                    return 1;
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        #endregion
    }
    public class TimeTableModelCell
    {
        public string DayofWeek;
        public string Description;
        public int Lecture1;
        public int Lecture2;
        public int Lecture3;
        public int Lecture4;
        public int Lecture5;
        public int Lecture6;
        public int Lecture7;
        public int Lecture8;
        public int Lecture9;
    }
}
