using System;
using SQLite.Net.Attributes;
namespace DVidyaERP.Core.Models.Tables
{
    //LogInTable
    public class AttendanceTransactionTable
    {
        [NotNull]
        public int Code { get; set; }
        public int AttendanceMaster_Code { get; set; }
        public int FacultyMaster_Code { get; set; }
        public int StudentMaster_Code { get; set; }
        [MaxLength(500)]
        public string StudentName { get; set; }
        public bool Status { get; set; }
        public int AttandanceStatus { get; set; }
    }
    //Employ Table
}


