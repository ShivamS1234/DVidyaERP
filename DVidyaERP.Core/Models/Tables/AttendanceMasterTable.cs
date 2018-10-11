using System;
using SQLite.Net.Attributes;
namespace DVidyaERP.Core.Models.Tables
{
    //LogInTable
    public class AttendanceMasterTable
    {
        [PrimaryKey,NotNull]
        public int Id { get; set; }
        public string UpdateDate { get; set; }
        public string AttendanceDate { get; set; }
        public int ClassMaster_Code { get; set; }
        public int StreamMaster_Code { get; set; }
        public int SectionMaster_Code { get; set; }
        public int FacultyMaster_Code { get; set; }
        public int SubjectMaster_Code { get; set; }
    }
    //Employ Table

}


